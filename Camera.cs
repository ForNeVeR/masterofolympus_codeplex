using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Master_Of_Olympus
{
    public class Camera2D
    {
        protected float m_zoom;
        public Matrix m_transform;
        public Vector2 m_pos;
        protected float m_rotation;

        public Camera2D()
        {
            m_zoom = 1.0f;
            m_rotation = 0.0f;
            m_pos = Vector2.Zero;
        }

        public float Zoom
        {
            get { return m_zoom; }
            set { m_zoom = value; if (m_zoom < 0.1f) m_zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return m_rotation; }
            set { m_rotation = value; }
        }

        public void Move(Vector2 amount)
        {
            m_pos += amount;
        }

        public Vector2 Pos
        {
            get { return m_pos; }
            set { m_pos = value; }
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            m_transform =
              Matrix.CreateTranslation(new Vector3(-m_pos.X, -m_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return m_transform;
        }

        public Vector2 GetMouseAbsolutePos(GraphicsDevice graphics_device, int screen_w, int screen_h)
        {
            float MouseWorldX = (Mouse.GetState().X - graphics_device.Viewport.Width * 0.5f +
                (graphics_device.Viewport.Width * 0.5f + Pos.X) * (float)Math.Pow(Zoom, 3)) /
                  (float)Math.Pow(Zoom, 3);

            float MouseWorldY = ((Mouse.GetState().Y - graphics_device.Viewport.Height * 0.5f +
                (graphics_device.Viewport.Height * 0.5f + Pos.Y) * (float)Math.Pow(Zoom, 3))) /
                    (float)Math.Pow(Zoom, 3);

            MouseWorldX -= screen_w / 2;
            MouseWorldY -= screen_h / 2;

            return new Vector2(MouseWorldX, MouseWorldY);
        }
    }
}


