using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data.SqlClient;

namespace DataAccess
{
    public class TypeDefinitionDAO
    {
        #region Column
        private const string TYPEDEFINITIONID = "TypeDefinitionID";
        private const string TYPEDEFINITIONID_PA = "@TypeDefinitionID";

        private const string TYPE = "Type";
        private const string TYPE_PA = "@Type";

        private const string DEFINITION = "Definition";
        private const string DEFINITION_PA = "@Definition";
        #endregion

        #region ALLCOLUMNS
        private const string ALLCOLUMNS = TYPEDEFINITIONID + ", "
                                          + TYPE + ", "
                                          + DEFINITION + ", ";
        #endregion

        #region TABLE NAME
        private const string TABLENAME = " [TypeDefinition] ";
        #endregion

        #region DELETE
        private const string DELETE = "DELETE FROM" + TABLENAME;
        #endregion 


    
        public static List<TypeDefinitionEntity> GetTypeDef(string course)
        {
          try
            {
                List<TypeDefinitionEntity> TypeDefs = null;
                DAOParameters dParam = new DAOParameters();

                string command = "Select * from " + TABLENAME + " WHERE " + TYPE + " = " + TYPE_PA;

                dParam.AddParameter(TYPE_PA, course);
                List<SqlParameter> sqlParams = Methods.GetSQLParameters(dParam);

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = QueryHandler.ExecuteSelect(command, sqlParams, sqlConn);

                if (theReader != null)
                {
                    TypeDefs = MapTypeDefs(theReader);
                }

                theReader.Close();
                MSSqlConnectionHandler.CloseDbConnection(); //Close DB Connection

                return TypeDefs; 
            }
            catch (Exception ex)
            {
                throw ex;
            }             
        }

        private static List<TypeDefinitionEntity> MapTypeDefs(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TypeDefinitionEntity> TypeDefs = null;

            while (theReader.Read())
            {
                if (TypeDefs == null)
                {
                    TypeDefs = new List<TypeDefinitionEntity>();
                }
                TypeDefinitionEntity TypeDef = TypeDefMapper(nullHandler);
                TypeDefs.Add(TypeDef);
            }
            return TypeDefs;
        }

        private static TypeDefinitionEntity TypeDefMapper(SQLNullHandler nullHandler)
        {
            TypeDefinitionEntity TypeDef = new TypeDefinitionEntity();

            TypeDef.Id = nullHandler.GetInt32(TYPEDEFINITIONID);
            TypeDef.Type = nullHandler.GetString(TYPE);
            TypeDef.Definition = nullHandler.GetString(DEFINITION);

            TypeDef.CreatorID = nullHandler.GetInt32("CreatedBy");
            TypeDef.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            TypeDef.ModifierID = nullHandler.GetInt32("ModifiedBy");
            TypeDef.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return TypeDef;
        }
    }
}
