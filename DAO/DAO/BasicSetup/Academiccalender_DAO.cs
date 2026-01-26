using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
	public class rbAcademiccalenderDao : Base_DAO
	{
		#region Constants
		
		private const string ACADEMICCALENDERID = "ACADEMICCALENDERID";//0
        private const string ACADEMICCALENDERID_PA = "@ACADEMICCALENDERID";//0
		
		private const string CALENDERUNITTYPEID = "CALENDERUNITTYPEID";//1
		private const string CALENDERUNITTYPEID_PA  = "@CALENDERUNITTYPEID";

		
		private const string YEAR = "YEAR";//2
		private const string YEAR_PA  = "@YEAR";

		
		private const string BATCHCODE = "BATCHCODE";//3
		private const string BATCHCODE_PA  = "@BATCHCODE";

		
		private const string ISCURRENT = "ISCURRENT";//4
		private const string ISCURRENT_PA  = "@ISCURRENT";

		
		private const string ISNEXT = "ISNEXT";//5
		private const string ISNEXT_PA  = "@ISNEXT";

		
		private const string STARTDATE = "STARTDATE";//6
		private const string STARTDATE_PA  = "@STARTDATE";

		
		private const string ENDDATE = "ENDDATE";//7
		private const string ENDDATE_PA  = "@ENDDATE";

		
		private const string FULLPAYNOFINELSTDT = "FULLPAYNOFINELSTDT";//8
		private const string FULLPAYNOFINELSTDT_PA  = "@FULLPAYNOFINELSTDT";

		
		private const string FIRSTINSTNOFINELSTDT = "FIRSTINSTNOFINELSTDT";//9
		private const string FIRSTINSTNOFINELSTDT_PA  = "@FIRSTINSTNOFINELSTDT";

		
		private const string SECINSTNOFINELSTDT = "SECINSTNOFINELSTDT";//10
		private const string SECINSTNOFINELSTDT_PA  = "@SECINSTNOFINELSTDT";

		
		private const string THIRDINSTNOFINELSTDS = "THIRDINSTNOFINELSTDS";//11
		private const string THIRDINSTNOFINELSTDS_PA  = "@THIRDINSTNOFINELSTDS";

		
		private const string ADDDROPLASTDATEFULL = "ADDDROPLASTDATEFULL";//12
		private const string ADDDROPLASTDATEFULL_PA  = "@ADDDROPLASTDATEFULL";

		
		private const string ADDDROPLASTDATEHALF = "ADDDROPLASTDATEHALF";//13
		private const string ADDDROPLASTDATEHALF_PA  = "@ADDDROPLASTDATEHALF";

		
		private const string LASTDATEENROLLNOFINE = "LASTDATEENROLLNOFINE";//14
		private const string LASTDATEENROLLNOFINE_PA  = "@LASTDATEENROLLNOFINE";

		
		private const string LASTDATEENROLLWFINE = "LASTDATEENROLLWFINE";//15
		private const string LASTDATEENROLLWFINE_PA  = "@LASTDATEENROLLWFINE";

		
		private const string CREATEDBY = "CREATEDBY";//16
		private const string CREATEDBY_PA  = "@CREATEDBY";

		
		private const string MODIFIEDBY = "MODIFIEDBY";//18
		private const string MODIFIEDBY_PA  = "@MODIFIEDBY";

		
		private const string MODIFIEDDATE = "MODIFIEDDATE";//19
		private const string MODIFIEDDATE_PA  = "@MODIFIEDDATE";

		
		private const string ADMISSIONSTARTDATE = "ADMISSIONSTARTDATE";//20
		private const string ADMISSIONSTARTDATE_PA  = "@ADMISSIONSTARTDATE";


        private const string ADMISSIONENDDATE = "ADMISSIONENDDATE";//21
        private const string ADMISSIONENDDATE_PA = "@ADMISSIONENDDATE";

	    private const string ISACTIVEADMISSION = "ISACTIVEADMISSION";
        private const string ISACTIVEADMISSION_PA = "@ISACTIVEADMISSION";

        private const string REGISTRATIONSTARTDATE = "REGISTRATIONSTARTDATE";
        private const string REGISTRATIONSTARTDATE_PA = "@REGISTRATIONSTARTDATE";

        private const string REGISTRATIONENDTDATE = "REGISTRATIONENDDATE";
        private const string REGISTRATIONENDTDATE_PA = "@REGISTRATIONENDDATE";

        private const string ISACTIVEREGISTRATION = "ISACTIVEREGISTRATION";
        private const string ISACTIVEREGISTRATION_PA = "@ISACTIVEREGISTRATION";

		#endregion
	
		#region PKCOLUMNS

	    private const string ALLCOLUMNS = "[" + ACADEMICCALENDERID + "]," +
	                                      "[" + CALENDERUNITTYPEID + "]," +
	                                      "[" + YEAR + "]," +
	                                      "[" + BATCHCODE + "]," +
	                                      "[" + ISCURRENT + "]," +
	                                      "[" + ISNEXT + "]," +
	                                      "[" + STARTDATE + "]," +
	                                      "[" + ENDDATE + "]," +
	                                      "[" + FULLPAYNOFINELSTDT + "]," +
	                                      "[" + FIRSTINSTNOFINELSTDT + "]," +
	                                      "[" + SECINSTNOFINELSTDT + "]," +
	                                      "[" + THIRDINSTNOFINELSTDS + "]," +
	                                      "[" + ADDDROPLASTDATEFULL + "]," +
	                                      "[" + ADDDROPLASTDATEHALF + "]," +
	                                      "[" + LASTDATEENROLLNOFINE + "]," +
	                                      "[" + LASTDATEENROLLWFINE + "]," +
	                                      "[" + ADMISSIONSTARTDATE + "]," +
                                          "[" + ADMISSIONENDDATE + "]," +
	                                      "[" + ISACTIVEADMISSION + "]," +
	                                      "[" + REGISTRATIONSTARTDATE + "]," +
	                                      "[" + REGISTRATIONENDTDATE + "]," +
	                                      "[" + ISACTIVEREGISTRATION + "],";
                                        
		#endregion 
		#region NOPKCOLUMNS
		private const string NOPKCOLUMNS  = "[" + CALENDERUNITTYPEID + "],"+
										"[" + YEAR + "],"+
										"[" + BATCHCODE + "],"+
										"[" + ISCURRENT + "],"+
										"[" + ISNEXT + "],"+
										"[" + STARTDATE + "],"+
										"[" + ENDDATE + "],"+
										"[" + FULLPAYNOFINELSTDT + "],"+
										"[" + FIRSTINSTNOFINELSTDT + "],"+
										"[" + SECINSTNOFINELSTDT + "],"+
										"[" + THIRDINSTNOFINELSTDS + "],"+
										"[" + ADDDROPLASTDATEFULL + "],"+
										"[" + ADDDROPLASTDATEHALF + "],"+
										"[" + LASTDATEENROLLNOFINE + "],"+
										"[" + LASTDATEENROLLWFINE + "],"+
										"[" + ADMISSIONSTARTDATE + "],"+
										"[" + ADMISSIONENDDATE + "],"+
                                        "[" + ISACTIVEADMISSION + "]," +
                                        "[" + REGISTRATIONSTARTDATE + "]," +
                                        "[" + REGISTRATIONENDTDATE + "]," +
                                        "[" + ISACTIVEREGISTRATION + "],";
		#endregion 
		private const string TABLENAME = "ACADEMICCALENDER";
		#region SELECT
		private const string SELECT = "SELECT "
								 + ALLCOLUMNS
								 + BASECOLUMNS
								 + " FROM " + TABLENAME;
		#endregion 
		#region INSERT

		private const string INSERT = "INSERT INTO" + TABLENAME
									+"("
									 + NOPKCOLUMNS
									 + BASECOLUMNS
									 + ")"
									+" VALUES ("
									 + CALENDERUNITTYPEID_PA + ","
									 + YEAR_PA + ","
									 + BATCHCODE_PA + ","
									 + ISCURRENT_PA + ","
									 + ISNEXT_PA + ","
									 + STARTDATE_PA + ","
									 + ENDDATE_PA + ","
									 + FULLPAYNOFINELSTDT_PA + ","
									 + FIRSTINSTNOFINELSTDT_PA + ","
									 + SECINSTNOFINELSTDT_PA + ","
									 + THIRDINSTNOFINELSTDS_PA + ","
									 + ADDDROPLASTDATEFULL_PA + ","
									 + ADDDROPLASTDATEHALF_PA + ","
									 + LASTDATEENROLLNOFINE_PA + ","
									 + LASTDATEENROLLWFINE_PA + ","
									 + ADMISSIONSTARTDATE_PA + ","
									 + ADMISSIONENDDATE_PA + ","
                                     + ISACTIVEADMISSION_PA + ","
                                     + REGISTRATIONSTARTDATE_PA + ","
                                     + REGISTRATIONENDTDATE_PA + ","
                                     + ISACTIVEREGISTRATION_PA + ","
									 + CREATORID_PA + ","
									 + CREATEDDATE_PA + ","
									 + MODIFIERID_PA + ","
									 + MOIDFIEDDATE_PA + ")";
		#endregion 
		#region UPDATE

		private const string UPDATE = "UPDATE" + TABLENAME + "SET"
									 + CALENDERUNITTYPEID + " = " + CALENDERUNITTYPEID_PA + ","
									 + YEAR + " = " + YEAR_PA + ","
									 + BATCHCODE + " = " + BATCHCODE_PA + ","
									 + ISCURRENT + " = " + ISCURRENT_PA + ","
									 + ISNEXT + " = " + ISNEXT_PA + ","
									 + STARTDATE + " = " + STARTDATE_PA + ","
									 + ENDDATE + " = " + ENDDATE_PA + ","
									 + FULLPAYNOFINELSTDT + " = " + FULLPAYNOFINELSTDT_PA + ","
									 + FIRSTINSTNOFINELSTDT + " = " + FIRSTINSTNOFINELSTDT_PA + ","
									 + SECINSTNOFINELSTDT + " = " + SECINSTNOFINELSTDT_PA + ","
									 + THIRDINSTNOFINELSTDS + " = " + THIRDINSTNOFINELSTDS_PA + ","
									 + ADDDROPLASTDATEFULL + " = " + ADDDROPLASTDATEFULL_PA + ","
									 + ADDDROPLASTDATEHALF + " = " + ADDDROPLASTDATEHALF_PA + ","
									 + LASTDATEENROLLNOFINE + " = " + LASTDATEENROLLNOFINE_PA + ","
									 + LASTDATEENROLLWFINE + " = " + LASTDATEENROLLWFINE_PA + ","
									 + CREATEDBY + " = " + CREATEDBY_PA + ","
									 + CREATEDDATE + " = " + CREATEDDATE_PA + ","
									 + MODIFIEDBY + " = " + MODIFIEDBY_PA + ","
									 + MODIFIEDDATE + " = " + MODIFIEDDATE_PA + ","
									 + ADMISSIONSTARTDATE + " = " + ADMISSIONSTARTDATE_PA + ","
									 + ADMISSIONENDDATE + " = " + ADMISSIONENDDATE_PA + ","
									 + CREATORID + " = " + CREATORID_PA + ","
									 + CREATEDDATE + " = " + CREATEDDATE_PA + ","
									 + MODIFIERID + " = " + MODIFIERID_PA + ","
									 + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
		#endregion 
		#region DELETE

		private const string DELETE = "DELETE FROM" + TABLENAME;
		#endregion 
		#region Methods
        private static CcAcademiccalenderEntity Mapper(SQLNullHandler nullHandler)
		{
            CcAcademiccalenderEntity ccAcademiccalenderEntity = new CcAcademiccalenderEntity();

            ccAcademiccalenderEntity.Id = nullHandler.GetInt32("ACADEMICCALENDERID");
            ccAcademiccalenderEntity.Calenderunittypeid = nullHandler.GetInt32("CALENDERUNITTYPEID");
            ccAcademiccalenderEntity.Year = nullHandler.GetInt32("YEAR");
            ccAcademiccalenderEntity.Batchcode = nullHandler.GetString(BATCHCODE);
            ccAcademiccalenderEntity.Startdate = nullHandler.GetDateTime("STARTDATE");
            ccAcademiccalenderEntity.Enddate = nullHandler.GetDateTime("ENDDATE");
            ccAcademiccalenderEntity.Fullpaynofinelstdt = nullHandler.GetDateTime("FULLPAYNOFINELSTDT");
            ccAcademiccalenderEntity.Firstinstnofinelstdt = nullHandler.GetDateTime("FIRSTINSTNOFINELSTDT");
            ccAcademiccalenderEntity.Secinstnofinelstdt = nullHandler.GetDateTime("SECINSTNOFINELSTDT");
            ccAcademiccalenderEntity.Thirdinstnofinelstds = nullHandler.GetDateTime("THIRDINSTNOFINELSTDS");
            ccAcademiccalenderEntity.Adddroplastdatefull = nullHandler.GetDateTime("ADDDROPLASTDATEFULL");
            ccAcademiccalenderEntity.Adddroplastdatehalf = nullHandler.GetDateTime("ADDDROPLASTDATEHALF");
            ccAcademiccalenderEntity.Lastdateenrollnofine = nullHandler.GetDateTime("LASTDATEENROLLNOFINE");
            ccAcademiccalenderEntity.Lastdateenrollwfine = nullHandler.GetDateTime("LASTDATEENROLLWFINE");
            ccAcademiccalenderEntity.CreatorID = nullHandler.GetInt32("CREATEDBY");
            ccAcademiccalenderEntity.CreatedDate = nullHandler.GetDateTime("CREATEDDATE");
            ccAcademiccalenderEntity.ModifierID = nullHandler.GetInt32("MODIFIEDBY");
            ccAcademiccalenderEntity.ModifiedDate = nullHandler.GetDateTime(MODIFIEDDATE);
            ccAcademiccalenderEntity.Admissionstartdate = nullHandler.GetDateTime(ADMISSIONSTARTDATE);
            ccAcademiccalenderEntity.Admissionenddate = nullHandler.GetDateTime(ADMISSIONENDDATE);
            ccAcademiccalenderEntity.Isactiveadmission = nullHandler.GetBoolean(ISACTIVEADMISSION);
            ccAcademiccalenderEntity.Registrationstartdate = nullHandler.GetDateTime(REGISTRATIONSTARTDATE);
            ccAcademiccalenderEntity.Registrationenddate = nullHandler.GetDateTime(REGISTRATIONENDTDATE);
            ccAcademiccalenderEntity.Isactiveregistration = nullHandler.GetBoolean(ISACTIVEREGISTRATION);

            return ccAcademiccalenderEntity;
		}//end of method Mapper()

        private static List<CcAcademiccalenderEntity> Maps(SqlDataReader theReader)
		{ 
			SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<CcAcademiccalenderEntity> ccAcademiccalenderEntities = null;
			while(theReader.Read())
			{
                if (ccAcademiccalenderEntities == null)
				{
                    ccAcademiccalenderEntities = new List<CcAcademiccalenderEntity>();
				}
                CcAcademiccalenderEntity uiuemsCcAcademiccalenderEntity = Mapper(nullHandler);
                ccAcademiccalenderEntities.Add(uiuemsCcAcademiccalenderEntity);
			}

            return ccAcademiccalenderEntities;
		}//end of method Map()


        private static CcAcademiccalenderEntity Map(SqlDataReader theReader)
		{ 
			SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            CcAcademiccalenderEntity uiuemsCcAcademiccalenderEntity = null;
			if(theReader.Read())
			{
                uiuemsCcAcademiccalenderEntity = new CcAcademiccalenderEntity();
				uiuemsCcAcademiccalenderEntity = Mapper(nullHandler);
			}

			return uiuemsCcAcademiccalenderEntity;
		}//end of method Map()



        private static List<SqlParameter> MakeSqlParameterList(CcAcademiccalenderEntity ccAcademiccalenderEntitiy)
		{
			DAOParameters dps = new DAOParameters();

            dps.AddParameter(CALENDERUNITTYPEID_PA, ccAcademiccalenderEntitiy.Calenderunittypeid);
            dps.AddParameter(YEAR_PA, ccAcademiccalenderEntitiy.Year);
            dps.AddParameter(BATCHCODE_PA, ccAcademiccalenderEntitiy.Batchcode);
            dps.AddParameter(ISCURRENT_PA, ccAcademiccalenderEntitiy.Iscurrent);
            dps.AddParameter(ISNEXT_PA, ccAcademiccalenderEntitiy.Isnext);
            dps.AddParameter(STARTDATE_PA, ccAcademiccalenderEntitiy.Startdate);
            dps.AddParameter(ENDDATE_PA, ccAcademiccalenderEntitiy.Enddate);
            dps.AddParameter(FULLPAYNOFINELSTDT_PA, ccAcademiccalenderEntitiy.Fullpaynofinelstdt);
            dps.AddParameter(FIRSTINSTNOFINELSTDT_PA, ccAcademiccalenderEntitiy.Firstinstnofinelstdt);
            dps.AddParameter(SECINSTNOFINELSTDT_PA, ccAcademiccalenderEntitiy.Secinstnofinelstdt);
            dps.AddParameter(THIRDINSTNOFINELSTDS_PA, ccAcademiccalenderEntitiy.Thirdinstnofinelstds);
            dps.AddParameter(ADDDROPLASTDATEFULL_PA, ccAcademiccalenderEntitiy.Adddroplastdatefull);
            dps.AddParameter(ADDDROPLASTDATEHALF_PA, ccAcademiccalenderEntitiy.Adddroplastdatehalf);
            dps.AddParameter(LASTDATEENROLLNOFINE_PA, ccAcademiccalenderEntitiy.Lastdateenrollnofine);
            dps.AddParameter(LASTDATEENROLLWFINE_PA, ccAcademiccalenderEntitiy.Lastdateenrollwfine);

            dps.AddParameter(ADMISSIONSTARTDATE_PA, ccAcademiccalenderEntitiy.Admissionstartdate);
            dps.AddParameter(ADMISSIONENDDATE_PA, ccAcademiccalenderEntitiy.Admissionenddate);
            dps.AddParameter(ISACTIVEADMISSION_PA, ccAcademiccalenderEntitiy.Isactiveadmission);
            dps.AddParameter(REGISTRATIONSTARTDATE_PA, ccAcademiccalenderEntitiy.Registrationstartdate);
            dps.AddParameter(REGISTRATIONENDTDATE_PA, ccAcademiccalenderEntitiy.Registrationenddate);
            dps.AddParameter(ISACTIVEREGISTRATION_PA, ccAcademiccalenderEntitiy.Isactiveregistration);
            dps.AddParameter(CREATORID_PA, ccAcademiccalenderEntitiy.CreatorID);
            dps.AddParameter(CREATEDDATE_PA, ccAcademiccalenderEntitiy.CreatedDate);
            dps.AddParameter(MODIFIERID_PA, ccAcademiccalenderEntitiy.ModifierID);
            dps.AddParameter(MOIDFIEDDATE_PA, ccAcademiccalenderEntitiy.ModifiedDate);
			List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
			return ps;
		}

        public static int save(List<CcAcademiccalenderEntity> uiuemsCcAcademiccalenderEntities)
		{
			try
			{
				int counter = 0;
				MSSqlConnectionHandler.GetConnection();
				MSSqlConnectionHandler.StartTransaction();
				string cmd = "@return_value int,@result int"
						+"EXEC @return_value = [dbo].[sp_]"
						//Add the required Parameters here
					+"SELECT	@result as '@result' ";
                foreach (CcAcademiccalenderEntity uiuemsCcAcademiccalenderEntity in uiuemsCcAcademiccalenderEntities)
				{
                    CcAcademiccalenderEntity tempEntity = new CcAcademiccalenderEntity();
					//Assign the Paramerter here like empEntity.ID = uiuemsCcAcademiccalenderEntity.ID;


					counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(tempEntity));
				}
				MSSqlConnectionHandler.CommitTransaction();
				MSSqlConnectionHandler.CloseDbConnection();
				return counter;
			}
			catch(Exception ex)
			{
				MSSqlConnectionHandler.RollBackAndClose();
				throw ex;
			}
		}

        //internal static int delete (int ID)
        //{
        //    int counter = 0;
        //    try
        //    {
        //        string cmd = DELETE +"WHERE Academiccalenderid = "+CALENDERUNITTYPEID_PA;
        //        DAOParameters dps = new DAOParameters();
        //        dps.AddParameter( , ID);
        //        List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
        //        counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return counter;
        //}

		public static CcAcademiccalenderEntity GetNextAcademicCalenderEntity ()
		{
			try
			{
				CcAcademiccalenderEntity ccAcademiccalenderEntity = null;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                string cmd = SELECT + " WHERE ISNEXT = 1";
				
				//List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                ccAcademiccalenderEntity = Map(dr);
                return ccAcademiccalenderEntity;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        public static CcAcademiccalenderEntity GetCurrentAcademicCalenderEntity()
        {
            try
            {
                CcAcademiccalenderEntity ccAcademiccalenderEntity = null;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                string cmd = SELECT + " WHERE ISCURRENT = 1";

                //List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                ccAcademiccalenderEntity = Map(dr);
                return ccAcademiccalenderEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		#endregion 
	}

}//End Of Namespace DataAccess
