using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Master_Of_Olympus
{
    public class Map
    {
        private Dictionary<int, Texture2D> m_terrain_textures = new Dictionary<int, Texture2D>();
        private Dictionary<int, Texture2D> m_hydra_textures = new Dictionary<int, Texture2D>();
        private Tile[,] m_terrain_tiles;
        private Vector2[,] m_terrain_tiles_absolute_pos;
        private s_Terrain_tiles_info[,] m_terrain_tiles_info;
        private Tile[] m_hydra_tiles;

        private struct s_Terrain_tiles_info
        {
            public TileType type;
        }

        public Map()
        {
            m_terrain_tiles = new Tile[228, 228];
            m_terrain_tiles_absolute_pos = new Vector2[228, 228];
            m_hydra_tiles = new Tile[8];
            m_terrain_tiles_info = new s_Terrain_tiles_info[228, 228];
        }

        public void Draw(ref SpriteBatch sprite_batch)
        {
            for (int i = 0; i < 228; ++i)
                for (int j = 0; j < 228; ++j)
                    m_terrain_tiles[i, j].Draw(ref sprite_batch);

            for (int i = 0; i < 8; ++i)
                m_hydra_tiles[i].Draw(ref sprite_batch);
        }

        public void LoadAllTilesImages(ContentManager content_manager)
        {
            // Load Zeus_Terrain's tiles
            for (int i = 1; i <= 163; ++i)
            {
                string str = "Zeus_land1_" + i.ToString("00000");
                m_terrain_textures.Add(i, content_manager.Load<Texture2D>("Zeus_Terrain/" + str));
            }

            for (int i = 1; i <= 8; ++i)
            {
                string str = "zeus_hydra_" + i.ToString("00000");
                m_hydra_textures.Add(i, content_manager.Load<Texture2D>("Zeus_Hydra/" + str));
            }

            Random random = new Random();

            for (int i = 0; i < 228; ++i)
            {
                for (int j = 0; j < 228; ++j)
                {
                    Texture2D texture_tile = m_terrain_textures[random.Next(106,163)];
                    Vector2 pos = new Vector2();
                    pos.X = (i * texture_tile.Width) + (j % 2) * (texture_tile.Width / 2);
                    pos.Y = j * (texture_tile.Height / 2);

                    m_terrain_tiles_absolute_pos[i, j] = pos;
                    m_terrain_tiles_info[i, j].type = TileType.CLEAN_FIELD;
                    m_terrain_tiles[i, j] = new Tile(pos, new Vector2(i, j), texture_tile);
                }
            }

            for (int i = 0; i < 8; ++i)
            {
                m_hydra_tiles[i] = new Tile(m_terrain_tiles_absolute_pos[random.Next(227), random.Next(227)], new Vector2(),
                    m_hydra_textures[i+1]);
            }
        }
    }
}
