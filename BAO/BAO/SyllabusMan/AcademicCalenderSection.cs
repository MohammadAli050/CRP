using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using DataAccess;
using Common;

namespace BussinessObject
{
    [Serializable]
    public class AcademicCalenderSection : Base
    {
        #region DBColumns
        //AcaCal_SectionID	    int	        Unchecked
        //AcademicCalenderID	int	        Unchecked
        //CourseID	            int	        Unchecked
        //VersionID	            int	        Checked
        //SectionName	        varchar(20)	Unchecked
        //Capacity	            int	        Unchecked
        //RoomInfoOneID	        int	        Unchecked
        //RoomInfoTwoID	        int	        Checked
        //DayOne	            int	        Unchecked
        //DayTwo	            int	        Checked
        //TimeSlotPlanOneID	    int	        Unchecked
        //TimeSlotPlanTwoID	    int	        Checked
        //TeacherOneID	        int	        Unchecked
        //TeacherTwoID	        int	        Checked
        //DeptID	            int	        Unchecked
        //ProgramID             int         checked
        //ShareDptIDOne	        int	        Checked
        //ShareDptIDTwo	        int	        Checked
        //CreatedBy	            int	        Unchecked
        //CreatedDate	        datetime	Unchecked
        //ModifiedBy	        int	        Checked
        //ModifiedDate	        datetime	Checked

        //TypeDefinitionID      int
        //Occupied              int
        #endregion

        #region Variables
        private int _academicCalenderID;
        private int _lastacademicCalenderID;
        private AcademicCalender _linkAcademicCalender = null;
        private int _childCourseID;
        private int _childVersionID;
        private Course _childCourse = null;
        private int _lastchildCourseID;
        private int _lastchildVersionID;
        private List<Course> _equiCourse = null;

        private string _sectionName;
        private string _lastSectionName;
        private int _capacity;

        private int _roomInfoOneID;
        private RoomInfo _roomInfoOne = null;
        private int _roomInfoTwoID;
        private RoomInfo _roomInfoTwo = null;

        private int _timeSlotPlanOneID;
        private TimeSlotPlan _timeSlotPlanOne = null;
        private int _timeSlotPlanTwoID;
        private TimeSlotPlan _timeSlotPlanTwo = null;

        private int _dayOneValue;
        private FriendlyWeekDays _dayOne;
        private int _dayTwoValue;
        private FriendlyWeekDays _dayTwo;

        private int _teacherIDOne;
        private Teacher _assignedTeacherOne = null;
        private int _teacherIDTwo;
        private Teacher _assignedTeacherTwo = null;

        private int _deptID;
        private int _lastdeptID;
        private Department _ownerDepartment = null;

        private int _programID;
        private int _lastProgramID;

        private int _shareDptIDOne;
        private Department _shareDptOne = null;

        private int _shareDptIDTwo;
        private Department _shareDptTwo = null;

        private int _typeDefinitionID;
        private TypeDefinition _typeDefinition = null;

        private int _occupied;

        #endregion

        #region Construcotr
        public AcademicCalenderSection()
            : base()
        {
            _academicCalenderID = 0;
            _linkAcademicCalender = null;
            _childCourseID = 0;
            _childVersionID = 0;
            _childCourse = null;
            _sectionName = string.Empty;

            _dayOneValue = 0;
            _dayOne = null;
            _dayTwoValue = 0;
            _dayTwo = null;

            _roomInfoOneID = 0;
            _roomInfoOne = null;
            _roomInfoTwoID = 0;
            _roomInfoTwo = null;

            _timeSlotPlanOneID = 0;
            _timeSlotPlanOne = null;
            _timeSlotPlanTwoID = 0;
            _timeSlotPlanTwo = null;

            _teacherIDOne = 0;
            _assignedTeacherOne = null;
            _teacherIDTwo = 0;
            _assignedTeacherTwo = null;

            _deptID = 0;
            _ownerDepartment = null;

            _shareDptIDOne = 0;
            _shareDptOne = null;

            _typeDefinitionID = 0;
            _typeDefinition = null;

            _occupied = 0;
        }
        #endregion

        #region Constants
        #region Column Constants
        private const string ACACAL_SECTIONID = "AcaCal_SectionID";//0

        private const string ACADEMIC_CALENDER_ID = "AcademicCalenderID";
        private const string ACADEMIC_CALENDER_ID_PA = "@AcademicCalenderID";

        private const string COURSE_ID = "CourseID";
        private const string COURSE_ID_PA = "@CourseID";

        private const string VERSION_ID = "VersionID";
        private const string VERSION_ID_PA = "@VersionID";

        private const string SECTIONNAME = "SectionName";//2
        private const string SECTIONNAME_PA = "@SectionName";//2

        private const string CAPACITY = "Capacity";//2
        private const string CAPACITY_PA = "@Capacity";//2

        private const string ROOMINFOIDONE = "RoomInfoOneID";//3
        private const string ROOMINFOIDONE_PA = "@RoomInfoOneID";//3
        private const string ROOMINFOIDTWO = "RoomInfoTwoID";//4
        private const string ROOMINFOIDTWO_PA = "@RoomInfoTwoID";//4

        private const string TIMESLOTPLANIDONE = "TimeSlotPlanOneID";//5
        private const string TIMESLOTPLANIDONE_PA = "@TimeSlotPlanOneID";//5
        private const string TIMESLOTPLANIDTWO = "TimeSlotPlanTwoID";//6
        private const string TIMESLOTPLANIDTWO_PA = "@TimeSlotPlanTwoID";//6

        private const string DAYONE = "DayOne";//7
        private const string DAYONE_PA = "@DayOne";//7
        private const string DAYTWO = "DayTwo";//8
        private const string DAYTWO_PA = "@DayTwo";//8

        private const string TEACHERIDONE = "TeacherOneID";//9
        private const string TEACHERIDONE_PA = "@TeacherOneID";//9
        private const string TEACHERIDTWO = "TeacherTwoID";//10
        private const string TEACHERIDTWO_PA = "@TeacherTwoID";//10

        private const string DEPTID = "DeptID";//11
        private const string DEPTID_PA = "@DeptID";//11

        private const string PROGRAMID = "ProgramID";//11
        private const string PROGRAMID_PA = "@ProgramID";//11

        private const string SHAREDPTIDONE = "ShareDptIDOne";//11
        private const string SHAREDPTIDONE_PA = "@ShareDptIDOne";//11

        private const string SHAREDPTIDTWO = "ShareDptIDTwo";//11
        private const string SHAREDPTIDTWO_PA = "@ShareDptIDTwo";//11

        private const string TYPEDEFINITION = "TypeDefinitionID";
        private const string TYPEDEFINITION_PA = "@TypeDefinitionID";

