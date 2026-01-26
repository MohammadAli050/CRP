using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class StdDiscount_DAO : Base_DAO
    {
        #region Constants
        private const string ADMISSIONID_PA = "@AdmissionID";
        private const string TYPEDEFID_PA = "@TypeDefID";
        private const string TYPEPERCENTAGE_PA = "@TypePercentage";
        private const string EFFECTIVEACACALID_PA = "@EffectiveAcaCalID";

        private const string ALLCOLUMNS = "[ID], "
                                        + "[AdmissionID], "
                                        + "[TypeDefID], "
                                        + "[TypePercentage], "
                                        + "EffectiveAcaCalID";

        private const string NOPKCOLUMNS = "[AdmissionID], "
                                        + "[TypeDefID], "
                                        + "[TypePercentage], "
                                        + "EffectiveAcaCalID";

        private const string TABLENAME = " [StdDiscountCurrent] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        //private const string INSERT = "INSERT INTO" + TABLENAME + "("
        //                     + NOPKCOLUMNS
        //                     + BASEMUSTCOLUMNS + ")"
        //                     + "VALUES ( "
        //                     + ADMISSIONID_PA + ", "
        //                     + TYPEDEFID_PA + ", "
        //                     + TYPEPERCENTAGE_PA + ", "
        //                     + CREATORID_PA + ", "
        //                     + CREATEDDATE_PA + ")";

        //private const string UPDATE = "UPDATE" + TABLENAME
        //                    + "SET [AdmissionID] = " + ADMISSIONID_PA + ", "//1
        //                    + "[TypeDefID] = " + TYPEDEFID_PA + ","//2
        //                    + "[TypePercentage] = " + TYPEPERCENTAGE_PA + ","//3
        //                    + "[CreatedBy] = " + CREATORID_PA + ","//7
        //                    + "[CreatedDate] = " + CREATEDDATE_PA + ","//8
        //                    + "[ModifiedBy] = " + MODIFIERID_PA + ","//9
        //                    + "[ModifiedDate] = " + MOIDFIEDDATE_PA;//10

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        #region Methods
        private static StdDiscountEntity Mapper(SQLNullHandler nullHandler)
        {
            StdDiscountEntity sd = new StdDiscountEntity();

            sd.Id = nullHandler.GetInt32("ID");
            sd.AdmID = nullHandler.GetInt32("AdmissionID");
            sd.TypeDefID = nullHandler.GetInt32("TypeDefID");
            sd.TypePercentage = nullHandler.GetDecimal("TypePercentage");
            sd.CreatorID = nullHandler.GetInt32("CreatedBy");
            sd.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            sd.ModifierID = nullHandler.GetInt32("ModifiedBy");
            sd.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return sd;
        }
        private static List<StdDiscountEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<StdDiscountEntity> sds = null;

            while (theReader.Read())
            {
                if (sds == null)
                {
                    sds = new List<StdDiscountEntity>();
                }
                StdDiscountEntity sd = Mapper(nullHandler);
                sds.Add(sd);
            }

            return sds;
        }
        private static StdDiscountEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            StdDiscountEntity sd = null;
            if (theReader.Read())
            {
                sd = new StdDiscountEntity();
                sd = Mapper(nullHandler);
            }
            return sd;
        }

        private static List<SqlParameter> MakeSqlParameterList(StdDiscountEntity sde)
        {
            Common.DAOParameters dps = new Common.DAOParameters();
            dps.AddParameter(ADMISSIONID_PA, sde.AdmID);
            dps.AddParameter(TYPEDEFID_PA, sde.TypeDefID);
            dps.AddParameter(TYPEPERCENTAGE_PA, sde.TypePercentage);
            dps.AddParameter(EFFECTIVEACACALID_PA, sde.EffectiveAcaCalID);
            dps.AddParameter(CREATORID_PA, sde.CreatorID);
            //dps.AddParameter(CREATEDDATE_PA, sde.CreatedDate);

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }


        public static int Save(List<StdDiscountEntity> sds)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                //string cmd = "DECLARE	@return_value int, " +
                //                "@result int " +

                //        "EXEC	@return_value = [dbo].[sp_InsertIntoStdDiscount] " +
                //                "@admissionID = " + ADMISSIONID_PA + ", " +
                //                "@typeDefID = " + TYPEDEFID_PA + ", " +
                //                "@typePercentage = " + TYPEPERCENTAGE_PA + ", " +
                //                "@effectiveAcaCalId = " + EFFECTIVEACACALID_PA + ", " +
                //                "@createdBy = " + CREATORID_PA + ", " +
                //    //"@createdDate = " + CREATEDDATE_PA + ", " +
                //                "@result = @result OUTPUT " +

                //        "SELECT	@result as '@result' ";

                foreach (StdDiscountEntity sd in sds)
                {
                    //StdDiscountEntity ent = new StdDiscountEntity();
                    //ent.AdmID = sd.AdmID;
                    //ent.TypeDefID = sd.TypeDefID;
                    //ent.TypePercentage = sd.TypePercentage;
                    //ent.EffectiveAcaCalID = sd.EffectiveAcaCalID;
                    //ent.CreatorID = sd.CreatorID;
                    ////ent.CreatedDate = sd.CreatedDate;used sql database date written in stored procedure
                    //counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(ent));
                    string cmd = "DECLARE	@return_value int, " +
                                "@result int " +

                        "EXEC	@return_value = [dbo].[sp_InsertIntoStdDiscount] " +
                                "@admissionID = " + sd.AdmID + ", " +
                                "@typeDefID = " + sd.TypeDefID + ", " +
                                "@typePercentage = " + sd.TypePercentage + ", " +
                                "@effectiveAcaCalId = " + sd.EffectiveAcaCalID + ", " +
                                "@createdBy = " + sd.CreatorID + ", " +
                        //"@createdDate = " + CREATEDDATE_PA + ", " +
                                "@result = @result OUTPUT " +

                        "SELECT	@result as '@result' ";
                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, null);
                    
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
        internal static int Delete(int adminID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + " Where AdmissionID = " + ADMISSIONID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ADMISSIONID_PA, adminID);

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
        public static List<StdDiscountEntity> GetStdDiscounts(int adminID)
        {
            try
            {
                List<StdDiscountEntity> sdes = null;

                string cmd = SELECT + " Where AdmissionID = " + ADMISSIONID_PA;

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter(ADMISSIONID_PA, adminID);

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
