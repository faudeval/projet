#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
#endregion

namespace TestInterface
{
    /// <summary>
    /// Classe devant à terme afficher un Form permettant d'envoyer nos scores en passant en paramètres
    /// le login et le mot de passe du joueur.
    /// </summary>
    public partial class LoginForm : Form
    {
        #region Initialize
        /// <summary>
        /// Constructeur
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            linkLabel1.Links.Remove(linkLabel1.Links[0]);
            linkLabel1.Links.Add(0, linkLabel1.Text.Length, "http://g2iut.alwaysdata.net/inscription.php");
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Clic sur le bouton "Annuler"
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Clic sur le lien "Pas encore de compte ?"
        /// </summary>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(sInfo);
        }

        /// <summary>
        /// Clic sur le bouton "Envoyer"
        /// </summary>
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (connexion())
            {
                // Afficher la liste des jeux avec un nouveau score, avec une checkbox à côté
            }
        }
        #endregion
    }
}
