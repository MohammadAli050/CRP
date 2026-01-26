using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class DiscountContinuationSetup_DAO:Base_DAO
    {
        private const string ACACALID = "AcaCalID";
        private const string PROGRAMID = "ProgramID";
        private const string TYPEDEFINITIONID = "TypeDefinitionID";
        private const string MINCREDITS = "MinCredits";
        private const string MAXCREDITS = "MaxCredits";
        private const string MINCGPA = "MinCGPA";

        private const string DISCOUNTCONTINUATIONID_PA = "@DiscountContinuationID";
        private const string ACACALID_PA = "@AcaCalID";
        private const string PROGRAMID_PA = "@ProgramID";
        private const string TYPEDEFINITIONID_PA = "@TypeDefinitionID";
        private const string MINCREDITS_PA = "@MinCredits";
        private const string MAXCREDITS_PA = "@MaxCredits";
        private const string MINCGPA_PA = "@MinCGPA";

        private const string ALLCOLUMNS = "[DiscountContinuationID], "
                                        + "[AcaCalID], "
                                        + "[ProgramID], "
                                        + "[TypeDefinitionID], "
                                        + "MinCredits, "
                                        + "MaxCredits, "
                                        + "MinCGPA,";

        private const string NOPKCOLUMNS = "[AcaCalID], "
                                        + "[ProgramID], "
                                        + "[TypeDefinitionID], "
                                        + "MinCredits, "
                                        + "MaxCredits, "
                                        + "MinCGPA, ";

        private const string TABLENAME = " [DiscountContinuationSetup] ";

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
                             + TYPEDEFINITIONID_PA + ", "
                             + MINCREDITS_PA + ", "
                             + MAXCREDITS_PA + ", "
                             + MINCGPA_PA + ", "
                             + CREATORID_PA + ", "
                             + "getdate())";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET [AcaCalID] = " + ACACALID_PA + ", "//1
                            + "[ProgramID] = " + PROGRAMID_PA + ","//2
                            + "[TypeDefinitionID] = " + TYPEDEFINITIONID_PA + ","//3
                            + "[MinCredits] = " + MINCREDITS_PA + ","//4
                            + "[MaxCredits] = " + MAXCREDITS_PA + ","//5
                            + "[MinCGPA] = " + MINCGPA_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","//9
                            + "[ModifiedDate] = getdate()";//10

        private const string DELETE = "DELETE FROM" + TABLENAME;

        private static DiscountContinuationSetupEntity Mapper(SQLNullHandler nullHandler)
        {
            DiscountContinuationSetupEntity sd = new DiscountContinuationSetupEntity();

            sd.Id = nullHandler.GetInt32("DiscountContinuationID");
            sd.Acacalid = nullHandler.GetInt32("AcaCalID");
            sd.Programid = nullHandler.GetInt32("ProgramID");
            sd.Typedefinitionid = nullHandler.GetInt32("TypeDefinitionID");
            sd.Mincredits = nullHandler.GetDecimal("MinCredits");
            sd.Maxcredits = nullHandler.GetDecimal(MAXCREDITS);
            sd.Mincgpa = nullHandler.GetDecimal("MinCGPA");
            sd.CreatorID = nullHandler.GetInt32("CreatedBy");
            sd.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            sd.ModifierID = nullHandler.GetInt32("ModifiedBy");
            sd.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return sd;
        }
        private static List<DiscountContinuationSetupEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<DiscountContinuationSetupEntity> sds = null;

            while (theReader.Read())
            {
                if (sds == null)
                {
                    sds = new List<DiscountContinuationSetupEntity>();
                }
                DiscountContinuationSetupEntity sd = Mapper(nullHandler);
                sds.Add(sd);
            }

            return sds;
        }
        private static DiscountContinuationSetupEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            DiscountContinuationSetupEntity sd = null;
            if (theReader.Read())
            {
                sd = new DiscountContinuationSetupEntity();
                sd = Mapper(nullHandler);
            }
            return sd;
        }
        private static List<SqlParameter> MakeSqlParameterList(DiscountContinuationSetupEntity sde)
        {
            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(ACACALID_PA, sde.Acacalid);
            dps.AddParameter(PROGRAMID_PA, sde.Programid);
            dps.AddParameter(TYPEDEFINITIONID_PA, sde.Typedefinitionid);
            dps.AddParameter(MINCREDITS_PA, sde.Mincredits);
            dps.AddParameter(MAXCREDITS_PA, sde.Maxcredits);
            dps.AddParameter(MINCGPA_PA, sde.Mincgpa);
            dps.AddParameter(CREATORID_PA, sde.CreatorID);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }

        public static int Save(List<DiscountContinuationSetupEntity> dcses)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                int acaID = dcses[0].Acacalid;
                int progId = dcses[0].Programid;
                Delete(acaID, progId);

                string cmd = INSERT;

                foreach (DiscountContinuationSetupEntity sd in dcses)
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
        internal static int Delete(int acaID, int progID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + " Where AcaCalID = " + ACACALID_PA + " and ProgramID = " + PROGRAMID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ACACALID_PA, acaID);
                dps.AddParameter(PROGRAMID_PA, progID);

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
        //public static int Delete(int discountContinuationSetupID, int acaID, int progID)
        //{
        //    int counter = 0;
        //    try
        //    {
        //        string cmd = DELETE + " Where AcaCalID = " + ACACALID_PA + " and ProgramID = " + PROGRAMID_PA + " and DiscountContinuationID = " + ID_PA;

        //        Common.DAOParameters dps = new Common.DAOParameters();
        //        dps.AddParameter(ACACALID_PA, acaID);
        //        dps.AddParameter(PROGRAMID_PA, progID);
        //        dps.AddParameter(DISCOUNTCONTINUATIONID_PA, discountContinuationSetupID);

        //        List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                                    
        //        counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
                  
        //        return counter;
        //    }
        //    catch (Exception ex)
        //    {
        //        //FixMe
        //        MSSqlConnectionHandler.RollBackAndClose();
                  //throw ex;
        //    }
        //}
        public static List<DiscountContinuationSetupEntity> GetDiscounts(int acaID, int progID)
        {
            try
            {
                List<DiscountContinuationSetupEntity> sdes = null;

                string cmd = SELECT + " Where AcaCalID = " + ACACALID_PA + " and ProgramID = " + PROGRAMID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ACACALID_PA, acaID);
                dps.AddParameter(PROGRAMID_PA, progID);
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
        
    }
}
