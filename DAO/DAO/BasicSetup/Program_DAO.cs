using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
	public class rbProgram_DAO : Base_DAO
	{
		#region Constants
		
		private const string PROGRAMID = "PROGRAMID";//0
	    private const string PROGRAMID_PA = "@PROGRAMID";

		
		private const string CODE = "CODE";//1
		private const string CODE_PA  = "@CODE";

		
		private const string SHORTNAME = "SHORTNAME";//2
		private const string SHORTNAME_PA  = "@SHORTNAME";

		
		private const string TOTALCREDIT = "TOTALCREDIT";//3
		private const string TOTALCREDIT_PA  = "@TOTALCREDIT";

		
		private const string PROGRAMTYPEID = "PROGRAMTYPEID";//4
		private const string PROGRAMTYPEID_PA  = "@PROGRAMTYPEID";

		
		private const string DEPTID = "DEPTID";//5
		private const string DEPTID_PA  = "@DEPTID";

		
		private const string DETAILNAME = "DETAILNAME";//6
		private const string DETAILNAME_PA  = "@DETAILNAME";


		#endregion
	
		#region PKCOLUMNS
		private const string ALLCOLUMNS  = "[" + PROGRAMID + "],"+
										"[" + CODE + "],"+
										"[" + SHORTNAME + "],"+
										"[" + TOTALCREDIT + "],"+
										"[" + PROGRAMTYPEID + "],"+
										"[" + DEPTID + "],"+
										"[" + DETAILNAME + "],";
		#endregion 
		#region NOPKCOLUMNS
		private const string NOPKCOLUMNS  = "[" + CODE + "],"+
										"[" + SHORTNAME + "],"+
										"[" + TOTALCREDIT + "],"+
										"[" + PROGRAMTYPEID + "],"+
										"[" + DEPTID + "],"+
										"[" + DETAILNAME + "],";
		#endregion 
		private const string TABLENAME = "PROGRAM";
		#region SELECT
		private const string SELECT = "SELECT "
								 + ALLCOLUMNS
                                 + BASECOLUMNS
								 + "FROM " + TABLENAME;
		#endregion 
		#region INSERT

		private const string INSERT = "INSERT INTO" + TABLENAME
									+"("
									 + NOPKCOLUMNS
									 + BASECOLUMNS
									 + ")"
									+" VALUES ("
									 + CODE_PA + ","
									 + SHORTNAME_PA + ","
									 + TOTALCREDIT_PA + ","
									 + PROGRAMTYPEID_PA + ","
									 + DEPTID_PA + ","
									 + DETAILNAME_PA + ","
									 + CREATORID_PA + ","
									 + CREATEDDATE_PA + ","
									 + MODIFIERID_PA + ","
									 + MOIDFIEDDATE_PA + ")";
		#endregion 
		#region UPDATE

		private const string UPDATE = "UPDATE" + TABLENAME + "SET"
									 + CODE + " = " + CODE_PA + ","
									 + SHORTNAME + " = " + SHORTNAME_PA + ","
									 + TOTALCREDIT + " = " + TOTALCREDIT_PA + ","
									 + PROGRAMTYPEID + " = " + PROGRAMTYPEID_PA + ","
									 + DEPTID + " = " + DEPTID_PA + ","
									 + DETAILNAME + " = " + DETAILNAME_PA + ","
									 + CREATORID + " = " + CREATORID_PA + ","
									 + CREATEDDATE + " = " + CREATEDDATE_PA + ","
									 + MODIFIERID + " = " + MODIFIERID_PA + ","
									 + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
		#endregion 
		#region DELETE

		private const string DELETE = "DELETE FROM" + TABLENAME;
		#endregion 
		#region Methods
		private static RbProgramEntity Mapper(SQLNullHandler nullHandler)
		{
			RbProgramEntity rbProgramEntity = new RbProgramEntity();
			
			rbProgramEntity.Id = nullHandler.GetInt32("PROGRAMID");
			rbProgramEntity.Code = nullHandler.GetString("CODE");
			rbProgramEntity.Shortname = nullHandler.GetString("SHORTNAME");
			rbProgramEntity.Programtypeid = nullHandler.GetInt32("PROGRAMTYPEID");
			rbProgramEntity.Deptid = nullHandler.GetInt32("DEPTID");

			return rbProgramEntity;
		}//end of method Mapper()

		private static List<RbProgramEntity> Maps (SqlDataReader theReader)
		{ 
			SQLNullHandler nullHandler = new SQLNullHandler(theReader);
			List<RbProgramEntity> uiuemsCcProgramEntities = null;
			while(theReader.Read())
			{
				if(uiuemsCcProgramEntities == null)
				{
					uiuemsCcProgramEntities = new List<RbProgramEntity>();
				}
				RbProgramEntity rbProgramEntity = Mapper (nullHandler);
					uiuemsCcProgramEntities.Add(rbProgramEntity);
			}

			return uiuemsCcProgramEntities;
		}//end of method Map()


		private static RbProgramEntity Map(SqlDataReader theReader)
		{ 
			SQLNullHandler nullHandler = new SQLNullHandler(theReader);
			RbProgramEntity rbProgramEntity = null;
			if(theReader.Read())
			{
				rbProgramEntity = new RbProgramEntity();
				rbProgramEntity = Mapper(nullHandler);
			}

			return rbProgramEntity;
		}//end of method Map()


		
		private static List<SqlParameter> MakeSqlParameterList(RbProgramEntity rbProgramEntity)
		{
			DAOParameters dps = new DAOParameters();
			//dps.AddParameter(CODE_PA,uiuemsCcProgramEntity.Programid);
			//dps.AddParameter(SHORTNAME_PA,uiuemsCcProgramEntity.Code);
			//dps.AddParameter(TOTALCREDIT_PA,uiuemsCcProgramEntity.Shortname);
            dps.AddParameter(PROGRAMTYPEID_PA, rbProgramEntity.Totalcredit);
			//dps.AddParameter(DEPTID_PA,uiuemsCcProgramEntity.Programtypeid);
			//dps.AddParameter(CREATORID_PA,uiuemsCcProgramEntity.Creatorid);
			//dps.AddParameter(CREATEDDATE_PA,uiuemsCcProgramEntity.Createddate);
			//dps.AddParameter(MODIFIERID_PA,uiuemsCcProgramEntity.Modifierid);
			//dps.AddParameter(MOIDFIEDDATE_PA,uiuemsCcProgramEntity.Moidfieddate);
			List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
			return ps;
		}

		public static int save (List<RbProgramEntity> uiuemsCcProgramEntities)
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
				foreach(RbProgramEntity uiuemsCcProgramEntity in uiuemsCcProgramEntities)
				{
					RbProgramEntity tempEntity = new RbProgramEntity();
					//Assign the Paramerter here like empEntity.ID = uiuemsCcProgramEntity.ID;


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
        //        string cmd = DELETE +"WHERE Programid = "+PROGRAMID;
        //        DAOParameters dps = new DAOParameters();
        //        dps.AddParameter(PROGRAMID , ID);
        //        List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
        //        counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return counter;
        //}

		public static List<RbProgramEntity> GetPrograms ()
		{
			try
			{
				List<RbProgramEntity> CcProgramEntities = null;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
			    string cmd = SELECT;

                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                CcProgramEntities = Maps(dr);
			    dr.Close();
                return CcProgramEntities;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        public static RbProgramEntity GetCcProgramEntityByID(int programId)
        {
            RbProgramEntity rbProgramEntity = null;
            try
            {
                
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                string cmd = SELECT +" WHERE PROGRAMID = "+PROGRAMID_PA;
                DAOParameters dps = new DAOParameters();

                dps.AddParameter(PROGRAMID_PA, programId);

                List<SqlParameter> ps = Methods.GetSQLParameters(dps);

                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, ps, sqlConnection);
                rbProgramEntity = Map(dr);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return rbProgramEntity;
        }

	    #endregion 
	}

}//End Of Namespace DataAccess
