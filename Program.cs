using System.Globalization;
using System.Runtime.Serialization;

namespace kalkulacka_bednicek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            double[] rozpocet = new double[2];
            rozpocet[0] = 0;
            rozpocet[1] = 0;
            List<Item> bedna = new List<Item>();
        
            Truhla nova_truhla = new Truhla();
            int kontrola = 0;
            int kontrola2 = 1;
            int kontrola3 = 1;
            int kontrola4 = 0;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("*-------------------*");
                Console.WriteLine("|KALKULAČKA BEDNIČEK|");
                Console.WriteLine("*-------------------*");
                Console.WriteLine();
                Console.WriteLine("___---___---___---___---___---___---");
                Console.WriteLine("1. Hlavní menu");
                Console.WriteLine("---___---___---___---___---___---___");
                double input3 = Over();
                switch(input3)
                {
                    case 1:
                        kontrola3--;
                        break;
                }
                Console.Clear();
                 
                while (kontrola3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("-----------");
                    Console.WriteLine("HLAVNÍ MENU");
                    Console.WriteLine("-----------");
                    Console.WriteLine();
                    Console.WriteLine("___---___---___---___---___---___---");
                    Console.WriteLine("1. Načtení nové bedny");
                    Console.WriteLine("2. Menu bedny");
                    Console.WriteLine("3. Úvodní Nabídka");
                    Console.WriteLine("---___---___---___---___---___---___");
                    double input2 = Over();
                    switch (input2)
                    {
                        case 1:
                            if (bedna.Count != 0)
                            {
                                bedna.Clear();
                            }
                            Console.Clear();
                            Console.WriteLine("Zadej název bedny (stejný jako název souboru se skiny dané bedny)");
                            string nazev_truhly = Console.ReadLine();
                            Console.WriteLine("Zadej cenu bedny");
                            nova_truhla.Cena_Bedny = Over();
                            nova_truhla.Nazev = nazev_truhly;

                            using (StreamReader sr = new StreamReader($"{nova_truhla.Nazev}.csv"))
                            {
                                string radek = sr.ReadLine();
                                while ((radek = sr.ReadLine()) != null)
                                {

                                    string[] kategorie = radek.Replace("\"", "").Split(",");
                                    Item new_item = new Item();
                                    new_item.Name = kategorie[0];
                                    new_item.Cost = double.Parse(kategorie[1].Replace("\"", ""), System.Globalization.CultureInfo.InvariantCulture);
                                    new_item.Chance = double.Parse(kategorie[2].Replace("\"", ""), System.Globalization.CultureInfo.InvariantCulture);
                                    new_item.Color = kategorie[3];
                                    bedna.Add(new_item);


                                }

                            }

                            kontrola4++;
                            break;
                        case 2:
                            if(kontrola4 != 0)
                            {
                                kontrola2--;
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Jako první musíš načíst bednu");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                                break;
                        case 3:
                            kontrola3++;
                            Console.Clear();
                            break;
                    }
                    if (kontrola4 == 1)
                    {
                        Console.Clear();
                    }
               

                    while (kontrola2 == 0)
                    {
                        
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("----------");
                        Console.WriteLine("MENU BEDNY");
                        Console.WriteLine("----------");
                        Console.WriteLine();
                        Console.WriteLine("___---___---___---___---___---___---");
                        Console.WriteLine("1. Obsah bedny");
                        Console.WriteLine("2. Šance z jedné bedny");
                        Console.WriteLine("3. Simulace otevření X bedniček");
                        Console.WriteLine("4. (reset balance)");
                        Console.WriteLine("5. Uložit informace o bedně");
                        Console.WriteLine("6. Hlavní menu");
                        Console.WriteLine("---___---___---___---___---___---___");
                        Console.WriteLine();
                        Console.WriteLine($"|----------------------|");
                        Console.WriteLine($" Útrata: {rozpocet[0]}$");
                        Console.WriteLine($" Výdělek: {rozpocet[1]}$");
                        Console.WriteLine($"|----------------------|");
                        double input = Over();
                        Console.Clear();
                        if (kontrola == 0)
                        {
                            nova_truhla = Profit(bedna, nova_truhla);
                        }
                        switch (input)
                        {

                            case 1:
                                Console.Clear();
                                Barvicky(bedna);
                                Console.WriteLine(); Console.WriteLine();
                                break;
                            case 2:

                                Console.WriteLine($"Cena: {nova_truhla.Cena_Bedny}$");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Profit: {nova_truhla.Profit_Chance}%");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Lose: {nova_truhla.Lose_Chance}%");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine($"Min. Dvojnásobný Profit: {nova_truhla.Profit2_Chance}%");
                                Console.WriteLine($"Udržení: {nova_truhla.Mid_Chance}%");
                                Console.WriteLine(); Console.WriteLine();
                                
                                break;
                            case 3:
                                Console.Clear();
                                rozpocet = Simulace_Otevirani(bedna, rozpocet, nova_truhla);

                                break;
                            case 4:
                                rozpocet[0] = 0;
                                rozpocet[1] = 0;
                                break;
                            case 5:
                                using (StreamWriter sw = new StreamWriter($"{nova_truhla.Nazev}_info.csv"))
                                {
                                    sw.WriteLine("Cena,Profit,Lose,Dvoj,Mid");
                                    sw.WriteLine($"{nova_truhla.Cena_Bedny.ToString(CultureInfo.InvariantCulture)},{nova_truhla.Profit_Chance.ToString(CultureInfo.InvariantCulture)},{nova_truhla.Lose_Chance.ToString(CultureInfo.InvariantCulture)},{nova_truhla.Profit2_Chance.ToString(CultureInfo.InvariantCulture)},{nova_truhla.Mid_Chance.ToString(CultureInfo.InvariantCulture)}");
                                }
                                break;
                            case 6:
                                kontrola2++;
                                break;

                        }
                        kontrola++;

                    } //menu jedné bedny
                }
            }
        }
        static Truhla Profit(List<Item> bednaa, Truhla truhla)
        {
            int kontrola = 0;
           
            
                for (int i = 0; i < bednaa.Count; i++)
                {
                    if (bednaa[i].Cost >= truhla.Cena_Bedny)
                    {
                        truhla.Profit_Chance += bednaa[i].Chance;
                    }
                    if (bednaa[i].Cost < truhla.Cena_Bedny)
                    {
                        truhla.Lose_Chance += bednaa[i].Chance;
                    }
                    if (bednaa[i].Cost > truhla.Cena_Bedny * 2)
                    {
                        truhla.Profit2_Chance += bednaa[i].Chance;
                    }
                    if (bednaa[i].Cost > truhla.Cena_Bedny * 0.8 && bednaa[i].Cost < truhla.Cena_Bedny)
                    {
                        truhla.Mid_Chance += bednaa[i].Chance;
                    }
                }
            

            return truhla;

        }
        static string Otevreni(List<Item> bednaa)
        {
           
            Random rnd = new Random();
            double total = 0;
            double roll = 0;
            
            total = bednaa.Sum(i => i.Chance);
            roll = rnd.NextDouble() * total;
            foreach (var item in bednaa)
            {
                if (roll < item.Chance)
                {
                    return item.Name;
                }
                   
                roll -= item.Chance;
            }
            return null;
            
        }
        static double[] Simulace_Otevirani(List<Item> bednaa,double[] rozpocett, Truhla truhla)
        {
            
            Console.WriteLine("Kolik beden chceš otevřít");
            double input = Over();
            Item nejdrazsi = new Item();
            rozpocett[0] += truhla.Cena_Bedny * input;
            List<Item> gamba = new List<Item>();
            for (int i = 0; i < input; i++)
            {
               
                gamba.Add(new Item());
                gamba[i].Name = Otevreni(bednaa);           
                
                for (int y = 0; y < bednaa.Count; y++)
                {
                    if (gamba[i].Name == bednaa[y].Name && (bednaa[y].Color == "Grey" || bednaa[y].Color == "Gray"))
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(gamba[i].Name);
                        gamba[i].Cost = bednaa[y].Cost;


                    }
                    if (gamba[i].Name == bednaa[y].Name && (bednaa[y].Color == "Dark_Grey" || bednaa[y].Color == "Dark_Gray"))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(gamba[i].Name);
                        gamba[i].Cost = bednaa[y].Cost;
                    }
                    if (gamba[i].Name == bednaa[y].Name && bednaa[y].Color == "Blue")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(gamba[i].Name);
                        gamba[i].Cost = bednaa[y].Cost;
                    }
                    if (gamba[i].Name == bednaa[y].Name && bednaa[y].Color == "Purple")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine(gamba[i].Name);
                        gamba[i].Cost = bednaa[y].Cost;
                    }
                    if (gamba[i].Name == bednaa[y].Name && bednaa[y].Color == "Pink")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(gamba[i].Name);
                        gamba[i].Cost = bednaa[y].Cost;
                    }
                    if (gamba[i].Name == bednaa[y].Name && bednaa[y].Color == "Red")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(gamba[i].Name);
                        gamba[i].Cost = bednaa[y].Cost;
                    }
                    if (gamba[i].Name == bednaa[y].Name && bednaa[y].Color == "Gold")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(gamba[i].Name);
                        gamba[i].Cost = bednaa[y].Cost;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    if (gamba[i].Cost == bednaa[y].Cost)
                    {
                        rozpocett[1] += gamba[i].Cost;
                        if (nejdrazsi.Cost < gamba[i].Cost)
                        {
                            nejdrazsi.Name = gamba[i].Name;
                            nejdrazsi.Cost = gamba[i].Cost;
                               
                        }
                    }
                }
               
                
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Nejdražší: {nejdrazsi.Name}({nejdrazsi.Cost}$)");
            Console.ForegroundColor = ConsoleColor.White;

            return rozpocett;
        }
        static double Over()
        {
            double cislo = 0;
            while(!double.TryParse(Console.ReadLine(), out cislo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Neplatne");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return cislo;
        }
        static void Barvicky(List<Item> bednaa)
        {
            for (int i = 0; i < bednaa.Count; i++)
            {
                if (bednaa[i].Color == "Grey" || bednaa[i].Color == "Gray")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{bednaa[i].Name} ({bednaa[i].Chance}% | {bednaa[i].Cost}$)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (bednaa[i].Color == "Blue")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{bednaa[i].Name} ({bednaa[i].Chance}% | {bednaa[i].Cost}$)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (bednaa[i].Color == "Dark_Grey" || bednaa[i].Color == "Dark_Gray")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{bednaa[i].Name} ({bednaa[i].Chance}% | {bednaa[i].Cost}$)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (bednaa[i].Color == "Pink")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{bednaa[i].Name} ({bednaa[i].Chance}% | {bednaa[i].Cost}$)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (bednaa[i].Color == "Purple")
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"{bednaa[i].Name} ({bednaa[i].Chance}% | {bednaa[i].Cost}$)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (bednaa[i].Color == "Red")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{bednaa[i].Name} ({bednaa[i].Chance}% | {bednaa[i].Cost}$)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (bednaa[i].Color == "Gold")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{bednaa[i].Name} ({bednaa[i].Chance}% | {bednaa[i].Cost}$)");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
        }

      

    }
}
