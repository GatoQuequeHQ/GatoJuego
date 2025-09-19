namespace GatoQueque.GatoJuego.Core.World;

internal sealed class PathCell : Cell
{
	internal PathCell(
		BiomeDescriptor biome,
		ReadOnlySpan<PathDirection> fromDirections,
		PathDirection toDirection)
		: base(biome)
	{
		FromDirections = [ ..fromDirections ];
		ToDirection = toDirection;
	}

	internal IReadOnlyCollection<PathDirection> FromDirections { get; }

	internal PathDirection ToDirection { get; }
}
