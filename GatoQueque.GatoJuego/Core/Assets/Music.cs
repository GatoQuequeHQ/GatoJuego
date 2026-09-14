using Raylib_cs;

namespace GatoQueque.GatoJuego.Core.Assets;

public sealed class Music : IDisposable
{
	private readonly String _filePath;
	private Boolean _disposed;
	private Raylib_cs.Music _inRam;
	private Single _pan = AudioConstants.DefaultPan;
	private Single _pitch = AudioConstants.DefaultPitch;
	private Single _volume = AudioConstants.DefaultVolume;

	public Music(String filePath)
	{
		_filePath = filePath;
	}

	public Single Volume
	{
		get => _volume;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetMusicVolume(Value, value);
			_volume = value;
		}
	}

	public Single Pitch
	{
		get => _pitch;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetMusicPitch(Value, value);
			_pitch = value;
		}
	}

	public Single Pan
	{
		get => _pan;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetMusicPan(Value, value);
			_pan = value;
		}
	}

	public TimeSpan Position
	{
		get => TimeSpan.FromSeconds(Raylib.GetMusicTimePlayed(Value));
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SeekMusicStream(Value, (Single)value.TotalSeconds);
		}
	}

	public Boolean IsLooping
	{
		get => Value.Looping;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Value.Looping = value;
		}
	}

	public Boolean IsLoadedInRam { get; private set; }

	public Int32 LastUsedTick { get; private set; }

	public ref Raylib_cs.Music Value
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

	public void LoadToRam()
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

	public void Unload()
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

	public void Play()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PlayMusicStream(Value);
	}

	public void Pause()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PauseMusicStream(Value);
	}

	public void Resume()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.ResumeMusicStream(Value);
	}

	public void Stop()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.StopMusicStream(Value);
	}

	public void Update()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.UpdateMusicStream(Value);
	}

	~Music()
	{
		Dispose();
	}
}
