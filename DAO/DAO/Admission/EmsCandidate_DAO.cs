using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class EmsCandidate_DAO : Base_DAO
    {
        #region Constants

        private const string CANDIDATE_ID = "CANDIDATE_ID";//0
        private const string CANDIDATE_ID_PA = "@CANDIDATE_ID";


        private const string CANDIDATE_PREFIX = "CANDIDATE_PREFIX";//1
        private const string CANDIDATE_PREFIX_PA = "@CANDIDATE_PREFIX";


        private const string CANDIDATE_FNAME = "CANDIDATE_FNAME";//2
        private const string CANDIDATE_FNAME_PA = "@CANDIDATE_FNAME";


        private const string CANDIDATE_MNAME = "CANDIDATE_MNAME";//3
        private const string CANDIDATE_MNAME_PA = "@CANDIDATE_MNAME";


        private const string CANDIDATE_LNAME = "CANDIDATE_LNAME";//4
        private const string CANDIDATE_LNAME_PA = "@CANDIDATE_LNAME";


        private const string CANDIDATE_ADDRESS = "CANDIDATE_ADDRESS";//5
        private const string CANDIDATE_ADDRESS_PA = "@CANDIDATE_ADDRESS";

        private const string CANDIDATE_DISTRICT = "CANDIDATE_DISTRICT";//5
        private const string CANDIDATE_DISTRICT_PA = "@CANDIDATE_DISTRICT";


        private const string CANDIDATE_PHONE = "CANDIDATE_PHONE";//6
        private const string CANDIDATE_PHONE_PA = "@CANDIDATE_PHONE";


        private const string CANDIDATE_GENDER = "CANDIDATE_GENDER";//7
        private const string CANDIDATE_GENDER_PA = "@CANDIDATE_GENDER";


        private const string CANDIDATE_REFERENCE = "CANDIDATE_REFERENCE";//8
        private const string CANDIDATE_REFERENCE_PA = "@CANDIDATE_REFERENCE";


        private const string CANDIDATE_PAYMENT_SERIAL = "CANDIDATE_PAYMENT_SERIAL";//9
        private const string CANDIDATE_PAYMENT_SERIAL_PA = "@CANDIDATE_PAYMENT_SERIAL";


        private const string LEVEL1_EXAM = "LEVEL1_EXAM";//10
        private const string LEVEL1_EXAM_PA = "@LEVEL1_EXAM";


        private const string LEVEL1_INSTITUTE = "LEVEL1_INSTITUTE";//11
        private const string LEVEL1_INSTITUTE_PA = "@LEVEL1_INSTITUTE";


        private const string LEVEL1_RESULT = "LEVEL1_RESULT";//12
        private const string LEVEL1_RESULT_PA = "@LEVEL1_RESULT";


        private const string LEVEL1_PASSING = "LEVEL1_PASSING";//13
        private const string LEVEL1_PASSING_PA = "@LEVEL1_PASSING";


        private const string LEVEL2_EXAM = "LEVEL2_EXAM";//14
        private const string LEVEL2_EXAM_PA = "@LEVEL2_EXAM";


        private const string LEVEL2_INSTITUTE = "LEVEL2_INSTITUTE";//15
        private const string LEVEL2_INSTITUTE_PA = "@LEVEL2_INSTITUTE";


        private const string LEVEL2_RESULT = "LEVEL2_RESULT";//16
        private const string LEVEL2_RESULT_PA = "@LEVEL2_RESULT";


        private const string LEVEL2_PASSING = "LEVEL2_PASSING";//17
        private const string LEVEL2_PASSING_PA = "@LEVEL2_PASSING";


        private const string LEVEL3_EXAM = "LEVEL3_EXAM";//18
        private const string LEVEL3_EXAM_PA = "@LEVEL3_EXAM";


        private const string LEVEL3_INSTITUTE = "LEVEL3_INSTITUTE";//19
        private const string LEVEL3_INSTITUTE_PA = "@LEVEL3_INSTITUTE";


        private const string LEVEL3_RESULT = "LEVEL3_RESULT";//20
        private const string LEVEL3_RESULT_PA = "@LEVEL3_RESULT";


        private const string LEVEL3_PASSING = "LEVEL3_PASSING";//21
        private const string LEVEL3_PASSING_PA = "@LEVEL3_PASSING";

        private const string LEVEL4_EXAM = "LEVEL4_EXAM";//18
        private const string LEVEL4_EXAM_PA = "@LEVEL4_EXAM";


        private const string LEVEL4_INSTITUTE = "LEVEL4_INSTITUTE";//19
        private const string LEVEL4_INSTITUTE_PA = "@LEVEL4_INSTITUTE";


        private const string LEVEL4_RESULT = "LEVEL4_RESULT";//20
        private const string LEVEL4_RESULT_PA = "@LEVEL4_RESULT";


        private const string LEVEL4_PASSING = "LEVEL4_PASSING";//21
        private const string LEVEL4_PASSING_PA = "@LEVEL4_PASSING";


        private const string FORM_PURCHASING_DATE = "FORM_PURCHASING_DATE";//22
        private const string FORM_PURCHASING_DATE_PA = "@FORM_PURCHASING_DATE";


        private const string FORM_SUBMISSION_DATE = "FORM_SUBMISSION_DATE";//23
        private const string FORM_SUBMISSION_DATE_PA = "@FORM_SUBMISSION_DATE";

        private const string REMARK_FORM = "REMARK_FORM";
        private const string REMARK_FORM_PA = "@REMARK_FORM";

        private const string REMARK_MRN = "REMARK_MRN";
        private const string REMARK_MRN_PA = "@REMARK_MRN";


        private const string ADMISSION_TEST_ROLL = "ADMISSION_TEST_ROLL";//24
        private const string ADMISSION_TEST_ROLL_PA = "@ADMISSION_TEST_ROLL";


        private const string TEST_TO_APPEAR = "TEST_TO_APPEAR";//25
        private const string TEST_TO_APPEAR_PA = "@TEST_TO_APPEAR";


        private const string IS_DIPLOMA = "IS_DIPLOMA";//26
        private const string IS_DIPLOMA_PA = "@IS_DIPLOMA";


        private const string IS_PASSED = "IS_PASSED";//27
        private const string IS_PASSED_PA = "@IS_PASSED";

        private const string IS_PREMATH = "IS_PREMATH";//27
        private const string IS_PREMATH_PA = "@IS_PREMATH_PA";

        private const string IS_PREENGLISH = "IS_PREENGLISH";//27
        private const string IS_PREENGLLISH_PA = "@IS_PREENGLLISH_PA";

        private const string IS_ACTIVE = "IS_ACTIVE";//27
        private const string IS_ACTIVE_PA = "@IS_ACTIVE_PA";




        #endregion

        #region PKCOLUMNS
        private const string ALLCOLUMNS = "[" + CANDIDATE_ID + "]," +
                                        "[" + CANDIDATE_PREFIX + "]," +
                                        "[" + CANDIDATE_FNAME + "]," +
                                        "[" + CANDIDATE_MNAME + "]," +
                                        "[" + CANDIDATE_LNAME + "]," +
                                        "[" + CANDIDATE_ADDRESS + "]," +
                                        "[" + CANDIDATE_DISTRICT + "]," +
                                        "[" + CANDIDATE_PHONE + "]," +
                                        "[" + CANDIDATE_GENDER + "]," +
                                        "[" + CANDIDATE_REFERENCE + "]," +
                                        "[" + CANDIDATE_PAYMENT_SERIAL + "]," +
                                        "[" + LEVEL1_EXAM + "]," +
                                        "[" + LEVEL1_INSTITUTE + "]," +
                                        "[" + LEVEL1_RESULT + "]," +
                                        "[" + LEVEL1_PASSING + "]," +
                                        "[" + LEVEL2_EXAM + "]," +
                                        "[" + LEVEL2_INSTITUTE + "]," +
                                        "[" + LEVEL2_RESULT + "]," +
                                        "[" + LEVEL2_PASSING + "]," +
                                        "[" + LEVEL3_EXAM + "]," +
                                        "[" + LEVEL3_INSTITUTE + "]," +
                                        "[" + LEVEL3_RESULT + "]," +
                                        "[" + LEVEL3_PASSING + "]," +
                                        "[" + LEVEL4_EXAM + "]," +
                                        "[" + LEVEL4_INSTITUTE + "]," +
                                        "[" + LEVEL4_RESULT + "]," +
                                        "[" + LEVEL4_PASSING + "]," +
                                        "[" + FORM_PURCHASING_DATE + "]," +
                                        "[" + FORM_SUBMISSION_DATE + "]," +
                                        "[" + REMARK_FORM + "]," +
                                        "[" + REMARK_MRN + "]," +
                                        "[" + ADMISSION_TEST_ROLL + "]," +
                                        "[" + TEST_TO_APPEAR + "]," +
                                        "[" + IS_DIPLOMA + "]," +
                                        "[" + IS_PASSED + "],"+
                                        "[" + IS_PREMATH + "],"+
                                        "[" + IS_PREENGLISH + "],"+
                                        "[" + IS_ACTIVE + "],";
        #endregion
        #region AllCOLUMNWWITHOUTSUBMISSIONDATE
        private const string ALLCOLUMNSWITHOUTSUBMISSIONDATE = "[" + CANDIDATE_ID + "]," +
                                    "[" + CANDIDATE_PREFIX + "]," +
                                    "[" + CANDIDATE_FNAME + "]," +
                                    "[" + CANDIDATE_MNAME + "]," +
                                    "[" + CANDIDATE_LNAME + "]," +
                                    "[" + CANDIDATE_ADDRESS + "]," +
                                    "[" + CANDIDATE_DISTRICT + "]," +
                                    "[" + CANDIDATE_PHONE + "]," +
                                    "[" + CANDIDATE_GENDER + "]," +
                                    "[" + CANDIDATE_REFERENCE + "]," +
                                    "[" + CANDIDATE_PAYMENT_SERIAL + "]," +
                                    "[" + LEVEL1_EXAM + "]," +
                                    "[" + LEVEL1_INSTITUTE + "]," +
                                    "[" + LEVEL1_RESULT + "]," +
                                    "[" + LEVEL1_PASSING + "]," +
                                    "[" + LEVEL2_EXAM + "]," +
                                    "[" + LEVEL2_INSTITUTE + "]," +
                                    "[" + LEVEL2_RESULT + "]," +
                                    "[" + LEVEL2_PASSING + "]," +
                                    "[" + LEVEL3_EXAM + "]," +
                                    "[" + LEVEL3_INSTITUTE + "]," +
                                    "[" + LEVEL3_RESULT + "]," +
                                    "[" + LEVEL3_PASSING + "]," +
                                    "[" + LEVEL4_EXAM + "]," +
                                    "[" + LEVEL4_INSTITUTE + "]," +
                                    "[" + LEVEL4_RESULT + "]," +
                                    "[" + LEVEL4_PASSING + "]," +
                                    "[" + FORM_PURCHASING_DATE + "]," +
            //"[" + FORM_SUBMISSION_DATE + "]," +
                                    "[" + REMARK_FORM + "]," +
                                    "[" + REMARK_MRN + "]," +
                                    "[" + ADMISSION_TEST_ROLL + "]," +
                                    "[" + TEST_TO_APPEAR + "]," +
                                    "[" + IS_DIPLOMA + "]," +
                                    "[" + IS_PASSED + "],"+
                                    "[" + IS_PREMATH + "]," +
                                    "[" + IS_PREENGLISH + "]," +
                                    "[" + IS_ACTIVE + "],";

        #endregion
        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + CANDIDATE_PREFIX + "]," +
                                        "[" + CANDIDATE_FNAME + "]," +
                                        "[" + CANDIDATE_MNAME + "]," +
                                        "[" + CANDIDATE_LNAME + "]," +
                                        "[" + CANDIDATE_ADDRESS + "]," +
                                        "[" + CANDIDATE_DISTRICT + "]," +
                                        "[" + CANDIDATE_PHONE + "]," +
                                        "[" + CANDIDATE_GENDER + "]," +
                                        "[" + CANDIDATE_REFERENCE + "]," +
                                        "[" + CANDIDATE_PAYMENT_SERIAL + "]," +
                                        "[" + LEVEL1_EXAM + "]," +
                                        "[" + LEVEL1_INSTITUTE + "]," +
                                        "[" + LEVEL1_RESULT + "]," +
                                        "[" + LEVEL1_PASSING + "]," +
                                        "[" + LEVEL2_EXAM + "]," +
                                        "[" + LEVEL2_INSTITUTE + "]," +
                                        "[" + LEVEL2_RESULT + "]," +
                                        "[" + LEVEL2_PASSING + "]," +
                                        "[" + LEVEL3_EXAM + "]," +
                                        "[" + LEVEL3_INSTITUTE + "]," +
                                        "[" + LEVEL3_RESULT + "]," +
                                        "[" + LEVEL3_PASSING + "]," +
                                        "[" + LEVEL4_EXAM + "]," +
                                        "[" + LEVEL4_INSTITUTE + "]," +
                                        "[" + LEVEL4_RESULT + "]," +
                                        "[" + LEVEL4_PASSING + "]," +
                                        "[" + FORM_PURCHASING_DATE + "]," +
                                        "[" + FORM_SUBMISSION_DATE + "]," +
                                        "[" + REMARK_FORM + "]," +
                                        "[" + REMARK_MRN + "]," +
                                        "[" + ADMISSION_TEST_ROLL + "]," +
                                        "[" + TEST_TO_APPEAR + "]," +
                                        "[" + IS_DIPLOMA + "]," +
                                        "[" + IS_PASSED + "],"+
                                        "[" + IS_PREMATH + "]," +
                                        "[" + IS_PREENGLISH + "]," +
                                        "[" + IS_ACTIVE + "]";
        #endregion
        private const string TABLENAME = "CANDIDATE";
        #region SELECT
        private const string SELECT = "SELECT "
                                 + ALLCOLUMNS
                                 + BASECOLUMNS
                                 + "FROM " + TABLENAME;
        #endregion
        #region INSERT

        private const string INSERT = "INSERT INTO " + TABLENAME
                                    + "("
                                     + ALLCOLUMNSWITHOUTSUBMISSIONDATE
                                     + BASECOLUMNS
                                     + ")"
                                    + " VALUES ("
                                     + CANDIDATE_ID_PA + ","
                                     + CANDIDATE_PREFIX_PA + ","
                                     + CANDIDATE_FNAME_PA + ","
                                     + CANDIDATE_MNAME_PA + ","
                                     + CANDIDATE_LNAME_PA + ","
                                     + CANDIDATE_ADDRESS_PA + ","
                                     + CANDIDATE_DISTRICT_PA + ","
                                     + CANDIDATE_PHONE_PA + ","
                                     + CANDIDATE_GENDER_PA + ","
                                     + CANDIDATE_REFERENCE_PA + ","
                                     + CANDIDATE_PAYMENT_SERIAL_PA + ","
                                     + LEVEL1_EXAM_PA + ","
                                     + LEVEL1_INSTITUTE_PA + ","
                                     + LEVEL1_RESULT_PA + ","
                                     + LEVEL1_PASSING_PA + ","
                                     + LEVEL2_EXAM_PA + ","
                                     + LEVEL2_INSTITUTE_PA + ","
                                     + LEVEL2_RESULT_PA + ","
                                     + LEVEL2_PASSING_PA + ","
                                     + LEVEL3_EXAM_PA + ","
                                     + LEVEL3_INSTITUTE_PA + ","
                                     + LEVEL3_RESULT_PA + ","
                                     + LEVEL3_PASSING_PA + ","
                                     + LEVEL4_EXAM_PA + ","
                                     + LEVEL4_INSTITUTE_PA + ","
                                     + LEVEL4_RESULT_PA + ","
                                     + LEVEL4_PASSING_PA + ","
                                     + FORM_PURCHASING_DATE_PA + ","
            //+ FORM_SUBMISSION_DATE_PA + ","
                                     + REMARK_FORM_PA + ","
                                     + REMARK_MRN_PA + ","
                                     + ADMISSION_TEST_ROLL_PA + ","
                                     + TEST_TO_APPEAR_PA + ","
                                     + IS_DIPLOMA_PA + ","
                                     + IS_PASSED_PA + ","
                                     + IS_PREMATH_PA+","
                                     + IS_PREENGLLISH_PA+","
                                     + IS_ACTIVE_PA + ","
                                     + CREATORID_PA + ","
                                     + CREATEDDATE_PA + ","
                                     + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE_PA + ")";
        #endregion
        #region UPDATE

        private const string UPDATE = "UPDATE " + TABLENAME + " SET "
                                     + CANDIDATE_PREFIX + " = " + CANDIDATE_PREFIX_PA + ","
                                     + CANDIDATE_FNAME + " = " + CANDIDATE_FNAME_PA + ","
                                     + CANDIDATE_MNAME + " = " + CANDIDATE_MNAME_PA + ","
                                     + CANDIDATE_LNAME + " = " + CANDIDATE_LNAME_PA + ","
                                     + CANDIDATE_ADDRESS + " = " + CANDIDATE_ADDRESS_PA + ","
                                     + CANDIDATE_DISTRICT + " = " + CANDIDATE_DISTRICT_PA + ","
                                     + CANDIDATE_PHONE + " = " + CANDIDATE_PHONE_PA + ","
                                     + CANDIDATE_GENDER + " = " + CANDIDATE_GENDER_PA + ","
                                     + CANDIDATE_REFERENCE + " = " + CANDIDATE_REFERENCE_PA + ","
                                     + CANDIDATE_PAYMENT_SERIAL + " = " + CANDIDATE_PAYMENT_SERIAL_PA + ","
                                     + LEVEL1_EXAM + " = " + LEVEL1_EXAM_PA + ","
                                     + LEVEL1_INSTITUTE + " = " + LEVEL1_INSTITUTE_PA + ","
                                     + LEVEL1_RESULT + " = " + LEVEL1_RESULT_PA + ","
                                     + LEVEL1_PASSING + " = " + LEVEL1_PASSING_PA + ","
                                     + LEVEL2_EXAM + " = " + LEVEL2_EXAM_PA + ","
                                     + LEVEL2_INSTITUTE + " = " + LEVEL2_INSTITUTE_PA + ","
                                     + LEVEL2_RESULT + " = " + LEVEL2_RESULT_PA + ","
                                     + LEVEL2_PASSING + " = " + LEVEL2_PASSING_PA + ","
                                     + LEVEL3_EXAM + " = " + LEVEL3_EXAM_PA + ","
                                     + LEVEL3_INSTITUTE + " = " + LEVEL3_INSTITUTE_PA + ","
                                     + LEVEL3_RESULT + " = " + LEVEL3_RESULT_PA + ","
                                     + LEVEL3_PASSING + " = " + LEVEL3_PASSING_PA + ","
                                     + LEVEL4_EXAM + " = " + LEVEL4_EXAM_PA + ","
                                     + LEVEL4_INSTITUTE + " = " + LEVEL4_INSTITUTE_PA + ","
                                     + LEVEL4_RESULT + " = " + LEVEL4_RESULT_PA + ","
                                     + LEVEL4_PASSING + " = " + LEVEL4_PASSING_PA + ","
                                     + FORM_PURCHASING_DATE + " = " + FORM_PURCHASING_DATE_PA + ","
                                     + FORM_SUBMISSION_DATE + " = " + FORM_SUBMISSION_DATE_PA + ","
                                     + REMARK_FORM + " = " + REMARK_FORM_PA + ","
                                     + REMARK_MRN + " = " + REMARK_MRN_PA + ","
                                     + ADMISSION_TEST_ROLL + " = " + ADMISSION_TEST_ROLL_PA + ","
                                     + TEST_TO_APPEAR + " = " + TEST_TO_APPEAR_PA + ","
                                     + IS_DIPLOMA + " = " + IS_DIPLOMA_PA + ","
                                     + IS_PASSED + " = " + IS_PASSED_PA + ","
                                     + IS_PREMATH + " = " + IS_PREMATH_PA + ","
                                     + IS_PREENGLISH + " = " + IS_PREENGLLISH_PA + ","
                                     + IS_ACTIVE + " = " + IS_ACTIVE_PA + ","
                                     + CREATORID + " = " + CREATORID_PA + ","
                                     + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                                     + MODIFIERID + " = " + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion
        #region DELETE

        private const string DELETE = "DELETE FROM " + TABLENAME;
        #endregion
        #region Methods
        private static EmsCandidateEntity Mapper(SQLNullHandler nullHandler)
        {
            EmsCandidateEntity emsCandidateEntity = new EmsCandidateEntity();

            emsCandidateEntity.Id = nullHandler.GetInt32(CANDIDATE_ID);
            emsCandidateEntity.CandidatePrefix = nullHandler.GetInt32(CANDIDATE_PREFIX);
            emsCandidateEntity.CandidateFname = nullHandler.GetString(CANDIDATE_FNAME);
            emsCandidateEntity.CandidateMname = nullHandler.GetString(CANDIDATE_MNAME);
            emsCandidateEntity.CandidateLname = nullHandler.GetString(CANDIDATE_LNAME);
            emsCandidateEntity.CandidateAddress = nullHandler.GetString(CANDIDATE_ADDRESS);
            emsCandidateEntity.CandidateDistrict = nullHandler.GetString(CANDIDATE_DISTRICT);
            emsCandidateEntity.CandidatePhone = nullHandler.GetString(CANDIDATE_PHONE);
            emsCandidateEntity.CandidateGender = nullHandler.GetInt32(CANDIDATE_GENDER);
            emsCandidateEntity.CandidateReference = nullHandler.GetString(CANDIDATE_REFERENCE);
            emsCandidateEntity.CandidatePaymentSerial = nullHandler.GetInt32(CANDIDATE_PAYMENT_SERIAL);
            emsCandidateEntity.Level1Exam = nullHandler.GetString(LEVEL1_EXAM);
            emsCandidateEntity.Level1Institute = nullHandler.GetString(LEVEL1_INSTITUTE);
            emsCandidateEntity.Level1Result = nullHandler.GetString(LEVEL1_RESULT);
            emsCandidateEntity.Level1Passing = nullHandler.GetInt32(LEVEL1_PASSING);
            emsCandidateEntity.Level2Exam = nullHandler.GetString(LEVEL2_EXAM);
            emsCandidateEntity.Level2Institute = nullHandler.GetString(LEVEL2_INSTITUTE);
            emsCandidateEntity.Level2Result = nullHandler.GetString(LEVEL2_RESULT);
            emsCandidateEntity.Level2Passing = nullHandler.GetInt32(LEVEL2_PASSING);
            emsCandidateEntity.Level3Exam = nullHandler.GetString(LEVEL3_EXAM);
            emsCandidateEntity.Level3Institute = nullHandler.GetString(LEVEL3_INSTITUTE);
            emsCandidateEntity.Level3Result = nullHandler.GetString(LEVEL3_RESULT);
            emsCandidateEntity.Level3Passing = nullHandler.GetInt32(LEVEL3_PASSING);
            emsCandidateEntity.Level4Exam = nullHandler.GetString(LEVEL4_EXAM);
            emsCandidateEntity.Level4Institute = nullHandler.GetString(LEVEL4_INSTITUTE);
            emsCandidateEntity.Level4Result = nullHandler.GetString(LEVEL4_RESULT);
            emsCandidateEntity.Level4Passing = nullHandler.GetInt32(LEVEL4_PASSING);
            emsCandidateEntity.FormPurchasingDate = nullHandler.GetDateTime(FORM_PURCHASING_DATE);
            emsCandidateEntity.FormSubmissionDate = nullHandler.GetDateTime(FORM_SUBMISSION_DATE);
            emsCandidateEntity.RemarkForm = nullHandler.GetString(REMARK_FORM);
            emsCandidateEntity.RemarkMRN = nullHandler.GetString(REMARK_MRN);
            emsCandidateEntity.AdmissionTestRoll = nullHandler.GetString(ADMISSION_TEST_ROLL);
            emsCandidateEntity.TestToAppear = nullHandler.GetInt32(TEST_TO_APPEAR);
            emsCandidateEntity.IsDiploma = nullHandler.GetBoolean(IS_DIPLOMA);
            emsCandidateEntity.IsPassed = nullHandler.GetBoolean(IS_PASSED);
            emsCandidateEntity.IsPreMath = nullHandler.GetBoolean(IS_PREMATH);
            emsCandidateEntity.IsPrenglish = nullHandler.GetBoolean(IS_PREENGLISH);
            emsCandidateEntity.IsActive = nullHandler.GetBoolean(IS_ACTIVE);
            emsCandidateEntity.CreatorID = nullHandler.GetInt32(CREATORID);
            emsCandidateEntity.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            emsCandidateEntity.ModifierID = nullHandler.GetInt32(MODIFIERID);
            emsCandidateEntity.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);


            return emsCandidateEntity;
        }//end of method Mapper()

        private static List<EmsCandidateEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<EmsCandidateEntity> emsCandidateEntities = null;
            while (theReader.Read())
            {
                if (emsCandidateEntities == null)
                {
                    emsCandidateEntities = new List<EmsCandidateEntity>();
                }
                EmsCandidateEntity emsCandidateEntity = Mapper(nullHandler);
                emsCandidateEntities.Add(emsCandidateEntity);
            }

            return emsCandidateEntities;
        }//end of method Map()


        private static EmsCandidateEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            EmsCandidateEntity emsCandidateEntity = null;
            if (theReader.Read())
            {
                emsCandidateEntity = new EmsCandidateEntity();
                emsCandidateEntity = Mapper(nullHandler);
            }

            return emsCandidateEntity;
        }//end of method Map()



        private static List<SqlParameter> MakeSqlParameterList(EmsCandidateEntity emsCandidateEntity)
        {
            DAOParameters dps = new DAOParameters();

            dps.AddParameter(CANDIDATE_ID_PA, emsCandidateEntity.Id);
            dps.AddParameter(CANDIDATE_PREFIX_PA, emsCandidateEntity.CandidatePrefix);
            dps.AddParameter(CANDIDATE_FNAME_PA, emsCandidateEntity.CandidateFname);
            dps.AddParameter(CANDIDATE_MNAME_PA, emsCandidateEntity.CandidateMname);
            dps.AddParameter(CANDIDATE_LNAME_PA, emsCandidateEntity.CandidateLname);
            dps.AddParameter(CANDIDATE_ADDRESS_PA, emsCandidateEntity.CandidateAddress);
            dps.AddParameter(CANDIDATE_DISTRICT_PA, emsCandidateEntity.CandidateDistrict);
            dps.AddParameter(CANDIDATE_PHONE_PA, emsCandidateEntity.CandidatePhone);
            dps.AddParameter(CANDIDATE_GENDER_PA, emsCandidateEntity.CandidateGender);
            dps.AddParameter(CANDIDATE_REFERENCE_PA, emsCandidateEntity.CandidateReference);
            int maxMRN = 0;
            if (Variables.SaveMode == SaveMode.Add)
            {
                maxMRN = GetMaxMRN();
            }
            else
            {
                maxMRN = emsCandidateEntity.CandidatePaymentSerial;
            }
            dps.AddParameter(CANDIDATE_PAYMENT_SERIAL_PA, maxMRN);
            dps.AddParameter(LEVEL1_EXAM_PA, emsCandidateEntity.Level1Exam);
            dps.AddParameter(LEVEL1_INSTITUTE_PA, emsCandidateEntity.Level1Institute);
            dps.AddParameter(LEVEL1_RESULT_PA, emsCandidateEntity.Level1Result);
            dps.AddParameter(LEVEL1_PASSING_PA, emsCandidateEntity.Level2Passing);
            dps.AddParameter(LEVEL2_EXAM_PA, emsCandidateEntity.Level2Exam);
            dps.AddParameter(LEVEL2_INSTITUTE_PA, emsCandidateEntity.Level2Institute);
            dps.AddParameter(LEVEL2_RESULT_PA, emsCandidateEntity.Level2Result);
            dps.AddParameter(LEVEL2_PASSING_PA, emsCandidateEntity.Level2Passing);
            dps.AddParameter(LEVEL3_EXAM_PA, emsCandidateEntity.Level3Exam);
            dps.AddParameter(LEVEL3_INSTITUTE_PA, emsCandidateEntity.Level3Institute);
            dps.AddParameter(LEVEL3_RESULT_PA, emsCandidateEntity.Level3Result);
            dps.AddParameter(LEVEL3_PASSING_PA, emsCandidateEntity.Level3Passing);
            dps.AddParameter(LEVEL4_EXAM_PA, emsCandidateEntity.Level4Exam);
            dps.AddParameter(LEVEL4_INSTITUTE_PA, emsCandidateEntity.Level4Institute);
            dps.AddParameter(LEVEL4_RESULT_PA, emsCandidateEntity.Level4Result);
            dps.AddParameter(LEVEL4_PASSING_PA, emsCandidateEntity.Level4Passing);
            dps.AddParameter(FORM_PURCHASING_DATE_PA, emsCandidateEntity.FormPurchasingDate);
            //if (Variables.SaveMode == SaveMode.Add)
            //{
            //    dps.AddParameter(FORM_SUBMISSION_DATE_PA, null); 
            //}
            //else
            //{
            //    dps.AddParameter(FORM_SUBMISSION_DATE_PA, emsCandidateEntity.FormSubmissionDate);
            //}
            dps.AddParameter(FORM_SUBMISSION_DATE_PA, emsCandidateEntity.FormSubmissionDate);
            dps.AddParameter(REMARK_FORM_PA, emsCandidateEntity.RemarkForm);

            //dps.AddParameter(CANDIDATE_PAYMENT_SERIAL_PA,maxMRN);
            dps.AddParameter(REMARK_MRN_PA, emsCandidateEntity.RemarkMRN);
            dps.AddParameter(ADMISSION_TEST_ROLL_PA, emsCandidateEntity.AdmissionTestRoll);
            dps.AddParameter(TEST_TO_APPEAR_PA, emsCandidateEntity.TestToAppear);
            dps.AddParameter(IS_DIPLOMA_PA, emsCandidateEntity.IsDiploma);
            dps.AddParameter(IS_PASSED_PA, emsCandidateEntity.IsPassed);
            dps.AddParameter(IS_PREMATH_PA,emsCandidateEntity.IsPreMath);
            dps.AddParameter(IS_PREENGLLISH_PA, emsCandidateEntity.IsPrenglish);
            dps.AddParameter(IS_ACTIVE_PA,emsCandidateEntity.IsActive);
            dps.AddParameter(CREATORID_PA, emsCandidateEntity.CreatorID);
            dps.AddParameter(CREATEDDATE_PA, emsCandidateEntity.CreatedDate);
            dps.AddParameter(MODIFIERID_PA, emsCandidateEntity.ModifierID);
            dps.AddParameter(MOIDFIEDDATE_PA, emsCandidateEntity.ModifiedDate);


            List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
            return ps;
        }

        public static int save(EmsCandidateEntity emsCandidateEntity)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTransaction = MSSqlConnectionHandler.StartTransaction();
                List<SqlParameter> ps = new List<SqlParameter>();
                string cmd = INSERT;
                if (Variables.SaveMode == SaveMode.Update)
                {
                    cmd = UPDATE + " " + "WHERE" + " " + CANDIDATE_ID + " = " + CANDIDATE_ID_PA;
                    DAOParameters dps = new DAOParameters();


                    dps.AddParameter(CANDIDATE_PREFIX_PA, emsCandidateEntity.CandidatePrefix);
                    dps.AddParameter(CANDIDATE_FNAME_PA, emsCandidateEntity.CandidateFname);
                    dps.AddParameter(CANDIDATE_MNAME_PA, emsCandidateEntity.CandidateMname);
                    dps.AddParameter(CANDIDATE_LNAME_PA, emsCandidateEntity.CandidateLname);
                    dps.AddParameter(CANDIDATE_ADDRESS_PA, emsCandidateEntity.CandidateAddress);
                    dps.AddParameter(CANDIDATE_DISTRICT_PA, emsCandidateEntity.CandidateDistrict);
                    dps.AddParameter(CANDIDATE_PHONE_PA, emsCandidateEntity.CandidatePhone);
                    dps.AddParameter(CANDIDATE_GENDER_PA, emsCandidateEntity.CandidateGender);
                    dps.AddParameter(CANDIDATE_REFERENCE_PA, emsCandidateEntity.CandidateReference);
                    dps.AddParameter(CANDIDATE_PAYMENT_SERIAL_PA, emsCandidateEntity.CandidatePaymentSerial);
                    dps.AddParameter(LEVEL1_EXAM_PA, emsCandidateEntity.Level1Exam);
                    dps.AddParameter(LEVEL1_INSTITUTE_PA, emsCandidateEntity.Level1Institute);
                    dps.AddParameter(LEVEL1_RESULT_PA, emsCandidateEntity.Level1Result);
                    dps.AddParameter(LEVEL1_PASSING_PA, emsCandidateEntity.Level2Passing);
                    dps.AddParameter(LEVEL2_EXAM_PA, emsCandidateEntity.Level2Exam);
                    dps.AddParameter(LEVEL2_INSTITUTE_PA, emsCandidateEntity.Level2Institute);
                    dps.AddParameter(LEVEL2_RESULT_PA, emsCandidateEntity.Level2Result);
                    dps.AddParameter(LEVEL2_PASSING_PA, emsCandidateEntity.Level2Passing);
                    dps.AddParameter(LEVEL3_EXAM_PA, emsCandidateEntity.Level3Exam);
                    dps.AddParameter(LEVEL3_INSTITUTE_PA, emsCandidateEntity.Level3Institute);
                    dps.AddParameter(LEVEL3_RESULT_PA, emsCandidateEntity.Level3Result);
                    dps.AddParameter(LEVEL3_PASSING_PA, emsCandidateEntity.Level3Passing);
                    dps.AddParameter(LEVEL4_EXAM_PA, emsCandidateEntity.Level4Exam);
                    dps.AddParameter(LEVEL4_INSTITUTE_PA, emsCandidateEntity.Level4Institute);
                    dps.AddParameter(LEVEL4_RESULT_PA, emsCandidateEntity.Level4Result);
                    dps.AddParameter(LEVEL4_PASSING_PA, emsCandidateEntity.Level4Passing);
                    dps.AddParameter(FORM_PURCHASING_DATE_PA, emsCandidateEntity.FormPurchasingDate);
                    dps.AddParameter(FORM_SUBMISSION_DATE_PA, emsCandidateEntity.FormSubmissionDate);
                    dps.AddParameter(REMARK_FORM_PA, emsCandidateEntity.RemarkForm);
                    dps.AddParameter(REMARK_MRN_PA, emsCandidateEntity.RemarkMRN);
                    dps.AddParameter(ADMISSION_TEST_ROLL_PA, emsCandidateEntity.AdmissionTestRoll);
                    dps.AddParameter(TEST_TO_APPEAR_PA, emsCandidateEntity.TestToAppear);
                    dps.AddParameter(IS_DIPLOMA_PA, emsCandidateEntity.IsDiploma);
                    dps.AddParameter(IS_PASSED_PA, emsCandidateEntity.IsPassed);
                    dps.AddParameter(IS_PREMATH_PA, emsCandidateEntity.IsPreMath);
                    dps.AddParameter(IS_PREENGLLISH_PA, emsCandidateEntity.IsPrenglish);
                    dps.AddParameter(IS_ACTIVE_PA, emsCandidateEntity.IsActive);
                    dps.AddParameter(CREATORID_PA, emsCandidateEntity.CreatorID);
                    dps.AddParameter(CREATEDDATE_PA, emsCandidateEntity.CreatedDate);
                    dps.AddParameter(MODIFIERID_PA, emsCandidateEntity.ModifierID);
                    dps.AddParameter(MOIDFIEDDATE_PA, emsCandidateEntity.ModifiedDate);
                    dps.AddParameter(CANDIDATE_ID_PA, emsCandidateEntity.Id);

                    List<SqlParameter> sps = Common.Methods.GetSQLParameters(dps);
                    counter = QueryHandler.ExecuteSaveBatchAction(cmd, sps, sqlConnection, sqlTransaction);
                    MSSqlConnectionHandler.CommitTransaction();
                    MSSqlConnectionHandler.CloseDbConnection();
                    return counter;
                }

                ps = MakeSqlParameterList(emsCandidateEntity);

                counter = QueryHandler.ExecuteSaveBatchAction(cmd, ps, sqlConnection, sqlTransaction);
                sqlTransaction.Commit();
                //MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int save(List<EmsCandidateEntity> candidateEntities)
        {
            int counter = 0;
            try
            {
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();

                string cmd = "";
                if (Variables.SaveMode == SaveMode.Add)
                {
                    cmd = INSERT;
                }
                if (Variables.SaveMode == SaveMode.Update)
                {
                    cmd = UPDATE + " " + "WHERE " + CANDIDATE_ID + " = " + CANDIDATE_ID_PA;

                }
                foreach (EmsCandidateEntity candidateEntity in candidateEntities)
                {
                    //EmsCandidateEntity tempEntity = candidateEntity;//new EmsCandidateEntity();
                    //tempEntity.Id = candidateEntity.Id;
                    //tempEntity.IsPassed = candidateEntity.IsPassed;
                    counter += QueryHandler.ExecuteSelectBatchAction(cmd, MakeSqlParameterList(candidateEntity));
                }
                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
            }

            catch (Exception exception)
            {
                throw exception;
            }
            return counter;
        }

        public static int update(EmsCandidateEntity emsCandidateEntity)
        {
            int counter = 0;

            try
            {
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTransaction = MSSqlConnectionHandler.StartTransaction();

                string strUpdateQuery = UPDATE +
                                        " WHERE " + emsCandidateEntity.Id + " = " + CANDIDATE_ID_PA;
                counter = QueryHandler.ExecuteSaveBatchAction(UPDATE, MakeSqlParameterList(emsCandidateEntity),
                                                              sqlConnection, sqlTransaction);
                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();

            }
            catch (Exception exception)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw exception;
            }

            return counter;
        }

        internal static int delete(int ID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE + " WHERE CandidateId = " + CANDIDATE_ID_PA;
                DAOParameters dps = new DAOParameters();
                dps.AddParameter(CANDIDATE_ID_PA, ID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }

        public static List<EmsCandidateEntity> GetCandidateStudents()
        {
            try
            {
                List<EmsCandidateEntity> emsCandidateEntities = new List<EmsCandidateEntity>();
                string cmd = SELECT;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();

                SqlDataReader dr = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                emsCandidateEntities = Maps(dr);
                return emsCandidateEntities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<EmsCandidateEntity> GetCandidatesByAdmissionTestRolls(List<string> testRolls)
        {
            List<EmsCandidateEntity> candidateEntities = new List<EmsCandidateEntity>();
            EmsCandidateEntity candidateEntity = new EmsCandidateEntity();
            string cmd = "";

            try
            {
                foreach (var testRoll in testRolls)
                {
                    cmd = SELECT + " WHERE " + ADMISSION_TEST_ROLL + " = " + ADMISSION_TEST_ROLL_PA;
                    DAOParameters dps = new DAOParameters();
                    dps.AddParameter(ADMISSION_TEST_ROLL_PA, testRoll);
                    List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                    SqlDataReader dataReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
                    candidateEntity = Map(dataReader);
                    candidateEntities.Add(candidateEntity);
                    dataReader.Close();
                }
            }

            catch (Exception exception)
            {
                throw exception;
            }

            return candidateEntities;
        }

        public static EmsCandidateEntity GetCandidateByID(int ID)
        {
            EmsCandidateEntity candidateEntity = null;
            try
            {
                if (candidateEntity == null)
                {
                    candidateEntity = new EmsCandidateEntity();
                }
                string cmd = SELECT + " WHERE " + CANDIDATE_ID + " = " + ID_PA;
                DAOParameters dps = new DAOParameters();
                dps.AddParameter(ID_PA, ID);
                //SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dataReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
                //Check, ADMission DAO, Threis is an error if I write this method exactly like that
                candidateEntity = Map(dataReader);
                dataReader.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return candidateEntity;
        }

        public static int GetMaxCandidateId()
        {
            int id = 0;
            try
            {
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTransaction = MSSqlConnectionHandler.StartTransaction();
                string cmd = "select max(CANDIDATE_ID) from CANDIDATE;";
                id = QueryHandler.GetMaxID(cmd, sqlConnection, sqlTransaction);
                sqlTransaction.Commit();
                sqlConnection.Close();
            }
            catch (Exception exception)
            {

                throw exception;
            }
            return id;
        }

        public static int GetMaxMRN()
        {
            int MRN = 0;
            try
            {
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTransaction = MSSqlConnectionHandler.StartTransaction();
                string cmd = "select max(CANDIDATE_PAYMENT_SERIAL) from CANDIDATE;";
                MRN = QueryHandler.GetMaxID(cmd, sqlConnection, sqlTransaction);
                sqlTransaction.Commit();
                sqlConnection.Close();
            }
            catch (Exception exception)
            {

                throw exception;
            }
            return MRN;
        }


        public static string MakeAdmissionRoll(string programCode, int roll)
        {
            string admissionRoll = "";


            try
            {
                admissionRoll = (roll.ToString()).PadLeft(4, '0');
                admissionRoll = programCode + admissionRoll;
            }

            catch (Exception exception)
            {
                throw exception;
            }
            return admissionRoll;
        }

        public static string getMaxAdmissionRoll(string programCode)
        {
            string maxAdmissionRoll = string.Empty;
            List<string> admissionRolls = new List<string>();
            List<EmsCandidateEntity> candidateEntities = new List<EmsCandidateEntity>();
            try
            {
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                //SqlTransaction sqlTransaction = MSSqlConnectionHandler.StartTransaction();
                string cmd = SELECT
                            + " " + "where ADMISSION_TEST_ROLL != ''";
                sqlConnection.Open();
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(cmd, sqlConnection);

                candidateEntities = Maps(dataReader);

                sqlConnection.Close();
                //Make this String List into an integer List
                List<Int32> rolls = new List<int>();
                if (candidateEntities == null)
                {
                    rolls.Add(0);
                }
                else
                {
                    //This loop will trimout the first 3 digits, program code, from the admission Test Roll
                    foreach (var candidate in candidateEntities)
                    {
                        string tempRoll = string.Empty;
                        tempRoll = candidate.AdmissionTestRoll.Substring(3);
                        admissionRolls.Add(tempRoll);
                    }

                    //This loop will convert the admissionroll serials from string to integer
                    foreach (string admissionRoll in admissionRolls)
                    {
                        int tempRoll = Convert.ToInt32(admissionRoll);
                        rolls.Add(tempRoll);
                    }
                }

                int maxRoll = rolls.Max()+1;                
                maxAdmissionRoll = MakeAdmissionRoll(programCode, maxRoll);

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return maxAdmissionRoll;
        }

        //public static string ChangeAdmissionRoll
        #endregion
    }

}//End Of Namespace DataAccess
