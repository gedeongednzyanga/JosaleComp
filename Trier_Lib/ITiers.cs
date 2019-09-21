using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trier_Lib
{
    public interface ITiers
    {
        int Id { get; set; }
        string Nom { get; set; }
        string Postnom { get; set; }
        string Prenom { get; set; }
        string Contact { get; set; }
        string Mail { get; set; }
        string Addresse { get; set; }
        int Nouveau();
        void Save(ITiers tiers);
        void Delete(int id);
        List<ITiers> AllTiers();
        ITiers OneTier(int id);
    }
}
