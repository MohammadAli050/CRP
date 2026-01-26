using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IShareBatchInSectionRepository
    {
        int Insert(ShareBatchInSection sharebatchinsection);
        bool Update(ShareBatchInSection sharebatchinsection);
        bool Delete(int AcademicCalenderSectionId);
        ShareBatchInSection GetById(int AcademicCalenderSectionId);
        List<ShareBatchInSection> GetAll();

        bool DeleteByAcademicCalenderSectionId(int id);
    }
}

