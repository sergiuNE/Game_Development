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

        // Load animations for the vampire, like idle, walk, attack, etc.
        animations["idleLeft"] = new Animation(_content.Load<Texture2D>("idle_vampire_left"), frameCount: 1);
        animations["idleRight"] = new Animation(_content.Load<Texture2D>("idle_vampire_right"), frameCount: 1);
        animations["walkLeft"] = new Animation(_content.Load<Texture2D>("walk_vampire_left"), frameCount: 5);
        animations["walkRight"] = new Animation(_content.Load<Texture2D>("walk_vampire_right"), frameCount: 5);
        animations["attackLeft"] = new Animation(_content.Load<Texture2D>("vampire_attack_left"), frameCount: 5);
        animations["attackRight"] = new Animation(_content.Load<Texture2D>("vampire_attack_right"), frameCount: 5);
        animations["dieLeft"] = new Animation(_content.Load<Texture2D>("vampire_die_left"), frameCount: 5);
        animations["dieRight"] = new Animation(_content.Load<Texture2D>("vampire_die_right"), frameCount: 5);
        animations["hurtLeft"] = new Animation(_content.Load<Texture2D>("vampire_hurt_left"), frameCount: 5);
        animations["hurtRight"] = new Animation(_content.Load<Texture2D>("vampire_hurt_right"), frameCount: 5);
        animations["jumpLeft"] = new Animation(_content.Load<Texture2D>("vampire_jump_left"), frameCount: 5);
        animations["jumpRight"] = new Animation(_content.Load<Texture2D>("vampire_jump_right"), frameCount: 5);
        animations["runLeft"] = new Animation(_content.Load<Texture2D>("vampire_run_left"), frameCount: 5);
        animations["runRight"] = new Animation(_content.Load<Texture2D>("vampire_run_right"), frameCount: 5);

        return animations;
    }
}
