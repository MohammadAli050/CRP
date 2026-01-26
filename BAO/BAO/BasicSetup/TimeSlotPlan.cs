using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;
using Common;

namespace BussinessObject
{
    [Serializable]
    public class TimeSlotPlan : Base
    {
        #region DBColumns
        //TimeSlotPlanID
        //StartHour
        //StartMin
        //StartAMPM
        //EndHour
        //EndMin
        //EndAMPM
        //Type
        //CreatedBy
        //CreatedDate
        //ModifiedBy
        //ModifiedDate 
	    #endregion

        #region Variables
        private int _startHour;
        private int _startMin;
        private AMPM _startAMPM;
        private int _endHour;
        private int _endMin;
        private AMPM _endAMPM;
        private TimeSlotType _type;
        #endregion

        #region Constructor
        public TimeSlotPlan():base()
        {
            _startHour = 0;
            _startMin = 0;
            _startAMPM = AMPM.AM;
            _endHour = 0;
            _endMin = 0;
            _endAMPM = AMPM.AM;
            _type = TimeSlotType.None;
        } 
        #endregion

        #region Constants

        private const string TIMESLOTPLANID = "TimeSlotPlanID";

        private const string STARTHOUR_PA = "@StartHour";
        private const string STARTHOUR = "StartHour";

        private const string STARTMIN_PA = "@StartMin";
        private const string STARTMIN = "StartMin";

        private const string STARTAMPM_PA = "@StartAMPM";
        private const string STARTAMPM = "StartAMPM";

        private const string ENDHOUR_PA = "@EndHour";
        private const string ENDHOUR = "EndHour";

        private const string ENDMIN_PA = "@EndMin";
        private const string ENDMIN = "EndMin";

        private const string ENDAMPM_PA = "@EndAMPM";
        private const string ENDAMPM = "EndAMPM";

        private const string TYPE_PA = "@Type";
        private const string TYPE = "Type";

        private const string ALLCOLUMNS = TIMESLOTPLANID + ", "
                                        + STARTHOUR + ", "
                                        + STARTMIN + ", "
                                        + STARTAMPM + ", "
                                        + ENDHOUR + ", "
                                        + ENDMIN + ", "
                                        + ENDAMPM + ", "
                                        + TYPE + ", ";

        private const string NOPKCOLUMNS = STARTHOUR + ", "
                                        + STARTMIN + ", "
                                        + STARTAMPM + ", "
                                        + ENDHOUR + ", "
                                        + ENDMIN + ", "
                                        + ENDAMPM + ", "
                                        + TYPE + ", ";

        private const string TABLENAME = " [TimeSlotPlan] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS + ")"
                             + "VALUES ( "
                             + STARTHOUR_PA + ", "
                             + STARTMIN_PA + ", "
                             + STARTAMPM_PA + ", "
                             + ENDHOUR_PA + ", "
                             + ENDMIN_PA + ", "
                             + ENDAMPM_PA + ", "
                             + TYPE_PA + ", "
                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET " + STARTHOUR + " = " + STARTHOUR_PA + ", "
                            + STARTMIN + " = " + STARTMIN_PA + ", "
                            + STARTAMPM + " = " + STARTAMPM_PA + ", "
                            + ENDHOUR + " = " + ENDHOUR_PA + ","
                            + ENDMIN + " = " + ENDMIN_PA + ","
                            + ENDAMPM + " = " + ENDAMPM_PA + ","
                            + TYPE + " = " + TYPE_PA + ","
                            + CREATORID + " = " + CREATORID_PA + ","
                            + CREATEDDATE + " = " + CREATEDDATE_PA + ","
                            + MODIFIERID + " = " + MODIFIERID_PA + ","
                            + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion

        #region Properties
        public int StartHour
        {
            get { return _startHour; }
            set { _startHour = value; }
        }
        protected SqlParameter StartHourParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STARTHOUR_PA;

                sqlParam.Value = StartHour;

