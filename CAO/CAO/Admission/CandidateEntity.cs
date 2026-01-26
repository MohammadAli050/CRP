using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Common
{
    [Serializable]
    public class CandidateEntity : BaseEntity
    {
        #region Variables

        private int _candidateId;
        //private int _programId;
        //private int _academiccalenderId;
        private int _candidatePrefix;
        private string _candidateFname;
        //private string _candidateMname;
        //private string _candidateLname;
        //private string _candidateAddress;
        private string _candidatePhone;
        private int _candidateGender;
        //private string _candidateReference;
        //private string _candidatePaymentSerial;
        //private string _level1Exam;
        //private string _level1Institute;
        //private string _level1Result;
        //private int _level1Passing;
        //private string _level2Exam;
        //private string _level2Institute;
        //private string _level2Result;
        //private int _level2Passing;
        //private string _level3Exam;
        //private string _level3Institute;
        //private string _level3Result;
        //private int _level3Passing;
        //private string _level4Exam;
        //private string _level4Institute;
        //private string _level4Result;
        //private int _level4Passing;
        //private DateTime _formPurchasingDate;
        //private DateTime _formSubmissionDate;
        private string _admissionTestRoll;
        //private int _testToAppear;
        private bool _isDiploma;
        
        private bool _isPassed;
        private bool _isPre_English;
        private bool _isPre_Math;
        private string _studentRoll;
        
        #endregion

        #region Properties

        public int CandidateId
        {
            get { return _candidateId; }
            set { _candidateId = value; }
        }

        //public int Programid
        //{
        //    get { return _programId; }
        //    set { _programId = value; }
        //}

        //public int Academiccalenderid
        //{
        //    get { return _academiccalenderId; }
        //    set { _academiccalenderId = value; }
        //}

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

        //public string CandidateMname
        //{
        //    get { return _candidateMname; }
        //    set { _candidateMname = value; }
        //}

        //public string CandidateLname
        //{
        //    get { return _candidateLname; }
        //    set { _candidateLname = value; }
        //}

        //public string CandidateAddress
        //{
        //    get { return _candidateAddress; }
        //    set { _candidateAddress = value; }
        //}

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

        //public string CandidateReference
        //{
        //    get { return _candidateReference; }
        //    set { _candidateReference = value; }
        //}

        //public string CandidatePaymentSerial
        //{
        //    get { return _candidatePaymentSerial; }
        //    set { _candidatePaymentSerial = value; }
        //}

        //public string Level1Exam
        //{
        //    get { return _level1Exam; }
        //    set { _level1Exam = value; }
        //}

        //public string Level1Institute
        //{
        //    get { return _level1Institute; }
        //    set { _level1Institute = value; }
        //}

        //public string Level1Result
        //{
        //    get { return _level1Result; }
        //    set { _level1Result = value; }
        //}

        //public int Level1Passing
        //{
        //    get { return _level1Passing; }
        //    set { _level1Passing = value; }
        //}

        //public string Level2Exam
        //{
        //    get { return _level2Exam; }
        //    set { _level2Exam = value; }
        //}

        //public string Level2Institute
        //{
        //    get { return _level2Institute; }
        //    set { _level2Institute = value; }
        //}

        //public string Level2Result
        //{
        //    get { return _level2Result; }
        //    set { _level2Result = value; }
        //}

        //public int Level2Passing
        //{
        //    get { return _level2Passing; }
        //    set { _level2Passing = value; }
        //}

        //public string Level3Exam
        //{
        //    get { return _level3Exam; }
        //    set { _level3Exam = value; }
        //}

        //public string Level3Institute
        //{
        //    get { return _level3Institute; }
        //    set { _level3Institute = value; }
        //}

        //public string Level3Result
        //{
        //    get { return _level3Result; }
        //    set { _level3Result = value; }
        //}

        //public int Level3Passing
        //{
        //    get { return _level3Passing; }
        //    set { _level3Passing = value; }
        //}

        //public string Level4Exam
        //{
        //    get { return _level4Exam; }
        //    set { _level4Exam = value; }
        //}

        //public string Level4Institute
        //{
        //    get { return _level4Institute; }
        //    set { _level4Institute = value; }
        //}

        //public string Level4Result
        //{
        //    get { return _level4Result; }
        //    set { _level4Result = value; }
        //}

        //public int Level4Passing
        //{
        //    get { return _level4Passing; }
        //    set { _level4Passing = value; }
        //}

        //public DateTime FormPurchasingDate
        //{
        //    get { return _formPurchasingDate; }
        //    set { _formPurchasingDate = value; }
        //}

        //public DateTime FormSubmissionDate
        //{
        //    get { return _formSubmissionDate; }
        //    set { _formSubmissionDate = value; }
        //}

        public string AdmissionTestRoll
        {
            get { return _admissionTestRoll; }
            set { _admissionTestRoll = value; }
        }

        //public int TestToAppear
        //{
        //    get { return _testToAppear; }
        //    set { _testToAppear = value; }
        //}

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

        public bool IsPre_English
        {
            get { return _isPre_English; }
            set { _isPre_English = value; }
        }

        public bool IsPre_Math
        {
            get { return _isPre_Math; }
            set { _isPre_Math = value; }
        }

        public string StudentRoll
        {
            get
            {
                return this._studentRoll;
            }
            set
            {
                this._studentRoll = value;
            }
        }
        #endregion

        #region Constructor
        public CandidateEntity()
            : base()
        {
            _candidateId = 0;
            //_programId = 0;
            //_academiccalenderId = 0;
            _candidatePrefix = 0;
            _candidateFname = "";
            //_candidateMname = "";
            //_candidateLname = "";
            //_candidateAddress = "";
            _candidatePhone = "";
            _candidateGender = 0;
            //_candidateReference = "";
            //_candidatePaymentSerial = "";
            //_level1Exam = "";
            //_level1Institute = "";
            //_level1Result = "";
            //_level1Passing = 0;
            //_level2Exam = "";
            //_level2Institute = "";
            //_level2Result = "";
            //_level2Passing = 0;
            //_level3Exam = "";
            //_level3Institute = "";
            //_level3Result = "";
            //_level3Passing = 0;
            //_level4Exam = "";
            //_level4Institute = "";
            //_level4Result = "";
            //_level4Passing = 0;
            //_formPurchasingDate = DateTime.MinValue;
            //_formSubmissionDate = DateTime.MinValue;
            _admissionTestRoll = "";
            //_testToAppear = 0;            
            _isPassed = false;
            _isPre_English=false;
            _isPre_Math = false;
            _studentRoll = string.Empty;
        }
        #endregion


    }
}