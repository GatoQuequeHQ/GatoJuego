using GatoQueque.GatoJuego.Core.World;
using Microsoft.Extensions.Localization;

namespace GatoQueque.GatoJuego.Core.Content;

internal sealed class TowerCategoryIndex
{
	private readonly IStringLocalizer _localizer;

	public TowerCategoryIndex(IStringLocalizer<TowerCategory> localizer)
	{
		_localizer = localizer;
		ShortRange = new TowerCategory(GetName(nameof(ShortRange)));
	}

	internal TowerCategory ShortRange { get; }

	private String GetName(String id) => _localizer[$"{id}.{nameof(TowerCategory.Name)}"];
}
