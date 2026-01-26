using LogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ISectionDTORepository
    {
        List<SectionDTO> GetAllBy(int acaCalId,   int programId, int courseId, int versionId);
    }
}
