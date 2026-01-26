using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class GradeDetails_DAO : Base_DAO
	{
		#region Constants
		
		private const string GRADEID = "GradeId";//0

		
		private const string ACACALID = "AcaCalId";//1
		private const string ACACALID_PA  = "@AcaCalId";

		
		private const string PROGRAMID = "ProgramId";//2
		private const string PROGRAMID_PA  = "@ProgramId";

		
		private const string GRADE = "Grade";//3
		private const string GRADE_PA  = "@Grade";

		
		private const string RETAKEDISCOUNT = "RetakeDiscount";//4
		private const string RETAKEDISCOUNT_PA  = "@RetakeDiscount";

		
		private const string GRADEPOINT = "GradePoint";//5
		private const string GRADEPOINT_PA  = "@GradePoint";

		
		private const string MINMARKS = "MinMarks";//6
		private const string MINMARKS_PA  = "@MinMarks";

		
		private const string MAXMARKS = "MaxMarks";//7
		private const string MAXMARKS_PA  = "@MaxMarks";


		#endregion
	
		#region PKCOLUMNS
		private const string ALLCOLUMNS  =  "[" + GRADEID + "],"+
										    "[" + ACACALID + "],"+
										    "[" + PROGRAMID + "],"+
										    "[" + GRADE + "],"+
										    "[" + RETAKEDISCOUNT + "],"+
										    "[" + GRADEPOINT + "],"+
										    "[" + MINMARKS + "],"+
										    "[" + MAXMARKS + "],";
		#endregion 
		#region NOPKCOLUMNS
        private const string NOPKCOLUMNS =  "[" + ACACALID + "]," +
                                            "[" + PROGRAMID + "]," +
                                            "[" + GRADE + "]," +
                                            "[" + RETAKEDISCOUNT + "]," +
                                            "[" + GRADEPOINT + "]," +
                                            "[" + MINMARKS + "]," +
                                            "[" + MAXMARKS + "],";
		#endregion 
		private const string TABLENAME = " GradeDetails ";
		#region SELECT
		private const string SELECT = "SELECT "
								 + ALLCOLUMNS
								 + BASECOLUMNS
								 + "FROM " + TABLENAME;
		#endregion 
		#region INSERT

		private const string INSERT = " INSERT INTO " + TABLENAME
									+"("
									 + NOPKCOLUMNS
									 + BASEMUSTCOLUMNS
									 + ")"
									+" VALUES ("
									 + ACACALID_PA + ","
									 + PROGRAMID_PA + ","
									 + GRADE_PA + ","
									 + RETAKEDISCOUNT_PA + ","
									 + GRADEPOINT_PA + ","
									 + MINMARKS_PA + ","
									 + MAXMARKS_PA + ","
									 + CREATORID_PA + ","
									 + CREATEDDATE_PA + ")";
		#endregion 
		#region UPDATE

		private const string UPDATE = " UPDATE " + TABLENAME + " SET "
									 + ACACALID + " = " + ACACALID_PA + ","
									 + PROGRAMID + " = " + PROGRAMID_PA + ","
									 + GRADE + " = " + GRADE_PA + ","
									 + RETAKEDISCOUNT + " = " + RETAKEDISCOUNT_PA + ","
									 + GRADEPOINT + " = " + GRADEPOINT_PA + ","
									 + MINMARKS + " = " + MINMARKS_PA + ","
									 + MAXMARKS + " = " + MAXMARKS_PA + ","
									 + MODIFIERID + " = " + MODIFIERID_PA + ","
									 + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
		#endregion 
		#region DELETE

		private const string DELETE = "DELETE FROM" + TABLENAME;
		#endregion 
		#region Methods
		private static GradeDetailsEntity Mapper(SQLNullHandler nullHandler)
		{
			GradeDetailsEntity gradeDetailsEntity = new GradeDetailsEntity();
			
			gradeDetailsEntity.Id = nullHandler.GetInt32("GradeId");
            gradeDetailsEntity.Gradeid = nullHandler.GetInt32("GradeId");
            gradeDetailsEntity.Grade = nullHandler.GetString("Grade");
			gradeDetailsEntity.Acacalid = nullHandler.GetInt32("AcaCalId");
			gradeDetailsEntity.Programid = nullHandler.GetInt32("ProgramId");
			gradeDetailsEntity.Minmarks = nullHandler.GetInt32("MinMarks");
            gradeDetailsEntity.Maxmarks = nullHandler.GetInt32("Maxmarks");
            gradeDetailsEntity.Gradepoint = nullHandler.GetDecimal("Gradepoint");
            gradeDetailsEntity.Retakediscount = nullHandler.GetDecimal("Retakediscount");

			return gradeDetailsEntity;
		}//end of method Mapper()

		private static List<GradeDetailsEntity> Maps (SqlDataReader theReader)
		{ 
			SQLNullHandler nullHandler = new SQLNullHandler(theReader);
			List<GradeDetailsEntity> gradedetailsEntities = null;
			while(theReader.Read())
			{
				if(gradedetailsEntities == null)
				{
					gradedetailsEntities = new List<GradeDetailsEntity>();
				}
				GradeDetailsEntity gradeDetailsEntity = Mapper (nullHandler);
					gradedetailsEntities.Add(gradeDetailsEntity);
			}

			return gradedetailsEntities;
		}//end of method Map()


		private static GradeDetailsEntity Map(SqlDataReader theReader)
		{ 
			SQLNullHandler nullHandler = new SQLNullHandler(theReader);
			GradeDetailsEntity GradeDetailsEntity = null;
			if(theReader.Read())
			{
				GradeDetailsEntity = new GradeDetailsEntity();
				GradeDetailsEntity = Mapper(nullHandler);
			}

			return GradeDetailsEntity;
		}//end of method Map()


		
		private static List<SqlParameter> MakeSqlParameterList(GradeDetailsEntity GradeDetailsEntity)
		{
			DAOParameters dps = new DAOParameters();
			//dps.AddParameter(AcaCalId_PA,GradeDetailsEntity.Gradeid);
			//dps.AddParameter(ProgramId_PA,GradeDetailsEntity.Acacalid);
			//dps.AddParameter(Grade_PA,GradeDetailsEntity.Programid);
			//dps.AddParameter(RetakeDiscount_PA,GradeDetailsEntity.Grade);
			//dps.AddParameter(GradePoint_PA,GradeDetailsEntity.Retakediscount);
			//dps.AddParameter(MinMarks_PA,GradeDetailsEntity.Gradepoint);
			//dps.AddParameter(CREATORID_PA,GradeDetailsEntity.Creatorid);
			//dps.AddParameter(CREATEDDATE_PA,GradeDetailsEntity.Createddate);
			//dps.AddParameter(MODIFIERID_PA,GradeDetailsEntity.Modifierid);
			//dps.AddParameter(MOIDFIEDDATE_PA,GradeDetailsEntity.Moidfieddate);
			List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
			return ps;
		}

        public static int save(List<GradeDetailsEntity> gradedetailsEntities, int creatorId, int acaCalId, int programId)
		{
			try
			{
				int counter = 0;
				MSSqlConnectionHandler.GetConnection();
				MSSqlConnectionHandler.StartTransaction();
				string cmd = INSERT;

                foreach (GradeDetailsEntity entity in gradedetailsEntities)
				{
                    DAOParameters dps = new DAOParameters();
                    dps.AddParameter(ACACALID_PA, acaCalId);
                    dps.AddParameter(PROGRAMID_PA, programId);
                    dps.AddParameter(GRADE_PA, entity.Grade);
                    dps.AddParameter(RETAKEDISCOUNT_PA, entity.Retakediscount);
                    dps.AddParameter(GRADEPOINT_PA, entity.Gradepoint);
                    dps.AddParameter(MINMARKS_PA, entity.Minmarks);
                    dps.AddParameter(MAXMARKS_PA, entity.Maxmarks);
                    dps.AddParameter(CREATORID_PA, creatorId);
                    dps.AddParameter(CREATEDDATE_PA, DateTime.Now);
                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

					counter += QueryHandler.ExecuteSelectBatchAction(cmd, ps);
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

        public static int Delete(int id)
		{
			int counter = 0;
			try
			{
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

				string cmd = DELETE +" WHERE Gradeid = "+ ID_PA;
				DAOParameters dps = new DAOParameters();
				dps.AddParameter(ID_PA , id);
				List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
				counter = QueryHandler. ExecuteDeleteBatchAction(cmd, ps);
                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
			}
			catch(Exception ex)
			{
                MSSqlConnectionHandler.RollBackAndClose();
				throw ex;
			}
			return counter;
		}

		public static List<GradeDetailsEntity> GetCandidateStudents (int ID)
		{
			try
			{
				List<GradeDetailsEntity> uiuemsCcGradedetailsEntities = null;
				string cmd = SELECT +"WHERE Gradeid = "+ACACALID_PA;
				DAOParameters dps = new DAOParameters();
				//dps.AddParameter( , );
				List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
				SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
				uiuemsCcGradedetailsEntities = Maps(dr);
				return uiuemsCcGradedetailsEntities;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		#endregion 
	
    
        public static List<GradeDetailsEntity> Load(int acaCalId, int programId)
        {
            try
            {
                List<GradeDetailsEntity> gradedetailsEntities = null;
                string cmd = SELECT + " WHERE AcaCalId = " + ACACALID_PA + " and ProgramId = " + PROGRAMID_PA + " ORDER BY GRADE ";
                DAOParameters dps = new DAOParameters();
                dps.AddParameter(ACACALID_PA , acaCalId);
                dps.AddParameter(PROGRAMID_PA, programId);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
                gradedetailsEntities = Maps(dr);
                dr.Close();
                return gradedetailsEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static int Update(GradeDetailsEntity entity)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();
                string cmd = UPDATE + " WHERE Gradeid = " + ID_PA;
                
                    DAOParameters dps = new DAOParameters();
                    dps.AddParameter(ID_PA, entity.Id);
                    dps.AddParameter(ACACALID_PA, entity.Acacalid);
                    dps.AddParameter(PROGRAMID_PA, entity.Programid);
                    dps.AddParameter(GRADE_PA, entity.Grade);
                    dps.AddParameter(RETAKEDISCOUNT_PA, entity.Retakediscount);
                    dps.AddParameter(GRADEPOINT_PA, entity.Gradepoint);
                    dps.AddParameter(MINMARKS_PA, entity.Minmarks);
                    dps.AddParameter(MAXMARKS_PA, entity.Maxmarks);
                    dps.AddParameter(MODIFIERID_PA, entity.ModifierID);
                    dps.AddParameter(MOIDFIEDDATE_PA, entity.ModifiedDate);
                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);
               
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

        public static int CheckDuplicate(int acaCalId, int programId)
        {
            int counter = 0;
            try
            {
                //MSSqlConnectionHandler.GetConnection();
                //MSSqlConnectionHandler.StartTransaction();

                string cmd = " select count(*)'count' from  dbo.GradeDetails where AcaCalId = " + acaCalId + " and ProgramId = " + programId + " ";
                counter = QueryHandler.MSSqlExecuteScalar(cmd, MSSqlConnectionHandler.GetConnection());

                //MSSqlConnectionHandler.CommitTransaction();
                //MSSqlConnectionHandler.CloseDbConnection();
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
            return counter;
        }
    }

}//End Of Namespace DataAccess
