using System;

namespace FinalPJ
{
    internal class Program
    {
        public static float Question() //generate question and return answer as float
        {
            Random num = new Random();
            float result = 0f;
            int display = 0; //symbol index that display after number
            int memo = 0; //First operation index (from 1st display)
            int memo2 = 0; // Second operation index (from 2nd display)
            string[] symbol = { "+", "-", "x", "/" };

            for (int i = 1; i <= 3; i++)
            {
                int number = num.Next(0, 100); //generate number
                int opIndex = num.Next(0, 4);

                while (number == 0 && (memo == 3 || memo2 == 3)) //Divided by zero protection
                {
                    number = num.Next(1, 100);
                }
                Console.Write(number + " ");

                if (i == 1)  //first number
                {
                    display += opIndex;
                    result += number;
                    memo += display;
                    Console.Write(symbol[display] + " ");
                    continue;
                }

                else if (i == 2)  //first number operate with second number
                {
                    display = 0;
                    display += opIndex;
                    memo2 += display;
                    Console.Write(symbol[display] + " ");
                }

                else if (i == 3)  //result from (i == 2) operate with third number
                {
                    memo = 0;
                    memo += memo2;
                }

                if (i > 1 && memo == 0) result += number; //plus

                else if (i > 1 && memo == 1) result -= number; //minus

                else if (i > 1 && memo == 2) result *= number; //multiply

                else if (i > 1 && memo == 3) result /= number; //divide

                if (i == 3) break;
            }
            Console.Write("= ?\n");
            return result;
        }
        public static void Game()
        {
            Console.WriteLine("===== Welcome To Calculate Fight =====\n");

            Random num = new Random();
            float result;
            int ans;
            int round = 0;
            int bossHp = 30000;
            int hp = 5000;

            Console.Write("What is your hero name : "); //username
            string name = Console.ReadLine();

            Console.WriteLine($"\nWelcome, {'"'}{name}{'"'} Please help me defeat The boss");
            Console.WriteLine($"\nRULES : Solve math questions {'"'}FROM LEFT TO RIGHT{'"'} to attack the boss");
            Console.WriteLine("\tIn case there's a decimal places" +
                "\n\t- if the next digit is 5 or more,increase the previous digit by one." +
                "\n\t- if it's 4 or less, keep the previous digit the same.");      //TUTORIAL
            
            Console.WriteLine("\n\tAre you ready?");
            Console.WriteLine("\tPress [E] to START.");
            while (Console.ReadKey(true).Key != ConsoleKey.E);   //START

            Console.WriteLine($"\n============== Round 1 ==============\n");
            Console.WriteLine("BossHP = 30000");
            Console.WriteLine($"{name}'s HP = 5000\n");

            while (bossHp > 0)
            {
                result = Question();
                Console.Write("\nYour Answer : ");
                string rawAns = Console.ReadLine();

                while (true) //Input Error Check
                {
                    try
                    {
                        ans = Convert.ToInt32(rawAns);
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\n**************************************");
                        Console.WriteLine("Please enter an integer and try again.");
                        Console.WriteLine("**************************************\n");
                    }
                    Console.Write("Your Answer : ");
                    rawAns = Console.ReadLine();
                }

                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - -\n");

                if (ans == Convert.ToInt32(result))
                {
                    int dmg = num.Next(2000, 5000);
                    Console.WriteLine($"CORRECT! {name} attacked the boss for {dmg} units!");
                    bossHp -=dmg;
                    round++;
                    Console.WriteLine($"\n============== Round {round+1} ==============\n");
                    Console.WriteLine($"Boss HP = {bossHp}\n{name}'s HP = {hp}\n");
                }
                else
                {
                    int bossDmg = num.Next(500, 1000);
                    Console.WriteLine($"WRONG! {name} was attacked by the boss for {bossDmg} units!");
                    Console.WriteLine($"ANSWER : {Convert.ToInt32(result)}");
                    hp -= bossDmg;
                    round++;
                    Console.WriteLine($"\n============== Round {round+1} ==============\n");
                    Console.WriteLine($"Boss HP = {bossHp}\n{name}'s HP = {hp}\n");
                }
                if (hp <= 0)
                {
                    Console.WriteLine("============== YOU DIE ==============");
                    break;
                }
            }
            if (bossHp <= 0) Console.WriteLine("============== VICTORY ==============");
        }
        public static void Main()
        {
            while (true)
            {
                Game();
                Console.WriteLine("\nPress [Enter] to end or Press any key to restart game\n\n\n");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("================= END =================");
                    break;
                }
                else continue;
            }
        }
    }
}
