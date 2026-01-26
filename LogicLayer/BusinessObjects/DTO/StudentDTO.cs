using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessObjects
{
    [Serializable]
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public int PersonID { get; set; }
        public int CandidateId { get; set; }
        public int ProgramID { get; set; }
        public string Program { get; set; }
        public int BatchId { get; set; }
        public string Batch { get; set; }
        public int TreeMasterID { get; set; }
        public int TreeCalendarMasterID { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDiploma { get; set; }
        public string Remarks { get; set; }
        public int AccountHeadsID { get; set; }
        public bool IsCompleted { get; set; }
        public int CompletedAcaCalId { get; set; }
        public string TranscriptSerial { get; set; }
        public int AcademicCalenderID { get; set; }
        public int AcademicCalenderYear { get; set; }
        public decimal CGPA { get; set; }
        public decimal GPA { get; set; }
    }


    public class SiblingDTO
    {
        public int StudentID { get; set; }
        public int GroupID { get; set; }
        public string Roll { get; set; }
        public string Name { get; set; }
        public int PersonID { get; set; }
        public int CandidateId { get; set; }
        public int ProgramID { get; set; }
        public string Program { get; set; }
        public int BatchId { get; set; }
        public string Batch { get; set; }
        public int TreeMasterID { get; set; }
        public int TreeCalendarMasterID { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDiploma { get; set; }
        public string Remarks { get; set; }
        public int AccountHeadsID { get; set; }
        public bool IsCompleted { get; set; }
        public int CompletedAcaCalId { get; set; }
        public string TranscriptSerial { get; set; }
    }
}
