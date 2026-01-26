using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    public class Admission : Base
    {
        #region DBColumns
        /*
        [AdmissionID]           [int] 
	    [StudentID]             [int]
	    [PersonID]              [int]
	    [AdmissionCalenderID]   [int] 
	    [Remarks]               [varchar](500) 
	    [IsRule]                [bit] 
	    [IsLastAdmission]       [bit] 
	    [CreatedBy]             [int] 
	    [CreatedDate]           [datetime] 
	    [ModifiedBy]            [int] 
	    [ModifiedDate]          [datetime]
         */
        #endregion

        #region Variables
        private int _studentID;
        private int _personID;
        private int _admissionCalID;
        private string _remarks;
        private bool _isRule;
        private bool _isLastAdmission;
        private Student _student = null;
        private Person _person = null;
        #endregion

        #region Constructor
        public Admission()
        {
            _studentID = 0;
            _personID = 0;
            _admissionCalID = 0;
            _remarks = string.Empty;
            _isRule = true;
            _isLastAdmission = false;
        }
        #endregion

        #region Constants

        #region Column Constants
        private const string ADMISSIONID = "AdmissionID";

        private const string STUDENTID = "StudentID";
        private const string STUDENTID_PA = "@StudentID";

        private const string PERSONID = "PersonID";
        private const string PERSONID_PA = "@PersonID";

        private const string ADMISSIONCALID = "AdmissionCalenderID";
        private const string ADMISSIONCALID_PA = "@AdmissionCalenderID";

        private const string REMARKS = "Remarks";
        private const string REMARKS_PA = "@Remarks";

        private const string ISRULE = "IsRule";
        private const string ISRULE_PA = "@IsRule";

        private const string ISLASTADMISSION = "IsLastAdmission";
        private const string ISLASTADMISSION_PA = "@IsLastAdmission";
        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + ADMISSIONID + "], "
                                        + "[" + STUDENTID + "], "
                                        + "[" + PERSONID + "], "
                                        + "[" + ADMISSIONCALID + "], "
                                        + "[" + ISRULE + "], "
                                        + "[" + ISLASTADMISSION + "], "
                                        + "[" + REMARKS + "], ";
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + STUDENTID + "], "
                                        + "[" + PERSONID + "], "
                                        + "[" + ADMISSIONCALID + "], "
                                        + "[" + ISRULE + "], "
                                        + "[" + ISLASTADMISSION + "], "
                                        + "[" + REMARKS + "], ";

        #endregion

        private const string TABLENAME = " [Admission] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + NOPKCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
            //+ ID_PA + ", "
                    + STUDENTID_PA + ", "
                    + PERSONID_PA + ", "
                    + ADMISSIONCALID_PA + ", "
                    + ISRULE_PA + ", "
                    + ISLASTADMISSION_PA + ", "
                    + REMARKS_PA + ", "

                    + CREATORID_PA + ", "
                    + CREATEDDATE_PA + ", "
                    + MODIFIERID_PA + ", "
                    + MOIDFIEDDATE_PA + ")";
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + STUDENTID + "] =" + STUDENTID_PA + ", "
                    + "[" + PERSONID + "] = " + PERSONID_PA + ", "
                    + "[" + ADMISSIONCALID + "] = " + ADMISSIONCALID_PA + ", "
                    + "[" + ISRULE + "] = " + ISRULE_PA + ", "
                    + "[" + ISLASTADMISSION + "] = " + ISLASTADMISSION_PA + ", "
                    + "[" + REMARKS + "] = " + REMARKS_PA + ", "

                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;
        #endregion

        #region DELETE
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #endregion

        #region Properties

        public int StudentId
        {
            get { return this._studentID; }
            set { this._studentID = value; }
        }
        private SqlParameter StudentIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STUDENTID_PA;
                if (StudentId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = StudentId;
                }
                return sqlParam;
            }
        }

        public int PersonId
        {
            get { return this._personID; }
            set { this._personID = value; }
        }
        private SqlParameter PersonIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PERSONID_PA;
                if (PersonId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = PersonId;
                }
                return sqlParam;
            }
        }

        public int AdmissionCalId
        {
            get { return this._admissionCalID; }
            set { this._admissionCalID = value; }
        }
        private SqlParameter AdmissionCalIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ADMISSIONCALID_PA;
                if (AdmissionCalId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AdmissionCalId;
                }
                return sqlParam;
            }
        }

        public bool IsRule
        {
            get { return _isRule; }
            set { _isRule = value; }
        }
        private SqlParameter IsRuleParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISRULE_PA;
                sqlParam.Value = _isRule;

                return sqlParam;
            }
        }

        public bool IsLastAdmission
        {
            get { return _isLastAdmission; }
            set { _isLastAdmission = value; }
        }
        private SqlParameter IsLastAdmissionParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISLASTADMISSION_PA;
                sqlParam.Value = _isLastAdmission;

                return sqlParam;
            }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private SqlParameter RemarksParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = REMARKS_PA;
                if (Remarks == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = Remarks;
                }
                return sqlParam;
            }
        }

        public Student Student
        {
            get
            {
                if (_student == null)
                {
                    if (_studentID > 0)
                    {
                        _student = Student.GetStudent(_studentID);
                    }
                }
                return _student;
            }
        }

        public Person Person
        {
            get
            {
                if (_person == null)
                {
                    if (_personID > 0)
                    {
                        _person = Person.GetPersonById(_personID);
                    }
                }
                return _person;
            }
        }

        #endregion

        #region Functions
        private static Admission admissionMapper(SQLNullHandler nullHandler)
        {
            Admission admission = new Admission();

            admission.Id = nullHandler.GetInt32(ADMISSIONID);
            admission.StudentId = nullHandler.GetInt32(STUDENTID);
            admission.PersonId = nullHandler.GetInt32(PERSONID);
            admission.AdmissionCalId = nullHandler.GetInt32(ADMISSIONCALID);
            admission.IsRule = nullHandler.GetBoolean(ISRULE);
            admission.IsLastAdmission = nullHandler.GetBoolean(ISLASTADMISSION);
            admission.Remarks = nullHandler.GetString(REMARKS);


            admission.CreatorID = nullHandler.GetInt32(CREATORID);
            admission.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            admission.ModifierID = nullHandler.GetInt32(MODIFIERID);
            admission.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return admission;
        }

        private static List<Admission> mapAdmissions(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Admission> admissions = null;

            while (theReader.Read())
            {
                if (admissions == null)
                {
                    admissions = new List<Admission>();
                }
                Admission admission = admissionMapper(nullHandler);
                admissions.Add(admission);
            }

            return admissions;
        }

        private static Admission mapAdmission(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Admission admission = null;
            if (theReader.Read())
            {
                admission = new Admission();
                admission = admissionMapper(nullHandler);
            }

            return admission;
        }
        #endregion

        internal static int saveAdmission(Admission admission, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;


                if (admission.Id == 0)
                {
                    #region Insert
                    command = INSERT;
                    sqlParams = new SqlParameter[] { //admission.IDParam,
                                                 admission.StudentIdParam,
                                                 admission.PersonIdParam,
                                                 admission.AdmissionCalIdParam,
                                                 admission.IsRuleParam,
                                                 admission.IsLastAdmissionParam,
                                                 admission.RemarksParam,
                                                 admission.CreatorIDParam,
                                                 admission.CreatedDateParam,
                                                 admission.ModifierIDParam,
                                                 admission.ModifiedDateParam};
                    #endregion
                }
                else
                {
                    #region Update
                    //command = UPDATE
                    //+ " WHERE [" + NODEID + "] = " + ID_PA;
                    //sqlParams = new SqlParameter[] { node.NameParam,  
                    //                             node.IsLastLevelParam, 
                    //                             node.MinCreditParam, 
                    //                             node.MaxCreditParam,  
                    //                             node.MinCoursesParam,  
                    //                             node.MaxCoursesParam,
                    //                             node.IsActiveParam,
                    //                             node.IsVirtualParam,
                    //                             node.IsBundleParam,
                    //                             node.IsAssociatedParam,
                    //                             node.OperatorIDParam,
                    //                             node.CreatorIDParam,
                    //                             node.CreatedDateParam,
                    //                             node.ModifierIDParam,
                    //                             node.ModifiedDateParam,
                    //                             node.IDParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                return counter;

            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static Admission GetAdmissionById(int studentId, int personId)
        {
            Admission admission = new Admission();

            string command = SELECT + "WHERE [" + STUDENTID + "] = " + STUDENTID_PA + " AND [" + PERSONID + "] = " + PERSONID_PA;

            //SqlParameter sqlParam = new SqlParameter(ID_PA, id);

            SqlParameter[] sqlParam = new SqlParameter[] { new SqlParameter(STUDENTID_PA, studentId), new SqlParameter(PERSONID_PA, personId) };

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, sqlParam);

            admission = mapAdmission(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return admission;
        }

        public static Admission GetAdmissionByStdIDandAcaCalID(int stdID, int acaCalID)
        {
            Admission ad = null;

            string command = SELECT
                                + " WHERE AdmissionCalenderID = " + acaCalID + " and StudentID = " + stdID;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            ad = mapAdmission(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();
            return ad;
        }

        public static List<Admission> GetAdmittedStudentsByAcaCalandProgID(int AdmCalID, int progID)
        {
            List<Admission> admStds = new List<Admission>();
            List<Student> stds = Student.GetStudentsByProgID(progID);
            if (stds != null)
            {
                foreach (Student std in stds)
                {
                    Admission ad = Admission.GetAdmissionByStdIDandAcaCalID(std.Id, AdmCalID);
                    if (ad != null)
                    {
                        admStds.Add(ad);
                    }
                }
            }
            MSSqlConnectionHandler.CloseDbConnection();
            return admStds;
        }
    }
}
