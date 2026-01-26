using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class SiblingSetup_DAO : Base_DAO
    {
        #region Constants

        private const string SIBLINGSETUPID = "SiblingSetupId";//0


        private const string GROUPID = "GroupID";//1
        private const string GROUPID_PA = "@GroupID";

        private const string APPLICANTID = "ApplicantId";//2
        private const string APPLICANTID_PA = "@ApplicantId";

        private const string ROLL_PA = "@Roll";

        #endregion

        #region PKCOLUMNS
        private const string ALLCOLUMNS = "[" + SIBLINGSETUPID + "]," +
                                        "[" + GROUPID + "]," +
                                        "[" + APPLICANTID + "],";
        #endregion
        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + GROUPID + "]," +
                                        "[" + APPLICANTID + "],";
        #endregion
        private const string TABLENAME = " SiblingSetup ";
        #region SELECT
        private const string SELECT = " SELECT "
                                 + ALLCOLUMNS
                                 + BASECOLUMNS
                                 + " FROM " + TABLENAME;
        #endregion
        #region INSERT

        private const string INSERT = " INSERT INTO " + TABLENAME
                                    + "("
                                     + NOPKCOLUMNS
                                     + BASEMUSTCOLUMNS
                                     + ")"
                                    + " VALUES ("
                                     + GROUPID_PA + ","
                                     + APPLICANTID_PA + ","
                                     + CREATORID_PA + ","
                                     + CREATEDDATE_PA + ")";
        #endregion
        #region UPDATE

        private const string UPDATE = "UPDATE" + TABLENAME + "SET"
                                     + GROUPID + " = " + GROUPID_PA + ","
                                     + APPLICANTID + " = " + APPLICANTID_PA + ","
                                     + CREATORID + " = " + CREATORID_PA + ","
                                     + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                                     + MODIFIERID + " = " + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion
        #region DELETE

        private const string DELETE = "DELETE FROM " + TABLENAME;
        #endregion
        #region Methods
        private static SiblingSetupEntity Mapper(SQLNullHandler nullHandler)
        {
            SiblingSetupEntity siblingSetupEntity = new SiblingSetupEntity();

            siblingSetupEntity.Id = nullHandler.GetInt32(SIBLINGSETUPID);
            siblingSetupEntity.GroupId = nullHandler.GetInt32(GROUPID);
            siblingSetupEntity.ApplicantId = nullHandler.GetInt32(APPLICANTID);
            siblingSetupEntity.CreatorID = nullHandler.GetInt32(CREATORID);
            siblingSetupEntity.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            siblingSetupEntity.ModifierID = nullHandler.GetInt32(MODIFIERID);
            siblingSetupEntity.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return siblingSetupEntity;
        }//end of method Mapper()

        private static List<SiblingSetupEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<SiblingSetupEntity> siblingsetupEntities = null;
            while (theReader.Read())
            {
                if (siblingsetupEntities == null)
                {
                    siblingsetupEntities = new List<SiblingSetupEntity>();
                }
                SiblingSetupEntity siblingSetupEntity = Mapper(nullHandler);
                siblingsetupEntities.Add(siblingSetupEntity);
            }

            return siblingsetupEntities;
        }//end of method Map()

        private static SiblingSetupEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            SiblingSetupEntity SiblingSetupEntity = null;
            if (theReader.Read())
            {
                SiblingSetupEntity = new SiblingSetupEntity();
                SiblingSetupEntity = Mapper(nullHandler);
            }

            return SiblingSetupEntity;
        }//end of method Map()

        //private static List<SqlParameter> MakeSqlParameterList(SiblingSetupEntity SiblingSetupEntity)
        //{
        //    DAOParameters dps = new DAOParameters();

        //    dps.AddParameter(APPLICANTID_PA, siblingSetupEntity.Applicantid);

        //    dps.AddParameter(CREATORID_PA, siblingSetupEntity.CreatorID);
        //    dps.AddParameter(CREATEDDATE_PA, siblingSetupEntity.CreatedDate);

        //    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
        //    return ps;
        //}

        public static int Save(SiblingSetupEntity siblingSetupEntity)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();
                string cmd = INSERT;

                DAOParameters dps = new DAOParameters();
                dps.AddParameter(APPLICANTID_PA, siblingSetupEntity.ApplicantId);
                dps.AddParameter(GROUPID_PA, siblingSetupEntity.GroupId);
                dps.AddParameter(CREATORID_PA, siblingSetupEntity.CreatorID);
                dps.AddParameter(CREATEDDATE_PA, siblingSetupEntity.CreatedDate);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps) ;

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

        public static int Save(List<SiblingSetupEntity> siblingSetupEntities)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                int MaxGroupID = GetMaxGroupId(sqlConn, sqlTran);

                string cmd = INSERT;

                foreach (SiblingSetupEntity siblingSetupEntity in siblingSetupEntities)
                {
                    siblingSetupEntity.GroupId = MaxGroupID;

                    DAOParameters dps = new DAOParameters();

                    dps.AddParameter(APPLICANTID_PA, siblingSetupEntity.ApplicantId);
                    dps.AddParameter(GROUPID_PA, siblingSetupEntity.GroupId);
                    dps.AddParameter(CREATORID_PA, siblingSetupEntity.CreatorID);
                    dps.AddParameter(CREATEDDATE_PA, siblingSetupEntity.CreatedDate);

                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);
                }
                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();

                if (counter > 0)
                {
                    return MaxGroupID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        private static int GetMaxGroupId(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            Int32 maxGroupId = 0;

            string command = " SELECT MAX(GroupID) FROM " + TABLENAME;

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                maxGroupId = 1;
            }
            else
            {
                maxGroupId = Convert.ToInt32(ob) + 1;
            }

            return maxGroupId;
        }

        public static int Delete(int ID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + " WHERE SiblingSetupId = " + ID;

                //DAOParameters dps = new DAOParameters();
                //dps.AddParameter(ID_PA , ID);
                //List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                counter = QueryHandler.MSSqlExecuteActionDelete(cmd, sqlConn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }

        public static List<SiblingSetupEntity> GetCandidateStudents(int roll)
        {
            try
            {
                List<SiblingSetupEntity> uiuemsAcSiblingsetupEntities = null;

                string cmd = SELECT + "WHERE Groupid = (select GroupID from SiblingSetup where ApplicantId = (select StudentID from Student where Roll = '" + GROUPID_PA + "'))";

                DAOParameters dps = new DAOParameters();

                // dps.AddParameter( GROUPID_PA, );
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
                uiuemsAcSiblingsetupEntities = Maps(dr);
                return uiuemsAcSiblingsetupEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SiblingSetupEntity> GetAllInGroupBy(string roll)
        {
            try
            {
                List<SiblingSetupEntity> siblingsetupEntities = null;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string cmd = SELECT + " WHERE Groupid = (select GroupID from SiblingSetup where ApplicantId = (select StudentID from Student where Roll = '" + roll + "'))";

                //DAOParameters dps = new DAOParameters();
                //dps.AddParameter(ROLL_PA, roll);
                //List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, sqlConn);
                
                siblingsetupEntities = Maps(dr);
                dr.Close();

                return siblingsetupEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public static List<SiblingSetupEntity> GetAllInGroupBy(int groupId)
        {
            try
            {
                List<SiblingSetupEntity> siblingsetupEntities = null;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

                string cmd = SELECT + " WHERE Groupid = " + groupId;

                //DAOParameters dps = new DAOParameters();
                //dps.AddParameter(ROLL_PA, roll);
                //List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, sqlConn);

                siblingsetupEntities = Maps(dr);
                dr.Close();

                return siblingsetupEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int CheckDuplicate(int applicantId)
        {
            int counter = 0;
            try
            {
                string cmd = " select count(*) from  SiblingSetup where ApplicantId = " + applicantId;

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                counter = QueryHandler.MSSqlExecuteScalar(cmd, sqlConn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }
    }

}//End Of Namespace DataAccess
