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
        public int price;
        public bool isSell;


        public ConItem(ItemCategory cat,string _name,int _healing,string _information,int _price)
        {
            category = cat;
            name = _name;
            healing = _healing;
            information = _information;
            amount = 1;
            price = _price;
            isSell = false;
        }

        

	}

    public struct EquipItem
    {
        public ItemCategory category;
        public StringBuilder name;
        public int damage;
        public int armor;
        public bool isEquipped { get; set; }
        public string information;
        public int price;
        public bool isSell { get; set; }

        public EquipItem(ItemCategory cat, StringBuilder _name,int _damage,int _armor,bool _equip,string _information,int _price)
        {
            category = cat;
            name = _name;
            damage = _damage;
            armor = _armor;
            isEquipped = _equip;
            information = _information;
            price = _price;
            isSell = false;
        }

        public void Equipped()
        {
            string equ = "[E] ";

            if (!isEquipped)
            {
                name.Insert(0, equ);
                isEquipped = true;
            }
            else
            {
                name.Replace(equ, "");
                isEquipped = false;
            }
        }




       public void IsSell()
        {
            isSell = true;
        }
    }
}

