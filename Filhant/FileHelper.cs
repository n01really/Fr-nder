using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fränder.Filhant
{
    class FileHelper
    {
        private static readonly string GameDirectory = Path.Combine(FileSystem.AppDataDirectory, "GameFiles");
        private static readonly string GraveyardFilePath = Path.Combine(GameDirectory, "graveyard.txt");

        // Skapa mappen om den inte redan finns
        public static void CreateGameDirectory()
        {
            try
            {
                if (!Directory.Exists(GameDirectory))
                {
                    Directory.CreateDirectory(GameDirectory);
                    Console.WriteLine("Spelfiler mapp skapad!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid skapande av mapp: {ex.Message}");
            }
        }

        // Spara en lista av strängar till filen (t.ex. Graveyard-lista)
        public static async Task SaveListToFileAsync(List<string> dataList)
        {
            try
            {
                // Se till att mappen finns
                CreateGameDirectory();

                // Skriv till fil
                await File.WriteAllLinesAsync(GraveyardFilePath, dataList);
                Console.WriteLine("Data sparad i graveyard.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid att spara fil: {ex.Message}");
            }
        }

        // Läs data från filen
        public static async Task<List<string>> ReadListFromFileAsync()
        {
            try
            {
                if (File.Exists(GraveyardFilePath))
                {
                    var lines = await File.ReadAllLinesAsync(GraveyardFilePath);
                    return new List<string>(lines);
                }
                else
                {
                    Console.WriteLine("Filen existerar inte.");
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid läsning från fil: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
