using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class Studentdiscountworksheet_DAO : Base_DAO
    {
        #region Constants

        private const string STDDISCOUNTWORKSHEETID = "StdDiscountWorksheetID";//0


        private const string STUDENTID = "StudentID";//1
        private const string STUDENTID_PA = "@StudentID";


        private const string TOTALCRREGINPREVIOUSSESSION = "TotalCrRegInPreviousSession";//2
        private const string TOTALCRREGINPREVIOUSSESSION_PA = "@TotalCrRegInPreviousSession";


        private const string GPAINPREVIOUSSESSION = "GPAinPreviousSession";//3
        private const string GPAINPREVIOUSSESSION_PA = "@GPAinPreviousSession";


        private const string CGPAUPTOPREVIOUSSAESSION = "CGPAupToPreviousSaession";//4
        private const string CGPAUPTOPREVIOUSSAESSION_PA = "@CGPAupToPreviousSaession";


        private const string TOTALCRREGINCURRENTSESSION = "TotalCrRegInCurrentSession";//5
        private const string TOTALCRREGINCURRENTSESSION_PA = "@TotalCrRegInCurrentSession";


        private const string DISCOUNTTYPEID = "DiscountTypeId";//6
        private const string DISCOUNTTYPEID_PA = "@DiscountTypeId";


        private const string DISCOUNTPERCENTAGE = "DiscountPercentage";//7
        private const string DISCOUNTPERCENTAGE_PA = "@DiscountPercentage";


        #endregion

        #region PKCOLUMNS
        private const string ALLCOLUMNS = "[" + STDDISCOUNTWORKSHEETID + "]," +
                                        "[" + STUDENTID + "]," +
                                        "[" + TOTALCRREGINPREVIOUSSESSION + "]," +
                                        "[" + GPAINPREVIOUSSESSION + "]," +
                                        "[" + CGPAUPTOPREVIOUSSAESSION + "]," +
                                        "[" + TOTALCRREGINCURRENTSESSION + "]," +
                                        "[" + DISCOUNTTYPEID + "]," +
                                        "[" + DISCOUNTPERCENTAGE + "],";
        #endregion
        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + STUDENTID + "]," +
                                        "[" + TOTALCRREGINPREVIOUSSESSION + "]," +
                                        "[" + GPAINPREVIOUSSESSION + "]," +
                                        "[" + CGPAUPTOPREVIOUSSAESSION + "]," +
                                        "[" + TOTALCRREGINCURRENTSESSION + "]," +
                                        "[" + DISCOUNTTYPEID + "]," +
                                        "[" + DISCOUNTPERCENTAGE + "],";
        #endregion
        private const string TABLENAME = "StudentDiscountWorkSheet";
        #region SELECT
        private const string SELECT = "SELECT "
                                 + ALLCOLUMNS
                                 + BASECOLUMNS
                                 + "FROM " + TABLENAME;
        #endregion
        #region INSERT

        private const string INSERT = "INSERT INTO" + TABLENAME
                                    + "("
                                     + NOPKCOLUMNS
                                     + BASECOLUMNS
                                     + ")"
                                    + " VALUES ("
                                     + STUDENTID_PA + ","
                                     + TOTALCRREGINPREVIOUSSESSION_PA + ","
                                     + GPAINPREVIOUSSESSION_PA + ","
                                     + CGPAUPTOPREVIOUSSAESSION_PA + ","
                                     + TOTALCRREGINCURRENTSESSION_PA + ","
                                     + DISCOUNTTYPEID_PA + ","
                                     + DISCOUNTPERCENTAGE_PA + ","
                                     + CREATORID_PA + ","
                                     + CREATEDDATE_PA + ","
                                     + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE_PA + ")";
        #endregion
        #region UPDATE

        private const string UPDATE = "UPDATE" + TABLENAME + "SET"
                                     + STUDENTID + " = " + STUDENTID_PA + ","
                                     + TOTALCRREGINPREVIOUSSESSION + " = " + TOTALCRREGINPREVIOUSSESSION_PA + ","
                                     + GPAINPREVIOUSSESSION + " = " + GPAINPREVIOUSSESSION_PA + ","
                                     + CGPAUPTOPREVIOUSSAESSION + " = " + CGPAUPTOPREVIOUSSAESSION_PA + ","
                                     + TOTALCRREGINCURRENTSESSION + " = " + TOTALCRREGINCURRENTSESSION_PA + ","
                                     + DISCOUNTTYPEID + " = " + DISCOUNTTYPEID_PA + ","
                                     + DISCOUNTPERCENTAGE + " = " + DISCOUNTPERCENTAGE_PA + ","
                                     + CREATORID + " = " + CREATORID_PA + ","
                                     + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                                     + MODIFIERID + " = " + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion
        #region DELETE

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        #region Methods
        private static StudentDiscountWorksheetEntity Mapper(SQLNullHandler nullHandler)
        {
            StudentDiscountWorksheetEntity entity = new StudentDiscountWorksheetEntity();

            entity.Id = nullHandler.GetInt32("StdDiscountWorksheetID");
            entity.StudentId = nullHandler.GetInt32("StudentID");
            entity.Roll = nullHandler.GetString("Roll");
            entity.Name = nullHandler.GetString("Name");
            entity.ProgramId = nullHandler.GetInt32("ProgramID");
            entity.AcacalId = nullHandler.GetInt32("AcaCalID");
            entity.AdmissionCalId = nullHandler.GetInt32("AdmissionCalId");
            entity.TotalCrRegInpreviousSession = nullHandler.GetDecimal("TotalCrRegInPreviousSession");
            entity.GpaInPreviousSession = nullHandler.GetDecimal("GPAinPreviousSession");
            entity.CgpaUptoPreviousSession = nullHandler.GetDecimal("CGPAupToPreviousSession");
            entity.TotalCrRegInCurrentSession = nullHandler.GetDecimal("TotalCrRegInCurrentSession");
            entity.DiscountTypeId = nullHandler.GetInt32("DiscountTypeId");
            entity.Discountpercentage = nullHandler.GetDecimal("DiscountPercentage");
            entity.Remarks = nullHandler.GetString("Remarks");

            return entity;
        }//end of method Mapper()

        private static List<StudentDiscountWorksheetEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<StudentDiscountWorksheetEntity> studentdiscountworksheetEntities = null;
            while (theReader.Read())
            {
                if (studentdiscountworksheetEntities == null)
                {
                    studentdiscountworksheetEntities = new List<StudentDiscountWorksheetEntity>();
                }
                StudentDiscountWorksheetEntity studentDiscountWorksheetEntity = Mapper(nullHandler);
                studentdiscountworksheetEntities.Add(studentDiscountWorksheetEntity);
            }

            return studentdiscountworksheetEntities;
        }//end of method Map()


        private static StudentDiscountWorksheetEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            StudentDiscountWorksheetEntity uiuemsBlStudentDiscountWorksheetEntity = null;
            if (theReader.Read())
            {
                uiuemsBlStudentDiscountWorksheetEntity = new StudentDiscountWorksheetEntity();
                uiuemsBlStudentDiscountWorksheetEntity = Mapper(nullHandler);
            }

            return uiuemsBlStudentDiscountWorksheetEntity;
        }//end of method Map()



        private static List<SqlParameter> MakeSqlParameterList(StudentDiscountWorksheetEntity uiuemsBlStudentDiscountWorksheetEntity)
        {
            DAOParameters dps = new DAOParameters();
            //dps.AddParameter(StudentID_PA,uiuemsBlStudentDiscountWorksheetEntity.Stddiscountworksheetid);
            //dps.AddParameter(TotalCrRegInPreviousSession_PA,uiuemsBlStudentDiscountWorksheetEntity.Studentid);
            //dps.AddParameter(GPAinPreviousSession_PA,uiuemsBlStudentDiscountWorksheetEntity.Totalcrreginprevioussession);
            //dps.AddParameter(CGPAupToPreviousSaession_PA,uiuemsBlStudentDiscountWorksheetEntity.Gpainprevioussession);
            //dps.AddParameter(TotalCrRegInCurrentSession_PA,uiuemsBlStudentDiscountWorksheetEntity.Cgpauptoprevioussaession);
            //dps.AddParameter(DiscountTypeId_PA,uiuemsBlStudentDiscountWorksheetEntity.Totalcrregincurrentsession);
            //dps.AddParameter(CREATORID_PA,uiuemsBlStudentDiscountWorksheetEntity.Creatorid);
            //dps.AddParameter(CREATEDDATE_PA,uiuemsBlStudentDiscountWorksheetEntity.Createddate);
            //dps.AddParameter(MODIFIERID_PA,uiuemsBlStudentDiscountWorksheetEntity.Modifierid);
            //dps.AddParameter(MOIDFIEDDATE_PA,uiuemsBlStudentDiscountWorksheetEntity.Moidfieddate);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }

        public static int save(List<StudentDiscountWorksheetEntity> uiuemsBlStudentdiscountworksheetEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();
                string cmd = "@return_value int,@result int"
                        + "EXEC @return_value = [dbo].[sp_]"
                    //Add the required Parameters here
                    + "SELECT	@result as '@result' ";
                foreach (StudentDiscountWorksheetEntity uiuemsBlStudentDiscountWorksheetEntity in uiuemsBlStudentdiscountworksheetEntities)
                {
                    StudentDiscountWorksheetEntity tempEntity = new StudentDiscountWorksheetEntity();
                    //Assign the Paramerter here like empEntity.ID = uiuemsBlStudentDiscountWorksheetEntity.ID;


                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(tempEntity));
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

        internal static int delete(int ID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + "WHERE Stddiscountworksheetid = " + STUDENTID_PA;
                DAOParameters dps = new DAOParameters();
                //dps.AddParameter( , ID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }

        public static List<StudentDiscountWorksheetEntity> GetCandidateStudents(int ID)
        {
            try
            {
                List<StudentDiscountWorksheetEntity> studentdiscountworksheetEntities = null;
                string cmd = SELECT + "WHERE Stddiscountworksheetid = " + STUDENTID_PA;
                DAOParameters dps = new DAOParameters();
                //dps.AddParameter( , );
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
                studentdiscountworksheetEntities = Maps(dr);
                return studentdiscountworksheetEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public static List<StudentDiscountWorksheetEntity> LoadDAta(int programId, int batchId, int studentId)
        {
            try
            {
                List<StudentDiscountWorksheetEntity> studentdiscountworksheetEntities = null;
                string cmd = "select s.roll, isnull(p.FirstName,'') +' '+ isnull(p.MiddleName,'') +' '+ isnull(p.LastName,'') as Name, SDW.* from StudentDiscountWorkSheet as SDW inner join Student as S on s.StudentID = SDW.StudentID inner join Person as p on p.PersonID = s.PersonID where SDw.ProgramID = " + programId ;

                if(batchId != 0)
                {
                    cmd += " and SDw.AdmissionCalId=  " + batchId;
                }
                if(studentId!=0)
                {
                    cmd += " and SDW.StudentID = " + studentId;
                }
                //DAOParameters dps = new DAOParameters();
                ////dps.AddParameter( , );
                //List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, MSSqlConnectionHandler.GetConnection());
                studentdiscountworksheetEntities = Maps(dr);
                return studentdiscountworksheetEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Save(List<StudentDiscountWorksheetEntity> sdwEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                int effectedRows = Delete(sdwEntities);

                string cmd = @"INSERT INTO StudentDiscountWorkSheet
                                   (StudentID
                                   ,ProgramID
                                   ,AcaCalID
                                   ,AdmissionCalId
                                   ,TotalCrRegInPreviousSession
                                   ,GPAinPreviousSession
                                   ,CGPAupToPreviousSession
                                   ,TotalCrRegInCurrentSession
                                   ,DiscountTypeId
                                   ,DiscountPercentage
                                   ,Remarks
                                   ,CreatedBy
                                   ,CreatedDate)
                             VALUES
                                   (@StudentID
                                   ,@ProgramID
                                   ,@AcaCalID
                                   ,@AdmissionCalId
                                   ,@TotalCrRegInPreviousSession
                                   ,@GPAinPreviousSession
                                   ,@CGPAupToPreviousSession
                                   ,@TotalCrRegInCurrentSession
                                   ,@DiscountTypeId
                                   ,@DiscountPercentage
                                   ,@Remarks
                                   ,@CreatedBy
                                   ,@CreatedDate)
                                   ";

                foreach (StudentDiscountWorksheetEntity dwe in sdwEntities)
                {
                    Common.DAOParameters dps = new Common.DAOParameters();
                    dps.AddParameter("@StudentID", dwe.StudentId);
                    dps.AddParameter("@ProgramID", dwe.ProgramId);
                    dps.AddParameter("@AcaCalID", dwe.AcacalId);
                    dps.AddParameter("@AdmissionCalId", dwe.AdmissionCalId);
                    dps.AddParameter("@TotalCrRegInPreviousSession", dwe.TotalCrRegInpreviousSession);
                    dps.AddParameter("@GPAinPreviousSession", dwe.GpaInPreviousSession);
                    dps.AddParameter("@CGPAupToPreviousSession", dwe.CgpaUptoPreviousSession);
                    dps.AddParameter("@TotalCrRegInCurrentSession", dwe.TotalCrRegInCurrentSession);
                    dps.AddParameter("@DiscountTypeId", dwe.DiscountTypeId);
                    dps.AddParameter("@DiscountPercentage", dwe.Discountpercentage);
                    dps.AddParameter("@Remarks", dwe.Remarks);
                    dps.AddParameter("@CreatedBy", dwe.CreatorID);
                    dps.AddParameter("@CreatedDate", dwe.CreatedDate);

                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                    counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);
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

        private static int Delete(List<StudentDiscountWorksheetEntity> sdwEntities)
        {
            int counter = 0;

            foreach (StudentDiscountWorksheetEntity sdwe in sdwEntities)
            {
                string cmd = @"
                                DELETE FROM StudentDiscountWorkSheet 
                                WHERE StudentId = @StudentId 
                                and ProgramID = @ProgramID 
                                and AdmissionCalId = @AdmissionCalId
                            ";

                Common.DAOParameters dps = new Common.DAOParameters();
                dps.AddParameter("@StudentId", sdwe.StudentId);
                dps.AddParameter("@ProgramID", sdwe.ProgramId);
                dps.AddParameter("@AdmissionCalId", sdwe.AdmissionCalId);

                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

                counter = QueryHandler.ExecuteSelectBatchAction(cmd, ps);
            }
            return counter;
        }

        public static int Generate(int programId, int batchId, int studentId)
        {
            int counter = 0;

            string cmd = @"
                           DECLARE	@return_value int

                            EXEC	@return_value = usp_Prepare_Student_Discount_Worksheet
		                            @StuId = @StudentId,
		                            @BatchId = @AdmissionCalId,
		                            @ProgramId = @ProgID

                            SELECT	'Return Value' = @return_value
                          ";

            DAOParameters dps = new DAOParameters();

            if(studentId==0)
            {
                dps.AddParameter("@StudentId", DBNull.Value);
            }
            else
            {
                dps.AddParameter("@StudentId", studentId);
            }
            if(programId==0)
            {
                dps.AddParameter("@ProgID", DBNull.Value);
            }
            else
            {
                dps.AddParameter("@ProgID", programId);
            }
            if(batchId==0)
            {
                dps.AddParameter("@AdmissionCalId", DBNull.Value);
            }
            else
            {
                dps.AddParameter("@AdmissionCalId", batchId);
            }
            

            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);

            SqlConnection con= MSSqlConnectionHandler.GetConnection();
            SqlTransaction trn= MSSqlConnectionHandler.StartTransaction();

            counter = QueryHandler.ExecuteSaveBatchScalar(cmd, ps,con,trn);

            trn.Commit();
            con.Close();
            return counter;
        }
    }

}//End Of Namespace DataAccess
