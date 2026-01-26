using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITreeCalendarDetailRepository
    {
        int Insert(TreeCalendarDetail treeCalendarDetail);
        bool Update(TreeCalendarDetail treeCalendarDetail);
        bool Delete(int id);
        TreeCalendarDetail GetById(int? id);
        List<TreeCalendarDetail> GetAll();
        List<TreeCalendarDetail> GetByTreeCalenderMasterId(int treeCalenderMasterId);
    }
}
