using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentCourseHistoryReplicaRepository
    {
        int Insert(StudentCourseHistoryReplica studentcoursehistoryreplica);
        bool Update(StudentCourseHistoryReplica studentcoursehistoryreplica);
        bool Delete(int ID);
        StudentCourseHistoryReplica GetById(int? ID);
        List<StudentCourseHistoryReplica> GetAll();

        List<StudentCourseHistoryReplica> GetAllByCourseHistoryID(int id);
    }
}

