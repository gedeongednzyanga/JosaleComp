using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Lib
{
    public interface IClient
    {
        int Id { get; set; }
        string Nom { get; set; }
        string Postnom { get; set; }
        string Prenom { get; set; }
        string Contact { get; set; }
        string Mail { get; set; }
        string Addresse { get; set; }
        int Nouveau();
        void Save(IClient client);
        void Delete(int id);
        List<IClient> AllClient();
        IClient OneClient(int id);
    }
}
