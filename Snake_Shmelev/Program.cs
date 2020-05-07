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

/*TODO LIST*/
/*1. Сделать GAMEOVER в классе - Сделано*/
/*2. Сделать SCOREBOARD в классе - Сделано*/
/*3. Поменять цвет заднего фона*/
/*4. Изменить цвет барьеров*/
/*5. Изменить цвет змейки*/

namespace Snake_Shmelev
{
	class Program
	{
		static void Main(string[] args)
		{
			Sounds player = new Sounds(@"C:\Users\Anqlem\source\repos\Snake_Shmelev\Snake_Shmelev\resourses\");
			player.Play();

			Sounds player2 = new Sounds(@"C:\Users\Anqlem\source\repos\Snake_Shmelev\Snake_Shmelev\resourses\");

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
			ScoreBoard scoreboard = new ScoreBoard();
			GameOver writegameover = new GameOver();
			writegameover.writegameover(points);
			player.Play("GO.mp3");
			scoreboard.scoreboard(points);
			Console.ReadLine();
		}

	}
}