﻿using System;
namespace TextRPG
{
	enum GameState
	{
		Play,
		GameOver
	}

   


    public class GameManager
	{
		GameState gameState;
		Player player;
		bool isSelectClass;

        public GameManager()
		{
			gameState = GameState.Play;
        }




        public void GameStart()
		{
			if (!isSelectClass)
			{
				SelectClassMessage();
				Console.Clear();

            }
			//클래스 선택


            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

			while (gameState == GameState.Play)
			{

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
				}
				else
				{
					Console.Clear();
                    Console.WriteLine("잘못된 입력 입니다.");
				}

			}
		}


        //----------------------------------------------------------------------------Menu

        string MainMenu()
        {
            Console.ResetColor();

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
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
                default:
					return "0";

            }

        }


        void StateMenu()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("상태 보기");
			Console.ResetColor();

			Console.Write($"LV\t: {player.level}\nClass\t: {player.clas}\nDamage\t: {player.attackDamge}\nArmor\t: {player.armor}\nHealth\t: {player.health}\nGold\t: {player.gold}\n");
			Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기 ");
			NextActionMessage();
			string select = Console.ReadLine();

			if (select != "0")
			{
				ConsoleClear();
                Console.WriteLine("잘못된 입력 입니다.");
                StateMenu();

			}
			else
			{
				Console.Clear();
			}
        }

		void InventoryMenu()
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("인벤토리;");
			Console.ResetColor();

			Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
			Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 관리");
            Console.WriteLine("0. 나가기");
			NextActionMessage();
			string num = Console.ReadLine();
			if (num == "1")
			{
				Console.Clear();
                ShowMyEquipmentMenu();

			}else if (num == "2")
			{
				//inventory
				ShowMyInventory();

			}
			else if (num != "0")
			{
				Console.Clear();
				Console.WriteLine("잘못 입력했습니다.");
                InventoryMenu();
            }
            else
            {
                Console.Clear();
            }
			
        }


        void ShowMyEquipmentMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("인벤토리 - 장착 관리");
			Console.ResetColor();
            Console.WriteLine("보유중인 아이템을 관리 할 수 있습니다.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("[장착 목록]");
            Console.WriteLine();
            Console.WriteLine("\t[방어구]");
            Console.WriteLine();
            Console.Write("모자\t: {0}\t", player.equipment[0]?.name);
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("방어력: {0}\n", player.equipment[0]?.armor);
			Console.ResetColor();

            Console.Write("상의\t: {0}\t\t", player.equipment[1]?.name);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("방어력: {0}\n", player.equipment[1]?.armor);
            Console.ResetColor();
            Console.Write("하의\t: {0}\t", player.equipment[2]?.name);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("방어력: {0}\n", player.equipment[2]?.armor);
            Console.ResetColor();
            Console.Write("장갑\t: {0}\t", player.equipment[3]?.name);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("방어력: {0}\n", player.equipment[3]?.armor);
            Console.ResetColor();
            Console.Write("신발\t: {0}\t", player.equipment[4]?.name);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("방어력: {0}\n", player.equipment[4]?.armor);

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("\t[무기]");
            Console.WriteLine();
            Console.Write("오른손\t: {0}", player.weaponEqu[0]?.name);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\t공격력 : {0}\n", player.weaponEqu[0]?.damage);
            Console.ResetColor();

            Console.Write("왼손\t: {0}", player.weaponEqu[1]?.name);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\t공격력 : {0}\n", player.weaponEqu[1]?.damage);
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            NextActionMessage();
            string num = Console.ReadLine();
            if (num != "0")
            {
                ShowMyEquipmentMenu();
			}
			else
			{
				Console.Clear();
                InventoryMenu();
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


		void SelectClassMessage()
		{
            Console.WriteLine("클래스를 선택해 주세요.");
            Console.WriteLine("1. Warrior ");
            string num = Console.ReadLine();
            if (num == "1")
			{
				player = new Player("Warrior");
				isSelectClass = true;


            }
			else
			{
                SelectClassMessage();
			}

        }

		
        //----------------------------------------------------------------------------Message

    }
}

