using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.DTO;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IExamMarkPublishAcaCalSectionRelationRepository
    {
        int Insert(ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation);
        bool Update(ExamMarkPublishAcaCalSectionRelation exammarkpublishacacalsectionrelation);
        bool Delete(int Id);
        ExamMarkPublishAcaCalSectionRelation GetById(int? Id);
        ExamMarkPublishAcaCalSectionRelation GetByAcacalSecId(int Id);
        List<ExamMarkPublishAcaCalSectionRelation> GetAll();
        List<AcacalSectionResultPublishDTO> GetByProgramIdAcaCalId(int programId, int acaCalId);
    }
}

