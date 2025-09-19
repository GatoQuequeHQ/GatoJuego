namespace GatoQueque.GatoJuego.Core.World;

internal sealed class BiomeDescriptor
{
	internal BiomeDescriptor(String name, String description)
	{
		Name = name;
		Description = description;
	}

	internal String Name { get; }

	internal String Description { get; }

	internal static BiomeDescriptor NilBiome { get; } = new(String.Empty, String.Empty);
}
