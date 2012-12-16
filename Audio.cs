using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Master_Of_Olympus
{
    public class Audio
    {
        private Song m_current_song;

        public Audio()
        {
        }

        public void LoadAudioContent(ContentManager content_manager)
        {
            m_current_song = content_manager.Load<Song>("Music/Amolfi");
            //MediaPlayer.Play(m_current_song);
        }
    }
}
