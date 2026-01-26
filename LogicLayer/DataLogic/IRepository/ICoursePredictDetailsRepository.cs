using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ICoursePredictDetailsRepository
    {
        int Insert(CoursePredictDetails coursepredictdetails);
        bool Update(CoursePredictDetails coursepredictdetails);
        bool Delete(int Id);
        CoursePredictDetails GetById(int Id);
        List<CoursePredictDetails> GetAll();
        List<CoursePredictDetails> GetAll(int acaCalId, int programId);
    }
}

