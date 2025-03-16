//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Timers;
//using System.IO;
//using Timer = System.Timers.Timer;
//using Fränder.Filhant;

//namespace Fränder.Game
//{
    
//    class Graveyard
//    {
//        private List<string> franderList;
//        private Timer saveTimer;
//        public Graveyard()
//        {
//            franderList = new List<string>();
//            LoadFromFile();

//            saveTimer = new Timer(60000); // 60000 ms = 1 minut
//            saveTimer.Elapsed += (sender, e) => SaveToFile();
//            saveTimer.Start();
//        }

//        public void AddFrand(string name, DateTime birthDate, DateTime? deathDate = null)
//        {
//            string deathInfo = deathDate.HasValue ? $"Död: {deathDate.Value:yyyy-MM-dd}" : "Lever";
//            string entry = $"{name} - Född: {birthDate:yyyy-MM-dd} - {deathInfo}";
//            franderList.Add(entry);

//            // Spara listan varje gång en ny Fränd läggs till
//            FileHelper.SaveListToFileAsync(franderList);
//        }

//        public List<string> GetAllFrander()
//        {
//            return franderList;
//        }

//        private void LoadFromFile()
//        {
//            var loadedList = FileHelper.ReadListFromFileAsync().Result;
//            if (loadedList != null && loadedList.Any())
//            {
//                franderList = loadedList;
//            }
//        }
//        private void SaveToFile()
//        {
//            FileHelper.SaveListToFileAsync(franderList);
//        }
//    }
//}
