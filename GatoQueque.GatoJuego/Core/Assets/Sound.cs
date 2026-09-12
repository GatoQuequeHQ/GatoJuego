namespace GatoQueque.GatoJuego.Core.Assets;

public abstract class Sound
{
	public abstract Single Volume { get; set; }
	public abstract Single Pitch { get; set; }
	public abstract Single Pan { get; set; }
	public abstract Boolean IsPlaying { get; }
	public abstract void Play();
	public abstract void Pause();
	public abstract void Resume();
	public abstract void Stop();
}
