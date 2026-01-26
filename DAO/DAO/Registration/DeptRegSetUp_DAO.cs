using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DeptRegSetUpEntity_DAO:Base_DAO
    {
        #region Constants
        #region Columns
        private const string DeptRegSetUpID = "DeptRegSetUpID";

        private const string PROGRAMID = "ProgramID";
        private const string PROGRAMID_PA = "@ProgramID";

        private const string LOCALCGPA1 = "LocalCGPA1";
        private const string LOCALCGPA1_PA = "@LocalCGPA1";

        private const string LOCALCREDIT1 = "LocalCredit1";
        private const string LOCALCREDIT1_PA = "@LocalCredit1";

        private const string LOCALCGPA2 = "LocalCGPA2";
        private const string LOCALCGPA2_PA = "@LocalCGPA2";

        private const string LOCALCREDIT2 = "LocalCredit2";
        private const string LOCALCREDIT2_PA = "@LocalCredit2";

        private const string LOCALCGPA3 = "LocalCGPA3";
        private const string LOCALCGPA3_PA = "@LocalCGPA3";

        private const string LOCALCREDIT3 = "LocalCredit3";
        private const string LOCALCREDIT3_PA = "@LocalCredit3";

        private const string MANCGPA1 = "ManCGPA1";
        private const string MANCGPA1_PA = "@ManCGPA1";

        private const string MANCREDIT1 = "ManCredit1";
        private const string MANCREDIT1_PA = "@ManCredit1";

        private const string MANRETAKEGRADELIMIT1 = "ManRetakeGradeLimit1";
        private const string MANRETAKEGRADELIMIT1_PA = "@ManRetakeGradeLimit1";

        private const string MANCGPA2 = "ManCGPA2";
        private const string MANCGPA2_PA = "@ManCGPA2";

        private const string MANCREDIT2 = "ManCredit2";
        private const string MANCREDIT2_PA = "@ManCredit2";

        private const string MANRETAKEGRADELIMIT2 = "ManRetakeGradeLimit2";
        private const string MANRETAKEGRADELIMIT2_PA = "@ManRetakeGradeLimit2";

        private const string MANCGPA3 = "ManCGPA3";
        private const string MANCGPA3_PA = "@ManCGPA3";

        private const string MANCREDIT3 = "ManCredit3";
        private const string MANCREDIT3_PA = "@ManCredit3";

        private const string MANRETAKEGRADELIMIT3 = "ManRetakeGradeLimit3";
        private const string MANRETAKEGRADELIMIT3_PA = "@ManRetakeGradeLimit3";

        private const string MAXCGPA1 = "MaxCGPA1";
        private const string MAXCGPA1_PA = "@MaxCGPA1";

        private const string MAXCREDIT1 = "MaxCredit1";
        private const string MAXCREDIT1_PA = "@MaxCredit1";

        private const string MAXCGPA2 = "MaxCGPA2";
        private const string MAXCGPA2_PA = "@MaxCGPA2";

        private const string MAXCREDIT2 = "MaxCredit2";
        private const string MAXCREDIT2_PA = "@MaxCredit2";

        private const string MAXCGPA3 = "MaxCGPA3";
        private const string MAXCGPA3_PA = "@MaxCGPA3";

        private const string MAXCREDIT3 = "MaxCredit3";
        private const string MAXCREDIT3_PA = "@MaxCredit3";

        private const string COURSERETAKELIMIT = "CourseRetakeLimit";
        private const string COURSERETAKELIMIT_PA = "@CourseRetakeLimit";

        private const string PROJECTCGPA = "ProjectCGPA";
        private const string PROJECTCGPA_PA = "@ProjectCGPA";

        private const string PROJECTCREDIT = "ProjectCredit";
        private const string PROJECTCREDIT_PA = "@ProjectCredit";

        private const string THESISCGPA = "ThesisCGPA";
        private const string THESISCGPA_PA = "@ThesisCGPA";

        private const string THESISCREDIT = "ThesisCredit";
        private const string THESISCREDIT_PA = "@ThesisCredit";

        private const string MAJORCGPA = "MajorCGPA";
        private const string MAJORCGPA_PA = "@MajorCGPA";

        private const string MAJORCREDIT = "MajorCredit";
        private const string MAJORCREDIT_PA = "@MajorCredit";

        private const string PROBATIONLOCK = "ProbationLock";
        private const string PROBATIONLOCK_PA = "@ProbationLock";

        #endregion

        #region All-Columns
        private const string ALLCOLUMNS = DeptRegSetUpID + ", "
                                + PROGRAMID + ", "
                                + LOCALCGPA1 + ", "
                                + LOCALCREDIT1 + ", "
                                + LOCALCGPA2 + ", "
                                + LOCALCREDIT2 + ", "
                                + LOCALCGPA3 + ", "
                                + LOCALCREDIT3 + ", "
                                + MANCGPA1 + ", "
                                + MANCREDIT1 + ", "
                                + MANRETAKEGRADELIMIT1 + ", "
                                + MANCGPA2 + ", "
                                + MANCREDIT2 + ", "
                                + MANRETAKEGRADELIMIT2 + ", "
                                + MANCGPA3 + ", "
                                + MANCREDIT3 + ", "
                                + MANRETAKEGRADELIMIT3 + ", "
                                + MAXCGPA1 + ", "
                                + MAXCREDIT1 + ", "
                                + MAXCGPA2 + ", "
                                + MAXCREDIT2 + ", "
                                + MAXCGPA3 + ", "
                                + MAXCREDIT3 + ", "
                                + PROJECTCGPA + ", "
                                + PROJECTCREDIT + ", "
                                + THESISCGPA + ", "
                                + THESISCREDIT + ", "
                                + MAJORCGPA + ", "
                                + MAJORCREDIT + ", "
                                + PROBATIONLOCK + ", "
                                + COURSERETAKELIMIT + ", ";

        #endregion

        #region NoPK-Columns
        private const string NOPKCOLUMNS = PROGRAMID + ", "
                                + LOCALCGPA1 + ", "
                                + LOCALCREDIT1 + ", "
                                + LOCALCGPA2 + ", "
                                + LOCALCREDIT2 + ", "
                                + LOCALCGPA3 + ", "
                                + LOCALCREDIT3 + ", "
                                + MANCGPA1 + ", "
                                + MANCREDIT1 + ", "
                                + MANRETAKEGRADELIMIT1 + ", "
                                + MANCGPA2 + ", "
                                + MANCREDIT2 + ", "
                                + MANRETAKEGRADELIMIT2 + ", "
                                + MANCGPA3 + ", "
                                + MANCREDIT3 + ", "
                                + MANRETAKEGRADELIMIT3 + ", "
                                + MAXCGPA1 + ", "
                                + MAXCREDIT1 + ", "
                                + MAXCGPA2 + ", "
                                + MAXCREDIT2 + ", "
                                + MAXCGPA3 + ", "
                                + MAXCREDIT3 + ", "
                                + PROJECTCGPA + ", "
                                + PROJECTCREDIT + ", "
                                + THESISCGPA + ", "
                                + THESISCREDIT + ", "
                                + MAJORCGPA + ", "
                                + MAJORCREDIT + ", "
                                + PROBATIONLOCK + ", "
                                + COURSERETAKELIMIT + ", ";
        #endregion

        private const string TABLENAME = " [DeptRegSetUp] ";

        #region Select
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                     + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + NOPKCOLUMNS
                     + BASECOLUMNS
                     + ")"
                     + "VALUES ( " + PROGRAMID_PA + ", "
                                + LOCALCGPA1_PA + ", "
                                + LOCALCREDIT1_PA + ", "
                                + LOCALCGPA2_PA + ", "
                                + LOCALCREDIT2_PA + ", "
                                + LOCALCGPA3_PA + ", "
                                + LOCALCREDIT3_PA + ", "
                                + MANCGPA1_PA + ", "
                                + MANCREDIT1_PA + ", "
                                + MANRETAKEGRADELIMIT1_PA + ", "////888
                                + MANCGPA2_PA + ", "
                                + MANCREDIT2_PA + ", "
                                + MANRETAKEGRADELIMIT2_PA + ", "
                                + MANCGPA3_PA + ", "
                                + MANCREDIT3_PA + ", "
                                + MANRETAKEGRADELIMIT3_PA + ", "
                                + MAXCGPA1_PA + ", "
                                + MAXCREDIT1_PA + ", "
                                + MAXCGPA2_PA + ", "
                                + MAXCREDIT2_PA + ", "
                                + MAXCGPA3_PA + ", "
                                + MAXCREDIT3_PA + ", "
                                + PROJECTCGPA_PA + ", "
                                + PROJECTCREDIT_PA + ", "
                                + THESISCGPA_PA + ", "
                                + THESISCREDIT_PA + ", "
                                + MAJORCGPA_PA + ", "
                                + MAJORCREDIT_PA + ", "
                                + PROBATIONLOCK_PA + ", "
                                + COURSERETAKELIMIT_PA + ", "
                                + CREATORID_PA + ", "
                                + CREATEDDATE_PA + ", "
                                + MODIFIERID_PA + ", "
                                + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME + "SET "
                             + PROGRAMID + " = " + PROGRAMID_PA + ", "
                             + LOCALCGPA1 + " = " + LOCALCGPA1_PA + ", "
                             + LOCALCREDIT1 + " = " + LOCALCREDIT1_PA + ", "
                             + LOCALCGPA2 + " = " + LOCALCGPA2_PA + ", "
                             + LOCALCREDIT2 + " = " + LOCALCREDIT2_PA + ", "
                             + LOCALCGPA3 + " = " + LOCALCGPA3_PA + ", "
                             + LOCALCREDIT3 + " = " + LOCALCREDIT3_PA + ", "
                             + MANCGPA1 + " = " + MANCGPA1_PA + ", "
                             + MANCREDIT1 + " = " + MANCREDIT1_PA + ", "
                             + MANRETAKEGRADELIMIT1 + " = " + MANRETAKEGRADELIMIT1_PA + ", "
                             + MANCGPA2 + " = " + MANCGPA2_PA + ", "
                             + MANCREDIT2 + " = " + MANCREDIT2_PA + ", "
                             + MANRETAKEGRADELIMIT2 + " = " + MANRETAKEGRADELIMIT2_PA + ", "
                             + MANCGPA3 + " = " + MANCGPA3_PA + ", "
                             + MANCREDIT3 + " = " + MANCREDIT3_PA + ", "
                             + MANRETAKEGRADELIMIT3 + " = " + MANRETAKEGRADELIMIT3_PA + ", "
                             + MAXCGPA1 + " = " + MAXCGPA1_PA + ", "
                             + MAXCREDIT1 + " = " + MAXCREDIT1_PA + ", "
                             + MAXCGPA2 + " = " + MAXCGPA2_PA + ", "
                             + MAXCREDIT2 + " = " + MAXCREDIT2_PA + ", "
                             + MAXCGPA3 + " = " + MAXCGPA3_PA + ", "
                             + MAXCREDIT3 + " = " + MAXCREDIT3_PA + ", "
                             + PROJECTCGPA + " = " + PROJECTCGPA_PA + ", "
                             + PROJECTCREDIT + " = " + PROJECTCREDIT_PA + ", "
                             + THESISCGPA + " = " + THESISCGPA_PA + ", "
                             + THESISCREDIT + " = " + THESISCREDIT_PA + ", "
                             + MAJORCGPA + " = " + MAJORCGPA_PA + ", "
                             + MAJORCREDIT + " = " + MAJORCREDIT_PA + ", "
                             + PROBATIONLOCK + " = " + PROBATIONLOCK_PA + ", "
                             + COURSERETAKELIMIT + " = " + COURSERETAKELIMIT_PA + ", "
                             + CREATORID + " = " + CREATORID_PA + ","
                             + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                             + MODIFIERID + " = " + MODIFIERID_PA + ","
                             + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region Methods

        private static DeptRegSetUpEntity Mapper(SQLNullHandler nullHandler)
        {
            DeptRegSetUpEntity obj = new DeptRegSetUpEntity();

            obj.Id = nullHandler.GetInt32(DeptRegSetUpID);//0
            obj.ProgramID = nullHandler.GetInt32(PROGRAMID);//4
            obj.LocalCGPA1 = nullHandler.GetDecimal(LOCALCGPA1);//3
            obj.LocalCredit1 = nullHandler.GetDecimal(LOCALCREDIT1);//3
            obj.LocalCGPA2 = nullHandler.GetDecimal(LOCALCGPA2);//3
            obj.LocalCredit2 = nullHandler.GetDecimal(LOCALCREDIT2);//3
            obj.LocalCGPA3 = nullHandler.GetDecimal(LOCALCGPA3);//3
            obj.LocalCredit3 = nullHandler.GetDecimal(LOCALCREDIT3);//3

            obj.ManCGPA1 = nullHandler.GetDecimal(MANCGPA1);//3
            obj.ManCredit1 = nullHandler.GetDecimal(MANCREDIT1);//3
            obj.ManRetakeGradeLimit1 = nullHandler.GetString(MANRETAKEGRADELIMIT1);//3
            obj.ManCGPA2 = nullHandler.GetDecimal(MANCGPA2);//3
            obj.ManCredit2 = nullHandler.GetDecimal(MANCREDIT2);//3
            obj.ManRetakeGradeLimit2 = nullHandler.GetString(MANRETAKEGRADELIMIT2);//3
            obj.ManCGPA3 = nullHandler.GetDecimal(MANCGPA3);//3
            obj.ManCredit3 = nullHandler.GetDecimal(MANCREDIT3);//3
            obj.ManRetakeGradeLimit3 = nullHandler.GetString(MANRETAKEGRADELIMIT3);//3

            obj.MaxCGPA1 = nullHandler.GetDecimal(MAXCGPA1);//3
            obj.MaxCredit1 = nullHandler.GetDecimal(MAXCREDIT1);//3
            obj.MaxCGPA2 = nullHandler.GetDecimal(MAXCGPA2);//3
            obj.MaxCredit2 = nullHandler.GetDecimal(MAXCREDIT2);//3
            obj.MaxCGPA3 = nullHandler.GetDecimal(MAXCGPA3);//3
            obj.MaxCredit3 = nullHandler.GetDecimal(MAXCREDIT3);//3

            obj.ProjCGPA = nullHandler.GetDecimal(PROJECTCGPA);
            obj.ProjectCredit = nullHandler.GetDecimal(PROJECTCREDIT);
            obj.ThesisCGPA = nullHandler.GetDecimal(THESISCGPA);
            obj.ThesisCredit = nullHandler.GetDecimal(THESISCREDIT);
            obj.MajorCGPA = nullHandler.GetDecimal(MAJORCGPA);
            obj.MajorCredit = nullHandler.GetDecimal(MAJORCREDIT);
            obj.ProbLock = nullHandler.GetInt32(PROBATIONLOCK);

            obj.CourseRetakeLimit = nullHandler.GetInt32(COURSERETAKELIMIT);//3

            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static DeptRegSetUpEntity MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            DeptRegSetUpEntity obj = null;
            if (theReader.Read())
            {
                obj = new DeptRegSetUpEntity();
                obj = Mapper(nullHandler);
            }

            return obj;
        }       

        internal static DeptRegSetUpEntity GetByProgramID(int programID)
        {
            DeptRegSetUpEntity deptEntity = null;
            SqlDataReader theReader = null;

            try
            {
                string command = SELECT
                                    + "WHERE [" + PROGRAMID + "] = " + PROGRAMID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(PROGRAMID_PA, programID);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                theReader = QueryHandler.ExecuteSelectBatchQuery(command, ps);

                deptEntity = MapClass(theReader);
                theReader.Close();

            }
            catch (Exception ex)
            {
                //FixMe
                theReader.Close();
                throw ex;
            }
            return deptEntity;
        }
        #endregion

    }
}
