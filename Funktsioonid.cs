using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Failitootlus
{
    public class Funktsioonid
    {
        // Ülesanne 1 – Lemmiktoidu salvestamine faili
        public static void LemmiktoiduSalvestamine()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Retseptid.txt");
                StreamWriter sw = new StreamWriter(path, true); // true = lisa lõppu

                Console.WriteLine("Sisesta Itaalia toidu nimi (nt Lasagne, Risotto): ");
                string toit = Console.ReadLine();

                sw.WriteLine(toit);
                sw.Close();

                Console.WriteLine("\"" + toit + "\" salvestatud faili Retseptid.txt");
            }
            catch (Exception)
            {
                Console.WriteLine("Viga faili kirjutamisel!");
            }
        }

        // Ülesanne 2 – Kogu menüü kuvamine
        public static void MenüüKuvamine()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Retseptid.txt");
                StreamReader sr = new StreamReader(path);
                string sisu = sr.ReadToEnd();
                sr.Close();

                Console.WriteLine("--- Retseptid.txt sisu ---");
                Console.WriteLine(sisu);
            }
            catch (Exception)
            {
                Console.WriteLine("Viga faili lugemisel! Kas Retseptid.txt eksisteerib?");
            }
        }

        // Ülesanne 3 – Koostisosade muutmine
        public static void KoostisosadeMuutmine()
        {
            LooKoostisosadTxt();

            Retsept retsept = new Retsept("Minu retsept");
            retsept.LaeFailed("Koostisosad.txt");

            Console.WriteLine("--- Algne nimekiri ---");
            retsept.Kuva();

            // Kasutaja otsustab, kas muuta esimest elementi
            Console.WriteLine("Kas soovid muuta esimest koostisosa? (jah/ei): ");
            string muutaVastus = Console.ReadLine();

            if (muutaVastus.ToLower() == "jah")
            {
                Console.WriteLine("Sisesta uus väärtus: ");
                string uusVäärtus = Console.ReadLine();
                retsept.Koostisosad[0] = uusVäärtus;
                Console.WriteLine("Esimene element muudetud.");
            }
            else
            {
                Console.WriteLine("Esimene element jäi muutmata.");
            }

            // Kasutaja otsustab, mida eemaldada
            Console.WriteLine("Sisesta koostisosa nimi, mida eemaldada (või jäta tühjaks): ");
            string eemalda = Console.ReadLine();

            if (eemalda != "")
            {
                retsept.Eemalda(eemalda);
            }
            else
            {
                Console.WriteLine("Midagi ei eemaldatud.");
            }

            Console.WriteLine("--- Uuendatud nimekiri ---");
            retsept.Kuva();
        }

        // Ülesanne 4 – Külmkapi kontroll ehk otsing listist
        public static void KülmkapiKontroll()
        {
            LooKoostisosadTxt();

            Retsept retsept = new Retsept("Minu retsept");
            retsept.LaeFailed("Koostisosad.txt");

            Console.WriteLine("Sisesta koostisosa nimi, mida otsida: ");
            string otsitav = Console.ReadLine();

            if (retsept.OnOlemas(otsitav))
            {
                Console.WriteLine("Koostisosa on olemas!");
            }
            else
            {
                Console.WriteLine("Seda koostisosa meil retseptis ei ole.");
            }
        }

        // Ülesanne 5 – Uuendatud nimekirja salvestamine
        public static void UuendatudSalvestamine()
        {
            LooKoostisosadTxt();

            Retsept retsept = new Retsept("Minu retsept");
            retsept.LaeFailed("Koostisosad.txt");

            Console.WriteLine("--- Praegune nimekiri ---");
            retsept.Kuva();

            Console.WriteLine("Kas soovid muuta esimest koostisosa? (jah/ei): ");
            string muutaVastus = Console.ReadLine();

            if (muutaVastus.ToLower() == "jah")
            {
                Console.WriteLine("Sisesta uus väärtus: ");
                retsept.Koostisosad[0] = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Esimene element jäi muutmata.");
            }

            Console.WriteLine("Sisesta koostisosa nimi, mida eemaldada (või jäta tühjaks): ");
            string eemalda = Console.ReadLine();

            if (eemalda != "")
            {
                retsept.Eemalda(eemalda);
            }
            else
            {
                Console.WriteLine("Midagi ei eemaldatud.");
            }

            // Salvestame
            retsept.Salvesta("Koostisosad.txt");
        }

        // Ülesanne 6* – Itaalia restorani menüü (Failist Tuple'isse)
        public static void RestoranMenüü()
        {
            LooMenuTxt();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Menuu.txt");

            // Samm 2: List mis hoiab Tuple<roaNimi, koostisosad, hind>
            List<Tuple<string, string, double>> menyyList = new List<Tuple<string, string, double>>();

            try
            {
                // Samm 3: loe kõik read mällu
                string[] read = File.ReadAllLines(path);

                // Samm 4-5: foreach + Split(';') + double.Parse -> Lisa Tuple listi
                foreach (string rida in read)
                {
                    string[] osad = rida.Split(';');

                    if (osad.Length == 3)
                    {
                        string roaNimi = osad[0].Trim();
                        string koostisosad = osad[1].Trim();
                        double hind = double.Parse(osad[2].Trim(), CultureInfo.InvariantCulture);

                        menyyList.Add(Tuple.Create(roaNimi, koostisosad, hind));
                    }
                    else
                    {
                        Console.WriteLine("Vale formaat real: " + rida);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Viga faili lugemisel!");
                return;
            }

            // Samm 6: kujundatud menüü printimine PadRight joondusega
            Console.WriteLine();
            Console.WriteLine("    ITAALIA RESTORAN");
            Console.WriteLine("    Tere tulemast meie restorani!");
            Console.WriteLine();

            foreach (Tuple<string, string, double> roog in menyyList)
            {
                string roaNimi = roog.Item1;
                string koostisosad = roog.Item2;
                double hind = roog.Item3;

                // Roa nimi vasakul, hind paremal – PadRight joondus
                Console.WriteLine("  " + roaNimi.PadRight(33) + hind.ToString("F2") + " €");
                Console.WriteLine("    " + koostisosad);
                Console.WriteLine("  " + new string('-', 45));
            }

            // do-while ConsoleKeyInfo – oota kuni Backspace
            Console.WriteLine("-----do-------");
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            do
            {
                Console.WriteLine("Vajuta Backspace");
                key = Console.ReadKey();
            }
            while (key.Key != ConsoleKey.Backspace);

            Console.WriteLine("\nMenüü suletud.");
        }

        // Abimeetod – loob Menuu.txt kui faili pole
        static void LooMenuTxt()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Menuu.txt");
            if (!File.Exists(path))
            {
                File.WriteAllLines(path, new string[]
                {
                    "Margherita pitsa;San Marzano tomatid, värske mozzarella, basiilik;8.50",
                    "Pasta Carbonara;Spagetid, guanciale, pecorino juust, muna;12.00",
                    "Tiramisu;Mascarpone, espresso, savoiardi küpsised;6.50"
                });
                Console.WriteLine("Loodud Menuu.txt.");
            }
        }

        // Abimeetod – loob Koostisosad.txt kui faili pole
        static void LooKoostisosadTxt()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Koostisosad.txt");
            if (!File.Exists(path))
            {
                File.WriteAllLines(path, new string[]
                {
                    "Oliiviõli", "Küüslauk", "Tomat", "Ketsup", "Basiilik", "Parmesan", "Pasta"
                });
                Console.WriteLine("Loodud Koostisosad.txt.");
            }
        }
    }
}
