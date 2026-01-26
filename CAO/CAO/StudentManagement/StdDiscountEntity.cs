using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace Common
{
    [Serializable]
    public class StdDiscountEntity : BaseEntity
    {
        #region DBColumns
        //[ID] [int] IDENTITY(1,1) NOT NULL,
        //[AdmissionID] [int] NOT NULL,
        //[TypeDefID] [int] NOT NULL,
        //[TypePercentage] [decimal](18, 0) NULL,
        //[CreatedBy] [int] NOT NULL,
        //[CreatedDate] [datetime] NOT NULL,
        //[ModifiedBy] [int] NULL,
        //[ModifiedDate] [datetime] NULL,
        #endregion

        #region Variables
        private int _admID;
        private int _typeDefID;
        private decimal _typePercentage;
        private int _effectiveAcaCalID;

        public int EffectiveAcaCalID
        {
            get { return _effectiveAcaCalID; }
            set { _effectiveAcaCalID = value; }
        }
        #endregion

        #region Properties

        public int AdmID
        {
            get { return _admID; }
            set { _admID = value; }
        }

        public int TypeDefID
        {
            get { return _typeDefID; }
            set { _typeDefID = value; }
        }

        public decimal TypePercentage
        {
            get { return _typePercentage; }
            set { _typePercentage = value; }
        }
        #endregion

        #region Constructor
        public StdDiscountEntity()
            : base()
        {
            _admID = 0;
            _typeDefID = 0;
            _typePercentage = -1M;
            _effectiveAcaCalID = 0;
        }
        #endregion
    }
}
