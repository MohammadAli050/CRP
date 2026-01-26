using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;

namespace BussinessObject
{
    [Serializable]
    public class CourseACUSpanMas : Base
    {
        #region DBCOlumns
        //CourseACUSpanMasID	int	        Unchecked
        //CourseID	            int	        Unchecked
        //VersionID	            int	        Unchecked
        //MaxACUNo	            int	        Checked
        //MinACUNo	            int	        Checked
        //CreatedBy	            int	        Unchecked
        //CreatedDate	        datetime	Unchecked
        //ModifiedBy	        int	        Checked
        //ModifiedDate	        datetime	Checked
        #endregion

        #region Variables
        private int _courseID;
        private int _versionID;
        private Course _course;

        private int _maxACUNo;
        private int _minACUNo;

        private List<CourseACUSpanDtl> _courseACUSpanDetails = null;


        #endregion

        #region Construcotr
        public CourseACUSpanMas()
            : base()
        {
            _courseID = 0;
            _versionID = 0;
            _course = null;
            _maxACUNo = 0;
            _minACUNo = 0;
            _courseACUSpanDetails = null;
        } 
        #endregion

        #region Constants
        #region Column Constants
        private const string COURSEACUSPANMASID = "CourseACUSpanMasID";//0

        private const string COURSE_ID = "CourseID";//1
        private const string COURSE_ID_PA = "@CourseID";//1

        private const string VERSION_ID = "VersionID";//2
        private const string VERSION_ID_PA = "@VersionID";//2

        private const string MAXACUNO = "MaxACUNo";//3
        private const string MAXACUNO_PA = "@MaxACUNo";//3

        private const string MINACUNO = "MinACUNo";//4
        private const string MINACUNO_PA = "@MinACUNo";//4
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + COURSEACUSPANMASID + "], "//0
                                        + "[" + COURSE_ID + "], "//1
                                        + "[" + VERSION_ID + "], "//2
                                        + "[" + MAXACUNO + "], "//3
                                        + "[" + MINACUNO + "], ";//4
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + COURSE_ID + "], "//0
                                        + "[" + VERSION_ID + "], "//1
                                        + "[" + MAXACUNO + "], "//2
                                        + "[" + MINACUNO + "], ";//3
        #endregion

