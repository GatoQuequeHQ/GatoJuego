namespace GatoQueque.GatoJuego.Core.World;

internal sealed class TowerCell : Cell
{
	internal TowerCell(BiomeDescriptor biome)
		: base(biome)
	{ }

	internal Tower? Tower { get; set; }
}
