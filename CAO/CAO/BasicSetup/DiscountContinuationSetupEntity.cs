using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class DiscountContinuationSetupEntity:BaseEntity
    {
        #region Variables
        private int _acacalid;

        public int Acacalid
        {
            get { return _acacalid; }
            set { _acacalid = value; }
        }
        private int _programid;

        public int Programid
        {
            get { return _programid; }
            set { _programid = value; }
        }
        private int _typedefinitionid;

        public int Typedefinitionid
        {
            get { return _typedefinitionid; }
            set { _typedefinitionid = value; }
        }
        private decimal _mincredits;

        public decimal Mincredits
        {
            get { return _mincredits; }
            set { _mincredits = value; }
        }
        private decimal _maxcredits;

        public decimal Maxcredits
        {
            get { return _maxcredits; }
            set { _maxcredits = value; }
        }
        private decimal _mincgpa;

        public decimal Mincgpa
        {
            get { return _mincgpa; }
            set { _mincgpa = value; }
        }
        //private string _range;
        #endregion

        #region Constructor
        public DiscountContinuationSetupEntity()
            : base()
        {
            _acacalid = 0;
            _programid = 0;
            _typedefinitionid = 0;
            _mincredits = 0M;
            _maxcredits = 0M;
            _mincgpa = 0M;
        }
        #endregion        

    }
}
