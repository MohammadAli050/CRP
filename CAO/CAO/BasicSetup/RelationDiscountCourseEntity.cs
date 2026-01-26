using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class RelationDiscountCourseEntity : BaseEntity
    {
        #region variables and properties

        private int _relationBetweenDiscountCourseTypeID;

        public int RelationBetweenDiscountCourseTypeID
        {
            get { return _relationBetweenDiscountCourseTypeID; }
            set { _relationBetweenDiscountCourseTypeID = value; }
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
        private int _typeDefCourseID;

        public int TypeDefCourseID
        {
            get { return _typeDefCourseID; }
            set { _typeDefCourseID = value; }
        }

        #endregion

        #region constructor
        public RelationDiscountCourseEntity()
            : base()
        {
            _acaCalID = 0;
            _programID = 0;
            _relationBetweenDiscountCourseTypeID = 0;
            _typeDefCourseID = 0;
            _typeDefDiscountID = 0;
        }
        #endregion
    }
}
