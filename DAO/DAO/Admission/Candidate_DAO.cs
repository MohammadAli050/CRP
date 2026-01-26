using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;
using System.Data;

namespace DataAccess
{
    public class Candidate_DAO : Base_DAO
    {
        #region Constants

        private const string CANDIDATE_ID = "CANDIDATE_ID";//0

        private const string PROGRAMID = "PROGRAMID";//1
        private const string PROGRAMID_PA = "@PROGRAMID";


        private const string ACADEMICCALENDERID = "ACADEMICCALENDERID";//2
        private const string ACADEMICCALENDERID_PA = "@ACADEMICCALENDERID";


        private const string CANDIDATE_PREFIX = "CANDIDATE_PREFIX";//3
        private const string CANDIDATE_PREFIX_PA = "@CANDIDATE_PREFIX";


        private const string CANDIDATE_FNAME = "CANDIDATE_FNAME";//4
        private const string CANDIDATE_FNAME_PA = "@CANDIDATE_FNAME";


        //private const string CANDIDATE_MNAME = "CANDIDATE_MNAME";//5
        //private const string CANDIDATE_MNAME_PA = "@CANDIDATE_MNAME";


        //private const string CANDIDATE_LNAME = "CANDIDATE_LNAME";//6
        //private const string CANDIDATE_LNAME_PA = "@CANDIDATE_LNAME";


        //private const string CANDIDATE_ADDRESS = "CANDIDATE_ADDRESS";//7
        //private const string CANDIDATE_ADDRESS_PA = "@CANDIDATE_ADDRESS";


        private const string CANDIDATE_PHONE = "CANDIDATE_PHONE";//8
        private const string CANDIDATE_PHONE_PA = "@CANDIDATE_PHONE";


        private const string CANDIDATE_GENDER = "CANDIDATE_GENDER";//9
        private const string CANDIDATE_GENDER_PA = "@CANDIDATE_GENDER";


        //private const string CANDIDATE_REFERENCE = "CANDIDATE_REFERENCE";//10
        //private const string CANDIDATE_REFERENCE_PA = "@CANDIDATE_REFERENCE";


        //private const string CANDIDATE_PAYMENT_SERIAL = "CANDIDATE_PAYMENT_SERIAL";//11
        //private const string CANDIDATE_PAYMENT_SERIAL_PA = "@CANDIDATE_PAYMENT_SERIAL";


        //private const string LEVEL1_EXAM = "LEVEL1_EXAM";//12
        //private const string LEVEL1_EXAM_PA = "@LEVEL1_EXAM";


        //private const string LEVEL1_INSTITUTE = "LEVEL1_INSTITUTE";//13
        //private const string LEVEL1_INSTITUTE_PA = "@LEVEL1_INSTITUTE";


        //private const string LEVEL1_RESULT = "LEVEL1_RESULT";//14
        //private const string LEVEL1_RESULT_PA = "@LEVEL1_RESULT";


        //private const string LEVEL1_PASSING = "LEVEL1_PASSING";//15
        //private const string LEVEL1_PASSING_PA = "@LEVEL1_PASSING";


        //private const string LEVEL2_EXAM = "LEVEL2_EXAM";//16
        //private const string LEVEL2_EXAM_PA = "@LEVEL2_EXAM";


        //private const string LEVEL2_INSTITUTE = "LEVEL2_INSTITUTE";//17
        //private const string LEVEL2_INSTITUTE_PA = "@LEVEL2_INSTITUTE";


        //private const string LEVEL2_RESULT = "LEVEL2_RESULT";//18
        //private const string LEVEL2_RESULT_PA = "@LEVEL2_RESULT";


        //private const string LEVEL2_PASSING = "LEVEL2_PASSING";//19
        //private const string LEVEL2_PASSING_PA = "@LEVEL2_PASSING";


        //private const string LEVEL3_EXAM = "LEVEL3_EXAM";//20
        //private const string LEVEL3_EXAM_PA = "@LEVEL3_EXAM";


        //private const string LEVEL3_INSTITUTE = "LEVEL3_INSTITUTE";//21
        //private const string LEVEL3_INSTITUTE_PA = "@LEVEL3_INSTITUTE";


        //private const string LEVEL3_RESULT = "LEVEL3_RESULT";//22
        //private const string LEVEL3_RESULT_PA = "@LEVEL3_RESULT";


        //private const string LEVEL3_PASSING = "LEVEL3_PASSING";//23
        //private const string LEVEL3_PASSING_PA = "@LEVEL3_PASSING";


