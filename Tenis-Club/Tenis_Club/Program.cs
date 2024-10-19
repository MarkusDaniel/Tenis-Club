using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Beadando
{
    internal class Program
    {
        static TeniszKlub teniszklub = new TeniszKlub();
        static void Main(string[] args)
        {
            bool programfut = true;
            while (programfut)
            {
                Console.WriteLine();
                Console.WriteLine("|=================================|");
                Console.WriteLine("|               Menü              |");
                Console.WriteLine("|                                 |");
                Console.WriteLine("|=================================|");
                Console.WriteLine("|            1. Új Pálya          |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|       2. Pálya felszámolása     |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|            3. Új Tag            |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|           4. Tag kilép          |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|            5. Foglalás          |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|       6. Foglalás lemondása     |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|       7. Pályák lekérdezése     |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|       8. Tagok lekérdezése      |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|     9. Foglalások lekérdezése   |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|         10. Foglalás ára        |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|        11. Napi összbevétel     |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|         12. Fájl beolvasás      |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine("|           13. Kilépés           |");
                Console.WriteLine("|_________________________________|");
                Console.WriteLine();
                Console.WriteLine("Válasz egy menü-pontot!");

                int menupont;
                if (int.TryParse(Console.ReadLine(), out menupont))
                {
                    switch (menupont)
                    {
                        case 1:
                            PalyaLetrehozas();
                            break;

                        case 2:
                            PalyaFelszamolasa();
                            break;

                        case 3:
                            UjTag();
                            break;

                        case 4:
                            TagKilep();
                            break;

                        case 5:
                            Foglalas();
                            break;

                        case 6:
                            FoglalasLemond();
                            break;

                        case 7:
                            Pályáklekerdezese();
                            break;

                        case 8:
                            OsszesTag();
                            break;

                        case 9:
                            Foglalaslekerd();
                            break;

                        case 10:
                            TagFoglalasAra();
                            break;

                        case 11:
                            NapiOsszbevetel();
                            break;

                        case 12:
                            BeolvasAdatokat();
                            break;

                        case 13:
                            programfut = false;
                            break;

                        default:
                            Console.WriteLine("Nincs ilyen menüpont.");
                            Console.WriteLine("Válassz egy létező menüpontot!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Nincs ilyen menüpont.");
                    Console.WriteLine("Válassz egy létező menüpontot!");
                }
                Console.WriteLine();
                Console.ReadKey();
                
            }
        }
        //létrehozzuk a pályát mauálisan
        static void PalyaLetrehozas()
        {
            Console.WriteLine("Adjon egy számot a Pályának!");
            int palyaSzam = int.Parse(Console.ReadLine());

            Console.WriteLine("Add meg a pálya típusát!");
            foreach (var tipus in Enum.GetValues(typeof(PalyaTipus))) 
            {
                Console.WriteLine($"{(int)tipus}. {tipus}");
            }
            Console.WriteLine("Az alábbi tipusok közül választhatsz:");
            int palyaTipus = int.Parse(Console.ReadLine());

            Console.WriteLine("Legyen fedett a pálya? (fedett/nyitott): ");
            bool fedett = Console.ReadLine().ToLower() == "fedett";

            teniszklub.UjPaja(palyaSzam, (PalyaTipus)palyaTipus, fedett);
            Console.WriteLine("A pályát sikeresen létrehoztad.");
        }

        //felszámoljuk a már létező pályát
        static void PalyaFelszamolasa()
        {
            Console.WriteLine("Ad meg a felszálolandó pálya számát!");
            int palyaSzam = int.Parse(Console.ReadLine());
            if (teniszklub.OsszesPalya().ContainsKey(palyaSzam))
            {
                teniszklub.PalyaFelszamolasa(palyaSzam);
                Console.WriteLine("A pályát felszámoltad.");
            }
            else
            {
                Console.WriteLine("Nincs ilyen számú pálya.");
                Console.WriteLine("Adj meg egy létező pályaszámot.");
            }
        }

        // Pályák lekérdezése
        static void Pályáklekerdezese()
        {
            var palyak = teniszklub.OsszesPalya();
            if(palyak.Count == 0)
            {
                Console.WriteLine("Jelenleg egy pálya sincs létrehozva");
            }else
            {
                foreach (var palya in palyak)
                {
                    Console.WriteLine($"Pálya száma: {palya.Key}, Pálya típusa: {palya.Value.Item1}, Fedettség: {(palya.Value.Item2 ? "fedett" : "nyitott")} ");
                }
            }
        }

        //Új tag lép be a klubba
        static void UjTag()
        {
            Console.WriteLine("Adja meg a tag nevét!");
            string tagNev = Console.ReadLine();

            teniszklub.UjTag(tagNev);
            Console.WriteLine("Sikeres jelentkezés.");
            Console.WriteLine("A személy mostmár tagja a klubnak.");
        }
        // Tag kilép a klubból
        static void TagKilep()
        {
            Console.WriteLine("Adja meg a tag nevét akit el akar távolítani!");
            string tagNev = Console.ReadLine();

            teniszklub.TagKilep(tagNev);
            Console.WriteLine("Sikeres kilépés.");
            Console.WriteLine("A személy mostmár nem tagja a klubnak.");
        }
        //Klubtagok lekérdezése
        static void OsszesTag()
        {
            if(teniszklub.OsszesTag().Count == 0)
            {
                Console.WriteLine("Még nem lépett be senki a klubba.");
            }
            else
            {
                Console.WriteLine("A klub tagjai: ");
                foreach (var tag in teniszklub.OsszesTag())
                {
                    Console.WriteLine(tag);
                }
            }
        }

        //Foglalás Készítése
        static void Foglalas()
        {
            Console.WriteLine("Add meg a tag nevét!");
            string tagNev = Console.ReadLine();

            Console.WriteLine("Add meg a pálya számát!");
            int palyaSzam = int.Parse(Console.ReadLine());

            Console.WriteLine("Add meg a fogalás dátumát !");
            Console.WriteLine("Dátum formátuma:");
            Console.WriteLine("(EEEEE-HH-NN)");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Add meg a foglalás időpontját!");
            Console.WriteLine("6-20 óra között lehet időpontot foglalni");
            int ora = int.Parse(Console.ReadLine());

            teniszklub.UjFoglalás(tagNev, palyaSzam, datum, ora);
            Console.WriteLine("A foglalás sikeres volt.");
        }

        //A tag lemondja a foglalást
        static void FoglalasLemond()
        {
            Console.WriteLine("Add meg a tag nevét!");
            string tagNev = Console.ReadLine();

            Console.WriteLine("Add meg a legoglaltpálya számát!");
            int palyaSzam = int.Parse(Console.ReadLine());

            Console.WriteLine("Add meg a lemondani kívánt fogalás dátumát !");
            Console.WriteLine("Dátum formátuma:");
            Console.WriteLine("(EEEEE-HH-NN)");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Add meg a lemondani kívánt foglalás időpontját!");
            Console.WriteLine("6-20 óra között lehet időpontot foglalni");
            int ora = int.Parse(Console.ReadLine());

            teniszklub.FoglalasLemondas(tagNev, palyaSzam, datum, ora);
            Console.WriteLine("A foglalás sikeresen lemondva.");
        }

        //A foglalások lekérdezése adott pályára
        static void Foglalaslekerd()
        {
            Console.WriteLine("Add meg a pálya számát!");
            int palyaSzam = int.Parse(Console.ReadLine());

            Console.WriteLine("Add meg a dátumot!");
            Console.WriteLine("Dátum formátuma:");
            Console.WriteLine("(ÉÉÉÉ-HH-NN)");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            List<Foglalas> foglalasok = teniszklub.OsszesFoglalas(datum, palyaSzam);
            foreach (var foglalas in foglalasok)
            {
                Console.WriteLine($"Tag: {foglalas.TagNev}, Pálya:{foglalas.PalyaSzam}");
                Console.WriteLine($"Datum: {foglalas.Datum}, Óra:{foglalas.Ora}");
            }
        }
        // Egy tagnak mennyit kell fizetnie a foglalásért
        static void TagFoglalasAra()
        {
            Console.WriteLine("Add meg a tag nevét!");
            string tagNev = Console.ReadLine();

            // Ellenőrizzük, hogy a megadott tag létezik-e
            if (!teniszklub.OsszesTag().Contains(tagNev))
            {
                Console.WriteLine("Nincs ilyen nevű tag a klubban.");
                return;
            }

            Console.WriteLine("Add meg a dátumot!");
            Console.WriteLine("Dátum formátuma:");
            Console.WriteLine("(ÉÉÉÉ-HH-NN)");
            DateTime datum;
            if (!DateTime.TryParse(Console.ReadLine(), out datum))
            {
                Console.WriteLine("Hibás dátumformátum.");
                return;
            }

            Console.WriteLine("Adja meg a pálya számát!");
            int palyaSzam;
            if (!int.TryParse(Console.ReadLine(), out palyaSzam))
            {
                Console.WriteLine("Hibás pályaszám formátum.");
                return;
            }

            // Ellenőrizzük, hogy van-e foglalása a megadott napon a tagnak az adott pályán
            var foglalasok = teniszklub.OsszesFoglalas(datum, palyaSzam);
            var tagFoglalasok = foglalasok.Where(foglalas => foglalas.TagNev == tagNev).ToList();

            if (tagFoglalasok.Count == 0)
            {
                Console.WriteLine("A tagnak nincs foglalása ezen a napon az adott pályán.");
                return;
            }

            double osszeg = 0;

            // Számítsuk ki az összes foglalás árát
            foreach (var foglalas in tagFoglalasok)
            {
                var palyaAdatok = teniszklub.OsszesPalya()[palyaSzam];
                var palyaTipus = palyaAdatok.Item1;
                var fedett = palyaAdatok.Item2;

                double foglalasAr = teniszklub.Kaukcio(palyaTipus, fedett);
                osszeg += foglalasAr;
            }

            Console.WriteLine($"A tagnak összesen {osszeg} Ft-ot kell fizetnie ezen a napon az adott pályán a foglalásokért.");
        }
        static void NapiOsszbevetel()
        {
            Console.WriteLine("Add meg a dátumot!");
            Console.WriteLine("Dátum formátuma:");
            Console.WriteLine("(ÉÉÉÉ-HH-NN)");
            DateTime datum;
            if (!DateTime.TryParse(Console.ReadLine(), out datum))
            {
                Console.WriteLine("Hibás dátumformátum.");
                return;
            }

            double osszbevetel = teniszklub.NapiOsszbevetel(datum);
            Console.WriteLine($"A teniszklub összbevétele ezen a napon: {osszbevetel} Ft.");
        }

        private static void BeolvasAdatokat()
        {
            Console.WriteLine("Add meg a fájl nevét!");
            string filename = Console.ReadLine();

            teniszklub = Beolvaso.Olvas(filename);
            // Ellenőrizhetjük, hogy a beolvasás megtörtént-e
            if (teniszklub != null)
            {
                Console.WriteLine("A beolvasás sikeres volt!");
                // Itt folytathatjuk a további műveleteket a teniszklub objektummal
            }
            else
            {
                Console.WriteLine("Hiba történt a beolvasás során.");
            }
        }

    }
}
