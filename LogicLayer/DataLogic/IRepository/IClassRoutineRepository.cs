using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IClassRoutineRepository
    {
        int Insert(ClassRoutine classRoutine);
        bool Update(ClassRoutine classRoutine);
        bool Delete(int id);
        ClassRoutine GetById(int? id);
        List<ClassRoutine> GetAll();

        List<rClassRoutineByProgram> GetClassRoutineByProgramAndAcaCalId(int programID, int acaCalID);

        List<rClassScheduleForFaculty> GetClassScheduleForFaculty(int facultyId, int programId, int sessionId);
    }
}
