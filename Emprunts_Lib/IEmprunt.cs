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
        int Reftier { get; set; }
        DateTime DateEmprunt { get; set; }
        DateTime DateRembu { get; set; }
        string Tier { get; set; }

        int Nouveau();
        void Save(IEmprunt emprunt);
        void Delete(int id);
        List<IEmprunt> AllEmprunt();
        IEmprunt OneEmprunt(int id);
    }
}
