using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.Animations;
using System.Collections.Generic;

public class SpriteFactory //Factory Design
{
    private ContentManager _content;

    public SpriteFactory(ContentManager content)
    {
        _content = content;
    }

    // Gemeenschappelijke methode voor het laden van animaties
    private Animation LoadAnimation(string textureName, List<Rectangle> hitboxes, int frameCount)
    {
        return new Animation(
            _content.Load<Texture2D>(textureName),
            frameCount,
            frameHitboxes: hitboxes
        );
    }

    // Methode om de animaties te laden
    public Dictionary<string, Animation> LoadAnimations()
    {
        var animations = new Dictionary<string, Animation>();

        // Herbruikbare hitbox voor de meeste animaties
        var defaultHitboxes = new List<Rectangle> {
            new Rectangle(12, 10, 38, 50),
            new Rectangle(10, 12, 40, 48),
            new Rectangle(14, 10, 36, 50),
            new Rectangle(10, 10, 40, 52),
            new Rectangle(12, 10, 38, 50)
        };

        // Voor elke animatie kun je nu de LoadAnimation methode aanroepen
        animations["idleLeft"] = LoadAnimation("idle_vampire_left", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);
        animations["idleRight"] = LoadAnimation("idle_vampire_right", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);

        animations["walkLeft"] = LoadAnimation("walk_vampire_left", defaultHitboxes, 5);
        animations["walkRight"] = LoadAnimation("walk_vampire_right", defaultHitboxes, 5);

        animations["attackLeft"] = LoadAnimation("vampire_attack_left", defaultHitboxes, 5);
        animations["attackRight"] = LoadAnimation("vampire_attack_right", defaultHitboxes, 5);

        animations["dieLeft"] = LoadAnimation("vampire_die_left", defaultHitboxes, 5);
        animations["dieRight"] = LoadAnimation("vampire_die_right", defaultHitboxes, 5);

        animations["hurtLeft"] = LoadAnimation("vampire_hurt_left", defaultHitboxes, 5);
        animations["hurtRight"] = LoadAnimation("vampire_hurt_right", defaultHitboxes, 5);

        animations["jumpLeft"] = LoadAnimation("vampire_jump_left", defaultHitboxes, 5);
        animations["jumpRight"] = LoadAnimation("vampire_jump_right", defaultHitboxes, 5);

        animations["runLeft"] = LoadAnimation("vampire_run_left", defaultHitboxes, 5);
        animations["runRight"] = LoadAnimation("vampire_run_right", defaultHitboxes, 5);

        return animations;
    }
}