        private const string TABLENAME = " [CourseACUSpanMas] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
                     + ID_PA + ", "//0
                     + COURSE_ID_PA + ", "//1
                     + VERSION_ID_PA + ", "//2
                     + MAXACUNO_PA + ", "//3
                     + MINACUNO_PA + ", "//4
                     + CREATORID_PA + ", "//5
                     + CREATEDDATE_PA + ", "//6
                     + MODIFIERID_PA + ", "//7
                     + MOIDFIEDDATE_PA + ")";//8 
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + COURSE_ID + "] = " + COURSE_ID_PA + ", "//1
                    + "[" + VERSION_ID + "] = " + VERSION_ID_PA + ", "//2
                    + "[" + MAXACUNO + "] = " + MAXACUNO_PA + ", "//3
                    + "[" + MINACUNO + "] = " + MINACUNO_PA + ", "//4
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "//5
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//6
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "//7
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//8
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Preperties
        #region Course
        /// <summary>
        /// Associated Course ID
        /// </summary>
        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }
        private SqlParameter CourseIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = COURSE_ID_PA;
                if (CourseID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = CourseID;
                }
                return sqlParam;
            }
        }
        /// <summary>
        /// Associated Course Version Id
        /// </summary>
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }
        private SqlParameter VersionIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = VERSION_ID_PA;
                if (VersionID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = VersionID;
                }
                return sqlParam;
            }
        }
        /// <summary>
        /// Associated Course
        /// </summary>
        public Course OwnerCourse
        {
            get
            {
                if (_course == null)
                {
                    if ((_courseID > 0) && (_versionID > 0))
                    {
                        _course = Course.GetCourse(_courseID, _versionID);
                    }
                }
                return _course;
            }
        }
        #endregion

        public int MaxACUNo
        {
            get { return _maxACUNo; }
            set { _maxACUNo = value; }
        }
        private SqlParameter MaxACUNoParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = MAXACUNO_PA;

                param.Value = MaxACUNo;

                return param;
            }
        }

        public int MinACUNo
        {
            get { return _minACUNo; }
            set { _minACUNo = value; }
        }
        private SqlParameter MinACUNoParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = MINACUNO_PA;
                param.Value = MinACUNo;
                return param;
            }
        }


        public List<CourseACUSpanDtl> CourseACUSpanDetails
        {
            get 
            {
                if (_courseACUSpanDetails == null)
                {
                    _courseACUSpanDetails = CourseACUSpanDtl.GetsByMas(Id);
                }
                return _courseACUSpanDetails; 
            }
            set { _courseACUSpanDetails = value; }
        }
        #endregion

        #region Methods
        private static CourseACUSpanMas Mapper(SQLNullHandler nullHandler)
        {
            CourseACUSpanMas obj = new CourseACUSpanMas();

            obj.Id = nullHandler.GetInt32(COURSEACUSPANMASID);//0
            obj.CourseID = nullHandler.GetInt32(COURSE_ID);//1
            obj.VersionID = nullHandler.GetInt32(VERSION_ID);//2
            obj.MaxACUNo = nullHandler.GetInt32(MAXACUNO);//3
            obj.MinACUNo = nullHandler.GetInt32(MINACUNO);//4
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8

            return obj;
        }
        private static CourseACUSpanMas MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CourseACUSpanMas obj = null;
            if (theReader.Read())
            {
                obj = new CourseACUSpanMas();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<CourseACUSpanMas> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CourseACUSpanMas> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<CourseACUSpanMas>();
                }
                CourseACUSpanMas obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static CourseACUSpanMas Get(int id)
        {
            string command = SELECT
                            + "WHERE [" + COURSEACUSPANMASID + "] = " + ID_PA;

            SqlParameter ciDParam = MSSqlConnectionHandler.MSSqlParamGenerator(id, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ciDParam });

            CourseACUSpanMas obj = MapClass(theReader);
            theReader.Close();

            obj.CourseACUSpanDetails = CourseACUSpanDtl.GetsByMas(obj.Id, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static List<CourseACUSpanMas> Gets()
        {
            string command = SELECT;


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<CourseACUSpanMas> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static CourseACUSpanMas GetByCourse(int courseID, int versionID)
        {
            string command = SELECT
                            + "WHERE [" + COURSE_ID + "] = " + COURSE_ID_PA + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA;

            SqlParameter ciDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
            SqlParameter viDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ciDParam, viDParam });

            CourseACUSpanMas obj = MapClass(theReader);
            theReader.Close();

            if (obj != null)
            {
                obj.CourseACUSpanDetails = CourseACUSpanDtl.GetsByMas(obj.Id, sqlConn);
            }

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        internal static CourseACUSpanMas GetByCourse(int courseID, int versionID, bool loadDetails, SqlConnection sqlConn)
        {
            string command = SELECT
                            + "WHERE [" + COURSE_ID + "] = " + COURSE_ID_PA + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA;

            SqlParameter ciDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
            SqlParameter viDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);


            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { ciDParam, viDParam });

            CourseACUSpanMas obj = MapClass(theReader);
            theReader.Close();

            if (loadDetails)
            {
                obj.CourseACUSpanDetails = CourseACUSpanDtl.GetsByMas(obj.Id, sqlConn);
            }

            //MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        internal static CourseACUSpanMas GetByCourse(int courseID, int versionID, bool loadDetails, SqlConnection sqlConn,SqlTransaction sqlTran)
        {
            string command = SELECT
                            + "WHERE [" + COURSE_ID + "] = " + COURSE_ID_PA + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA;

            SqlParameter ciDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
            SqlParameter viDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);


            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command, sqlConn, sqlTran, new SqlParameter[] { ciDParam, viDParam });

            CourseACUSpanMas obj = MapClass(theReader);
            theReader.Close();

            if (loadDetails)
            {
                obj.CourseACUSpanDetails = CourseACUSpanDtl.GetsByMas(obj.Id, sqlConn);
            }

            //MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX(" + COURSEACUSPANMASID + ") FROM " + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newID = 1;
            }
            else if (ob is Int32)
            {
                newID = Convert.ToInt32(ob) + 1;
            }

            return newID;
        }

        public static int Save(CourseACUSpanMas courseACUSpanMas)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (courseACUSpanMas.Id == 0)
                {
                    courseACUSpanMas.Id = CourseACUSpanDtl.GetMaxID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { courseACUSpanMas.IDParam,  
                                                     courseACUSpanMas.CourseIDParam,
                                                     courseACUSpanMas.VersionIDParam,
                                                     courseACUSpanMas.MaxACUNoParam,
                                                     courseACUSpanMas.MinACUNoParam,
                                                     courseACUSpanMas.CreatorIDParam, 
                                                     courseACUSpanMas.CreatedDateParam, 
                                                     courseACUSpanMas.ModifierIDParam, 
                                                     courseACUSpanMas.ModifiedDateParam };
                }
                else
                {


                    command = UPDATE
                            + " WHERE [" + COURSEACUSPANMASID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { courseACUSpanMas.CourseIDParam,
                                                     courseACUSpanMas.VersionIDParam,
                                                     courseACUSpanMas.MaxACUNoParam,
                                                     courseACUSpanMas.MinACUNoParam,
                                                     courseACUSpanMas.CreatorIDParam, //7
                                                     courseACUSpanMas.CreatedDateParam, //8
                                                     courseACUSpanMas.ModifierIDParam, //9
                                                     courseACUSpanMas.ModifiedDateParam, //10
                                                     courseACUSpanMas.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                CourseACUSpanDtl.DeleteByMaster(courseACUSpanMas.Id, sqlConn, sqlTran);

                if (courseACUSpanMas.CourseACUSpanDetails != null && courseACUSpanMas.CourseACUSpanDetails.Count > 0)
                {
                    foreach (CourseACUSpanDtl item in courseACUSpanMas.CourseACUSpanDetails)
                    {
                        item.CourseACUSpanMasID = courseACUSpanMas.Id;
                        CourseACUSpanDtl.Save(item, sqlConn, sqlTran);

                    }
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
        internal static int Save(CourseACUSpanMas courseACUSpanMas, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                //SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;
                courseACUSpanMas.Id = CourseACUSpanMas.GetMaxID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { courseACUSpanMas.IDParam,  
                                                     courseACUSpanMas.CourseIDParam,
                                                     courseACUSpanMas.VersionIDParam,
                                                     courseACUSpanMas.MaxACUNoParam,
                                                     courseACUSpanMas.MinACUNoParam,
                                                     courseACUSpanMas.CreatorIDParam, 
                                                     courseACUSpanMas.CreatedDateParam, 
                                                     courseACUSpanMas.ModifierIDParam, 
                                                     courseACUSpanMas.ModifiedDateParam };

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                CourseACUSpanDtl.DeleteByMaster(courseACUSpanMas.Id, sqlConn, sqlTran);

                if (courseACUSpanMas.CourseACUSpanDetails != null && courseACUSpanMas.CourseACUSpanDetails.Count > 0)
                {
                    foreach (CourseACUSpanDtl item in courseACUSpanMas.CourseACUSpanDetails)
                    {
                        item.CourseACUSpanMasID = courseACUSpanMas.Id;
                        CourseACUSpanDtl.Save(item, sqlConn, sqlTran);

                    }
                }

                //MSSqlConnectionHandler.CommitTransaction();
                //MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int Delete(int courseACUSpanMasID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                CourseACUSpanDtl.DeleteByMaster(courseACUSpanMasID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE [" + COURSEACUSPANMASID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanMasID, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });


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
        internal static int Delete(int courseACUSpanMasID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                CourseACUSpanDtl.DeleteByMaster(courseACUSpanMasID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE [" + COURSEACUSPANMASID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseACUSpanMasID, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int Delete(int courseID, int versionID, int masterId,SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                //CourseACUSpanMas master = CourseACUSpanMas.GetByCourse(courseID,versionID, false, sqlConn);
                CourseACUSpanDtl.DeleteByMaster(masterId, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE [" + COURSEACUSPANMASID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(masterId, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        internal static int Delete(int courseID, int versionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                CourseACUSpanMas master = CourseACUSpanMas.GetByCourse(courseID,versionID, false, sqlConn,sqlTran);
                if (master != null)
                {
                    CourseACUSpanDtl.DeleteByMaster(master.Id, sqlConn, sqlTran);

                    string command = DELETE
                                    + "WHERE [" + COURSEACUSPANMASID + "] = " + ID_PA;

                    SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(master.Id, ID_PA);
                    counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });
                }

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        #endregion
    }

}
