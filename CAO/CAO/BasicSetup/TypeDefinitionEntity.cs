using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class TypeDefinitionEntity : BaseEntity
   {
       #region DBColumns
       //ID int 
       //Type nvarchar(
       //Definition nvarchar(250) 
       #endregion

       #region Variables
       private string _type;
       private string _definition; 
       #endregion

       #region Properties
       public string Type
       {
           get { return _type; }
           set { _type = value; }
       }

       public string Definition
       {
           get { return _definition; }
           set { _definition = value; }
       } 
       #endregion

       #region Constructor
       public TypeDefinitionEntity()
       {
           Type = string.Empty;
           Definition = string.Empty;
       } 
       #endregion
    }
}