        private const string OCCUPIED = "Occupied";
        private const string OCCUPIED_PA = "@Occupied";

        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + ACACAL_SECTIONID + "], "//0
                                        + "[" + ACADEMIC_CALENDER_ID + "], "
                                        + "[" + COURSE_ID + "], "
                                        + "[" + VERSION_ID + "], "
                                        + "[" + SECTIONNAME + "], "//2
                                        + "[" + CAPACITY + "], "//3
                                        + "[" + ROOMINFOIDONE + "], "//3
                                        + "[" + ROOMINFOIDTWO + "], "//4
                                        + "[" + TIMESLOTPLANIDONE + "], "//5
                                        + "[" + TIMESLOTPLANIDTWO + "], "//6
                                        + "[" + DAYONE + "], "//7
                                        + "[" + DAYTWO + "], "//8
                                        + "[" + TEACHERIDONE + "], "//9
                                        + "[" + TEACHERIDTWO + "], "//10
                                        + "[" + DEPTID + "], "
                                        + "[" + PROGRAMID + "], "
                                        + "[" + SHAREDPTIDONE + "], "
                                        + "[" + SHAREDPTIDTWO + "], "//11
                                        + "[" + TYPEDEFINITION + "], "
                                        + "[" + OCCUPIED + "], ";
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + ACADEMIC_CALENDER_ID + "], "
                                         + "[" + COURSE_ID + "], "
                                         + "[" + VERSION_ID + "], "
                                         + "[" + SECTIONNAME + "], "//2
                                         + "[" + CAPACITY + "], "//3
                                         + "[" + ROOMINFOIDONE + "], "//3
                                         + "[" + ROOMINFOIDTWO + "], "//4
                                         + "[" + TIMESLOTPLANIDONE + "], "//5
                                         + "[" + TIMESLOTPLANIDTWO + "], "//6
                                         + "[" + DAYONE + "], "//7
                                         + "[" + DAYTWO + "], "//8
                                         + "[" + TEACHERIDONE + "], "//9
                                         + "[" + TEACHERIDTWO + "], "//10
                                         + "[" + DEPTID + "], "
                                        + "[" + PROGRAMID + "], "
                                        + "[" + SHAREDPTIDONE + "], "
                                        + "[" + SHAREDPTIDTWO + "], "//11
                                        + "[" + TYPEDEFINITION + "], "
                                        + "[" + OCCUPIED + "], ";
        #endregion

        private const string TABLENAME = " [AcademicCalenderSection] ";

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
            //+ ID_PA + ", "//0
                     + ACADEMIC_CALENDER_ID_PA + ", "
                     + COURSE_ID_PA + ", "
                     + VERSION_ID_PA + ", "
                     + SECTIONNAME_PA + ", "//2
                     + CAPACITY_PA + ", "//3
                     + ROOMINFOIDONE_PA + ", "//3
                     + ROOMINFOIDTWO_PA + ", "//4
                     + TIMESLOTPLANIDONE_PA + ", "//5
                     + TIMESLOTPLANIDTWO_PA + ", "//6
                     + DAYONE_PA + ", "//7
                     + DAYTWO_PA + ", "//8
                     + TEACHERIDONE_PA + ", "//9
                     + TEACHERIDTWO_PA + ", "//10
                     + DEPTID_PA + ", "//11
                     + PROGRAMID_PA + ", "
                     + SHAREDPTIDONE_PA + ", "
                     + SHAREDPTIDTWO_PA + ", "
                     + TYPEDEFINITION_PA + ", "
                     + OCCUPIED_PA + ", "

                     + CREATORID_PA + ", "//12
                     + CREATEDDATE_PA + ", "//13
                     + MODIFIERID_PA + ", "//14
                     + MOIDFIEDDATE_PA + ")";//15 
        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA + ", "//1
                    + "[" + COURSE_ID + "] = " + COURSE_ID_PA + ", "//2
                    + "[" + VERSION_ID + "] = " + VERSION_ID_PA + ", "//3
                    + "[" + SECTIONNAME + "] = " + SECTIONNAME_PA + ", "//2
                    + "[" + CAPACITY + "] = " + CAPACITY_PA + ", "//3
                    + "[" + ROOMINFOIDONE + "] = " + ROOMINFOIDONE_PA + ", "//3
                    + "[" + ROOMINFOIDTWO + "] = " + ROOMINFOIDTWO_PA + ", "//4
                    + "[" + TIMESLOTPLANIDONE + "] = " + TIMESLOTPLANIDONE_PA + ", "//5
                    + "[" + TIMESLOTPLANIDTWO + "] = " + TIMESLOTPLANIDTWO_PA + ", "//6
                    + "[" + DAYONE + "] = " + DAYONE_PA + ", "//7
                    + "[" + DAYTWO + "] = " + DAYTWO_PA + ", "//8
                    + "[" + TEACHERIDONE + "] = " + TEACHERIDONE_PA + ", "//9
                    + "[" + TEACHERIDTWO + "] = " + TEACHERIDTWO_PA + ", "//10
                    + "[" + DEPTID + "] = " + DEPTID_PA + ", "//11
                    + "[" + PROGRAMID + "] = " + PROGRAMID_PA + ", "//11
                    + "[" + SHAREDPTIDONE + "] = " + SHAREDPTIDONE_PA + ", "
                    + "[" + SHAREDPTIDTWO + "] = " + SHAREDPTIDTWO_PA + ", "
                    + "[" + TYPEDEFINITION + "] = " + TYPEDEFINITION_PA + ", "

                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "//12
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//13
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "//14
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//15
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Properties

        #region Academic Calender
        public int AcademicCalenderID
        {
            get { return _academicCalenderID; }
            set { _academicCalenderID = value; }
        }
        private SqlParameter AcademicCalenderIDParam
        {
            get
            {
                SqlParameter academicCalenderIDParam = new SqlParameter();
                academicCalenderIDParam.ParameterName = ACADEMIC_CALENDER_ID_PA;
                if (AcademicCalenderID == 0)
                {
                    academicCalenderIDParam.Value = DBNull.Value;
                }
                else
                {
                    academicCalenderIDParam.Value = AcademicCalenderID;
                }
                return academicCalenderIDParam;
            }
        }
        public AcademicCalender LinkAcademicCalender
        {
            get
            {
                if (_linkAcademicCalender == null)
                {
                    if ((_academicCalenderID > 0))
                    {
                        _linkAcademicCalender = AcademicCalender.Get(_academicCalenderID);
                    }
                }
                return _linkAcademicCalender;
            }
        }
        public int LastacademicCalenderID
        {
            get { return _lastacademicCalenderID; }
            set { _lastacademicCalenderID = value; }
        }

        public string AcademicCalenderName
        {
            get
            {
                return LinkAcademicCalender.Name;
            }
        }
        #endregion

