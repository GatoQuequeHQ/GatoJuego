using Raylib_cs;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class AliasSound : Sound, IDisposable
{
	private readonly FileSound _original;
	private Boolean _disposed;
	private Raylib_cs.Sound _inRam;
	private Single _pan = AudioConstants.DefaultPan;
	private Single _pitch = AudioConstants.DefaultPitch;
	private Single _volume = AudioConstants.DefaultVolume;

	internal AliasSound(FileSound original)
	{
		_original = original;
	}

	internal ref Raylib_cs.Sound Value
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToRam();
			return ref _inRam;
		}
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

	public void Dispose()
	{
		if (_disposed)
			return;

		Unload();
		GC.SuppressFinalize(this);
		_disposed = true;
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

	internal void LoadToRam()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (IsLoadedInRam)
			return;

		var inRam = Raylib.LoadSoundAlias(_original.Value);
		if (!Raylib.IsSoundValid(inRam))
			throw new AssetLoadException("Failed to create sound alias");

		_inRam = inRam;
		IsLoadedInRam = true;
	}

	internal void Unload()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (!IsLoadedInRam)
			return;

		Raylib.UnloadSoundAlias(_inRam);
		IsLoadedInRam = false;
		_volume = AudioConstants.DefaultVolume;
		_pitch = AudioConstants.DefaultPitch;
		_pan = AudioConstants.DefaultPan;
	}

	~AliasSound()
	{
		Dispose();
	}
}
