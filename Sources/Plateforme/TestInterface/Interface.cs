using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net;


namespace TestInterface
{
    public partial class Interface : Form
    {
        List<Game> localGames;
        List<Game> remoteGames;
        String executableDirectory;
        ToolStripMenuItem[] tab;

        public Interface()
        {
            InitializeComponent();
            tab = new ToolStripMenuItem[] { miniaturesToolStripMenuItem, mosaïquesToolStripMenuItem, icônesToolStripMenuItem, listeToolStripMenuItem, détailsToolStripMenuItem };
            executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            loadGames();
        }

        private void envoyerLesScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog(this);
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void loadGames()
        {
            try
            {
                localGames = XMLReader.ReadXML(executableDirectory + "/Jeux.xml");
                int i = 0;
                smallIconList.Images.Clear();
                mediumIconList.Images.Clear();
                listView1.Items.Clear();
                foreach (Game g in localGames)
                {
                    ListViewItem lvi = new ListViewItem(g.Name, i);
                    lvi.Tag = g;
                    Bitmap b = new Bitmap(executableDirectory.Substring(6) + @"\" + g.Icon);
                    smallIconList.Images.Add(b);
                    mediumIconList.Images.Add(b);
                    lvi.SubItems.Add(g.IState());
                    lvi.SubItems.Add(g.Description);
                    listView1.Items.Add(lvi);
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Il manque un fichier de configuration : Jeux.xml", "Impossible de lancer le jeu", MessageBoxButtons.OK);
            }
        }
        
        private void LaunchGame(Game g)
        {
            if (Game.IsPlayable(g))
            {
                System.Diagnostics.ProcessStartInfo myInfo = new System.Diagnostics.ProcessStartInfo();
                myInfo.FileName = g.BaseName;
                myInfo.WorkingDirectory = "games\\" + g.PathName;
                System.Diagnostics.Process.Start(myInfo);
            }
            else if (g.Install == InstallState.Installing)
            {
                DialogResult reload = MessageBox.Show("Veuillez patienter, le téléchargement est en cours...\nVoulez-vous annuler le téléchargement en cours et le relancer ?", "Téléchargement en cours...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (reload == DialogResult.Yes)
                {
                    // Annuler le téléchargement et le recommencer
                }
            }
            else
            {
                DialogResult install = MessageBox.Show("Ce jeu n'est pas installé. Voulez-vous le télecharger et l'installer ?", "Jeu non-installé", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (install == DialogResult.Yes)
                {
                    // Télécharger le jeu et modifier le fichier XML
                }
            }

        }
        
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            LaunchGame((Game)listView1.SelectedItems[0].Tag);
        }

        private void miniaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
            chercheItem((ToolStripMenuItem)sender);
        }

        private void mosaïquesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
            chercheItem((ToolStripMenuItem)sender);
        }

        private void icônesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            chercheItem((ToolStripMenuItem)sender);
        }

        private void listeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
            chercheItem((ToolStripMenuItem)sender);
        }

        private void détailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            chercheItem((ToolStripMenuItem)sender);
        }

        private void chercheItem(ToolStripMenuItem item)
        {
            int i = 0;
            while (i < 5)
            {
                if (tab[i].Checked)
                    tab[i].Checked = false;
                if (tab[i] == item)
                    tab[i].Checked = true;
                i++;
            }
        }

        private void téléchargerDesJeuxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = BDD.query("SELECT nom, chemin, version, description, icone FROM jeux", "nom", "chemin", "version", "description", "icone");
            if (ds == null)
            {
                MessageBox.Show("Impossible de télécharger la liste de jeux", "Erreur lors du téléchargement", MessageBoxButtons.OK);
            }
            else
            {
                remoteGames = new List<Game>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Téléchargement de l'icône
                    WebClient client = new WebClient();
                    client.DownloadFile(new Uri("http://g2iut.alwaysdata.net/icons/"+dr["icone"]), executableDirectory.Substring(6) + @"\remoteIcons\" + dr["icone"]);
                    remoteGames.Add(new Game((string)dr["nom"], (string)dr["icone"], (string)dr["chemin"], (string)dr["description"], (int)dr["version"], InstallState.NotInstalled));
                }
                foreach (Game rg in remoteGames)
                {
                    int j = 0;
                    bool installed = false;
                    while (j<localGames.Count && !installed)
                    {
                        if (rg.Executable == localGames[j].Executable)
                        {
                            int i = 0;
                            while (i < listView1.Items.Count && (Game)listView1.Items[i].Tag != localGames[j])
                            {
                                i++;
                            }
                            if (i < listView1.Items.Count)
                            {
                                if (rg.Equals(localGames[j]))
                                {
                                    listView1.Items[i].SubItems[1].Text = "Installé";
                                    localGames[j].Install = InstallState.Installed;
                                }
                                else
                                {
                                    listView1.Items[i].SubItems[1].Text = "Mise à jour disponible";
                                    localGames[j].Install = InstallState.UpdateAvailable;
                                }
                            }
                        }
                        j++;
                    }
                }
            }
        }
    }
}