        #region Child Course
        public int ChildCourseID
        {
            get { return _childCourseID; }
            set { _childCourseID = value; }
        }
        private SqlParameter ChildCourseIDParam
        {
            get
            {
                SqlParameter oparandCourseIDParam = new SqlParameter();
                oparandCourseIDParam.ParameterName = COURSE_ID_PA;
                if (ChildCourseID == 0)
                {
                    oparandCourseIDParam.Value = DBNull.Value;
                }
                else
                {
                    oparandCourseIDParam.Value = ChildCourseID;
                }
                return oparandCourseIDParam;
            }
        }
        public int LastchildCourseID
        {
            get { return _lastchildCourseID; }
            set { _lastchildCourseID = value; }
        }
        public int ChildVersionID
        {
            get { return _childVersionID; }
            set { _childVersionID = value; }
        }
        private SqlParameter ChildVersionIDParam
        {
            get
            {
                SqlParameter oparandVersionIDParam = new SqlParameter();
                oparandVersionIDParam.ParameterName = VERSION_ID_PA;
                if (ChildVersionID == 0)
                {
                    oparandVersionIDParam.Value = DBNull.Value;
                }
                else
                {
                    oparandVersionIDParam.Value = ChildVersionID;
                }
                return oparandVersionIDParam;
            }
        }
        public int LastchildVersionID
        {
            get { return _lastchildVersionID; }
            set { _lastchildVersionID = value; }
        }
        public Course ChildCourse
        {
            get
            {
                if (_childCourse == null)
                {
                    if ((_childCourseID > 0) && (_childVersionID > 0))
                    {
                        _childCourse = Course.GetCourse(_childCourseID, _childVersionID);
                    }
                }
                return _childCourse;
            }
        }
        //public List<Course> EquiCourse
        public Course EquiCourse
        {
            get
            {
                if (_equiCourse == null)
                {
                    if ((_childCourseID > 0) && (_childVersionID > 0))
                    {
                        _equiCourse = EquivalentCourse.GetEquiCourse(_childCourseID, _childVersionID);
                    }
                }
                if (_equiCourse == null)
                {
                    return new Course();
                }
                else
                {
                    return _equiCourse[0];
                }
            }
            set
            {
                _equiCourse[0] = value;
            }
        }
        public string CourseName
        {
            get
            {
                return ChildCourse.FullCode;
            }
        }
        #endregion

        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        private SqlParameter SectionNameParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = SECTIONNAME_PA;
                sqlIDParam.Value = SectionName;

