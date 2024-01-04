using System;
namespace TextRPG
{
	public class ConsoleText
	{
        public void WrongInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("잘못된 입력 입니다.");
            Console.ResetColor();
        }

        public void DarkRedText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str);
            Console.ResetColor();
        }
        public void RedText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str);
            Console.ResetColor();
        }

        public void YellowText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(str);
            Console.ResetColor();
        }
        public void DarkYellowText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(str);
            Console.ResetColor();
        }

        public void DarkBlueText(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(str);
            Console.ResetColor();
        }
        public void BlueText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(str);
            Console.ResetColor();
        }
        public void GreenText(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str);
            Console.ResetColor();
        }


        public void NextActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해 주세요.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">> ");
        }



        public void TextAnimation(string str,int animationSpeed)
        {
            foreach(char c in str)
            {
                Console.Write(c);
                Thread.Sleep(animationSpeed);
            }
            Console.Clear();
        }


    }
}

