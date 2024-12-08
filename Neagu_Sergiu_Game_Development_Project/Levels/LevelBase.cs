using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Neagu_Sergiu_Game_Development_Project.Levels
{
    public abstract class LevelBase
    {
        protected GraphicsDevice GraphicsDevice;
        protected ContentManager Content;
        protected Vampire _vampire;
        protected List<Rectangle> _pathBounds;
        protected List<Rectangle> _blockedAreas;
        protected Texture2D _currentCastleTexture;


        public LevelBase(GraphicsDevice graphicsDevice, ContentManager content, Vampire vampire)
        {
            GraphicsDevice = graphicsDevice;
            Content = content;
            _vampire = vampire;
            _pathBounds = new List<Rectangle>();
            _blockedAreas = new List<Rectangle>();
        }

        public virtual void LoadContent()
        {
            // This is overwritten in the subclasses
        }

        public Texture2D GetCastleTexture()
        {
            return _currentCastleTexture;
        }

        public void CheckCollision()
        {
            bool isInsidePath = false;

            // Check that the vampire's BoundingBox overlaps with one of the path rectangles
            foreach (var path in _pathBounds)
            {
                if (_vampire.CurrentHitbox.Intersects(path))
                {
                    isInsidePath = true;
                    break;
                }
            }

            // Check if the vampire hits a blocked area
            foreach (var blockedArea in _blockedAreas)
            {
                if (_vampire.CurrentHitbox.Intersects(blockedArea))
                {
                    isInsidePath = false;
                    break;
                }
            }

            // If the vampire is outside the path, reset position to previous valid position
            if (!isInsidePath)
            {
                _vampire.Position = _vampire.PreviousPosition;
            }
        }
    }
}
