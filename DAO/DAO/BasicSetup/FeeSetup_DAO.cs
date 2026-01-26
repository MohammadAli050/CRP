using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class FeeSetup_DAO :Base_DAO
    {
        #region Constants
        private const string ACACALID_PA = "@AcaCalID";
        private const string PROGRAMID_PA = "@ProgramID";
        private const string TYPEDEFID_PA = "@TypeDefID";
        private const string AMOUNT_PA = "@Amount";


        private const string FEESETUPID = "FeeSetupID";
        private const string ACACALID = "AcaCalID";
        private const string PROGRAMID = "ProgramID";
        private const string TYPEDEFID = "TypeDefID";
        private const string AMOUNT = "Amount";

        private const string ALLCOLUMNS = "[FeeSetupID], "
                                        + "[AcaCalID], "
                                        + "[ProgramID], "
                                        + "[TypeDefID], "
                                        + "Amount, ";

        private const string NOPKCOLUMNS = "[AcaCalID], "
                                        + "[ProgramID], "
                                        + "[TypeDefID], "
                                        + "Amount, ";

        private const string TABLENAME = " [FeeSetup] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASEMUSTCOLUMNS + ")"
                             + "VALUES ( "
                             + ACACALID_PA + ", "
                             + PROGRAMID_PA + ", "
                             + TYPEDEFID_PA + ", "
                             + AMOUNT_PA + ", "
                             + CREATORID_PA + ", "
                             + "getdate())";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [AcaCalID] = " + ACACALID_PA + ", "//1
                            + "[ProgramID] = " + PROGRAMID_PA + ","//2
                            + "[TypeDefID] = " + TYPEDEFID_PA + ","//3
                            + "[Amount] = " + AMOUNT_PA + ","//3
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","//9
                            + "[ModifiedDate] = getdate()";//10

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        #region Methods
        private static FeeSetupEntity Mapper(SQLNullHandler nullHandler)
        {
            FeeSetupEntity sd = new FeeSetupEntity();

            sd.Id = nullHandler.GetInt32(FEESETUPID);
            sd.AcaCalID = nullHandler.GetInt32(ACACALID);
            sd.ProgramID = nullHandler.GetInt32(PROGRAMID);
            sd.TypeDefID = nullHandler.GetInt32(TYPEDEFID);
            sd.Amount = nullHandler.GetDecimal(AMOUNT);
            sd.CreatorID = nullHandler.GetInt32("CreatedBy");
            sd.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            sd.ModifierID = nullHandler.GetInt32("ModifiedBy");
            sd.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return sd;
        }
        private static List<FeeSetupEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<FeeSetupEntity> sds = null;

            while (theReader.Read())
            {
                if (sds == null)
                {
                    sds = new List<FeeSetupEntity>();
                }
                FeeSetupEntity sd = Mapper(nullHandler);
                sds.Add(sd);
            }

            return sds;
        }
        private static FeeSetupEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            FeeSetupEntity sd = null;
            if (theReader.Read())
            {
                sd = new FeeSetupEntity();
                sd = Mapper(nullHandler);
            }
            return sd;
        }

        private static List<SqlParameter> MakeSqlParameterList(FeeSetupEntity sde)
        {
            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(ACACALID_PA, sde.AcaCalID);
            dps.AddParameter(PROGRAMID_PA, sde.ProgramID);
            dps.AddParameter(TYPEDEFID_PA, sde.TypeDefID);
            dps.AddParameter(AMOUNT_PA, sde.Amount);
            dps.AddParameter(CREATORID_PA, sde.CreatorID);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }


        public static int Save(List<FeeSetupEntity> sds)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();
                Delete(sds[0].AcaCalID, sds[0].ProgramID);
                string cmd = INSERT;

                foreach (FeeSetupEntity sd in sds)
                {
                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(sd));
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
        internal static int Delete(int acaCalID, int progId)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + " Where [AcaCalID] = " + ACACALID_PA + " and [ProgramID] = " + PROGRAMID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ACACALID_PA, acaCalID);
                dps.AddParameter(PROGRAMID_PA, progId);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
                return counter;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }
        public static List<FeeSetupEntity> Gets(int acaCalID, int progId)
        {
            try
            {
                List<FeeSetupEntity> sdes = null;

                string cmd = SELECT + " Where [AcaCalID] = " + ACACALID_PA + " and [ProgramID] = " + PROGRAMID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ACACALID_PA, acaCalID);
                dps.AddParameter(PROGRAMID_PA, progId);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                SqlDataReader rd = QueryHandler.ExecuteSelectQuery(cmd, ps);
                sdes = Maps(rd);
                return sdes;
            }
            catch (Exception ex)
            {
                //FixMe
                throw ex;
            }
        }
        #endregion
    }
}
