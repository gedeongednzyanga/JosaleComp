using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connexion_manager
{
    public class Connexion
    {
        public Connexion(){ }

        private string _server;
        private string _database;
        private string _user;
        private string _password;
        private int _port = 0;

        public string Server
        {
            get { return _server; }
            set { if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Please specify a server");
                else
                    _server = value;
            }
        }

        public string Database
        {
            get { return _database; }
            set { if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Please specify a database...");
                else
                    _database = value;
            }
        }

        public string User
        {
            get { return _user; }
            set { if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Invalid username...");
                else
                    _user = value;
            }
        }

        public string Password
        {
            get { return _password; }
            set { if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Incorrect password...");
                else _password = value;
            }
        }

        public int Port
        {
            get { return _port; }
            set { if (value <= 0)
                    throw new InvalidOperationException("Invalid port number...");
                else
                    _port = value;
            }
        }
    }
}
