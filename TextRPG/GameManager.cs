﻿using System;
using System.Runtime.Intrinsics.X86;

namespace TextRPG
{
	public enum GameState
	{
		Play,
		GameOver
	}

    


    public class GameManager
	{
        static public GameState gameState;
        Player player;
        Merchant merchant;
        DungeonManager dungeonManager;
		bool isSelectClass;

        public GameManager()
		{
			gameState = GameState.Play;
        }




        public void GameStart()
		{
			player = new Player();
            merchant = new Merchant();
            dungeonManager = new DungeonManager();
			//if (!isSelectClass)
			//{
			//	SelectClassMessage();
			//	Console.Clear();

   //         }
			//클래스 선택


            Console.WriteLine("이곳에 온걸 환영하네, \n던전에 들어가기 전에 이곳에서 준비하고 가시게나.\n\n");

			while (gameState == GameState.Play)
			{
                if (player.currentHealth == 0)
                {
                    gameState = GameState.GameOver;
                    break;
                }

				string select = MainMenu();
				if (select == "1")
				{
					Console.Clear();
                    //player state
                    StateMenu();

				}
				else if (select == "2")
				{
                    Console.Clear();
					InventoryMenu();
                    //Inventory
                }
				else if (select == "3")
				{
                    //store
                    Console.Clear();
                    MerchantMenu();
				}
                else if (select == "4")
                {
                    if(player.currentHealth <= 35)
                    {
                        Console.Clear();
                        Console.WriteLine("체력이 낮아 던전에 입장할 수 없습니다. 체력을 회복해 주세요.");
                    }
                    else
                    {
                        Console.Clear();
                        DungeonMenu();
                    }
                    
                }
                else if (select == "5")
                {
                    //rest
                    Console.Clear();

                }
                else
				{
					Console.Clear();
                    WrongInput();
				}

			}
            Console.Write("플레이어 사망");
		}


        //----------------------------------------------------------------------------Menu

        string MainMenu()
        {
            Console.ResetColor();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전");
            Console.WriteLine("5. 휴식");
            NextActionMessage();

            string select = Console.ReadLine();

            switch (select)
            {
                case "1":
                    return "1";
                case "2":
                    return "2";
                case "3":
                    return "3";
                case "4":
                    return "4";
                case "5":
                    return "5";
                default:
					return "0";

            }

        }


