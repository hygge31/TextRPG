using System;
using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json;

namespace TextRPG
{
	public enum GameState
	{
		Play,
		GameOver
	}


    public class GameManager :ConsoleText
	{
        static public GameState gameState;
        Player player;
        Merchant merchant;
        DungeonManager dungeonManager;
		bool isSelectClass;

        
        public void GameStart()
		{
            gameState = GameState.Play;
            merchant = new Merchant();
            dungeonManager = new DungeonManager();
            //player = new Player();
            LoadData();
            
            Console.WriteLine("이곳에 온걸 환영하네, \n던전에 들어가기 전에 이곳에서 준비하고 가시게나.\n\n");

			while (gameState == GameState.Play)
			{
                if (player.currentHealth == 0)
                {
                    gameState = GameState.GameOver;
                    Console.Clear();
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
                        Console.WriteLine("체력이 낮습니다!");
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
                    RestMenu();

                }
                else if (select == "6")
                {
                    //rest
                    Console.Clear();
                    SaveData();

                }
                else if (select == "7")
                {
                    //rest
                    Console.WriteLine("게임 저장 후 종료 합니다.");
                    SaveData();
                    break;

                }
                else
				{
					Console.Clear();
                    WrongInput();
				}

			}
            if(gameState == GameState.GameOver)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("던전 탐색중 사망하였습니다......");
                Console.WriteLine("로드 파일을 불러오겠습니까?");
                Console.WriteLine();
                Console.WriteLine("1. 예 ");
                Console.WriteLine("2. 아니요 ");
                Console.WriteLine();
                NextActionMessage();
                string select = Console.ReadLine();

                if(int.TryParse(select,out int number))
                {
                    if(number == 1)
                    {
                        GameStart();
                    }else if(number == 2)
                    {
                        TextAnimation("게임을 종료 합니다......",100);
                    }
                }

            }
            
            
		}


        //----------------------------------------------------------------------------Menu

        string MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전");
            Console.WriteLine("5. 휴식");
            Console.WriteLine("6. 게임 데이터 저장");
            Console.WriteLine("7. 게임 종료");
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
                case "6":
                    return "6";
                case "7":
                    return "7";
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


            Console.Write("Name\t: ");
            DarkMagentaText(player.playerName);
            Console.WriteLine();
            if (player.level < 10)
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
                    
					if(currentItem.category == "Weapon")
					{
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("- ");
                        Console.ResetColor();
                        //Console.Write(currentItem.name + "\t");
                        FindTextChangeColorGreen(currentItem.name.ToString(), "[E] ");
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
                        //Console.Write(currentItem.name + "\t");
                        FindTextChangeColorGreen(currentItem.name.ToString(), "[E] ");
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
                    if (currentItem.category == "Weapon")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(num+".");
                        Console.ResetColor();
                        //Console.Write(currentItem.name + "\t");
                        FindTextChangeColorGreen(currentItem.name.ToString(), equipStr);
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
                        //Console.Write(currentItem.name + "\t");
                        FindTextChangeColorGreen(currentItem.name.ToString(), equipStr);
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

                    if (currentItem.category == "Weapon")
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
                    }else if(currentItem.category == "Armor")
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
            //foreach (Object item in player.inventory)
            //{
            //    if (item is ConItem)
            //    {
            //        ConItem currentItem = (ConItem)item;
            //        Console.WriteLine($"{idx}. {currentItem.name}\t\t{currentItem.information}");
            //        idx++;
            //    }

