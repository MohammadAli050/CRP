using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class CourseWavTransfrDt : Base
    {
        #region Variables

        private int _CourseWavTransfrMasterID;
        private int _OwnerNodeCourseID;
        private string _AgainstCourseInfo;

        #endregion

        #region Constants
        #region Column Constants
        private const string COURSEWAVTRANSFRDETAILID = "CourseWavTransfrDetailID";

        private const string COURSEWAVTRANSFRMASTERID = "CourseWavTransfrMasterID";
        private const string COURSEWAVTRANSFRMASTERID_PA = "@CourseWavTransfrMasterID";

        private const string OWNERNODECOURSEID = "OwnerNodeCourseID";
        private const string OWNERNODECOURSEID_PA = "@OwnerNodeCourseID";

        private const string AGAINSTCOURSEINFO = "AgainstCourseInfo";
        private const string AGAINSTCOURSEINFO_PA = "@AgainstCourseInfo";
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + COURSEWAVTRANSFRDETAILID + "], "
                                        + "[" + COURSEWAVTRANSFRMASTERID + "], "
                                        + "[" + OWNERNODECOURSEID + "], "
                                        + "[" + AGAINSTCOURSEINFO + "], ";
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + COURSEWAVTRANSFRMASTERID + "], "
                                        + "[" + OWNERNODECOURSEID + "], "
                                        + "[" + AGAINSTCOURSEINFO + "], ";
        #endregion

        private const string TABLENAME = " [CourseWavTransfrDetail] ";

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
                     + COURSEWAVTRANSFRMASTERID_PA + ", "
                     + OWNERNODECOURSEID_PA + ", "
                     + AGAINSTCOURSEINFO_PA + ", "
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + COURSEWAVTRANSFRMASTERID + "] = " + COURSEWAVTRANSFRMASTERID_PA + ", "
                    + "[" + OWNERNODECOURSEID + "] = " + OWNERNODECOURSEID_PA + ", "
                    + "[" + AGAINSTCOURSEINFO + "] = " + AGAINSTCOURSEINFO_PA + ", "
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Properties
        [Column(Name = "CourseWavTransfrMasterID")]
        public int CourseWavTransfrMasterID
        {
            get { return _CourseWavTransfrMasterID; }
            set { _CourseWavTransfrMasterID = value; }
        }
        private SqlParameter CourseWavTransfrMasterIDParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = COURSEWAVTRANSFRMASTERID_PA;

                param.Value = CourseWavTransfrMasterID;

                return param;
            }
        }

        [Column(Name = "OwnerNodeCourseID")]
        public int OwnerNodeCourseID
        {
            get { return _OwnerNodeCourseID; }
            set { _OwnerNodeCourseID = value; }
        }
        private SqlParameter OwnerNodeCourseIDParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = OWNERNODECOURSEID_PA;

                param.Value = OwnerNodeCourseID;

                return param;
            }
        }

        [Column(Name = "AgainstCourseInfo")]
        public string AgainstCourseInfo
        {
            get { return _AgainstCourseInfo; }
            set { _AgainstCourseInfo = value; }
        }
        private SqlParameter AgainstCourseInfoParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = AGAINSTCOURSEINFO_PA;
                if (AgainstCourseInfo == null)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = AgainstCourseInfo;
                }

                return param;
            }
        }
        #endregion

        #region Methods

        #region Class Mapper

        private static CourseWavTransfrDt Mapper(SQLNullHandler nullHandler)
        {
            CourseWavTransfrDt obj = new CourseWavTransfrDt();

            obj.Id = nullHandler.GetInt32(COURSEWAVTRANSFRDETAILID);
            obj.CourseWavTransfrMasterID = nullHandler.GetInt32(COURSEWAVTRANSFRMASTERID);
            obj.OwnerNodeCourseID = nullHandler.GetInt32(OWNERNODECOURSEID);
            obj.AgainstCourseInfo = nullHandler.GetString(AGAINSTCOURSEINFO);
            obj.CreatorID = nullHandler.GetInt32(CREATORID);
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return obj;
        }        

        private static CourseWavTransfrDt MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CourseWavTransfrDt obj = null;
            if (theReader.Read())
            {
                obj = new CourseWavTransfrDt();
                obj = Mapper(nullHandler);
            }
            return obj;
        }
        private static List<CourseWavTransfrDt> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CourseWavTransfrDt> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<CourseWavTransfrDt>();
                }
                CourseWavTransfrDt obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        #endregion

        #region Public Methods

        public static int Save(CourseWavTransfrDt detail)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (detail.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { detail.IDParam,  
                                                     detail.CourseWavTransfrMasterIDParam,
                                                     detail.OwnerNodeCourseIDParam,
                                                     detail.AgainstCourseInfoParam,
                                                     detail.CreatorIDParam, 
                                                     detail.CreatedDateParam, 
                                                     detail.ModifierIDParam, 
                                                     detail.ModifiedDateParam };
                }
                else
                {


                    command = UPDATE
                            + " WHERE [" + COURSEWAVTRANSFRDETAILID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { detail.CourseWavTransfrMasterIDParam,
                                                     detail.OwnerNodeCourseIDParam,
                                                     detail.AgainstCourseInfoParam,
                                                     detail.CreatorIDParam, 
                                                     detail.CreatedDateParam, 
                                                     detail.ModifierIDParam, 
                                                     detail.ModifiedDateParam, 
                                                     detail.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

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
        public static int Delete(int detailID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE [" + COURSEWAVTRANSFRDETAILID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(detailID, ID_PA);
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

        #endregion

        #region Internal Methods

        internal static int DeleteByMaster(int detailID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = DELETE
                                + "WHERE [" + COURSEWAVTRANSFRMASTERID + "] = " + COURSEWAVTRANSFRMASTERID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(detailID, COURSEWAVTRANSFRMASTERID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int Save(CourseWavTransfrDt detail, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                command = INSERT;
                
                sqlParams = new SqlParameter[] { detail.CourseWavTransfrMasterIDParam,
                                                 detail.OwnerNodeCourseIDParam,
                                                 detail.AgainstCourseInfoParam,
                                                 detail.CreatorIDParam, 
                                                 detail.CreatedDateParam, 
                                                 detail.ModifierIDParam, 
                                                 detail.ModifiedDateParam };
                

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}
