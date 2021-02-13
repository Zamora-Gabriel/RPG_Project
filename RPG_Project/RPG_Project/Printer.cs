using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Printer
    {
        

        const int WINDOW_SIZE = 80;

        //
        // COMMENT ALL OF THIS BETTER SO ITS NOT A MESS
        //

        ///
        /// TMP ART REMOVE LATER
        ///
        string[] enemyOneArt = new string[] { "o~\\", "|_-__\\" };

        ///
        /// TMP ART REMOVE LATER
        ///

        /// <summary>
        /// Slightly modified version of the code I used to print things in my c# dice shooter project, Really hope it's okay I resued my code.
        /// Takes a string array and centeres it and prints in a border.
        /// </summary>
        /// <param name="text"></param>         //Array of string to be printed - MANDATORY
        /// <param name="printTop"></param>     //Bool value for if you want the top border to print. Default to true    - Optional
        /// <param name="printBottom"></param>  //Bool value for if you want the bottom border to print. Default to true - Optional
        /// <param name="colour"></param>       //Colour of font, defaults to dark-yellow -Optional
        public void PrintArray(string[] text, bool printTop = true, bool printBottom = true, int colour = 0)
        {
            //Colour selection
            ConsoleColor[] colourChoices = { ConsoleColor.DarkYellow, ConsoleColor.DarkCyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.Cyan };

            //strings for box sides
            string border = new String('─', WINDOW_SIZE - 2);
            string borderSide = ("│");
            string topBorder = ($"┌{border}┐");
            string bottomBorder = ($"└{border}┘");

            //calculates middle of box and full console
            int windowWidth = WINDOW_SIZE - (2 * borderSide.Length);
            int winWidth = (Console.WindowWidth / 2);

            //print top row
            if (printTop)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0," + ((winWidth) + ((border.Length / 2) +1)) + "}", topBorder);
            }

            //loop through array prints each line
            for (int i = 0; i < text.Length; i++)
            {
                //sets any formatting variables before passing strings to centering
                string processedVar = String.Format(text[i]);
                //changes colour for border and prints left edge
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string leftBorder = String.Format("{0," + ((winWidth) - (windowWidth / 2)) + "}", borderSide);
                Console.Write(leftBorder);
                //changes colour for text and prints body
                Console.ForegroundColor = colourChoices[colour];
                Console.Write("{0," + ((winWidth) + (processedVar.Length / 2) - leftBorder.Length) + "}", processedVar);
                //changes colour back for border and prints right edge
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                int offset = ((windowWidth / 2 + 1) - (processedVar.Length / 2));
                Console.WriteLine("{0," + (offset) + "}", borderSide);
            }

            //prints bottom of box5
            if (printBottom)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0," + ((winWidth) + ((border.Length / 2)+1)) + "}", bottomBorder);
            }
        }



        /// <summary>
        /// Takes a single string and centeres it while printing in a border
        /// </summary>
        /// <param name="text"></param>         //The string to be printed - MANDATORY
        /// <param name="printTop"></param>     //Bool value for if you want the top border to print. Default to true    - Optional
        /// <param name="printBottom"></param>  //Bool value for if you want the bottom border to print. Default to true - Optional
        /// <param name="colour"></param>       //Int for colour of font, defaults to dark-yellow -Optional
        /// <param name="name"></param>         //String for formated strings   -Optional
        /// <param name="age"></param>          //Int for formated strings      -Optional
        public void PrintSingle(string text, bool printTop = true, bool printBottom = true, string name = "", int damage = 0)
        {
            ConsoleColor[] colourChoices = { ConsoleColor.DarkYellow, ConsoleColor.DarkCyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.Cyan };
            //strings for box sides
            string border = new String('─', WINDOW_SIZE - 2);
            string borderSide = ("│");
            string topBorder = ($"┌{border}┐");
            string bottomBorder = ($"└{border}┘");

            //calculates middle of box and full console
            int windowWidth = WINDOW_SIZE - (2 * borderSide.Length);
            int winWidth = (Console.WindowWidth / 2);

            //print top of box
            if (printTop)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0," + ((winWidth) + ((border.Length / 2))) + "}", topBorder);
            }
            
            string processedVar = String.Format(text, name, damage);

            //changes colour for border and prints left edge
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string leftBorder = String.Format("{0," + ((winWidth) - (windowWidth / 2) -1) + "}", borderSide);
            Console.Write(leftBorder);
            //changes colour for text and prints body
            Console.Write("{0," + ((winWidth) + (processedVar.Length / 2) - leftBorder.Length) + "}", processedVar);
            //changes colour back for border and prints right edge
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            int offset = ((windowWidth / 2) - (processedVar.Length / 2));
            Console.WriteLine("{0," + (offset) + "}", borderSide);
            //prints bottom of box
            if (printBottom)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0," + ((winWidth) + ((border.Length / 2))) + "}", bottomBorder);
            }
        }



        /// <summary>
        /// These next four functions work together to print the main battle menu
        /// Each printing a seperate part of the menu
        /// 
        /// They could in possibly be combined, but it's terrible enough to read as is.
        /// </summary>

        public void PrintTopScreen(string[][] text)
        {
            //Colour selection
            ConsoleColor[] colourChoices = { ConsoleColor.DarkYellow, ConsoleColor.DarkCyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.DarkRed, ConsoleColor.Cyan };

            //strings for box sides
            string border = new String('─', WINDOW_SIZE - 2);
            string borderSide = ("│");
            string topBorder = ($"┌{border}┐");
            string bottomBorder = ($"├{border}┤");

            //calculates middle of box and full console
            int windowWidth = WINDOW_SIZE - (2 * borderSide.Length);
            int winWidth = (Console.WindowWidth / 2);

            //print top row
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0," + ((winWidth) + ((border.Length / 2))) + "}", topBorder);

            int currentLine = 1;
            bool printing = true;
           // int x = 0;
            int y = 0;
            int offset = 0;
            string processedLine = "";
            //loop through array prints each line
            while (printing)
            {
                processedLine = string.Format("{0," + ((winWidth) - (windowWidth / 2) - 1) + "}", borderSide);
                for (int x = 0; x < text.Length; x++)
                {

                    y = currentLine - 1;
                    while (y < currentLine)
                    {
                        switch (text.Length)
                        {
                            case 1:
                                processedLine += string.Format("{0," + ((winWidth) + (text[x][y].Length / 2) - processedLine.Length) + "}", text[x][y]);
                                offset = ((windowWidth / 2) - (text[x][y].Length / 2));
                                break;
                            case 2:
                                processedLine += string.Format("{0," + ((winWidth/3) + (text[x][y].Length / 2) + processedLine.Length/6) + "}", text[x][y]);
                                offset = ((windowWidth / 2) - 1 - (processedLine.Length / 4));
                                break;
                            case 3:
                                processedLine += string.Format("{0," + ((winWidth / 4) + (text[x][y].Length / 3) + processedLine.Length / 10) + "}", text[x][y]);
                                offset = ((windowWidth / 2) + 2 - (processedLine.Length / 3));
                                break;
                        }
                        y++;
                    }
                }
                processedLine += string.Format("{0," + (offset) + "}", borderSide);
                Console.WriteLine("{0}", processedLine);
                currentLine++;
                if(currentLine == text[0].Length+1)
                {
                    printing = false;
                }
            }
            Console.WriteLine("{0," + ((winWidth) + ((border.Length / 2))) + "}", bottomBorder);
        }

        public void PrintMiddleScreen(string[] text)
        {

            string border = new String('─', WINDOW_SIZE - 2);
            string borderSide = ("│");

            //calculates middle of box and full console
            int windowWidth = WINDOW_SIZE - (2 * borderSide.Length);
            int winWidth = (Console.WindowWidth / 2);

            //loop through array prints each line
            for (int i = 0; i < text.Length; i++)
            {
                string leftBorder = String.Format("{0," + ((winWidth) - (windowWidth / 2) -1) + "}", borderSide);
                Console.Write(leftBorder);
                Console.Write("{0," + ((winWidth) + (text[i].Length / 2) - leftBorder.Length) + "}", text[i]);
                int offset = ((windowWidth / 2) - (text[i].Length / 2));
                Console.WriteLine("{0," + (offset) + "}", borderSide);
            }
        }

        public void PrintBottomScreen(string[] text)
        {
            string border = new String('─', WINDOW_SIZE - 2);
            string borderSide = ("│");
            string bottomBorder = ($"└{border}┘");

            //calculates middle of box and full console
            int windowWidth = WINDOW_SIZE - (2 * borderSide.Length);
            int winWidth = (Console.WindowWidth / 2);

            //loop through array prints each line
            for (int i = 0; i < text.Length; i++)
            {
                string leftBorder = String.Format("{0," + ((winWidth) - (windowWidth / 2) - 1) + "}", borderSide);
                Console.Write(leftBorder);
                Console.Write("{0," + ((winWidth) + (text[i].Length / 2) - leftBorder.Length) + "}", text[i]);
                int offset = ((windowWidth / 2) - (text[i].Length / 2));
                Console.WriteLine("{0," + (offset) + "}", borderSide);
            }
            Console.WriteLine("{0," + ((winWidth) + ((border.Length / 2))) + "}", bottomBorder);
        }


        public string[] PrepareString(string[] text, int currentHP, int maxHP, string Name, int colour = 0)
        {
            string[] returnString = new string[text.Length];
            int windowSize = 15;

            //loop through array prints each line
            for (int i = 0; i < text.Length; i++)
            {
                string finalString;
                string processedVar = string.Format(text[i], currentHP, maxHP, Name);
                finalString = string.Format("{0," + ((windowSize / 2) + (processedVar.Length / 2)) + "}", processedVar);
                returnString[i] = finalString;
            }
            return returnString;
        }
    }
}
