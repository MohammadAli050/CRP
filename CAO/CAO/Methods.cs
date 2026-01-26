
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;

namespace Common
{
    public class Methods
    {
        public static string[] SplitValues(string str)
        {
            return str.Split(new char[] { ',', '-',' '});
        }
        /// <summary>
        /// sql parameters (name and value) are set here.
        /// here "0" value is considered to be DBNull
        /// </summary>
        /// <param name="dps"></param>
        /// <returns></returns>
        public static List<SqlParameter> GetSQLParameters(DAOParameters dps)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            foreach (DictionaryEntry de in dps.hs)
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = de.Key.ToString();
                if (de.Value.ToString() == Convert.ToString(0))
                {
                    p.Value = DBNull.Value;
                }
                else
                {
                    p.Value = de.Value;
                }

                parameters.Add(p);
            }

            return parameters;
        }

        /// <summary>
        /// sql parameters (name and value) are set here.
        /// here "0" value is not considered to be DBNull
        /// </summary>
        /// <param name="dps"></param>
        /// <returns></returns>
        public static List<SqlParameter> GetSQLParametersWithZero(DAOParameters dps)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            foreach (DictionaryEntry de in dps.hs)
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = de.Key.ToString();
                p.Value = de.Value;

                parameters.Add(p);
            }

            return parameters;
        }
    }    
}
