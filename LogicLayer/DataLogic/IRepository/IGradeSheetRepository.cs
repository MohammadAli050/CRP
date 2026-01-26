using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
  public interface IGradeSheetRepository
    {
      int Insert(GradeSheet gradeSheet);
      bool Update(GradeSheet gradeSheet);
      bool Delete(int id);
      GradeSheet GetById(int? id);
      List<GradeSheet> GetAll();
      List<GradeSheet> GetAllByAcaCalSectionId(int id);
    }
}
