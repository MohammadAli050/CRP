using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Course : Base
    {
        #region Variables
        private int _versionID;
        private string _formalCode;
        private string _versionCode;

        private string _title;

        private int _assocCourseID;
        private int _assocVersionID;
        private Course _associatedCourse;

        private int _startACUID;
        private AcademicCalender _startACU;

        private int _ownerProgID;
        private Program _ownerProgram;

        private string _courseContent;

        private bool _isCredit;
        private decimal _credits;

        private bool _isSectionMandatory;
        private bool _hasEquivalents;
        private bool _hasMultipleACUSpan;
        private bool _isActive;

        private int _courseGroupId;

        private List<Course> _equiCourse = null;

        private List<EquivalentCourse> _equivalents = null;

        private int _courseACUSpanMasID = 0;
        private CourseACUSpanMas _courseACUSpanMas = null;
        private int _typeDefinitionID;
        #endregion

        #region Constants
        #region Course Columns
        private const string COURSEID = "CourseID";//0
        private const string VERSIONID = "VersionID";//1
        private const string VERSIONID_PA = "@VersionID";

        private const string FORMALCODE = "FormalCode";//2
        private const string FORMALCODE_PA = "@FormalCode";
        private const string VERSIONCODE = "VersionCode";//3
        private const string VERSIONCODE_PA = "@VersionCode";

        private const string TITLE = "Title";//4
        private const string TITLE_PA = "@Title";

        private const string ASSOCCOURSEID = "AssocCourseID";//5
        private const string ASSOCCOURSEID_PA = "@AssocCourseID";
        private const string ASSOCVERSIONID = "AssocVersionID";//6
        private const string ASSOCVERSIONID_PA = "@AssocVersionID";

        private const string STARTACADEMICCALENDERID = "StartAcademicCalenderID";//7
        private const string STARTACADEMICCALENDERID_PA = "@StartAcademicCalenderID";

        private const string PROGRAMID = "ProgramID";//8
        private const string PROGRAMID_PA = "@ProgramID";

        private const string COURSECONTENT = "CourseContent";//9
        private const string COURSECONTENT_PA = "@CourseContent";

        private const string ISCREDITCOURSE = "IsCreditCourse";//10
        private const string ISCREDITCOURSE_PA = "@IsCreditCourse";

        private const string CREDITS = "Credits";//11
        private const string CREDITS_PA = "@Credits";

        private const string ISSECTIONMANDATORY = "IsSectionMandatory";//12
        private const string ISSECTIONMANDATORY_PA = "@IsSectionMandatory";

        private const string HASEQUIVALENTS = "HasEquivalents";//13
        private const string HASEQUIVALENTS_PA = "@HasEquivalents";

        private const string HASMULTIPLEACUSPAN = "HasMultipleACUSpan";//14
        private const string HASMULTIPLEACUSPAN_PA = "@HasMultipleACUSpan";

        private const string ISACTIVE = "IsActive";//15
        private const string ISACTIVE_PA = "@IsActive";

        private const string TYPEDEFINITIONID = "TypeDefinitionID";
        private const string TYPEDEFINITIONID_PA = "@TypeDefinitionID";

        private const string COURSEGROUPID = "CourseGroupId";
        private const string COURSEGROUPID_PA = "@CourseGroupId";

        #endregion

        #region All Columns
        private const string ALLCOLUMNS = "[" + COURSEID + "], "//0
                                + "[" + VERSIONID + "], "//1
                                + "[" + FORMALCODE + "], "//2
                                + "[" + VERSIONCODE + "], "//3
                                + "[" + TITLE + "], "//4
                                + "[" + ASSOCCOURSEID + "], "//5
                                + "[" + ASSOCVERSIONID + "], "//6
                                + "[" + STARTACADEMICCALENDERID + "], "//7
                                + "[" + PROGRAMID + "], "//8
                                + "[" + COURSECONTENT + "], "//9
                                + "[" + ISCREDITCOURSE + "], "//10
                                + "[" + CREDITS + "], "//11
                                + "[" + ISSECTIONMANDATORY + "], "//12
                                + "[" + HASEQUIVALENTS + "], "//13
                                + "[" + HASMULTIPLEACUSPAN + "], "//14
                                + "[" + ISACTIVE + "], "
                                + "[" + TYPEDEFINITIONID + "], "//15 
                                + "[" + COURSEGROUPID + "], ";
        #endregion

        #region No PK Columns
        private const string NOPKCOLUMNS = "[" + FORMALCODE + "], "//2
                                + "[" + VERSIONCODE + "], "//3
                                + "[" + TITLE + "], "//4
                                + "[" + ASSOCCOURSEID + "], "//5
                                + "[" + ASSOCVERSIONID + "], "//6
                                + "[" + STARTACADEMICCALENDERID + "], "//7
                                + "[" + PROGRAMID + "], "//8
                                + "[" + COURSECONTENT + "], "//9
                                + "[" + ISCREDITCOURSE + "], "//10
                                + "[" + CREDITS + "], "//11
                                + "[" + ISSECTIONMANDATORY + "], "//12
                                + "[" + HASEQUIVALENTS + "], "//13
                                + "[" + HASMULTIPLEACUSPAN + "], "//14
                                + "[" + ISACTIVE + "], "
                                + "[" + TYPEDEFINITIONID + "], "
                                + "[" + COURSEGROUPID + "], ";
        #endregion

        private const string TABLENAME = " [Course] ";

        #region Select
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS
                     + ")"
                     + "VALUES ( "
                     + ID_PA + ", "//0
                     + VERSIONID_PA + ", "//1
                     + FORMALCODE_PA + ", "//2
                     + VERSIONCODE_PA + ", "//3
                     + TITLE_PA + ", "//4
                     + ASSOCCOURSEID_PA + ", "//5
                     + ASSOCVERSIONID_PA + ", "//6
                     + STARTACADEMICCALENDERID_PA + ", "//7
                     + PROGRAMID_PA + ", "//8
                     + COURSECONTENT_PA + ", "//9
                     + ISCREDITCOURSE_PA + ", "//10
                     + CREDITS_PA + ", "//11
                     + ISSECTIONMANDATORY_PA + ", "//12
                     + HASEQUIVALENTS_PA + ", "//13
                     + HASMULTIPLEACUSPAN_PA + ", "//14
                     + ISACTIVE_PA + ", "//15
                     + TYPEDEFINITIONID_PA + ", "
                     + COURSEGROUPID_PA + ", "
                     + CREATORID_PA + ", "
                     + CREATEDDATE_PA + ", "
                     + MODIFIERID_PA + ", "
                     + MOIDFIEDDATE_PA + ")";
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME
                + "SET [" + FORMALCODE + "] = " + FORMALCODE_PA + ", "
                + "[" + VERSIONCODE + "] = " + VERSIONCODE_PA + ", "
                + "[" + TITLE + "] = " + TITLE_PA + ", "
                + "[" + ASSOCCOURSEID + "] = " + ASSOCCOURSEID_PA + ", "//5
                + "[" + ASSOCVERSIONID + "] = " + ASSOCVERSIONID_PA + ", "//6
                + "[" + STARTACADEMICCALENDERID + "] = " + STARTACADEMICCALENDERID_PA + ", "//7
                + "[" + PROGRAMID + "] = " + PROGRAMID_PA + ", "//8
                + "[" + COURSECONTENT + "] = " + COURSECONTENT_PA + ", "//9
                + "[" + ISCREDITCOURSE + "] = " + ISCREDITCOURSE_PA + ", "//10
                + "[" + CREDITS + "] = " + CREDITS_PA + ", "//11
                + "[" + ISSECTIONMANDATORY + "] = " + ISSECTIONMANDATORY_PA + ", "//12
                + "[" + HASEQUIVALENTS + "] = " + HASEQUIVALENTS_PA + ", "//13
                + "[" + HASMULTIPLEACUSPAN + "] = " + HASMULTIPLEACUSPAN_PA + ", "//14
                + "[" + ISACTIVE + "] = " + ISACTIVE_PA + ", "//15
                + "[" + TYPEDEFINITIONID + "] = " + TYPEDEFINITIONID_PA + ", "//15
                + "[" + COURSEGROUPID + "] = " + COURSEGROUPID_PA + ", "//15
                + "[" + CREATORID + "] = " + CREATORID_PA + ","//16
                + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ","//17
                + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ","//18
                + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//19 
        #endregion

        private const string DELETE = "DELETE" + TABLENAME;
        #endregion

        #region Constructor
        public Course()
            : base()
        {
            _versionID = 0;//0
            _formalCode = string.Empty;//1
            _versionCode = string.Empty;//2
            _title = string.Empty;//3
            _assocCourseID = 0;//4
            _assocVersionID = 0;//5
            _associatedCourse = null;//6
            _startACUID = 0;//7
            _ownerProgID = 0;//8
            _courseContent = string.Empty;//9
            _isCredit = true;//10
            _credits = 0;//11
            _isSectionMandatory = true;//12
            _hasEquivalents = false;//13
            _hasMultipleACUSpan = false;//14
            _isActive = true;//15
            _equiCourse = null;//16
            _courseACUSpanMas = null;//17
            _equivalents = null;//18
            _typeDefinitionID = 0;
        }
        #endregion

        #region Properties

        #region VersionID
        /// <summary>
        /// Second Part of the primary key
        /// </summary>
        public int VersionID
        {
            get { return _versionID; }
            set { _versionID = value; }
        }
        private SqlParameter VersionIDParam
        {
            get
            {
                SqlParameter versionIDParam = new SqlParameter();
                versionIDParam.ParameterName = VERSIONID_PA;
                if (_versionID == 0)
                {
                    versionIDParam.Value = DBNull.Value;
                }
                else
                {
                    versionIDParam.Value = _versionID;
                }
                return versionIDParam;
            }
        }
        #endregion

        #region Formal Code
        /// <summary>
        /// Formal Code
        /// </summary>
        public string FormalCode
        {
            get { return _formalCode; }
            set { _formalCode = value; }
        }
        private SqlParameter FormalCodeParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = FORMALCODE_PA;

                sqlParam.Value = _formalCode;

                return sqlParam;
            }
        }
        #endregion

        #region Version Code
        /// <summary>
        /// Version Code
        /// </summary>
        public string VersionCode
        {
            get { return _versionCode; }
            set { _versionCode = value; }
        }
        private SqlParameter VersionCodeParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = VERSIONCODE_PA;
                if (_versionCode.Trim() == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = _versionCode;
                }
                return sqlParam;
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private SqlParameter TitleParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TITLE_PA;
                if (_title.Trim() == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = _title;
                }

                return sqlParam;
            }
        }
        #endregion

        #region Associated Course
        /// <summary>
        /// Associated Course ID
        /// </summary>
        public int AssocCourseID
        {
            get { return _assocCourseID; }
            set { _assocCourseID = value; }
        }
        private SqlParameter AssocCourseIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ASSOCCOURSEID_PA;
                if (AssocCourseID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AssocCourseID;
                }
                return sqlParam;
            }
        }
        /// <summary>
        /// Associated Course Version Id
        /// </summary>
        public int AssocVersionID
        {
            get { return _assocVersionID; }
            set { _assocVersionID = value; }
        }
        private SqlParameter AssocVersionIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ASSOCVERSIONID_PA;
                if (AssocVersionID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AssocVersionID;
                }
                return sqlParam;
            }
        }
        /// <summary>
        /// Associated Course
        /// </summary>
        public Course AssociatedCourse
        {
            get
            {
                if (_associatedCourse == null)
                {
                    if ((_assocCourseID > 0) && (_assocVersionID > 0))
                    {
                        _associatedCourse = Course.GetCourse(_assocCourseID, _assocVersionID);
                    }
                }
                return _associatedCourse;
            }
        }
        #endregion

        #region StartACU
        public int StartACUID
        {
            get { return _startACUID; }
            set { _startACUID = value; }
        }
        private SqlParameter StartACUIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STARTACADEMICCALENDERID_PA;
                if (StartACUID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = StartACUID;
                }
                return sqlParam;
            }
        }
        public AcademicCalender StartACU
        {
            get
            {
                if (_startACU == null)
                {
                    if (StartACUID > 0)
                    {
                        _startACU = AcademicCalender.Get(StartACUID);
                    }
                }
                return _startACU;
            }
        }
        #endregion

        #region Owner Program
        public int OwnerProgID
        {
            get { return _ownerProgID; }
            set { _ownerProgID = value; }
        }
        private SqlParameter OwnerProgramIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PROGRAMID_PA;
                if (OwnerProgID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = OwnerProgID;
                }
                return sqlParam;
            }
        }
        public Program OwnerProgram
        {
            get
            {
                if (_ownerProgram == null)
                {
                    if (this.OwnerProgID > 0)
                    {
                        _ownerProgram = Program.GetProgram(this.OwnerProgID);
                    }
                }
                return _ownerProgram;
            }
        }
        #endregion

        #region Course Content
        public string CourseContent
        {
            get { return _courseContent; }
            set { _courseContent = value; }
        }
        private SqlParameter CourseContentParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = COURSECONTENT_PA;
                if (CourseContent.Trim().Length == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = CourseContent;
                }
                return sqlParam;
            }
        }
        #endregion

        #region Is Credit
        public bool IsCredit
        {
            get { return _isCredit; }
            set { _isCredit = value; }
        }
        private SqlParameter IsCreditParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISCREDITCOURSE_PA;

                sqlParam.Value = IsCredit;
                return sqlParam;
            }
        }
        #endregion

        #region Credits
        /// <summary>
        /// Credits
        /// </summary>
        public decimal Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        private SqlParameter CreditsParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CREDITS_PA;

                sqlParam.Value = _credits;

                return sqlParam;
            }
        }
        #endregion

        #region Is Section Mandatory
        #region IsSectionMandatory
        public bool IsSectionMandatory
        {
            get { return _isSectionMandatory; }
            set { _isSectionMandatory = value; }
        }
        private SqlParameter IsSectionMandatoryParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISSECTIONMANDATORY_PA;

                sqlParam.Value = IsSectionMandatory;
                return sqlParam;
            }
        }
        #endregion
        #endregion

        #region Has Equivalents
        public bool HasEquivalents
        {
            get { return _hasEquivalents; }
            set { _hasEquivalents = value; }
        }
        private SqlParameter HasEquivalentsParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = HASEQUIVALENTS_PA;

                sqlParam.Value = HasEquivalents;
                return sqlParam;
            }
        }
        #endregion

        #region Has Multiple ACU Span
        public bool HasMultipleACUSpan
        {
            get { return _hasMultipleACUSpan; }
            set { _hasMultipleACUSpan = value; }
        }
        private SqlParameter HasMultipleACUSpanParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = HASMULTIPLEACUSPAN_PA;

                sqlParam.Value = HasMultipleACUSpan;
                return sqlParam;
            }
        }
        #endregion

        #region Is Active
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private SqlParameter IsActiveParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ISACTIVE_PA;

                sqlParam.Value = IsActive;
                return sqlParam;
            }
        }
        #endregion


        #region Collection of actual courses that are equivalents
        public List<Course> EquiCourse
        {
            get
            {
                if (_equiCourse == null)
                {
                    if ((Id > 0) && (VersionID > 0))
                    {
                        _equiCourse = EquivalentCourse.GetEquiCourse(Id, VersionID);
                    }
                }
                return _equiCourse;
            }
            set
            {
                _equiCourse = value;
            }
        }
        #endregion

        #region Collection of equicourse objects that contians link to actual courses
        public List<EquivalentCourse> Equivalents
        {
            get
            {
                if (_equivalents == null)
                {
                    if ((Id > 0) && (VersionID > 0))
                    {
                        _equivalents = EquivalentCourse.GetEquiCourses(Id, VersionID);
                    }
                }
                return _equivalents;
            }
            set { _equivalents = value; }
        }
        #endregion

        #region Course ACU Span
        public int CourseACUSpanMasID
        {
            get { return _courseACUSpanMasID; }
        }
        public CourseACUSpanMas CourseACUSpanInfo
        {
            get
            {
                if (_courseACUSpanMas == null)
                {
                    if ((Id > 0) && (VersionID > 0))
                    {
                        _courseACUSpanMas = CourseACUSpanMas.GetByCourse(Id, VersionID);
                        if (_courseACUSpanMas != null)
                        {
                            _courseACUSpanMasID = _courseACUSpanMas.Id;
                        }
                    }
                }
                return _courseACUSpanMas;
            }
            set
            {
                _courseACUSpanMas = value;
                if (_courseACUSpanMas != null)
                {
                    _courseACUSpanMasID = _courseACUSpanMas.Id;
                }
            }
        }
        #endregion

        public int TypeDefinitionID
        {
            get { return _typeDefinitionID; }
            set { _typeDefinitionID = value; }
        }
        private SqlParameter TypeDefinitionIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TYPEDEFINITIONID_PA;

                sqlParam.Value = TypeDefinitionID;
                return sqlParam;
            }
        }

        /// <summary>
        /// Combined formal code and full code
        /// </summary>
        public string FullCode
        {
            get { return _formalCode + "-" + _versionCode; }
            set
            {
                string tkened = value;
                string[] x = tkened.Split(new char[] { '-' });
                _formalCode = x[0];
                _versionCode = x[1];
            }
        }
        public string FullCodeAndCourse
        {
            get
            {

                string programCode = OwnerProgram == null ? " X " : OwnerProgram.ShortName;
                return _formalCode + "-" + _versionCode + "-" + _title + "-" + _credits.ToString() + "-" + programCode;
            }
            set
            {
                string tkened = value;
                string[] x = tkened.Split(new char[] { '-' });
                _formalCode = x[0];
                _versionCode = x[1];
                _title = x[2];
                _credits = Convert.ToDecimal(x[3]);
            }
        }

        public int CourseGroupId
        {
            get { return _courseGroupId; }
            set { _courseGroupId = value; }
        }
        private SqlParameter CourseGroupIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = COURSEGROUPID_PA;
                if (CourseGroupId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = CourseGroupId;
                }
                return sqlParam;
            }
        }


        #endregion

        #region Functions
        #region Private Functions
        /// <summary>
        /// Map a null handled data reader to a course object
        /// </summary>
        /// <param name="nullHandler">It is a null handled data reader</param>
        /// <returns>Course</returns>
        private static Course courseMapper(SQLNullHandler nullHandler)
        {
            Course course = new Course();
            course.Id = nullHandler.GetInt32(COURSEID);
            course.VersionID = nullHandler.GetInt32(VERSIONID);
            course.FormalCode = nullHandler.GetString(FORMALCODE);
            course.VersionCode = nullHandler.GetString(VERSIONCODE);
            course.Title = nullHandler.GetString(TITLE);
            course.AssocCourseID = nullHandler.GetInt32(ASSOCCOURSEID);
            course.AssocVersionID = nullHandler.GetInt32(ASSOCVERSIONID);
            course.StartACUID = nullHandler.GetInt32(STARTACADEMICCALENDERID);
            course.OwnerProgID = nullHandler.GetInt32(PROGRAMID);
            course.CourseContent = nullHandler.GetString(COURSECONTENT);
            course.IsCredit = nullHandler.GetBoolean(ISCREDITCOURSE);
            course.Credits = nullHandler.GetDecimal(CREDITS);
            course.IsSectionMandatory = nullHandler.GetBoolean(ISSECTIONMANDATORY);
            course.HasEquivalents = nullHandler.GetBoolean(HASEQUIVALENTS);
            course.HasMultipleACUSpan = nullHandler.GetBoolean(HASMULTIPLEACUSPAN);
            course.IsActive = nullHandler.GetBoolean(ISACTIVE);
            course.TypeDefinitionID = nullHandler.GetInt32(TYPEDEFINITIONID);
            course.CourseGroupId = nullHandler.GetInt32(COURSEGROUPID);
            course.CreatorID = nullHandler.GetInt32(CREATORID);
            course.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            course.ModifierID = nullHandler.GetInt32(MODIFIERID);
            course.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);
            return course;
        }

        /// <summary>
        /// Link a datareader containing a collection of courses to a nullhandler and then call the mapper
        /// </summary>
        /// <param name="theReader">Raw datareader</param>
        /// <returns>Collection of course</returns>
        private static List<Course> mapCourses(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Course> courses = null;

            while (theReader.Read())
            {
                if (courses == null)
                {
                    courses = new List<Course>();
                }
                Course course = courseMapper(nullHandler);
                courses.Add(course);
            }
            return courses;
        }

        /// <summary>
        /// Link a datareader containing a course to a nullhandler and then call the mapper
        /// </summary>
        /// <param name="theReader">Raw datareader</param>
        /// <returns>Course</returns>
        private static Course mapCourse(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Course course = null;
            if (theReader.Read())
            {
                course = courseMapper(nullHandler);
            }
            return course;
        }
        #endregion

        /// <summary>
        /// Check for duplicate formal code and version code combination
        /// </summary>
        /// <param name="course">Course to be validated</param>
        /// <param name="oldCourseCode">In case of edit old code and in case of add empty string</param>
        /// <returns>True if duplicate exists otherwise false</returns>
        public static bool HasDuplicateCode(Course course, string oldCourseCode)
        {
            if (course == null)
            {
                return Course.IsExist(course.FormalCode);
            }
            else
            {
                if (course.Id == 0)
                {
                    if (Course.IsExist(course.FormalCode))
                    {
                        return Course.IsExist(course.FormalCode, course.VersionCode);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (course.FormalCode != oldCourseCode)
                    {
                        if (Course.IsExist(course.FormalCode))
                        {
                            return Course.IsExist(course.FormalCode, course.VersionCode);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Find course using an inclusive search of FormalCode, VersionCode, Title
        /// </summary>
        /// <param name="parameter"> Can be a part of formal code and/or versioncode and/or title</param>
        /// <returns>Collection of courses</returns>
        public static List<Course> GetCourses(string parameter)
        {
            List<Course> courses = null;

            string command = SELECT
                            + "WHERE [FormalCode] Like '%" + parameter + "%' OR [VersionCode] LIKE '%" + parameter + "%' OR [Title] LIKE '%" + parameter + "%' ORDER BY FormalCode,VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        /// <summary>
        /// Find course using an inclusive search of FormalCode
        /// </summary>
        /// <param name="parameters">Must be a part of formal code</param>
        /// <returns>Collection of courses</returns>
        public static List<Course> GetCoursesByCode(string parameters)
        {
            List<Course> courses = null;

            string command = SELECT
                            + "WHERE FormalCode IN ('" + parameters + "') ORDER BY FormalCode,VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        /// <summary>
        /// Gets all the courses
        /// </summary>
        /// <returns>COllection of courses</returns>
        public static List<Course> GetCourses()
        {
            List<Course> courses = null;

            string command = SELECT + " ORDER BY FormalCode,VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        public static List<Course> GetCoursesByProgram(int program)
        {
            List<Course> courses = null;

            string command = SELECT + " WHERE ProgramID = " + program + " ORDER BY FormalCode,VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        /// <summary>
        /// Gets all mother courses
        /// </summary>
        /// <returns>COllection of courses</returns>
        public static List<Course> GetActiveMotherCourses()
        {
            List<Course> courses = null;

            string command = SELECT + "WHERE VersionCode IS NULL AND [IsActive]=1 ORDER BY FormalCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        /// <summary>
        /// Gets all the active courses
        /// </summary>
        /// <returns>Collection of courses</returns>
        public static List<Course> GetActiveCourses()
        {
            List<Course> courses = null;

            string command = SELECT
                                + "WHERE [IsActive]=1 ORDER BY FormalCode,VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formalCode"></param>
        /// <param name="verCode"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static List<Course> GetCourses(string formalCode, string verCode, string title)
        {
            List<Course> courses = null;

            StringBuilder command = new StringBuilder(SELECT + "WHERE [IsActive]=1");

            if (formalCode.Trim() != string.Empty)
            {
                command.Append(" AND [FormalCode] Like '%" + formalCode + "%'");
            }
            if (verCode.Trim() != string.Empty)
            {
                command.Append(" AND [VersionCode] Like '%" + verCode + "%'");
            }
            if (title.Trim() != string.Empty)
            {
                command.Append(" AND [Title] Like '%" + title + "%'");
            }

            command.Append(" ORDER BY FormalCode,VersionCode");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        public static Course GetCourses(string formalCode, string verCode)
        {
            Course courses = null;

            StringBuilder command = new StringBuilder(SELECT + "WHERE [IsActive]=1");


            command.Append(" AND [FormalCode] = '" + formalCode + "'");
            if (verCode.Trim() != string.Empty)
            {
                command.Append(" AND [VersionCode] = '" + verCode + "'");
            }
            else
            {
                command.Append(" AND [VersionCode] is null");
            }

            command.Append(" ORDER BY FormalCode,VersionCode");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            courses = mapCourse(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }


        public static List<Course> GetCourses(int teacher, int acaCal, int program)
        {
            List<Course> courses = null;

            string command = "SELECT c.* FROM dbo.Course AS c INNER JOIN " +
            "(SELECT s1.ProgramID, s1.TeacherOneID,s1.AcademicCalenderID, s1.CourseID, s1.VersionID " +
            "FROM AcademicCalenderSection AS s1 " +
            "WHERE s1.TeacherOneID = " + teacher + " AND s1.AcademicCalenderID = " + acaCal + " " +
            "UNION " +
            "SELECT s2.ProgramID,s2.TeacherTwoID,s2.AcademicCalenderID, s2.CourseID, s2.VersionID " +
            "FROM AcademicCalenderSection  AS s2 " +
            "WHERE s2.TeacherTwoID = " + teacher + " AND s2.AcademicCalenderID = " + acaCal + ") AS sec " +
            "ON sec.CourseID =c.CourseID AND sec.VersionID = c.VersionID " +
            "WHERE sec.ProgramID = " + program + " and c.IsActive=1 " +
            "ORDER BY FormalCode,VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }


        /// <summary>
        /// get course by version code
        /// </summary>
        /// <param name="verCode"></param>
        /// <returns></returns>
        public static Course GetCourseByVerCode(string verCode)
        {
            Course course = new Course();

            string command = SELECT + " WHERE VersionCode = '" + verCode + "'";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            course = mapCourse(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return course;
        }
        public static List<Course> GetAllCourses(string formalCode, string verCode)
        {
            List<Course> courses = null;

            StringBuilder command = new StringBuilder(SELECT + "WHERE");

            if (formalCode.Trim() != string.Empty)
            {
                command.Append(" [FormalCode] = '" + formalCode + "'");
                if (verCode.Trim() != string.Empty)
                {
                    command.Append(" AND [VersionCode] = '" + verCode + "'");
                }
                else
                {
                    command.Append(" AND [VersionCode] is null");
                }
            }
            else if (verCode.Trim() != string.Empty)
            {
                command.Append(" [VersionCode] = '" + verCode + "'");
            }

            command.Append(" ORDER BY FormalCode,VersionCode");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }
        public static List<Course> GetCoursesByVCOde(string verCode)
        {
            List<Course> courses = null;

            StringBuilder command = new StringBuilder(SELECT + "WHERE");


            command.Append(" [VersionCode] LIKE '%" + verCode + "%'");


            command.Append(" ORDER BY FormalCode,VersionCode");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        public static Course GetCourse(int courseID, int versionID)
        {
            Course course = null;

            string command = SELECT
                            + "WHERE CourseID = " + ID_PA + " AND VersionID = " + VERSIONID_PA;

            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, ID_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSIONID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { courseIDParam, versionIDParam });

            course = mapCourses(theReader)[0];
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return course;
        }
        public static Course GetActiveCourse(int courseID, int versionID)
        {
            Course course = null;

            string command = SELECT
                            + "WHERE IsActive = 1 AND CourseID = " + ID_PA + " AND VersionID = " + VERSIONID_PA;

            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, ID_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSIONID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { courseIDParam, versionIDParam });

            course = mapCourse(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return course;
        }
        internal static Course GetCourse(int courseID, int versionID, SqlConnection sqlConn)
        {
            Course course = null;

            string command = SELECT
                            + "WHERE CourseID = " + ID_PA + " AND VersionID = " + VERSIONID_PA;

            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, ID_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSIONID_PA);

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { courseIDParam, versionIDParam });

            course = mapCourse(theReader);
            theReader.Close();
            //SqlConnectionHandler.CloseDbConnection();
            return course;
        }

        internal static int GetMaxCourseID(SqlConnection sqlConn)
        {
            int newCourseID = 0;

            string command = "SELECT MAX(CourseID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            if (ob == null || ob == DBNull.Value)
            {
                newCourseID = 1;
            }
            else if (ob is Int32)
            {
                newCourseID = Convert.ToInt32(ob) + 1;
            }

            return newCourseID;
        }
        internal static int GetMaxCourseID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newCourseID = 0;

            string command = "SELECT MAX(CourseID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newCourseID = 1;
            }
            else if (ob is Int32)
            {
                newCourseID = Convert.ToInt32(ob) + 1;
            }

            return newCourseID;
        }

        internal static int GetMaxVersionID(SqlConnection sqlConn, int courseID)
        {
            int newCourseID = 0;

            string command = "SELECT MAX(VersionID) FROM" + TABLENAME
                                + "WHERE CourseID = " + ID_PA;
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, ID_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { courseIDParam });

            if (ob == null || ob == DBNull.Value)
            {
                newCourseID = 1;
            }
            else if (ob is Int32)
            {
                newCourseID = Convert.ToInt32(ob) + 1;
            }

            return newCourseID;
        }
        internal static int GetMaxVersionID(SqlConnection sqlConn, SqlTransaction sqlTran, int courseID)
        {
            int newCourseID = 0;

            string command = "SELECT MAX(VersionID) FROM" + TABLENAME
                                + "WHERE CourseID = " + ID_PA;
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, ID_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { courseIDParam });

            if (ob == null || ob == DBNull.Value)
            {
                newCourseID = 1;
            }
            else if (ob is Int32)
            {
                newCourseID = Convert.ToInt32(ob) + 1;
            }

            return newCourseID;
        }


        public static int ImportCourses(DataSet courses)
        {
            int counter = 0;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            foreach (DataRow drow in courses.Tables["Courses"].Rows)
            {
                int courseID = Course.GetMaxCourseID(sqlConn);
                int versionID = Course.GetMaxVersionID(sqlConn, courseID);

                string command = INSERT + "("
                    + courseID.ToString() + " "
                    + versionID.ToString() + " '"
                    + drow["FormalCode"].ToString() + "','"
                    + drow["VersionCode"].ToString() + "',"
                    + drow["Title"].ToString() + "',"
                    + drow["Credits"].ToString() + ")";
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran);
            }

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();
            return counter;
        }
        public static DataSet ExportCourses(List<Course> courses)
        {
            DataSet dsCourses = new DataSet();

            DataTable dtCourses = new DataTable();
            dtCourses.Columns.Add("FormalCode", typeof(string));
            dtCourses.Columns.Add("VersionCode", typeof(string));
            dtCourses.Columns.Add("Title", typeof(string));
            dtCourses.Columns.Add("Credits", typeof(decimal));

            foreach (Course course in courses)
            {
                DataRow drCourse = dtCourses.NewRow();
                drCourse["FormalCode"] = course.FormalCode;
                drCourse["VersionCode"] = course.Title;
                drCourse["Title"] = course.Title;
                drCourse["Credits"] = course.Credits;

                dtCourses.Rows.Add(drCourse);
            }

            dtCourses.TableName = "Courses";
            dsCourses.Tables.Add(dtCourses);

            return dsCourses;
        }

        public static int SaveCourse(Course course)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;                

                if (course.Id == 0)
                {
                    course.Id = Course.GetMaxCourseID(sqlConn, sqlTran);
                    course.VersionID = Course.GetMaxVersionID(sqlConn, sqlTran, course.Id);
                    command = INSERT;
                    
                    sqlParams = new SqlParameter[] { course.IDParam, 
                                                 course.VersionIDParam, 
                                                 course.FormalCodeParam, 
                                                 course.VersionCodeParam, 
                                                 course.TitleParam, 
                                                 course.AssocCourseIDParam,
                                                 course.AssocVersionIDParam,
                                                 course.StartACUIDParam,
                                                 course.OwnerProgramIDParam,
                                                 course.CourseContentParam,
                                                 course.IsCreditParam,
                                                 course.CreditsParam,
                                                 course.IsSectionMandatoryParam,
                                                 course.HasEquivalentsParam,
                                                 course.HasMultipleACUSpanParam,
                                                 course.IsActiveParam,
                                                 course.TypeDefinitionIDParam,
                                                 course.CourseGroupIdParam,
                                                 course.CreatorIDParam,
                                                 course.CreatedDateParam,
                                                 course.ModifierIDParam,
                                                 course.ModifiedDateParam};
                }
                else
                {                   
                    command = UPDATE
                               + " WHERE CourseID = " + ID_PA + " AND VersionID = " + VERSIONID_PA;
                    sqlParams = new SqlParameter[] { course.FormalCodeParam, 
                                                 course.VersionCodeParam, 
                                                 course.TitleParam, 
                                                 course.AssocCourseIDParam,
                                                 course.AssocVersionIDParam,
                                                 course.StartACUIDParam,
                                                 course.OwnerProgramIDParam,
                                                 course.CourseContentParam,
                                                 course.IsCreditParam,
                                                 course.CreditsParam,
                                                 course.IsSectionMandatoryParam,
                                                 course.HasEquivalentsParam,
                                                 course.HasMultipleACUSpanParam,
                                                 course.IsActiveParam,
                                                 course.TypeDefinitionIDParam,
                                                 course.CourseGroupIdParam,
                                                 course.CreatorIDParam,
                                                 course.CreatedDateParam,
                                                 course.ModifierIDParam,
                                                 course.ModifiedDateParam,
                                                 course.IDParam, 
                                                 course.VersionIDParam};
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                EquivalentCourse.Delete(course.Id, course.VersionID, sqlConn, sqlTran);
                if (course.HasEquivalents)
                {
                    if (course.Equivalents != null)
                    {
                        if (course.Equivalents.Count > 0)
                        {
                            SaveEquiCourse(course.Equivalents, course.Id, course.VersionID, sqlConn, sqlTran);
                        }
                    }
                }

                CourseACUSpanMas.Delete(course.Id, course.VersionID, sqlConn, sqlTran);
                if (course.HasMultipleACUSpan)
                {
                    if (course.CourseACUSpanInfo != null)
                    {
                        SaveACUSpan(course.CourseACUSpanInfo, course.Id, course.VersionID, sqlConn, sqlTran);
                    }
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
        
        internal static void SaveEquiCourse(List<EquivalentCourse> listEquiCourses, int courseID, int versionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            foreach (EquivalentCourse ec in listEquiCourses)
            {
                ec.ParentCourseID = courseID;
                ec.ParentVersionID = versionID;
            }
            EquivalentCourse.Delete(courseID, versionID, sqlConn, sqlTran);
            EquivalentCourse.Save(listEquiCourses, sqlConn, sqlTran);
        }

        internal static void SaveACUSpan(CourseACUSpanMas courseACUMas, int courseID, int versionID, SqlConnection sqlConn, SqlTransaction sqlTran)
        {

            courseACUMas.CourseID = courseID;
            courseACUMas.VersionID = versionID;
                         
            CourseACUSpanMas.Save(courseACUMas, sqlConn, sqlTran);
        }

        public static int SaveCourses(List<Course> courses)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                foreach (Course course in courses)
                {
                    string command = string.Empty;
                    SqlParameter[] sqlParams = null;

                    if (course.Id == 0)
                    {
                        course.Id = Course.GetMaxCourseID(sqlConn, sqlTran);
                        course.VersionID = Course.GetMaxVersionID(sqlConn, sqlTran, course.Id);
                        command = INSERT;
                        sqlParams = new SqlParameter[] { course.IDParam, 
                                                 course.VersionIDParam, 
                                                 course.FormalCodeParam, 
                                                 course.VersionCodeParam, 
                                                 course.TitleParam, 
                                                 course.AssocCourseIDParam,
                                                 course.AssocVersionIDParam,
                                                 course.StartACUIDParam,
                                                 course.OwnerProgramIDParam,
                                                 course.CourseContentParam,
                                                 course.IsCreditParam,
                                                 course.CreditsParam,
                                                 course.IsSectionMandatoryParam,
                                                 course.HasEquivalentsParam,
                                                 course.HasMultipleACUSpanParam,
                                                 course.IsActiveParam,
                                                 course.TypeDefinitionIDParam,
                                                 course.CourseGroupIdParam,
                                                 course.CreatorIDParam,
                                                 course.CreatedDateParam,
                                                 course.ModifierIDParam,
                                                 course.ModifiedDateParam};
                    }
                    else
                    {
                        command = UPDATE
                                   + " WHERE CourseID = " + ID_PA + " AND VersionID = " + VERSIONID_PA;
                        sqlParams = new SqlParameter[] { course.FormalCodeParam, 
                                                 course.VersionCodeParam, 
                                                 course.TitleParam, 
                                                 course.AssocCourseIDParam,
                                                 course.AssocVersionIDParam,
                                                 course.StartACUIDParam,
                                                 course.OwnerProgramIDParam,
                                                 course.CourseContentParam,
                                                 course.IsCreditParam,
                                                 course.CreditsParam,
                                                 course.IsSectionMandatoryParam,
                                                 course.HasEquivalentsParam,
                                                 course.HasMultipleACUSpanParam,
                                                 course.IsActiveParam,
                                                 course.TypeDefinitionIDParam,
                                                 course.CourseGroupIdParam,
                                                 course.CreatorIDParam,
                                                 course.CreatedDateParam,
                                                 course.ModifierIDParam,
                                                 course.ModifiedDateParam,
                                                 course.IDParam, 
                                                 course.VersionIDParam };
                    }
                    counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);
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

        internal static int UpdateEuivalentMarker(SqlConnection sqlConn, SqlTransaction sqlTran, bool hasEquivalents, int courseID, int versionID)
        {
            try
            {
                string command = "UPDATE " + TABLENAME + "SET [" + HASEQUIVALENTS + "] = " + HASEQUIVALENTS_PA
                        + "WHERE [" + COURSEID + "] = " + ID_PA + " AND [" + VERSIONID + "] = " + VERSIONID_PA;
                SqlParameter hasEquiParam = MSSqlConnectionHandler.MSSqlParamGenerator(hasEquivalents, HASEQUIVALENTS_PA);
                SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, ID_PA);
                SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSIONID_PA);
                return DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { hasEquiParam, courseIDParam, versionIDParam });
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }

        }

        public static int DeleteCourse(int courseID, int versionID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                CourseACUSpanMas.Delete(courseID, versionID, sqlConn, sqlTran);
                EquivalentCourse.Delete(courseID, versionID, sqlConn, sqlTran);

                string command = DELETE
                                + "WHERE CourseID = " + ID_PA + " AND VersionID = " + VERSIONID_PA;

                SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, ID_PA);
                SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSIONID_PA);

                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { courseIDParam, versionIDParam });

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

        public static bool IsExist(string formalCode)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE FormalCode = @FormalCode";
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(formalCode, "@FormalCode");
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { courseIDParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool IsExist(string formalCode, string versionCode)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE FormalCode = " + FORMALCODE_PA + " AND VersionCode = " + VERSIONCODE_PA;
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(formalCode, FORMALCODE_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionCode, VERSIONCODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { courseIDParam, versionIDParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        internal static bool IsExist(string formalCode, SqlConnection sqlConn)
        {
            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE FormalCode = " + FORMALCODE_PA;
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(formalCode, FORMALCODE_PA);
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { courseIDParam });

            return (Convert.ToInt32(ob) > 0);
        }
        public static SortedList GetCoursesByDeptProgTree(int deptID, int progID, int rootNodeID)
        {
            List<TreeMaster> treeMases = new List<TreeMaster>();
            SortedList hs = new SortedList();

            try
            {
                if (deptID == 0 && rootNodeID == 0)
                {
                    treeMases = TreeMaster.GetByProgram(progID);
                }
                else if (deptID > 0 && rootNodeID == 0)
                {
                    List<Program> programs = null;
                    if (progID == 0)
                    {
                        programs = Program.GetProgramsByDeptID(deptID.ToString());
                        if (programs == null)
                        {
                            return hs;
                        }
                        foreach (Program p in programs)
                        {
                            List<TreeMaster> treeMasesbyProg = TreeMaster.GetByProgram(p.Id);
                            if (treeMasesbyProg != null)
                            {
                                foreach (TreeMaster tm in treeMasesbyProg)
                                {
                                    treeMases.Add(tm);
                                }
                            }
                        }
                    }
                    else
                    {
                        treeMases = TreeMaster.GetByProgram(progID);
                    }
                }
                else
                {
                    TreeMaster mas = TreeMaster.GetByProgram(progID, rootNodeID);
                    treeMases.Add(mas);
                }
                if (treeMases != null)
                {
                    foreach (TreeMaster tmt in treeMases)
                    {
                        List<NodeCourse> nodeCouses = NodeCourse.GetNodeCoursesByRoot(tmt.Id);
                        foreach (NodeCourse nc in nodeCouses)
                        {
                            if (nc.ChildActiveCourse != null)
                            {
                                if (hs.Contains(nc.ChildActiveCourse.FullCode) == false)
                                {
                                    hs.Add(nc.ChildActiveCourse.FullCode, nc.ChildActiveCourse);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return hs;
        }
        #endregion

        public static List<Course> GetCoursesByStudentId(int studentId)
        {
            List<Course> courses = null;

            StringBuilder command = new StringBuilder("select c.* from StudentCourseHistory as sch inner join Course as c on sch.CourseID = c.CourseID and sch.VersionID = c.VersionID");

            if (studentId > 0)
            {
                command.Append(" where sch.StudentID = " + studentId);
            }

            command.Append(" ORDER BY c.FormalCode, c.VersionCode");

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }

        public static List<Course> GetCoursesByTeacher(int teacher, int acaCal)
        {
            List<Course> courses = null;

            string command = "SELECT c.* FROM dbo.Course AS c INNER JOIN " +
            "(SELECT s1.AcaCal_SectionID, s1.SectionName, s1.ProgramID, s1.TeacherOneID,s1.AcademicCalenderID, s1.CourseID, s1.VersionID " +
            "FROM AcademicCalenderSection AS s1 " +
            "WHERE s1.TeacherOneID = " + teacher + " AND s1.AcademicCalenderID = " + acaCal + " " +
            "UNION " +
            "SELECT s2.AcaCal_SectionID, s2.SectionName, s2.ProgramID,s2.TeacherTwoID,s2.AcademicCalenderID, s2.CourseID, s2.VersionID " +
            "FROM AcademicCalenderSection  AS s2 " +
            "WHERE s2.TeacherTwoID = " + teacher + " AND s2.AcademicCalenderID = " + acaCal + ") AS sec " +
            "ON sec.CourseID =c.CourseID AND sec.VersionID = c.VersionID " +
            "WHERE c.IsActive = 1 " +
            "ORDER BY FormalCode, VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            courses = mapCourses(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();

            return courses;
        }
    }
}
