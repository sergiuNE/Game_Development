using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project;
using System.Collections.Generic;

public class SpriteFactory
{
    private ContentManager _content;

    public SpriteFactory(ContentManager content)
    {
        _content = content;
    }

    public Dictionary<string, Animation> LoadAnimations()
    {
        var animations = new Dictionary<string, Animation>();

        // Load animations

        var idleLeftHitboxes = new List<Rectangle>
        {
            new Rectangle(10, 10, 40, 50)
        };
        animations["idleLeft"] = new Animation(
            _content.Load<Texture2D>("idle_vampire_left"),
            frameCount: 1,
            frameHitboxes: idleLeftHitboxes
        );

        var idleRightHitboxes = new List<Rectangle>
        {
            new Rectangle(10, 10, 40, 50) 
        };
        animations["idleRight"] = new Animation(
            _content.Load<Texture2D>("idle_vampire_right"),
            frameCount: 1,
            frameHitboxes: idleRightHitboxes
        );

        var walkLeftHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50),
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50)  
        };
        animations["walkLeft"] = new Animation(
            _content.Load<Texture2D>("walk_vampire_left"),
            frameCount: 5,
            frameHitboxes: walkLeftHitboxes
        );

        var walkRightHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50),
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50)  
        };
        animations["walkRight"] = new Animation(
            _content.Load<Texture2D>("walk_vampire_right"),
            frameCount: 5,
            frameHitboxes: walkRightHitboxes
        );

        var attackLeftHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52),
            new Rectangle(12, 10, 38, 50) 
        };
        animations["attackLeft"] = new Animation(
            _content.Load<Texture2D>("vampire_attack_left"),
            frameCount: 5,
            frameHitboxes: attackLeftHitboxes
        );

        var attackRightHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50)  
        };
        animations["attackRight"] = new Animation(
            _content.Load<Texture2D>("vampire_attack_right"),
            frameCount: 5,
            frameHitboxes: attackRightHitboxes
        );

        var dieLeftHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52),
            new Rectangle(12, 10, 38, 50)  
        };
        animations["dieLeft"] = new Animation(
            _content.Load<Texture2D>("vampire_die_left"),
            frameCount: 5,
            frameHitboxes: dieLeftHitboxes
        );

        var dieRightHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50),
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50),
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50)
        };
        animations["dieRight"] = new Animation(
            _content.Load<Texture2D>("vampire_die_right"),
            frameCount: 5,
            frameHitboxes: dieLeftHitboxes
        );

        var hurtLeftHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50)  
        };
        animations["hurtLeft"] = new Animation(
            _content.Load<Texture2D>("vampire_hurt_left"),
            frameCount: 5,
            frameHitboxes: hurtLeftHitboxes
        );

        var hurtRightHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50) 
        };
        animations["hurtRight"] = new Animation(
            _content.Load<Texture2D>("vampire_hurt_right"),
            frameCount: 5,
            frameHitboxes: hurtRightHitboxes
        );

        var jumpLeftHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50) 
        };
        animations["jumpLeft"] = new Animation(
            _content.Load<Texture2D>("vampire_jump_left"),
            frameCount: 5,
            frameHitboxes: jumpLeftHitboxes
        );

        var jumpRightHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50),
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52),
            new Rectangle(12, 10, 38, 50)  
        };
        animations["jumpRight"] = new Animation(
            _content.Load<Texture2D>("vampire_jump_right"),
            frameCount: 5,
            frameHitboxes: jumpRightHitboxes
        );

        var runLeftHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50)  
        };
        animations["runLeft"] = new Animation(
            _content.Load<Texture2D>("vampire_run_left"),
            frameCount: 5,
            frameHitboxes: runLeftHitboxes
        );

        var runRightHitboxes = new List<Rectangle>
        {
            new Rectangle(12, 10, 38, 50), 
            new Rectangle(10, 12, 40, 48), 
            new Rectangle(14, 10, 36, 50), 
            new Rectangle(10, 10, 40, 52), 
            new Rectangle(12, 10, 38, 50)  
        };
        animations["runRight"] = new Animation(
            _content.Load<Texture2D>("vampire_run_right"),
            frameCount: 5,
            frameHitboxes: runRightHitboxes
        );

        return animations;
    }
}
