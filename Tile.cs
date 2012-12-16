using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Master_Of_Olympus
{
    public enum TileType
    {
        CLEAN_FIELD, WATER, HYDRA
    }

    public enum GenTileType
    {
        BUILDING, MONSTER, UNIT, GOD, SANCTUARY, CLEAN_FIELD
    }

    public class Tile
    {
        protected Texture2D m_texture_tile;
        protected Vector2 m_pos_absolute;
        protected Vector2 m_pos_relative;
        protected TileType m_tile_type;

        public int Width
        {
            get { return m_texture_tile.Width; }
        }

        public int Height
        {
            get { return m_texture_tile.Height; }
        }

        public Vector2 AbsolutePosition
        {
            get { return m_pos_absolute; }
        }

        public Tile()
        {
        }

        public Tile(Vector2 pos_absolute, Vector2 pos_relative, Texture2D texture, TileType tile_type)
        {
            m_tile_type = tile_type;
            m_pos_absolute = pos_absolute;
            m_pos_relative = pos_relative;
            m_texture_tile = texture;
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            sprite_batch.Draw(m_texture_tile, m_pos_absolute, Color.White);
        }
    }
}




