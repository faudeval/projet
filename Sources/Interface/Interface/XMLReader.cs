#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
#endregion

namespace TestInterface
{
    /// <summary>
    /// Classe permettant d'extraire des informations d'un fichier XML
    /// </summary>
    static class XMLReader
    {
        #region Private Static Methods
        /// <summary>
        /// Methode retournant un HashTable contenant le nom de la balise
        /// comme clé et son contenu comme valeur, ou alors seulement le
        /// contenu de la balise sous forme de chaîne de caractères
        /// </summary>
        /// <param name="textReader">XmlTextReader du fichier à explorer</param>
        /// <returns>Objet contenant le contenu de la balise</returns>
        private static Object ReturnElement(XmlTextReader textReader)
        {
            Hashtable table = new Hashtable();
            Object returnElement;
            while (textReader.Read())
            {
                textReader.MoveToElement();
                if (textReader.NodeType == XmlNodeType.Element)
                {
                    String name = textReader.Name;
                    do
                    {
                        returnElement = ReturnElement(textReader);
                        if (returnElement is String)
                        {
                            if (table.ContainsKey(name))
                            {
                                if (table[name] is List<Object>)
                                    ((List<Object>)table[name]).Add(returnElement);
                                else
                                {
                                    Object o = table[name];
                                    table[name] = new List<Object>();
                                    ((List<Object>)table[name]).Add(o);
                                    ((List<Object>)table[name]).Add(returnElement);
                                }
                            }
                            else
                                table.Add(name, returnElement);
                        }
                        else
                        {
                            if (table.ContainsKey(name))
                            {
                                if (table[name] is List<Object>)
                                    ((List<Object>)table[name]).Add(new Hashtable((Hashtable)returnElement));
                                else
                                {
                                    Object o = table[name];
                                    table[name] = new List<Object>();
                                    ((List<Object>)table[name]).Add(o);
                                    ((List<Object>)table[name]).Add(new Hashtable((Hashtable)returnElement));
                                }
                            }
                            else
                                table.Add(name, new Hashtable((Hashtable)returnElement));
                        }
                    } while (textReader.Name != name || textReader.NodeType != XmlNodeType.EndElement);

                }
                else if (textReader.NodeType == XmlNodeType.Text)
                {
                    returnElement = textReader.Value;
                    textReader.Read();
                    return (String)returnElement;
                }
                else if (textReader.NodeType == XmlNodeType.EndElement)
                    return table;
            }
            return table;
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Methode principale permettant de générer une liste de jeux
        /// à partir du fichier XML passé en paramètres
        /// </summary>
        /// <param name="path">Chemin vers le fichier XML</param>
        /// <returns>Liste des jeux à retourner</returns>
        public static List<Game> ReadXML(string path)
        {
            List<Game> games = new List<Game>();

            XmlTextReader textReader = new XmlTextReader(path);
            textReader.WhitespaceHandling = WhitespaceHandling.None;
            Hashtable XML = (Hashtable)ReturnElement(textReader);
            //Console.WriteLine(PrintKeysAndValues(XML, ""));

            try
            {
                XML = (Hashtable)XML["Jeux"];
                List<Object> list = (List<Object>)(XML["Jeu"]);

                foreach (Object o in list) // Pour chaque Jeu
                {
                    Hashtable ht = (Hashtable)o;

                    // On l'ajoute à jeux
                    games.Add(new Game((String)(ht["Nom"]), (String)((ht["Icone"])), (String)(ht["Executable"]), (String)(ht["Description"]), int.Parse((String)(ht["Version"])), InstallState.Installed));
                }

                foreach (Game j in games)
                    Console.WriteLine("{0}", j);
                /*            }
                            catch (Exception e)
                            { Console.WriteLine("Erreur : {0}", e.Message); } // Fichier non trouvé*/
                textReader.Close();
            }
            catch (Exception sarlon)
            { }
            return games;
        }

#if DEBUG
        /// <summary>
        /// Affiche les valeurs associées d'un Hashtable
        /// </summary>
        /// <param name="myHT">Hashtable à afficher</param>
        /// <param name="previous">Chaîne de caractères à afficher avant les valeurs</param>
        /// <returns>Chaîne de caractères contenant le hashtable mis en forme</returns>
        public static String PrintKeysAndValues(Hashtable myHT, string previous)
        {
            String s = "";
            foreach (DictionaryEntry de in myHT)
            {
                s += previous + de.Key + " : ";
                if (de.Value is Hashtable)
                {
                    s += "\n";
                    s += PrintKeysAndValues((Hashtable)de.Value, previous + "\t");
                }
                else if (de.Value is List<Object>)
                {
                    foreach (Object o in (List<Object>)de.Value)
                    {
                        s += "\n";
                        if (o is String)
                            s += "\t" + previous + o.ToString();
                        else
                            s += "---" + PrintKeysAndValues((Hashtable)o, previous + "\t");
                    }
                }
                else
                    s += de.Value;
                s += "\n";
            }
            s += "\n";
            return s;
        }
#endif
        #endregion
    }
}
