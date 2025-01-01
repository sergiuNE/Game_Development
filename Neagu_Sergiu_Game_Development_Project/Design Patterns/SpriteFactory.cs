using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.Animations;
using System.Collections.Generic;

namespace Neagu_Sergiu_Game_Development_Project.Design_Patterns
{
    public class SpriteFactory //Factory Design
    {
        private ContentManager _content;

        public SpriteFactory(ContentManager content)
        {
            _content = content;
        }

        private Animation LoadAnimation(string textureName, List<Rectangle> hitboxes, int frameCount)
        {
            return new Animation(
                _content.Load<Texture2D>(textureName),
                frameCount,
                frameHitboxes: hitboxes
            );
        }

        public Dictionary<string, Animation> LoadAnimations()
        {
            var animations = new Dictionary<string, Animation>();

            // Reusable hitbox for most animations
            var defaultHitboxes = new List<Rectangle> {
            new Rectangle(12, 10, 38, 50),
            new Rectangle(10, 12, 40, 48),
            new Rectangle(14, 10, 36, 50),
            new Rectangle(10, 10, 40, 52),
            new Rectangle(12, 10, 38, 50)
        };

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

        public Dictionary<string, Animation> LoadAnimationsForHunter(string hunterType)
        {
            var animations = new Dictionary<string, Animation>();

            var defaultHitboxes = new List<Rectangle> {
            new Rectangle(10, 10, 40, 50),
            new Rectangle(12, 12, 36, 48),
            new Rectangle(10, 10, 40, 50),
            new Rectangle(14, 10, 36, 50),
            new Rectangle(12, 12, 38, 48)
        };

            // Load animations based on hunter type
            if (hunterType == "HunterOne")
            {
                animations["idleLeft"] = LoadAnimation("hunter_one_idle_left", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);
                animations["idleRight"] = LoadAnimation("hunter_one_idle_right", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);

                animations["walkLeft"] = LoadAnimation("hunter_one_walk_left", defaultHitboxes, 5);
                animations["walkRight"] = LoadAnimation("hunter_one_walk_right", defaultHitboxes, 5);

                animations["attackLeft"] = LoadAnimation("hunter_one_attack_left", defaultHitboxes, 5);
                animations["attackRight"] = LoadAnimation("hunter_one_attack_right", defaultHitboxes, 5);

                animations["dieLeft"] = LoadAnimation("hunter_one_die_left", defaultHitboxes, 5);
                animations["dieRight"] = LoadAnimation("hunter_one_die_right", defaultHitboxes, 5);

                animations["hurtLeft"] = LoadAnimation("hunter_one_hurt_left", defaultHitboxes, 5);
                animations["hurtRight"] = LoadAnimation("hunter_one_hurt_right", defaultHitboxes, 5);

                animations["runLeft"] = LoadAnimation("hunter_one_running_left", defaultHitboxes, 5);
                animations["runRight"] = LoadAnimation("hunter_one_running_right", defaultHitboxes, 5);
            }
            else if (hunterType == "HunterTwo")
            {
                animations["idleLeft"] = LoadAnimation("hunter_two_idle_left", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);
                animations["idleRight"] = LoadAnimation("hunter_two_idle_right", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);

                animations["walkLeft"] = LoadAnimation("hunter_two_walk_left", defaultHitboxes, 5);
                animations["walkRight"] = LoadAnimation("hunter_two_walk_right", defaultHitboxes, 5);

                animations["attackLeft"] = LoadAnimation("hunter_two_attack_left", defaultHitboxes, 5);
                animations["attackRight"] = LoadAnimation("hunter_two_attack_right", defaultHitboxes, 5);

                animations["dieLeft"] = LoadAnimation("hunter_two_die_left", defaultHitboxes, 5);
                animations["dieRight"] = LoadAnimation("hunter_two_die_right", defaultHitboxes, 5);

                animations["hurtLeft"] = LoadAnimation("hunter_two_hurt_left", defaultHitboxes, 5);
                animations["hurtRight"] = LoadAnimation("hunter_two_hurt_right", defaultHitboxes, 5);

                animations["runLeft"] = LoadAnimation("hunter_two_run_left", defaultHitboxes, 5);
                animations["runRight"] = LoadAnimation("hunter_two_run_right", defaultHitboxes, 5);
            }

            else if (hunterType == "HunterThree")
            {
                animations["idleLeft"] = LoadAnimation("hunter_three_idle_left", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);
                animations["idleRight"] = LoadAnimation("hunter_three_idle_right", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);

                animations["walkLeft"] = LoadAnimation("hunter_three_walk_left", defaultHitboxes, 5);
                animations["walkRight"] = LoadAnimation("hunter_three_walk_right", defaultHitboxes, 5);

                animations["attackLeft"] = LoadAnimation("hunter_three_attack_left", defaultHitboxes, 5);
                animations["attackRight"] = LoadAnimation("hunter_three_attack_right", defaultHitboxes, 5);

                animations["dieLeft"] = LoadAnimation("hunter_three_die_left", defaultHitboxes, 5);
                animations["dieRight"] = LoadAnimation("hunter_three_die_right", defaultHitboxes, 5);

                animations["hurtLeft"] = LoadAnimation("hunter_three_hurt_left", defaultHitboxes, 5);
                animations["hurtRight"] = LoadAnimation("hunter_three_hurt_right", defaultHitboxes, 5);

                animations["runLeft"] = LoadAnimation("hunter_three_run_left", defaultHitboxes, 5);
                animations["runRight"] = LoadAnimation("hunter_three_run_right", defaultHitboxes, 5);
            }

            else if (hunterType == "FinalBossHunter")
            {
                animations["idleLeft"] = LoadAnimation("final_boss_idle_left", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);
                animations["idleRight"] = LoadAnimation("final_boss_idle_right", new List<Rectangle> { new Rectangle(10, 10, 40, 50) }, 1);

                animations["walkLeft"] = LoadAnimation("final_boss_walk_left", defaultHitboxes, 5);
                animations["walkRight"] = LoadAnimation("final_boss_walk_right", defaultHitboxes, 5);

                animations["attackLeft"] = LoadAnimation("final_boss_attack_left", defaultHitboxes, 5);
                animations["attackRight"] = LoadAnimation("final_boss_attack_right", defaultHitboxes, 5);

                animations["dieLeft"] = LoadAnimation("final_boss_die_left", defaultHitboxes, 5);
                animations["dieRight"] = LoadAnimation("final_boss_die_right", defaultHitboxes, 5);

                animations["hurtLeft"] = LoadAnimation("final_boss_hurt_left", defaultHitboxes, 5);
                animations["hurtRight"] = LoadAnimation("final_boss_hurt_right", defaultHitboxes, 5);
            }
            return animations;
        }
    }
}