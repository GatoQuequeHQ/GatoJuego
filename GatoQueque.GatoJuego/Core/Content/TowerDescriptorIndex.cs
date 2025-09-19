using GatoQueque.GatoJuego.Core.World;
using Microsoft.Extensions.Localization;

namespace GatoQueque.GatoJuego.Core.Content;

internal sealed class TowerDescriptorIndex
{
	private readonly IStringLocalizer _localizer;

	public TowerDescriptorIndex(IStringLocalizer<TowerDescriptor> localizer, TowerCategoryIndex categories)
	{
		_localizer = localizer;
		ExampleTower = new TowerDescriptor(GetName(nameof(ExampleTower)), GetDescription(nameof(ExampleTower)),
			categories.ShortRange, 2);
	}

	internal TowerDescriptor ExampleTower { get; }

	private String GetName(String id) => _localizer[$"{id}.{nameof(TowerDescriptor.Name)}"];

	private String GetDescription(String id) => _localizer[$"{id}.{nameof(TowerDescriptor.Description)}"];
}
