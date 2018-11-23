using System;
using System.Collections;
using System.IO;

namespace Udraw
{
    internal class cursorposition
    {
        public ConsoleColor brushcolor ( )
        {
            int choice = 0;
            Console.Write("Choose the brush Color\n1.Red\n2.Blue\n3.Green\n4.White\n5.Yellow\n6.Cyan\n");
            choice=int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: return ConsoleColor.Red;
                case 2: return ConsoleColor.Blue;
                case 3: return ConsoleColor.Green;
                case 4: return ConsoleColor.White;
                case 5: return ConsoleColor.Yellow;
                case 6: return ConsoleColor.Cyan;
                default: return ConsoleColor.White;
            }
        }

        public char brushshape ( )
        {
            int choice = 0;
            Console.Write("1.Blocks\n2.Airbrush\n3.Circle\n");
            choice=int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: return (char)220;
                case 2: return (char)176;
                case 3: return (char)248;
                default: return (char)220;
            }
        }

        public void create ( )
        {
            Stack saver = new Stack();
            char cursor = brushshape();

            Console.Clear(); char temp = cursor;
            Console.ForegroundColor=brushcolor();
            Console.Clear();
            Console.WriteLine("INSTRUCTIONS\n1.W|A|S|D to control the cursor.\n2.I to turn on the Eraser mode and O to turn it off.\n3.Press e to exit\n4.Controls are all case insensitive.\n4.Press r to reset");
            Console.ReadKey();
            Console.Clear();
            FileStream fs = new FileStream(@"ABC.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            StreamReader sr = new StreamReader(fs);

            Console.Clear();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            int x = Console.LargestWindowWidth/2;
            int y = Console.LargestWindowHeight/2;
            bool loopcondition = true;
            char t = (char)0;
            while (loopcondition)
            {
                char command = Console.ReadKey(true).KeyChar;

                if (command=='w')
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(cursor);
                    y=y-1;
                    saver=save(x, y, cursor, Console.ForegroundColor);
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+";");
                }
                else if (command=='a')
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(cursor);
                    x=x-1;
                    saver=save(x, y, cursor, Console.ForegroundColor);
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+";");
                }
                else if (command=='s')
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(cursor);
                    y=y+1;
                    saver=save(x, y, cursor, Console.ForegroundColor);
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+";");
                }
                else if (command=='d')
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(cursor);
                    x=x+1;
                    saver=save(x, y, cursor, Console.ForegroundColor);
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+",");
                    saver.Pop();
                    sw.Write(saver.Peek()+";");
                }
                else if (command=='e')
                {
                    Console.Clear();

                    sw.BaseStream.Close();
                    fs.Close();
                    Console.Write("Do you want to save the file (y/n)");
                    char c = Convert.ToChar(Console.ReadLine());
                    if (c=='y')
                    {
                        Console.Write("Saving....\n");
                        System.Threading.Thread.Sleep(2000);

                        Console.Clear();
                        Console.Write("Enter the name of the file : ");
                        string name = Console.ReadLine()+".txt";
                        File.Move(@"ABC.txt", name);

                        File.Delete(@"ABC.txt");
                        Console.Clear();

                        loopcondition=false;
                    }
                    else
                    {
                        File.Delete(@"ABC.txt");
                        loopcondition=false;
                        Console.Clear();
                    }

                    Environment.Exit(0);
                }
                else if (command=='r')
                {
                    Console.Clear();
                    x=Console.LargestWindowWidth/2;
                    y=Console.LargestWindowHeight/2;
                    Console.SetCursorPosition(x, y);
                    saver.Clear();
                    File.WriteAllText(fs.Name, "");
                }
                else if (command=='t')
                {
                    cursor=(char)0;
                }
                else if (command=='o')
                {
                    cursor=temp;
                }
            }
        }

        public Stack save (int x, int y, char c, ConsoleColor color)
        {
            Stack s = new Stack();
            s.Push(x);
            s.Push(y);
            s.Push(c);
            s.Push(color);

            return s;
        }

        public void load ( )
        {
            Console.Write("Enter the name of the file:"); string name = Console.ReadLine();
            name=name+".txt";
            FileStream fs = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);

            while (!sr.EndOfStream)
            {
                string[] stringArray = sr.ReadLine().Split(';');
                string[,] ct = new string[4, stringArray.Length];
                string[] s = new string[4];
                for (int i = 0; i<stringArray.Length; i++)
                {
                    for (int j = 0; j<4; j++)
                    {
                        s=stringArray[i].Split(',');
                        ct[j, i]=s[j];
                    }
                }
                Console.Clear();
                for (int i = 0; i<stringArray.Length; i++)
                {
                    if (ct[0, i]=="Red")
                    {
                        Console.ForegroundColor=ConsoleColor.Red;
                    }
                    else if (ct[0, i]=="Yellow")
                    {
                        Console.ForegroundColor=ConsoleColor.Yellow;
                    }
                    else if (ct[0, i]=="Blue")
                    {
                        Console.ForegroundColor=ConsoleColor.Blue;
                    }
                    else if (ct[0, i]=="Green")
                    {
                        Console.ForegroundColor=ConsoleColor.Green;
                    }
                    else if (ct[0, i]=="Cyan")
                    {
                        Console.ForegroundColor=ConsoleColor.Cyan;
                    }
                    else if (ct[0, i]=="White")
                    {
                        Console.ForegroundColor=ConsoleColor.White;
                    }

                    Console.WindowHeight=Console.LargestWindowHeight;
                    Console.WindowWidth=Console.LargestWindowWidth;
                    char cursor = Convert.ToChar(ct[1, i]);
                    Console.SetCursorPosition(int.Parse(ct[3, i]), int.Parse(ct[2, i]));
                    Console.Write(cursor);
                }
                char command = Console.ReadKey(true).KeyChar;
            }
        }

        public void mainmenu ( )
        {
            Console.Write("1.Create\n2.Load\n3.Exit\n");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1: Console.Clear(); create(); break;
                case 2: load(); break;
                case 3: break;
                default: break;
            }
        }
    }

    internal class Program
    {
        private static void Main (string[] args)
        {
            cursorposition c = new cursorposition();
            c.mainmenu();
        }
    }
}