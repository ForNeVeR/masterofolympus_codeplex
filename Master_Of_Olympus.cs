using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Master_Of_Olympus
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Master_Of_Olympus : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sprite_batch;
        private int m_screen_w;
        private int m_screen_h;
        private Camera2D m_cam;
        private Map m_map;
        private SpriteFont menu_font;
        private bool m_draw_menu = true;
        private Logic m_logic;
        private Audio m_audio;

        private void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferFormat = displayMode.Format;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = displayMode.Width;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = displayMode.Height;

            m_screen_h = displayMode.Height;
            m_screen_w = displayMode.Width;
        }

        public Master_Of_Olympus()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            graphics.IsFullScreen = false;

            this.IsMouseVisible = true;

            Window.Title = "Master of Olympus - PROJET DE SUP EPITA 2017";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            m_cam = new Camera2D();
            m_cam.Pos = new Vector2(m_screen_w * 2, m_screen_h * 2);
            m_map = new Map(m_screen_w, m_screen_h);
            m_audio = new Audio();
            m_logic = new Logic(m_screen_w, m_screen_h, m_audio);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            sprite_batch = new SpriteBatch(GraphicsDevice);
            m_map.LoadAllTilesImages(Content);
            menu_font = Content.Load<SpriteFont>("Menu");
            m_audio.LoadAudioContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard_state = Keyboard.GetState();
            MouseState mouse_state = Mouse.GetState();

            if (keyboard_state.IsKeyDown(Keys.Escape))
                this.Exit();

            if (m_draw_menu)
                m_logic.HandleEventsMenu(ref m_draw_menu, keyboard_state, mouse_state);

            else
            {
                m_logic.HandleEventsInGame(keyboard_state, mouse_state,
                    m_cam.GetMouseAbsolutePos(GraphicsDevice, m_screen_w, m_screen_h), m_cam);
                m_map.UpdateUnits();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (m_draw_menu)
            {
                sprite_batch.Begin();

                string str = "Soutenance 1\n\nMaster of Olympus for EPITA ...\ngarin_b ; monti_c ; abita_a ; assaad_a";
                Vector2 pos = menu_font.MeasureString(str) / 2;
                sprite_batch.DrawString(menu_font, str,
                    new Vector2(m_screen_w / 2, m_screen_h / 2),
                    Color.Chartreuse, 0, pos, 1.0f, SpriteEffects.None, 0.5f);

                sprite_batch.End();
            }

            if (!m_draw_menu)
            {
                // Draw the terrain
                sprite_batch.Begin(SpriteSortMode.BackToFront,
                            BlendState.AlphaBlend,
                            null,
                            null,
                            null,
                            null,
                            m_cam.GetTransformation(GraphicsDevice));

                m_map.DrawTerrain(sprite_batch, m_cam);

                sprite_batch.End();

                // Draw UPON the terrain
                sprite_batch.Begin(SpriteSortMode.BackToFront,
                            BlendState.AlphaBlend,
                            null,
                            null,
                            null,
                            null,
                            m_cam.GetTransformation(GraphicsDevice));

                m_map.Draw(sprite_batch);

                sprite_batch.End();
            }

            base.Draw(gameTime);
        }
    }
}
