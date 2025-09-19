namespace GatoQueque.GatoJuego.Core.World;

internal abstract class Tower
{
	internal abstract TowerDescriptor Descriptor { get; }

	internal abstract void Update();
}
