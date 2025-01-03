using Microsoft.Xna.Framework.Media;

namespace Neagu_Sergiu_Game_Development_Project.Design_Patterns
{
    public class BackgroundMusicManager //Singleton Design Pattern
    {
        private static BackgroundMusicManager _instance;
        private Song _currentSong;

        private BackgroundMusicManager() {}

        public static BackgroundMusicManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BackgroundMusicManager();
                return _instance;
            }
        }

        public void Play(Song song, bool isLooping = true)
        {
            if (_currentSong != song)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = isLooping;
                _currentSong = song;
            }
        }
    }
}