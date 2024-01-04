﻿using System;
namespace TextRPG
{
	public class DungeonManager : ConsoleText
	{
		public List<Dungeon> dungeons;
        
		public DungeonManager()
		{
			dungeons = new List<Dungeon>();
            Init();
		}

		void Init()
        {
            Dungeon easyDun = new Dungeon("쉬움 던전", 5, 1000);
            Dungeon normalDun = new Dungeon("일반 던전", 11, 1700);
            Dungeon hardDun = new Dungeon("어려운 던전", 17, 2500);
            dungeons.Add(easyDun);
            dungeons.Add(normalDun);
            dungeons.Add(hardDun);
        }
        public bool EnterDungeon(Dungeon dungeon,Player player)
        {
            Random random = new Random();
            int healthPenalty = dungeon.recommendArmor - player.armor;
            int playerArmor = player.armor + player.increaseInArmor;

            TextAnimation($"{dungeon.dungeonName}에 입장 중 입니다.", 100);
            TextAnimation("던전 탐색 진행중............", 200);
            TextAnimation("몬스터와 대치 중............", 200);

            if (dungeon.recommendArmor > playerArmor)
            {
                int ranClear = random.Next(0, 101);
                if (ranClear <= 40)
                {
                    Console.WriteLine(ranClear);
                    player.DungeonClearFail();
                    return false;
                }
                else
                {
                    int decrease = random.Next(dungeon.minHealthDecrease + healthPenalty, dungeon.maxHealthDecrease + healthPenalty);
                    player.DungeonClear(decrease, dungeon.reward);
                    return true;
                }
            }
            else
            {
                int decrease = random.Next(dungeon.minHealthDecrease + healthPenalty, dungeon.maxHealthDecrease + healthPenalty);
                player.DungeonClear(decrease, dungeon.reward);
                return true;
            }

        }
	}

    public struct Dungeon
    {
        public string dungeonName;
        public int recommendArmor;
        public int reward;
        public int minHealthDecrease;
        public int maxHealthDecrease;

        public Dungeon(string _dungeonName, int _recommendArmor, int _reward)
        {
            dungeonName = _dungeonName;
            recommendArmor = _recommendArmor;
            reward = _reward;
            minHealthDecrease = 20;
            maxHealthDecrease = 35;

        }
    }
}