                return sqlIDParam;
            }
        }
        public string LastSectionName
        {
            get { return _lastSectionName; }
            set { _lastSectionName = value; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        private SqlParameter CapacityParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = CAPACITY_PA;
                sqlIDParam.Value = Capacity;

                return sqlIDParam;
            }
        }

        public int RoomInfoOneID
        {
            get { return _roomInfoOneID; }
            set { _roomInfoOneID = value; }
        }
        private SqlParameter RoomInfoIDOneParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = ROOMINFOIDONE_PA;
                sqlIDParam.Value = RoomInfoOneID;

                return sqlIDParam;
            }
        }
        public RoomInfo RoomInfoOne
        {
            get
            {
                if (_roomInfoOne == null)
                {
                    if ((RoomInfoOneID > 0))
                    {
                        _roomInfoOne = RoomInfo.GetRoomInfo(RoomInfoOneID);
                    }
                }
                return _roomInfoOne;
            }
        }

        public int RoomInfoTwoID
        {
            get { return _roomInfoTwoID; }
            set { _roomInfoTwoID = value; }
        }
        private SqlParameter RoomInfoIDTwoParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = ROOMINFOIDTWO_PA;

                if (RoomInfoTwoID == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = RoomInfoTwoID;
                }


                return sqlIDParam;
            }
        }
        public RoomInfo RoomInfoTwo
        {
            get
            {
                if (_roomInfoTwo == null)
                {
                    if ((RoomInfoTwoID > 0))
                    {
                        _roomInfoTwo = RoomInfo.GetRoomInfo(RoomInfoTwoID);
                    }
                }
                return _roomInfoTwo;
            }
        }


        public int TimeSlotPlanOneID
        {
            get { return _timeSlotPlanOneID; }
            set { _timeSlotPlanOneID = value; }
        }
        private SqlParameter TimeSlotPlanOneIDParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = TIMESLOTPLANIDONE_PA;
                sqlIDParam.Value = TimeSlotPlanOneID;

                return sqlIDParam;
            }
        }
        public TimeSlotPlan TimeSlotPlanOne
        {
            get
            {
                if (_timeSlotPlanOne == null)
                {
                    if ((TimeSlotPlanOneID > 0))
                    {
                        _timeSlotPlanOne = TimeSlotPlan.GetTimeSlotPlan(TimeSlotPlanOneID);
                    }
                }
                return _timeSlotPlanOne;
            }
        }

        public int TimeSlotPlanTwoID
        {
            get { return _timeSlotPlanTwoID; }
            set { _timeSlotPlanTwoID = value; }
        }
        private SqlParameter TimeSlotPlanTwoIDParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = TIMESLOTPLANIDTWO_PA;

                if (TimeSlotPlanTwoID == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = TimeSlotPlanTwoID;
                }

                return sqlIDParam;
            }
        }
        public TimeSlotPlan TimeSlotPlanTwo
        {
            get
            {
                if (_timeSlotPlanTwo == null)
                {
                    if ((TimeSlotPlanTwoID > 0))
                    {
                        _timeSlotPlanTwo = TimeSlotPlan.GetTimeSlotPlan(TimeSlotPlanTwoID);
                    }
                }
                return _timeSlotPlanTwo;
            }
        }

        public int DayOneValue
        {
            get { return _dayOneValue; }
            set { _dayOneValue = value; }
        }
        public FriendlyWeekDays DayOne
        {
            get
            {
                if (_dayOne == null)
                {
                    _dayOne = new FriendlyWeekDays(DayOneValue);
                }
                _dayOne.Value = DayOneValue;
                return _dayOne;
            }

        }
        private SqlParameter DayOneParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = DAYONE_PA;
                if (DayOneValue == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = DayOneValue;
                }

                return sqlIDParam;
            }
        }

        public int DayTwoValue
        {
            get { return _dayTwoValue; }
            set { _dayTwoValue = value; }
        }
        public FriendlyWeekDays DayTwo
        {
            get
            {
                if (_dayTwo == null)
                {
                    _dayTwo = new FriendlyWeekDays(DayTwoValue);
                }
                _dayTwo.Value = DayTwoValue;
                return _dayTwo;
            }
        }
        private SqlParameter DayTwoParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = DAYTWO_PA;

                if (DayTwoValue == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = DayTwoValue;
                }


                return sqlIDParam;
            }
        }


        public int TeacherIDOne
        {
            get { return _teacherIDOne; }
            set { _teacherIDOne = value; }
        }
        private SqlParameter TeacherIDOneParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = TEACHERIDONE_PA;
                sqlIDParam.Value = TeacherIDOne;

                return sqlIDParam;
            }
        }
        public Teacher AssignedTeacherOne
        {
            get
            {
                if (_assignedTeacherOne == null)
                {
                    if ((TeacherIDOne > 0))
                    {
                        _assignedTeacherOne = Teacher.Get(TeacherIDOne);
                    }
                }
                return _assignedTeacherOne;
            }
        }

        public int TeacherIDTwo
        {
            get { return _teacherIDTwo; }
            set { _teacherIDTwo = value; }
        }
        private SqlParameter TeacherIDTwoParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = TEACHERIDTWO_PA;
                if (TeacherIDTwo == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = TeacherIDTwo;
                }


                return sqlIDParam;
            }
        }
        public Teacher AssignedTeacherTwo
        {
            get
            {
                if (_assignedTeacherTwo == null)
                {
                    if ((TeacherIDTwo > 0))
                    {
                        _assignedTeacherTwo = Teacher.Get(TeacherIDTwo);
                    }
                }
                return _assignedTeacherTwo;
            }
        }

        public int DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }
        private SqlParameter DeptIDParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = DEPTID_PA;
                sqlIDParam.Value = DeptID;

                return sqlIDParam;
            }
        }
        public int LastdeptID
        {
            get { return _lastdeptID; }
            set { _lastdeptID = value; }
        }
        public Department OwnerDepartment
        {
            get
            {
                if (_ownerDepartment == null)
                {
                    if (_deptID > 0)
                    {
                        _ownerDepartment = Department.GetDept(_deptID);
                    }
                }
                return _ownerDepartment;
            }
        }

        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }
        private SqlParameter ProgramIDParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = PROGRAMID_PA;
                if (ProgramID == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = ProgramID;
                }
                return sqlIDParam;
            }
        }
        public int LastProgramID
        {
            get { return _lastProgramID; }
            set { _lastProgramID = value; }
        }

        public int ShareDptIDOne
        {
            get { return _shareDptIDOne; }
            set { _shareDptIDOne = value; }
        }
        private SqlParameter ShareDptIDOneParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = SHAREDPTIDONE_PA;
                if (ShareDptIDOne == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = ShareDptIDOne;
                }


                return sqlIDParam;
            }
        }
        public Department ShareDptOne
        {
            get
            {
                if (_shareDptOne == null)
                {
                    if (ShareDptIDOne > 0)
                    {
                        _shareDptOne = Department.GetDept(ShareDptIDOne);
                    }
                }
                return _shareDptOne;
            }
        }

        public int ShareDptIDTwo
        {
            get { return _shareDptIDTwo; }
            set { _shareDptIDTwo = value; }
        }
        private SqlParameter ShareDptIDTwoParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = SHAREDPTIDTWO_PA;
                if (ShareDptIDTwo == 0)
                {
                    sqlIDParam.Value = DBNull.Value;
                }
                else
                {
                    sqlIDParam.Value = ShareDptIDTwo;
                }


                return sqlIDParam;
            }
        }
        public Department ShareDptTwo
        {
            get
            {
                if (_shareDptTwo == null)
                {
                    if (ShareDptIDTwo > 0)
                    {
                        _shareDptTwo = Department.GetDept(ShareDptIDTwo);
                    }
                }
                return _shareDptTwo;
            }
        }

        public string SectionTime
        {
            get { return SectionName + " - " + TimeSlotPlanOne.Time + " - " + DayOne.Name + ((TimeSlotPlanTwo == null) ? "" : " -- " + TimeSlotPlanTwo.Time) + ((DayTwoValue == 0) ? "" : " - " + DayTwo.Name); }
        }

        public int TypeDefinationID
        {
            get { return _typeDefinitionID; }
            set { _typeDefinitionID = value; }
        }
        private SqlParameter TypeDefinationIDParam
        {
            get
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = TYPEDEFINITION_PA;
                if (TypeDefinationID == 0)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = TypeDefinationID;
                }
                return param;
            }
        }
        public TypeDefinition SectionTypeDefinition
        {
            get
            {
                if (_typeDefinition == null)
                {
                    if (TypeDefinationID > 0)
                    {
                        _typeDefinition = TypeDefinition.GetTypeDef(TypeDefinationID);
                    }
                }
                return _typeDefinition;
            }
        }


        public int Occupied
        {
            get { return _occupied; }
            set { _occupied = value; }
        }
        private SqlParameter OccupiedParam
        {
            get
            {
                SqlParameter sqlIDParam = new SqlParameter();

                sqlIDParam.ParameterName = OCCUPIED_PA;
                sqlIDParam.Value = Occupied;

                return sqlIDParam;
            }
        }
        #endregion

        #region Methods
        private static AcademicCalenderSection Mapper(SQLNullHandler nullHandler)
        {
            AcademicCalenderSection obj = new AcademicCalenderSection();

            obj.Id = nullHandler.GetInt32(ACACAL_SECTIONID);//0
            obj.AcademicCalenderID = nullHandler.GetInt32(ACADEMIC_CALENDER_ID);//2
            obj.LastacademicCalenderID = nullHandler.GetInt32(ACADEMIC_CALENDER_ID);
            obj.ChildCourseID = nullHandler.GetInt32(COURSE_ID);
            obj.LastchildCourseID = nullHandler.GetInt32(COURSE_ID);
            obj.ChildVersionID = nullHandler.GetInt32(VERSION_ID);//9
            obj.LastchildVersionID = nullHandler.GetInt32(VERSION_ID);
            obj.SectionName = nullHandler.GetString(SECTIONNAME);//2
            obj.LastSectionName = nullHandler.GetString(SECTIONNAME);
            obj.Capacity = nullHandler.GetInt32(CAPACITY);
            obj.RoomInfoOneID = nullHandler.GetInt32(ROOMINFOIDONE);//3
            obj.RoomInfoTwoID = nullHandler.GetInt32(ROOMINFOIDTWO);//4
            obj.TimeSlotPlanOneID = nullHandler.GetInt32(TIMESLOTPLANIDONE);//5
            obj.TimeSlotPlanTwoID = nullHandler.GetInt32(TIMESLOTPLANIDTWO);//6
            obj.DayOneValue = nullHandler.GetInt32(DAYONE);//7
            obj.DayTwoValue = nullHandler.GetInt32(DAYTWO);//8
            obj.TeacherIDOne = nullHandler.GetInt32(TEACHERIDONE);//9
            obj.TeacherIDTwo = nullHandler.GetInt32(TEACHERIDTWO);//10
            obj.DeptID = nullHandler.GetInt32(DEPTID);//11
            obj.LastdeptID = nullHandler.GetInt32(DEPTID);
            obj.ProgramID = nullHandler.GetInt32(PROGRAMID);
            obj.LastProgramID = nullHandler.GetInt32(PROGRAMID);
            obj.ShareDptIDOne = nullHandler.GetInt32(SHAREDPTIDONE);
            obj.ShareDptIDTwo = nullHandler.GetInt32(SHAREDPTIDTWO);
            obj.CreatorID = nullHandler.GetInt32(CREATORID);//12
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//13
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//14
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//15

            obj.TypeDefinationID = nullHandler.GetInt32(TYPEDEFINITION);
            obj.Occupied = nullHandler.GetInt32(OCCUPIED);

            return obj;
        }
        private static AcademicCalenderSection MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            AcademicCalenderSection obj = null;
            if (theReader.Read())
            {
                obj = new AcademicCalenderSection();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<AcademicCalenderSection> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<AcademicCalenderSection> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<AcademicCalenderSection>();
                }
                AcademicCalenderSection obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        private static HashSet<AcademicCalenderSection> MapDistinctCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            HashSet<AcademicCalenderSection> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new HashSet<AcademicCalenderSection>();
                }
                AcademicCalenderSection obj = Mapper(nullHandler);
                if (!collection.Contains(obj))
                {
                    collection.Add(obj);
                }
            }

            return collection;
        }
        private static void MapDistinctCollection(SqlDataReader theReader, HashSet<AcademicCalenderSection> collection)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new HashSet<AcademicCalenderSection>();
                }
                AcademicCalenderSection obj = Mapper(nullHandler);
                if (!collection.Contains(obj))
                {
                    collection.Add(obj);
                }
            }
        }

        public static List<AcademicCalenderSection> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<AcademicCalenderSection> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }
        public static AcademicCalenderSection Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + ACACAL_SECTIONID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            AcademicCalenderSection obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static AcademicCalenderSection Get(int academicCalenderID, int courseID, int versionID, string sectionName)
        {
            string command = SELECT
                            + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA
                            + " AND [" + COURSE_ID + "] = " + COURSE_ID_PA
                            + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA
                            + " AND [" + SECTIONNAME + "] = " + SECTIONNAME_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);
            SqlParameter secNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(sectionName, SECTIONNAME_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { academicCalenderIDParam, courseIDParam, versionIDParam, secNameParam });

            AcademicCalenderSection obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
        public static List<AcademicCalenderSection> Gets(int academicCalenderID, int courseID, int versionID)
        {
            string command = SELECT
                            + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA
                            + " AND [" + COURSE_ID + "] = " + COURSE_ID_PA
                            + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { academicCalenderIDParam, courseIDParam, versionIDParam });

            List<AcademicCalenderSection> obj = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static List<AcademicCalenderSection> GetByACACAlenderID(int academicCalenderID)
        {
            string command = SELECT
                            + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { academicCalenderIDParam });


            List<AcademicCalenderSection> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static HashSet<AcademicCalenderSection> GetByACACAlenderID(Course course)
        {
            StringBuilder command = new StringBuilder(SELECT
                            + "WHERE [" + COURSE_ID + "] = " + COURSE_ID_PA + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA);

            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(course.Id, COURSE_ID_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(course.VersionID, VERSION_ID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command.ToString(), sqlConn, sqlTran, new SqlParameter[] { courseIDParam, versionIDParam });
            HashSet<AcademicCalenderSection> collection = MapDistinctCollection(theReader);

            command = new StringBuilder(SELECT
                            + "FROM dbo.AcademicCalenderSection LEFT OUTER JOIN "
                            + "dbo.Course ON dbo.AcademicCalenderSection.CourseID = dbo.Course.CourseID AND "
                            + "dbo.AcademicCalenderSection.VersionID = dbo.Course.VersionID "
                            + "WHERE (dbo.Course.FormalCode = @FORMALCODE)");

            courseIDParam = null;
            courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(course.FormalCode, "@FORMALCODE");

            theReader = null;
            theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command.ToString(), sqlConn, sqlTran, new SqlParameter[] { courseIDParam });
            MapDistinctCollection(theReader, collection);

            command = new StringBuilder(SELECT
                + "FROM dbo.AcademicCalenderSection LEFT OUTER JOIN "
                + "dbo.EquiCourse ON dbo.AcademicCalenderSection.CourseID = dbo.EquiCourse.ParentCourseID AND "
                + "dbo.AcademicCalenderSection.VersionID = dbo.EquiCourse.ParentVersionID "
                + "WHERE [" + COURSE_ID + "] = " + COURSE_ID_PA + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA);

            courseIDParam = null;
            courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(course.Id, COURSE_ID_PA);

            versionIDParam = null;
            versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(course.VersionID, VERSION_ID_PA);

            theReader = null;
            theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command.ToString(), sqlConn, sqlTran, new SqlParameter[] { courseIDParam, versionIDParam });
            MapDistinctCollection(theReader, collection);

            command = new StringBuilder(SELECT
            + "FROM dbo.AcademicCalenderSection INNER JOIN "
            + "dbo.EquiCourse ON dbo.AcademicCalenderSection.CourseID = dbo.EquiCourse.EquiCourseID AND "
            + "dbo.AcademicCalenderSection.VersionID = dbo.EquiCourse.EquiVersionID "
            + "WHERE [" + COURSE_ID + "] = " + COURSE_ID_PA + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA);

            courseIDParam = null;
            courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(course.Id, COURSE_ID_PA);

            versionIDParam = null;
            versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(course.VersionID, VERSION_ID_PA);

            theReader = null;
            theReader = MSSqlConnectionHandler.MSSqlExecuteBatchQuerry(command.ToString(), sqlConn, sqlTran, new SqlParameter[] { courseIDParam, versionIDParam });
            MapDistinctCollection(theReader, collection);

            theReader.Close();

            MSSqlConnectionHandler.CommitTransaction();
            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        internal static int GetMaxID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newID = 0;

            string command = "SELECT MAX(" + ACACAL_SECTIONID + ") FROM [" + TABLENAME + "]";
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newID = 1;
            }
            else if (ob is Int32)
            {
                newID = Convert.ToInt32(ob) + 1;
            }

            return newID;
        }

        public static bool IsExist(int academicCalenderID, int intDeptID, int intProgID, int courseID, int versionID, string sectionName, int capacity, int roomInfoOneID, int roomInfoTwoID, int dayOneValue, int dayTwoValue, int timeSlotPlanOneID, int timeSlotPlanTwoID, int teacherIDOne, int teacherIDTwo, int typeDefinationID)
        {
            string command = SELECT
                                        + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + academicCalenderID
                                        + " AND [" + COURSE_ID + "] = " + courseID
                                        + " AND [" + VERSION_ID + "] = " + versionID
                                        + " AND [" + SECTIONNAME + "] = " + "'" + sectionName + "'"
                                        + " AND [" + CAPACITY + "] = " + "'" + capacity + "'"
                                        + " AND [" + DEPTID + "] = " + intDeptID
                //
                                        + " AND [" + ROOMINFOIDONE + "] = " + roomInfoOneID
                                        +" AND [" + ROOMINFOIDTWO + "] = " + roomInfoTwoID
                                        +" AND [" + DAYONE + "] = " + dayOneValue
                                        +" AND [" + DAYTWO + "] = " + dayTwoValue
                                        +" AND [" + TIMESLOTPLANIDONE + "] = " + timeSlotPlanOneID
                                        +" AND [" + TIMESLOTPLANIDTWO + "] = " + timeSlotPlanTwoID
                                        +" AND [" + TEACHERIDONE + "] = " + teacherIDOne
                                        +" AND [" + TEACHERIDTWO + "] = " + teacherIDTwo
                                        +" AND [" + TYPEDEFINITION + "] = " + typeDefinationID
                                        //
                                        +" AND [" + PROGRAMID + "] = " + intProgID;

            //SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);
            //SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
            //SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);
            //SqlParameter secNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(sectionName, SECTIONNAME_PA);
            //SqlParameter deptIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(intDeptID, DEPTID_PA);
            //SqlParameter progIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(intProgID, PROGRAMID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            //object ob = MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { academicCalenderIDParam, courseIDParam, versionIDParam, secNameParam, deptIDParam, progIDParam });
            object ob = MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn);

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }
        internal static bool IsExist(int academicCalenderID, int courseID, int versionID, string sectionName, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = SELECT
                                        + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA
                                        + " AND [" + COURSE_ID + "] = " + COURSE_ID_PA
                                        + " AND [" + VERSION_ID + "] = " + VERSION_ID_PA
                                        + " AND [" + SECTIONNAME + "] = " + SECTIONNAME_PA;

            SqlParameter academicCalenderIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(academicCalenderID, ACADEMIC_CALENDER_ID_PA);
            SqlParameter courseIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(courseID, COURSE_ID_PA);
            SqlParameter versionIDParam = MSSqlConnectionHandler.MSSqlParamGenerator(versionID, VERSION_ID_PA);
            SqlParameter secNameParam = MSSqlConnectionHandler.MSSqlParamGenerator(sectionName, SECTIONNAME_PA);

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { academicCalenderIDParam, courseIDParam, versionIDParam, secNameParam });

            return (Convert.ToInt32(ob) > 0);
        }
        public static bool HasDuplicateCode(AcademicCalenderSection obj, int academicCalenderID, int intDeptID, int intProgramID, int courseID, int versionID, string sectionName, int capacity, int roomInfoOneID, int roomInfoTwoID, int dayOneValue, int dayTwoValue, int timeSlotPlanOneID, int timeSlotPlanTwoID, int teacherIDOne, int teacherIDTwo, int typeDefinationID)
        {
            if (obj == null)
            {
                return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.DeptID, obj.ProgramID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, obj.Capacity, obj.RoomInfoOneID, obj.RoomInfoTwoID, obj.DayOneValue, obj.DayTwoValue, obj.TimeSlotPlanOneID, obj.TimeSlotPlanTwoID, obj.TeacherIDOne, obj.TeacherIDTwo, obj.TypeDefinationID);
            }
            else
            {
                if (obj.Id == 0)
                {
                    if (AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.DeptID, obj.ProgramID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, obj.Capacity, obj.RoomInfoOneID, obj.RoomInfoTwoID, obj.DayOneValue, obj.DayTwoValue, obj.TimeSlotPlanOneID, obj.TimeSlotPlanTwoID, obj.TeacherIDOne, obj.TeacherIDTwo, obj.TypeDefinationID))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (obj.LastacademicCalenderID != academicCalenderID || obj.LastchildCourseID != courseID || obj.LastchildVersionID != versionID || obj.LastSectionName != sectionName || obj.LastdeptID != intDeptID || obj.LastProgramID != intProgramID || obj.Capacity != capacity || obj.RoomInfoOneID != roomInfoOneID || obj.RoomInfoTwoID != roomInfoTwoID || obj.DayOneValue != dayOneValue || obj.DayTwoValue != dayTwoValue || obj.TimeSlotPlanOneID != timeSlotPlanOneID || obj.TimeSlotPlanTwoID != timeSlotPlanTwoID || obj.TeacherIDOne != teacherIDOne || obj.TeacherIDTwo != teacherIDTwo || obj.TypeDefinationID != typeDefinationID)
                    {
                        return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.DeptID, obj.ProgramID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, obj.Capacity, obj.RoomInfoOneID, obj.RoomInfoTwoID, obj.DayOneValue, obj.DayTwoValue, obj.TimeSlotPlanOneID, obj.TimeSlotPlanTwoID, obj.TeacherIDOne, obj.TeacherIDTwo, obj.TypeDefinationID);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        internal static bool HasDuplicateCode(AcademicCalenderSection obj, int academicCalenderID, int courseID, int versionID, string sectionName, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (obj == null)
            {
                return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran);
            }
            else
            {
                if (obj.Id == 0)
                {
                    if (AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran))
                    {
                        return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (obj.AcademicCalenderID != academicCalenderID || obj.ChildCourseID != courseID || obj.ChildVersionID != versionID || obj.SectionName != sectionName)
                    {
                        return AcademicCalenderSection.IsExist(obj.AcademicCalenderID, obj.ChildCourseID, obj.ChildVersionID, obj.SectionName, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Save(AcademicCalenderSection obj, int academicCalenderID, int courseID, int versionID, string sectionName)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (HasDuplicateCode(obj, academicCalenderID, courseID, versionID, sectionName, sqlConn, sqlTran))
                {
                    throw new Exception("Duplicate Section Name Not Allowed.");
                }

                if (obj.Id == 0)
                {
                    #region Insert
                    command = INSERT;
                    sqlParams = new SqlParameter[] { //obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.AcademicCalenderIDParam,  
                                                     obj.ChildCourseIDParam,
                                                     obj.ChildVersionIDParam, //+ACACAL_COURSEID_PA + ", "//1 
                                                     obj.SectionNameParam,//+ SECTIONNAME_PA + ", "//2
                                                     obj.RoomInfoIDOneParam, //+ ROOMINFOIDONE_PA + ", "//3 
                                                     obj.RoomInfoIDTwoParam,//+ ROOMINFOIDTWO_PA + ", "//4
                                                     obj.TimeSlotPlanOneIDParam,  //+ TIMESLOTPLANIDONE_PA + ", "//5
                                                     obj.TimeSlotPlanTwoIDParam,//+ TIMESLOTPLANIDTWO_PA + ", "//6
                                                     obj.DayOneParam,  //+ DAYONE_PA + ", "//7
                                                     obj.DayTwoParam,//+ DAYTWO_PA + ", "//8
                                                     obj.TeacherIDOneParam,  //+ TEACHERIDONE_PA + ", "//9
                                                     obj.TeacherIDTwoParam,//+ TEACHERIDTWO_PA + ", "//10
                                                     obj.DeptIDParam,//+ DEPTID_PA + ", "//11
                                                     obj.ShareDptIDOneParam,
                                                     obj.ShareDptIDTwoParam,
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam };//+ MOIDFIEDDATE_PA + ")";//15 
                    #endregion
                }
                else
                {

                    #region Update
                    command = UPDATE
                    + " WHERE [" + ACACAL_SECTIONID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.AcademicCalenderIDParam,  
                                                     obj.ChildCourseIDParam,
                                                     obj.ChildVersionIDParam, //+ACACAL_COURSEID_PA + ", "//1 
                                                     obj.SectionNameParam,//+ SECTIONNAME_PA + ", "//2
                                                     obj.RoomInfoIDOneParam, //+ ROOMINFOIDONE_PA + ", "//3 
                                                     obj.RoomInfoIDTwoParam,//+ ROOMINFOIDTWO_PA + ", "//4
                                                     obj.TimeSlotPlanOneIDParam,  //+ TIMESLOTPLANIDONE_PA + ", "//5
                                                     obj.TimeSlotPlanTwoIDParam,//+ TIMESLOTPLANIDTWO_PA + ", "//6
                                                     obj.DayOneParam,  //+ DAYONE_PA + ", "//7
                                                     obj.DayTwoParam,//+ DAYTWO_PA + ", "//8
                                                     obj.TeacherIDOneParam,  //+ TEACHERIDONE_PA + ", "//9
                                                     obj.TeacherIDTwoParam,//+ TEACHERIDTWO_PA + ", "//10
                                                     obj.DeptIDParam,//+ DEPTID_PA + ", "//11
                                                     obj.ShareDptIDOneParam,
                                                     obj.ShareDptIDTwoParam,
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

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
        public static int Save(AcademicCalenderSection obj)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (obj.Id == 0)
                {
                    #region Insert
                    command = INSERT;
                    sqlParams = new SqlParameter[] { //obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                     obj.AcademicCalenderIDParam,  
                                                     obj.ChildCourseIDParam,
                                                     obj.ChildVersionIDParam, //+ACACAL_COURSEID_PA + ", "//1 
                                                     obj.SectionNameParam,//+ SECTIONNAME_PA + ", "//2
                                                     obj.CapacityParam,
                                                     obj.RoomInfoIDOneParam, //+ ROOMINFOIDONE_PA + ", "//3 
                                                     obj.RoomInfoIDTwoParam,//+ ROOMINFOIDTWO_PA + ", "//4
                                                     obj.TimeSlotPlanOneIDParam,  //+ TIMESLOTPLANIDONE_PA + ", "//5
                                                     obj.TimeSlotPlanTwoIDParam,//+ TIMESLOTPLANIDTWO_PA + ", "//6
                                                     obj.DayOneParam,  //+ DAYONE_PA + ", "//7
                                                     obj.DayTwoParam,//+ DAYTWO_PA + ", "//8
                                                     obj.TeacherIDOneParam,  //+ TEACHERIDONE_PA + ", "//9
                                                     obj.TeacherIDTwoParam,//+ TEACHERIDTWO_PA + ", "//10
                                                     obj.DeptIDParam,//+ DEPTID_PA + ", "//11
                                                     obj.ProgramIDParam,
                                                     obj.ShareDptIDOneParam,
                                                     obj.ShareDptIDTwoParam,
                                                     obj.TypeDefinationIDParam,
                                                     obj.OccupiedParam,
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam };//+ MOIDFIEDDATE_PA + ")";//15 
                    #endregion
                }
                else
                {

                    #region Update
                    command = UPDATE
                    + " WHERE [" + ACACAL_SECTIONID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { obj.AcademicCalenderIDParam,  
                                                     obj.ChildCourseIDParam,
                                                     obj.ChildVersionIDParam, //+ACACAL_COURSEID_PA + ", "//1 
                                                     obj.SectionNameParam,//+ SECTIONNAME_PA + ", "//2
                                                     obj.CapacityParam,
                                                     obj.RoomInfoIDOneParam, //+ ROOMINFOIDONE_PA + ", "//3 
                                                     obj.RoomInfoIDTwoParam,//+ ROOMINFOIDTWO_PA + ", "//4
                                                     obj.TimeSlotPlanOneIDParam,  //+ TIMESLOTPLANIDONE_PA + ", "//5
                                                     obj.TimeSlotPlanTwoIDParam,//+ TIMESLOTPLANIDTWO_PA + ", "//6
                                                     obj.DayOneParam,  //+ DAYONE_PA + ", "//7
                                                     obj.DayTwoParam,//+ DAYTWO_PA + ", "//8
                                                     obj.TeacherIDOneParam,  //+ TEACHERIDONE_PA + ", "//9
                                                     obj.TeacherIDTwoParam,//+ TEACHERIDTWO_PA + ", "//10
                                                     obj.DeptIDParam,//+ DEPTID_PA + ", "//11
                                                     obj.ProgramIDParam,
                                                     obj.ShareDptIDOneParam,
                                                     obj.ShareDptIDTwoParam,
                                                     obj.TypeDefinationIDParam,
                                                     obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                     obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                     obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                     obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                     obj.IDParam };
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            //catch (SqlException SqlEx)
            //{
            //    if (SqlEx.Number == 2627)
            //    {
            //        throw new Exception(Message.DUPLICATEMESSAGE);
            //    }
            //    else
            //    {
            //        throw SqlEx;                    
            //    }
            //}
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }
        internal static int Save(AcademicCalenderSection obj, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int counter = 0;

            string command = string.Empty;
            SqlParameter[] sqlParams = null;

            if (obj.Id == 0)
            {
                #region Insert
                obj.Id = AcademicCalenderSection.GetMaxID(sqlConn, sqlTran);
                command = INSERT;
                sqlParams = new SqlParameter[] { obj.IDParam,//"[" + ACACAL_SECTIONID + "], "//0
                                                 obj.AcademicCalenderIDParam,  
                                                 obj.ChildCourseIDParam,
                                                 obj.ChildVersionIDParam, //+ACACAL_COURSEID_PA + ", "//1 
                                                 obj.SectionNameParam,//+ SECTIONNAME_PA + ", "//2
                                                 obj.RoomInfoIDOneParam, //+ ROOMINFOIDONE_PA + ", "//3 
                                                 obj.RoomInfoIDTwoParam,//+ ROOMINFOIDTWO_PA + ", "//4
                                                 obj.TimeSlotPlanOneIDParam,  //+ TIMESLOTPLANIDONE_PA + ", "//5
                                                 obj.TimeSlotPlanTwoIDParam,//+ TIMESLOTPLANIDTWO_PA + ", "//6
                                                 obj.DayOneParam,  //+ DAYONE_PA + ", "//7
                                                 obj.DayTwoParam,//+ DAYTWO_PA + ", "//8
                                                 obj.TeacherIDOneParam,  //+ TEACHERIDONE_PA + ", "//9
                                                 obj.TeacherIDTwoParam,//+ TEACHERIDTWO_PA + ", "//10
                                                 obj.DeptIDParam,//+ DEPTID_PA + ", "//11
                                                 obj.ShareDptIDOneParam,
                                                 obj.ShareDptIDTwoParam,
                                                 obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                 obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                 obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                 obj.ModifiedDateParam };//+ MOIDFIEDDATE_PA + ")";//15 
                #endregion
            }
            else
            {

                #region Update
                command = UPDATE
                + " WHERE [" + ACACAL_SECTIONID + "] = " + ID_PA;
                sqlParams = new SqlParameter[] { obj.AcademicCalenderIDParam,  
                                                 obj.ChildCourseIDParam,
                                                 obj.ChildVersionIDParam, //+ACACAL_COURSEID_PA + ", "//1 
                                                 obj.SectionNameParam,//+ SECTIONNAME_PA + ", "//2
                                                 obj.RoomInfoIDOneParam, //+ ROOMINFOIDONE_PA + ", "//3 
                                                 obj.RoomInfoIDTwoParam,//+ ROOMINFOIDTWO_PA + ", "//4
                                                 obj.TimeSlotPlanOneIDParam,  //+ TIMESLOTPLANIDONE_PA + ", "//5
                                                 obj.TimeSlotPlanTwoIDParam,//+ TIMESLOTPLANIDTWO_PA + ", "//6
                                                 obj.DayOneParam,  //+ DAYONE_PA + ", "//7
                                                 obj.DayTwoParam,//+ DAYTWO_PA + ", "//8
                                                 obj.TeacherIDOneParam,  //+ TEACHERIDONE_PA + ", "//9
                                                 obj.TeacherIDTwoParam,//+ TEACHERIDTWO_PA + ", "//10
                                                 obj.DeptIDParam,//+ DEPTID_PA + ", "//11
                                                 obj.ShareDptIDOneParam,
                                                 obj.ShareDptIDTwoParam,
                                                 obj.CreatorIDParam, //+ CREATORID_PA + ", "//12
                                                 obj.CreatedDateParam, //+ CREATEDDATE_PA + ", "//13
                                                 obj.ModifierIDParam, //+ MODIFIERID_PA + ", "//14
                                                 obj.ModifiedDateParam, //+ MOIDFIEDDATE_PA + ")";//15 
                                                 obj.IDParam };//"[" + ACACAL_SECTIONID + "], "//0
                #endregion
            }
            counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);


            return counter;
        }

        public static int Delete(int rowiD, int calenderID)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlParameter iDParam = new SqlParameter();
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();
                if (rowiD != -1)
                {
                    command = DELETE
                                    + "WHERE [" + ACACAL_SECTIONID + "] = " + ID_PA;

                    iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(rowiD, ID_PA);
                }
                else if (calenderID != -1)
                {
                    command = DELETE
                                        + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + ACADEMIC_CALENDER_ID_PA;

                    iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(calenderID, ACADEMIC_CALENDER_ID_PA);
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

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

        public static List<AcademicCalenderSection> GetCourseRoutine(int intCalID, int intDeptID, int intProgramID, int intCourseID, int intVersionID)
        {
            string command = string.Empty;
            if (intCourseID != 0)
            {
                if (intProgramID == 0 && intDeptID == 0)
                {
                    command = SELECT
                                    + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + intCalID + " and [" + COURSE_ID + "] = " + intCourseID + " and [" + VERSION_ID + "] = " + intVersionID;
                }
                else if (intDeptID != 0 && intProgramID == 0)
                {
                    command = SELECT
                                    + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + intCalID + " and [" + DEPTID + "] = " + intDeptID + " and [" + COURSE_ID + "] = " + intCourseID + " and [" + VERSION_ID + "] = " + intVersionID;
                }
                else if (intDeptID != 0 && intProgramID != 0)
                {
                    command = SELECT
                                    + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + intCalID + " and [" + DEPTID + "] = " + intDeptID + " and [" + PROGRAMID + "] = " + intProgramID + " and [" + COURSE_ID + "] = " + intCourseID + " and [" + VERSION_ID + "] = " + intVersionID;
                }
            }
            else
            {
                if (intProgramID == 0 && intDeptID == 0)
                {
                    command = SELECT
                                    + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + intCalID;
                }
                else if (intDeptID != 0 && intProgramID == 0)
                {
                    command = SELECT
                                    + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + intCalID + " and [" + DEPTID + "] = " + intDeptID;
                }
                else if (intDeptID != 0 && intProgramID != 0)
                {
                    command = SELECT
                                    + "WHERE [" + ACADEMIC_CALENDER_ID + "] = " + intCalID + " and [" + DEPTID + "] = " + intDeptID + " and [" + PROGRAMID + "] = " + intProgramID;
                }
            }

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<AcademicCalenderSection> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        #endregion

        public static List<AcademicCalenderSection> GetsByTeacherAndAcaCal(int teacher, int acaCal)
        {
            string command = "SELECT sec.* FROM dbo.Course AS c INNER JOIN " +
                            "(SELECT s1.* " +
                            "FROM AcademicCalenderSection AS s1 " +
                            "WHERE s1.TeacherOneID = " + teacher + " AND s1.AcademicCalenderID = " + acaCal + " " +
                            "UNION " +
                            "SELECT s2.* " +
                            "FROM AcademicCalenderSection  AS s2 " +
                            "WHERE s2.TeacherTwoID = " + teacher + " AND s2.AcademicCalenderID = " + acaCal + ") AS sec " +
                            "ON sec.CourseID =c.CourseID AND sec.VersionID = c.VersionID " +
                            "WHERE c.IsActive = 1 " +
                            "ORDER BY FormalCode, VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            List<AcademicCalenderSection> obj = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static List<AcademicCalenderSection> GetsByTeacherAcacalProgram(int acaCal, int program)
        {
            string command = "SELECT sec.* FROM dbo.Course AS c INNER JOIN " +
                            "(SELECT s1.* " +
                            "FROM AcademicCalenderSection AS s1 " +
                            "WHERE  s1.AcademicCalenderID = " + acaCal + " AND s1.ProgramID = " + program + " " +
                            "UNION " +
                            "SELECT s2.* " +
                            "FROM AcademicCalenderSection  AS s2 " +
                            "WHERE  s2.AcademicCalenderID = " + acaCal + " AND s2.ProgramID = " + program + ") AS sec " +
                            "ON sec.CourseID =c.CourseID AND sec.VersionID = c.VersionID " +
                            "WHERE c.IsActive = 1 " +
                            "ORDER BY FormalCode, VersionCode";

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command.ToString(), sqlConn);

            List<AcademicCalenderSection> obj = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }
    }
}
