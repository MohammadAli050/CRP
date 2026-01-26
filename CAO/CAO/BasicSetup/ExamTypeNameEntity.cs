using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class ExamTypeNameEntity : BaseEntity
    {
        #region DBColumns
        //ExamTypeNameID int 
        //TypeDefinitionID int 
        //Name varchar(150) 
        //TotalAllottedMarks int 
        //Default bit  
        #endregion

        #region Variables
        private int _typeDefinitionID;
        private string _name;
        private int _totalAllottedMarks;
        private bool _default;
        #endregion

        #region Properties
        public int TypeDefinitionID
        {
            get { return _typeDefinitionID; }
            set { _typeDefinitionID = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int TotalAllottedMarks
        {
            get { return _totalAllottedMarks; }
            set { _totalAllottedMarks = value; }
        }

        public bool Default
        {
            get { return _default; }
            set { _default = value; }
        }
        #endregion

        #region Constructor
        public ExamTypeNameEntity()
        {
            TypeDefinitionID = 0;
            Name = string.Empty;
            TotalAllottedMarks = 0;
            Default = true;
        }
        #endregion
    }
}
