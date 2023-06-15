namespace zipper
{
    public class consoleUI : UserInterface
    {
        public override void init()
        {
            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            // Select directories from %APPDATA%
            // List<string> roamingFolders = MultiSelectFromList(prefix: new string[] { "Files in [directory]", "_______" }, items: spider.ls(roaming), suffix: new string[] { "_______", "Please select the desired directories..." });
        }
        public override List<string> MultiSelectFromList(string[] prefix, string[] items, string[] suffix,
            ConsoleColor selectedStyling = ConsoleColor.Green,
            ConsoleColor highlightStyling = ConsoleColor.DarkGray)
        {
            //Console.WindowHeight
            // Upper and Lower bounds for scrollable space
            int scrollMin = prefix.Length + 1;
            int scrollMax = suffix.Length + Console.WindowHeight; // was: prefix.Length + items.Length;

            // Tracks the cursor location
            int cursorY = 0;

            // Tracks the view offset from the top of the list
            int offsetY = 0;

            // Available space for the list of items
            int listSpace = Console.WindowHeight - (prefix.Length + 1) - suffix.Length;

            // Tracks checked Items
            bool[] checkedItems = new bool[items.Length];

            Console.Clear();

            while (cursorY != -1)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                // Write prefix
                foreach (string phrase in prefix)
                    Console.WriteLine(phrase);

                for (int i = 0; (i < listSpace) && (0 <= i) && (i < items.Length); i++) //was (int i = 0; i < items.Length; i++)
                {
                    // Add check if 'caret' should be on this item
                    if (i == cursorY - offsetY)
                    {
                        Console.BackgroundColor = highlightStyling;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("-->");
                    }
                    else Console.Write("   ");

                    Console.ResetColor();
                    Console.Write(" ");
                    if (checkedItems[i + offsetY])
                        Console.ForegroundColor = selectedStyling;
                    Console.WriteLine(items[i + offsetY] + new String(' ', Console.WindowWidth - items[i + offsetY].Length - 4));
                    Console.ResetColor();
                }

                // Write suffix
                foreach (string phrase in suffix)
                    Console.WriteLine(phrase);

                int dY = 0;
            userInput:
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                    dY = -1;
                else if (key.Key == ConsoleKey.DownArrow)
                    dY = 1;
                else if (key.Key == ConsoleKey.Spacebar)
                    checkedItems[cursorY] = !checkedItems[cursorY];
                else if (key.Key == ConsoleKey.Enter)
                    break;
                else
                    goto userInput;

                if (cursorY + dY > items.Length - 1)
                {
                    cursorY = 0;
                    offsetY = 0;
                }
                else if (cursorY + dY < 0)
                {
                    cursorY = items.Length - 1;
                    if (items.Length > listSpace)
                        offsetY = items.Length - listSpace;
                    else
                        offsetY = 0;
                }
                else
                    cursorY += dY;

                if ((dY == 1) && (cursorY > listSpace - 1 + offsetY))
                    //offsetY = Math.Min(++offsetY, items.Length);
                    offsetY++;
                if ((dY == -1) && (cursorY < offsetY))
                    //offsetY = Math.Max(--offsetY, 0);
                    offsetY--;

                // No wrap around
                //cursorY = Math.Max(Math.Min(cursorY + dY, scrollMax), scrollMin);

            }

            List<string> selection = new List<string>();
            for (int i = 0; i < checkedItems.Length; i++)
                if (checkedItems[i])
                    selection.Add(items[i]);
            Console.Clear();
            return selection;
        }
        public override int SingleSelectFromList(
            string[] prefix, 
            string[] items, 
            string[] suffix,
            ConsoleColor selectedStyling = ConsoleColor.Green,
            ConsoleColor highlightStyling = ConsoleColor.DarkGray
            )
            /* Pass in a list, get out the index of the selected item. */
        {
            //Console.WindowHeight
            // Upper and Lower bounds for scrollable space
            int scrollMin = prefix.Length + 1;
            int scrollMax = suffix.Length + Console.WindowHeight; // was: prefix.Length + items.Length;

            // Tracks the cursor location
            int cursorY = 0;

            // Tracks the view offset from the top of the list
            int offsetY = 0;

            // Available space for the list of items
            int listSpace = Console.WindowHeight - (prefix.Length + 1) - suffix.Length;

            // Tracks checked Items
            bool[] checkedItems = new bool[items.Length];

            Console.Clear();

            while (cursorY != -1)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                // Write prefix
                foreach (string phrase in prefix)
                    Console.WriteLine(phrase);
                for (int i = 0; (i < listSpace) && (0 <= i) && (i < items.Length); i++) //was (int i = 0; i < items.Length; i++)
                {
                    // Add check if 'caret' should be on this item
                    if (i == cursorY - offsetY)
                    {
                        Console.BackgroundColor = highlightStyling;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("-->");
                    }
                    else Console.Write("   ");

                    Console.ResetColor();
                    Console.Write(" ");
                    if (checkedItems[i + offsetY])
                        Console.ForegroundColor = selectedStyling;
                    Console.WriteLine(items[i + offsetY] + new String(' ', Console.WindowWidth - items[i + offsetY].Length - 4));
                    Console.ResetColor();
                }

                // Write suffix
                foreach (string phrase in suffix)
                    Console.WriteLine(phrase);

                int dY = 0;
            userInput:
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                    dY = -1;
                else if (key.Key == ConsoleKey.DownArrow)
                    dY = 1;
                else if (key.Key == ConsoleKey.Spacebar) {
                        checkedItems[cursorY] = !checkedItems[cursorY];
                        break;
                    }
                else if (key.Key == ConsoleKey.Enter) {
                        checkedItems[cursorY] = !checkedItems[cursorY];
                        break;
                    }
                else
                    goto userInput;

                if (cursorY + dY > items.Length - 1)
                {
                    cursorY = 0;
                    offsetY = 0;
                }
                else if (cursorY + dY < 0)
                {
                    cursorY = items.Length - 1;
                    if (items.Length > listSpace)
                        offsetY = items.Length - listSpace;
                    else
                        offsetY = 0;
                }
                else
                    cursorY += dY;

                if ((dY == 1) && (cursorY > listSpace - 1 + offsetY))
                    //offsetY = Math.Min(++offsetY, items.Length);
                    offsetY++;
                if ((dY == -1) && (cursorY < offsetY))
                    //offsetY = Math.Max(--offsetY, 0);
                    offsetY--;

                // No wrap around
                // cursorY = Math.Max(Math.Min(cursorY + dY, scrollMax), scrollMin);

            }

            int selection = -1;
            for (int i = 0; i < checkedItems.Length; i++)
                if (checkedItems[i])
                    selection = i;
            Console.Clear();
            Console.CursorVisible = true;
            return selection;
        }
    }
}