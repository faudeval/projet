using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace TestInterface
{
    class BDD
    {
        private static string server = "mysql2.alwaysdata.com";
        private static string dbuser = "g2iut";
        private static string dbpass = "rolypolyholy";
        private static string database = "g2iut_db";

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
            catch (Exception e)
            { ds = null; }
            return ds;
        }
    }
}
