using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentOldRepository
    {
        int Insert(StudentOld student_Old);
        bool Update(StudentOld student_Old);
        bool Delete(int id);
        StudentOld GetById(int? id);
        List<StudentOld> GetAll();
    }
}
