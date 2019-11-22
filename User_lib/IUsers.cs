using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_lib
{
    public interface IUsers
    {
        int Id { get; set; }
        string Nom_user { get; set; }
        string User_name { get; set; }
        string Pass_user { get; set; }
        int Niveau { get; set; }

        void Save(IUsers user);
        int Test_user(string username, string pass);
    }
}
