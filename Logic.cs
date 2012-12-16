using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace Master_Of_Olympus
{
    public class Logic
    {
        private int m_scroll_speed = 15;
        private int m_screen_w, m_screen_h;
        private Audio m_audio;

        // CONST
        private const int MIN_FIELD_WIDTH = 58;
        private const int MIN_FIELD_HEIGHT = 30;
        private const int NB_TILES_X = 228;
        private const int NB_TILES_Y = 228;

        public Logic(int screen_w, int screen_h, Audio audio)
        {
            m_screen_h = screen_h;
            m_screen_w = screen_w;
            m_audio = audio;
        }

        public void HandleEventsMenu(ref bool draw_menu, KeyboardState keyboard_state, MouseState mouse_state)
        {
            if (keyboard_state.IsKeyDown(Keys.Enter))
            {
                draw_menu = false;
            }
        }

        public void HandleEventsInGame(KeyboardState keyboard_state, MouseState mouse_state,
            Vector2 mouse_absolute_pos, Camera2D cam)
        {
            if (mouse_state.X <= 0 && mouse_absolute_pos.X > MIN_FIELD_WIDTH)
                cam.Move(new Vector2(-m_scroll_speed, 0));

            if (mouse_state.Y <= 0 && mouse_absolute_pos.Y > MIN_FIELD_HEIGHT)
                cam.Move(new Vector2(0, -m_scroll_speed));

            if ((mouse_state.X >= m_screen_w - 1) && (mouse_absolute_pos.X < (MIN_FIELD_WIDTH * NB_TILES_X) - 1))
                cam.Move(new Vector2(m_scroll_speed, 0));

            if ((mouse_state.Y >= m_screen_h - 1) && (mouse_absolute_pos.Y < (MIN_FIELD_HEIGHT / 2 * NB_TILES_Y) - 1))
                cam.Move(new Vector2(0, m_scroll_speed));

            if (keyboard_state.IsKeyDown(Keys.Subtract))
                cam.Zoom -= 0.1f;

            if (keyboard_state.IsKeyDown(Keys.Add))
                cam.Zoom += 0.1f;

            if (keyboard_state.IsKeyDown(Keys.Multiply))
                cam.Zoom = 1.0f;
        }
    }
}
