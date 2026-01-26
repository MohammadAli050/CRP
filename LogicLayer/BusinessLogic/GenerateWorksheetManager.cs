using LogicLayer.DataLogic.DAFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.BusinessLogic
{
    public class GenerateWorksheetManager
    {
        public static int Insert(int studentId, int treeCalendarMasterID, int treeMasterID, int openCourse, int academicCalenderID, int programID)
        {
            int id = RepositoryManager.GenerateWorksheet_Repository.Insert(studentId, treeCalendarMasterID, treeMasterID, openCourse, academicCalenderID, programID);
            return id;
        }
    }
}