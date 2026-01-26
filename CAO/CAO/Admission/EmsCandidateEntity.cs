using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
    [Serializable]
    public class EmsCandidateEntity : BaseEntity
    {

        #region DBColumns
        //CANDIDATE_ID int,
        //CANDIDATE_PREFIX int,
        //CANDIDATE_FNAME varchar (50),
        //CANDIDATE_MNAME varchar (50),
        //CANDIDATE_LNAME varchar (50),
        //CANDIDATE_ADDRESS varchar (300),
        //CANDIDATE_PHONE varchar (50),
        //CANDIDATE_GENDER int,
        //CANDIDATE_REFERENCE varchar (100),
        //CANDIDATE_PAYMENT_SERIAL int,
        //LEVEL1_EXAM varchar (10),
        //LEVEL1_INSTITUTE varchar (100),
        //LEVEL1_RESULT varchar (10),
        //LEVEL1_PASSING int,
        //LEVEL2_EXAM varchar (10),
        //LEVEL2_INSTITUTE varchar (100),
        //LEVEL2_RESULT varchar (10),
        //LEVEL2_PASSING int,
        //LEVEL3_EXAM varchar (10),
        //LEVEL3_INSTITUTE varchar (100),
        //LEVEL3_RESULT varchar (10),
        //LEVEL3_PASSING int,
        //FORM_PURCHASING_DATE date,
        //FORM_SUBMISSION_DATE date,
        //REAMRK varchar(200),
        //ADMISSION_TEST_ROLL varchar (10),
        //TEST_TO_APPEAR int,
        //IS_DIPLOMA bit,
        //IS_PASSED bit,j
        //IS_PREMATH BIT,
        //IS_PREENGLISH BIT,
        //IS_ACTIVE BIT,
        //CREATED_BY int,
        //CREATED_DATE datetime,
        //MODIFIED_BY int,
        //MODIFIED_DATE datetime
        #endregion

        #region Variables

        //private int _candidateId;
        private int _candidatePrefix;
        private string _candidateFname;
        private string _candidateMname;
        private string _candidateLname;
        private string _candidateAddress;
        private string _candidateDistrict;
        private string _candidatePhone;
        private int _candidateGender;
        private string _candidateReference;
        private int _candidatePaymentSerial;
        private string _level1Exam;
        private string _level1Institute;
        private string _level1Result;
        private int _level1Passing;
        private string _level2Exam;
        private string _level2Institute;
        private string _level2Result;
        private int _level2Passing;
        private string _level3Exam;
        private string _level3Institute;
        private string _level3Result;
        private int _level3Passing;
        private string _level4Exam;
        private string _level4Institute;
        private string _level4Result;
        private int _level4Passing;
        private DateTime _formPurchasingDate;
        private DateTime _formSubmissionDate;
        private string _remarkForm;
        private string _remarkMRN;
        private string _admissionTestRoll;
        private int _testToAppear;
        private bool _isDiploma;
        private bool _isPassed;
        private bool _isPreMath;
        private bool _isPreEnglish;
        private bool _isActive;
        //private int _createdBy;
        //private DateTime _createdDate;
        //private int _modifiedBy;
        //private DateTime _modifiedDate;
        #endregion

        #region Properties


        public int CandidatePrefix
        {
            get { return _candidatePrefix; }
            set { _candidatePrefix = value; }
        }

        public string CandidateFname
        {
            get { return _candidateFname; }
            set { _candidateFname = value; }
        }

        public string CandidateMname
        {
            get { return _candidateMname; }
            set { _candidateMname = value; }
        }

        public string CandidateLname
        {
            get { return _candidateLname; }
            set { _candidateLname = value; }
        }

        public string CandidateAddress
        {
            get { return _candidateAddress; }
            set { _candidateAddress = value; }
        }

        public string CandidateDistrict
        {
            get { return _candidateDistrict; }
            set { _candidateDistrict = value; }
        }

        public string CandidatePhone
        {
            get { return _candidatePhone; }
            set { _candidatePhone = value; }
        }

        public int CandidateGender
        {
            get { return _candidateGender; }
            set { _candidateGender = value; }
        }

        public string CandidateReference
        {
            get { return _candidateReference; }
            set { _candidateReference = value; }
        }

        public int CandidatePaymentSerial
        {
            get { return _candidatePaymentSerial; }
            set { _candidatePaymentSerial = value; }
        }

        public string Level1Exam
        {
            get { return _level1Exam; }
            set { _level1Exam = value; }
        }

        public string Level1Institute
        {
            get { return _level1Institute; }
            set { _level1Institute = value; }
        }

        public string Level1Result
        {
            get { return _level1Result; }
            set { _level1Result = value; }
        }

        public int Level1Passing
        {
            get { return _level1Passing; }
            set { _level1Passing = value; }
        }

        public string Level2Exam
        {
            get { return _level2Exam; }
            set { _level2Exam = value; }
        }

        public string Level2Institute
        {
            get { return _level2Institute; }
            set { _level2Institute = value; }
        }

        public string Level2Result
        {
            get { return _level2Result; }
            set { _level2Result = value; }
        }

        public int Level2Passing
        {
            get { return _level2Passing; }
            set { _level2Passing = value; }
        }

        public string Level3Exam
        {
            get { return _level3Exam; }
            set { _level3Exam = value; }
        }

        public string Level3Institute
        {
            get { return _level3Institute; }
            set { _level3Institute = value; }
        }

        public string Level3Result
        {
            get { return _level3Result; }
            set { _level3Result = value; }
        }

        public int Level3Passing
        {
            get { return _level3Passing; }
            set { _level3Passing = value; }
        }

        public string Level4Exam
        {
            get { return _level4Exam; }
            set { _level4Exam = value; }
        }

        public string Level4Institute
        {
            get { return _level4Institute; }
            set { _level4Institute = value; }
        }

        public string Level4Result
        {
            get { return _level4Result; }
            set { _level4Result = value; }
        }

        public int Level4Passing
        {
            get { return _level4Passing; }
            set { _level4Passing = value; }
        }


        public DateTime FormPurchasingDate
        {
            get { return _formPurchasingDate; }
            set { _formPurchasingDate = value; }
        }

        public DateTime FormSubmissionDate
        {
            get { return _formSubmissionDate; }
            set { _formSubmissionDate = value; }
        }

        public string RemarkForm
        {
            get { return _remarkForm; }
            set { _remarkForm = value; }

        }

        public string RemarkMRN
        {
            get { return _remarkMRN; }
            set { _remarkMRN = value; }

        }
        public string AdmissionTestRoll
        {
            get { return _admissionTestRoll; }
            set { _admissionTestRoll = value; }
        }

        public int TestToAppear
        {
            get { return _testToAppear; }
            set { _testToAppear = value; }
        }

        public bool IsDiploma
        {
            get { return _isDiploma; }
            set { _isDiploma = value; }
        }

        public bool IsPassed
        {
            get { return _isPassed; }
            set { _isPassed = value; }
        }

        public bool IsPreMath
        {
            get { return _isPreMath; }
            set { _isPreMath = value; }
        }

        public bool IsPrenglish
        {
            get { return _isPreEnglish; }
            set { _isPreEnglish = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }


        #endregion
        #region Constructor
        public EmsCandidateEntity()
            : base()
        {

            _candidatePrefix = 0;
            _candidateFname = string.Empty;
            _candidateMname = string.Empty;
            _candidateLname = string.Empty;
            _candidateAddress = string.Empty;
            _candidateDistrict = string.Empty;
            _candidatePhone = string.Empty;
            _candidateGender = 0;
            _candidateReference = string.Empty;
            _candidatePaymentSerial = 0;
            _level1Exam = string.Empty;
            _level1Institute = string.Empty;
            _level1Result = string.Empty;
            _level1Passing = 0;
            _level2Exam = string.Empty;
            _level2Institute = string.Empty;
            _level2Result = string.Empty;
            _level2Passing = 0;
            _level3Exam = string.Empty;
            _level3Institute = string.Empty;
            _level3Result = string.Empty;
            _level3Passing = 0;
            _level4Exam = string.Empty;
            _level4Institute = string.Empty;
            _level4Result = string.Empty;
            _level4Passing = 0;
            _formPurchasingDate = DateTime.MinValue;
            _formSubmissionDate = DateTime.MinValue;
            _remarkForm = string.Empty;
            _remarkMRN = string.Empty;
            _admissionTestRoll = string.Empty;
            _testToAppear = 0;
            _isDiploma = false;
            _isPassed = false;
            _isPreMath = false;
            _isPreEnglish = false;
            _isActive = false;

        }
        #endregion


    }//End of Class
}//End of namesapce