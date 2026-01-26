using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class GradeDetails
    {
        public int GradeId { get; set; }
        public int GradeMasterId { get; set; }
        public string Grade { get; set; }
        public decimal GradePoint { get; set; }
        public decimal MinMarks { get; set; }
        public decimal MaxMarks { get; set; }
        public decimal RetakeDiscount { get; set; }
        public int Sequence { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string GradeWithPoint { get { return Grade + " ( " + GradePoint + " )"; } }

        #region Custom Property

        public string Marks
        {
            get
            {
                if (Grade == "A+")
                {
                    return Convert.ToString(MinMarks) + "% and Above";
                }
                else if (Grade == "F")
                {
                    return "Below 40%";
                }
                else if (Grade == "I" || Grade == "AB")
                {
                    return "-";
                }
                else
                {
                    return Convert.ToString(MinMarks) + "% to <" + Convert.ToString(MaxMarks + 1) + "%";
                }
                
            }
        }

        #endregion


    }
}