        void StateMenu()
		{
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("상태 보기");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
			string level = "";
			if(player.level < 10)
			{
				level = "0" + player.level;
			}
			else
			{
				level = player.level.ToString();
			}

			Console.Write("LV. ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine(level);
			Console.ResetColor();

			Console.Write("Class\t:");
			Console.Write("{0}\n",player.clas);

            Console.Write("Damage\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.attackDamge);
			Console.ResetColor();
            if(player.increaseInDamage != 0)
            {
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("+");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(player.increaseInDamage);
                Console.ResetColor();
                Console.Write(")\n");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write("Armor\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.armor);
            Console.ResetColor();
            if (player.increaseInArmor != 0)
            {
                Console.Write(" (");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("+");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(player.increaseInArmor);
                Console.ResetColor();
                Console.Write(")\n");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write("Health\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.currentHealth);
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(player.maxHealth);
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Gold\t: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(player.gold);
            Console.ResetColor();
            Console.Write(" G\n");

            Console.Write("경험치\t: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(player.currentExp);
            Console.ResetColor();
            Console.Write(" / ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(player.levelUpExp);
            Console.ResetColor();




            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기 ");
			NextActionMessage();
			string select = Console.ReadLine();

			if (select != "0")
			{
				ConsoleClear();
                WrongInput();
                StateMenu();

			}
			else
			{
				Console.Clear();
			}
        }
        
        //---------------------------------------------------------------------------------------------------------------------Inventory
        void InventoryMenu()
		{
            ShowMyEquipmentMenu();
			
        }


		void ShowMyEquipmentMenu()
		{
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("인벤토리;");
            Console.ResetColor();
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
			ShowEquipment();

        }

		void ShowEquipment()
		{
            foreach (Object item in player.inventory)
            {
                if (item is EquipItem)
                {
                    EquipItem currentItem = (EquipItem)item;
					if(currentItem.category == ItemCategory.Weapon)
					{
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("- ");
                        Console.ResetColor();
                        Console.Write(currentItem.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        Console.Write("공격력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.damage);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        Console.Write(currentItem.information);
                        Console.WriteLine();
                    }
					else
					{
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("- ");
                        Console.ResetColor();
                        Console.Write(currentItem.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
						Console.Write("방어력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.armor);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
						Console.Write(currentItem.information);
						Console.WriteLine();

                    }
                   
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            if(select == "1")
            {
                Console.Clear();
                EquipManagement();
            }
            else if(select =="0")
            {
                Console.Clear();
            }
            else
            {
                Console.Clear();
                WrongInput();
                InventoryMenu();
            }
        }


        void EquipManagement()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("인벤토리;");
            Console.ResetColor();
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            
            int num = 1;
            string equipStr = "[E] ";
            foreach (Object item in player.inventory)
            {
                if (item is EquipItem)
                {
                    EquipItem currentItem = (EquipItem)item;
                    if (currentItem.category == ItemCategory.Weapon)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(num+".");
                        Console.ResetColor();
                        Console.Write(currentItem.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        Console.Write("공격력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.damage);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        Console.Write(currentItem.information);
                        Console.WriteLine();
                        
                        num++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(num+".");
                        Console.ResetColor();
                        Console.Write(currentItem.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        Console.Write("방어력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.armor);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        Console.Write(currentItem.information);
                        Console.WriteLine();
                        
                        num++;
                    }

                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            
            if(select != "0")
            {
                if(int.TryParse(select,out int idx) && idx-1 < player.inventory.Count)
                {
                    EquipItem currentItem = (EquipItem)player.inventory[idx-1];

                    if (currentItem.category == ItemCategory.Weapon)
                    {
                        int stringIdx = currentItem.name.ToString().IndexOf(equipStr);

                        if (stringIdx == -1)
                        {
                            if (player.weaponEqu[0] == null)
                            {
                                currentItem.Equipped();
                                player.inventory[idx - 1] = currentItem;
                                player.IncreaseDamageAndArmor(currentItem.damage, currentItem.armor);
                                player.weaponEqu[0] = currentItem;
                            }
                            else
                            {
                                EquipItem? equipItem = player.weaponEqu[0];
                                for(int i = 0; i < player.inventory.Count; i++)
                                {
                                    if(player.inventory[i] is EquipItem)
                                    {
                                        EquipItem item = (EquipItem)player.inventory[i];
                                        if(item.name == equipItem?.name)
                                        {
                                            EquipItem newItem = item;
                                            newItem.Equipped();
                                            player.inventory[i] = newItem;

                                            
                                            player.IncreaseDamageAndArmor(-equipItem.Value.damage, -equipItem.Value.armor);

                                        }
                                    }
                                }
                                currentItem.Equipped();
                                player.inventory[idx - 1] = currentItem;
                                player.IncreaseDamageAndArmor(currentItem.damage, currentItem.armor);
                                player.weaponEqu[0] = currentItem;

                            }

                        }
                        else
                        {
                            currentItem.Equipped();
                            player.inventory[idx - 1] = currentItem;
                            player.IncreaseDamageAndArmor(-player.weaponEqu[0].Value.damage, -player.weaponEqu[0].Value.armor);
                            player.weaponEqu[0] = null;
                        }
                    }else if(currentItem.category == ItemCategory.Armor)
                    {
                        int stringIdx = currentItem.name.ToString().IndexOf(equipStr);

                        if (stringIdx == -1)
                        {
                            if (player.equipment[0] == null)
                            {
                                currentItem.Equipped();
                                player.inventory[idx - 1] = currentItem;
                                player.IncreaseDamageAndArmor(currentItem.damage, currentItem.armor);
                                player.equipment[0] = currentItem;
                            }
                            else
                            {
                                EquipItem? equipItem = player.equipment[0];
                                for (int i = 0; i < player.inventory.Count; i++)
                                {
                                    if (player.inventory[i] is EquipItem)
                                    {
                                        EquipItem item = (EquipItem)player.inventory[i];
                                        if (item.name == equipItem?.name)
                                        {
                                            EquipItem newItem = item;
                                            newItem.Equipped();
                                            player.inventory[i] = newItem;


                                            player.IncreaseDamageAndArmor(-equipItem.Value.damage, -equipItem.Value.armor);

                                        }
                                    }
                                }
                                currentItem.Equipped();
                                player.inventory[idx - 1] = currentItem;
                                player.IncreaseDamageAndArmor(currentItem.damage, currentItem.armor);
                                player.equipment[0] = currentItem;

                            }

                        }
                        else
                        {
                            currentItem.Equipped();
                            player.inventory[idx - 1] = currentItem;
                            player.IncreaseDamageAndArmor(-player.equipment[0].Value.damage, -player.equipment[0].Value.armor);
                            player.equipment[0] = null;
                        }
                    }

                   
                  
                    Console.Clear();
                    EquipManagement();

                }
                else
                {
                    Console.Clear();
                    WrongInput();
                    EquipManagement();
                }
            }else
            {
                Console.Clear();
                ShowMyEquipmentMenu();
            }
          


        }

   //     void ShowMyEquipmentMenu()
   //     {
   //         Console.Clear();
   //         Console.ForegroundColor = ConsoleColor.DarkRed;
   //         Console.WriteLine("인벤토리 - 장착 관리");
			//Console.ResetColor();
   //         Console.WriteLine("보유중인 아이템을 관리 할 수 있습니다.");
   //         Console.ResetColor();
   //         Console.WriteLine();
   //         Console.WriteLine("[장착 목록]");
   //         Console.WriteLine();
   //         Console.WriteLine("\t[방어구]");
   //         Console.WriteLine();
   //         Console.Write("모자\t: {0}\t", player.equipment[0]?.name);
			//Console.ForegroundColor = ConsoleColor.Blue;
			//Console.Write("방어력: {0}\n", player.equipment[0]?.armor);
			//Console.ResetColor();

   //         Console.Write("상의\t: {0}\t\t", player.equipment[1]?.name);
   //         Console.ForegroundColor = ConsoleColor.Blue;
   //         Console.Write("방어력: {0}\n", player.equipment[1]?.armor);
   //         Console.ResetColor();
   //         Console.Write("하의\t: {0}\t", player.equipment[2]?.name);
   //         Console.ForegroundColor = ConsoleColor.Blue;
   //         Console.Write("방어력: {0}\n", player.equipment[2]?.armor);
   //         Console.ResetColor();
   //         Console.Write("장갑\t: {0}\t", player.equipment[3]?.name);
   //         Console.ForegroundColor = ConsoleColor.Blue;
   //         Console.Write("방어력: {0}\n", player.equipment[3]?.armor);
   //         Console.ResetColor();
   //         Console.Write("신발\t: {0}\t", player.equipment[4]?.name);
   //         Console.ForegroundColor = ConsoleColor.Blue;
   //         Console.Write("방어력: {0}\n", player.equipment[4]?.armor);

   //         Console.ResetColor();

   //         Console.WriteLine();
   //         Console.WriteLine("\t[무기]");
   //         Console.WriteLine();
   //         Console.Write("오른손\t: {0}", player.weaponEqu[0]?.name);
   //         Console.ForegroundColor = ConsoleColor.Red;
   //         Console.Write("\t공격력 : {0}\n", player.weaponEqu[0]?.damage);
   //         Console.ResetColor();

   //         Console.Write("왼손\t: {0}", player.weaponEqu[1]?.name);
   //         Console.ForegroundColor = ConsoleColor.Red;
   //         Console.Write("\t공격력 : {0}\n", player.weaponEqu[1]?.damage);
   //         Console.ResetColor();

   //         Console.WriteLine();
   //         Console.WriteLine("0. 나가기");
   //         NextActionMessage();
   //         string num = Console.ReadLine();
   //         if (num != "0")
   //         {
   //             ShowMyEquipmentMenu();
			//}
			//else
			//{
			//	Console.Clear();
   //             InventoryMenu();
			//}

   //     }

        void ShowMyInventory()
		{
			Console.Clear();
			Console.WriteLine("[아이템 목록]");
			Console.ResetColor();

			int idx = 1;
			foreach(Object item in player.inventory)
			{
				if(item is EquipItem)
				{
					EquipItem currentItem = (EquipItem)item;
					Console.WriteLine($"{idx}. {currentItem.name}\t\t{currentItem.information}");
                    idx++;
				}
			}
            foreach (Object item in player.inventory)
            {
                if (item is ConItem)
                {
                    ConItem currentItem = (ConItem)item;
                    Console.WriteLine($"{idx}. {currentItem.name}\t\t{currentItem.information}");
                    idx++;
                }

            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            NextActionMessage();
            string num = Console.ReadLine();
            if (num != "0")
            {
                ShowMyInventory();
            }
            else
            {
                Console.Clear();
				InventoryMenu();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------Inventory
        //---------------------------------------------------------------------------------------------------------------------Merchant
        void MerchantMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("어서 오시게.. 이곳에서 단단히 준비하고 가는게...좋을걸세...");
            Console.WriteLine();
            Console.WriteLine("1. 아이탬 구매 / 판매");
            //Console.WriteLine("2. 소모품 구매");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            NextActionMessage();
            string select = Console.ReadLine();

            if (select == "1")
            {
                //장비 구매
                Console.Clear();
                MerchantEquipMenu();
            }
            //else if (select == "2")
            //{
            //    Console.Clear();
            //    //소모품 구매
            //}
            else if(select == "0")
            {
                Console.Clear();

            }else
            {
                Console.Clear();
                WrongInput();
                MerchantMenu();
            }
        }

        void MerchantEquipMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("자네에게 맞는 장비를 골라 보시게.. 물론...꽁짜는 아닐세.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(player.gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();

            Console.WriteLine("[아이탬 목록]");
            foreach (Object item in merchant.equipItems)
            {
                if (item is EquipItem)
                {
                    EquipItem currentItem = (EquipItem)item;
                    if (currentItem.category == ItemCategory.Weapon)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("- ");
                        Console.ResetColor();
                        Console.Write(currentItem.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write("공격력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.damage);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write(currentItem.information);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        if (currentItem.isSell)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(" 구매 완료");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(currentItem.price);
                            Console.ResetColor();
                            Console.Write(" G\n");
                        }
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("- ");
                        Console.ResetColor();
                        Console.Write(currentItem.name);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write("방어력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.armor);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write(currentItem.information);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        if (currentItem.isSell)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(" 구매 완료");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(currentItem.price);
                            Console.ResetColor();
                            Console.Write(" G\n");
                        }

                    }

                }
            }
            Console.WriteLine();
            Console.WriteLine("1.아이템 구매");
            Console.WriteLine("2.아이템 판매");
            Console.WriteLine("0.나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            if(select == "1")
            {
                Console.Clear();
                MerchantEquipManagement();

            }else if(select == "2")
            {
                Console.Clear();
                MerchantEquipSellManagement();
            }
            else if (select == "0")
            {
                Console.Clear();
                MerchantMenu();
            }
            else
            {
                Console.Clear();
                WrongInput();
                MerchantEquipMenu();
            }

        }

        void MerchantEquipManagement()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("돈은...준비 됐나....");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(player.gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();
            Console.WriteLine("[아이탬 목록]");
            int idx = 1;


            foreach (EquipItem item in merchant.equipItems)
            {
                    if (item.category == ItemCategory.Weapon)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(idx + ".");
                        Console.ResetColor();
                        Console.Write(item.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write("공격력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(item.damage);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write(item.information);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        if (item.isSell)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(" 구매 완료");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.price);
                            Console.ResetColor();
                            Console.Write(" G\n");
                        }
                        idx++;
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(idx + ".");
                        Console.ResetColor();
                        Console.Write(item.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write("방어력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(item.armor);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write(item.information);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        if (item.isSell)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(" 구매 완료");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(item.price);
                            Console.ResetColor();
                            Console.Write(" G\n");
                        }
                        idx++;
                        
                    }

                }
            
            Console.WriteLine();
            Console.WriteLine("0.나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            if(int.TryParse(select,out int number) && number-1 < merchant.equipItems.Count)
            {
                if (number != 0)
                {
                    EquipItem currentItem = merchant.equipItems[number - 1];
                    if (currentItem.isSell)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("이미 구매한 아이템 입니다.");
                        Console.ResetColor();
                        MerchantEquipManagement();
                        
                    }
                    else if (player.gold >= currentItem.price)
                    {

                        currentItem.isSell = true;
                        merchant.equipItems[number - 1] = currentItem;
                        player.gold -= currentItem.price;
                        player.inventory.Add(currentItem);
                        
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("구매를 완료했습니다.");
                        Console.ResetColor();
                        MerchantEquipManagement();
                        
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Gold가 부족합니다.");
                        Console.ResetColor();
                        MerchantEquipManagement();
                    }
                }
                else
                {
                    Console.Clear();
                    MerchantEquipMenu();
                }
            }
            else
            {
                Console.Clear();
                WrongInput();
                MerchantEquipManagement();
            }
            //rr
        }

        void MerchantEquipSellManagement()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("흠... 무엇을 팔텐가..");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(player.gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();
            Console.WriteLine("[아이탬 목록]");
            int idx = 1;
            foreach (object item in player.inventory)
            {
                if(item is EquipItem)
                {
                    EquipItem currentItem = (EquipItem)item;
                    if (currentItem.category == ItemCategory.Weapon)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(idx + ".");
                        Console.ResetColor();
                        Console.Write(currentItem.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write("공격력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.damage);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write(currentItem.information);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write("판매 금액 :");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(currentItem.price *0.85f);
                        Console.ResetColor();
                        Console.Write(" G\n");

                        idx++;

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(idx + ".");
                        Console.ResetColor();
                        Console.Write(currentItem.name + "\t");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write("방어력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(currentItem.armor);
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t| ");
                        Console.ResetColor();
                        Console.Write(currentItem.information);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" | ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(currentItem.price * 0.85f);
                        Console.ResetColor();
                        Console.Write(" G\n");

                        idx++;

                    }
                }
               

            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            NextActionMessage();
            string select = Console.ReadLine();
            if(int.TryParse(select,out int number) && number -1 < player.inventory.Count)
            {
                if(number != 0)
                {
                    EquipItem item = (EquipItem)player.inventory[number - 1];
                    item.isSell = false;
                    player.gold += (int)(item.price * 0.85f);
                    string chr = "[E] ";
                    int chrIdx = item.name.ToString().IndexOf("[E] ");
                    if (chrIdx != -1)
                    {
                        player.IncreaseDamageAndArmor(-item.damage, -item.armor);
                        item.name.Remove(chrIdx, chr.Length);
                    }

                    for (int i = 0; i < merchant.equipItems.Count; i++)
                    {
                        if (merchant.equipItems[i].name == item.name)
                        {
                            if (merchant.equipItems[i].isSell)
                            {
                                merchant.equipItems[i] = item;
                            }
                        }
                    }
                    player.inventory.RemoveAt(number - 1);
                    
                    Console.Clear();
                    MerchantEquipSellManagement();
                }
                else
                {
                    Console.Clear();
                    MerchantEquipMenu();
                }
                

            }
            else
            {
                Console.Clear();
                WrongInput();
                MerchantEquipSellManagement();
            }

        }

        void MerchantConMenu()
        {

        }

        //---------------------------------------------------------------------------------------------------------------------Merchant
        //---------------------------------------------------------------------------------------------------------------------Dungeon
        void DungeonMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            DarkRedText("[던전 입장]\n");
            Console.WriteLine();
            Console.WriteLine("방어력이 낮으면 아마 40% 확률로 실패할걸세... ");
            Console.WriteLine("한번 들어가면 돌이킬 수 없다네.. 그래도 들어갈텐가..?");
            Console.WriteLine();
            Console.Write("현재 체력 : ");
            DarkRedText(player.currentHealth.ToString());
            Console.WriteLine();
            Console.Write("현재 방어력: ");
            DarkBlueText((player.armor + player.increaseInArmor).ToString());
            Console.WriteLine();
           
            Console.WriteLine();
            for(int i = 0; i< dungeonManager.dungeons.Count; i++)
            {
                Console.Write($"{i+1}. {dungeonManager.dungeons[i].dungeonName}\t");
                YellowText(" | ");
                Console.Write("권장 방어력\t: ");
                DarkRedText(dungeonManager.dungeons[i].recommendArmor.ToString());
                Console.Write(" 이상\n");
                
            }
            Console.WriteLine();
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            NextActionMessage();
            string select = Console.ReadLine();

            if(int.TryParse(select,out int number) && number-1<dungeonManager.dungeons.Count)
            {
                if(number != 0)
                {
                    Console.Clear();
                    dungeonManager.EnterDungeon(dungeonManager.dungeons[number - 1], player);
                }
                else
                {
                    Console.Clear();
                }

            }
            else
            {
                Console.Clear();
            }

        }
        //---------------------------------------------------------------------------------------------------------------------Dungeon
        //---------------------------------------------------------------------------------------------------------------------Rest
        void RestMenu()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------Rest
        //----------------------------------------------------------------------------Menu

        //----------------------------------------------------------------------------Message


        void ConsoleClear()
		{
			int sizeW = 100;
			int sizeH = 100;
			Console.SetCursorPosition(0, 0);

			for (int i = 0; i < sizeW; i++)
			{
				for (int j = 0; j < sizeH; j++)
				{
					Console.Write("");
				}
				Console.WriteLine();
			}
			Console.SetCursorPosition(0, 0);
		}

		void NextActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해 주세요.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">> ");
        }


        //void SelectClassMessage()
        //{
        //          Console.WriteLine("클래스를 선택해 주세요.");
        //          Console.WriteLine("1. Warrior ");
        //          string num = Console.ReadLine();
        //          if (num == "1")
        //	{
        //		player = new Player();
        //		isSelectClass = true;


        //          }
        //	else
        //	{
        //              SelectClassMessage();
        //	}

        //      }


        void WrongInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("잘못된 입력 입니다.");
            Console.ResetColor();
        }

        void DarkRedText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str);
            Console.ResetColor();
        }
        void YellowText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(str);
            Console.ResetColor();
        }
        void DarkBlueText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(str);
            Console.ResetColor();
        }

        //----------------------------------------------------------------------------Message

    }
}

