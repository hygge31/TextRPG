﻿using System;
using System.Text;


namespace TextRPG
{
	public class Player:ConsoleText
	{
        public string playerName;
		public int level;
        public int gold;
        public string clas;
        public float currentHealth;
        public float maxHealth;
        public float attackDamge;
        public float increaseInDamage;
        public int armor;
        public int increaseInArmor;
        public float currentExp;
        public float levelUpExp;
        //public List<ConItem> activeBuff;

        public EquipItem?[] equipment = new EquipItem?[1]; //갑옷
        public EquipItem?[] weaponEqu = new EquipItem?[1]; // 무기

        public List<EquipItem> inventory = new List<EquipItem>();

        public Player()
        {
            //Init();
        }

        //public Player(string className)
        //{
        //    clas = className;
        //    ClassInit(clas);
        //}

		//void ClassInit(string _clas)
		//{
		//	switch (_clas)
		//	{
  //              case "Warrior":
  //                  level = 1;
  //                  gold = 1500;
  //                  clas = _clas;
		//			health = 100;
		//			attackDamge = 7;
		//			armor = 10;

		//			equipment[0] = new EquipItem(ItemCategory.Armor, new StringBuilder("낡은 천 모자"), 0, 1, true,"다 뜯어져 허름한 천 모자이다.");
  //                  equipment[1] = new EquipItem(ItemCategory.Armor, new StringBuilder("낡은 천 옷"), 0, 3, true,"다 뜯어져 허름한 천 옷이다.");
  //                  equipment[2] = new EquipItem(ItemCategory.Armor, new StringBuilder("낡은 천 바지"), 0, 2, true, "다 뜯어져 허름한 천 바지이다.");
  //                  equipment[3] = null;
  //                  equipment[4] = new EquipItem(ItemCategory.Armor, new StringBuilder("낡은 천 신발"), 0, 1, true, "다 뜯어져 허름한 천 신발이다.");

  //                  weaponEqu[0] = new EquipItem(ItemCategory.Weapon, new StringBuilder("나무 몽둥이"), 2, 0, true,"바닥에 널부러진 흔한 나무 몽둥이 이다.");
  //                  weaponEqu[1] = null;

  //                  ArrangementInventory();

  //                  ConItem position = new ConItem(ItemCategory.Potion, "Low Healing Potion", 10, "매우 적은 양을 회복시켜준다.");
  //                  inventory.Add(position);

  //                  break;
  //          }

  //      }

        public void Init()
        {
            playerName = "";
            level = 1;
            clas = "(전사)";
            attackDamge = 10;
            armor = 5;
            maxHealth = 100;
            currentHealth = maxHealth;
            gold = 1500;
            currentExp = 0;
            levelUpExp = 20;
            //activeBuff = new List<ConItem>();

            EquipItem item = new EquipItem("Weapon", new StringBuilder("나무 몽둥이"), 2, 0, false, "일반 나무 몽둥이",100);
            EquipItem item2 = new EquipItem("Weapon", new StringBuilder("낡은 단검"), 4, 0, false, "흔해 빠진 낡은 단검",200);


            inventory.Add(item);
            inventory.Add(item2);
        }


        //public void ArrangementInventory()
        //{
        //    for(int i = 0; i< equipment.Length; i++)
        //    {
        //        EquipItem? item = equipment[i];

        //        if (item != null)
        //        {
        //            if (item.Value.isEquipped)
        //            {
        //                item.Value.Equipped();
        //            }
        //        }
        //        inventory.Add(item);
        //    }

        //    for (int i = 0; i < weaponEqu.Length; i++)
        //    {
        //        EquipItem? item = weaponEqu[i];

        //        if (item != null)
        //        {
        //            if (item.Value.isEquipped)
        //            {
        //                item.Value.Equipped();
        //            }
        //        }
        //        inventory.Add(item);
        //    }


        //}

