using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMarkMasterRepository
    {
        int Insert(ExamMarkMaster exammarkmaster);
        bool Update(ExamMarkMaster exammarkmaster);
        bool Delete(int ExamMarkMasterId);
        ExamMarkMaster GetById(int? ExamMarkMasterId);
        List<ExamMarkMaster> GetAll();
        ExamMarkMaster GetByAcaCalIdAcaCalSectionIdExamTemplateItemId(int acaCalId, int acaCalsectionId, int examTemplateBasicItemId);
    }
}

