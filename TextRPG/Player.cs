using System;
using System.Text;
namespace TextRPG
{
	public class Player
	{
		public int level;
        public int gold;
        public string clas;
        public float health;
        public float attackDamge;
        public float armor;
        public float currentExp;
        public float levelUpExp;
        public int increaseInDamage;
        public int increaseInArmor;

        public EquipItem?[] equipment = new EquipItem?[5]; //머리,갑옷, 바지, 장갑 , 신발
        public EquipItem?[] weaponEqu = new EquipItem?[2]; // 무기, 방패

        public List<Object> inventory = new List<Object>();

        public Player()
        {
            Init();
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

        void Init()
        {
            level = 1;
            clas = "(전사)";
            attackDamge = 10;
            armor = 5;
            health = 100;
            gold = 1500;
            currentExp = 0;
            levelUpExp = 20;

            EquipItem item = new EquipItem(ItemCategory.Weapon, new StringBuilder("나무 몽둥이"), 2, 0, false, "일반 나무 몽둥이");
            EquipItem item2 = new EquipItem(ItemCategory.Weapon, new StringBuilder("낡은 단검"), 4, 0, false, "흔해 빠진 낡은 단검");


            inventory.Add(item);
            inventory.Add(item2);
        }


        public void ArrangementInventory()
        {
            for(int i = 0; i< equipment.Length; i++)
            {
                EquipItem? item = equipment[i];

                if (item != null)
                {
                    if (item.Value.isEquipped)
                    {
                        item.Value.Equipped();
                    }
                }
                inventory.Add(item);
            }

            for (int i = 0; i < weaponEqu.Length; i++)
            {
                EquipItem? item = weaponEqu[i];

                if (item != null)
                {
                    if (item.Value.isEquipped)
                    {
                        item.Value.Equipped();
                    }
                }
                inventory.Add(item);
            }


        }

        public void IncreaseDamageAndArmor(int damage,int armor)
        {
            increaseInDamage += damage;
            increaseInArmor += armor;
        }



    }
}

