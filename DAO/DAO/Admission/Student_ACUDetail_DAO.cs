using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Student_ACUDetailEntity_DAO:Base_DAO
    {
        #region Constants
        #region Column Constants
        private const string STDACUDETAILID = "StdACUDetailID";//0

        private const string STDACADEMICCALENDERID = "StdAcademicCalenderID";//1
        private const string STDACADEMICCALENDERID_PA = "@StdAcademicCalenderID";

        private const string STUDENTID = "StudentID";//2
        private const string STUDENTID_PA = "@StudentID";

        private const string STATUSTYPEID = "StatusTypeID";//3
        private const string STATUSTYPEID_PA = "@StatusTypeID";

        private const string SCHSETUPID = "SchSetUpID";//4
        private const string SCHSETUPID_PA = "@SchSetUpID";

        private const string CGPAC = "CGPA";//5
        private const string CGPAC_PA = "@CGPA";

        private const string GPAC = "GPA";//6
        private const string GPAC_PA = "@GPA";

        private const string DESCRIPTION = "Description";//7
        private const string DESCRIPTION_PA = "@Description";
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + STDACUDETAILID + "], "//0
                                        + "[" + STDACADEMICCALENDERID + "], "
                                        + "[" + STUDENTID + "], "
                                        + "[" + STATUSTYPEID + "], "//0
                                        + "[" + SCHSETUPID + "], "
                                        + "[" + CGPAC + "], "
                                        + "[" + GPAC + "], "
                                        + "[" + DESCRIPTION + "], ";//11
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + STDACADEMICCALENDERID + "], "
                                        + "[" + STUDENTID + "], "
                                        + "[" + STATUSTYPEID + "], "//0
                                        + "[" + SCHSETUPID + "], "
                                        + "[" + CGPAC + "], "
                                        + "[" + GPAC + "], "
                                        + "[" + DESCRIPTION + "], ";//11
        #endregion

        private const string TABLENAME = " [StudentACUDetail] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + NOPKCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
            //+ ID_PA + ", "//0
                     + STDACADEMICCALENDERID_PA + ", "
                     + STUDENTID_PA + ", "
                     + STATUSTYPEID_PA + ", "//0
                     + SCHSETUPID_PA + ", "
                     + CGPAC_PA + ", "
                     + GPAC_PA + ", "
                     + DESCRIPTION_PA + ", "
                     + CREATORID_PA + ", "//12
                     + CREATEDDATE_PA + ", "//13
                     + MODIFIERID_PA + ", "//14
                     + MOIDFIEDDATE_PA + ")";//15 
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + STDACADEMICCALENDERID + "] = " + STDACADEMICCALENDERID_PA + ", "//1
                    + "[" + STUDENTID + "] = " + STUDENTID_PA + ", "//3
                    + "[" + STATUSTYPEID + "] = " + STATUSTYPEID_PA + ", "//3
                    + "[" + SCHSETUPID + "] = " + SCHSETUPID_PA + ", "//3
                    + "[" + CGPAC + "] = " + CGPAC_PA + ", "//3
                    + "[" + GPAC + "] = " + GPAC_PA + ", "//3
                    + "[" + DESCRIPTION + "] = " + DESCRIPTION_PA + ", "//2
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "//12
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//13
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "//14
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//15
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Methods
        private static Student_ACUDetailEntity Mapper(SQLNullHandler nullHandler)
        {
            Student_ACUDetailEntity obj = new Student_ACUDetailEntity();

            obj.Id = nullHandler.GetInt32(STDACUDETAILID);//0
            obj.CGPA = nullHandler.GetDecimal(CGPAC);//2
            obj.GPA = nullHandler.GetDecimal(GPAC);//2
            obj.SchSetUpID = nullHandler.GetInt32(SCHSETUPID);//2
            obj.StatusTypeID = nullHandler.GetInt32(STATUSTYPEID);//2
            obj.StdAcademicCalenderID = nullHandler.GetInt32(STDACADEMICCALENDERID);//2
            obj.Description = nullHandler.GetString(DESCRIPTION);//3
            obj.StudentID = nullHandler.GetInt32(STUDENTID);//4
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static Student_ACUDetailEntity MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Student_ACUDetailEntity obj = null;
            if (theReader.Read())
            {
                obj = new Student_ACUDetailEntity();
                obj = Mapper(nullHandler);
            }

            return obj;
        }      

        internal static Student_ACUDetailEntity GetMaxByStudentID(int studentID)
        {
            SqlDataReader theReader = null;
            try
            {
                string command = SELECT
                       + "WHERE [" + STDACUDETAILID + "] IN "
                       + "(SELECT MAX([" + STDACUDETAILID + "]) FROM " + TABLENAME + " WHERE [" + STUDENTID + "] = " + STUDENTID_PA + ")";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(STUDENTID_PA, studentID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                theReader = QueryHandler.ExecuteSelectBatchQuery(command, ps);
                Student_ACUDetailEntity obj = MapClass(theReader);
                theReader.Close();

                return obj;
            }
            catch (Exception ex)
            {
                //FixMe
                theReader.Close();
                throw ex;
            }
        }
        #endregion
    }
}
