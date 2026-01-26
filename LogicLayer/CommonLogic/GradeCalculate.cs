using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.CommonLogic
{
    class GradeCalculate
    {
        public static GradeDetails GradeRow(int gradeMasterId, decimal mark)
        {
            List<GradeDetails> gradeDetailsList = GradeDetailsManager.GetByGradeMasterId(gradeMasterId);
            foreach (GradeDetails e in gradeDetailsList)
            {
                if (e.MinMarks >= mark && e.MaxMarks <= mark)
                    return e;
            }

            GradeDetails temp = null;
            return temp;
        }
    }
}
