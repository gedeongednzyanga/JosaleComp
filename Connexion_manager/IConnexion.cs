using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Connexion_manager
{
    internal interface IConnexion
    {
        IDbConnection Initialize(Connexion connexion, Sgbd sgbd);
    }
}
