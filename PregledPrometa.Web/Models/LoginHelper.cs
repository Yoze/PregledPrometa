using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PregledPrometa.Web.Models
{
    public class LoginHelper
    {

        public static string ELBS_ConnString = Properties.Settings.Default.ELBS_ConnString;
        public static SqlConnection ELBS_Connection;

        /** 
        * LOGIN Helpers 
        */

        public List<string> LogInUserAndGetPodaciOperatera(string txtPwd, string txtUser)
        {
            List<string> podaciOperatera = new List<string>();

            using (ELBS_Connection = CreateElbsSqlConnection(txtUser, txtPwd))
            {
                try
                {
                    ELBS_Connection.Open();
                    if (ELBS_Connection.State == System.Data.ConnectionState.Open) // <== throw network exception here !!!
                    {
                        podaciOperatera = GetPodaciOperatera(txtUser);
                        return podaciOperatera;
                    }
                    else return null;

                }
                catch (Exception)
                {
                    return null;
                }
            }

        }



        private List<string> GetPodaciOperatera(string txtUser)
        {
            List<string> podaciOperatera = new List<string>();

            try
            {
                string selectLoggedUser = "SELECT shpro, naziv, userid FROM operater WHERE userid='" + txtUser + "'";

                SqlCommand komanda = new SqlCommand(selectLoggedUser, ELBS_Connection);
                SqlDataReader dr = komanda.ExecuteReader();

                while (dr.Read())
                {
                    podaciOperatera.Add(dr[0].ToString());
                    podaciOperatera.Add(dr[1].ToString());
                    podaciOperatera.Add(dr[2].ToString());
                }
                dr.Close();

                return podaciOperatera;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private SqlConnection CreateElbsSqlConnection(string txtUser, string txtPwd)
        {
            ELBS_ConnString = ELBS_ConnString + ";User ID=" + txtUser + ";Password=" + txtPwd;

            SqlConnection elbsConnection = new SqlConnection(ELBS_ConnString);

            return elbsConnection;
        }


    }
}