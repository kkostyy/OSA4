using System;
using System.Collections.Generic;
using System.IO;

namespace Failitootlus
{
    // Klass Retsept – hoiab retsepti nime ja koostisosade listi
    public class Retsept
    {
        public string Nimi;
        public List<string> Koostisosad;

        public Retsept(string nimi)
        {
            Nimi = nimi;
            Koostisosad = new List<string>();
        }

        // Lisa koostisosa
        public void Lisa(string koostisosa)
        {
            Koostisosad.Add(koostisosa);
        }

        // Eemalda koostisosa (.Remove)
        public void Eemalda(string koostisosa)
        {
            if (Koostisosad.Contains(koostisosa))
            {
                Koostisosad.Remove(koostisosa);
                Console.WriteLine("\"" + koostisosa + "\" eemaldatud.");
            }
            else
            {
                Console.WriteLine("\"" + koostisosa + "\" ei ole retseptis.");
            }
        }

        // Kontrolli olemasolu (.Contains) – ülesanne 4
        public bool OnOlemas(string koostisosa)
        {
            return Koostisosad.Contains(koostisosa);
        }

        // Kuva list (foreach)
        public void Kuva()
        {
            Console.WriteLine("Retsept: " + Nimi);
            foreach (string k in Koostisosad)
            {
                Console.WriteLine("  - " + k);
            }
        }

        // Lae koostisosad failist (File.ReadAllLines) – ülesanne 3
        public void LaeFailed(string failinimi)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, failinimi);
            Koostisosad.Clear();
            try
            {
                foreach (string rida in File.ReadAllLines(path))
                {
                    Koostisosad.Add(rida);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Viga faili lugemisel!");
            }
        }

        // Salvesta koostisosad faili (File.WriteAllLines) – ülesanne 5
        public void Salvesta(string failinimi)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, failinimi);
            try
            {
                File.WriteAllLines(path, Koostisosad);
                Console.WriteLine("Uus retsept on edukalt faili salvestatud!");
            }
            catch (Exception)
            {
                Console.WriteLine("Viga salvestamisel!");
            }
        }
    }
}
