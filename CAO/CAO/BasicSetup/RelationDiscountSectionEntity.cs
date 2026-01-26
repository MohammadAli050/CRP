using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class RelationDiscountSectionEntity:BaseEntity
    {
        #region variables and properties

        private int _relationBetweenDiscountSectionTypeID;

        public int RelationBetweenDiscountSectionTypeID
        {
            get { return _relationBetweenDiscountSectionTypeID; }
            set { _relationBetweenDiscountSectionTypeID = value; }
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
        private int _typeDefID;

        public int TypeDefID
        {
            get { return _typeDefID; }
            set { _typeDefID = value; }
        }

        #endregion

        #region constructor
        public RelationDiscountSectionEntity()
            : base()
        {
            _acaCalID = 0;
            _programID = 0;
            _relationBetweenDiscountSectionTypeID = 0;
            _typeDefID = 0;
            _typeDefDiscountID = 0;
        }
        #endregion
    }
}
