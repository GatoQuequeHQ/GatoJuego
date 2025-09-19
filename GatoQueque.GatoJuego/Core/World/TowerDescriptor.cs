namespace GatoQueque.GatoJuego.Core.World;

internal sealed class TowerDescriptor
{
	internal TowerDescriptor(String name, String description, TowerCategory category, Int32 price)
	{
		Name = name;
		Description = description;
		Category = category;
		Price = price;
	}

	internal String Name { get; }

	internal String Description { get; }

	internal TowerCategory Category { get; }

	internal Int32 Price { get; }
}
