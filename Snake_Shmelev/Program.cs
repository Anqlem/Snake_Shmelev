using AxWMPLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;

namespace Snake_Shmelev
{
	class Program
	{
		static void Main(string[] args)
		{
			Sounds player = new Sounds(@"C:\Users\Anqlem\source\repos\Snake_Shmelev\Snake_Shmelev\resourses");
			player.Play();

			Sounds player2 = new Sounds(@"C:\Users\Anqlem\source\repos\Snake_Shmelev\Snake_Shmelev\resourses");

			Console.SetWindowSize(80, 25);

			Walls walls = new Walls(80, 25);
			walls.Draw();

			// Отрисовка точек			
			Point p = new Point(4, 5, '*');
			Snake snake = new Snake(p, 4, Direction.RIGHT);
			snake.Draw();

			FoodCreator foodCreator = new FoodCreator(80, 25, '$');
			Point food = foodCreator.CreateFood();
			food.Draw();
			int points = 0;

			while (true)
			{
				Console.SetCursorPosition(70, 2);
				Console.WriteLine($" P: {points}");
				if (walls.IsHit(snake) || snake.IsHitTail())
				{
					break;
				}
				if (snake.Eat(food))
				{
					points++;
					player2.Play("Effect.wav");
					food = foodCreator.CreateFood();
					food.Draw();
				}
				else
				{
					snake.Move();
				}

				Thread.Sleep(100);
				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo key = Console.ReadKey();
					snake.HandleKey(key.Key);
				}
			}
			WriteGameOver(points);
			player.Play("GO.mp3");
			ScoreBoard(points);
			Console.ReadLine();
		}

		static void WriteGameOver(int point)
		{
			int xOffset = 25;
			int yOffset = 8;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(xOffset, yOffset++);
			WriteText("============================", xOffset, yOffset++);
			WriteText("  G  A  M  E   O  V  E  R", xOffset + 1, yOffset++);
			yOffset++;
			WriteText("  Author : Mikhail Shmelev", xOffset + 2, yOffset++);
			yOffset++;
			WriteText("============================", xOffset, yOffset++);
			yOffset++;
			WriteText($" Your Score: {point}", xOffset, yOffset++);

		}

		static void WriteText(String text, int xOffset, int yOffset)
		{
			Console.SetCursorPosition(xOffset, yOffset);
			Console.WriteLine(text);
		}
		static void ScoreBoard(int point)
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
			StreamWriter sw = new StreamWriter("Nimetus.txt", true);
			sw.WriteLine($"{input}");
			sw.Close();
			StreamWriter psw = new StreamWriter("Points.txt", true);
			psw.WriteLine($"{point}");
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