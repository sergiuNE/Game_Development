using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Neagu_Sergiu_Game_Development_Project.Characters;

public abstract class LevelBase
{
    protected GraphicsDevice GraphicsDevice;
    protected ContentManager Content;
    protected Vampire _vampire;
    protected List<Rectangle> _pathBounds;
    protected List<Rectangle> _blockedAreas;
    protected Texture2D _currentCastleTexture;

    protected List<Hunter> _hunters;

    public LevelBase(GraphicsDevice graphicsDevice, ContentManager content, Vampire vampire, List<Hunter> hunters)
    {
        GraphicsDevice = graphicsDevice;
        Content = content;
        _vampire = vampire;
        _pathBounds = new List<Rectangle>();
        _blockedAreas = new List<Rectangle>();
        _hunters = hunters;
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

        // Check collision with each Hunter
        foreach (var hunter in _hunters)
        {
            if (_vampire.CurrentHitbox.Intersects(hunter.CurrentHitbox))
            {
                HandleVampireHunterCollision();
                break; 
            }
        }
    }

    protected virtual void HandleVampireHunterCollision()
    {
        // Default behavior: Prevent Vampire from moving into the Hunter
        _vampire.Position = _vampire.PreviousPosition;
    }
}
