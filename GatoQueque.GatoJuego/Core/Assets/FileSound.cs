using Raylib_cs;
using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class FileSound : Sound
{
	private const Single DefaultVolume = 1f;
	private const Single DefaultPitch = 1f;
	private const Single DefaultPan = 0.5f;
	private readonly String _filePath;
	private Boolean _disposed;
	private Raylib_cs.Sound _inRam;
	private Single _volume = DefaultVolume;
	private Single _pitch = DefaultPitch;
	private Single _pan = DefaultPan;

	internal FileSound(String filePath)
	{
		_filePath = filePath;
	}

	internal override Single Volume
	{
		get => _volume;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetSoundVolume(Value, value);
			_volume = value;
		}
	}

	internal override Single Pitch
	{
		get => _pitch;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetSoundPitch(Value, value);
			_pitch = value;
		}
	}

	internal override Single Pan
	{
		get => _pan;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetSoundPan(Value, value);
			_pan = value;
		}
	}

	internal Boolean IsLoadedInRam { get; private set; }

	internal Int32 LastUsedTick { get; private set; }

	internal ref Raylib_cs.Sound Value
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToRam();
			return ref _inRam;
		}
	}

	public void Dispose()
	{
		if (_disposed)
			return;

		Unload();
		GC.SuppressFinalize(this);
		_disposed = true;
	}

	internal void LoadToRam()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInRam)
			return;

		var inRam = Raylib.LoadSound(_filePath);
		if (!Raylib.IsSoundValid(inRam))
			throw new AssetLoadException($"Failed to load to the CPU a sound from the file: {_filePath}");

		_inRam = inRam;
		IsLoadedInRam = true;
	}

	internal void Unload()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (!IsLoadedInRam)
			return;

		Raylib.UnloadSound(_inRam);
		IsLoadedInRam = false;
		_volume = DefaultVolume;
		_pitch = DefaultPitch;
		_pan = DefaultPan;
	}

	internal override void Play()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PlaySound(Value);
	}


	internal override void Pause()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PauseSound(Value);
	}

	internal override void Resume()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.ResumeSound(Value);
	}

	internal override void Stop()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.StopSound(Value);
	}

	~FileSound()
	{
		Dispose();
	}
}
