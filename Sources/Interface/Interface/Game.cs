#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
#endregion

namespace TestInterface
{
    /// <summary>
    /// Enumération des états d'installation
    /// </summary>
    enum InstallState { Installed, UpdateAvailable, NotInstalled, Installing, UpToDate }

    /// <summary>
    /// Classe de jeu
    /// </summary>
    class Game
    {
        #region Fields
        string icon;
        InstallState install;
        #endregion

        #region Properties
        /// <summary>
        /// Version du jeu
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Description du jeu
        /// </summary>
        public String Description { get; private set; }
        /// <summary>
        /// Nom du jeu
        /// </summary>
        public String Name { get; private set; }
        /// <summary>
        /// Chemin vers l'exécutable
        /// </summary>
        public String Executable { get; private set; }

        /// <summary>
        /// Nom de fichier de l'icône du jeu
        /// </summary>
        public string RealIcon { get { return icon; } }

        /// <summary>
        /// Permet de mettre à jour l'affichage lors de la mise à jour de l'état d'installation
        /// </summary>
        public InstallState Install
        {
            get { return install; }
            set { install = value; Interface.ReloadListView(); }
        }

        /// <summary>
        /// Chemin vers le fichier de l'icône du jeu
        /// </summary>
        public String Icon
        {
            get { return (Install != InstallState.NotInstalled ? @"icons\" : "") + icon; }
            private set { icon = value; }
        }

        /// <summary>
        /// Chemin vers le dossier contenant le jeu
        /// </summary>
        public string BaseName
        {
            get
            {
                string str = Executable.IndexOf("\\") == -1 ? Executable : Executable.Substring(Executable.IndexOf("\\"));
                return str.Contains(".exe") ? str : str + ".exe";
            }
        }

        /// <summary>
        /// Chemin du vers l'exécutable
        /// </summary>
        public string PathName
        {
            get { return Executable.IndexOf("\\") == -1 ? Executable : Executable.Substring(0, Executable.IndexOf("\\")); }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom du jeu</param>
        /// <param name="icône">Icône représentant le jeu</param>
        /// <param name="executablePath">Chemin vers l'executable du jeu</param>
        /// <param name="description">Description du jeu</param>
        public Game(String nom, String icône, String executablePath, string description, int version, InstallState installed)
        {
            this.Name = nom;
            this.Icon = icône;
            this.Executable = executablePath;
            this.Description = description;
            this.Install = installed;
            this.Version = version;
        }
        #endregion

        #region Override Methods
#if DEBUG
        /// <summary>
        /// Affiche les membres significatifs du jeu
        /// </summary>
        /// <returns>Chaîne de caractères décrivant le jeu</returns>
        public override string ToString()
        {
            return ("Nom : " + Name + "\nIcône : " + Icon + "\nExecutable : " + Executable + "\nDescription : " + Description);
        }
#endif
        #endregion

        #region Public Methods
        /// <summary>
        /// Teste si deux jeux sont identiques (même version, description, nom et executable)
        /// </summary>
        /// <param name="g">Le jeu à tester</param>
        /// <returns>true ou false selon les membres des jeux</returns>
        public bool Equals(Game g)
        {
            return g.Version == Version &&
                g.Description == Description &&
                g.Name == Name &&
                g.Executable == Executable;
        }

        /// <summary>
        /// Renvoit le texte à afficher en fonction de l'état d'installation du jeu
        /// </summary>
        /// <returns>l'état du jeu</returns>
        public string IState()
        {
            switch (Install)
            {
                case InstallState.Installed:
                    return "Installé";
                case InstallState.Installing:
                    return "Téléchargement en cours...";
                case InstallState.UpdateAvailable:
                    return "Mise à jour disponible";
                case InstallState.UpToDate :
                    return "Jeu à jour";
                default:
                    return "Pas installé";
            }
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Indique si le jeu peut être lancé
        /// </summary>
        /// <param name="g">Le jeu à tester</param>
        /// <returns>true ou false selon l'état d'installation du jeu</returns>
        public static bool IsPlayable(Game g)
        {
            return g.Install == InstallState.Installed || g.Install == InstallState.UpdateAvailable || g.Install == InstallState.UpToDate;
        }
        #endregion
    }
}
