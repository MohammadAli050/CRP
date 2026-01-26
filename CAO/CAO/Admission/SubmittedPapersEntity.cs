using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class SubmittedPapersEntity : BaseEntity
    {  
        private int _submittedpapersid;
        public int Submittedpapersid 
        { 
            get { return _submittedpapersid; }
            set { _submittedpapersid = value; } 
        }

        private int _personid;
        public int Personid { get { return _personid; } set { _personid = value; } }

        private int _candidateid;
        public int Candidateid { get { return _candidateid; } set { _candidateid = value; } }

        private bool _ssc_c;
        public bool Ssc_c { get { return _ssc_c; } set { _ssc_c = value; } }

        private bool _ssc_m;
        public bool Ssc_m { get { return _ssc_m; } set { _ssc_m = value; } }

        private bool _ssc_t;
        public bool Ssc_t { get { return _ssc_t; } set { _ssc_t = value; } }

        private bool _hsc_c;
        public bool Hsc_c { get { return _hsc_c; } set { _hsc_c = value; } }

        private bool _hsc_m;
        public bool Hsc_m { get { return _hsc_m; } set { _hsc_m = value; } }

        private bool _hsc_t;
        public bool Hsc_t { get { return _hsc_t; } set { _hsc_t = value; } }

        private bool _bachelor_c;
        public bool Bachelor_c { get { return _bachelor_c; } set { _bachelor_c = value; } }

        private bool _bachelor_m;
        public bool Bachelor_m { get { return _bachelor_m; } set { _bachelor_m = value; } }

        private bool _bachelor_t;
        public bool Bachelor_t { get { return _bachelor_t; } set { _bachelor_t = value; } }

        private bool _masters_c;
        public bool Masters_c { get { return _masters_c; } set { _masters_c = value; } }

        private bool _masters_m;
        public bool Masters_m { get { return _masters_m; } set { _masters_m = value; } }

        private bool _masters_t;
        public bool Masters_t { get { return _masters_t; } set { _masters_t = value; } }

        private bool _photo;
        public bool Photo { get { return _photo; } set { _photo = value; } }

        public SubmittedPapersEntity():base()
        {
            _submittedpapersid = 0;
            _personid=0;
            _candidateid=0;
            _ssc_c=false;
            _ssc_m = false;
            _ssc_t = false;
            _hsc_c = false;
            _hsc_m = false;
            _hsc_t = false;
            _bachelor_c = false;
            _bachelor_m = false;
            _bachelor_t = false;
            _masters_c = false;
            _masters_m = false;
            _masters_t = false;
            _photo = false;
        }
    }
}
