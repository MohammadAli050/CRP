using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IShareProgramInSectionRepository
    {
        int Insert(ShareProgramInSection shareprograminsection);
        bool Update(ShareProgramInSection shareprograminsection);
        bool Delete(int AcademicCalenderSectionId, int ProgramId);
        ShareProgramInSection GetById(int AcademicCalenderSectionId, int ProgramId);
        List<ShareProgramInSection> GetAll();       

        bool DeleteByAcademicCalenderSectionId(int academicCalenderSectionId);
    }
}

