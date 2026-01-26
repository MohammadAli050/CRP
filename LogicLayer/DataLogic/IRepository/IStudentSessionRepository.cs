using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentSessionRepository
    {
        int Insert(StudentSession studentsession);
        bool Update(StudentSession studentsession);
        bool Delete(int Id);
        StudentSession GetById(int? Id); 
        List<StudentSession> GetAll();
    }
}

