using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class FrmDsnrDetail
    {
        public int FrmDsnrDetail_ID { get; set; }
        public int FrmDsnrMaster_ID { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public int FieldPosition { get; set; }
        public bool IsAdmitField { get; set; }
        public int AdmitPosition { get; set; }
        public string TableColName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; } 
    }
}
