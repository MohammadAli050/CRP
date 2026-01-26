using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class TypeDefinition : Base
    {
        #region DBColumns
        //ID			        int		        Unchecked
        //Type		            nvarchar(250)	Unchecked
        //Definition	varchar(250)	Unchecked   
        //AccountsID int
        //IsCourseSpecificBilling
        //IsLifetimeOnceBilling
        //IsPerAcaCalBilling
        //Priority
        #endregion

        #region Variables

        private string _type;
        private string _definition;
        
        private int _accountsID;
        private bool _isCourseSpecific;
        private bool _isLifetimeOnce;
        private bool _isPerAcaCal;

        private int _priority;

        #endregion

        #region Constants

        private const string TYPE = "Type";
        private const string TYPE_PA = "@Type";
        private const string DEFINITION = "Definition";
        private const string DEFINITION_PA = "@Definition";

        private const string ACCOUNTSID = "AccountsID";
        private const string ACCOUNTSID_PA = "@AccountsID";
        private const string ISCOURSESPECIFIC = "IsCourseSpecific";
        private const string ISCOURSESPECIFIC_PA = "@IsCourseSpecific";
        private const string ISLIFETIMEONCE = "IsLifetimeOnce";
        private const string ISLIFETIMEONCE_PA = "@IsLifetimeOnce";
        private const string ISPERACACAL = "IsPerAcaCal";
        private const string ISPERACACAL_PA = "@IsPerAcaCal";

        private const string PRIORITY = "Priority";
        private const string PRIORITY_PA = "@Priority";

        private const string ALLCOLUMNS = "[TypeDefinitionID], "
                                        + TYPE + ","
                                        + DEFINITION + ","

                                        + ACCOUNTSID + ","
                                        + ISCOURSESPECIFIC + ","
                                        + ISLIFETIMEONCE + ","                                        
                                        + ISPERACACAL + ","

                                        + PRIORITY + ",";

        private const string NOPKCOLUMNS = TYPE + ","
                                        + DEFINITION + ","

                                        + ACCOUNTSID + ","
                                        + ISCOURSESPECIFIC + ","
                                        + ISLIFETIMEONCE + ","
                                        + ISPERACACAL + ","

                                        + PRIORITY + ",";

        private const string TABLENAME = " [TypeDefinition] ";

        private const string SELECT = "SELECT "
                            + ALLCOLUMNS
                            + BASECOLUMNS
                            + "FROM" + TABLENAME;

        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                             + NOPKCOLUMNS
                             + BASECOLUMNS + ")"

                             + "VALUES ( "

                             + TYPE_PA + ", "
                             + DEFINITION_PA + ", "

                             + ACCOUNTSID_PA + ", "
                             + ISCOURSESPECIFIC_PA + ", "
                             + ISLIFETIMEONCE_PA + ", "
                             + ISPERACACAL_PA + ", "
                             + PRIORITY_PA + ", "

                             + CREATORID_PA + ", "
                             + CREATEDDATE_PA + ", "
                             + MODIFIERID_PA + ", "
                             + MOIDFIEDDATE_PA + ")";

        private const string UPDATE = "UPDATE" + TABLENAME
                            + "SET " + TYPE +" = " + TYPE_PA + ", "
                            + DEFINITION + " = " + DEFINITION_PA + ", "

                           + ACCOUNTSID + " = " + ACCOUNTSID_PA + ", "
                           + ISCOURSESPECIFIC + " = " + ISCOURSESPECIFIC_PA + ", "
                           + ISLIFETIMEONCE + " = " + ISLIFETIMEONCE_PA + ", "
                           + ISPERACACAL + " = " + ISPERACACAL_PA + ", "
                           + PRIORITY + " = " + PRIORITY_PA + ", "

                            + "[CreatedBy] = " + CREATORID_PA + ","
                            + "[CreatedDate] = " + CREATEDDATE_PA + ","
                            + "[ModifiedBy] = " + MODIFIERID_PA + ","
                            + "[ModifiedDate] = " + MOIDFIEDDATE_PA;

        private const string DELETE = "DELETE FROM" + TABLENAME;

        #endregion

        #region Constructor

        public TypeDefinition()
            : base()
        {
            _type = string.Empty;
            _definition = string.Empty;

            _accountsID = 0;
            _isCourseSpecific = false;
            _isLifetimeOnce = false;
            _isPerAcaCal = false;
            _priority = 0;
        }

        #endregion

        #region Parameter

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private SqlParameter TypeParam
        {
            get
            {
                SqlParameter typeParam = new SqlParameter();

                typeParam.ParameterName = TYPE_PA;
                typeParam.Value = Type;

                return typeParam;
            }
        }

        public string Definition
        {
            get { return _definition; }
            set { _definition = value; }
        }
        private SqlParameter DefinitionParam
        {
            get
            {
                SqlParameter definitionParam = new SqlParameter();

                definitionParam.ParameterName = DEFINITION_PA;
                definitionParam.Value = Definition;

                return definitionParam;
            }
        }
        //.......................
        public int AccountsID
        {
            get { return _accountsID; }
            set { _accountsID = value; }
        }
        private SqlParameter AccountsIDParam
        {
            get
            {
                SqlParameter Param = new SqlParameter();

                Param.ParameterName = ACCOUNTSID_PA;
                Param.Value = AccountsID;

                return Param;
            }
        }

        public bool IsCourseSpecific
        {
            get { return _isCourseSpecific; }
            set { _isCourseSpecific = value; }
        }
        private SqlParameter IsCourseSpecificParam
        {
            get
            {
                SqlParameter Param = new SqlParameter();

                Param.ParameterName = ISCOURSESPECIFIC_PA;
                Param.Value = IsCourseSpecific;

                return Param;
            }
        }

        public bool IsLifetimeOnce
        {
            get { return _isLifetimeOnce; }
            set { _isLifetimeOnce = value; }
        }
        private SqlParameter IsLifetimeOnceParam
        {
            get
            {
                SqlParameter Param = new SqlParameter();

                Param.ParameterName = ISLIFETIMEONCE_PA;
                Param.Value = IsLifetimeOnce;

                return Param;
            }
        }

        public bool IsPerAcaCal
        {
            get { return _isPerAcaCal; }
            set { _isPerAcaCal = value; }
        }
        private SqlParameter IsPerAcaCalParam
        {
            get
            {
                SqlParameter Param = new SqlParameter();

                Param.ParameterName = ISPERACACAL_PA;
                Param.Value = IsPerAcaCal;

                return Param;
            }
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        private SqlParameter PriorityParam
        {
            get
            {
                SqlParameter Param = new SqlParameter();

                Param.ParameterName = PRIORITY_PA;
                Param.Value = Priority;

                return Param;
            }
        }
        #endregion

        #region Methods

        public static bool HasDuplicateCode(TypeDefinition typeDef)
        {
            if (typeDef == null)
            {
                return TypeDefinition.IsExist(typeDef.Type, typeDef.Definition);
            }
            else
            {
                if (typeDef.Id == 0)
                {
                    if (TypeDefinition.IsExist(typeDef.Type, typeDef.Definition))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (typeDef.Type != typeDef.Type)
                    {
                        return TypeDefinition.IsExist(typeDef.Type, typeDef.Definition);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool IsExist(string type, string disDef)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [Type] = " + TYPE_PA + " and [Definition] = " + DEFINITION_PA;
            SqlParameter[] codeParam;
            codeParam = new SqlParameter[]{ MSSqlConnectionHandler.MSSqlParamGenerator(type, TYPE_PA),
                           MSSqlConnectionHandler.MSSqlParamGenerator(disDef, DEFINITION_PA)};
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, codeParam);

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }

        public static bool IsExist(string type)
        {
            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();

            string command = "SELECT COUNT(*) FROM" + TABLENAME
                            + "WHERE [Type] = " + TYPE_PA ;
            SqlParameter[] codeParam;
            codeParam = new SqlParameter[]{ MSSqlConnectionHandler.MSSqlParamGenerator(type, TYPE_PA)};
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteScalar(command, sqlConn, codeParam);

            MSSqlConnectionHandler.CloseDbConnection();

            return (Convert.ToInt32(ob) > 0);
        }

        public static int Save(TypeDefinition _typeDef)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = string.Empty;
                SqlParameter[] sqlParams = null;
                
                if (_typeDef.Id == 0)
                {
                    command = INSERT;
                    sqlParams = new SqlParameter[] { _typeDef.TypeParam,  
                                                     _typeDef.DefinitionParam,

                                                     _typeDef.AccountsIDParam,
                                                     _typeDef.IsCourseSpecificParam,
                                                     _typeDef.IsLifetimeOnceParam,
                                                     _typeDef.IsPerAcaCalParam,
                                                     _typeDef.PriorityParam,

                                                     _typeDef.CreatorIDParam, 
                                                     _typeDef.CreatedDateParam, 
                                                     _typeDef.ModifierIDParam, 
                                                     _typeDef.ModifiedDateParam };
                }
                else
                {

                    command = UPDATE
                            + " WHERE TypeDefinitionID = " + ID_PA;
                    sqlParams = new SqlParameter[] { _typeDef.TypeParam,  
                                                     _typeDef.DefinitionParam,

                                                     _typeDef.AccountsIDParam,
                                                     _typeDef.IsCourseSpecificParam,
                                                     _typeDef.IsLifetimeOnceParam,
                                                     _typeDef.IsPerAcaCalParam,
                                                     _typeDef.PriorityParam,

                                                     _typeDef.CreatorIDParam, 
                                                     _typeDef.CreatedDateParam, 
                                                     _typeDef.ModifierIDParam, 
                                                     _typeDef.ModifiedDateParam, 
                                                     _typeDef.IDParam };
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static List<TypeDefinition> GetTypes(string type)
        {
            try
            {
                string command = SELECT
                 + "WHERE [Type] Like '%" + type + "%'";

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

                List<TypeDefinition> TypeDef = MapTypeDefs(theReader);
                theReader.Close();

                MSSqlConnectionHandler.CloseDbConnection();

                return TypeDef;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public static List<TypeDefinition> GetTypes(string type, string Defination)
        {
            try
            {
                string command = SELECT
                 + "WHERE [Type] Like '%" + type + "%' and Definition Like '%" + Defination  + "%'";

                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

                List<TypeDefinition> TypeDef = MapTypeDefs(theReader);
                theReader.Close();

                MSSqlConnectionHandler.CloseDbConnection();

                return TypeDef;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TypeDefinition> GetTypes()
        {
            string command = SELECT;

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn);

            List<TypeDefinition> TypeDef = MapTypeDefs(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return TypeDef;
        }

        private static List<TypeDefinition> MapTypeDefs(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<TypeDefinition> TypeDefs = null;

            while (theReader.Read())
            {
                if (TypeDefs == null)
                {
                    TypeDefs = new List<TypeDefinition>();
                }
                TypeDefinition TypeDef = TypeDefMapper(nullHandler);
                TypeDefs.Add(TypeDef);
            }

            return TypeDefs;
        }

        private static TypeDefinition TypeDefMapper(SQLNullHandler nullHandler)
        {
            TypeDefinition TypeDef = new TypeDefinition();

            TypeDef.Id = nullHandler.GetInt32("TypeDefinitionID");
            TypeDef.Type = nullHandler.GetString(TYPE);
            TypeDef.Definition = nullHandler.GetString(DEFINITION);

            TypeDef.AccountsID = nullHandler.GetInt32(ACCOUNTSID);
            TypeDef.IsCourseSpecific = nullHandler.GetBoolean(ISCOURSESPECIFIC);
            TypeDef.IsLifetimeOnce = nullHandler.GetBoolean(ISLIFETIMEONCE);
            TypeDef.IsPerAcaCal = nullHandler.GetBoolean(ISPERACACAL);
            TypeDef.Priority = nullHandler.GetInt32(PRIORITY);

            TypeDef.CreatorID = nullHandler.GetInt32("CreatedBy");
            TypeDef.CreatedDate = nullHandler.GetDateTime("CreatedDate");
            TypeDef.ModifierID = nullHandler.GetInt32("ModifiedBy");
            TypeDef.ModifiedDate = nullHandler.GetDateTime("ModifiedDate");

            return TypeDef;
        }

        public static TypeDefinition GetTypeDef(int id)
        {
            string command = SELECT
                            + "WHERE TypeDefinitionID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(id, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            TypeDefinition TypeDef = MapTypeDef(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return TypeDef;
        }

        private static TypeDefinition MapTypeDef(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            TypeDefinition TypeDef = null;
            if (theReader.Read())
            {
                TypeDef = new TypeDefinition();
                TypeDef = TypeDefMapper(nullHandler);
            }

            return TypeDef;
        }

        public static int Delete(int id)
        {
            try
            {
                int counter = 0;
                SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
                SqlTransaction sqlTran = MSSqlConnectionHandler.StartTransaction();

                string command = DELETE
                                + "WHERE TypeDefinitionID = " + ID_PA;

                SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(id, ID_PA);
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, new SqlParameter[] { iDParam });

                MSSqlConnectionHandler.CommitTransaction();
                MSSqlConnectionHandler.CloseDbConnection();
                return counter;
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        public static bool HasDuplicateCode(AcademicCalender _trimesterInfo)
        {
            if (_trimesterInfo == null)
            {
                return AcademicCalender.IsExist(_trimesterInfo.Code);
            }
            else
            {
                if (_trimesterInfo.Id == 0)
                {
                    if (AcademicCalender.IsExist(_trimesterInfo.Code))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (_trimesterInfo.Code != _trimesterInfo.Code)
                    {
                        return AcademicCalender.IsExist(_trimesterInfo.Code);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        #endregion
    }
}
