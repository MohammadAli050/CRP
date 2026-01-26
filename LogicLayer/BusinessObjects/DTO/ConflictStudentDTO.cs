using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects.DTO
{
    [Serializable]
    public class ConflictStudentDTO
    {
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string Course { get; set; }
    }
}