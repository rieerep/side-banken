using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sidoBanken
{
    internal class Users
    {
        private string _userName;
        private string _passWord;

        public Users(string username, string password)
        {
            _userName = username;
            _passWord = password;
        }
        public string Username 
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string passWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }
    }
}