        //private const string FORM_PURCHASING_DATE = "FORM_PURCHASING_DATE";//24
        //private const string FORM_PURCHASING_DATE_PA = "@FORM_PURCHASING_DATE";


        //private const string FORM_SUBMISSION_DATE = "FORM_SUBMISSION_DATE";//25
        //private const string FORM_SUBMISSION_DATE_PA = "@FORM_SUBMISSION_DATE";


        private const string ADMISSION_TEST_ROLL = "ADMISSION_TEST_ROLL";//26
        private const string ADMISSION_TEST_ROLL_PA = "@ADMISSION_TEST_ROLL";


        //private const string TEST_TO_APPEAR = "TEST_TO_APPEAR";//27
        //private const string TEST_TO_APPEAR_PA = "@TEST_TO_APPEAR";


        private const string IS_DIPLOMA = "IS_DIPLOMA";//28
        private const string IS_DIPLOMA_PA = "@IS_DIPLOMA";

        private const string ISPRE_ENGLISH = "IS_PREENGLISH";
        private const string ISPRE_ENGLISH_PA = "@IS_PREENGLISH";

        private const string ISPRE_MATH = "IS_PREMATH";
        private const string ISPRE_MATH_PA = "@IS_PREMATH";

        private const string STUDENTROLL = "Roll";
        private const string STUDENTROLL_PA = "@Roll";


        #endregion

        #region PKCOLUMNS
        private const string ALLCOLUMNS = "[" + CANDIDATE_ID + "]," +
                                        "[" + PROGRAMID + "]," +
                                        "[" + ACADEMICCALENDERID + "]," +
                                        "[" + CANDIDATE_PREFIX + "]," +
                                        "[" + CANDIDATE_FNAME + "]," +
                                        //"[" + CANDIDATE_MNAME + "]," +
                                        //"[" + CANDIDATE_LNAME + "]," +
                                        //"[" + CANDIDATE_ADDRESS + "]," +
                                        "[" + CANDIDATE_PHONE + "]," +
                                        "[" + CANDIDATE_GENDER + "]," +
                                        //"[" + CANDIDATE_REFERENCE + "]," +
                                        //"[" + CANDIDATE_PAYMENT_SERIAL + "]," +
                                        //"[" + LEVEL1_EXAM + "]," +
                                        //"[" + LEVEL1_INSTITUTE + "]," +
                                        //"[" + LEVEL1_RESULT + "]," +
                                        //"[" + LEVEL1_PASSING + "]," +
                                        //"[" + LEVEL2_EXAM + "]," +
                                        //"[" + LEVEL2_INSTITUTE + "]," +
                                        //"[" + LEVEL2_RESULT + "]," +
                                        //"[" + LEVEL2_PASSING + "]," +
                                        //"[" + LEVEL3_EXAM + "]," +
                                        //"[" + LEVEL3_INSTITUTE + "]," +
                                        //"[" + LEVEL3_RESULT + "]," +
                                        //"[" + LEVEL3_PASSING + "]," +
                                        //"[" + FORM_PURCHASING_DATE + "]," +
                                        //"[" + FORM_SUBMISSION_DATE + "]," +
                                        "[" + ADMISSION_TEST_ROLL + "]," +
                                        //"[" + TEST_TO_APPEAR + "]," +
                                        "[" + IS_DIPLOMA + "]," +
                                        "[" + ISPRE_ENGLISH + "]," +
                                        "[" + ISPRE_MATH + "]," +
                                        "[" + STUDENTROLL + "] ";
        #endregion
        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + PROGRAMID + "]," +
                                        "[" + ACADEMICCALENDERID + "]," +
                                        "[" + CANDIDATE_PREFIX + "]," +
                                        "[" + CANDIDATE_FNAME + "]," +
                                        //"[" + CANDIDATE_MNAME + "]," +
                                        //"[" + CANDIDATE_LNAME + "]," +
                                        //"[" + CANDIDATE_ADDRESS + "]," +
                                        "[" + CANDIDATE_PHONE + "]," +
                                        "[" + CANDIDATE_GENDER + "]," +
                                        //"[" + CANDIDATE_REFERENCE + "]," +
                                        //"[" + CANDIDATE_PAYMENT_SERIAL + "]," +
                                        //"[" + LEVEL1_EXAM + "]," +
                                        //"[" + LEVEL1_INSTITUTE + "]," +
                                        //"[" + LEVEL1_RESULT + "]," +
                                        //"[" + LEVEL1_PASSING + "]," +
                                        //"[" + LEVEL2_EXAM + "]," +
                                        //"[" + LEVEL2_INSTITUTE + "]," +
                                        //"[" + LEVEL2_RESULT + "]," +
                                        //"[" + LEVEL2_PASSING + "]," +
                                        //"[" + LEVEL3_EXAM + "]," +
                                        //"[" + LEVEL3_INSTITUTE + "]," +
                                        //"[" + LEVEL3_RESULT + "]," +
                                        //"[" + LEVEL3_PASSING + "]," +
                                        //"[" + FORM_PURCHASING_DATE + "]," +
                                        //"[" + FORM_SUBMISSION_DATE + "]," +
                                        "[" + ADMISSION_TEST_ROLL + "]," +
                                        //"[" + TEST_TO_APPEAR + "]," +
                                        "[" + ISPRE_ENGLISH + "]," +
                                        "[" + ISPRE_MATH + "]," +
                                        "[" + IS_DIPLOMA + "],";
        #endregion
        private const string TABLENAME = "VIEW_ADMITE_CANDIDATE";
        #region SELECT
        private const string SELECT = " SELECT "
                                 + ALLCOLUMNS
                                 //+ BASECOLUMNS
                                 + " FROM " + TABLENAME;
        #endregion
        #region INSERT

