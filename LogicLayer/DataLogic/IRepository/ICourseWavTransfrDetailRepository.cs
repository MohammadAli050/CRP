using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICourseWavTransfrDetailRepository
    {
        int Insert(CourseWavTransfrDetail courseWavTransfrDetail);
        bool Update(CourseWavTransfrDetail courseWavTransfrDetail);
        bool Delete(int id);
        CourseWavTransfrDetail GetById(int? id);
        List<CourseWavTransfrDetail> GetAll();
    }
}
