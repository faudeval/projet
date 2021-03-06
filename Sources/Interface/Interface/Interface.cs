﻿#region Using Statements
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
using System.IO.Compression;
#endregion
// TODO : Déplacer les nouvelles icônes aux bonnes places lors du téléchargement / de la mise à jour

namespace TestInterface
{
    /// <summary>
    /// Interface
    /// </summary>
    public partial class Interface : Form
    {
        #region Fields
        private static List<Interface> interfaces = new List<Interface>();
        List<Game> localGames;
        List<Game> remoteGames;
        String executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        ToolStripMenuItem[] tab;
        String temporaryPath = System.IO.Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "G2IUT");
        #endregion

        #region Initialize
        /// <summary>
        /// Constructeur
        /// </summary>
        public Interface()
        {
            System.IO.Directory.CreateDirectory(temporaryPath);
            // Disposed += new EventHandler(Interface_Disposed);
            InitializeComponent();
            tab = new ToolStripMenuItem[] { miniaturesToolStripMenuItem, mosaïquesToolStripMenuItem, icônesToolStripMenuItem, listeToolStripMenuItem, détailsToolStripMenuItem };
            localGames = new List<Game>();
            loadGames();
            interfaces.Add(this);
        }
        #endregion

        #region Events Handlers
        /// <summary>
        /// Clic sur l'élement du menu "Envoyer les scores"
        /// </summary>
        private void envoyerLesScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog(this);
        }

        /// <summary>
        /// Clic sur l'élement du menu "Télécharger des jeux"
        /// </summary>
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
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    // Téléchargement de l'icône
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile(new Uri("http://g2iut.alwaysdata.net/icons/" + dr["icone"]), Path.Combine(temporaryPath, (string)dr["icone"]));
                        remoteGames.Add(new Game((string)dr["nom"], (string)dr["icone"], (string)dr["chemin"], (string)dr["description"], (int)dr["version"], InstallState.NotInstalled));
                    }
                }
                catch (Exception sarlon)
                {
                    string msg = "";
#if DEBUG
                    msg = sarlon.Message;
#endif
                    MessageBox.Show("Impossible de contacter le serveur " + msg, "Erreur lors de la récupération", MessageBoxButtons.OK);
                }
                foreach (Game rg in remoteGames)
                {
                    int j = 0;
                    bool installed = false;
                    while (j < localGames.Count && !installed)
                    {
                        if (rg.Executable == localGames[j].Executable)
                        {
                            installed = true;
                            int i = 0;
                            while (i < listView1.Items.Count && (Game)listView1.Items[i].Tag != localGames[j])
                            {
                                i++;
                            }
                            if (i < listView1.Items.Count)
                            {
                                if (rg.Equals(localGames[j]))
                                    localGames[j].Install = InstallState.UpToDate;
                                else
                                    localGames[j].Install = InstallState.UpdateAvailable;
                            }
                        }
                        j++;
                    }
                    if (!installed)
                    {
                        int lviPosition = listView1.Items.Count;
                        ListViewItem lvi = new ListViewItem(rg.Name, lviPosition);
                        lvi.Tag = rg;
                        Bitmap b = new Bitmap(Path.Combine(temporaryPath, rg.Icon));
                        smallIconList.Images.Add(b);
                        mediumIconList.Images.Add(b);
                        lvi.SubItems.Add(rg.IState());
                        lvi.SubItems.Add(rg.Description);
                        listView1.Items.Add(lvi);
                    }
                }
            }
        }

        /// <summary>
        /// Clic sur l'élement du menu "Quitter"
        /// </summary>
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Activation d'un élément de la ListView (Double Clic, appui sur entrée...)
        /// </summary>
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            LaunchGame((Game)listView1.SelectedItems[0].Tag);
        }

        /// <summary>
        /// Clic sur l'élement du menu "Miniatures"
        /// </summary>
        private void miniaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
            chercheItem((ToolStripMenuItem)sender);
        }

        /// <summary>
        /// Clic sur l'élement du menu "Mosaïques"
        /// </summary>
        private void mosaïquesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
            chercheItem((ToolStripMenuItem)sender);
        }

        /// <summary>
        /// Clic sur l'élement du menu "Icônes"
        /// </summary>
        private void icônesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            chercheItem((ToolStripMenuItem)sender);
        }

        /// <summary>
        /// Clic sur l'élement du menu "Liste"
        /// </summary>
        private void listeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
            chercheItem((ToolStripMenuItem)sender);
        }

        /// <summary>
        /// Clic sur l'élement du menu "Détails"
        /// </summary>
        private void détailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            chercheItem((ToolStripMenuItem)sender);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Chargement des jeux présents dans le fichier XML
        /// </summary>
        private void loadGames()
        {
            try
            {
                localGames = XMLReader.ReadXML(Path.Combine(executableDirectory, "Jeux.xml"));
                int i = 0;
                smallIconList.Images.Clear();
                mediumIconList.Images.Clear();
                listView1.Items.Clear();
                foreach (Game g in localGames)
                {
                    ListViewItem lvi = new ListViewItem(g.Name, i);
                    lvi.Tag = g;
                    Bitmap b = new Bitmap(Path.Combine(executableDirectory.Substring(6), g.Icon));
                    smallIconList.Images.Add(b);
                    mediumIconList.Images.Add(b);
                    lvi.SubItems.Add(g.IState());
                    lvi.SubItems.Add(g.Description);
                    listView1.Items.Add(lvi);
                    i++;
                }
            }
            catch (Exception sarlon)
            {
                MessageBox.Show("Il manque un fichier de configuration : Jeux.xml", "Impossible de lancer le jeu", MessageBoxButtons.OK);
            }
        }
        
        /// <summary>
        /// Lancement du jeu
        /// </summary>
        /// <param name="g">Jeux à lancer</param>
        private void LaunchGame(Game g)
        {
            if (Game.IsPlayable(g))
            {
                if (g.Install == InstallState.UpdateAvailable)
                {
                    DialogResult update = MessageBox.Show("Une mise à jour est disponible pour ce jeu.\nVoulez-vous l'installer ?", "Mise à jour disponible", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (update == DialogResult.Yes)
                    {
                        foreach (ListViewItem lvi in listView1.Items)
                        {
                            if (((Game)lvi.Tag).Executable == g.Executable)
                            {
                                foreach (Game rg in remoteGames)
                                {
                                    if (rg.Executable == g.Executable)
                                    {
                                        g = rg;
                                        Console.WriteLine(rg.Description);
                                        lvi.Tag = g;
                                    }
                                }
                            }
                        }
                        try
                        {
                            WebClient client = new WebClient();
                            client.DownloadFile(new Uri("http://g2iut.alwaysdata.net/games/" + g.Executable + ".gzip"), Path.Combine(temporaryPath, g.Executable));
                            g.Install = InstallState.Installing;
                            ZipHandler.DecompressToDirectory(Path.Combine(temporaryPath, g.Executable), Path.Combine(executableDirectory.Substring(6), "games", g.Executable));
                            if (File.Exists(Path.Combine(executableDirectory.Substring(6), g.Icon)))
                                File.Delete(Path.Combine(executableDirectory.Substring(6), g.Icon));
                            File.Copy(temporaryPath + g.Icon.Substring(g.Icon.IndexOf("\\")), Path.Combine(executableDirectory.Substring(6), g.Icon));
                            g.Install = InstallState.UpToDate;

                            try
                            {
                                using (FileStream fs = new FileStream(Path.Combine(executableDirectory.Substring(6), "Jeux.xml"), FileMode.Open))
                                {
                                    fs.Seek(0, SeekOrigin.Begin);
                                    MoveAfter(fs, g.Executable);
                                    if (fs.Position > 100)
                                    {
                                        fs.Seek(-100, SeekOrigin.Current);
                                        MoveAfter(fs, "</Jeu>");
                                    }
                                    else
                                    {
                                        fs.Seek(0, SeekOrigin.Begin);
                                        MoveAfter(fs, "<Jeux>");
                                    }
                                    long position = fs.Position;
                                    MoveAfter(fs, "</Jeu>");
                                    byte[] array = new byte[fs.Length - fs.Position];
                                    fs.Read(array, 0, (int)(fs.Length - fs.Position));
                                    fs.Seek(position, SeekOrigin.Begin);
                                    fs.Write(array, 0, array.Length);
                                    while (fs.Position < fs.Length)
                                        fs.WriteByte((byte)' ');
                                    fs.Close();
                                }
                                WriteGame(g);
                            }
                            catch(Exception sarlon)
                            {
                                MessageBox.Show("Le fichier de configuration Jeux.xml est inaccessible.\nVos changements ne seront pas pris en compte au prochain démarrage.", "Fichier manquant", MessageBoxButtons.OK);
                            }
                        }
                        catch (Exception sarlon)
                        {
                            string msg = "";
#if DEBUG
                            msg = sarlon.Message;
#endif
                            MessageBox.Show("Impossible de contacter le serveur " + msg, "Erreur lors de la récupération", MessageBoxButtons.OK);
                        }
                    }
                    else if (update == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                System.Diagnostics.ProcessStartInfo myInfo = new System.Diagnostics.ProcessStartInfo();
                myInfo.FileName = g.BaseName;
                myInfo.WorkingDirectory = Path.Combine("games", g.PathName);
                System.Diagnostics.Process.Start(myInfo);
            }
            else if (g.Install == InstallState.Installing)
            {
                MessageBox.Show("Veuillez patienter, l'installation est en cours...", "Installation en cours...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /*
                DialogResult reload = MessageBox.Show("Veuillez patienter, le téléchargement est en cours...\nVoulez-vous annuler le téléchargement en cours et le relancer ?", "Téléchargement en cours...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (reload == DialogResult.Yes)
                {
                    // TODO : Annuler le téléchargement et le recommencer
                }*/
            }
            else
            {
                DialogResult install = MessageBox.Show("Ce jeu n'est pas installé. Voulez-vous le télecharger et l'installer ?", "Jeu non-installé", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (install == DialogResult.Yes)
                {
                    // TODO : Téléchargement Asynchrone, avec barre de progression
                    // pour ne pas bloquer la plateforme
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile(new Uri("http://g2iut.alwaysdata.net/games/" + g.Executable + ".gzip"), Path.Combine(temporaryPath, g.Executable));
                        g.Install = InstallState.Installing;
                        ZipHandler.DecompressToDirectory(Path.Combine(temporaryPath, g.Executable), Path.Combine(executableDirectory.Substring(6), "games", g.Executable));
                        g.Install = InstallState.Installed;
                        if (File.Exists(Path.Combine(executableDirectory.Substring(6), g.Icon)))
                            File.Delete(Path.Combine(executableDirectory.Substring(6), g.Icon));
                        File.Copy(temporaryPath + g.Icon.Substring(g.Icon.LastIndexOf("\\")), Path.Combine(executableDirectory.Substring(6), g.Icon));
                        WriteGame(g);
                    }
                    catch (Exception sarlon)
                    {
                        string msg = "";
#if DEBUG
                        msg = sarlon.Message;
#endif
                        MessageBox.Show("Impossible de contacter le serveur " + msg, "Erreur lors de la récupération", MessageBoxButtons.OK);
                    }
                }
            }
        }

        /// <summary>
        /// Lors du changement de mode d'affichage, modification de l'élément sélectionné
        /// </summary>
        /// <param name="item">Element à sélectionner</param>
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

        /// <summary>
        /// Positionne le flux après la chaîne de caractères passée en paramètres
        /// </summary>
        /// <param name="fs">Le FileStream à positionner</param>
        /// <param name="target">La chaîne de caractères après laquelle se positionner</param>
        private static void MoveAfter(FileStream fs, string target)
        {
            int i = 0, j;
            while (i < target.Length)
            {
                if (target[i] != (j = fs.ReadByte()))
                    i = 0;
                else
                    i++;
            }
        }

        /// <summary>
        /// Ajoute le jeu passé en paramètres au fichier Jeux.xml
        /// </summary>
        /// <param name="g">Le jeu à ajouter</param>
        private void WriteGame(Game g)
        {
            try
            {
                using (FileStream fs = new FileStream(Path.Combine(executableDirectory.Substring(6), "Jeux.xml"), FileMode.Open))
                {
                    MoveAfter(fs, "</Jeux>");
                    fs.Seek(-7, SeekOrigin.Current);
                    AddText(fs, "\t<Jeu>\n");
                    AddText(fs, "\t\t<Nom>" + g.Name + "</Nom>\n");
                    AddText(fs, "\t\t<Icone>" + g.RealIcon + "</Icone>\n");
                    AddText(fs, "\t\t<Executable>" + g.Executable + "</Executable>\n");
                    AddText(fs, "\t\t<Description>" + g.Description + "</Description>\n");
                    AddText(fs, "\t\t<Version>" + g.Version.ToString() + "</Version>\n");
                    AddText(fs, "\t</Jeu>\n");
                    AddText(fs, "</Jeux>");
                    fs.Close();
                }
            }
            catch (Exception sarlon)
            {
                MessageBox.Show("Le fichier de configuration Jeux.xml est inaccessible.\nVos changements ne seront pas pris en compte au prochain démarrage.\n" + sarlon.Message, "Fichier manquant", MessageBoxButtons.OK);
            }
        }
        #endregion

        #region Protected Override Methods
        /// <summary>
        /// Méthode executée lors de la fermeture de la fenêtre.
        /// Elle s'occupe entre autres de supprimer les fichiers/dossiers temporaires
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Dispose();
 	        base.OnFormClosing(e);
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Recharge les SubItems de chaque ListViewItem
        /// </summary>
        public static void ReloadListView()
        {
            foreach (Interface inter in interfaces)
            {
                foreach (ListViewItem lvi in inter.listView1.Items)
                {
                    Game rg = (Game)lvi.Tag;
                    lvi.SubItems[1].Text = rg.IState();
                    lvi.SubItems[2].Text = rg.Description;
                }
            }
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Methode pour écrire dans un fichier
        /// </summary>
        /// <param name="fs">Le FileStream dans lequel écrire</param>
        /// <param name="value">La chaîne de caractères à écrire</param>
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        #endregion
    }
}