        private const string INSERT = "INSERT INTO" + TABLENAME
                                    + "("
                                     + NOPKCOLUMNS
                                     + BASECOLUMNS
                                     + ")"
                                    + " VALUES ("
                                     + PROGRAMID_PA + ","
                                     + ACADEMICCALENDERID_PA + ","
                                     + CANDIDATE_PREFIX_PA + ","
                                     + CANDIDATE_FNAME_PA + ","
                                     //+ CANDIDATE_MNAME_PA + ","
                                     //+ CANDIDATE_LNAME_PA + ","
                                     //+ CANDIDATE_ADDRESS_PA + ","
                                     + CANDIDATE_PHONE_PA + ","
                                     + CANDIDATE_GENDER_PA + ","
                                     //+ CANDIDATE_REFERENCE_PA + ","
                                     //+ CANDIDATE_PAYMENT_SERIAL_PA + ","
                                     //+ LEVEL1_EXAM_PA + ","
                                     //+ LEVEL1_INSTITUTE_PA + ","
                                     //+ LEVEL1_RESULT_PA + ","
                                     //+ LEVEL1_PASSING_PA + ","
                                     //+ LEVEL2_EXAM_PA + ","
                                     //+ LEVEL2_INSTITUTE_PA + ","
                                     //+ LEVEL2_RESULT_PA + ","
                                     //+ LEVEL2_PASSING_PA + ","
                                     //+ LEVEL3_EXAM_PA + ","
                                     //+ LEVEL3_INSTITUTE_PA + ","
                                     //+ LEVEL3_RESULT_PA + ","
                                     //+ LEVEL3_PASSING_PA + ","
                                     //+ FORM_PURCHASING_DATE_PA + ","
                                     //+ FORM_SUBMISSION_DATE_PA + ","
                                     + ADMISSION_TEST_ROLL_PA + ","
                                     //+ TEST_TO_APPEAR_PA + ","
                                     + IS_DIPLOMA_PA + ","
                                     + CREATORID_PA + ","
                                     + CREATEDDATE_PA + ","
                                     + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE_PA + ")";
        #endregion
        #region UPDATE

