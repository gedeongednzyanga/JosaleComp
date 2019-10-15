using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emprunts_Lib
{
    public interface IEmprunt

    {
        int Id { get; set; }
        float Montant { get; set; }
        float MontantRemb { get; set; }
        float Reste { get; set; }
        int Reftier { get; set; }
        DateTime DateEmprunt { get; set; }
        DateTime DateRembu { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string SurName { get; set; }
        string Tier { get; set; }

        int Nouveau();
        void Save(IEmprunt emprunt);
        void Delete(int id);
        List<IEmprunt> AllEmprunt();
        List<IEmprunt> Get_TierNomId();
        List<IEmprunt> AllEmprunt_Remb();
        IEmprunt OneEmprunt(int id);
    }
}