        public void IncreaseDamageAndArmor(float damage,int armor)
        {
            increaseInDamage += damage;
            increaseInArmor += armor;
        }
        //public void RemoveList(int idx)
        //{
        //    inventory.Remove(idx);
        //}
        public void DungeonClearFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("던전 클리어 실패!");
            Console.ResetColor();
            Console.WriteLine("몬스터로 부터 도망쳤습니다. 떨어진 체력을 마을에서 회복해 주세요.");
            Console.WriteLine();
            Console.Write(currentHealth);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" -> ");
            Console.ResetColor();
            currentHealth -= currentHealth / 2;
            Console.Write(currentHealth);

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해 주세요.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">> ");
            string select = Console.ReadLine();
            Console.Clear();
        }

        public void DungeonClear(int decrease,int reward,int rewardExp)
        {
            Random random = new Random();

            if(currentHealth - decrease <= 0)
            {
                currentHealth = 0;
            }
            else
            {
                float randomRewardRate = random.Next((int)attackDamge, (int)attackDamge * 2) / 100f;
                int addReward = (int)(reward * randomRewardRate);
                gold += reward + addReward;


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("던전 클리어!");
                Console.ResetColor();
                Console.WriteLine("던전 클리어! 감소한 체력을 마을에서 회복해 주세요.");
                Console.WriteLine();
                Console.WriteLine("[탐험 결과]");


                //levelUp
                if(currentExp + rewardExp >= levelUpExp)
                {
                    currentExp += rewardExp;


                    int currentLevel = level;
                    float currentDamage = attackDamge;
                    int currentArmor = armor;
                    float currentMaxHealth = maxHealth;

                    LevelUp();
                    GreenText("Level Up!!");
                    Console.WriteLine();
                    YellowText("--------------------------------------------------------");
                    Console.WriteLine();
                    Console.Write("Lv\t: ");
                    Console.Write(currentLevel);
                    DarkYellowText(" -> ");
                    DarkRedText(level.ToString());
                    Console.WriteLine();
                    Console.Write("공격력\t: ");
                    Console.Write(currentDamage);
                    DarkYellowText(" -> ");
                    DarkRedText(attackDamge.ToString());
                    Console.WriteLine();
                    Console.Write("방어력\t: ");
                    Console.Write(currentArmor);
                    DarkYellowText(" -> ");
                    DarkRedText(armor.ToString());
                    Console.WriteLine();
                    Console.Write("최대 체력\t: ");
                    Console.Write(currentMaxHealth);
                    DarkYellowText(" -> ");
                    DarkRedText(maxHealth.ToString());
                    Console.WriteLine();
                    YellowText("--------------------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine();


                    Console.Write("던전 보상\t: ");
                    YellowText(reward.ToString());
                    Console.Write(" G\n");
                    Console.Write("추가 보상\t: ");
                    YellowText(addReward.ToString());
                    Console.Write(" G");
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("체력 ");
                    DarkRedText(currentHealth.ToString());
                    DarkYellowText(" -> ");
                    currentHealth -= decrease;
                    DarkRedText(currentHealth.ToString());
                    Console.WriteLine();
                    Console.Write("던전 보상\t: ");
                    YellowText(reward.ToString());
                    Console.Write(" G\n");
                    Console.Write("추가 보상\t: ");
                    YellowText(addReward.ToString());
                    Console.Write(" G");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("경험치: ");
                    DarkBlueText(currentExp.ToString());
                    DarkYellowText(" -> ");
                    currentExp += rewardExp;
                    BlueText(currentExp.ToString());
                    Console.WriteLine();

                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                NextActionMessage();
                string select = Console.ReadLine();
                Console.Clear();

            }

            void LevelUp()
            {
                level++;
                currentExp = currentExp-levelUpExp;
                levelUpExp = levelUpExp * 2;
                armor += 1;
                attackDamge += 0.5f;
                maxHealth += 20;
                currentHealth = maxHealth;

                if (currentExp >= levelUpExp)
                {
                    LevelUp();
                }
            }

        }
        
    }
    
}


