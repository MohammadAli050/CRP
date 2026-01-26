using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public abstract class BaseEntity
    {
        #region Variables
        private int _id;
        private int _creatorID;
        private DateTime _createdDate;
        private int _modifierID;
        private DateTime _modifiedDate;
        #endregion

        #region Properties
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Primary key/ID of the user who creates a record
        /// </summary>
        public int CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }        
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        /// <summary>
        /// Primary key/ID of the user who last modified a record
        /// </summary>
        public int ModifierID
        {
            get { return _modifierID; }
            set { _modifierID = value; }
        }

        /// <summary>
        /// Date of modification
        /// </summary>
        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        #endregion

        #region Constructor
        public BaseEntity()
        {
            _id = 0;
            _creatorID = 0;
            _createdDate = DateTime.MinValue;
            _modifierID = 0;
            _modifiedDate = DateTime.MinValue;
        } 
        #endregion
    }
}
