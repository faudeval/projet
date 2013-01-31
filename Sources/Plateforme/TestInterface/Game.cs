using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace TestInterface
{
    enum InstallState { Installed, UpdateAvailable, NotInstalled, Installing }
    class Game
    {
        string icone;
        public int Version { get; set; }
        public String Description { get; private set; }
        public String Name { get; private set; }
        public String Executable { get; private set; }
        public InstallState Install { get; set; }
        public String Icon
        {
            get { return (Install != InstallState.NotInstalled ? @"icons\" : @"remoteIcons\") + icone; }
            private set { icone = value; }
        }
        public string BaseName
        {
            get
            {
                string str = Executable.IndexOf("\\") == -1 ? Executable : Executable.Substring(Executable.IndexOf("\\"));
                return str.Contains(".exe") ? str : str + ".exe";
            }
        }
        public string PathName
        {
            get { return Executable.IndexOf("\\") == -1 ? Executable : Executable.Substring(0, Executable.IndexOf("\\")); }
        }

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
        }

#if DEBUG
        public override string ToString()
        {
            return ("Nom : " + Name + "\nIcône : " + Icon + "\nExecutable : " + Executable + "\nDescription : " + Description);
        }
#endif

        public bool Equals(Game g)
        {
            return g.Version == Version &&
                g.Description == Description &&
                g.Name == Name &&
                g.Executable == Executable;
        }

        public static bool IsPlayable(Game g)
        {
            return g.Install == InstallState.Installed || g.Install == InstallState.UpdateAvailable;
        }

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
                default:
                    return "Pas installé";
            }
        }
    }
}
