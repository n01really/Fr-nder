using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fränder.Models
{
    class GraveYard
    {



        public class FrandRecord
        {
            public string Name { get; set; }
            public DateTime Created { get; set; }
            public DateTime? Died { get; set; } 
        }

        private static List<FrandRecord> frandList = new();

        //Lägger till en ny Fränd i listan
        public static void AddFrand(string name)
        {
            frandList.Add(new FrandRecord
            {
                Name = name,
                Created = DateTime.Now,
                Died = null
            });
        }

        //public static void MarkAsDead(string name)
        //{
        //    var frand = frandList.Find(f => f.Name == name && f.Died == null);
        //    if (frand != null)
        //    {
        //        frand.Died = DateTime.Now;
        //    }
        //}

        //Hämtar alla Fränder från listan
        public static List<FrandRecord> GetAllFrands()
        {
            return frandList;
        }
    }

}
