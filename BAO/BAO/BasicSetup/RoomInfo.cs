using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class RoomInfo : Base
    {
        #region variables

        private string _roomno;        
        private string _roomname;        
        private int _roomtypeId;        
        private int _capacity;
        private int _examCapacity;
        //private int _campusId;
        private int _buildingId;
        private int _roomId;        
        private int _addressId;
        private RoomType _roomType;
        
        #endregion

        #region properties

        private const string ROOMNO_PA = "@RoomNumber";
        private const string ROOMNAME_PA = "@RoomName";
        private const string ROOMTYPEID_PA = "@RoomTypeID";
        private const string CAPACITY_PA = "@Capacity";
        private const string ADDRESSID_PA = "@AddressID";
        private const string EXAMCAPACITY_PA = "@ExamCapacity";
       // private const string CAMPUSID_PA = "@CampusId";
        private const string BUILDINGID_PA = "@BuildingId";


        private const string ALLCOLUMNS = "[RoomInfoID], "
                                        + "[RoomNumber], "
                                        + "[RoomName], "
                                        + "[RoomTypeID], "
                                        + "[Capacity], "
                                        + "[ExamCapacity], "
                                       // + "[CampusId], "
                                        + "[BuildingId], "
                                        + "[AddressID], ";

        private const string NOPKCOLUMNS = "[RoomNumber], "
                                        + "[RoomName], "
                                        + "[RoomTypeID], "
                                        + "[Capacity], "
                                        + "[ExamCapacity], "
                                        //+ "[CampusId], "
                                        + "[BuildingId], "
                                        + "[AddressID], ";

        private const string TABLENAME = " [RoomInformation] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS + ")"
                             + "VALUES ( "
                             + ROOMNO_PA + ", "
                             + ROOMNAME_PA + ", "
                             + ROOMTYPEID_PA + ", "
                             + CAPACITY_PA + ", "
                             + EXAMCAPACITY_PA + ", "
                             //+ CAMPUSID_PA + ", "
                             + BUILDINGID_PA + ", "
                             + ADDRESSID_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [RoomNumber] = " + ROOMNO_PA + ", "
                            + "[RoomName] = " + ROOMNAME_PA + ","
                            + "[RoomTypeID] = " + ROOMTYPEID_PA + ","
                            + "[Capacity] = " + CAPACITY_PA + ","
                            + "[ExamCapacity] = " + EXAMCAPACITY_PA + ","
                            //+ "[CampusId] = " + CAMPUSID_PA + ","
                            + "[BuildingId] = " + BUILDINGID_PA + ","
                            + "[AddressID] = " + ADDRESSID_PA + ","
                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;

        public string Roomno
        {
            get { return _roomno; }
            set { _roomno = value; }
        }
        private SqlParameter RoomnoParam
        {
            get
            {
                SqlParameter roomnoParam = new SqlParameter();
                roomnoParam.ParameterName = ROOMNO_PA;
                roomnoParam.Value = Roomno;
                return roomnoParam;
            }
        }

        public string Roomname
        {
            get { return _roomname; }
            set { _roomname = value; }
        }
        private SqlParameter RoomnameParam
        {
            get
            {
                SqlParameter roomnameParam = new SqlParameter();
                roomnameParam.ParameterName = ROOMNAME_PA;
                roomnameParam.Value = Roomname;
                return roomnameParam;
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

        public RoomType Type
        {
            get
            {
                if (_roomType == null)
                {
                    if (_roomtypeId > 0)
                    {
                        _roomType = RoomType.GetRoomType(_roomtypeId);
                    }
                }
                return _roomType;
            }
        }
        public string TypeName
        {
            get
            {
                return Type.Roomtypename;
            }
        }

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        private SqlParameter CapacityParam
        {
            get
            {
                SqlParameter capacityParam = new SqlParameter();
                capacityParam.ParameterName = CAPACITY_PA;
                capacityParam.Value = Capacity;
                return capacityParam;
            }
        }

        public int RoomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }

        public int AddressId
        {
            get { return _addressId; }
            set { _addressId = value; }
        }
        private SqlParameter AddressIdParam
        {
            get
            {
                SqlParameter addressIdParam = new SqlParameter();
                addressIdParam.ParameterName = ADDRESSID_PA;
                addressIdParam.Value = AddressId;
                return addressIdParam;
            }
        }

        public string RoomNumberAndCapacity
        {
            get
            {
                return _roomno + "-" + _capacity.ToString();
            }
            set
            {
                string tkened = value;
                string[] x = tkened.Split(new char[] { '-' });
                _roomno = x[0];
                _capacity = Convert.ToInt32( x[1]);
            }
        }

        public int ExamCapacityId
        {
            get { return _examCapacity; }
            set { _examCapacity = value; }
        }
        private SqlParameter ExamCapacityIdParam
        {
            get
            {
                SqlParameter examCapacityIdParam = new SqlParameter();
                examCapacityIdParam.ParameterName = EXAMCAPACITY_PA;
                examCapacityIdParam.Value = ExamCapacityId;
                return examCapacityIdParam;
            }
        }

        //public int CampusId
        //{
        //    get { return _campusId; }
        //    set { _campusId = value; }
        //}
        //private SqlParameter CampusIdParam
        //{
        //    get
        //    {
        //        SqlParameter campusIdParam = new SqlParameter();
        //        campusIdParam.ParameterName = CAMPUSID_PA;
        //        campusIdParam.Value = CampusId;
        //        return campusIdParam;
        //    }
        //}

        public int BuildingId
        {
            get { return _buildingId; }
            set { _buildingId = value; }
        }
        private SqlParameter BuildingIdParam
        {
            get
            {
                SqlParameter buildingIdParam = new SqlParameter();
                buildingIdParam.ParameterName = BUILDINGID_PA;
                buildingIdParam.Value = BuildingId;
                return buildingIdParam;
            }
        }

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
        private static RoomInfo RoomInfoMapper(SQLNullHandler nullHandler)
        {
            RoomInfo rf = new RoomInfo();

            rf.Id = nullHandler.GetInt32("RoomInfoID");
            rf.Roomno = nullHandler.GetString("RoomNumber");
            rf.Roomname = nullHandler.GetString("RoomName");
            rf.RoomtypeId = nullHandler.GetInt32("RoomTypeID");
            rf.Capacity = nullHandler.GetInt32("Capacity");
            rf.ExamCapacityId = nullHandler.GetInt32("ExamCapacity");
            rf.BuildingId = nullHandler.GetInt32("BuildingId");
            rf.AddressId = nullHandler.GetInt32("AddressID");
            rf.CreatorID = nullHandler.GetInt32("CreatedBy");
            rf.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            rf.ModifierID = nullHandler.GetInt32("ModifiedBy");
            rf.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return rf;

        }
        #if (false)
        /// <summary>
        /// If more than one rows are need to handle, this method maps all rows     
        /// </summary>
        /// <param name="theReader"></param>
        /// <returns></returns>
        #endif
        private static List<RoomInfo> MapRoomsInfo(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<RoomInfo> rfs = null;
            while (theReader.Read())
            {
                if (rfs == null)
                {
                    rfs = new List<RoomInfo>();
                }
                RoomInfo rf = RoomInfoMapper(nullHandler);
                rfs.Add(rf);
            }
            return rfs;
        }
        #if (false)
        /// <summary>
        /// all columns of exactly one row are mapped here
        /// </summary>
        /// <param name="theReader"></param>
        /// <returns></returns>
        #endif
        private static RoomInfo MapRoomInfo(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            RoomInfo rf = null;
            if (theReader.Read())
            {
                rf = new RoomInfo();
                rf = RoomInfoMapper(nullHandler);
            }

            return rf;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<RoomInfo> GetRoomsInfo()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<RoomInfo> rfs = MapRoomsInfo(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rfs;
        }
        /// <summary>
        /// Get all or particular information
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<RoomInfo> GetRoomsInfo(string parameter)
        {
            string command = SELECT
                            + "WHERE [RoomNumber] Like '%" + parameter + "%' OR [RoomName] LIKE '%" + parameter + "%' OR [Capacity] LIKE '%" + parameter + "%'";
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<RoomInfo> rfs = MapRoomsInfo(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rfs;
        }

        public static RoomInfo GetRoomInfo(int roomId)
        {
            string command = SELECT
                            + "WHERE RoomInfoID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(roomId, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            RoomInfo rf = MapRoomInfo(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return rf;

        }
        public static RoomInfo GetRoomInfo(string strRoomNo)
        {
            try
            {
                string command = SELECT
                                + "WHERE RoomNumber = " + ROOMNO_PA;
                SqlParameter roomnoParam = MSSqlConnectionHandler.MSSqlParamGenerator(strRoomNo, ROOMNO_PA);

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { roomnoParam });

                RoomInfo rf = MapRoomInfo(theReader);
                theReader.Close();

                MSSqlConnectionHandler.CloseDbConnection();

                return rf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Save(RoomInfo rf)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                //if (HasDuplicateCode(rf, sqlConn, sqlTran))
                //{
                //    throw new Exception("Duplicate rf Code Not Allowed.");
                //}

                if (rf.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { rf.RoomnoParam,  
                                                     rf.RoomnameParam,
                                                     rf.RoomtypeIdParam,
                                                     rf.CapacityParam,
                                                     rf.ExamCapacityIdParam,
                                                     //rf.CampusIdParam,
                                                     rf.BuildingIdParam,
                                                     rf.AddressIdParam,
                                                     rf.CreatorIDParam, 
                                                     rf.CreatedDateParam, 
                                                     rf.ModifierIDParam, 
                                                     rf.ModifiedDateParam };
                }
                else
                {
                    command = UPDATE
                            + " WHERE RoomInfoID = " + ID_PA;
                    sqlParams = new SqlParameter[] { rf.IDParam,
                                                     rf.RoomnoParam,  
                                                     rf.RoomnameParam,
                                                     rf.RoomtypeIdParam,
                                                     rf.CapacityParam,
                                                     rf.ExamCapacityIdParam,
                                                     //rf.CampusIdParam,
                                                     rf.BuildingIdParam,
                                                     rf.AddressIdParam,
                                                     rf.CreatorIDParam, 
                                                     rf.CreatedDateParam, 
                                                     rf.ModifierIDParam, 
                                                     rf.ModifiedDateParam };
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
                                + "WHERE RoomInfoID = " + ID_PA;

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
        
        public static bool IsExist(string strRoomname, string strRoomno, int intAddressID)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [AddressID] = " + ADDRESSID_PA + " and [RoomName] = " + ROOMNAME_PA + " or [RoomNumber] = " + ROOMNO_PA;
            SqlParameter roomNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(strRoomname, ROOMNAME_PA);
            SqlParameter roomNoParam = MSSqlConnectionHandler.MSSqlParamGenerator(strRoomno, ROOMNO_PA);
            SqlParameter addressIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(intAddressID, ADDRESSID_PA);

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { addressIDParam, roomNameParam, roomNoParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicate(RoomInfo rf)
        {
            if (rf == null)
            {
                return RoomInfo.IsExist(rf.Roomname, rf.Roomno, rf.AddressId);
            }
            else
            {
                if (rf.Id == 0)
                {
                    if (RoomInfo.IsExist(rf.Roomname, rf.Roomno, rf.AddressId))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (RoomInfo.IsExist(rf.Roomname, rf.Roomno, rf.AddressId))
                    {
                        return true;
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
