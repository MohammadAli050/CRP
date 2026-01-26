using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class CourseWaiverTransferMaster : Base
    {
        #region Variables

        private int _StudentID;
        private string _UniversityName;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private int _DivisionType;
        private List<CourseWavTransfrDt> _courseWavTransDetails = null;

        #endregion

        #region Constants
        #region Column Constants
        private const string COURSEWAVTRANSFRMASTERID = "CourseWavTransfrMasterID";

        private const string STUDENTID = "StudentID";
        private const string STUDENTID_PA = "@StudentID";

        private const string UNIVERSITYNAME = "UniversityName";
        private const string UNIVERSITYNAME_PA = "@UniversityName";

        private const string FROMDATE = "FromDate";
        private const string FROMDATE_PA = "@FromDate";

        private const string TODATE = "ToDate";
        private const string TODATE_PA = "@ToDate";

        private const string DIVISIONTYPE = "DivisionType";
        private const string DIVISIONTYPE_PA = "@DivisionType";

        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + COURSEWAVTRANSFRMASTERID + "], "
                                        + "[" + STUDENTID + "], "
                                        + "[" + UNIVERSITYNAME + "], "
                                        + "[" + FROMDATE + "], "
                                        + "[" + TODATE + "], "
                                        + "[" + DIVISIONTYPE + "], ";
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + STUDENTID + "], "
                                        + "[" + UNIVERSITYNAME + "], "
                                        + "[" + FROMDATE + "], "
                                        + "[" + TODATE + "], "
                                        + "[" + DIVISIONTYPE + "], ";
        #endregion

        private const string TABLENAME = " [CourseWavTransfrMaster] ";

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
                     + ID_PA + ", "
                     + STUDENTID_PA + ", "
                     + UNIVERSITYNAME_PA + ", "
                     + FROMDATE_PA + ", "
                     + TODATE_PA + ", "
                     + DIVISIONTYPE_PA + ", "
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + STUDENTID + "] = " + STUDENTID_PA + ", "
                    + "[" + UNIVERSITYNAME + "] = " + UNIVERSITYNAME_PA + ", "
                    + "[" + FROMDATE + "] = " + FROMDATE_PA + ", "
                    + "[" + TODATE + "] = " + TODATE_PA + ", "
                    + "[" + DIVISIONTYPE + "] = " + DIVISIONTYPE_PA + ", "
                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Properties

        public int StudentID
        {
            get
            {
                return _StudentID;
            }
            set
            {
                _StudentID = value;
            }
        }

        private SqlParameter StudentIDParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = STUDENTID_PA;

                param.Value = StudentID;

                return param;
            }
        }

        public string UniversityName
        {
            get
            {
                return _UniversityName;
            }
            set
            {
                _UniversityName = value;
            }
        }

        private SqlParameter UniversityNameParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = UNIVERSITYNAME_PA;
                if (UniversityName == null)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = UniversityName;
                }

                return param;
            }
        }

        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                _FromDate = value;
            }
        }

        private SqlParameter FromDateParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = FROMDATE_PA;

                if (FromDate == DateTime.MinValue)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = FromDate;
                }

                return param;
            }
        }

        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
            }
        }

        private SqlParameter ToDateParam
        {
            get
            {
                SqlParameter param = new SqlParameter();             
                param.ParameterName = TODATE_PA;

                if (ToDate == DateTime.MinValue)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = ToDate;
                }

                return param;
            }
        }

        public int DivisionType
        {
            get
            {
                return _DivisionType;
            }
            set
            {
                _DivisionType = value;
            }
        }

        private SqlParameter DivisionTypeParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = DIVISIONTYPE_PA;

                param.Value = DivisionType;

                return param;
            }
        }

        public List<CourseWavTransfrDt> CourseWavTransfrDetails
        {
            get
            {
                if (_courseWavTransDetails == null)
                {
                    _courseWavTransDetails = new List<CourseWavTransfrDt>();
                }
                return _courseWavTransDetails;
            }
            set { _courseWavTransDetails = value; }
        }

        #endregion        

        #region Construcotr
        public CourseWaiverTransferMaster()
            : base()
        {
            _StudentID = 0;
            _UniversityName = null;
            _FromDate = DateTime.MinValue;
            _ToDate = DateTime.MinValue;
            _DivisionType = 0;
            //_courseACUSpanDetails = null;
        } 
        #endregion

        #region Methods

        #region Class Map

        private static CourseWaiverTransferMaster Mapper(SQLNullHandler nullHandler)
        {
            CourseWaiverTransferMaster obj = new CourseWaiverTransferMaster();

            obj.Id = nullHandler.GetInt32(COURSEWAVTRANSFRMASTERID);
            obj.StudentID = nullHandler.GetInt32(STUDENTID);
            obj.UniversityName = nullHandler.GetString(UNIVERSITYNAME);
            obj.FromDate = nullHandler.GetDateTime(FROMDATE);
            obj.ToDate = nullHandler.GetDateTime(TODATE);
            obj.DivisionType = nullHandler.GetInt32(DIVISIONTYPE);
            obj.CreatorID = nullHandler.GetInt32(CREATORID);
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return obj;
        }
        private static CourseWaiverTransferMaster MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            CourseWaiverTransferMaster obj = null;
            if (theReader.Read())
            {
                obj = new CourseWaiverTransferMaster();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<CourseWaiverTransferMaster> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<CourseWaiverTransferMaster> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<CourseWaiverTransferMaster>();
                }
                CourseWaiverTransferMaster obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }
        
        
        #endregion

        #region Internal Methods

        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX(" + COURSEWAVTRANSFRMASTERID + ") FROM " + TABLENAME;
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

        #endregion

        #region Public Methods

        public static int Save(CourseWaiverTransferMaster courseWavTransfrMas)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (courseWavTransfrMas.Id == 0)
                {
                    courseWavTransfrMas.Id = GetMaxID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { courseWavTransfrMas.IDParam,  
                                                     courseWavTransfrMas.StudentIDParam,
                                                     courseWavTransfrMas.UniversityNameParam,
                                                     courseWavTransfrMas.FromDateParam,
                                                     courseWavTransfrMas.ToDateParam,
                                                     courseWavTransfrMas.DivisionTypeParam,
                                                     courseWavTransfrMas.CreatorIDParam, 
                                                     courseWavTransfrMas.CreatedDateParam, 
                                                     courseWavTransfrMas.ModifierIDParam, 
                                                     courseWavTransfrMas.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE [" + COURSEWAVTRANSFRMASTERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { courseWavTransfrMas.StudentIDParam,
                                                     courseWavTransfrMas.UniversityNameParam,
                                                     courseWavTransfrMas.FromDateParam,
                                                     courseWavTransfrMas.ToDateParam,
                                                     courseWavTransfrMas.DivisionTypeParam,
                                                     courseWavTransfrMas.CreatorIDParam, 
                                                     courseWavTransfrMas.CreatedDateParam, 
                                                     courseWavTransfrMas.ModifierIDParam, 
                                                     courseWavTransfrMas.ModifiedDateParam, 
                                                     courseWavTransfrMas.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                CourseWavTransfrDt.DeleteByMaster(courseWavTransfrMas.Id, sqlConn, sqlTran);

                if (courseWavTransfrMas.CourseWavTransfrDetails != null && courseWavTransfrMas.CourseWavTransfrDetails.Count > 0)
                {
                    foreach (CourseWavTransfrDt item in courseWavTransfrMas.CourseWavTransfrDetails)
                    {
                        item.CourseWavTransfrMasterID = courseWavTransfrMas.Id;
                        CourseWavTransfrDt.Save(item, sqlConn, sqlTran);
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
        public static int SaveImportData(CourseWaiverTransferMaster courseWavTransfrMas, int index, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;                
                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (courseWavTransfrMas.Id == 0)
                {
                    courseWavTransfrMas.Id = (index == 0) ? GetMaxID(sqlConn, sqlTran) : index;
                    command = INSERT;
                    sqlParams = new SqlParameter[] { courseWavTransfrMas.IDParam,  
                                                     courseWavTransfrMas.StudentIDParam,
                                                     courseWavTransfrMas.UniversityNameParam,
                                                     courseWavTransfrMas.FromDateParam,
                                                     courseWavTransfrMas.ToDateParam,
                                                     courseWavTransfrMas.DivisionTypeParam,
                                                     courseWavTransfrMas.CreatorIDParam, 
                                                     courseWavTransfrMas.CreatedDateParam, 
                                                     courseWavTransfrMas.ModifierIDParam, 
                                                     courseWavTransfrMas.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE [" + COURSEWAVTRANSFRMASTERID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { courseWavTransfrMas.StudentIDParam,
                                                     courseWavTransfrMas.UniversityNameParam,
                                                     courseWavTransfrMas.FromDateParam,
                                                     courseWavTransfrMas.ToDateParam,
                                                     courseWavTransfrMas.DivisionTypeParam,
                                                     courseWavTransfrMas.CreatorIDParam, 
                                                     courseWavTransfrMas.CreatedDateParam, 
                                                     courseWavTransfrMas.ModifierIDParam, 
                                                     courseWavTransfrMas.ModifiedDateParam, 
                                                     courseWavTransfrMas.IDParam };
                }

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                CourseWavTransfrDt.DeleteByMaster(courseWavTransfrMas.Id, sqlConn, sqlTran);

                if (courseWavTransfrMas.CourseWavTransfrDetails != null && courseWavTransfrMas.CourseWavTransfrDetails.Count > 0)
                {
                    foreach (CourseWavTransfrDt item in courseWavTransfrMas.CourseWavTransfrDetails)
                    {
                        item.CourseWavTransfrMasterID = courseWavTransfrMas.Id;
                        CourseWavTransfrDt.Save(item, sqlConn, sqlTran);
                    }
                }
                return courseWavTransfrMas.Id;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        public static int Delete(int MasID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
                CourseACUSpanDtl.DeleteByMaster(MasID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE [" + COURSEWAVTRANSFRMASTERID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(MasID, ID_PA);
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

        #endregion
    }
}
