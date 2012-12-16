using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Master_Of_Olympus
{
    public class Hydra : Tile
    {
        private Vector2 m_old_abs_pos;
        private int m_key_anim_right;

        public Hydra(Vector2 pos_absolute, Vector2 pos_relative, Texture2D texture, TileType tile_type)
        {
            m_tile_type = tile_type;
            m_pos_absolute = pos_absolute;
            m_pos_relative = pos_relative;
            m_texture_tile = texture;
            m_old_abs_pos = m_pos_absolute;
            m_key_anim_right = 2;
        }

        public void Update()
        {
            Console.WriteLine(m_pos_absolute.X);

            if (m_pos_relative.X >= 200)
                return;

            int old_width = m_texture_tile.Width, old_height = m_texture_tile.Height;

            m_texture_tile = Resources.hydra_anim_right_textures[m_key_anim_right];
            int width_diff = m_texture_tile.Width - old_width, height_diff = m_texture_tile.Height - old_height;

            if (width_diff < 0)
                m_pos_absolute.X += -width_diff + 2;

            else
                m_pos_absolute.X -= width_diff - 2;

            if (height_diff < 0)
                m_pos_absolute.Y += -height_diff;

            else
                m_pos_absolute.Y -= height_diff;

            m_key_anim_right += 8;

            if (m_key_anim_right > 122)
                m_key_anim_right = 2;

            if (m_pos_absolute.X - m_old_abs_pos.X >= 50)
            {
                m_old_abs_pos.X = m_pos_absolute.X;
                m_pos_relative.X++;
            }
        }
    }
}
