using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Master_Of_Olympus
{
    public static class Resources
    {
        public static Dictionary<int, Texture2D> terrain_textures = new Dictionary<int, Texture2D>();
        public static Dictionary<int, Texture2D> hydra_anim_right_textures = new Dictionary<int, Texture2D>();
        public static Dictionary<int, Texture2D> hydra_anim_left_textures = new Dictionary<int, Texture2D>();

        public static void LoadTerrainTextures(ContentManager content_manager)
        {
            for (int i = 1; i <= 163; ++i)
            {
                string str = "Zeus_land1_" + i.ToString("00000");
                terrain_textures.Add(i, content_manager.Load<Texture2D>("Zeus_Terrain/land/" + str));
            }
        }

        public static void LoadHydraTextures(ContentManager content_manager)
        {
            for (int i = 2; i <= 122; i += 8)
            {
                string str = "zeus_hydra_" + i.ToString("00000");
                hydra_anim_right_textures.Add(i, content_manager.Load<Texture2D>("Zeus_Hydra/anim_right/" + str));
            }

            for (int i = 6; i <= 126; i += 8)
            {
                string str = "zeus_hydra_" + i.ToString("00000");
                hydra_anim_left_textures.Add(i, content_manager.Load<Texture2D>("Zeus_Hydra/anim_left/" + str));
            }
        }
    }
}
