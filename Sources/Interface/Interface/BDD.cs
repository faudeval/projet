using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace TestInterface
{
    /// <summary>
    /// Classe gérant
    /// </summary>
    class BDD
    {
        #region Configuration
        private static string server = "mysql2.alwaysdata.com";
        private static string dbuser = "g2iut";
        private static string dbpass = "rolypolyholy";
        private static string database = "g2iut_db";
        #endregion

        #region Static Methods
        /// <summary>
        /// Envoie une requête
        /// </summary>
        /// <param name="strRequete">Requête à exécuter</param>
        /// <param name="columnsToRetrieve">Champs à récupérer</param>
        /// <returns>DataSet contenant les champs</returns>
        public static DataSet query(String strRequete, params String[] columnsToRetrieve)
        {
            String strConn = String.Format("server={0}; user id={1}; password={2}; database={3}",
                server,
                dbuser,
                dbpass,
                database);
            DataSet ds = new DataSet();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(strConn))
                {
                    conn.Open();
                    MySqlCommand requete = new MySqlCommand();
                    requete.Connection = conn;
                    requete.CommandText = strRequete;
                    MySqlDataReader dr = requete.ExecuteReader();
                    ds.Load(dr, LoadOption.OverwriteChanges, columnsToRetrieve);
                }
            }
            catch (Exception sarlon)
            { ds = null; }
            return ds;
        }
        #endregion
    }
}
