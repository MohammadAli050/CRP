using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class FeeSetupEntity:BaseEntity
    {
        private int _feeSetupID;

        public int FeeSetupID
        {
            get { return _feeSetupID; }
            set { _feeSetupID = value; }
        }
        private int _acaCalID;

        public int AcaCalID
        {
            get { return _acaCalID; }
            set { _acaCalID = value; }
        }
        private int _programID;

        public int ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }
        private int _typeDefID;

        public int TypeDefID
        {
            get { return _typeDefID; }
            set { _typeDefID = value; }
        }
        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public FeeSetupEntity()
            : base()
        { 
            _feeSetupID = 0;
              _acaCalID = 0;
              _programID = 0;
              _typeDefID = 0;
              _amount = -1M;

        }
    }
}
