using LogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class CourseGroup
    {
        public int CourseGroupId { get; set; }
        public string GroupName { get; set; }
        public int TypeDefinitionId { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public TypeDefinition Type
        {
            get
            {
                TypeDefinition td = TypeDefinitionManager.GetById(TypeDefinitionId);
                return td;
            }
        }
    }
}

