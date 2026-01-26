using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class OperatorEntity:BaseEntity
    {
        #region Variables
        private int _operatorID;
        private string _name;
        #endregion

        #region Constructor
        public OperatorEntity()
            : base()
        {
            _name = string.Empty;
        }
        #endregion

        #region Properties
        public int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion
    }
}
