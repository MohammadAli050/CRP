using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IStudentDocumentRepository
    {
        int Insert(StudentDocument studentdocument);
        bool Update(StudentDocument studentdocument);
        bool Delete(int Id);
        StudentDocument GetById(int? Id);
        List<StudentDocument> GetAll();
        StudentDocument GetByPersonIdImageType(int personId, int ImageType);
    }
}

