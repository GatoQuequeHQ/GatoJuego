namespace GatoQueque.GatoJuego.Core.Assets;

internal sealed class PolyphonicSound : Sound
{
	private readonly LinkedList<Sound> _instances;
	private LinkedListNode<Sound> _currentInstance;

	public PolyphonicSound(FileSound sound, Int32 maxPolyphonyCount)
	{
		_instances = [ ];
		for (var i = 0; i < maxPolyphonyCount; i++)
			_instances.AddLast(new AliasSound(sound));
		_currentInstance = _instances.First!;
	}

	internal override Single Volume
	{
		get => _currentInstance.Value.Volume;
		set => _currentInstance.Value.Volume = value;
	}

	internal override Single Pitch
	{
		get => _currentInstance.Value.Pitch;
		set => _currentInstance.Value.Pitch = value;
	}

	internal override Single Pan
	{
		get => _currentInstance.Value.Pan;
		set => _currentInstance.Value.Pan = value;
	}

	internal override Boolean IsPlaying => _currentInstance.Value.IsPlaying;

	internal override void Play()
	{
		_currentInstance.Value.Play();
		_currentInstance = _currentInstance.Next ?? _instances.First!;
	}

	internal override void Pause()
	{
		foreach (var instance in _instances)
			instance.Pause();
	}

	internal override void Resume()
	{
		foreach (var instance in _instances)
			instance.Resume();
	}

	internal override void Stop()
	{
		foreach (var instance in _instances)
			instance.Stop();
	}
}