        private const string UPDATE = "UPDATE" + TABLENAME + "SET"
                                     + PROGRAMID + " = " + PROGRAMID_PA + ","
                                     + ACADEMICCALENDERID + " = " + ACADEMICCALENDERID_PA + ","
                                     + CANDIDATE_PREFIX + " = " + CANDIDATE_PREFIX_PA + ","
                                     + CANDIDATE_FNAME + " = " + CANDIDATE_FNAME_PA + ","
                                     //+ CANDIDATE_MNAME + " = " + CANDIDATE_MNAME_PA + ","
                                     //+ CANDIDATE_LNAME + " = " + CANDIDATE_LNAME_PA + ","
                                     //+ CANDIDATE_ADDRESS + " = " + CANDIDATE_ADDRESS_PA + ","
                                     + CANDIDATE_PHONE + " = " + CANDIDATE_PHONE_PA + ","
                                     + CANDIDATE_GENDER + " = " + CANDIDATE_GENDER_PA + ","
                                     //+ CANDIDATE_REFERENCE + " = " + CANDIDATE_REFERENCE_PA + ","
                                     //+ CANDIDATE_PAYMENT_SERIAL + " = " + CANDIDATE_PAYMENT_SERIAL_PA + ","
                                     //+ LEVEL1_EXAM + " = " + LEVEL1_EXAM_PA + ","
                                     //+ LEVEL1_INSTITUTE + " = " + LEVEL1_INSTITUTE_PA + ","
                                     //+ LEVEL1_RESULT + " = " + LEVEL1_RESULT_PA + ","
                                     //+ LEVEL1_PASSING + " = " + LEVEL1_PASSING_PA + ","
                                     //+ LEVEL2_EXAM + " = " + LEVEL2_EXAM_PA + ","
                                     //+ LEVEL2_INSTITUTE + " = " + LEVEL2_INSTITUTE_PA + ","
                                     //+ LEVEL2_RESULT + " = " + LEVEL2_RESULT_PA + ","
                                     //+ LEVEL2_PASSING + " = " + LEVEL2_PASSING_PA + ","
                                     //+ LEVEL3_EXAM + " = " + LEVEL3_EXAM_PA + ","
                                     //+ LEVEL3_INSTITUTE + " = " + LEVEL3_INSTITUTE_PA + ","
                                     //+ LEVEL3_RESULT + " = " + LEVEL3_RESULT_PA + ","
                                     //+ LEVEL3_PASSING + " = " + LEVEL3_PASSING_PA + ","
                                     //+ FORM_PURCHASING_DATE + " = " + FORM_PURCHASING_DATE_PA + ","
                                     //+ FORM_SUBMISSION_DATE + " = " + FORM_SUBMISSION_DATE_PA + ","
                                     + ADMISSION_TEST_ROLL + " = " + ADMISSION_TEST_ROLL_PA + ","
                                     //+ TEST_TO_APPEAR + " = " + TEST_TO_APPEAR_PA + ","
                                     + IS_DIPLOMA + " = " + IS_DIPLOMA_PA + ","
                                     + CREATORID + " = " + CREATORID_PA + ","
                                     + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                                     + MODIFIERID + " = " + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion
        #region DELETE

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion
        #region Methods
        private static CandidateEntity Mapper(SQLNullHandler nullHandler)
        {
            CandidateEntity uiuemsErCandidateEntity = new CandidateEntity();

            uiuemsErCandidateEntity.CandidateId = nullHandler.GetInt32(CANDIDATE_ID);
           // uiuemsErCandidateEntity.Programid = nullHandler.GetInt32(PROGRAMID);
            //uiuemsErCandidateEntity.Academiccalenderid = nullHandler.GetInt32(ACADEMICCALENDERID);
            uiuemsErCandidateEntity.CandidatePrefix = nullHandler.GetInt32(CANDIDATE_PREFIX);
            uiuemsErCandidateEntity.CandidateFname = nullHandler.GetString(CANDIDATE_FNAME);
            //uiuemsErCandidateEntity.CandidateMname = nullHandler.GetString(CANDIDATE_MNAME);
            //uiuemsErCandidateEntity.CandidateLname = nullHandler.GetString(CANDIDATE_LNAME);
            //uiuemsErCandidateEntity.CandidateAddress = nullHandler.GetString(CANDIDATE_ADDRESS);
            uiuemsErCandidateEntity.CandidatePhone = nullHandler.GetString(CANDIDATE_PHONE);
            uiuemsErCandidateEntity.CandidateGender = nullHandler.GetInt32(CANDIDATE_GENDER);
            //uiuemsErCandidateEntity.CandidateReference = nullHandler.GetString(CANDIDATE_REFERENCE);
            //uiuemsErCandidateEntity.CandidatePaymentSerial = nullHandler.GetString(CANDIDATE_PAYMENT_SERIAL);
            //uiuemsErCandidateEntity.Level1Exam = nullHandler.GetString(LEVEL1_EXAM);
            //uiuemsErCandidateEntity.Level1Institute = nullHandler.GetString(LEVEL1_INSTITUTE);
            //uiuemsErCandidateEntity.Level1Result = nullHandler.GetString(LEVEL1_RESULT);
            //uiuemsErCandidateEntity.Level1Passing = nullHandler.GetInt32(LEVEL1_PASSING);
            //uiuemsErCandidateEntity.Level2Exam = nullHandler.GetString(LEVEL2_EXAM);
            //uiuemsErCandidateEntity.Level2Institute = nullHandler.GetString(LEVEL2_INSTITUTE);
            //uiuemsErCandidateEntity.Level2Result = nullHandler.GetString(LEVEL2_RESULT);
            //uiuemsErCandidateEntity.Level2Passing = nullHandler.GetInt32(LEVEL2_PASSING);
            //uiuemsErCandidateEntity.Level3Exam = nullHandler.GetString(LEVEL3_EXAM);
            //uiuemsErCandidateEntity.Level3Institute = nullHandler.GetString(LEVEL3_INSTITUTE);
            //uiuemsErCandidateEntity.Level3Result = nullHandler.GetString(LEVEL3_RESULT);
            //uiuemsErCandidateEntity.Level3Passing = nullHandler.GetInt32(LEVEL3_PASSING);
            //uiuemsErCandidateEntity.FormPurchasingDate = nullHandler.GetDateTime(FORM_PURCHASING_DATE);
            //uiuemsErCandidateEntity.FormSubmissionDate = nullHandler.GetDateTime(FORM_SUBMISSION_DATE);
            uiuemsErCandidateEntity.AdmissionTestRoll = nullHandler.GetString(ADMISSION_TEST_ROLL);
            //uiuemsErCandidateEntity.TestToAppear = nullHandler.GetInt32(TEST_TO_APPEAR);
            uiuemsErCandidateEntity.IsDiploma = nullHandler.GetBoolean(IS_DIPLOMA);

            uiuemsErCandidateEntity.IsPre_English = nullHandler.GetBoolean(ISPRE_ENGLISH);
            uiuemsErCandidateEntity.IsPre_Math = nullHandler.GetBoolean(ISPRE_MATH);

            uiuemsErCandidateEntity.StudentRoll = nullHandler.GetString(STUDENTROLL);

            return uiuemsErCandidateEntity;
        }//end of method Mapper()

