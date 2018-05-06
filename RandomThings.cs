using Terraria.ModLoader;

namespace RandomThings
{
	public class RandomThings : Mod
	{
		public RandomThings()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
	}
}
