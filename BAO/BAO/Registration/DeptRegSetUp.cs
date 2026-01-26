using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    [Table(Name = "DeptRegSetUp")]
    public class DeptRegSetUp : Base
    {
        #region DBColumns
        //DeptRegSetUpID	    int	        Unchecked
        //ProgramID	            int	        Unchecked
        //LocalCGPA1	        money	    Checked
        //LocalCredit1	        money	    Checked
        //LocalCGPA2	        money	    Checked
        //LocalCredit2	        money	    Checked
        //LocalCGPA3	        money	    Checked
        //LocalCredit3	        money	    Checked
        //ManCGPA1	            money	    Checked
        //ManCredit1	        money	    Checked
        //ManRetakeGradeLimit1	int	        Checked
        //ManCGPA2	            money	    Checked
        //ManCredit2	        money	    Checked
        //ManRetakeGradeLimit2	int	        Checked
        //ManCGPA3	            money	    Checked
        //ManCredit3	        money	    Checked
        //ManRetakeGradeLimit3	int	        Checked
        //MaxCGPA1	            money	    Checked
        //MaxCredit1	        money	    Checked
        //MaxCGPA2	            money	    Checked
        //MaxCredit2	        money	    Checked
        //MaxCGPA3	            money	    Checked
        //MaxCredit3	        money	    Checked        
        //ProjectCGPA	        money	    Checked
        //ProjectCredit	        money	    Checked
        //ThesisCGPA	        money	    Checked
        //ThesisCredit	        money	    Checked
        //MajorCGPA	            money	    Checked
        //MajorCredit	        money	    Checked
        //ProbationLock	        int	        Checked
        //CourseRetakeLimit	    int	        Checked
        //CreatedBy	            int	        Unchecked
        //CreatedDate	        datetime	Unchecked
        //ModifiedBy	        int	        Checked
        //ModifiedDate	        datetime	Checked
        #endregion

        #region Variables

        private int _programID;
        private Program _program;
        private Nullable<decimal> _localCGPA1;
        private Nullable<decimal> _localCredit1;
        private Nullable<decimal> _localCGPA2;
        private Nullable<decimal> _localCredit2;
        private Nullable<decimal> _localCGPA3;
        private Nullable<decimal> _localCredit3;
        private Nullable<decimal> _manCGPA1;
        private Nullable<decimal> _manCredit1;
        private string _manRetakeGradeLimit1;
        private Nullable<decimal> _manCGPA2;
        private Nullable<decimal> _manCredit2;
        private string _manRetakeGradeLimit2;
        private Nullable<decimal> _manCGPA3;
        private Nullable<decimal> _manCredit3;
        private string _manRetakeGradeLimit3;
        private Nullable<decimal> _maxCGPA1;
        private Nullable<decimal> _maxCredit1;
        private Nullable<decimal> _maxCGPA2;
        private Nullable<decimal> _maxCredit2;
        private Nullable<decimal> _maxCGPA3;
        private Nullable<decimal> _maxCredit3;
        private Nullable<int> _courseRetakeLimit;
        private Nullable<decimal> _projCGPA;
        private Nullable<decimal> _projCredit;
        private Nullable<decimal> _thesisCGPA;
        private Nullable<decimal> _thesisCredit;
        private Nullable<decimal> _majorCGPA;
        private Nullable<decimal> _majorCredit;
        private Nullable<int> _probLock;

        private Nullable<decimal> _autoPreRegCGPA1;
        private Nullable<decimal> _autoPreRegCredit1;
        private Nullable<decimal> _autoPreRegCGPA2;
        private Nullable<decimal> _autoPreRegCredit2;
        private Nullable<decimal> _autoPreRegCGPA3;
        private Nullable<decimal> _autoPreRegCredit3;

        #endregion

        #region Constructor
        public DeptRegSetUp()
        {
            _programID = 0;
            _program = null;
            _localCGPA1 = null;
            _localCredit1 = null;
            _localCGPA2 = null;
            _localCredit2 = null;
            _localCGPA3 = null;
            _localCredit3 = null;
            _manCGPA1 = null;
            _manCredit1 = null;
            _manRetakeGradeLimit1 = null;
            _manCGPA2 = null;
            _manCredit2 = null;
            _manRetakeGradeLimit2 = null;
            _manCGPA3 = null;
            _manCredit3 = null;
            _manRetakeGradeLimit3 = null;
            _maxCGPA1 = null;
            _maxCredit1 = null;
            _maxCGPA2 = null;
            _maxCredit2 = null;
            _maxCGPA3 = null;
            _maxCredit3 = null;
            _courseRetakeLimit = null;
            _projCGPA = null;
            _projCredit = null;
            _thesisCGPA = null;
            _thesisCredit = null;
            _majorCGPA = null;
            _majorCredit = null;
            _probLock = null;

            _autoPreRegCGPA1 = null;
            _autoPreRegCredit1 = null;
            _autoPreRegCGPA2 = null;
            _autoPreRegCredit2 = null;
            _autoPreRegCGPA3 = null;
            _autoPreRegCredit3 = null;
        }
        #endregion

        #region Constants
        #region Columns
        private const string DEPTREGSETUPID = "DeptRegSetUpID";

        private const string PROGRAMID = "ProgramID";
        private const string PROGRAMID_PA = "@ProgramID";

        private const string LOCALCGPA1 = "LocalCGPA1";
        private const string LOCALCGPA1_PA = "@LocalCGPA1";

        private const string LOCALCREDIT1 = "LocalCredit1";
        private const string LOCALCREDIT1_PA = "@LocalCredit1";

        private const string LOCALCGPA2 = "LocalCGPA2";
        private const string LOCALCGPA2_PA = "@LocalCGPA2";

        private const string LOCALCREDIT2 = "LocalCredit2";
        private const string LOCALCREDIT2_PA = "@LocalCredit2";

        private const string LOCALCGPA3 = "LocalCGPA3";
        private const string LOCALCGPA3_PA = "@LocalCGPA3";

        private const string LOCALCREDIT3 = "LocalCredit3";
        private const string LOCALCREDIT3_PA = "@LocalCredit3";

        private const string MANCGPA1 = "ManCGPA1";
        private const string MANCGPA1_PA = "@ManCGPA1";

        private const string MANCREDIT1 = "ManCredit1";
        private const string MANCREDIT1_PA = "@ManCredit1";

        private const string MANRETAKEGRADELIMIT1 = "ManRetakeGradeLimit1";
        private const string MANRETAKEGRADELIMIT1_PA = "@ManRetakeGradeLimit1";

        private const string MANCGPA2 = "ManCGPA2";
        private const string MANCGPA2_PA = "@ManCGPA2";

        private const string MANCREDIT2 = "ManCredit2";
        private const string MANCREDIT2_PA = "@ManCredit2";

        private const string MANRETAKEGRADELIMIT2 = "ManRetakeGradeLimit2";
        private const string MANRETAKEGRADELIMIT2_PA = "@ManRetakeGradeLimit2";

        private const string MANCGPA3 = "ManCGPA3";
        private const string MANCGPA3_PA = "@ManCGPA3";

        private const string MANCREDIT3 = "ManCredit3";
        private const string MANCREDIT3_PA = "@ManCredit3";

        private const string MANRETAKEGRADELIMIT3 = "ManRetakeGradeLimit3";
        private const string MANRETAKEGRADELIMIT3_PA = "@ManRetakeGradeLimit3";

        private const string MAXCGPA1 = "MaxCGPA1";
        private const string MAXCGPA1_PA = "@MaxCGPA1";

        private const string MAXCREDIT1 = "MaxCredit1";
        private const string MAXCREDIT1_PA = "@MaxCredit1";

        private const string MAXCGPA2 = "MaxCGPA2";
        private const string MAXCGPA2_PA = "@MaxCGPA2";

        private const string MAXCREDIT2 = "MaxCredit2";
        private const string MAXCREDIT2_PA = "@MaxCredit2";

        private const string MAXCGPA3 = "MaxCGPA3";
        private const string MAXCGPA3_PA = "@MaxCGPA3";

        private const string MAXCREDIT3 = "MaxCredit3";
        private const string MAXCREDIT3_PA = "@MaxCredit3";

        private const string COURSERETAKELIMIT = "CourseRetakeLimit";
        private const string COURSERETAKELIMIT_PA = "@CourseRetakeLimit";

        private const string PROJECTCGPA = "ProjectCGPA";
        private const string PROJECTCGPA_PA = "@ProjectCGPA";

        private const string PROJECTCREDIT = "ProjectCredit";
        private const string PROJECTCREDIT_PA = "@ProjectCredit";

        private const string THESISCGPA = "ThesisCGPA";
        private const string THESISCGPA_PA = "@ThesisCGPA";

        private const string THESISCREDIT = "ThesisCredit";
        private const string THESISCREDIT_PA = "@ThesisCredit";

        private const string MAJORCGPA = "MajorCGPA";
        private const string MAJORCGPA_PA = "@MajorCGPA";

        private const string MAJORCREDIT = "MajorCredit";
        private const string MAJORCREDIT_PA = "@MajorCredit";

        private const string PROBATIONLOCK = "ProbationLock";
        private const string PROBATIONLOCK_PA = "@ProbationLock";


        private const string AUTOPREREGCGPA1 = "AutoPreRegCGPA1";
        private const string AUTOPREREGCGPA1_PA = "@AutoPreRegCGPA1";

        private const string AUTOPREREGCREDIT1 = "AutoPreRegCredit1";
        private const string AUTOPREREGCREDIT1_PA = "@AutoPreRegCredit1";

        private const string AUTOPREREGCGPA2 = "AutoPreRegCGPA2";
        private const string AUTOPREREGCGPA2_PA = "@AutoPreRegCGPA2";

        private const string AUTOPREREGCREDIT2 = "AutoPreRegCredit2";
        private const string AUTOPREREGCREDIT2_PA = "@AutoPreRegCredit2";

        private const string AUTOPREREGCGPA3 = "AutoPreRegCGPA3";
        private const string AUTOPREREGCGPA3_PA = "@AutoPreRegCGPA3";

        private const string AUTOPREREGCREDIT3 = "AutoPreRegCredit3";
        private const string AUTOPREREGCREDIT3_PA = "@AutoPreRegCredit3";

        #endregion

        #region All-Columns
        private const string ALLCOLUMNS = DEPTREGSETUPID + ", "
                                + PROGRAMID + ", "
                                + LOCALCGPA1 + ", "
                                + LOCALCREDIT1 + ", "
                                + LOCALCGPA2 + ", "
                                + LOCALCREDIT2 + ", "
                                + LOCALCGPA3 + ", "
                                + LOCALCREDIT3 + ", "
                                + MANCGPA1 + ", "
                                + MANCREDIT1 + ", "
                                + MANRETAKEGRADELIMIT1 + ", "
                                + MANCGPA2 + ", "
                                + MANCREDIT2 + ", "
                                + MANRETAKEGRADELIMIT2 + ", "
                                + MANCGPA3 + ", "
                                + MANCREDIT3 + ", "
                                + MANRETAKEGRADELIMIT3 + ", "
                                + MAXCGPA1 + ", "
                                + MAXCREDIT1 + ", "
                                + MAXCGPA2 + ", "
                                + MAXCREDIT2 + ", "
                                + MAXCGPA3 + ", "
                                + MAXCREDIT3 + ", "
                                + PROJECTCGPA + ", "
                                + PROJECTCREDIT + ", "
                                + THESISCGPA + ", "
                                + THESISCREDIT + ", "
                                + MAJORCGPA + ", "
                                + MAJORCREDIT + ", "
                                + PROBATIONLOCK + ", "
                                + COURSERETAKELIMIT + ", "

                                + AUTOPREREGCGPA1 + ", "
                                + AUTOPREREGCREDIT1 + ", "
                                + AUTOPREREGCGPA2 + ", "
                                + AUTOPREREGCREDIT2 + ", "
                                + AUTOPREREGCGPA3 + ", "
                                + AUTOPREREGCREDIT3 + ", "
                                ;

        #endregion

        #region NoPK-Columns
        private const string NOPKCOLUMNS = PROGRAMID + ", "
                                + LOCALCGPA1 + ", "
                                + LOCALCREDIT1 + ", "
                                + LOCALCGPA2 + ", "
                                + LOCALCREDIT2 + ", "
                                + LOCALCGPA3 + ", "
                                + LOCALCREDIT3 + ", "
                                + MANCGPA1 + ", "
                                + MANCREDIT1 + ", "
                                + MANRETAKEGRADELIMIT1 + ", "
                                + MANCGPA2 + ", "
                                + MANCREDIT2 + ", "
                                + MANRETAKEGRADELIMIT2 + ", "
                                + MANCGPA3 + ", "
                                + MANCREDIT3 + ", "
                                + MANRETAKEGRADELIMIT3 + ", "
                                + MAXCGPA1 + ", "
                                + MAXCREDIT1 + ", "
                                + MAXCGPA2 + ", "
                                + MAXCREDIT2 + ", "
                                + MAXCGPA3 + ", "
                                + MAXCREDIT3 + ", "
                                + PROJECTCGPA + ", "
                                + PROJECTCREDIT + ", "
                                + THESISCGPA + ", "
                                + THESISCREDIT + ", "
                                + MAJORCGPA + ", "
                                + MAJORCREDIT + ", "
                                + PROBATIONLOCK + ", "
                                + COURSERETAKELIMIT + ", "
                                + AUTOPREREGCGPA1 + ", "
                                + AUTOPREREGCREDIT1 + ", "
                                + AUTOPREREGCGPA2 + ", "
                                + AUTOPREREGCREDIT2 + ", "
                                + AUTOPREREGCGPA3 + ", "
                                + AUTOPREREGCREDIT3 + ", ";
        #endregion

        private const string TABLENAME = " [DeptRegSetUp] ";

        #region Select
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                     + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region Insert
        private const string INSERT = "INSERT INTO" + TABLENAME
                     + "("
                     + NOPKCOLUMNS
                     + BASECOLUMNS
                     + ")"
                     + "VALUES ( " + PROGRAMID_PA + ", "
                                + LOCALCGPA1_PA + ", "
                                + LOCALCREDIT1_PA + ", "
                                + LOCALCGPA2_PA + ", "
                                + LOCALCREDIT2_PA + ", "
                                + LOCALCGPA3_PA + ", "
                                + LOCALCREDIT3_PA + ", "
                                + MANCGPA1_PA + ", "
                                + MANCREDIT1_PA + ", "
                                + MANRETAKEGRADELIMIT1_PA + ", "////888
                                + MANCGPA2_PA + ", "
                                + MANCREDIT2_PA + ", "
                                + MANRETAKEGRADELIMIT2_PA + ", "
                                + MANCGPA3_PA + ", "
                                + MANCREDIT3_PA + ", "
                                + MANRETAKEGRADELIMIT3_PA + ", "
                                + MAXCGPA1_PA + ", "
                                + MAXCREDIT1_PA + ", "
                                + MAXCGPA2_PA + ", "
                                + MAXCREDIT2_PA + ", "
                                + MAXCGPA3_PA + ", "
                                + MAXCREDIT3_PA + ", "
                                + PROJECTCGPA_PA + ", "
                                + PROJECTCREDIT_PA + ", "
                                + THESISCGPA_PA + ", "
                                + THESISCREDIT_PA + ", "
                                + MAJORCGPA_PA + ", "
                                + MAJORCREDIT_PA + ", "
                                + PROBATIONLOCK_PA + ", "
                                + COURSERETAKELIMIT_PA + ", "                               

                                + AUTOPREREGCGPA1_PA + ", "
                                + AUTOPREREGCREDIT1_PA + ", "
                                + AUTOPREREGCGPA2_PA + ", "
                                + AUTOPREREGCREDIT2_PA + ", "
                                + AUTOPREREGCGPA3_PA + ", "
                                + AUTOPREREGCREDIT3_PA + ", "

                                + CREATORID_PA + ", "
                                + CREATEDDATE_PA + ", "
                                + MODIFIERID_PA + ", "
                                + MOIDFIEDDATE_PA + ")"                                
                                ;
        #endregion

        #region Update
        private const string UPDATE = "UPDATE" + TABLENAME + "SET "
                             + PROGRAMID + " = " + PROGRAMID_PA + ", "
                             + LOCALCGPA1 + " = " + LOCALCGPA1_PA + ", "
                             + LOCALCREDIT1 + " = " + LOCALCREDIT1_PA + ", "
                             + LOCALCGPA2 + " = " + LOCALCGPA2_PA + ", "
                             + LOCALCREDIT2 + " = " + LOCALCREDIT2_PA + ", "
                             + LOCALCGPA3 + " = " + LOCALCGPA3_PA + ", "
                             + LOCALCREDIT3 + " = " + LOCALCREDIT3_PA + ", "
                             + MANCGPA1 + " = " + MANCGPA1_PA + ", "
                             + MANCREDIT1 + " = " + MANCREDIT1_PA + ", "
                             + MANRETAKEGRADELIMIT1 + " = " + MANRETAKEGRADELIMIT1_PA + ", "
                             + MANCGPA2 + " = " + MANCGPA2_PA + ", "
                             + MANCREDIT2 + " = " + MANCREDIT2_PA + ", "
                             + MANRETAKEGRADELIMIT2 + " = " + MANRETAKEGRADELIMIT2_PA + ", "
                             + MANCGPA3 + " = " + MANCGPA3_PA + ", "
                             + MANCREDIT3 + " = " + MANCREDIT3_PA + ", "
                             + MANRETAKEGRADELIMIT3 + " = " + MANRETAKEGRADELIMIT3_PA + ", "
                             + MAXCGPA1 + " = " + MAXCGPA1_PA + ", "
                             + MAXCREDIT1 + " = " + MAXCREDIT1_PA + ", "
                             + MAXCGPA2 + " = " + MAXCGPA2_PA + ", "
                             + MAXCREDIT2 + " = " + MAXCREDIT2_PA + ", "
                             + MAXCGPA3 + " = " + MAXCGPA3_PA + ", "
                             + MAXCREDIT3 + " = " + MAXCREDIT3_PA + ", "
                             + PROJECTCGPA + " = " + PROJECTCGPA_PA + ", "
                             + PROJECTCREDIT + " = " + PROJECTCREDIT_PA + ", "
                             + THESISCGPA + " = " + THESISCGPA_PA + ", "
                             + THESISCREDIT + " = " + THESISCREDIT_PA + ", "
                             + MAJORCGPA + " = " + MAJORCGPA_PA + ", "
                             + MAJORCREDIT + " = " + MAJORCREDIT_PA + ", "
                             + PROBATIONLOCK + " = " + PROBATIONLOCK_PA + ", "
                             + COURSERETAKELIMIT + " = " + COURSERETAKELIMIT_PA + ", "
                             + CREATORID + " = " + CREATORID_PA + ", "
                             + CREATEDDATE + " = " + CREATEDDATE_PA + ", "
                             + MODIFIERID + " = " + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE + " = " + MOIDFIEDDATE_PA + ", "

                            + AUTOPREREGCGPA1 + " = " + AUTOPREREGCGPA1_PA + ", "
                            + AUTOPREREGCREDIT1 + " = " + AUTOPREREGCREDIT1_PA + ", "
                            + AUTOPREREGCGPA2 + " = " + AUTOPREREGCGPA2_PA + ", "
                            + AUTOPREREGCREDIT2 + " = " + AUTOPREREGCREDIT2_PA + ", "
                            + AUTOPREREGCGPA3 + " = " + AUTOPREREGCGPA3_PA + ", "
                            + AUTOPREREGCREDIT3 + " = " + AUTOPREREGCREDIT3_PA 
                             ;
        #endregion

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region Properties
        [Column(Name = PROGRAMID)]
        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }
        private SqlParameter ProgramIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PROGRAMID_PA;

                sqlParam.Value = ProgramID;

                return sqlParam;
            }
        }
        public Program OwnerProgram
        {
            get
            {
                if (_program == null)
                {
                    if (this.ProgramID > 0)
                    {
                        _program = Program.GetProgram(this.ProgramID);
                    }
                }
                return _program;
            }
        }

        [Column(Name = LOCALCGPA1)]
        public Nullable<decimal> LocalCGPA1
        {
            get { return _localCGPA1; }
            set { _localCGPA1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter LocalCGPA1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = LOCALCGPA1_PA;
                if (!LocalCGPA1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = LocalCGPA1.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = LOCALCREDIT1)]
        public Nullable<decimal> LocalCredit1
        {
            get { return _localCredit1; }
            set { _localCredit1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter LocalCredit1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = LOCALCREDIT1_PA;
                if (!LocalCredit1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = LocalCredit1.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = LOCALCGPA2)]
        public Nullable<decimal> LocalCGPA2
        {
            get { return _localCGPA2; }
            set { _localCGPA2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter LocalCGPA2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = LOCALCGPA2_PA;
                if (!LocalCGPA2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = LocalCGPA2.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = LOCALCREDIT2)]
        public Nullable<decimal> LocalCredit2
        {
            get { return _localCredit2; }
            set { _localCredit2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter LocalCredit2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = LOCALCREDIT2_PA;
                if (!LocalCredit2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = LocalCredit2.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = LOCALCGPA3)]
        public Nullable<decimal> LocalCGPA3
        {
            get { return _localCGPA3; }
            set { _localCGPA3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter LocalCGPA3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = LOCALCGPA3_PA;
                if (!LocalCGPA3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = LocalCGPA3.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = LOCALCREDIT3)]
        public Nullable<decimal> LocalCredit3
        {
            get { return _localCredit3; }
            set { _localCredit3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter LocalCredit3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = LOCALCREDIT3_PA;
                if (!LocalCredit3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = LocalCredit3.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANCGPA1)]
        public Nullable<decimal> ManCGPA1
        {
            get { return _manCGPA1; }
            set { _manCGPA1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter ManCGPA1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANCGPA1_PA;
                if (!ManCGPA1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManCGPA1.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANCREDIT1)]
        public Nullable<decimal> ManCredit1
        {
            get { return _manCredit1; }
            set { _manCredit1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter ManCredit1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANCREDIT1_PA;
                if (!ManCredit1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManCredit1.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANRETAKEGRADELIMIT1)]
        public string ManRetakeGradeLimit1
        {
            get { return _manRetakeGradeLimit1; }
            set { _manRetakeGradeLimit1 = value; }
        }
        private SqlParameter ManRetakeGradeLimit1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANRETAKEGRADELIMIT1_PA;
                if (ManRetakeGradeLimit1 == null)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManRetakeGradeLimit1;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANCGPA2)]
        public Nullable<decimal> ManCGPA2
        {
            get { return _manCGPA2; }
            set { _manCGPA2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter ManCGPA2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANCGPA2_PA;
                if (!ManCGPA2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManCGPA2.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANCREDIT2)]
        public Nullable<decimal> ManCredit2
        {
            get { return _manCredit2; }
            set { _manCredit2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter ManCredit2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANCREDIT2_PA;
                if (!ManCredit2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManCredit2.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANRETAKEGRADELIMIT2)]
        public string ManRetakeGradeLimit2
        {
            get { return _manRetakeGradeLimit2; }
            set { _manRetakeGradeLimit2 = value; }
        }
        private SqlParameter ManRetakeGradeLimit2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANRETAKEGRADELIMIT2_PA;
                if (ManRetakeGradeLimit2 == null)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManRetakeGradeLimit2;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANCGPA3)]
        public Nullable<decimal> ManCGPA3
        {
            get { return _manCGPA3; }
            set { _manCGPA3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter ManCGPA3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANCGPA3_PA;
                if (!ManCGPA3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManCGPA3.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MANCREDIT3)]
        public Nullable<decimal> ManCredit3
        {
            get { return _manCredit3; }
            set { _manCredit3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter ManCredit3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANCREDIT3_PA;
                if (!ManCredit3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManCredit3.Value;
                }
                return sqlParam;
            }
        }


        [Column(Name = MANRETAKEGRADELIMIT3)]
        public string ManRetakeGradeLimit3
        {
            get { return _manRetakeGradeLimit3; }
            set { _manRetakeGradeLimit3 = value; }
        }
        private SqlParameter ManRetakeGradeLimit3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MANRETAKEGRADELIMIT3_PA;
                if (ManRetakeGradeLimit3 == null)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ManRetakeGradeLimit3;
                }
                return sqlParam;
            }
        }

        [Column(Name = MAXCGPA1)]
        public Nullable<decimal> MaxCGPA1
        {
            get { return _maxCGPA1; }
            set { _maxCGPA1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter MaxCGPA1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAXCGPA1_PA;
                if (!MaxCGPA1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MaxCGPA1.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MAXCREDIT1)]
        public Nullable<decimal> MaxCredit1
        {
            get { return _maxCredit1; }
            set { _maxCredit1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter MaxCredit1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAXCREDIT1_PA;
                if (!MaxCredit1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MaxCredit1.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MAXCGPA2)]
        public Nullable<decimal> MaxCGPA2
        {
            get { return _maxCGPA2; }
            set { _maxCGPA2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter MaxCGPA2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAXCGPA2_PA;
                if (!MaxCGPA2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MaxCGPA2.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MAXCREDIT2)]
        public Nullable<decimal> MaxCredit2
        {
            get { return _maxCredit2; }
            set { _maxCredit2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter MaxCredit2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAXCREDIT2_PA;
                if (!MaxCredit2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MaxCredit2.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MAXCGPA3)]
        public Nullable<decimal> MaxCGPA3
        {
            get { return _maxCGPA3; }
            set { _maxCGPA3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter MaxCGPA3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAXCGPA3_PA;
                if (!MaxCGPA3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MaxCGPA3.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = MAXCREDIT3)]
        public Nullable<decimal> MaxCredit3
        {
            get { return _maxCredit3; }
            set { _maxCredit3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter MaxCredit3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAXCREDIT3_PA;
                if (!MaxCredit3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MaxCredit3.Value;
                }
                return sqlParam;
            }
        }

        [Column(Name = COURSERETAKELIMIT)]
        public Nullable<int> CourseRetakeLimit
        {
            get { return _courseRetakeLimit; }
            set { _courseRetakeLimit = value; }
        }
        private SqlParameter CourseRetakeLimitParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = COURSERETAKELIMIT_PA;
                if (!CourseRetakeLimit.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = CourseRetakeLimit.Value;
                }
                return sqlParam;
            }
        }

        public Nullable<decimal> ProjCGPA
        {
            get { return _projCGPA; }
            set { _projCGPA = Math.Round(value.Value, 2); }
        }
        private SqlParameter ProjCGPAParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PROJECTCGPA_PA;
                if (!ProjCGPA.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ProjCGPA.Value;
                }
                return sqlParam;
            }
        }

        public Nullable<decimal> ProjectCredit
        {
            get { return _projCredit; }
            set { _projCredit = Math.Round(value.Value, 2); }
        }
        private SqlParameter ProjectCreditParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PROJECTCREDIT_PA;
                if (!ProjectCredit.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ProjectCredit.Value;
                }
                return sqlParam;
            }
        }

        public Nullable<decimal> ThesisCGPA
        {
            get { return _thesisCGPA; }
            set { _thesisCGPA = Math.Round(value.Value, 2); }
        }
        private SqlParameter ThesisCGPAParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = THESISCGPA_PA;
                if (!ThesisCGPA.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ThesisCGPA.Value;
                }
                return sqlParam;
            }
        }

        public Nullable<decimal> ThesisCredit
        {
            get { return _thesisCredit; }
            set { _thesisCredit = Math.Round(value.Value, 2); }
        }
        private SqlParameter ThesisCreditParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = THESISCREDIT_PA;
                if (!ThesisCredit.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ThesisCredit.Value;
                }
                return sqlParam;
            }
        }

        public Nullable<decimal> MajorCGPA
        {
            get { return _majorCGPA; }
            set { _majorCGPA = Math.Round(value.Value, 2); }
        }
        private SqlParameter MajorCGPAParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAJORCGPA_PA;
                if (!MajorCGPA.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MajorCGPA.Value;
                }
                return sqlParam;
            }
        }

        public Nullable<decimal> MajorCredit
        {
            get { return _majorCredit; }
            set { _majorCredit = Math.Round(value.Value, 2); }
        }
        private SqlParameter MajorCreditParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MAJORCREDIT_PA;
                if (!MajorCredit.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MajorCredit.Value;
                }
                return sqlParam;
            }
        }

        public Nullable<int> ProbLock
        {
            get { return _probLock; }
            set { _probLock = value; }
        }
        private SqlParameter ProbLockParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PROBATIONLOCK_PA;
                if (!ProbLock.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ProbLock.Value;
                }
                return sqlParam;
            }
        }

        //
          [Column(Name = AUTOPREREGCGPA1)]
        public Nullable<decimal> AutoPreRegCGPA1
        {
            get { return _autoPreRegCGPA1; }
            set { _autoPreRegCGPA1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter AutoPreRegCGPA1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = AUTOPREREGCGPA1_PA;
                if (!AutoPreRegCGPA1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AutoPreRegCGPA1.Value;
                }
                return sqlParam;
            }
        }

          [Column(Name = AUTOPREREGCREDIT1)]
        public Nullable<decimal> AutoPreRegCredit1
        {
            get { return _autoPreRegCredit1; }
            set { _autoPreRegCredit1 = Math.Round(value.Value, 2); }
        }
        private SqlParameter AutoPreRegCredit1Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = AUTOPREREGCREDIT1_PA;
                if (!AutoPreRegCredit1.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AutoPreRegCredit1.Value;
                }
                return sqlParam;
            }
        }

          [Column(Name = AUTOPREREGCGPA2)]
        public Nullable<decimal> AutoPreRegCGPA2
        {
            get { return _autoPreRegCGPA2; }
            set { _autoPreRegCGPA2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter AutoPreRegCGPA2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = AUTOPREREGCGPA2_PA;
                if (!AutoPreRegCGPA2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AutoPreRegCGPA2.Value;
                }
                return sqlParam;
            }
        }

          [Column(Name = AUTOPREREGCREDIT2)]
        public Nullable<decimal> AutoPreRegCredit2
        {
            get { return _autoPreRegCredit2; }
            set { _autoPreRegCredit2 = Math.Round(value.Value, 2); }
        }
        private SqlParameter AutoPreRegCredit2Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = AUTOPREREGCREDIT2_PA;
                if (!AutoPreRegCredit2.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AutoPreRegCredit2.Value;
                }
                return sqlParam;
            }
        }

          [Column(Name = AUTOPREREGCGPA3)]
        public Nullable<decimal> AutoPreRegCGPA3
        {
            get { return _autoPreRegCGPA3; }
            set { _autoPreRegCGPA3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter AutoPreRegCGPA3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = AUTOPREREGCGPA3_PA;
                if (!AutoPreRegCGPA3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AutoPreRegCGPA3.Value;
                }
                return sqlParam;
            }
        }

          [Column(Name = AUTOPREREGCREDIT3)]
        public Nullable<decimal> AutoPreRegCredit3
        {
            get { return _autoPreRegCredit3; }
            set { _autoPreRegCredit3 = Math.Round(value.Value, 2); }
        }
        private SqlParameter AutoPreRegCredit3Param
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = AUTOPREREGCREDIT3_PA;
                if (!AutoPreRegCredit3.HasValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = AutoPreRegCredit3.Value;
                }
                return sqlParam;
            }
        }


        #endregion

        #region Functions
        private static DeptRegSetUp Mapper(SQLNullHandler nullHandler)
        {
            DeptRegSetUp obj = new DeptRegSetUp();

            obj.Id = nullHandler.GetInt32(DEPTREGSETUPID);//0
            obj.ProgramID = nullHandler.GetInt32(PROGRAMID);//4
            obj.LocalCGPA1 = nullHandler.GetDecimal(LOCALCGPA1);//3
            obj.LocalCredit1 = nullHandler.GetDecimal(LOCALCREDIT1);//3
            obj.LocalCGPA2 = nullHandler.GetDecimal(LOCALCGPA2);//3
            obj.LocalCredit2 = nullHandler.GetDecimal(LOCALCREDIT2);//3
            obj.LocalCGPA3 = nullHandler.GetDecimal(LOCALCGPA3);//3
            obj.LocalCredit3 = nullHandler.GetDecimal(LOCALCREDIT3);//3

            obj.ManCGPA1 = nullHandler.GetDecimal(MANCGPA1);//3
            obj.ManCredit1 = nullHandler.GetDecimal(MANCREDIT1);//3
            obj.ManRetakeGradeLimit1 = nullHandler.GetString(MANRETAKEGRADELIMIT1);//3
            obj.ManCGPA2 = nullHandler.GetDecimal(MANCGPA2);//3
            obj.ManCredit2 = nullHandler.GetDecimal(MANCREDIT2);//3
            obj.ManRetakeGradeLimit2 = nullHandler.GetString(MANRETAKEGRADELIMIT2);//3
            obj.ManCGPA3 = nullHandler.GetDecimal(MANCGPA3);//3
            obj.ManCredit3 = nullHandler.GetDecimal(MANCREDIT3);//3
            obj.ManRetakeGradeLimit3 = nullHandler.GetString(MANRETAKEGRADELIMIT3);//3

            obj.MaxCGPA1 = nullHandler.GetDecimal(MAXCGPA1);//3
            obj.MaxCredit1 = nullHandler.GetDecimal(MAXCREDIT1);//3
            obj.MaxCGPA2 = nullHandler.GetDecimal(MAXCGPA2);//3
            obj.MaxCredit2 = nullHandler.GetDecimal(MAXCREDIT2);//3
            obj.MaxCGPA3 = nullHandler.GetDecimal(MAXCGPA3);//3
            obj.MaxCredit3 = nullHandler.GetDecimal(MAXCREDIT3);//3

            obj.ProjCGPA = nullHandler.GetDecimal(PROJECTCGPA);
            obj.ProjectCredit = nullHandler.GetDecimal(PROJECTCREDIT);
            obj.ThesisCGPA = nullHandler.GetDecimal(THESISCGPA);
            obj.ThesisCredit = nullHandler.GetDecimal(THESISCREDIT);
            obj.MajorCGPA = nullHandler.GetDecimal(MAJORCGPA);
            obj.MajorCredit = nullHandler.GetDecimal(MAJORCREDIT);
            obj.ProbLock = nullHandler.GetInt32(PROBATIONLOCK);

            obj.CourseRetakeLimit = nullHandler.GetInt32(COURSERETAKELIMIT);//3

            obj.CreatorID = nullHandler.GetInt32(CREATORID);//5
            obj.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);//6
            obj.ModifierID = nullHandler.GetInt32(MODIFIERID);//7
            obj.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);//8


            obj.AutoPreRegCGPA1 = nullHandler.GetDecimal(AUTOPREREGCGPA1);
            obj.AutoPreRegCredit1 = nullHandler.GetDecimal(AUTOPREREGCREDIT1);
            obj.AutoPreRegCGPA2 = nullHandler.GetDecimal(AUTOPREREGCGPA2);
            obj.AutoPreRegCredit2 = nullHandler.GetDecimal(AUTOPREREGCREDIT2);
            obj.AutoPreRegCGPA3 = nullHandler.GetDecimal(AUTOPREREGCGPA3);
            obj.AutoPreRegCredit3 = nullHandler.GetDecimal(AUTOPREREGCREDIT3);

            return obj;
        }
        private static DeptRegSetUp MapClass(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            DeptRegSetUp obj = null;
            if (theReader.Read())
            {
                obj = new DeptRegSetUp();
                obj = Mapper(nullHandler);
            }

            return obj;
        }
        private static List<DeptRegSetUp> MapCollection(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<DeptRegSetUp> collection = null;

            while (theReader.Read())
            {
                if (collection == null)
                {
                    collection = new List<DeptRegSetUp>();
                }
                DeptRegSetUp obj = Mapper(nullHandler);
                collection.Add(obj);
            }

            return collection;
        }

        public static List<DeptRegSetUp> Gets()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<DeptRegSetUp> collection = MapCollection(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static DeptRegSetUp Get(int iD)
        {
            string command = SELECT
                            + "WHERE [" + DEPTREGSETUPID + "] = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            DeptRegSetUp obj = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return obj;
        }

        public static DeptRegSetUp GetBProgramID(int programID)
        {
            string command = SELECT
                            + "WHERE [" + PROGRAMID + "] = " + PROGRAMID_PA;

            SqlParameter sqlParam = MSSqlConnectionHandler.MSSqlParamGenerator(programID, PROGRAMID_PA);


            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });


            DeptRegSetUp collection = MapClass(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return collection;
        }

        public static int Save(DeptRegSetUp obj)
        {
            try
            {
                int counter = 0;
                using (SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection())
                {
                    using (SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction())
                    {
                        string command = string.Empty;
                        SqlParameter[] sqlParams = null;

                        //if (HasDuplicateCode(obj, academicCalenderID, courseID, versionID, sectionName, sqlConn, sqlTran))
                        //{
                        //    throw new Exception("Duplicate Section Name Not Allowed.");
                        //}

                        if (obj.Id == 0)
                        {
                            #region Insert
                            command = INSERT;
                            sqlParams = new SqlParameter[] {    obj.ProgramIDParam,
                                                                obj.LocalCGPA1Param,
                                                                obj.LocalCredit1Param,
                                                                obj.LocalCGPA2Param,
                                                                obj.LocalCredit2Param,
                                                                obj.LocalCGPA3Param,
                                                                obj.LocalCredit3Param,
                                                                obj.ManCGPA1Param,
                                                                obj.ManCredit1Param,
                                                                obj.ManRetakeGradeLimit1Param,
                                                                obj.ManCGPA2Param,
                                                                obj.ManCredit2Param,
                                                                obj.ManRetakeGradeLimit2Param,
                                                                obj.ManCGPA3Param,
                                                                obj.ManCredit3Param,
                                                                obj.ManRetakeGradeLimit3Param,
                                                                obj.MaxCGPA1Param,
                                                                obj.MaxCredit1Param,
                                                                obj.MaxCGPA2Param,
                                                                obj.MaxCredit2Param,
                                                                obj.MaxCGPA3Param,
                                                                obj.MaxCredit3Param,
                                                                obj.ProjCGPAParam,
                                                                obj.ProjectCreditParam,
                                                                obj.ThesisCGPAParam,
                                                                obj.ThesisCreditParam,
                                                                obj.MajorCGPAParam,
                                                                obj.MajorCreditParam,
                                                                obj.ProbLockParam,
                                                                obj.CourseRetakeLimitParam,
                                                                obj.CreatorIDParam,
                                                                obj.CreatedDateParam,
                                                                obj.ModifierIDParam,
                                                                obj.ModifiedDateParam,
                                                                obj.AutoPreRegCGPA1Param,
                                                                obj.AutoPreRegCGPA2Param,
                                                                obj.AutoPreRegCGPA3Param,
                                                                obj.AutoPreRegCredit1Param,
                                                                obj.AutoPreRegCredit2Param,
                                                                obj.AutoPreRegCredit3Param};//+ MOIDFIEDDATE_PA + ")";//15 
                            #endregion
                        }
                        else
                        {

                            #region Update
                            command = UPDATE
                            + " WHERE [" + DEPTREGSETUPID + "] = " + ID_PA;
                            sqlParams = new SqlParameter[] { obj.ProgramIDParam,
 		                                                        obj.LocalCGPA1Param,
                                                                obj.LocalCredit1Param,
                                                                obj.LocalCGPA2Param,
                                                                obj.LocalCredit2Param,
                                                                obj.LocalCGPA3Param,
                                                                obj.LocalCredit3Param,
                                                                obj.ManCGPA1Param,
                                                                obj.ManCredit1Param,
                                                                obj.ManRetakeGradeLimit1Param,
                                                                obj.ManCGPA2Param,
                                                                obj.ManCredit2Param,
                                                                obj.ManRetakeGradeLimit2Param,
                                                                obj.ManCGPA3Param,
                                                                obj.ManCredit3Param,
                                                                obj.ManRetakeGradeLimit3Param,
                                                                obj.MaxCGPA1Param,
                                                                obj.MaxCredit1Param,
                                                                obj.MaxCGPA2Param,
                                                                obj.MaxCredit2Param,
                                                                obj.MaxCGPA3Param,
                                                                obj.MaxCredit3Param,
                                                                obj.ProjCGPAParam,
                                                                obj.ProjectCreditParam,
                                                                obj.ThesisCGPAParam,
                                                                obj.ThesisCreditParam,
                                                                obj.MajorCGPAParam,
                                                                obj.MajorCreditParam,
                                                                obj.ProbLockParam,
                                                                obj.CourseRetakeLimitParam,
                                                                obj.CreatorIDParam,
                                                                obj.CreatedDateParam,
                                                                obj.ModifierIDParam,
                                                                obj.ModifiedDateParam,
                                                                obj.IDParam,
                                                                obj.AutoPreRegCGPA1Param,
                                                                obj.AutoPreRegCGPA2Param,
                                                                obj.AutoPreRegCGPA3Param,
                                                                obj.AutoPreRegCredit1Param,
                                                                obj.AutoPreRegCredit2Param,
                                                                obj.AutoPreRegCredit3Param };
                            #endregion
                        }
                        counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                        MSSqlConnectionHandler.CommitTransaction();
                        MSSqlConnectionHandler.CloseDbConnection();
                    }
                }
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static int Delete(int iD)
        {
            try
            {
                int counter = 0;
                string command = string.Empty;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                command = DELETE
                                + "WHERE [" + DEPTREGSETUPID + "] = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(iD, ID_PA);

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
    }
}
