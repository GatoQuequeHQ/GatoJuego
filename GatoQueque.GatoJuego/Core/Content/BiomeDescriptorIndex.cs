using GatoQueque.GatoJuego.Core.World;
using Microsoft.Extensions.Localization;

namespace GatoQueque.GatoJuego.Core.Content;

internal sealed class BiomeDescriptorIndex
{
	private readonly IStringLocalizer _localizer;

	public BiomeDescriptorIndex(IStringLocalizer<BiomeDescriptor> localizer)
	{
		_localizer = localizer;
		ExampleBiome = new BiomeDescriptor(GetName(nameof(ExampleBiome)), GetDescription(nameof(ExampleBiome)));
	}

	internal BiomeDescriptor ExampleBiome { get; }

	private String GetName(String id) => _localizer[$"{id}.{nameof(BiomeDescriptor.Name)}"];

	private String GetDescription(String id) => _localizer[$"{id}.{nameof(BiomeDescriptor.Description)}"];
}
