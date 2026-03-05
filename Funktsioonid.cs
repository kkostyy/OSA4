using System;
using System.Collections.Generic;
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
