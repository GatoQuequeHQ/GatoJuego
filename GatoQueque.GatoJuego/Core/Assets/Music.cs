using Raylib_cs;

namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class Music : IDisposable
{
	private readonly String _filePath;
	private Boolean _disposed;
	private Raylib_cs.Music _inRam;
	private Single _pan = AudioConstants.DefaultPan;
	private Single _pitch = AudioConstants.DefaultPitch;
	private Single _volume = AudioConstants.DefaultVolume;

	internal Music(String filePath)
	{
		_filePath = filePath;
	}

	internal Single Volume
	{
		get => _volume;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetMusicVolume(Value, value);
			_volume = value;
		}
	}

	internal Single Pitch
	{
		get => _pitch;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetMusicPitch(Value, value);
			_pitch = value;
		}
	}

	internal Single Pan
	{
		get => _pan;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetMusicPan(Value, value);
			_pan = value;
		}
	}

	internal TimeSpan Position
	{
		get => TimeSpan.FromSeconds(Raylib.GetMusicTimePlayed(Value));
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SeekMusicStream(Value, (Single)value.TotalSeconds);
		}
	}

	internal Boolean IsLooping
	{
		get => Value.Looping;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Value.Looping = value;
		}
	}

	internal Boolean IsLoadedInRam { get; private set; }

	internal Int32 LastUsedTick { get; private set; }

	internal ref Raylib_cs.Music Value
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

		var inRam = Raylib.LoadMusicStream(_filePath);
		if (!Raylib.IsMusicValid(inRam))
			throw new AssetLoadException($"Failed to load to the CPU music from the file: {_filePath}");

		_inRam = inRam;
		IsLoadedInRam = true;
	}

	internal void Unload()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);

		if (!IsLoadedInRam)
			return;

		Raylib.UnloadMusicStream(_inRam);
		IsLoadedInRam = false;
		_volume = AudioConstants.DefaultVolume;
		_pitch = AudioConstants.DefaultPitch;
		_pan = AudioConstants.DefaultPan;
	}

	internal void Play()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PlayMusicStream(Value);
	}

	internal void Pause()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PauseMusicStream(Value);
	}

	internal void Resume()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.ResumeMusicStream(Value);
	}

	internal void Stop()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.StopMusicStream(Value);
	}

	internal void Update()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.UpdateMusicStream(Value);
	}

	~Music()
	{
		Dispose();
	}
}
