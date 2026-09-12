using Raylib_cs;

namespace GatoQueque.GatoJuego.Core.Assets;

public sealed class AliasSound : Sound
{
	private readonly FileSound _original;
	private Boolean _disposed;
	private Raylib_cs.Sound _inRam;
	private Single _pan = AudioConstants.DefaultPan;
	private Single _pitch = AudioConstants.DefaultPitch;
	private Single _volume = AudioConstants.DefaultVolume;

	public AliasSound(FileSound original)
	{
		_original = original;
	}

	public ref Raylib_cs.Sound Value
	{
		get
		{
			LastUsedTick = Environment.TickCount;
			LoadToRam();
			return ref _inRam;
		}
	}

	public override Single Volume
	{
		get => _volume;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetSoundVolume(Value, value);
			_volume = value;
		}
	}

	public override Single Pitch
	{
		get => _pitch;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetSoundPitch(Value, value);
			_pitch = value;
		}
	}

	public override Single Pan
	{
		get => _pan;
		set
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			Raylib.SetSoundPan(Value, value);
			_pan = value;
		}
	}

	public override Boolean IsPlaying => Raylib.IsSoundPlaying(_inRam);

	public Boolean IsLoadedInRam { get; private set; }

	public Int32 LastUsedTick { get; private set; }

	public void Dispose()
	{
		if (_disposed)
			return;

		Unload();
		GC.SuppressFinalize(this);
		_disposed = true;
	}

	public override void Play()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PlaySound(Value);
	}

	public override void Pause()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.PauseSound(Value);
	}

	public override void Resume()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.ResumeSound(Value);
	}

	public override void Stop()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
		Raylib.StopSound(Value);
	}

	public void LoadToRam()
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

	public void Unload()
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
