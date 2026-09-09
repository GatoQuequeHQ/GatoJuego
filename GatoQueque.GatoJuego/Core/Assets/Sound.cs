namespace GatoQueque.GatoJuego.Core.Assets;

internal abstract class Sound
{
	internal abstract Single Volume { get; set; }
	internal abstract Single Pitch { get; set; }
	internal abstract Single Pan { get; set; }
	internal abstract Boolean IsPlaying { get; }
	internal abstract void Play();
	internal abstract void Pause();
	internal abstract void Resume();
	internal abstract void Stop();
}