        private static List<CandidateEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<CandidateEntity> uiuemsErCandidateEntities = null;
            while (theReader.Read())
            {
                if (uiuemsErCandidateEntities == null)
                {
                    uiuemsErCandidateEntities = new List<CandidateEntity>();
                }
                CandidateEntity uiuemsErCandidateEntity = Mapper(nullHandler);
                uiuemsErCandidateEntities.Add(uiuemsErCandidateEntity);
            }

            return uiuemsErCandidateEntities;
        }//end of method Map()

        private static CandidateEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            CandidateEntity uiuemsErCandidateEntity = null;
            if (theReader.Read())
            {
                uiuemsErCandidateEntity = new CandidateEntity();
                uiuemsErCandidateEntity = Mapper(nullHandler);
            }

            return uiuemsErCandidateEntity;
        }//end of method Map()

        private static List<SqlParameter> MakeSqlParameterList(CandidateEntity uiuemsErCandidateEntity)
        {
            DAOParameters dps = new DAOParameters();
            //dps.AddParameter(PROGRAMID_PA,uiuemsErCandidateEntity.CandidateId);
            //dps.AddParameter(ACADEMICCALENDERID_PA,uiuemsErCandidateEntity.Programid);
            //dps.AddParameter(CANDIDATE_PREFIX_PA,uiuemsErCandidateEntity.Academiccalenderid);
            //dps.AddParameter(CANDIDATE_FNAME_PA,uiuemsErCandidateEntity.CandidatePrefix);
            //dps.AddParameter(CANDIDATE_MNAME_PA,uiuemsErCandidateEntity.CandidateFname);
            //dps.AddParameter(CANDIDATE_LNAME_PA,uiuemsErCandidateEntity.CandidateMname);
            //dps.AddParameter(CANDIDATE_ADDRESS_PA,uiuemsErCandidateEntity.CandidateLname);
            //dps.AddParameter(CANDIDATE_PHONE_PA,uiuemsErCandidateEntity.CandidateAddress);
            //dps.AddParameter(CANDIDATE_GENDER_PA,uiuemsErCandidateEntity.CandidatePhone);
            //dps.AddParameter(CANDIDATE_REFERENCE_PA,uiuemsErCandidateEntity.CandidateGender);
            //dps.AddParameter(CANDIDATE_PAYMENT_SERIAL_PA,uiuemsErCandidateEntity.CandidateReference);
            //dps.AddParameter(LEVEL1_EXAM_PA,uiuemsErCandidateEntity.CandidatePaymentSerial);
            //dps.AddParameter(LEVEL1_INSTITUTE_PA,uiuemsErCandidateEntity.Level1Exam);
            //dps.AddParameter(LEVEL1_RESULT_PA,uiuemsErCandidateEntity.Level1Institute);
            //dps.AddParameter(LEVEL1_PASSING_PA,uiuemsErCandidateEntity.Level1Result);
            //dps.AddParameter(LEVEL2_EXAM_PA,uiuemsErCandidateEntity.Level1Passing);
            //dps.AddParameter(LEVEL2_INSTITUTE_PA,uiuemsErCandidateEntity.Level2Exam);
            //dps.AddParameter(LEVEL2_RESULT_PA,uiuemsErCandidateEntity.Level2Institute);
            //dps.AddParameter(LEVEL2_PASSING_PA,uiuemsErCandidateEntity.Level2Result);
            //dps.AddParameter(LEVEL3_EXAM_PA,uiuemsErCandidateEntity.Level2Passing);
            //dps.AddParameter(LEVEL3_INSTITUTE_PA,uiuemsErCandidateEntity.Level3Exam);
            //dps.AddParameter(LEVEL3_RESULT_PA,uiuemsErCandidateEntity.Level3Institute);
            //dps.AddParameter(LEVEL3_PASSING_PA,uiuemsErCandidateEntity.Level3Result);
            //dps.AddParameter(FORM_PURCHASING_DATE_PA,uiuemsErCandidateEntity.Level3Passing);
            //dps.AddParameter(FORM_SUBMISSION_DATE_PA,uiuemsErCandidateEntity.FormPurchasingDate);
            //dps.AddParameter(ADMISSION_TEST_ROLL_PA,uiuemsErCandidateEntity.FormSubmissionDate);
            //dps.AddParameter(TEST_TO_APPEAR_PA,uiuemsErCandidateEntity.AdmissionTestRoll);
            //dps.AddParameter(CREATORID_PA,uiuemsErCandidateEntity.Creatorid);
            //dps.AddParameter(CREATEDDATE_PA,uiuemsErCandidateEntity.Createddate);
            //dps.AddParameter(MODIFIERID_PA,uiuemsErCandidateEntity.Modifierid);
            //dps.AddParameter(MOIDFIEDDATE_PA,uiuemsErCandidateEntity.Moidfieddate);
            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }

        public static int save(List<CandidateEntity> uiuemsErCandidateEntities)
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
                foreach (CandidateEntity uiuemsErCandidateEntity in uiuemsErCandidateEntities)
                {
                    CandidateEntity tempEntity = new CandidateEntity();
                    //Assign the Paramerter here like empEntity.ID = uiuemsErCandidateEntity.ID;


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
				string cmd = DELETE +"WHERE CandidateId = "+PROGRAMID_PA;
				DAOParameters dps = new DAOParameters();
				//dps.AddParameter( , );
				List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
				counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return counter;
		}

        public static List<CandidateEntity> GetCandidateStudents(int ID)
		{
			try
			{
				List<CandidateEntity> uiuemsErCandidateEntities = null;
				string cmd = SELECT +"WHERE CandidateId = "+PROGRAMID_PA;
				DAOParameters dps = new DAOParameters();
				//dps.AddParameter( , );
				List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
				SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
				uiuemsErCandidateEntities = Maps(dr);
				return uiuemsErCandidateEntities;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        #endregion

        public static List<CandidateEntity> GetCandidateBy(int acaCalID, int programID)
        {
            try
            {
                List<CandidateEntity> entities = null;
                string cmd = SELECT + " WHERE ACADEMICCALENDERID = " + ACADEMICCALENDERID_PA + " and PROGRAMID = " + PROGRAMID_PA + " and IS_PASSED = 1";
                DAOParameters dps = new DAOParameters();
                dps.AddParameter( ACADEMICCALENDERID_PA,acaCalID );
                dps.AddParameter(PROGRAMID_PA, programID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
                entities = Maps(dr);
                dr.Close();
                return entities;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int UpdateCandidateStatus(int candidateId, String stuRoll, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;
            try
            {
                string cmd = "UPDATE " + TABLENAME + " SET Roll = " + STUDENTROLL_PA + " WHERE CANDIDATE_ID = " + ID_PA;
                DAOParameters dps = new DAOParameters();
                dps.AddParameter(ID_PA, candidateId);
                dps.AddParameter(STUDENTROLL_PA, stuRoll);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                counter = QueryHandler.ExecuteSaveBatchAction(cmd, ps, sqlConn, sqlTran);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }
    }//End Of Namespace DataAccess
}
