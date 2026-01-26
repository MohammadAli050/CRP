using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IPersonPreviousExamRepository
    {
        int Insert(PersonPreviousExam candidateexam);
        bool Update(PersonPreviousExam candidateexam);
        bool Delete(int id);
        PersonPreviousExam GetById(int id);
        List<PersonPreviousExam> GetAll();
        List<PersonPreviousExam> GetAllByPersonId(int id);
    }
}
