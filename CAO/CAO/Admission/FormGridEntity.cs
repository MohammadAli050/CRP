using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class FormGridEntity
    {
        #region variables

        private int _id;
        private string _formSL;
        private DateTime _purchaseDate;
        private string _name;
        private string _address;
        private string _phone;
        private string _program;
        private int _paymelSL;
        private string _result;
        private string _testRoll;
        private int _test;
        private DateTime _subitDate;

        private bool _isPassed;
        #endregion

        #region Propertires
        public int ID
        {
            get { return _id; }
            set { _id = value;}

        }

        public string FormSL
        {
            get { return _formSL; }
            set { _formSL = value;}

        }

        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }

        }

        public string Address
        {
            get { return _address; }
            set {_address = value;}

        }

        public string Name
        {
            get { return _name; }
            set { _name = value;}

        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value;}

        }

        public string Program
        {
            get { return _program; }
            set { _program = value;}

        }

        public int PaymentSL
        {
            get { return _paymelSL; }
            set { _paymelSL  = value; }

        }

        public string Result
        {
            get { return _result; }
            set { _result = value; }

        }

        public string TestRoll
        {
            get { return _testRoll; }
            set {_testRoll = value;}

        }

        public int TestToAppear
        {
            get { return _test; }
            set {_test = value;}

        }

        public DateTime SubmissionDate
        {
            get { return _subitDate; }
            set {_subitDate = value;}

        }

        public bool IsPassed
        {
            get { return _isPassed; }
            set { _isPassed = value; }
        }
        
        #endregion

        #region Construsctor
        public FormGridEntity()
        {
            _id = 0;
            _formSL = string.Empty;
            _purchaseDate = DateTime.MinValue;
            _name = string.Empty;
            _address = string.Empty;
            _phone = string.Empty;
            _program = string.Empty;
            _paymelSL = 0;
            _result = string.Empty;
            _testRoll = string.Empty;
            _test = 0;
            _subitDate = DateTime.MinValue;
            _isPassed = false;
        }

        #endregion



    }
}
