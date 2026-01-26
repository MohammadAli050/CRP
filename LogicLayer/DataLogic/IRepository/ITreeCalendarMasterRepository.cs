using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITreeCalendarMasterRepository
    {
        int Insert(TreeCalendarMaster treeCalendarMaster);
        bool Update(TreeCalendarMaster treeCalendarMaster);
        bool Delete(int id);
        TreeCalendarMaster GetById(int? id);
        List<TreeCalendarMaster> GetAll();
        List<TreeCalendarMaster> GetAllByTreeMasterID(int treeMasterID);

        TreeCalendarMaster GetByTreeCalenderNameTreeMasterId(string treeCalenderName, int treeMasterId);
    }
}
