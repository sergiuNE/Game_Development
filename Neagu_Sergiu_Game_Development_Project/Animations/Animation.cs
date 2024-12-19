using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Neagu_Sergiu_Game_Development_Project.Animations
{
    public class Animation
    {
        public List<Rectangle> FrameHitboxes { get; private set; }
        public Texture2D Texture { get; private set; }
        public int FrameCount { get; private set; }       // The total number of frames in the animation
        public int CurrentFrame { get; private set; }     // The current frame being shown
        public float FrameTime { get; private set; }      // The time between frames (used to control animation speed)
        private float _elapsedTime;                         // The time that has passed since the last frame update

        public Animation(Texture2D texture, int frameCount, List<Rectangle> frameHitboxes, float frameTime = 0.1f)
        {
            if (frameHitboxes == null || frameHitboxes.Count != frameCount)
                throw new ArgumentException("Aantal hitboxes moet gelijk zijn aan het aantal frames.");

            Texture = texture;
            FrameCount = frameCount;
            FrameHitboxes = frameHitboxes;
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

        // Methode om de huidige hitbox op te halen
        public Rectangle GetCurrentHitbox()
        {
            if (FrameHitboxes == null || CurrentFrame >= FrameHitboxes.Count)
            {
                throw new InvalidOperationException("FrameHitboxes is niet correct geïnitialiseerd of de huidige frame-index is ongeldig.");
            }

            return FrameHitboxes[CurrentFrame];
        }
    }
}