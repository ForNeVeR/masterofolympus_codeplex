using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Master_Of_Olympus
{
    public class Logic
    {
        private int m_scroll_speed = 15;
        private int SCREEN_RESOLUTION_WIDTH, SCREEN_RESOLUTION_HEIGHT;

        public Logic(int screen_res_w, int screen_res_h)
        {
            SCREEN_RESOLUTION_HEIGHT = screen_res_h;
            SCREEN_RESOLUTION_WIDTH = screen_res_w;
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
            if (mouse_state.X <= 0 && mouse_absolute_pos.X > 58/*m_field.Width*/)
                cam.Move(new Vector2(-m_scroll_speed, 0));

            if (mouse_state.Y <= 0 && mouse_absolute_pos.Y > 30/*m_field.Height*/)
                cam.Move(new Vector2(0, -m_scroll_speed));

            if ((mouse_state.X >= SCREEN_RESOLUTION_WIDTH - 1) && (mouse_absolute_pos.X < (58*228) - 1))
                cam.Move(new Vector2(m_scroll_speed, 0));

            if ((mouse_state.Y >= SCREEN_RESOLUTION_HEIGHT - 1) && (mouse_absolute_pos.Y < (30*228) - 1))
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
