namespace GatoQueque.GatoJuego.Core.World;

internal class Cell
{
	private protected Cell(BiomeDescriptor biome)
	{
		Biome = biome;
	}

	internal BiomeDescriptor Biome { get; }

	internal static Cell NilCell { get; } = new(BiomeDescriptor.NilBiome);
}
