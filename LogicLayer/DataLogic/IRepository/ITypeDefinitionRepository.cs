using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;

namespace LogicLayer.DataLogic.IRepository
{
    public interface ITypeDefinitionRepository
    {
        int Insert(TypeDefinition typeDefinition);
        bool Update(TypeDefinition typeDefinition);
        bool Delete(int id);
        TypeDefinition GetById(int id);
        List<TypeDefinition> GetAll();
        List<TypeDefinition> GetAll(string type);
    }
}
