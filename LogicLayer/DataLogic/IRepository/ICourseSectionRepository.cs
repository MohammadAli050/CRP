using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseSectionRepository
    {
        List<CourseSectionByCourseIdDTO> GetSectionByCourseId(int courseId);
    }
}

