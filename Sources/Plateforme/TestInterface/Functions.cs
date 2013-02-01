using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace TestInterface
{
    partial class LoginForm
    {

        public bool UserExists(string pseudo, string pass)
        {
            DataSet res = BDD.query("SELECT id FROM joueur WHERE pseudo='" + pseudo + "' AND pass=SHA1('" + pass + "')", "pseudo");

            if (res != null && res.Tables[0].Rows.Count == 1)
            {
                int id = (int)res.Tables[0].Rows[0]["id"];
                //res = BDD.query("INSERT INTO joueur ('pseudo', 'pass', 'mail') VALUES ('trolol', SHA1('trolol'), 'trololo@trolol.lol')");
                return true;
            }
            else
                return false;
        }

        public bool connexion()
        {
            if (UserExists(txtLogin.Text, txtPass.Text))
                return true;
            else
            {
                MessageBox.Show(this, "Mauvais couple login/mot de passe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
