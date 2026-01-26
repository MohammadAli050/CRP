using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Course_DAO : Base_DAO
    {
        #region Constants
        #region Course Columns
        private const string COURSEID = "CourseID";//0
        private const string COURSEID_PA = "@CourseID";//0
        private const string VERSIONID = "VersionID";//1
        private const string VERSIONID_PA = "@VersionID";

        private const string FORMALCODE = "FormalCode";//2
        private const string FORMALCODE_PA = "@FormalCode";
        private const string VERSIONCODE = "VersionCode";//3
        private const string VERSIONCODE_PA = "@VersionCode";

        private const string TITLE = "Title";//4
        private const string TITLE_PA = "@Title";

        private const string ASSOCCOURSEID = "AssocCourseID";//5
        private const string ASSOCCOURSEID_PA = "@AssocCourseID";
        private const string ASSOCVERSIONID = "AssocVersionID";//6
        private const string ASSOCVERSIONID_PA = "@AssocVersionID";

        private const string STARTACADEMICCALENDERID = "StartAcademicCalenderID";//7
        private const string STARTACADEMICCALENDERID_PA = "@StartAcademicCalenderID";

        private const string PROGRAMID = "ProgramID";//8
        private const string PROGRAMID_PA = "@ProgramID";

        private const string COURSECONTENT = "CourseContent";//9
        private const string COURSECONTENT_PA = "@CourseContent";

        private const string ISCREDITCOURSE = "IsCreditCourse";//10
        private const string ISCREDITCOURSE_PA = "@IsCreditCourse";

        private const string CREDITS = "Credits";//11
        private const string CREDITS_PA = "@Credits";

        private const string ISSECTIONMANDATORY = "IsSectionMandatory";//12
        private const string ISSECTIONMANDATORY_PA = "@IsSectionMandatory";

        private const string HASEQUIVALENTS = "HasEquivalents";//13
        private const string HASEQUIVALENTS_PA = "@HasEquivalents";

        private const string HASMULTIPLEACUSPAN = "HasMultipleACUSpan";//14
        private const string HASMULTIPLEACUSPAN_PA = "@HasMultipleACUSpan";

        private const string ISACTIVE = "IsActive";//15
        private const string ISACTIVE_PA = "@IsActive";
        #endregion

        
		#region All Columns
        private const string ALLCOLUMNS = "[" + COURSEID + "], "//0
                                + "[" + VERSIONID + "], "//1
                                + "[" + FORMALCODE + "], "//2
                                + "[" + VERSIONCODE + "], "//3
                                + "[" + TITLE + "], "//4
                                + "[" + ASSOCCOURSEID + "], "//5
                                + "[" + ASSOCVERSIONID + "], "//6
                                + "[" + STARTACADEMICCALENDERID + "], "//7
                                + "[" + PROGRAMID + "], "//8
                                + "[" + COURSECONTENT + "], "//9
                                + "[" + ISCREDITCOURSE + "], "//10
                                + "[" + CREDITS + "], "//11
                                + "[" + ISSECTIONMANDATORY + "], "//12
                                + "[" + HASEQUIVALENTS + "], "//13
                                + "[" + HASMULTIPLEACUSPAN + "], "//14
                                + "[" + ISACTIVE + "], ";//15 
        #endregion

        #region No PK Columns
        private const string NOPKCOLUMNS = "[" + FORMALCODE + "], "//2
                                + "[" + VERSIONCODE + "], "//3
                                + "[" + TITLE + "], "//4
                                + "[" + ASSOCCOURSEID + "], "//5
                                + "[" + ASSOCVERSIONID + "], "//6
                                + "[" + STARTACADEMICCALENDERID + "], "//7
                                + "[" + PROGRAMID + "], "//8
                                + "[" + COURSECONTENT + "], "//9
                                + "[" + ISCREDITCOURSE + "], "//10
                                + "[" + CREDITS + "], "//11
                                + "[" + ISSECTIONMANDATORY + "], "//12
                                + "[" + HASEQUIVALENTS + "], "//13
                                + "[" + HASMULTIPLEACUSPAN + "], "//14
                                + "[" + ISACTIVE + "], ";//15 
        #endregion

        private const string TABLENAME = " [Course] ";
        private const string VIEWANME = " VIEW_NOT_SET_BILLABLE_COURSES ";

        #region Select
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        private const string SELECT_VIEW = "SELECT "
                                        + ALLCOLUMNS
                                        + "FROM" + VIEWANME;
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS
                     + ")"
                     + "VALUES ( "
                     + ID_PA + ", "//0
                     + VERSIONID_PA + ", "//1
                     + FORMALCODE_PA + ", "//2
                     + VERSIONCODE_PA + ", "//3
                     + TITLE_PA + ", "//4
                     + ASSOCCOURSEID_PA + ", "//5
                     + ASSOCVERSIONID_PA + ", "//6
                     + STARTACADEMICCALENDERID_PA + ", "//7
                     + PROGRAMID_PA + ", "//8
                     + COURSECONTENT_PA + ", "//9
                     + ISCREDITCOURSE_PA + ", "//10
                     + CREDITS_PA + ", "//11
                     + ISSECTIONMANDATORY_PA + ", "//12
                     + HASEQUIVALENTS_PA + ", "//13
                     + HASMULTIPLEACUSPAN_PA + ", "//14
                     + ISACTIVE_PA + ", "//15
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
                + "SET [" + FORMALCODE + "] = " + FORMALCODE_PA + ", "
                + "[" + VERSIONCODE + "] = " + VERSIONCODE_PA + ", "
                + "[" + TITLE + "] = " + TITLE_PA + ", "
                + "[" + ASSOCCOURSEID + "] = " + ASSOCCOURSEID_PA + ", "//5
                + "[" + ASSOCVERSIONID + "] = " + ASSOCVERSIONID_PA + ", "//6
                + "[" + STARTACADEMICCALENDERID + "] = " + STARTACADEMICCALENDERID_PA + ", "//7
                + "[" + PROGRAMID + "] = " + PROGRAMID_PA + ", "//8
                + "[" + COURSECONTENT + "] = " + COURSECONTENT_PA + ", "//9
                + "[" + ISCREDITCOURSE + "] = " + ISCREDITCOURSE_PA + ", "//10
                + "[" + CREDITS + "] = " + CREDITS_PA + ", "//11
                + "[" + ISSECTIONMANDATORY + "] = " + ISSECTIONMANDATORY_PA + ", "//12
                + "[" + HASEQUIVALENTS + "] = " + HASEQUIVALENTS_PA + ", "//13
                + "[" + HASMULTIPLEACUSPAN + "] = " + HASMULTIPLEACUSPAN_PA + ", "//14
                + "[" + ISACTIVE + "] = " + ISACTIVE_PA + ", "//15
                + "[" + CREATORID + "] = " + CREATORID_PA + ","//16
                + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ","//17
                + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ","//18
                + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//19 
        #endregion

        private const string DELETE = "DELETE" + TABLENAME;
        #endregion
        #region Methods

        #region Private Functions
        /// <summary>
        /// Map a null handled data reader to a course object
        /// </summary>
        /// <param name="nullHandler">It is a null handled data reader</param>
        /// <returns>Course</returns>
        private static CourseEntity Mapper(SQLNullHandler nullHandler)
        {
            CourseEntity course = new CourseEntity();
            course.Id = nullHandler.GetInt32(COURSEID);
            course.VersionID = nullHandler.GetInt32(VERSIONID);
            course.FormalCode = nullHandler.GetString(FORMALCODE);
            course.VersionCode = nullHandler.GetString(VERSIONCODE);
            course.Title = nullHandler.GetString(TITLE);
            course.AssocCourseID = nullHandler.GetInt32(ASSOCCOURSEID);
            course.AssocVersionID = nullHandler.GetInt32(ASSOCVERSIONID);
            course.StartACUID = nullHandler.GetInt32(STARTACADEMICCALENDERID);
            course.OwnerProgID = nullHandler.GetInt32(PROGRAMID);
            course.CourseContent = nullHandler.GetString(COURSECONTENT);
            course.IsCredit = nullHandler.GetBoolean(ISCREDITCOURSE);
            course.Credits = nullHandler.GetDecimal(CREDITS);
            course.IsSectionMandatory = nullHandler.GetBoolean(ISSECTIONMANDATORY);
            course.HasEquivalents = nullHandler.GetBoolean(HASEQUIVALENTS);
            course.HasMultipleACUSpan = nullHandler.GetBoolean(HASMULTIPLEACUSPAN);
            course.IsActive = nullHandler.GetBoolean(ISACTIVE);
            course.CreatorID = nullHandler.GetInt32(CREATORID);
            course.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            course.ModifierID = nullHandler.GetInt32(MODIFIERID);
            course.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return course;
        }

        /// <summary>
        /// Link a datareader containing a collection of courses to a nullhandler and then call the mapper
        /// </summary>
        /// <param name="theReader">Raw datareader</param>
        /// <returns>Collection of course</returns>
        private static List<CourseEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CourseEntity> courses = null;

            while (theReader.Read())
            {
                if (courses == null)
                {
                    courses = new List<CourseEntity>();
                }
                CourseEntity course = Mapper(nullHandler);
                courses.Add(course);
            }
            return courses;
        }

        /// <summary>
        /// Link a datareader containing a course to a nullhandler and then call the mapper
        /// </summary>
        /// <param name="theReader">Raw datareader</param>
        /// <returns>Course</returns>
        private static CourseEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CourseEntity course = null;
            if (theReader.Read())
            {
                course = Mapper(nullHandler);
            }
            return course;
        }
        #endregion

        internal static List<CourseEntity> GetCoursesByNode(int nodeId)
        {
            string cmd = "DECLARE	@return_value int EXEC @return_value = [dbo].[sp_GetCoursesUnderNode] @nodeID = @ndID SELECT 'Return Value' = @return_value";

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@ndID", nodeId);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);

            List<CourseEntity> courses = Maps(theReader);
            theReader.Close();
            return courses;
        }
        public static List<CourseEntity> GetAllCourses()
        {
            try
            {
                string strQuery = SELECT;
                List<CourseEntity> courses = new List<CourseEntity>();
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(strQuery, connection);

                courses = Maps(dataReader);
                dataReader.Close();
                return courses;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        internal static CourseEntity GetCourse(int courseID, int versionID)
        {
            string cmd = SELECT + " where " + COURSEID + " = " + COURSEID_PA + " and " + VERSIONID + " = " + VERSIONID_PA;

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(COURSEID_PA, courseID);
            dps.AddParameter(VERSIONID_PA, versionID);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);

            CourseEntity course = Map(theReader);
            theReader.Close();
            return course;
        }
        internal static CourseEntity GetCourseByNodeCourse(int nodeCourseID)
        {
            string cmd = "DECLARE @return_value int EXEC @return_value = [dbo].[sp_GetCourseByNodeCourse] @nodeCourseID = @ndcsID";

            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter("@ndcsID", nodeCourseID);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlDataReader theReader = QueryHandler.ExecuteSelectQuery(cmd, ps);

            CourseEntity course = Map(theReader);
            theReader.Close();
            return course;
        }

        internal static List<CourseEntity> GetAllNonCreditCoursesByProgram(int programID)
        {
            List<CourseEntity> courses = new List<CourseEntity>();
            try
            {
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                string sqlQuery = SELECT + "where isCreditCourse  = 0 AND "
                                + PROGRAMID + " = " + programID;
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(sqlQuery, connection);
                courses = Maps(dataReader);
                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return courses;
        }

        public static List<CourseEntity> GetAllCoursesByCourseCode(string searchText)
        {
            List<CourseEntity> courses = new List<CourseEntity>();
            try
            {
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                string sqlQuery = SELECT + "where "
                                + FORMALCODE + " = " + searchText;
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(sqlQuery, connection);
                courses = Maps(dataReader);
                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return courses;
        }

        public static CourseEntity GetCoursebyCourseID(int courseID)
        {
            CourseEntity course = new CourseEntity();
            try
            {
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                string sqlQuery = SELECT + "where "
                                + COURSEID + " = " + courseID;
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(sqlQuery, connection);
                course = Map(dataReader);
                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return course;
        }
        public static CourseEntity GetCourseByCourseIDVersionID(int courseId, int versionId)
        {
            try
            {
                CourseEntity entity = new CourseEntity();

                string sqlQuery = SELECT + "where "
                                + COURSEID + " = " + courseId + " AND "
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

        #endregion




    }
}
