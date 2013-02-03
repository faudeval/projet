using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace TestInterface
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
// Vérification de l'existence des fichiers/dossiers nécessaires
            if (!Directory.Exists("games"))
                Directory.CreateDirectory("games");
            if (!Directory.Exists("icons"))
                Directory.CreateDirectory("icons");
            if (!File.Exists("Jeux.xml"))
                File.Create("Jeux.xml");
            while (!File.Exists("jeux.xml")) ;
            FileStream fss = new FileStream("Jeux.xml", FileMode.Open);
            bool isFileInvalid = false;
            if(fss.Length < 13)
                isFileInvalid = true;
            fss.Close();
            if (isFileInvalid)
            {
                using (FileStream fs = new FileStream("Jeux.xml", FileMode.Create))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("<Jeux></Jeux>");
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Interface());
        }
    }
}
