using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace RandomThings.Items.Weapons
{
    public class CrusaderHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crusader's Hammer");
            Tooltip.SetDefault("Not inspired by Diablo 3 at all...");
        }
        public override string Texture
        {
            get { return "Terraria/Item_" + ItemID.PaladinsHammer; }
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PaladinsHammer);
            item.shoot = mod.ProjectileType<CrusaderHammerProjectile>();
            item.shootSpeed = 0f;
        }
    }

    public class CrusaderHammerProjectile : ModProjectile
    {
        Vector2 translation = new Vector2(0f, 0f);
        float startingRotation = MathHelper.ToRadians(Main.rand.Next(-359, 359));
        int startingDirection = Main.rand.Next(2) == 1 ? 1 : 0;
        float secondsToLive = 5f;

        public override string Texture
        {
            get { return "Terraria/Projectile_" + ProjectileID.PaladinsHammerFriendly; }
        }
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            // Each frame...
            projectile.ai[0] += 1f;
            projectile.rotation = MathHelper.Pi / 10f * (startingDirection == 1 ? projectile.ai[0] : -projectile.ai[0]);
            projectile.velocity = translation.RotatedBy(startingDirection == 1 ? startingRotation + MathHelper.Pi / 50f * projectile.ai[0] : startingRotation - MathHelper.Pi / 50f * projectile.ai[0]);
            translation.X += 0.1f;
            Dust.NewDust(projectile.position + new Vector2(15f, 15f), 5, 5, DustID.GoldCoin, -translation.RotatedBy(MathHelper.Pi / 50f * projectile.ai[0]).X * 0.1f, -translation.RotatedBy(MathHelper.Pi / 50f * projectile.ai[0]).Y * 0.1f);

            // Managing fading...
            if (projectile.ai[0] > secondsToLive * 60f - 10f)
            {
                // Fading out
                projectile.alpha += 25;
                if (projectile.alpha > 255)
                    projectile.alpha = 255;
            }
            else
            {
                // Fading in
                projectile.alpha -= 25;
                if (projectile.alpha < 0)
                    projectile.alpha = 0;
            }

            // Projectile lifetime
            if (projectile.ai[0] > secondsToLive * 60f)
                projectile.Kill();
        }
    }
}
