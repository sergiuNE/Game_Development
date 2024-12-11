using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neagu_Sergiu_Game_Development_Project.Characters;
using Neagu_Sergiu_Game_Development_Project.Behaviors;

namespace Neagu_Sergiu_Game_Development_Project.Design_Patterns
{
    public enum HunterType
    {
        HunterOne,
        HunterTwo,
        HunterThree,
        FinalBossHunter
    }

    public static class HunterFactory
    {
        public static Hunter CreateHunter(HunterType type, Vector2 position)
        {
            return type switch
            {
                HunterType.HunterOne => new HunterOne(position),
                HunterType.HunterTwo => new HunterTwo(position),
                HunterType.HunterThree => new HunterThree(position),
                HunterType.FinalBossHunter => new FinalBoss(position),
                _ => throw new ArgumentException("Invalid Hunter Type"),
            };
        }
    }
}
