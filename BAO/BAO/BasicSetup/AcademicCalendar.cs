using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class AcademicCalender : Base
    {

        #region Variables
        private int _calenderUnitTypeID;
        private CalenderUnitType _calenderUnitType;
        private string _Code;
        private int _year;
        private bool _isCurrent;
        private bool _isNext;
        private DateTime _startDate;
        private DateTime _endDate;
        private DateTime _fullPayNoFineLstDt;
        private DateTime _firstInstNoFineLstDt;
        private DateTime _secInstNoFineLstDt;
        private DateTime _thirdInstNoFineLstDs;
        private DateTime _addDropLastDateFull;
        private DateTime _addDropLastDateHalf;
        private DateTime _lastDateEnrollNoFine;
        private DateTime _lastDateEnrollWFine;
        private string _lastCode;

        private DateTime _admissionStartDate;
        private DateTime _admissionEndDate;
        private bool _isActiveAdmission;
        private DateTime _registrationStartDate;
        private DateTime _registrationEndDate;
        private bool _isActiveRegistration;

        #endregion

        #region Constants

        #region ColDefs
        private const string ACADEMIC_CALENDER_ID = "AcademicCalenderID";

        private const string CODE = "Code";
        private const string CODE_PA = "@Code";

        private const string CALENDER_UNIT_TYPE_ID = "CalenderUnitTypeID";
        private const string CALENDER_UNIT_TYPE_ID_PA = "@CalenderUnitTypeID";

        private const string YEAR = "Year";
        private const string YEAR_PA = "@Year";

        private const string ISCURRENT = "IsCurrent";
        private const string ISCURRENT_PA = "@IsCurrent";

        private const string ISNEXT = "IsNext";
        private const string ISNEXT_PA = "@IsNext";

        private const string STARTDATE = "StartDate";
        private const string STARTDATE_PA = "@StartDate";

        private const string ENDDATE = "EndDate";
        private const string ENDDATE_PA = "@EndDate";

        //..........
        private const string ADMISTARTDATE = "AdmissionStartDate";
        private const string ADMISTARTDATE_PA = "@AdmissionStartDate";

        private const string ADMIENDDATE = "AdmissionEndDate";
        private const string ADMIENDDATE_PA = "@AdmissionEndDate";

        private const string ISACTIVEADMI = "IsActiveAdmission";
        private const string ISACTIVEADMI_PA = "@IsActiveAdmission";

        private const string REGISTARTDATE = "RegistrationStartDate";
        private const string REGISTARTDATE_PA = "@RegistrationStartDate";

        private const string REGIENDDATE = "RegistrationEndDate";
        private const string REGIENDDATE_PA = "@RegistrationEndDate";

        private const string ISACTIVEREGI = "IsActiveRegistration";
        private const string ISACTIVEREGI_PA = "@IsActiveRegistration";

        #endregion

        #region All Columns
        private const string ALLCOLUMNS = ACADEMIC_CALENDER_ID + ", "
                                + CODE + ", "
                                + CALENDER_UNIT_TYPE_ID + ", "
                                + YEAR + ", "
                                + ISCURRENT + ", "
                                + ISNEXT + ", "
                                + STARTDATE + ", "
                                + ENDDATE + ", "

                                + ADMISTARTDATE + ", "
                                + ADMIENDDATE + ", "
                                + ISACTIVEADMI + ", "
                                + REGISTARTDATE + ", "
                                + REGIENDDATE + ", "
                                + ISACTIVEREGI + ", ";
        #endregion

        #region No PKColumns
        private const string NOPKCOLUMNS = CODE + ", "
                                + CALENDER_UNIT_TYPE_ID + ", "
                                + YEAR + ", "
                                + ISCURRENT + ", "
                                + ISNEXT + ", "
                                + STARTDATE + ", "
                                + ENDDATE + ", "

                                + ADMISTARTDATE + ", "
                                + ADMIENDDATE + ", "
                                + ISACTIVEADMI + ", "
                                + REGISTARTDATE + ", "
                                + REGIENDDATE + ", "
                                + ISACTIVEREGI + ", ";
        #endregion

        #region Table Name
        private const string TABLENAME = " [AcademicCalender] ";
        #endregion

        #region Select
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region Order by
        private const string OrderBy = " order by Code, Year";
        private const string OrderByDesc = " order by Code DESC, Year DESC";
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + NOPKCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
                     + CODE_PA + ", "
                     + CALENDER_UNIT_TYPE_ID_PA + ", "
                     + YEAR_PA + ", "
                     + ISCURRENT_PA + ", "
                     + ISNEXT_PA + ", "
                     + STARTDATE_PA + ", "
                     + ENDDATE_PA + ", "

                     + ADMISTARTDATE_PA + ", "
                     + ADMIENDDATE_PA + ", "
                     + ISACTIVEADMI_PA + ", "
                     + REGISTARTDATE_PA + ", "
                     + REGIENDDATE_PA + ", "
                     + ISACTIVEREGI_PA + ", "

                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET " + CODE + " = " + CODE_PA + ", "//1
                    + CALENDER_UNIT_TYPE_ID + " = " + CALENDER_UNIT_TYPE_ID_PA + ","//2
                    + YEAR + " = " + YEAR_PA + ","//3
                    + ISCURRENT + " = " + ISCURRENT_PA + ","//4
                    + ISNEXT + " = " + ISNEXT_PA + ", "
                    + STARTDATE + " = " + STARTDATE_PA + ","//5
                    + ENDDATE + " = " + ENDDATE_PA + ","//6                    

                    + CREATORID + " = " + CREATORID_PA + ","//7
                    + CREATEDDATE + " = " + CREATEDDATE_PA + ","//8
                    + MODIFIERID + " = " + MODIFIERID_PA + ","//9
                    + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA + ","//10

                    + ADMISTARTDATE + " = " + ADMISTARTDATE_PA + ","//11
                    + ADMIENDDATE + " = " + ADMIENDDATE_PA + ","//12
                    + ISACTIVEADMI + " = " + ISACTIVEADMI_PA + ","//13
                    + REGISTARTDATE + " = " + REGISTARTDATE_PA + ","//14
                    + REGIENDDATE + " = " + REGIENDDATE_PA + ","//15
                    + ISACTIVEREGI + " = " + ISACTIVEREGI_PA;//16
        #endregion

        #region DELETE
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #endregion

        #region Constructor
        public AcademicCalender()
            : base()
        {

            ////Must Change
            _calenderUnitTypeID = 0;
            _calenderUnitType = null;
            ////

            _Code = string.Empty;
            _year = 0;
            _isCurrent = false;
            _isNext = false;
            _endDate = DateTime.MinValue;
            _fullPayNoFineLstDt = DateTime.MinValue;
            _firstInstNoFineLstDt = DateTime.MinValue;
            _secInstNoFineLstDt = DateTime.MinValue;
            _thirdInstNoFineLstDs = DateTime.MinValue;
            _addDropLastDateFull = DateTime.MinValue;
            _addDropLastDateHalf = DateTime.MinValue;
            _lastDateEnrollNoFine = DateTime.MinValue;
            _lastDateEnrollWFine = DateTime.MinValue;
        }
        #endregion

        #region Properties

        public int CalenderUnitTypeID
        {
            get { return _calenderUnitTypeID; }
            set { _calenderUnitTypeID = value; }
        }
        private SqlParameter CalenderUnitTypeIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CALENDER_UNIT_TYPE_ID_PA;

                sqlParam.Value = CalenderUnitTypeID;

                return sqlParam;
            }
        }

        public CalenderUnitType CalenderUnitType
        {
            get
            {
                if (_calenderUnitType == null)
                {
                    if (this.CalenderUnitTypeID > 0)
                    {
                        _calenderUnitType = CalenderUnitType.GetCalUType(this.CalenderUnitTypeID);
                    }
                }
                return _calenderUnitType;
            }
            private set { _calenderUnitType = value; }
        }

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        private SqlParameter CodeParam
        {
            get
            {
                SqlParameter codeParam = new SqlParameter();
                codeParam.ParameterName = CODE_PA;

                codeParam.Value = Code;

                return codeParam;
            }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        private SqlParameter YearParam
        {
            get
            {
                SqlParameter codeParam = new SqlParameter();
                codeParam.ParameterName = YEAR_PA;

                codeParam.Value = Year;

                return codeParam;
            }
        }

        public bool IsCurrent
        {
            get { return _isCurrent; }
            set { _isCurrent = value; }
        }
        private SqlParameter IsCurrentParam
        {
            get
            {
                SqlParameter codeParam = new SqlParameter();
                codeParam.ParameterName = ISCURRENT_PA;

                codeParam.Value = IsCurrent;

                return codeParam;
            }
        }

        public bool IsNext
        {
            get { return _isNext; }
            set { _isNext = value; }
        }
        private SqlParameter IsNextParam
        {
            get
            {
                SqlParameter codeParam = new SqlParameter();
                codeParam.ParameterName = ISNEXT_PA;

                codeParam.Value = IsNext;

                return codeParam;
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        protected SqlParameter StartDateParam
        {
            get
            {
                SqlParameter startDateParam = new SqlParameter();
                startDateParam.ParameterName = STARTDATE_PA;

                if (StartDate == DateTime.MinValue)
                {
                    startDateParam.Value = DBNull.Value;
                }
                else
                {
                    startDateParam.Value = StartDate;
                }

                return startDateParam;
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        protected SqlParameter EndDateParam
        {
            get
            {
                SqlParameter endDateParam = new SqlParameter();
                endDateParam.ParameterName = ENDDATE_PA;

                if (EndDate == DateTime.MinValue)
                {
                    endDateParam.Value = DBNull.Value;
                }
                else
                {
                    endDateParam.Value = EndDate;
                }

                return endDateParam;
            }
        }

        public DateTime FullPayNoFineLstDt
        {
            get { return _fullPayNoFineLstDt; }
            set { _fullPayNoFineLstDt = value; }
        }
        public DateTime FirstInstNoFineLstDt
        {
            get { return _firstInstNoFineLstDt; }
            set { _firstInstNoFineLstDt = value; }
        }
        public DateTime SecInstNoFineLstDt
        {
            get { return _secInstNoFineLstDt; }
            set { _secInstNoFineLstDt = value; }
        }
        public DateTime ThirdInstNoFineLstDs
        {
            get { return _thirdInstNoFineLstDs; }
            set { _thirdInstNoFineLstDs = value; }
        }
        public DateTime AddDropLastDateFull
        {
            get { return _addDropLastDateFull; }
            set { _addDropLastDateFull = value; }
        }
        public DateTime AddDropLastDateHalf
        {
            get { return _addDropLastDateHalf; }
            set { _addDropLastDateHalf = value; }
        }
        public DateTime LastDateEnrollNoFine
        {
            get { return _lastDateEnrollNoFine; }
            set { _lastDateEnrollNoFine = value; }
        }
        public DateTime LastDateEnrollWFine
        {
            get { return _lastDateEnrollWFine; }
            set { _lastDateEnrollWFine = value; }
        }

        public string Name
        {
            get
            {
                return CalenderUnitType.TypeName + " " + Year.ToString();
            }
        }

        public string LastCode
        {
            get { return _lastCode; }
            set { _lastCode = value; }
        }

        //...............
        public DateTime AdmiStartDate
        {
            get { return _admissionStartDate; }
            set { _admissionStartDate = value; }
        }
        protected SqlParameter AdmiStartDateParam
        {
            get
            {
                SqlParameter admiStartDateParam = new SqlParameter();
                admiStartDateParam.ParameterName = ADMISTARTDATE_PA;

                if (AdmiStartDate == DateTime.MinValue)
                {
                    admiStartDateParam.Value = DBNull.Value;
                }
                else
                {
                    admiStartDateParam.Value = AdmiStartDate;
                }

                return admiStartDateParam;
            }
        }

        public DateTime AdmiEndDate
        {
            get { return _admissionEndDate; }
            set { _admissionEndDate = value; }
        }
        protected SqlParameter AdmiEndDateParam
        {
            get
            {
                SqlParameter admiEndDateParam = new SqlParameter();
                admiEndDateParam.ParameterName = ADMIENDDATE_PA;

                if (AdmiEndDate == DateTime.MinValue)
                {
                    admiEndDateParam.Value = DBNull.Value;
                }
                else
                {
                    admiEndDateParam.Value = AdmiEndDate;
                }

                return admiEndDateParam;
            }
        }

        public bool IsActiveAdmi
        {
            get { return _isActiveAdmission; }
            set { _isActiveAdmission = value; }
        }
        private SqlParameter IsActiveAdmiParam
        {
            get
            {
                SqlParameter isActiveAdmiParam = new SqlParameter();
                isActiveAdmiParam.ParameterName = ISACTIVEADMI_PA;

                isActiveAdmiParam.Value = IsActiveAdmi;

                return isActiveAdmiParam;
            }
        }

        public DateTime RegiStartDate
        {
            get { return _registrationStartDate; }
            set { _registrationStartDate = value; }
        }
        protected SqlParameter RegiStartDateParam
        {
            get
            {
                SqlParameter regiStartDateParam = new SqlParameter();
                regiStartDateParam.ParameterName = REGISTARTDATE_PA;

                if (RegiStartDate == DateTime.MinValue)
                {
                    regiStartDateParam.Value = DBNull.Value;
                }
                else
                {
                    regiStartDateParam.Value = RegiStartDate;
                }

                return regiStartDateParam;
            }
        }

        public DateTime RegiEndDate
        {
            get { return _registrationEndDate; }
            set { _registrationEndDate = value; }
        }
        protected SqlParameter RegiEndDateParam
        {
            get
            {
                SqlParameter regiEndDateParam = new SqlParameter();
                regiEndDateParam.ParameterName = REGIENDDATE_PA;

                if (RegiEndDate == DateTime.MinValue)
                {
                    regiEndDateParam.Value = DBNull.Value;
                }
                else
                {
                    regiEndDateParam.Value = RegiEndDate;
                }

                return regiEndDateParam;
            }
        }

        public bool IsActiveRegi
        {
            get { return _isActiveRegistration; }
            set { _isActiveRegistration = value; }
        }
        private SqlParameter IsActiveRegiParam
        {
            get
            {
                SqlParameter isActiveRegiParam = new SqlParameter();
                isActiveRegiParam.ParameterName = ISACTIVEREGI_PA;

                isActiveRegiParam.Value = IsActiveRegi;

                return isActiveRegiParam;
            }
        }


        #endregion

        #region Functions
        private static AcademicCalender Mapper(SQLNullHandler nullHandler)
        {
            AcademicCalender trimesterInfo = new AcademicCalender();

            trimesterInfo.Id = nullHandler.GetInt32("AcademicCalenderID");
            trimesterInfo.CalenderUnitTypeID = nullHandler.GetInt32("CalenderUnitTypeID");
            trimesterInfo.Code = nullHandler.GetString("Code");
            trimesterInfo.LastCode = nullHandler.GetString("Code");
            trimesterInfo.Year = nullHandler.GetInt32("Year");
            trimesterInfo.IsCurrent = nullHandler.GetBoolean("IsCurrent");
            trimesterInfo.IsNext = nullHandler.GetBoolean("IsNext");
            trimesterInfo.StartDate = nullHandler.GetDateTime("StartDate");
            trimesterInfo.EndDate = nullHandler.GetDateTime("EndDate");
            //trimesterInfo.FullPayNoFineLstDt = nullHandler.GetDateTime("FullPayNoFineLstDt");
            //trimesterInfo.FirstInstNoFineLstDt = nullHandler.GetDateTime("FirstInstNoFineLstDt");
            //trimesterInfo.SecInstNoFineLstDt = nullHandler.GetDateTime("SecInstNoFineLstDt");
            //trimesterInfo.ThirdInstNoFineLstDs = nullHandler.GetDateTime("ThirdInstNoFineLstDs");
            //trimesterInfo.AddDropLastDateFull = nullHandler.GetDateTime("AddDropLastDateFull");
            //trimesterInfo.AddDropLastDateHalf = nullHandler.GetDateTime("AddDropLastDateHalf");
            //trimesterInfo.LastDateEnrollNoFine = nullHandler.GetDateTime("LastDateEnrollNoFine");
            //trimesterInfo.LastDateEnrollWFine = nullHandler.GetDateTime("LastDateEnrollWFine");
            trimesterInfo.CreatorID = nullHandler.GetInt32("CreatedBy");
            trimesterInfo.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            trimesterInfo.ModifierID = nullHandler.GetInt32("ModifiedBy");
            trimesterInfo.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            trimesterInfo.AdmiStartDate = nullHandler.GetDateTime("AdmissionStartDate");
            trimesterInfo.AdmiEndDate = nullHandler.GetDateTime("AdmissionEndDate");
            trimesterInfo.IsActiveAdmi = nullHandler.GetBoolean("IsActiveAdmission");
            trimesterInfo.RegiStartDate = nullHandler.GetDateTime("RegistrationStartDate");
            trimesterInfo.RegiEndDate = nullHandler.GetDateTime("RegistrationEndDate");
            trimesterInfo.IsActiveRegi = nullHandler.GetBoolean("IsActiveRegistration");

            return trimesterInfo;
        }
        private static List<AcademicCalender> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<AcademicCalender> trimesterInfos = null;

            while (theReader.Read())
            {
                if (trimesterInfos == null)
                {
                    trimesterInfos = new List<AcademicCalender>();
                }
                AcademicCalender trimesterInfo = Mapper(nullHandler);
                trimesterInfos.Add(trimesterInfo);
            }

            return trimesterInfos;
        }
        private static AcademicCalender MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            AcademicCalender student = null;
            if (theReader.Read())
            {
                student = new AcademicCalender();
                student = Mapper(nullHandler);
            }

            return student;
        }

        public static List<AcademicCalender> Gets()
        {
            List<AcademicCalender> trimesterInfos = new List<AcademicCalender>();

            string command = SELECT + OrderBy;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            trimesterInfos = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return trimesterInfos;
        }
        public static AcademicCalender Get(int trimesterID)
        {
            AcademicCalender trimesterInfo = new AcademicCalender();

            string command = SELECT
                            + "WHERE AcademicCalenderID = " + trimesterID.ToString();

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            trimesterInfo = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return trimesterInfo;
        }
        public static List<AcademicCalender> GetsbyBatch(string code)
        {
            List<AcademicCalender> trimesterInfos = new List<AcademicCalender>();

            string command = SELECT
                            + "WHERE Code = " + CODE_PA;
            SqlParameter sqlParam = new SqlParameter(CODE_PA, code);
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            trimesterInfos = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return trimesterInfos;
        }
        public static AcademicCalender GetsbyCode(string code)
        {
            AcademicCalender trimesterInfos = new AcademicCalender();

            string command = SELECT
                            + "WHERE Code = " + CODE_PA;
            SqlParameter sqlParam = new SqlParameter(CODE_PA, code);
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            trimesterInfos = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return trimesterInfos;
        }
        public static AcademicCalender GetCurrent()
        {
            AcademicCalender trimesterInfo = new AcademicCalender();

            string command = SELECT
                            + "WHERE IsCurrent = 1";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            trimesterInfo = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return trimesterInfo;
        }
        public static AcademicCalender GetNext()
        {
            AcademicCalender trimesterInfo = new AcademicCalender();

            string command = SELECT
                            + "WHERE IsNext = 1";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            trimesterInfo = MapClass(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return trimesterInfo;
        }
        public static List<AcademicCalender> Gets(string parameters)
        {
            List<AcademicCalender> trimesterInfos = new List<AcademicCalender>();

            string command = "SELECT AC.AcademicCalenderID, AC.Code, AC.CalenderUnitTypeID, AC.Year, AC.IsCurrent, AC.IsNext, AC.StartDate, AC.EndDate, AC.AdmissionStartDate, AC.AdmissionEndDate, AC.IsActiveAdmission, AC.RegistrationStartDate, AC.RegistrationEndDate, AC.IsActiveRegistration, AC.CreatedBy, AC.CreatedDate, AC.ModifiedBy, AC.ModifiedDate "
                            + " FROM AcademicCalender as AC "
                            + " join CalenderUnitType as CUT on AC.CalenderUnitTypeID = CUT.CalenderUnitTypeID "
                            + " WHERE CUT.CalenderUnitMasterID =" + parameters + " " + OrderByDesc;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            trimesterInfos = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return trimesterInfos;
        }

        public static List<AcademicCalender> GetsByTremisterNSearchText(string parameters, string searchText)
        {

            List<AcademicCalender> trimesterInfos = new List<AcademicCalender>();

            string command = "SELECT AC.AcademicCalenderID, AC.Code, AC.CalenderUnitTypeID, AC.Year, AC.IsCurrent, AC.IsNext, AC.StartDate, AC.EndDate, AC.AdmissionStartDate, AC.AdmissionEndDate, AC.IsActiveAdmission, AC.RegistrationStartDate, AC.RegistrationEndDate, AC.IsActiveRegistration, AC.CreatedBy, AC.CreatedDate, AC.ModifiedBy, AC.ModifiedDate  "
                            + " FROM AcademicCalender as AC "
                            + " join CalenderUnitType as CUT on AC.CalenderUnitTypeID = CUT.CalenderUnitTypeID "
                            + " WHERE CUT.CalenderUnitMasterID =" + parameters + " and CUT.TypeName = " + "'" + searchText + "'" + "  " + OrderByDesc;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            trimesterInfos = MapCollection(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return trimesterInfos;

        }

        public static bool IsExist(string code)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [" + CODE + "] = " + CODE_PA;
            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { codeParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        internal static bool IsExist(string code, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [" + CODE + "] = " + CODE_PA;
            SqlParameter codeParam = MSSqlConnectionHandler.MSSqlParamGenerator(code, CODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { codeParam });

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicateCode(AcademicCalender dept)
        {
            if (dept == null)
            {
                return AcademicCalender.IsExist(dept.Code);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (AcademicCalender.IsExist(dept.Code))
                    {
                        return AcademicCalender.IsExist(dept.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (dept.Code != dept.LastCode)
                    {
                        return AcademicCalender.IsExist(dept.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public static bool HasDuplicateCode(AcademicCalender dept, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (dept == null)
            {
                return AcademicCalender.IsExist(dept.Code, sqlConn, sqlTran);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (AcademicCalender.IsExist(dept.Code, sqlConn, sqlTran))
                    {
                        return true;//AcademicCalender.IsExist(dept.BatchCode, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (dept.Code != dept.LastCode)
                    {
                        return AcademicCalender.IsExist(dept.Code, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Save(AcademicCalender trimesterInfo)
        {
            int counter = 0;
            int calenderUnitMasterId = GetAcademinCalenderUnitMasterId(trimesterInfo.Id);
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (HasDuplicateCode(trimesterInfo, sqlConn, sqlTran))
            {
                throw new Exception("Duplicate Batch Not Allowed.");
            }


            if (calenderUnitMasterId > 0)
            {
                if (trimesterInfo.IsCurrent)
                {
                    ResetCurrent(sqlConn, sqlTran, calenderUnitMasterId);
                }

                if (trimesterInfo.IsNext)
                {
                    ResetNext(sqlConn, sqlTran, calenderUnitMasterId);
                }

                if (trimesterInfo.IsActiveRegi)
                {
                    ResetRegistration(sqlConn, sqlTran, calenderUnitMasterId);
                }
            }

            if (trimesterInfo.Id == 0)
            {
                command = INSERT;
                sqlParams = new SqlParameter[] { trimesterInfo.CodeParam,  
                                                     trimesterInfo.CalenderUnitTypeIDParam,
                                                     trimesterInfo.YearParam,
                                                     trimesterInfo.IsCurrentParam,
                                                     trimesterInfo.IsNextParam,
                                                     trimesterInfo.StartDateParam,
                                                     trimesterInfo.EndDateParam,
                                                 
                                                     trimesterInfo.AdmiStartDateParam,
                                                     trimesterInfo.AdmiEndDateParam,
                                                     trimesterInfo.IsActiveAdmiParam,
                                                     trimesterInfo.RegiStartDateParam,
                                                     trimesterInfo.RegiEndDateParam,
                                                     trimesterInfo.IsActiveRegiParam,
                
                                                     trimesterInfo.CreatorIDParam, 
                                                     trimesterInfo.CreatedDateParam, 
                                                     trimesterInfo.ModifierIDParam, 
                                                     trimesterInfo.ModifiedDateParam};
            }
            else
            {
                command = UPDATE
                        + " WHERE AcademicCalenderID = " + ID_PA;
                sqlParams = new SqlParameter[] { trimesterInfo.CodeParam,  
                                                         trimesterInfo.CalenderUnitTypeIDParam,
                                                         trimesterInfo.YearParam,
                                                         trimesterInfo.IsCurrentParam,
                                                         trimesterInfo.IsNextParam,
                                                         trimesterInfo.StartDateParam,
                                                         trimesterInfo.EndDateParam,
                                                         trimesterInfo.CreatorIDParam, 
                                                         trimesterInfo.CreatedDateParam, 
                                                         trimesterInfo.ModifierIDParam, 
                                                         trimesterInfo.ModifiedDateParam,
                                                         trimesterInfo.IDParam,
                                                     
                                                         trimesterInfo.AdmiStartDateParam,
                                                         trimesterInfo.AdmiEndDateParam,
                                                         trimesterInfo.IsActiveAdmiParam,
                                                         trimesterInfo.RegiStartDateParam,
                                                         trimesterInfo.RegiEndDateParam,
                                                         trimesterInfo.IsActiveRegiParam};
            }

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);


            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }

        private static int GetAcademinCalenderUnitMasterId(int academicCalenderId)
        {
            try
            {
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                AcademicCalender trimesterInfo = new AcademicCalender();
                int calenderMasterId = 0;
                string command = string.Empty;
                command = " SELECT cut.CalenderUnitMasterID"
                    + " FROM AcademicCalender as ac "
                    + " join CalenderUnitType as cut on ac.CalenderUnitTypeID = cut.CalenderUnitTypeID"
                    + " where ac.AcademicCalenderID = " + academicCalenderId;

                object theReader = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

                calenderMasterId = Convert.ToInt32(theReader);
                MSSqlConnectionHandler.CloseDbConnection();
                return calenderMasterId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static int ResetNext(SqlConnection sqlConn, SqlTransaction sqlTran, int unitMasterId)
        {
            int counter = 0;
            string command = string.Empty;
            command = " UPDATE AcademicCalender "
                    + " SET IsNext = 0 "
                    + " FROM AcademicCalender as ac"
                    + " join CalenderUnitType as cut on ac.CalenderUnitTypeID = cut.CalenderUnitTypeID"
                    + " WHERE ac.IsNext = 1 and cut.CalenderUnitMasterID =" + unitMasterId;


            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        internal static int ResetCurrent(SqlConnection sqlConn, SqlTransaction sqlTran, int unitMasterId)
        {
            int counter = 0;
            string command = string.Empty;
            command = " UPDATE AcademicCalender "
                    + " SET IsCurrent = 0 "
                    + " FROM AcademicCalender as ac"
                    + " join CalenderUnitType as cut on ac.CalenderUnitTypeID = cut.CalenderUnitTypeID"
                    + " WHERE ac.IsCurrent = 1 and cut.CalenderUnitMasterID =" + unitMasterId;

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        private static int ResetRegistration(SqlConnection sqlConn, SqlTransaction sqlTran, int calenderUnitMasterId)
        {
            int counter = 0;
            string command = string.Empty;
            command = " UPDATE AcademicCalender "
                    + " SET IsActiveRegistration = 0 "
                    + " FROM AcademicCalender as ac"
                    + " join CalenderUnitType as cut on ac.CalenderUnitTypeID = cut.CalenderUnitTypeID"
                    + " WHERE ac.IsActiveRegistration = 1 and cut.CalenderUnitMasterID =" + calenderUnitMasterId;

            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);

            return counter;
        }

        public static int Delete(int trimesterID)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = DELETE
                            + "WHERE AcademicCalenderID =" + trimesterID.ToString();
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteAction(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        #endregion

        internal static string GetCode(int acaCalId, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT Code FROM" + TABLENAME
                              + "WHERE [AcademicCalenderID] = " + ID_PA;
            SqlParameter idParam = MSSqlConnectionHandler.MSSqlParamGenerator(acaCalId, ID_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { idParam });

            return Convert.ToString(ob);
        }
    }
}
