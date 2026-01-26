using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class BillableCourse_DAO : Base_DAO
    {
        #region DB Columns
        private const string BILLABLECOURSEID = "BillableCourseID";
        private const string ACACALID = "AcaCalID";
        private const string PROGID = "ProgramID";
        private const string BILLSTARTFROMRETAKENO = "BillStartFromRetakeNo";
        private const string ISCREDITCOURSE = "IsCreditCourse";

        private const string COURSEID = "CourseID";
        private const string VERSIONID = "VersionID";

        private const string ACACALID_PA = "@AcaCalID";
        private const string PROGID_PA = "@ProgramID";
        private const string BILLSTARTFROMRETAKENO_PA = "@BillStartFromRetakeNo";
        private const string ISCREDITCOURSE_PA = "@IsCreditCourse";
        private const string COURSEID_PA = "@CourseID";
        private const string VERSIONID_PA = "@VersionID";
        #endregion

        
		#region View Columns (not in the table)
        /*The other columns of the view(BILLABLECOURSEID,COURSEID,VERSIONID,ISCREDITCOURSE) has been declared*/
        private const string FORMAL_CODE = "FormalCode";
        private const string COURSE_TITLE = "Title";
        private const string CREDITS = "Credits";

        #endregion

        private const string ALLCOLUMNS = BILLABLECOURSEID + ", "
                                        + ACACALID + ", "
                                        + PROGID + ", "
                                        + BILLSTARTFROMRETAKENO + ", "
                                        + ISCREDITCOURSE + ", "
                                        + COURSEID + ", "
                                        + VERSIONID + ", ";
        private const string ALLCOLUMNSVIEW = BILLABLECOURSEID + ", "
                                            + FORMAL_CODE + ", "
                                            + COURSE_TITLE + ", "
                                            + CREDITS + ", "
                                            + ISCREDITCOURSE + ", "
                                            + COURSEID + ", "
                                            + VERSIONID;
        private const string NONPKCOLUMNS = ACACALID + ", "
                                        + PROGID + ", "
                                        + BILLSTARTFROMRETAKENO + ", "
                                        + ISCREDITCOURSE + ", "
                                        + COURSEID + ", "
                                        + VERSIONID + ", ";

        private const string NOTSETBILLABLECOLUMNS = FORMAL_CODE + ", "
                                                     + COURSE_TITLE + ", "
                                                     + CREDITS + ", "
                                                     + ISCREDITCOURSE + ","
                                                     + COURSEID + ", "
                                                     + VERSIONID;
        private const string TABLENAME = " [IsCourseBillable] ";
        private const string VIEW_NAME = " BILLABLE_COURSE_VIEW ";
        private const string VIEW_NOT_SET_BLLABLE_COURSES = " VIEW_NOT_SET_BILLABLE_COURSES ";

        #region SELECT
        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string SELECT_VIEW = "SELECT "
                                           + ALLCOLUMNSVIEW

                                           + " FROM " + VIEW_NAME;
        private const string SELECT_NOT_SET_BLLABLE_COURSES = "SELECT "
                                                            + NOTSETBILLABLECOLUMNS
                                                            + " FROM " 
                                                            + VIEW_NOT_SET_BLLABLE_COURSES;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NONPKCOLUMNS
                             + BASEMUSTCOLUMNS + ")"
                             + "VALUES ( "
                             + ACACALID_PA + ", "
                             + PROGID_PA + ", "
                             + BILLSTARTFROMRETAKENO_PA + ", "
                             + ISCREDITCOURSE_PA + ", "
                             + COURSEID_PA + ", "
                             + VERSIONID_PA + ", "
                             + CREATORID_PA + ", "
                             + "getdate())";
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE " + TABLENAME + "SET "
                            + ACACALID + " = " + ACACALID_PA + ","
                            + PROGID + " = " + PROGID_PA + ","
                            + BILLSTARTFROMRETAKENO + " = " + BILLSTARTFROMRETAKENO_PA + ","
                            + ISCREDITCOURSE + " = " + ISCREDITCOURSE_PA + ","
                            + COURSEID + " = " + COURSEID_PA + ","
                            + VERSIONID + " = " + VERSIONID_PA + ","
                            + MODIFIERID + " = " + MODIFIERID_PA + ","
                            + MOIDFIEDDATE + " = getdate()";
        #endregion

        #region DELETE
        private const string DELETE = "DELETE FROM " + TABLENAME;
        #endregion

        #region private Methods
        private static BillableCourseEntity Mapper(SQLNullHandler nullHandler)
        {
            BillableCourseEntity sd = new BillableCourseEntity();

            sd.Id = nullHandler.GetInt32(BILLABLECOURSEID);
            sd.AcaCalID = nullHandler.GetInt32(ACACALID);
            sd.ProgramID = nullHandler.GetInt32(PROGID);
            sd.BillStartFromRetakeNo = nullHandler.GetInt32(BILLSTARTFROMRETAKENO);
            sd.IsCreditCourse = nullHandler.GetBoolean(ISCREDITCOURSE);
            sd.CreatorID = nullHandler.GetInt32(CREATORID);
            sd.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            sd.ModifierID = nullHandler.GetInt32(MODIFIERID);
            sd.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return sd;
        }
        private static List<BillableCourseEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<BillableCourseEntity> sds = null;

            while (theReader.Read())
            {
                if (sds == null)
                {
                    sds = new List<BillableCourseEntity>();
                }
                BillableCourseEntity sd = Mapper(nullHandler);
                sds.Add(sd);
            }

            return sds;
        }
        private static BillableCourseEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            BillableCourseEntity sd = null;
            if (theReader.Read())
            {
                sd = new BillableCourseEntity();
                sd = Mapper(nullHandler);
            }
            return sd;
        }
        private static List<SqlParameter> MakeSqlParameterList(BillableCourseEntity sde, bool IsInsert)
        {
            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(ACACALID_PA, sde.AcaCalID);
            dps.AddParameter(PROGID_PA, sde.ProgramID);
            dps.AddParameter(BILLSTARTFROMRETAKENO_PA, sde.BillStartFromRetakeNo);
            dps.AddParameter(ISCREDITCOURSE_PA, sde.IsCreditCourse);
            dps.AddParameter(COURSEID_PA, sde.CourseID);
            dps.AddParameter(VERSIONID_PA, sde.VersionID);
            if (IsInsert)
            {
                dps.AddParameter(CREATORID_PA, sde.CreatorID);
            }
            else
            {
                dps.AddParameter(MODIFIERID_PA, sde.ModifierID);
            }

            List<SqlParameter> ps = Common.Methods.GetSQLParametersWithZero(dps);
            return ps;
        }
        #endregion



        #region Public Methods
        public static int Update(List<BillableCourseEntity> dcses)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                foreach (BillableCourseEntity sd in dcses)
                {
                    string cmd = UPDATE + " Where BillableCourseID = " + sd.Id;
                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(sd, false));
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int Update(BillableCourseEntity billableCourse)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();


                string cmd = UPDATE + " Where " + COURSEID + " = " + billableCourse.CourseID
                             + " AND "
                             + VERSIONID + " = " + billableCourse.VersionID;
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters = MakeSqlParameterList(billableCourse, false);
                counter = QueryHandler.ExecuteSelectBatchAction(cmd, parameters);


                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int Save(List<BillableCourseEntity> dcses)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                string cmd = INSERT;

                foreach (BillableCourseEntity sd in dcses)
                {
                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(sd, true));
                }

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int Save(BillableCourseEntity billableCourse)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                string cmd = INSERT;

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters = MakeSqlParameterList(billableCourse, true);
                counter = QueryHandler.ExecuteSelectBatchAction(cmd, parameters);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }


        public static List<BillableCourseEntity> GetDiscounts(int acaID, int progID)
        {
            try
            {
                List<BillableCourseEntity> sdes = null;

                string cmd = SELECT
                            + " Where "
                            + ACACALID + " = " + ACACALID_PA + " and "
                            + progID + " = " + PROGID_PA;
                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ACACALID_PA, acaID);
                dps.AddParameter(PROGID_PA, progID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader rd = QueryHandler.ExecuteSelectQuery(cmd, ps);
                sdes = Maps(rd);
                rd.Close();
                return sdes;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }
        public static List<BillableCourseEntity> GetAllBillableCourseInfo()
        {
            List<BillableCourseEntity> entities = new List<BillableCourseEntity>();
            try
            {
                string command = SELECT_VIEW;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(command, sqlConnection);
                while(dataReader.Read())
                {
                    BillableCourseEntity entity = new BillableCourseEntity();
                    entity.FormalCode = dataReader[FORMAL_CODE].ToString();
                    entity.Title = dataReader[COURSE_TITLE].ToString();
                    entity.CourseID = Convert.ToInt32((dataReader[COURSEID].ToString()));
                    entity.VersionID = Convert.ToInt32((dataReader[VERSIONID].ToString()));
                    entity.Credits = Convert.ToDecimal(dataReader[CREDITS].ToString());
                    entity.IsCreditCourse = Convert.ToBoolean(dataReader[ISCREDITCOURSE]);
                    entities.Add(entity);
                }
                //entities = Maps(dataReader);
                return entities;
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }
        public static List<BillableCourseEntity> GetAllCoursesByCourseCode(string searchText)
        {
            List<BillableCourseEntity> entities = new List<BillableCourseEntity>();
            try
            {
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                string sqlQuery = SELECT_VIEW + "where "
                                + FORMAL_CODE + " = " + searchText;
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(sqlQuery, connection);
                entities = Maps(dataReader);
                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return entities;
        }
        //public static List<BillableCourseEntity> GetAllBillableCourseInfo(string billableCourseCode)
        //{
        //    List<BillableCourseEntity> entities = new List<BillableCourseEntity>();
        //    try
        //    {
        //        SqlConnection connection = MSSqlConnectionHandler.GetConnection();
        //        //TO DO
        //        return entities;
        //    }
        //    catch (Exception exception)
        //    {

        //        throw exception;
        //    }
        //}
        

        #endregion



        public static BillableCourseEntity GetCourseByCourseIdVersionId(int courseID, int versionId)
        {
            try
            {
                BillableCourseEntity entity = new BillableCourseEntity();

                string sqlQuery = SELECT
                                +" WHERE " 
                                + COURSEID + " = " + courseID
                                +" AND "
                                + VERSIONID + " = " + versionId;
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(sqlQuery, connection);
                entity = Map(dataReader);
                dataReader.Close();
                return entity;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
