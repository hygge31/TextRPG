using System;
using System.Text;

namespace TextRPG

{
    public enum ItemCategory
    {
        Weapon,
        Armor,
        Potion,
        Food
    }

    public struct ConItem
	{
        public ItemCategory category;
        public string name;
        public string information;
        public int healing;
        public int amount;


        public ConItem(ItemCategory cat,string _name,int _healing,string _information)
        {
            category = cat;
            name = _name;
            healing = _healing;
            information = _information;
            amount = 1;
        }

	}

    public struct EquipItem
    {
        public ItemCategory category;
        public StringBuilder name;
        public int damage;
        public int armor;
        public bool isEquipped;
        public string information;

        public EquipItem(ItemCategory cat, StringBuilder _name,int _damage,int _armor,bool _equip,string _information)
        {
            category = cat;
            name = _name;
            damage = _damage;
            armor = _armor;
            isEquipped = _equip;
            information = _information;
        }

        public void Equipped()
        {
            string equ = "[E]";

            if (isEquipped)
            {
                name.Insert(0, equ);
            }
            else
            {
                name.Replace(equ, "");
            }
        }


    }
}

