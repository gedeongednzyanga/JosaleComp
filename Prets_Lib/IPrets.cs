using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prets_Lib
{
    public interface IPrets
    {
        int Id { get; set; }
        float Montant { get; set; }
        int RefCli { get; set; }
        int RefInteret { get; set;}
        DateTime DateRembour { get; set; }

        string Nom { get; set; }
        string Postnom { get; set; }
        string Prenom { get; set; }
        float Interet { get; set; }
        float Montantpaye { get; set; }
        DateTime DatePret { get; set; }

        int Nouveau();
        void Save(IPrets prets);
        void Delete(int id);
        List<IPrets> Allcredit();
        List<IPrets> Allcredit_Tri(int annee);
        List<IPrets> Allcredit_Tri_moi(int annee, string mois);
        List<IPrets> AllRembou();
        IPrets OneCredit(int id);
    }
}
