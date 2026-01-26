using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public abstract class Base
    {
        #region Variables
        private int _id;
        private int _creatorID;
        private UIUMSUser _creator;
        private DateTime _createdDate;
        private int _modifierID;
        private UIUMSUser _modifier;
        private DateTime _modifiedDate; 
        #endregion

        #region Constants
        protected const string ID_PA = "@IDparam";

        protected const string CREATORID = "CreatedBy";
        protected const string CREATORID_PA = "@CreatorIDParam";

        protected const string CREATEDDATE = "CreatedDate";
        protected const string CREATEDDATE_PA = "@CreatedDateParam";

        protected const string MODIFIERID = "ModifiedBy";
        protected const string MODIFIERID_PA = "@ModifierIDParam";

        protected const string MOIDFIEDDATE = "ModifiedDate";
        protected const string MOIDFIEDDATE_PA = "@ModifiedDateParam";

        protected const string BASECOLUMNS = "[CreatedBy], "
                                           + "[CreatedDate], "
                                           + "[ModifiedBy], "
                                           + "[ModifiedDate] ";

        protected const string BASEMUSTCOLUMNS = "[CreatedBy], "
                                           + "[CreatedDate] ";

        //protected const string BASEUPDATEcOLUMNS = "ModifiedBy, "
        //                                        + "ModifiedDate";

        #endregion

        #region Constructor
        public Base()
        {
            _id = 0;
            _creatorID = 0;
            _creator = null;
            _createdDate = DateTime.MinValue;
            _modifierID = 0;
            _modifier = null;
            _modifiedDate = DateTime.MinValue;
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected SqlParameter IDParam
        {
            get
            {
                SqlParameter iDParam = new SqlParameter();
                iDParam.ParameterName = ID_PA;

                iDParam.Value = _id;

                return iDParam;
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
        protected SqlParameter CreatorIDParam
        {
            get
            {
                SqlParameter creatorIDParam = new SqlParameter();
                creatorIDParam.ParameterName = CREATORID_PA;

                creatorIDParam.Value = _creatorID;

                return creatorIDParam;
            }
        }
        /// <summary>
        /// Record creator object
        /// </summary>
        public UIUMSUser Creator
        {
            get
            {
                if (_creator == null)
                {
                    if (_creatorID > 0)
                    {
                        _creator = UIUMSUser.Get(_creatorID, false);
                    }
                }
                return _creator;
            }
        }

        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        protected SqlParameter CreatedDateParam
        {
            get
            {
                SqlParameter createdDateParam = new SqlParameter();
                createdDateParam.ParameterName = CREATEDDATE_PA;

                createdDateParam.Value = _createdDate;

                return createdDateParam;
            }
        }

        /// <summary>
        /// Primary key/ID of the user who last modified a record
        /// </summary>
        public int ModifierID
        {
            get { return _modifierID; }
            set { _modifierID = value; }
        }
        protected SqlParameter ModifierIDParam
        {
            get
            {
                SqlParameter modifierIDParam = new SqlParameter();
                modifierIDParam.ParameterName = MODIFIERID_PA;

                if (_modifierID == 0)
                {
                    modifierIDParam.Value = DBNull.Value;
                }
                else
                {
                    modifierIDParam.Value = _modifierID;
                }

                return modifierIDParam;
            }
        }
        /// <summary>
        /// Record last updater object
        /// </summary>
        public UIUMSUser Modifier
        {
            get
            {
                if (_modifier == null)
                {
                    if (_modifierID > 0)
                    {
                        _modifier = UIUMSUser.Get(_modifierID, false);
                    }
                }
                return _modifier;
            }
        }

        /// <summary>
        /// Date of modification
        /// </summary>
        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        protected SqlParameter ModifiedDateParam
        {
            get
            {
                SqlParameter modifiedDateParam = new SqlParameter();
                modifiedDateParam.ParameterName = MOIDFIEDDATE_PA;

                if (_modifiedDate == DateTime.MinValue)
                {
                    modifiedDateParam.Value = DBNull.Value;
                }
                else
                {
                    modifiedDateParam.Value = _modifiedDate;
                }

                return modifiedDateParam;
            }
        }
        #endregion

    }
}
