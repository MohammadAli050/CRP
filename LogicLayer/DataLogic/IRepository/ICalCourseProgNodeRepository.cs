using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICalCourseProgNodeRepository
    {
        int Insert(CalCourseProgNode calCourseProgNode);
        bool Update(CalCourseProgNode calCourseProgNode);
        bool Delete(int id);
        CalCourseProgNode GetById(int? id);
        List<CalCourseProgNode> GetAll();
        List<CalCourseProgNode> GetByTreeCalenderDetailId(int treeCalenderDetailId);
        CalCourseProgNode GetByTreeCalDetCourseIdVersionIdNodeCourseIdPriority(int treeMaterId, int calenderDistributionId, int courseId, int versionId, int nodeCourseId, int priority);
        CalCourseProgNode GetByTreeCalDetNodeIdPriority(int treeMasterId, int calenderDistributionId, int nodeId, int priority);
    }
}
