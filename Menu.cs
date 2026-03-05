using System;
using System.Text;

namespace Failitootlus
{
    public class Menu
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\nFailitootlus - ülesanded");
                Console.WriteLine("1. Lemmiktoidu salvestamine (StreamWriter)");
                Console.WriteLine("2. Menüü kuvamine (StreamReader)");
                Console.WriteLine("3. Koostisosade muutmine (List + ReadAllLines)");
                Console.WriteLine("4. Külmkapi kontroll (Contains)");
                Console.WriteLine("5. Salvestamine tagasi faili (WriteAllLines)");
                Console.WriteLine("0. exit");

                string valik = Console.ReadLine();

                if (valik == "1")
                {
                    Funktsioonid.LemmiktoiduSalvestamine();
                }
                else if (valik == "2")
                {
                    Funktsioonid.MenüüKuvamine();
                }
                else if (valik == "3")
                {
                    Funktsioonid.KoostisosadeMuutmine();
                }
                else if (valik == "4")
                {
                    Funktsioonid.KülmkapiKontroll();
                }
                else if (valik == "5")
                {
                    Funktsioonid.UuendatudSalvestamine();
                }
                else if (valik == "0")
                {
                    Console.WriteLine("Nägemist!");
                    break;
                }
                else
                {
                    Console.WriteLine("Palun vali 0-5");
                }
            }
        }
    }
}
