using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamSetItemRepository
    {
        int Insert(ExamSetItem examSetItemp);
        bool Update(ExamSetItem examSetItemp);
        bool Delete(int TestSetGroupId);
        ExamSetItem GetById(int? itemId);
        List<ExamSetItem> GetAll();
        ExamSetItem GetByExamExamSetId(int examSetId, int examId);
        List<ExamSetItemDTO> GetAllExamSetItem();
    }
}