                return sqlParam;
            }
        }

        public int StartMin
        {
            get { return _startMin; }
            set { _startMin = value; }
        }
        protected SqlParameter StartMinParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STARTMIN_PA;

                sqlParam.Value = StartMin;

                return sqlParam;
            }
        }

        public AMPM StartAMPM
        {
            get { return _startAMPM; }
            set { _startAMPM = value; }
        }
        protected SqlParameter StartAMPMParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = STARTAMPM_PA;

                sqlParam.Value = (int)StartAMPM;

                return sqlParam;
            }
        }

        public string StartTime
        {
            get { return StartHour.ToString() + ":" + StartMin.ToString().PadLeft(2, '0') + ":" + StartAMPM.ToString(); }
        }

        public int EndHour
        {
            get { return _endHour; }
            set { _endHour = value; }
        }
        protected SqlParameter EndHourParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ENDHOUR_PA;

                sqlParam.Value = EndHour;

                return sqlParam;
            }
        }

        public int EndMin
        {
            get { return _endMin; }
            set { _endMin = value; }
        }
        protected SqlParameter EndMinParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ENDMIN_PA;

                sqlParam.Value = EndMin;

                return sqlParam;
            }
        }

        public AMPM EndAMPM
        {
            get { return _endAMPM; }
            set { _endAMPM = value; }
        }
        protected SqlParameter EndAMPMParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = ENDAMPM_PA;

                sqlParam.Value = (int)EndAMPM;

                return sqlParam;
            }
        }

        public string EndTime
        {
            get { return EndHour.ToString() + ":" + EndMin.ToString().PadLeft(2, '0') + ":" + EndAMPM.ToString(); }
        }
        
        public string Time
        {
            get {
                return StartTime + "-" + EndTime;
            }
            set
            {
                string tkened = value;
                string[] x = tkened.Split(new char[] { '-' });
                string s = x[0];
                string t = x[1];
            }
        }
        public TimeSlotType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        protected SqlParameter TypeParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = TYPE_PA;

                sqlParam.Value = (int)Type;

                return sqlParam;
            }
        }

        public TimeSpan DurationTime
        {

            get
            {
                TimeSpan StartTime = new TimeSpan(StartHour, StartMin, 0);
                TimeSpan EndTime = new TimeSpan(EndHour, EndMin, 0);
                TimeSpan DuraTime = StartTime.Subtract(EndTime);
                return DuraTime;
            }
        } 
        #endregion

        #region Methods
        private static TimeSlotPlan TimeSlotMapper(SQLNullHandler nullHandler)
        {
            TimeSlotPlan timeSlotPlan = new TimeSlotPlan();

            timeSlotPlan.Id = nullHandler.GetInt32(TIMESLOTPLANID);
            timeSlotPlan.StartHour = nullHandler.GetInt32(STARTHOUR);
            timeSlotPlan.StartMin = nullHandler.GetInt32(STARTMIN);
            timeSlotPlan.StartAMPM = (AMPM)nullHandler.GetInt32(STARTAMPM);
            timeSlotPlan.EndHour = nullHandler.GetInt32(ENDHOUR);
            timeSlotPlan.EndMin = nullHandler.GetInt32(ENDMIN);
            timeSlotPlan.EndAMPM = (AMPM)nullHandler.GetInt32(ENDAMPM);
            timeSlotPlan.Type = (TimeSlotType)nullHandler.GetInt32(TYPE);
            timeSlotPlan.CreatorID = nullHandler.GetInt32(CREATORID);
            timeSlotPlan.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            timeSlotPlan.ModifierID = nullHandler.GetInt32(MODIFIERID);
            timeSlotPlan.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return timeSlotPlan;
        }

        private static List<TimeSlotPlan> MapTimeSlotPlans(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TimeSlotPlan> timeSlotPlans = null;

            while (theReader.Read())
            {
                if (timeSlotPlans == null)
                {
                    timeSlotPlans = new List<TimeSlotPlan>();
                }
                TimeSlotPlan timeSlotPlan = TimeSlotMapper(nullHandler);
                timeSlotPlans.Add(timeSlotPlan);
            }

            return timeSlotPlans;
        }

        private static TimeSlotPlan MapTimeSlotPlan(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TimeSlotPlan timeSlotPlan = null;
            if (theReader.Read())
            {
                timeSlotPlan = new TimeSlotPlan();
                timeSlotPlan = TimeSlotMapper(nullHandler);
            }

            return timeSlotPlan;
        }

        public static List<TimeSlotPlan> GetTimeSlotPlans()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<TimeSlotPlan> timeSlotPlans = MapTimeSlotPlans(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return timeSlotPlans;
        }

        public static List<TimeSlotPlan> GetTimeSlotPlans(string parameter)
        {
            string command = SELECT
                            + "WHERE " + STARTHOUR + " = " + parameter + " OR " + ENDHOUR + " = " + parameter;
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<TimeSlotPlan> timeSlotPlans = MapTimeSlotPlans(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return timeSlotPlans;
        }

        public static TimeSlotPlan GetTimeSlotPlan(int timeSlotID)
        {
            string command = SELECT
                            + "WHERE "+TIMESLOTPLANID+" = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(timeSlotID, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            TimeSlotPlan timeSlotPlan = MapTimeSlotPlan(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return timeSlotPlan;
        }

        public static TimeSlotPlan GetTimeSlotPlan(int stratHour, int startMin, int endHour, int endMin, AMPM startAMPM, AMPM endAMPM, TimeSlotType type)
        {
            string command = SELECT
                            + "WHERE " + STARTHOUR + " = " + STARTHOUR_PA + " AND "
                            + STARTMIN + " = " + STARTMIN_PA + " AND "
                            + STARTAMPM + " = " + STARTAMPM_PA + " AND "
                            + ENDHOUR + " = " + ENDHOUR_PA + " AND "
                            + ENDMIN + " = " + ENDMIN_PA + " AND "
                            + ENDAMPM + " = " + ENDAMPM_PA + " AND "
                            + TYPE + " = " + TYPE_PA;

            SqlParameter stratHourParam = MSSqlConnectionHandler.MSSqlParamGenerator(stratHour, STARTHOUR_PA);
            SqlParameter startMinParam = MSSqlConnectionHandler.MSSqlParamGenerator(startMin, STARTMIN_PA);
            SqlParameter startAMPMParam = MSSqlConnectionHandler.MSSqlParamGenerator(startAMPM, STARTAMPM_PA);
            SqlParameter endHourParam = MSSqlConnectionHandler.MSSqlParamGenerator(endHour, ENDHOUR_PA);
            SqlParameter endMinParam = MSSqlConnectionHandler.MSSqlParamGenerator(endMin, ENDMIN_PA);
            SqlParameter endAMPMParam = MSSqlConnectionHandler.MSSqlParamGenerator(endAMPM, ENDAMPM_PA);
            SqlParameter typeParam = MSSqlConnectionHandler.MSSqlParamGenerator((int)type, TYPE_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { stratHourParam, startMinParam, startAMPMParam, endHourParam, endMinParam, endAMPMParam, typeParam });

            TimeSlotPlan timeSlotPlan = MapTimeSlotPlan(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return timeSlotPlan;
        }

        public static bool IsExist(int stratHour, int startMin, int endHour, int endMin, AMPM startAMPM, AMPM endAMPM, TimeSlotType type)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE " + STARTHOUR + " = " + STARTHOUR_PA + " AND "
                            + STARTMIN + " = " + STARTMIN_PA + " AND "
                            + STARTAMPM + " = " + STARTAMPM_PA + " AND "
                            + ENDHOUR + " = " + ENDHOUR_PA + " AND "
                            + ENDMIN + " = " + ENDMIN_PA + " AND "
                            + ENDAMPM + " = " + ENDAMPM_PA + " AND "
                            + TYPE + " = " + TYPE_PA;

            SqlParameter stratHourParam = MSSqlConnectionHandler.MSSqlParamGenerator(stratHour, STARTHOUR_PA);
            SqlParameter startMinParam = MSSqlConnectionHandler.MSSqlParamGenerator(startMin, STARTMIN_PA);
            SqlParameter startAMPMParam = MSSqlConnectionHandler.MSSqlParamGenerator(startAMPM, STARTAMPM_PA);
            SqlParameter endHourParam = MSSqlConnectionHandler.MSSqlParamGenerator(endHour, ENDHOUR_PA);
            SqlParameter endMinParam = MSSqlConnectionHandler.MSSqlParamGenerator(endMin, ENDMIN_PA);
            SqlParameter endAMPMParam = MSSqlConnectionHandler.MSSqlParamGenerator(endAMPM, ENDAMPM_PA);
            SqlParameter typeParam = MSSqlConnectionHandler.MSSqlParamGenerator((int)type, TYPE_PA);

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, new SqlParameter[] { stratHourParam, startMinParam, startAMPMParam, endHourParam, endMinParam, endAMPMParam, typeParam });

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }

        internal static bool IsExist(int stratHour, int startMin, int endHour, int endMin, AMPM startAMPM, AMPM endAMPM, TimeSlotType type, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE " + STARTHOUR + " = " + STARTHOUR_PA + " AND "
                            + STARTMIN + " = " + STARTMIN_PA + " AND "
                            + STARTAMPM + " = " + STARTAMPM_PA + " AND "
                            + ENDHOUR + " = " + ENDHOUR_PA + " AND "
                            + ENDMIN + " = " + ENDMIN_PA + " AND "
                            + ENDAMPM + " = " + ENDAMPM_PA + " AND "
                            + TYPE + " = " + TYPE_PA;

            SqlParameter stratHourParam = MSSqlConnectionHandler.MSSqlParamGenerator(stratHour, STARTHOUR_PA);
            SqlParameter startMinParam = MSSqlConnectionHandler.MSSqlParamGenerator(startMin, STARTMIN_PA);
            SqlParameter startAMPMParam = MSSqlConnectionHandler.MSSqlParamGenerator(startAMPM, STARTAMPM_PA);
            SqlParameter endHourParam = MSSqlConnectionHandler.MSSqlParamGenerator(endHour, ENDHOUR_PA);
            SqlParameter endMinParam = MSSqlConnectionHandler.MSSqlParamGenerator(endMin, ENDMIN_PA);
            SqlParameter endAMPMParam = MSSqlConnectionHandler.MSSqlParamGenerator(endAMPM, ENDAMPM_PA);
            SqlParameter typeParam = MSSqlConnectionHandler.MSSqlParamGenerator((int)type, TYPE_PA);

            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran, new SqlParameter[] { stratHourParam, startMinParam, startAMPMParam, endHourParam, endMinParam, endAMPMParam, typeParam });

            return (Convert.ToInt32(ob) > 0);
        }

        public static bool HasDuplicateCode(TimeSlotPlan dept, int stratHour, int startMin, int endHour, int endMin, AMPM startAMPM, AMPM endAMPM, TimeSlotType type)
        {
            if (dept == null)
            {
                return TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type))
                    {
                        return TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (dept.StartHour != stratHour || dept.StartMin != startMin || dept.EndHour != endHour || dept.EndMin != endMin || dept.StartAMPM != startAMPM || dept.EndAMPM != endAMPM || dept.Type != type)
                    {
                        return TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        internal static bool HasDuplicateCode(TimeSlotPlan dept, int stratHour, int startMin, int endHour, int endMin, AMPM startAMPM, AMPM endAMPM, TimeSlotType type, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            if (dept == null)
            {
                return TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type, sqlConn, sqlTran);
            }
            else
            {
                if (dept.Id == 0)
                {
                    if (TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type, sqlConn, sqlTran))
                    {
                        return TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (dept.StartHour != stratHour || dept.StartMin != startMin || dept.EndHour != endHour || dept.EndMin != endMin || dept.StartAMPM != startAMPM || dept.EndAMPM != endAMPM || dept.Type != type)
                    {
                        return TimeSlotPlan.IsExist(dept.StartHour,
                                            dept.StartMin,
                                            dept.EndHour,
                                            dept.EndMin,
                                            dept.StartAMPM,
                                            dept.EndAMPM,
                                            dept.Type, sqlConn, sqlTran);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Save(TimeSlotPlan program, int stratHour, int startMin, int endHour, int endMin, AMPM startAMPM, AMPM endAMPM, TimeSlotType type)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;

                if (HasDuplicateCode(program, stratHour,
                                            startMin,
                                            endHour,
                                            endMin,
                                            startAMPM,
                                            endAMPM,
                                            type, sqlConn, sqlTran))
                {
                    throw new Exception("Duplicate Program Code Not Allowed.");
                }

                if (program.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { program.StartHourParam,  
                                                     program.StartMinParam,
                                                     program.StartAMPMParam,
                                                     program.EndHourParam,
                                                     program.EndMinParam,
                                                     program.EndAMPMParam,
                                                     program.TypeParam,
                                                     program.CreatorIDParam, 
                                                     program.CreatedDateParam, 
                                                     program.ModifierIDParam, 
                                                     program.ModifiedDateParam };
                }
                else
                {


                    command = UPDATE
                            + " WHERE "+TIMESLOTPLANID+" = " + ID_PA;
                    sqlParams = new SqlParameter[] { program.StartHourParam,  
                                                     program.StartMinParam,
                                                     program.StartAMPMParam,
                                                     program.EndHourParam,
                                                     program.EndMinParam,
                                                     program.EndAMPMParam,
                                                     program.TypeParam,
                                                     program.CreatorIDParam, 
                                                     program.CreatedDateParam, 
                                                     program.ModifierIDParam, 
                                                     program.ModifiedDateParam , //10
                                                     program.IDParam };
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
        public static int Delete(int deptID)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE "+TIMESLOTPLANID+" = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(deptID, ID_PA);
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
        
        #endregion



        public static int CheckOverlaps(int sectionId, int studentId)
        {
            int count = 0;

            try
            {
                AcademicCalenderSection acsEntity = new AcademicCalenderSection();
                acsEntity = AcademicCalenderSection.Get(sectionId);

                List<Student_CalCourseProgNodeEntity> sccpn = new List<Student_CalCourseProgNodeEntity>();
                sccpn = Student_CalCourseProgNode_DAO.GetStdCalCourseProgNodeForStudent(studentId);

                foreach (Student_CalCourseProgNodeEntity item in sccpn)
                {
                    AcademicCalenderSection acsEntityTemp = new AcademicCalenderSection();
                    acsEntityTemp = AcademicCalenderSection.Get(item.AcaCal_SectionID);

                    //timeslotplan of wanted section.
                    TimeSlotPlan tsp1 = TimeSlotPlan.GetTimeSlotPlan(acsEntity.TimeSlotPlanOneID);
                    TimeSlotPlan tsp2 = TimeSlotPlan.GetTimeSlotPlan(acsEntity.TimeSlotPlanTwoID);

                    //timeslotplan of obtained section.
                    TimeSlotPlan tempTsp1 = TimeSlotPlan.GetTimeSlotPlan(acsEntityTemp.TimeSlotPlanOneID);
                    TimeSlotPlan tempTsp2 = TimeSlotPlan.GetTimeSlotPlan(acsEntityTemp.TimeSlotPlanTwoID);


                    // convert AM PM to 24 hours
                    TimeSpan ts1S = new TimeSpan(tsp1.StartAMPM.ToString() == "AM" ? tsp1.StartHour :
                                                tsp1.StartHour == 12 ? tsp1.StartHour : tsp1.StartHour + 12, tsp1.StartMin, 0);
                    TimeSpan ts1E = new TimeSpan(tsp1.EndAMPM.ToString() == "AM" ? tsp1.EndHour :
                                                tsp1.EndHour == 12 ? tsp1.EndHour : tsp1.EndHour + 12, tsp1.EndMin, 0);
                    TimeSpan ts2S = new TimeSpan(tsp2.StartAMPM.ToString() == "AM" ? tsp2.StartHour :
                                                tsp2.StartHour == 12 ? tsp2.StartHour : tsp2.StartHour + 12, tsp2.StartMin, 0);
                    TimeSpan ts2E = new TimeSpan(tsp2.EndAMPM.ToString() == "AM" ? tsp2.EndHour :
                                                tsp2.EndHour == 12 ? tsp2.EndHour : tsp2.EndHour + 12, tsp2.EndMin, 0);

                    TimeSpan tempTs1S = new TimeSpan(tempTsp1.StartAMPM.ToString() == "AM" ? tempTsp1.StartHour :
                                                    tempTsp1.StartHour == 12 ? tempTsp1.StartHour : tempTsp1.StartHour + 12, tempTsp1.StartMin, 0);
                    TimeSpan tempTs1E = new TimeSpan(tempTsp1.EndAMPM.ToString() == "AM" ? tempTsp1.EndHour :
                                                    tempTsp1.EndHour == 12 ? tempTsp1.EndHour : tempTsp1.EndHour + 12, tempTsp1.EndMin, 0);
                    TimeSpan tempTs2S = new TimeSpan(tempTsp2.StartAMPM.ToString() == "AM" ? tempTsp2.StartHour :
                                                    tempTsp2.StartHour == 12 ? tempTsp2.StartHour : tempTsp2.StartHour + 12, tempTsp2.StartMin, 0);
                    TimeSpan tempTs2E = new TimeSpan(tempTsp2.EndAMPM.ToString() == "AM" ? tempTsp2.EndHour :
                                                    tempTsp2.EndHour == 12 ? tempTsp2.EndHour : tempTsp2.EndHour + 12, tempTsp2.EndMin, 0);

                    if (ts1S >= tempTs1S && ts1E <= tempTs1E && ts1S >= tempTs2S && ts1E <= tempTs2E)
                    {
                        return 1;
                    }

                    if (ts2S >= tempTs1S && ts2E <= tempTs1E && ts2S >= tempTs2S && ts2E <= tempTs2E)
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            

            return count;
        }
    }
}
