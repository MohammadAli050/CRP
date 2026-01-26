using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class FeeSetup
    {
        public int FeeSetupID { get; set; }
        public int AcaCalID { get; set; }
        public int ProgramID { get; set; }
        public int BatchID { get; set; }
        public int TypeDefID { get; set; }
        public decimal Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public AcademicCalender Session
        {
            get
            {
                return AcademicCalenderManager.GetById(AcaCalID);
            }
        }

        public Program Program
        {
            get
            {
                return ProgramManager.GetById(ProgramID);
            }
        }

        public TypeDefinition Type
        {
            get
            {
                return TypeDefinitionManager.GetById(TypeDefID);
            }
        }

    }
}

