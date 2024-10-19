using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace _3.Beadando
{
    internal class Beolvaso
    {
        public static TeniszKlub Olvas(string filename)
        {
            var teniszklub = new TeniszKlub();
            Olvas(filename, teniszklub);
            return teniszklub;
        }

        private static void Olvas(string filename, TeniszKlub teniszklub)
        {
            try
            {
                // Kulturális információk
                CultureInfo russianCulture = CultureInfo.GetCultureInfo("ru-RU");
                CultureInfo japaneseCulture = CultureInfo.GetCultureInfo("ja-JP");
                CultureInfo chineseCulture = CultureInfo.GetCultureInfo("zh-CN");

                using (var reader = new StreamReader(filename))
                {
                    string line;
                    bool readingPalyak = false;
                    bool readingTagok = false;
                    bool readingFoglalasok = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "Pályák:")
                        {
                            readingPalyak = true;
                            readingTagok = false;
                            readingFoglalasok = false;
                            continue;
                        }
                        else if (line == "Tagok:")
                        {
                            readingPalyak = false;
                            readingTagok = true;
                            readingFoglalasok = false;
                            continue;
                        }
                        else if (line == "Foglalások:")
                        {
                            readingPalyak = false;
                            readingTagok = false;
                            readingFoglalasok = true;
                            continue;
                        }

                        if (readingPalyak)
                        {
                            var fields = line.Split(',');
                            int palyaSzam = int.Parse(fields[0]);
                            Enum.TryParse(fields[1], out PalyaTipus tipus);
                            bool fedett = fields[2] == "fedett";

                            teniszklub.UjPaja(palyaSzam, tipus, fedett);
                        }
                        else if (readingTagok)
                        {
                            // Orosz nevek olvasása
                            teniszklub.UjTag(line);
                        }
                        else if (readingFoglalasok)
                        {
                            var fields = line.Split(',');
                            string tagNev = fields[0];
                            int palyaSzam = int.Parse(fields[1]);

                            // Japán dátumok olvasása
                            DateTime datum = DateTime.Parse(fields[2], japaneseCulture);
                            int ora = int.Parse(fields[3]);

                            teniszklub.UjFoglalás(tagNev, palyaSzam, datum, ora);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Hiba: a fájl nem található. {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Hiba az olvasás során: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Hiba: hibás formátum. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
    }
}
