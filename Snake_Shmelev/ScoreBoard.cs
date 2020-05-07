using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Shmelev
{
    class ScoreBoard
    {
		public void scoreboard(int points)
		{
			System.Threading.Thread.Sleep(3000);
		again:
			Console.Clear();
			int xOffset = 25;
			int yOffset = 8;
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(xOffset, yOffset++);
			Console.Write("Name : ");
			string input = Console.ReadLine();
			if (input.Length < 3) goto again;
			StreamWriter sw = new StreamWriter("Name.txt", true);
			sw.WriteLine($"{input}");
			sw.Close();
			StreamWriter psw = new StreamWriter("Points.txt", true);
			psw.WriteLine($"{points}");
			psw.Close();
			Console.Clear();

			string[] sr = System.IO.File.ReadAllLines("Name.txt");
			string[] psr = System.IO.File.ReadAllLines("Points.txt");

			int[] psr2 = new int[psr.Length];
			for (int i = 0; i < psr.Length; i++)
			{
				psr2[i] = int.Parse(psr[i]);
			}

			int temp = 0;
			string temp2 = "";

			for (int write = 0; write < psr.Length; write++)
			{
				for (int sort = 0; sort < psr.Length - 1; sort++)
				{
					if (psr2[sort] < psr2[sort + 1])
					{
						temp = psr2[sort + 1];
						psr2[sort + 1] = psr2[sort];
						psr2[sort] = temp;

						temp2 = sr[sort + 1];
						sr[sort + 1] = sr[sort];
						sr[sort] = temp2;
					}
				}
			}

			for (int i = 0; i < 3; i++)
			{
				Console.SetCursorPosition(xOffset, yOffset++);
				Console.WriteLine($"{sr[i]} P: {psr2[i]}");
				yOffset++;
			}
		}
	}
}
