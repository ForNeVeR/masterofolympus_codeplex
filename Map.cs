using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Master_Of_Olympus
{
    public class Map
    {
        private Tile[,] m_terrain_tiles;
        static public Vector2[,] m_terrain_tiles_absolute_pos;
        static public s_Terrain_tiles_info[,] m_map_tiles_info;
        private Hydra m_hydra;
        private DateTime m_datetime_prev, m_datetime;

        private int m_screen_w;
        private int m_screen_h;

        // CONST
        private const int MIN_FIELD_WIDTH = 58;
        private const int MIN_FIELD_HEIGHT = 30;
        private const int NB_TILES_X = 228;
        private const int NB_TILES_Y = 228;

        public struct s_Terrain_tiles_info
        {
            public TileType type;
            public GenTileType gen_type;
        }

        public void SaveMap(string file_name)
        {
            string path = @"C:\Users\Adrien\Desktop\EPITA\Master_Of_Olympus\Master_Of_Olympus\Master_Of_Olympus\map_code\";
            path += file_name;

            if (!File.Exists(path))
            {
                using (StreamWriter file = new StreamWriter(path))
                {
                    for (int i = 0; i < 228; ++i)
                    {
                        for (int j = 0; j < 228; ++j)
                        {
                            int n = (int)Map.m_map_tiles_info[i, j].type;
                            file.Write(n);
                            if (j < 227)
                                file.Write(" ");
                        }

                        file.WriteLine();
                    }
                }
            }
        }

        public Map(int screen_w, int screen_h)
        {
            m_screen_h = screen_h;
            m_screen_w = screen_w;
            m_terrain_tiles = new Tile[NB_TILES_X, NB_TILES_Y];
            m_terrain_tiles_absolute_pos = new Vector2[NB_TILES_X, NB_TILES_Y];
            m_map_tiles_info = new s_Terrain_tiles_info[NB_TILES_X, NB_TILES_Y];
            m_datetime_prev = DateTime.Now;
        }

        public void DrawTerrain(SpriteBatch sprite_batch, Camera2D cam)
        {
            int cam_x = (int)cam.Pos.X - m_screen_w / 2;
            int cam_y = (int)cam.Pos.Y - m_screen_h / 2;
            int x = cam_x / MIN_FIELD_WIDTH;
            int y = cam_y / (MIN_FIELD_HEIGHT / 2) - 1;
            int x_offset = x + m_screen_w / MIN_FIELD_WIDTH + 2;
            int y_offset = y + 4 * m_screen_h / MIN_FIELD_HEIGHT;
            int i, j;

            if (x - 1 < 0)
                i = 0;

            else
                i = x - 1;

            if (x_offset > NB_TILES_X)
                x_offset = 228;

            if (y < 0)
                y = 0;

            if (y_offset > NB_TILES_Y)
                y_offset = 228;

            /*for (; i < x_offset; ++i)
                for (j = y; j < y_offset; ++j)
                    if (m_map_tiles_info[i,j].type == TileType.CLEAN_FIELD)
                        m_terrain_tiles[i, j].Draw(sprite_batch);*/

            for (; i < x_offset; ++i)
                for (j = y; j < y_offset; ++j)
                    if (m_map_tiles_info[i,j].type == TileType.CLEAN_FIELD_105)
                        m_terrain_tiles[i, j].Draw(sprite_batch);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            m_hydra.Draw(sprite_batch);
        }

        public void UpdateUnits()
        {
            m_datetime = DateTime.Now;
            TimeSpan timespan = m_datetime - m_datetime_prev;

            if (timespan.TotalMilliseconds > 30)
            {
                m_hydra.Update();
                m_datetime_prev = DateTime.Now;
            }
        }

        public void LoadAllTilesImages(ContentManager content_manager)
        {
            Random random = new Random();

            // Load Terrain
            Resources.LoadTerrainTextures(content_manager);

            for (int i = 0; i < NB_TILES_X; ++i)
            {
                for (int j = 0; j < NB_TILES_Y; ++j)
                {
                    int rand = random.Next(105, 163);
                    rand = 105;
                    //int rand = random.Next(106, 163);
                    Texture2D texture_tile = Resources.terrain_textures[rand];
                    Vector2 pos = new Vector2();
                    pos.X = (i * texture_tile.Width) + (j % 2) * (texture_tile.Width / 2);
                    pos.Y = j * (texture_tile.Height / 2);

                    m_terrain_tiles_absolute_pos[i, j] = pos;
                    if (rand == 105)
                        m_map_tiles_info[i, j].type = TileType.CLEAN_FIELD_105;
                    else
                        m_map_tiles_info[i, j].type = TileType.CLEAN_FIELD;

                    m_map_tiles_info[i, j].gen_type = GenTileType.CLEAN_FIELD;
                    m_terrain_tiles[i, j] = new Tile(pos, new Vector2(i, j), texture_tile, TileType.CLEAN_FIELD_105);
                }
            }

            SaveMap("test.txt");

            // Load Hydra
            Resources.LoadHydraTextures(content_manager);
            m_hydra = new Hydra(m_terrain_tiles_absolute_pos[50, 50], new Vector2(50, 50),
                Resources.hydra_anim_right_textures[2], TileType.HYDRA);
        }
    }
}
