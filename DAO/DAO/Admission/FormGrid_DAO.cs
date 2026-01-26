using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class FormGrid_DAO
    {
        #region Constants
        private const string CANDIDATE_ID = "ID";//0
        private const string CANDIDATE_ID_PA = "@ID";
        private const string FORM_SERIAL = "FormSL";
        private const string FORM_SERIAL_PA = "@FormSL";
        private const string FORM_PURCHASING_DATE = "DateOfPurchase";//22
        private const string FORM_PURCHASING_DATE_PA = "@DateOfPurchase";
        private const string CANDIDATE_NAME = "NAME";//2
        private const string CANDIDATE_NAME_PA = "@NAME";//2
        private const string CANDIDATE_ADDRESS = "ADDRESS";//5
        private const string CANDIDATE_ADDRESS_PA = "@ADDRESS";//5
        private const string CANDIDATE_PHONE = "PHONE";//6
        private const string CANDIDATE_PHONE_PA = "@PHONE";//6
        private const string CANDIDATE_PROGRAM = "PROGRAM";//7
        private const string CANDIDATE_PROGRAM_PA = "@PROGRAM";//7
        private const string PAYMENT_SERIAL = "MRNunber";
        private const string PAYMENT_SERIAL_PA = "@MRNunber";
        private const string CANDIDATE_RESULT = "RESULT";//20
        private const string CANDIDATE_RESULT_PA = "@RESULT";
        private const string TEST_ROLL = "TestRoll";//25
        private const string TEST_ROLL_PA = "@TestRoll";
        private const string TEST_TO_APPEAR = "Test";
        private const string TEST_TO_APPEAR_PA = "@Test";
        private const string FORM_SUBMISSION_DATE = "DateOfSubmission";//23
        private const string FORM_SUBMISSION_DATE_PA = "@DateOfSubmission";

        private const string IS_PASSED = "TestResult";
        private const string IS_PASSED_PA = "@TestResult";



        private const string ALLCOLUMNS = CANDIDATE_ID + "," +
                                    FORM_SERIAL + "," +
                                    FORM_PURCHASING_DATE + "," +
                                    CANDIDATE_NAME + "," +
                                    CANDIDATE_ADDRESS + "," +
                                    CANDIDATE_PHONE + "," +
                                    CANDIDATE_PROGRAM + "," +
                                    PAYMENT_SERIAL + "," +
                                    CANDIDATE_RESULT + "," +
                                    TEST_TO_APPEAR + "," +
                                    TEST_ROLL + "," +
                                    IS_PASSED + "," +
                                    FORM_SUBMISSION_DATE;

        private const string VIEWNAME = "VIEW_FORM_SALE_SUBMISSION_INFO";

        //private CONST
        private const string SELECT = "SELECT " +
                                ALLCOLUMNS +
                                " FROM " +
                                VIEWNAME;
        #endregion

        #region Methods

        private static FormGridEntity Mapper(SQLNullHandler nullHandler)
        {
            FormGridEntity formGridEntity = new FormGridEntity();

            formGridEntity.ID = nullHandler.GetInt32(CANDIDATE_ID);
            formGridEntity.Name = nullHandler.GetString(CANDIDATE_NAME);
            formGridEntity.Address = nullHandler.GetString(CANDIDATE_ADDRESS);
            formGridEntity.Phone = nullHandler.GetString(CANDIDATE_PHONE);
            formGridEntity.Program = nullHandler.GetString(CANDIDATE_PROGRAM);
            formGridEntity.Result = nullHandler.GetString(CANDIDATE_RESULT);
            formGridEntity.FormSL = nullHandler.GetString(FORM_SERIAL);
            formGridEntity.PaymentSL = nullHandler.GetInt32(PAYMENT_SERIAL);
            formGridEntity.TestToAppear = nullHandler.GetInt32(TEST_TO_APPEAR);
            formGridEntity.TestRoll = nullHandler.GetString(TEST_ROLL);
            formGridEntity.PurchaseDate = nullHandler.GetDateTime(FORM_PURCHASING_DATE);
            formGridEntity.SubmissionDate = nullHandler.GetDateTime(FORM_SUBMISSION_DATE);
            formGridEntity.IsPassed = nullHandler.GetBoolean(IS_PASSED);
            return formGridEntity;
        }

        private static List<FormGridEntity> Maps(SqlDataReader dataReader)
        {
            List<FormGridEntity> formGridEntities = null;

            SQLNullHandler nullHandler = new SQLNullHandler(dataReader);
            try
            {
                while (dataReader.Read())
                {
                    if (formGridEntities == null)
                    {
                        formGridEntities = new List<FormGridEntity>();
                    }

                    FormGridEntity formGridEntity = Mapper(nullHandler);
                    formGridEntities.Add(formGridEntity);
                }
            }
            catch (Exception exception)
            {

                throw exception;
            }

            return formGridEntities;
        }

        private static FormGridEntity Map(SqlDataReader dataReader)
        {
            FormGridEntity formGridEntity = null;

            SQLNullHandler nullHandler = new SQLNullHandler(dataReader);
            try
            {
                while (dataReader.Read())
                {
                    if (formGridEntity == null)
                    {
                        formGridEntity = new FormGridEntity();
                    }

                    formGridEntity = Mapper(nullHandler);
                }
            }
            catch (Exception exception)
            {

                throw exception;
            }
            return formGridEntity;
        }

        public static List<FormGridEntity> GetAllInfoForGrid()
        {
            List<FormGridEntity> formGridEntities = null;
            try
            {
                string cmd = SELECT;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                //SqlDataReader dataReader = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                formGridEntities = Maps(dataReader);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return formGridEntities;
        }

        public static List<FormGridEntity> GetAllInfoForAdmissionResultGrid()
        {
            List<FormGridEntity> formGridEntities = null;
            try
            {
                //string cmd = "SELECT " + TEST_ROLL + "," + CANDIDATE_NAME + "," + CANDIDATE_PROGRAM
                //    + " FROM " + VIEWNAME ;
                string cmd = SELECT + 
                            " WHERE " + TEST_ROLL + " != '' " +
                            " ORDER BY " + TEST_ROLL;
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlDataReader dataReader = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                //SqlDataReader dataReader = QueryHandler.ExecuteSelect(cmd, sqlConnection);
                formGridEntities = Maps(dataReader);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return formGridEntities;
        }

        public static List<FormGridEntity> GetAllCandidatesByProgram(string programShortName)
        {
            List<FormGridEntity> formGridEntities = null;
            try
            {
                string cmd = SELECT 
                        + " WHERE " + 
                        TEST_ROLL + " != '' AND " +
                        CANDIDATE_PROGRAM + " = " + CANDIDATE_PROGRAM_PA +
                        " ORDER BY " + TEST_ROLL;
                //string cmd = SELECT

                DAOParameters dps = new DAOParameters();
                dps.AddParameter(CANDIDATE_PROGRAM_PA, programShortName);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dataReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
                formGridEntities = Maps(dataReader);
                dataReader.Close();
            }

            catch (Exception exception)
            {
                throw exception;
            }
            return formGridEntities;
        }


        public static List<FormGridEntity> GetAllCandidatesByDates(DateTime initialDate,DateTime finalDate)
        {
            List<FormGridEntity> formGridEntities = null;
            try
            {
                string cmd = SELECT + " WHERE " + FORM_SUBMISSION_DATE + " BETWEEN @INITIAL_DATE AND @FINAL_DATE ";
                //string cmd = SELECT

                DAOParameters dps = new DAOParameters();
                //What will I do here? since 
                dps.AddParameter("@INITIAL_DATE", initialDate);
                dps.AddParameter("@FINAL_DATE", finalDate);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dataReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
                formGridEntities = Maps(dataReader);
                dataReader.Close();
            }

            catch (Exception exception)
            {
                throw exception;
            }
            return formGridEntities;
        }

        public static FormGridEntity GetCandidateByForm(string formSL)
        {
            FormGridEntity formGridEntity = null;
            try
            {
                string cmd = SELECT + " WHERE " + FORM_SERIAL + " = " + FORM_SERIAL_PA;
                
                DAOParameters dps = new DAOParameters();
                
                dps.AddParameter(FORM_SERIAL_PA, formSL);
                
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dataReader = QueryHandler.ExecuteSelectQuery(cmd, ps);
                formGridEntity = Map(dataReader);
                dataReader.Close();
            }

            catch (Exception exception)
            {
                throw exception;
            }
            return formGridEntity;
        }

        #endregion
    }
}

