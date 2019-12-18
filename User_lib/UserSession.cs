using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_lib
{
    public class UserSession
    {
        public static UserSession usersession = null;

        private string _UserNom;
        private int _UserNiveau;

        public static UserSession getInstance()
        {
            if (usersession == null)
                usersession = new UserSession();
            return usersession;
        }

        public string UserNom
        {
            get
            {
                return _UserNom;
            }

            set
            {
                _UserNom = value;
            }
        }

        public int UserNiveau
        {
            get
            {
                return _UserNiveau;
            }

            set
            {
                _UserNiveau = value;
            }
        }
    }
}
