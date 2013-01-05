using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Master_Of_Olympus
{
    public class Hydra : Tile
    {
        private enum Direction
        {
            RIGHT, LEFT, UP, DOWN, DOWN_LEFT, DOWN_RIGHT, UP_LEFT, UP_RIGHT
        }

        private Vector2 m_old_abs_pos;
        private int[] m_key_anim;
        private Direction m_direction;

        public Hydra(Vector2 pos_absolute, Vector2 pos_relative, Texture2D texture, TileType tile_type)
        {
            m_key_anim = new int[8];
            m_tile_type = tile_type;
            m_pos_absolute = pos_absolute;
            m_pos_relative = pos_relative;
            m_texture_tile = texture;
            m_old_abs_pos = m_pos_absolute;
            m_key_anim[(int)Direction.RIGHT] = 2;
            m_key_anim[(int)Direction.LEFT] = 6;
            m_key_anim[(int)Direction.UP] = 8;
            m_key_anim[(int)Direction.DOWN] = 4;
            m_key_anim[(int)Direction.DOWN_LEFT] = 5;
            m_key_anim[(int)Direction.DOWN_RIGHT] = 3;
            m_key_anim[(int)Direction.UP_LEFT] = 7;
            m_key_anim[(int)Direction.UP_RIGHT] = 1;
            m_direction = Direction.RIGHT;
        }

        private void SetTexture()
        {
            switch (m_direction)
            {
                case Direction.LEFT:
                    m_texture_tile = Resources.hydra_anim_left_textures[m_key_anim[(int)m_direction]];
                    break;

                case Direction.RIGHT:
                    m_texture_tile = Resources.hydra_anim_right_textures[m_key_anim[(int)m_direction]];
                    break;
            }
        }

        private void AnimationHack(int old_width, int old_height)
        {
            int width_diff = m_texture_tile.Width - old_width, height_diff = m_texture_tile.Height - old_height;

            if (width_diff < 0 && m_direction == Direction.RIGHT)
                m_pos_absolute.X += -width_diff + 2;

            else if (width_diff < 0 && m_direction == Direction.LEFT)
                m_pos_absolute.X += -2;

            else if (m_direction == Direction.RIGHT)
                m_pos_absolute.X += -width_diff + 2;

            else
                m_pos_absolute.X += -2;

            if (height_diff < 0)
                m_pos_absolute.Y += -height_diff;

            else
                m_pos_absolute.Y -= height_diff;
        }

        private void Logic()
        {
            m_key_anim[(int)m_direction] += 8;

            if (m_key_anim[(int)Direction.RIGHT] > 122)
                m_key_anim[(int)Direction.RIGHT] = 2;

            if (m_key_anim[(int)Direction.LEFT] > 126)
                m_key_anim[(int)Direction.LEFT] = 6;

            if (m_pos_absolute.X - m_old_abs_pos.X >= 58 && m_direction == Direction.RIGHT)
            {
                m_old_abs_pos.X = m_pos_absolute.X;
                m_pos_relative.X++;
            }

            if (m_pos_absolute.X - m_old_abs_pos.X <= -58 && m_direction == Direction.LEFT)
            {
                m_old_abs_pos.X = m_pos_absolute.X;
                m_pos_relative.X--;
            }
        }

        public void Update()
        {
            if (m_pos_relative.X >= 56)
                m_direction = Direction.LEFT;

            int old_width = m_texture_tile.Width, old_height = m_texture_tile.Height;

            SetTexture();
            AnimationHack(old_width, old_height);
            Logic();
        }
    }
}
