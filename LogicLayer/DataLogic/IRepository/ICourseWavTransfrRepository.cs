using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseWavTransfrRepository
    {
        int Insert(CourseWavTransfr courseWavTransfr);
        bool Update(CourseWavTransfr courseWavTransfr);
        bool Delete(int id);
        CourseWavTransfr GetById(int? id);
        List<CourseWavTransfr> GetAll();
        List<CourseWavTransfr> GetUniqueAll();
        List<CourseWavTransfr> GetByStudentId(int studentId);
    }
}
