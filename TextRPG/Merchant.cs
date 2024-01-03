using System;
using System.Text;
namespace TextRPG
{
    public class Merchant
    {

        public List<EquipItem> equipItems;
        public List<ConItem> conItems;

        public Merchant()
        {

            equipItems = new List<EquipItem>();
            conItems = new List<ConItem>();
            Init();
        }
        void Init()
        {
            EquipItem item1 = new EquipItem(ItemCategory.Weapon, new StringBuilder("나무 검"), 4, 0, false, "나무를 깎아만든 검이다.", 200);
            EquipItem item2 = new EquipItem(ItemCategory.Weapon, new StringBuilder("낡은 철 검"), 6, 0, false, "녹이 쓴 낡은 철검이다.", 400);
            EquipItem item3 = new EquipItem(ItemCategory.Weapon, new StringBuilder("뇌진도"), 9, 0, false, "번개 맞은 나무로 만든 단단한 목검이다.", 850);
            EquipItem item4 = new EquipItem(ItemCategory.Armor, new StringBuilder("낡은 가죽 상의"), 0, 4, false, "질긴 소가죽으로 만든 낡은 가죽 옷이다.", 500);
            EquipItem item5 = new EquipItem(ItemCategory.Armor, new StringBuilder("무쇠 갑옷"), 0, 9, false, "무쇠로 만든 무겁지만 단단한 갑옷이다.", 1000);
            EquipItem item6 = new EquipItem(ItemCategory.Armor, new StringBuilder("비키니"), 0, 60, false, "이상하게 방어력이 높다.", 657450);
            equipItems.Add(item1);
            equipItems.Add(item2);
            equipItems.Add(item3);
            equipItems.Add(item4);
            equipItems.Add(item5);
            equipItems.Add(item6);
            ConItem potion1 = new ConItem(ItemCategory.Potion, "체력 포션(소)", 10, "체력을 소량 회복 시켜준다.", 150);
            ConItem potion2 = new ConItem(ItemCategory.Potion, "체력 포션(중)", 30, "체력을 회복 시켜준다.", 500);
            ConItem potion3 = new ConItem(ItemCategory.Potion, "체력 포션(대)", 60, "체력을 회복 시켜준다.", 1000);
            ConItem potion4 = new ConItem(ItemCategory.Potion, "힘의 물약(소)", 1, "던전에 들어가기전 소량의 능력치를 올려준다.", 1000);
            conItems.Add(potion1);
            conItems.Add(potion2);
            conItems.Add(potion3);
            conItems.Add(potion4);

        }


    }	
}

