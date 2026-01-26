using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class AdmissionForm_DAO : Base_DAO
    {
        #region Constants

        private const string FORM_ID = "ID";
        private const string FORM_ID_PA = "@ID";

        private const string FORM_SERIAL = "FORM_SERIAL";//0
        private const string FORM_SERIAL_PA = "@FORM_SERIAL";

        private const string FORM_PRICE = "AMOUNT";
        private const string FORM_PRICE_PA = "@AMOUNT";


        private const string FORM_STATUS = "FORM_STATUS";//1
        private const string FORM_STATUS_PA = "@FORM_STATUS";


        private const string CANDIDATE_ID = "CANDIDATE_ID";//2
        private const string CANDIDATE_ID_PA = "@CANDIDATE_ID";


        private const string PROGRAMID = "PROGRAMID";//3
        private const string PROGRAMID_PA = "@PROGRAMID";


        private const string ACADEMICCALENDERID = "ACADEMICCALENDERID";//4
        private const string ACADEMICCALENDERID_PA = "@ACADEMICCALENDERID";


        #endregion

        #region PKCOLUMNS
        private const string ALLCOLUMNS = "[" + FORM_ID + "]," +
                                        "[" + FORM_SERIAL + "]," +
                                        "[" + FORM_PRICE + "]," +
                                        "[" + FORM_STATUS + "]," +
                                        "[" + CANDIDATE_ID + "]," +
                                        "[" + PROGRAMID + "]," +
                                        "[" + ACADEMICCALENDERID + "],";
        #endregion
        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + FORM_SERIAL + "]," +
                                        "[" + FORM_PRICE + "]," +
                                        "[" + FORM_STATUS + "]," +
                                        "[" + CANDIDATE_ID_PA + "]," +
                                        "[" + PROGRAMID + "]," +
                                        "[" + ACADEMICCALENDERID + "],";
        #endregion
        private const string TABLENAME = "ADMISSION_FORM";
        #region SELECT
        private const string SELECT = "SELECT "
                                 + ALLCOLUMNS
                                 + BASECOLUMNS
                                 + " FROM " + TABLENAME;
        #endregion
        #region INSERT

        private const string INSERT = "INSERT INTO " + TABLENAME
                                     + "("
                                     + ALLCOLUMNS
                                     + BASECOLUMNS
                                     + ")"
                                     + " VALUES ("
                                     + FORM_ID_PA + ","
                                     + FORM_SERIAL_PA + ","
                                     + FORM_PRICE_PA + ","
                                     + FORM_STATUS_PA + ","
                                     + CANDIDATE_ID_PA + ","
                                     + PROGRAMID_PA + ","
                                     + ACADEMICCALENDERID_PA + ","
                                     + CREATORID_PA + ","
                                     + CREATEDDATE_PA + ","
                                     + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE_PA + ")";
        #endregion
        #region UPDATE

        private const string UPDATE = "UPDATE " + TABLENAME + " SET "
                                     + FORM_SERIAL + " = " + FORM_SERIAL_PA + ","
                                     + FORM_PRICE + " = " + FORM_PRICE_PA + ","
                                     + FORM_STATUS + " = " + FORM_STATUS_PA + ","
                                     + CANDIDATE_ID + " = " + CANDIDATE_ID_PA + ","
                                     + PROGRAMID + " = " + PROGRAMID_PA + ","
                                     + ACADEMICCALENDERID + " = " + ACADEMICCALENDERID_PA + ","
                                     + CREATORID + " = " + CREATORID_PA + ","
                                     + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                                     + MODIFIERID + " = " + MODIFIERID_PA + ","
                                     + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;
        #endregion
        #region DELETE

        private const string DELETE = "DELETE FROM " + TABLENAME;
        #endregion
        #region Methods
        private static AdmissionFormEntity Mapper(SQLNullHandler nullHandler)
        {
            AdmissionFormEntity admissionFormEntity = new AdmissionFormEntity();


            admissionFormEntity.FormSerial = nullHandler.GetString(FORM_SERIAL);
            admissionFormEntity.FormPrice = nullHandler.GetDecimal(FORM_PRICE);
            admissionFormEntity.FormStatus = nullHandler.GetString(FORM_STATUS);
            admissionFormEntity.CandidateTableId = nullHandler.GetInt32(CANDIDATE_ID);
            admissionFormEntity.Programid = nullHandler.GetInt32(PROGRAMID);
            admissionFormEntity.CreatorID = nullHandler.GetInt32(CREATORID);
            admissionFormEntity.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            admissionFormEntity.ModifierID = nullHandler.GetInt32(MODIFIERID);
            admissionFormEntity.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return admissionFormEntity;
        }//end of method Mapper()

        private static List<AdmissionFormEntity> Maps(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            List<AdmissionFormEntity> admissionFormEntities = null;
            while (theReader.Read())
            {
                if (admissionFormEntities == null)
                {
                    admissionFormEntities = new List<AdmissionFormEntity>();
                }
                AdmissionFormEntity admissionFormEntity = Mapper(nullHandler);
                admissionFormEntities.Add(admissionFormEntity);
            }

            return admissionFormEntities;
        }//end of method Map()


        private static AdmissionFormEntity Map(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);
            AdmissionFormEntity admissionFormEntity = null;
            if (theReader.Read())
            {
                admissionFormEntity = new AdmissionFormEntity();
                admissionFormEntity = Mapper(nullHandler);
            }

            return admissionFormEntity;
        }//end of method Map()



        private static List<SqlParameter> MakeSqlParameterList(AdmissionFormEntity admissionFormEntity)
        {
            DAOParameters dps = new DAOParameters();

            if (Variables.SaveMode == SaveMode.Update)
            {
                dps.AddParameter(FORM_SERIAL_PA, admissionFormEntity.FormSerial);
            }
            else
            {
                int maxFormId = GetMaxFormID();
                CcAcademiccalenderEntity nextAccademicCalender = rbAcademiccalenderDao.GetNextAcademicCalenderEntity();
                string batchCode = nextAccademicCalender.Batchcode;
                string formSL = GenerateFormSerial(batchCode, maxFormId);
                dps.AddParameter(FORM_SERIAL_PA, formSL);
            }
            
            dps.AddParameter(FORM_PRICE, admissionFormEntity.FormPrice);
            dps.AddParameter(FORM_STATUS_PA, admissionFormEntity.FormStatus);
            dps.AddParameter(CANDIDATE_ID_PA, admissionFormEntity.CandidateTableId);
            dps.AddParameter(PROGRAMID_PA, admissionFormEntity.Programid);
            dps.AddParameter(ACADEMICCALENDERID_PA, admissionFormEntity.Academiccalenderid);
            dps.AddParameter(CREATORID_PA, admissionFormEntity.CreatorID);
            dps.AddParameter(CREATEDDATE_PA, admissionFormEntity.CreatedDate);
            dps.AddParameter(MODIFIERID_PA, admissionFormEntity.ModifierID);
            dps.AddParameter(MOIDFIEDDATE_PA, admissionFormEntity.ModifiedDate);
            dps.AddParameter(FORM_ID_PA, admissionFormEntity.Id);
            List<SqlParameter> ps = Methods.GetSQLParameters(dps);

            return ps;
        }

        public static int save(List<AdmissionFormEntity> admissionFormEntities)
        {
            try
            {
                int counter = 0;
                MSSqlConnectionHandler.GetConnection();
                MSSqlConnectionHandler.StartTransaction();
                string cmd = INSERT;
                foreach (AdmissionFormEntity admissionFormEntity in admissionFormEntities)
                {
                    AdmissionFormEntity tempEntity = new AdmissionFormEntity();
                    //Assign the Paramerter here like empEntity.ID = admissionFormEntity.ID;
                    tempEntity.Id = admissionFormEntity.Id;
                    
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

        public static int save(AdmissionFormEntity formEntity)
        {
            int counter = 0;

            try
            {
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                //MSSqlConnectionHandler.StartTransaction();
                string cmd = "";
                
                if (Variables.SaveMode == SaveMode.Add)
                {
                    cmd = INSERT;
                }
                if (Variables.SaveMode == SaveMode.Update)
                {
                    cmd = UPDATE + " " + "WHERE " + CANDIDATE_ID + " = " + formEntity.CandidateTableId;
                }

                List<SqlParameter> ps = AdmissionForm_DAO.MakeSqlParameterList(formEntity);

                counter = QueryHandler.ExecuteSaveAction(cmd, ps, sqlConnection);

                //MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return counter;
        }

        internal static int delete(int ID)
        {
            int counter = 0;
            try
            {
                string cmd = DELETE +  "WHERE FormSerial = " + FORM_SERIAL_PA;
                DAOParameters dps = new DAOParameters();
                dps.AddParameter(FORM_SERIAL_PA, ID);
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                counter = QueryHandler.ExecuteDeleteBatchAction(cmd, ps);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counter;
        }

        public static AdmissionFormEntity GetFormInformationOfCandidate(int ID)
        {
            AdmissionFormEntity admissionFormEntity = null;
            try
            {
                if (admissionFormEntity == null)
                {
                    admissionFormEntity = new AdmissionFormEntity();
                }
                string cmd = SELECT + " WHERE " + CANDIDATE_ID + " = " + CANDIDATE_ID_PA;
                DAOParameters dps = new DAOParameters();
                dps.AddParameter(CANDIDATE_ID_PA, ID);
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                List<SqlParameter> ps = Common.Methods.GetSQLParameters(dps);
                SqlDataReader dr = QueryHandler.ExecuteSelectQuery(cmd, ps);
                admissionFormEntity = Map(dr);
                return admissionFormEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetMaxFormID()
        {
            int maxFormID = 0;

            try
            {
                SqlConnection sqlConnection = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTransaction = MSSqlConnectionHandler.StartTransaction();
                string cmd = "SELECT MAX(ID) FROM ADMISSION_FORM;";
                maxFormID = QueryHandler.GetMaxID(cmd, sqlConnection, sqlTransaction);
                sqlTransaction.Commit();
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return maxFormID;
        }

        public static string GenerateFormSerial(string batchCode, int formID)
        {
            string formSerial = "";
            if (formID < 1000)
            {
                formSerial =  (formID.ToString()).PadLeft(3, '0');
            }
            else
            {
                formSerial =  (formID.ToString()).PadLeft(4, '0');
            }
            //string formSerial = batchCode + (formID.ToString()).PadLeft(4, '0');
            formSerial = batchCode + formSerial;
            return formSerial;
        }

        #endregion
    }//End Of Class AdmissionForm_DAO

}//End Of Namespace DataAccess
