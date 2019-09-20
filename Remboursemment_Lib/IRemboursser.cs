using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remboursemment_Lib
{
    public interface IRemboursser
    {
        int Id { get; set; }
        float Montant { get; set; }
        int RefCli { get; set; }
        string Nom { get; set; }
        string Postnom { get; set; }
        string Prenom { get; set; }
        DateTime DateRembour { get; set; }
        int Nouveau();
        void Save(IRemboursser rembour);
        void Delete(int id);
        List<IRemboursser> AllRembour();
        IRemboursser OneRembour(int id);
    }
}
