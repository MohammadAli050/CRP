using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class RoomType : Base
    {
        #region variables

        private string _roomtypename;
        private string _lastroomtypename;
        private int _roomtypeId;           
        
        #endregion

        #region properties
        public string LastRoomtypename
        {
            get { return _lastroomtypename; }
            set { _lastroomtypename = value; }
        }
        
        public string Roomtypename
        {
            get { return _roomtypename; }
            set { _roomtypename = value; }
        }
        private SqlParameter RoomtypenameParam
        {
            get
            {
                SqlParameter roomtypenameParam = new SqlParameter();
                roomtypenameParam.ParameterName = ROOMTYPENAME_PA;
                roomtypenameParam.Value = Roomtypename;
                return roomtypenameParam;
            }
        }

        public int RoomtypeId
        {
            get { return _roomtypeId; }
            set { _roomtypeId = value; }
        }
        private SqlParameter RoomtypeIdParam
        {
            get
            {
                SqlParameter roomtypeIdParam = new SqlParameter();
                roomtypeIdParam.ParameterName = ROOMTYPEID_PA;
                roomtypeIdParam.Value = RoomtypeId;
                return roomtypeIdParam;
            }
        }

        private const string ROOMTYPEID_PA = "@RoomTypeID";
        private const string ROOMTYPENAME_PA = "@TypeName";

        private const string ALLCOLUMNS = "[RoomTypeID], "
                                        + "[TypeName], ";
        private const string NOPKCOLUMNS = "[TypeName], ";
        private const string TABLENAME = " [RoomType] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS + ")"
                             + "VALUES ( "
                             + ROOMTYPENAME_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [TypeName] = " + ROOMTYPENAME_PA + ","
                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region methods
#if(false)
        /// <summary>
        /// mapping colums of the table with the corresponding class. 
        /// nullhandler is used to solve the following purpose.
        /// 1. in table, null can be assigned to an integer field. 
        ///     but in c #, interger value cant be null, its declaration value is zero.
        ///     so we need to map the column of the table with the dot net environment.
        /// </summary>
        /// <param name="nullHandler"></param>
        /// <returns></returns>
#endif
        private static RoomType RoomTypeMapper(SQLNullHandler nullHandler)
        {
            RoomType rt = new RoomType();

            rt.Id = nullHandler.GetInt32("RoomTypeID");
            rt.Roomtypename = nullHandler.GetString("TypeName");
            rt.CreatorID = nullHandler.GetInt32("CreatedBy");
            rt.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            rt.ModifierID = nullHandler.GetInt32("ModifiedBy");
            rt.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return rt;

        }
#if (false)
        /// <summary>
        /// If more than one rows are need to handle, this method maps all rows     
        /// </summary>
        /// <param name="theReader"></param>
        /// <returns></returns>
#endif
        private static List<RoomType> MapRoomTypesInfo(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<RoomType> rts = null;
            while (theReader.Read())
            {
                if (rts == null)
                {
                    rts = new List<RoomType>();
                }
                RoomType rt = RoomTypeMapper(nullHandler);
                rts.Add(rt);
            }
            return rts;
        }
#if (false)
        /// <summary>
        /// all columns of exactly one row are mapped here
        /// </summary>
        /// <param name="theReader"></param>
        /// <returns></returns>
#endif
        private static RoomType MapRoomType(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            RoomType rt = null;
            if (theReader.Read())
            {
                rt = new RoomType();
                rt = RoomTypeMapper(nullHandler);
            }

            return rt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<RoomType> GetRoomTypesInfo()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<RoomType> rts = MapRoomTypesInfo(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rts;
        }
        /// <summary>
        /// Get all or particular information
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<RoomType> GetRoomTypesInfo(string parameter)
        {
            string command = SELECT
                            + "WHERE [TypeName] Like '%" + parameter + "%'";
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<RoomType> rts = MapRoomTypesInfo(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rts;
        }

        public static RoomType GetRoomType(int roomTypeId)
        {
            string command = SELECT
                            + "WHERE RoomTypeID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(roomTypeId, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            RoomType rt = MapRoomType(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rt;
        }

        public static RoomType GetRoomType(string parameter)
        {
            string command = SELECT
                            + "WHERE TypeName = '"+parameter+"' ";
            
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            RoomType rt = MapRoomType(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rt;
        }

        public static int Save(RoomType rt)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                //if (HasDuplicateCode(rt, sqlConn, sqlTran))
                //{
                //    throw new Exception("Duplicate rt Code Not Allowed.");
                //}

                if (rt.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { rt.RoomtypeIdParam,  
                                                     rt.RoomtypenameParam,
                                                     rt.CreatorIDParam, 
                                                     rt.CreatedDateParam, 
                                                     rt.ModifierIDParam, 
                                                     rt.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE RoomTypeID = " + ID_PA;
                    sqlParams = new SqlParameter[] { rt.IDParam,  
                                                     rt.RoomtypenameParam,
                                                     rt.CreatorIDParam, 
                                                     rt.CreatedDateParam, 
                                                     rt.ModifierIDParam, 
                                                     rt.ModifiedDateParam };
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

        public static int Delete(int roomId)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE RoomTypeID = " + ID_PA;

                SqlParameter userIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(roomId, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { userIDParam });

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
        internal static bool IsExist(string strTypeName, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [TypeName] = " + ROOMTYPENAME_PA;
            SqlParameter typeNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(strTypeName, ROOMTYPENAME_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { typeNameParam });

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool IsExist(string strTypeName)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [TypeName] = " + ROOMTYPENAME_PA;
            SqlParameter typeNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(strTypeName, ROOMTYPENAME_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { typeNameParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicateType(RoomType rt)
        {
            if (rt == null)
            {
                return RoomType.IsExist(rt.Roomtypename);
            }
            else
            {
                if (rt.Id == 0)
                {
                    if (RoomType.IsExist(rt.Roomtypename))
                    {
                        return RoomType.IsExist(rt.Roomtypename);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (rt.Roomtypename != rt.LastRoomtypename)
                    {
                        return School.IsExist(rt.Roomtypename);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        #endregion

    }
}
