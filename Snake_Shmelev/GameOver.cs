using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Shmelev
{
    class GameOver
    {

		public void writegameover(int points)
		{
			int xOffset = 25;
			int yOffset = 8;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(xOffset, yOffset++);
			writetext("============================", xOffset, yOffset++);
			writetext("  G  A  M  E   O  V  E  R", xOffset + 1, yOffset++);
			yOffset++;
			writetext(" Author : Mikhail Shmelev", xOffset + 2, yOffset++);
			yOffset++;
			writetext("============================", xOffset, yOffset++);
			yOffset++;
			writetext($" Your Score: {points}", xOffset, yOffset++);

		}

		public void writetext(String text, int xOffset, int yOffset)
		{
			Console.SetCursorPosition(xOffset, yOffset);
			Console.WriteLine(text);
		}
	}
}
