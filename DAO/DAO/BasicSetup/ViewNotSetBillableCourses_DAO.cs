using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class ViewNotSetBillableCourses_DAO//:Base_DAO
    {
        #region Constants
        private const string FORMALCODE = "FormalCode";//0
        private const string TITLE = "Title";//1
        private const string COURSEID = "CourseId";
        private const string VERSIONID = "VersionId";
        private const string CREDITS = "Credits";
        private const string ISCREDITCOURSE = "IsCreditCourse";
        #endregion

        #region COLUMNS
        private const string ALLCOLUMNS = FORMALCODE + ","
                                         + TITLE + ","
                                         + COURSEID + ","
                                         + VERSIONID + ","
                                         + CREDITS + ","
                                         + ISCREDITCOURSE ;
        #endregion

        private const string VIEWNAME = "VIEW_NOT_SET_BILLABLE_COURSES";
        #region SELECT
        private const string SELECT = "SELECT "
                                 + ALLCOLUMNS
                                 + " FROM "
                                 + VIEWNAME;
        #endregion

        #region Methods
        #region Private Methods
        private static ViewNotSetBillableCoursesEntity Mapper(SQLNullHandler nullHandler)
        {
            try
            {
                ViewNotSetBillableCoursesEntity entity = new ViewNotSetBillableCoursesEntity();

                entity.Formalcode = nullHandler.GetString(FORMALCODE);
                entity.Title = nullHandler.GetString(TITLE);
                entity.Courseid = nullHandler.GetInt32(COURSEID);
                entity.Versionid = nullHandler.GetInt32(VERSIONID);
                entity.Credits = nullHandler.GetInt32(CREDITS);
                entity.Iscreditcourse = nullHandler.GetBoolean(ISCREDITCOURSE);

                return entity;
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }//end of method Mapper()
        private static List<ViewNotSetBillableCoursesEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<ViewNotSetBillableCoursesEntity> entities = null;
            while (theReader.Read())
            {
                if (entities == null)
                {
                    entities = new List<ViewNotSetBillableCoursesEntity>();
                }
                ViewNotSetBillableCoursesEntity entity = Mapper(nullHandler);
                entities.Add(entity);
            }

            return entities;
        }//end of method Map()
        private static ViewNotSetBillableCoursesEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            ViewNotSetBillableCoursesEntity entity = null;
            if (theReader.Read())
            {
                entity = new ViewNotSetBillableCoursesEntity();
                entity = Mapper(nullHandler);
            }

            return entity;
        }//end of method Map()
        private static List<SqlParameter> MakeSqlParameterList(ViewNotSetBillableCoursesEntity viewNotSetBillableCoursesEntity)
        {
            DAOParameters dps = new DAOParameters();

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }
        #endregion

        #region Public Method(s)
        public static List<ViewNotSetBillableCoursesEntity> GetAllNotSetBillableCources()
        {
            try
            {
                List<ViewNotSetBillableCoursesEntity> entities = new List<ViewNotSetBillableCoursesEntity>();
                string command = SELECT;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(command, sqlConnection);
                
                //entities = Maps(dataReader);
                while(dataReader.Read())
                {
                    ViewNotSetBillableCoursesEntity entity = new ViewNotSetBillableCoursesEntity();
                    entity.Formalcode = dataReader[FORMALCODE].ToString();
                    entity.Title = dataReader[TITLE].ToString();
                    entity.Courseid = Convert.ToInt32((dataReader[COURSEID].ToString()));
                    entity.Versionid = Convert.ToInt32((dataReader[VERSIONID].ToString()));
                    entity.Credits = Convert.ToDecimal(dataReader[CREDITS].ToString());
                    entity.Iscreditcourse = Convert.ToBoolean(dataReader[ISCREDITCOURSE]);

                    entities.Add(entity);
                }
                //dataReader.Close();
                return entities;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        #endregion
        #endregion
    }

}//End Of Namespace DataAccess
