using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IGradeSheetTemplateRepository
    {
        int Insert(GradeSheetTemplate gradeSheetTemplate);
        bool Update(GradeSheetTemplate gradeSheetTemplate);
        bool Delete(int id);
        GradeSheetTemplate GetById(int? id);
        List<GradeSheetTemplate> GetAll();
    }
}
