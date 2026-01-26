using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class TreeMaster
    {
        public int TreeMasterID { get; set; }
        public int ProgramID { get; set; }
        public int RootNodeID { get; set; }
        public Nullable<int> StartTrimesterID { get; set; }
        public Nullable<decimal> RequiredUnits { get; set; }
        public Nullable<decimal> PassingGPA { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        #region Custom Property
        public string Node_Name
        {
            get
            {
                Node node = NodeManager.GetById(RootNodeID);
                return node.Name;
            }
        }
        #endregion
    }
}
