using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Neagu_Sergiu_Game_Development_Project
{
    public class Animation
    {
        public Texture2D Texture { get; private set; }   // The texture containing the animation frames
        public int FrameCount { get; private set; }       // The total number of frames in the animation
        public int CurrentFrame { get; private set; }     // The current frame being shown
        public float FrameTime { get; private set; }      // The time between frames (used to control animation speed)
        private float _elapsedTime;                         // The time that has passed since the last frame update

        public Animation(Texture2D texture, int frameCount, float frameTime = 0.1f)
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameTime = frameTime;
            CurrentFrame = 0;
            _elapsedTime = 0f;
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If it's time to change to the next frame
            if (_elapsedTime >= FrameTime)
            {
                CurrentFrame++;
                if (CurrentFrame >= FrameCount)
                {
                    CurrentFrame = 0; // Loop back to the first frame
                }
                _elapsedTime = 0f; // Reset the elapsed time
            }
        }

        public Rectangle GetSourceRectangle()
        {
            // Calculate the width of each frame (assuming frames are laid out horizontally in a single row)
            int frameWidth = Texture.Width / FrameCount;
            return new Rectangle(CurrentFrame * frameWidth, 0, frameWidth, Texture.Height); // Return the current frame's source rectangle
        }
    }
}
