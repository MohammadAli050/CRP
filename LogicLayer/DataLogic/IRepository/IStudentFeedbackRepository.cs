using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentFeedbackRepository
    {
        int Insert(StudentFeedback studentfeedback);
        bool Update(StudentFeedback studentfeedback);
        bool Delete(int Id);
        StudentFeedback GetById(int? Id);
        List<StudentFeedback> GetAll();
        List<StudentFeedback> GetAllByStdentId(int StudentId);
    }
}