            //}
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
            DarkYellowText("-----------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            foreach (Object item in merchant.equipItems)
            {
                if (item is EquipItem)
                {
                    EquipItem currentItem = (EquipItem)item;
                    if (currentItem.category == "Weapon")
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
                DarkYellowText("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine();
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
            DarkYellowText("-----------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            int idx = 1;


            foreach (EquipItem item in merchant.equipItems)
            {
                    if (item.category == "Weapon")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(idx + ". ");
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
                        Console.Write(" | ");
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
                        Console.Write(idx + ". ");
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
                        Console.Write(" | ");
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
                DarkYellowText("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine();
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
            DarkYellowText("-----------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            int idx = 1;
            foreach (object item in player.inventory)
            {
                if(item is EquipItem)
                {
                    EquipItem currentItem = (EquipItem)item;
                    if (currentItem.category == "Weapon")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(idx + ". ");
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
                    DarkYellowText("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine();
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
            Console.WriteLine();
            Console.WriteLine();
            DarkRedText("[휴식하기]");
            Console.WriteLine();
            DarkYellowText("500");
            Console.Write("G ");
            Console.Write("를 지불하면 체력을 회복할 수 있습니다\n");
            Console.WriteLine();
            Console.Write("보유 골드 : ");
            YellowText(player.gold.ToString());
            Console.Write(" G");
            Console.WriteLine();
            Console.Write("현재 체력 : ");
            DarkRedText(player.currentHealth.ToString());
            DarkYellowText(" / ");
            RedText(player.maxHealth.ToString());
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("1. 휴식 하기");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            NextActionMessage();
            string select = Console.ReadLine();
            if(int.TryParse(select,out int number))
            {
                if(number == 1)
                {
                    if(player.gold < 500)
                    {
                        Console.Clear();
                        RedText("Gold");
                        Console.Write("가 부족 합니다.\n");
                        RestMenu();
                    }
                    else
                    {
                        if(player.currentHealth == player.maxHealth)
                        {
                            Console.Clear();
                            DarkRedText("체력이 이미 꽉 차있습니다.");
                            Console.WriteLine();
                            RestMenu();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine();
                            player.currentHealth = player.maxHealth;
                            TextAnimation("체력을 회복중 입니다............", 200);
                            Console.Clear();
                            GreenText("체력을 모두 회복했습니다!");
                            RestMenu();
                        }
                        

                    }
                }
                else
                {
                    Console.Clear();
                }
            }

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

        //----------------------------------------------------------------------------Message
        //----------------------------------------------------------------------------Save And Load
        void SaveData()
        {
            TextAnimation("데이터 저장중..........", 200);
            string currentDirectory = Directory.GetCurrentDirectory();
            string saveDataFolderPath = Path.Combine(currentDirectory, "SaveData");
            if (!Directory.Exists(saveDataFolderPath))
            {
                Directory.CreateDirectory(saveDataFolderPath);
            }
            string json = JsonConvert.SerializeObject(player, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include
            });

            File.WriteAllText(saveDataFolderPath + "/saveData.json", json);
            GreenText("데이터 저장 완료");

        }

        void LoadData()
        {
            TextAnimation("데이터 로드중...............", 200);
            string currentDirectory = Directory.GetCurrentDirectory();
            string saveDataFolderPath = Path.Combine(currentDirectory, "SaveData");
            if (File.Exists(saveDataFolderPath + "/saveData.json"))
            {
                string json = File.ReadAllText(saveDataFolderPath + "/saveData.json");
                Player loadPlayer = JsonConvert.DeserializeObject<Player>(json);
                player = loadPlayer;
                GreenText("데이터 로드 완료.");
                Console.WriteLine();

            }
            else
            {
                RedText("저장된 데이터가 없습니다.");
                Console.WriteLine();
                RedText("캐릭터를 새로 만듭니다.");
                Console.WriteLine();
                RedText("플레이어의 이름을 입력해 주세요.");
                Console.WriteLine();
                Console.WriteLine();
                GreenText(">> ");
                string playerName = Console.ReadLine();
                Console.Clear();
                Console.Write("플레이어의 이름이 ");
                BlueText(playerName);
                Console.Write(" 맞습니까?\n");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. 예");
                Console.WriteLine("2. 아니요");
                NextActionMessage();
                string select = Console.ReadLine();
                if(int.TryParse(select,out int number))
                {
                    if(number == 1)
                    {
                        player = new Player();
                        player.Init();
                        player.playerName = playerName;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        TextAnimation("캐릭터를 생성중 입니다........", 100);
                        Console.ResetColor();
                        Console.Clear();
                        GreenText("생성 완료!!  데이터 저장을 해주세요.");
                        Console.WriteLine();
                    }
                    else
                    {
                        LoadData();
                    }
                }




            }

        }

        //----------------------------------------------------------------------------Save And Load
    }
}

