using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class RelationDiscountRetakeEntity:BaseEntity
    {
        #region variables and properties

        private int _relationBetweenDiscountRetakeID;

        public int RelationBetweenDiscountRetakeID
        {
            get { return _relationBetweenDiscountRetakeID; }
            set { _relationBetweenDiscountRetakeID = value; }
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
        private int _typeDefDiscountID;

        public int TypeDefDiscountID
        {
            get { return _typeDefDiscountID; }
            set { _typeDefDiscountID = value; }
        }
        private bool _retakeEqualsToZero;

        public bool RetakeEqualsToZero
        {
            get { return _retakeEqualsToZero; }
            set { _retakeEqualsToZero = value; }
        }
        private bool _retakeGreaterThanZero;

        public bool RetakeGreaterThanZero
        {
            get { return _retakeGreaterThanZero; }
            set { _retakeGreaterThanZero = value; }
        }

        

        #endregion

        #region constructor
        public RelationDiscountRetakeEntity()
            : base()
        {
            _acaCalID = 0;
            _programID = 0;
            _relationBetweenDiscountRetakeID = 0;
            _typeDefDiscountID = 0;
            _retakeEqualsToZero = false;
            _retakeGreaterThanZero = false;
        }
        #endregion
    }
}
