using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentCountProgramBatchWise
    {
        public int BatchId { get; set; }
        public int BatchNO { get; set; }
        public int StudentCount { get; set; }

        #region Custom  Property

        public string BatchWiseCount
        {
            get
            {
                return BatchNO + " " + StudentCount;
            }
        }

        #endregion

    }
}
