using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    [Serializable]
    public class RegExpressionUsingProgAcaCal
    {
        private string progId;
        public string ProgId
        {
            get { return progId; }
            set { progId = value; }
        }

        private string progCode;
        public string ProgCode
        {
            get { return progCode; }
            set { progCode = value; }
        }

        private string batchId;
        public string BatchId
        {
            get { return batchId; }
            set { batchId = value; }
        }

        private string batchNo;
        public string BatchNo
        {
            get { return batchNo; }
            set { batchNo = value; }
        }

        private DateTime accessValidFrom;
        public DateTime AccessValidFrom
        {
            get { return accessValidFrom; }
            set { accessValidFrom = value; }
        }

        private DateTime accessValidTo;
        public DateTime AccessValidTo
        {
            get { return accessValidTo; }
            set { accessValidTo = value; }
        }
    }
}
