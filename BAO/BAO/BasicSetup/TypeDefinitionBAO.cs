using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
    public class TypeDefinitionBAO
    {
        public List<TypeDefinitionEntity> GetTypeDefs(string course)
        {
            return TypeDefinitionDAO.GetTypeDef(course);
        }
    }
}
