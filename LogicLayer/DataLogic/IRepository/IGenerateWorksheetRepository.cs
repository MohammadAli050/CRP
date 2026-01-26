using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IGenerateWorksheetRepository
    {
        int Insert(int studentId, int treeCalendarMasterID, int treeMasterID, int openCourse, int academicCalenderID, int programID);
    }
}
