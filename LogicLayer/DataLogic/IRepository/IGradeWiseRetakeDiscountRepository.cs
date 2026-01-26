using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface IGradeWiseRetakeDiscountRepository
    {
        int Insert(GradeWiseRetakeDiscount gradewiseretakediscount);
        bool Update(GradeWiseRetakeDiscount gradewiseretakediscount);
        bool Delete(int GradeWiseRetakeDiscountId);
        GradeWiseRetakeDiscount GetById(int GradeWiseRetakeDiscountId);
        List<GradeWiseRetakeDiscount> GetAll();

        List<GradeWiseRetakeDiscount> GetAllBy(int? programId, int sessionId);
    }
}

