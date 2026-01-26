using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLGradeSheetTemplateRepository : IGradeSheetTemplateRepository
    {
        Database db = null;

        private string sqlInsert = "GradeSheetTemplateInsert";
        private string sqlUpdate = "GradeSheetTemplateUpdate";
        private string sqlDelete = "GradeSheetTemplateDeleteById";
        private string sqlGetById = "GradeSheetTemplateGetById";
        private string sqlGetAll = "GradeSheetTemplateGetAll";
        
        
        public int Insert(GradeSheetTemplate gradeSheetTemplate)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, gradeSheetTemplate, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "GradeSheetTemplateID");

                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out id);
                }
            }
            catch (Exception ex)
            {
                id = 0;
            }

            return id;
        }

        public bool Update(GradeSheetTemplate gradeSheetTemplate)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, gradeSheetTemplate, isInsert);

                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlDelete);

                db.AddInParameter(cmd, "GradeSheetTemplateID", DbType.Int32, id);
                int rowsAffected = db.ExecuteNonQuery(cmd);

                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public GradeSheetTemplate GetById(int? id)
        {
            GradeSheetTemplate _gradeSheetTemplate = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeSheetTemplate> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeSheetTemplate>(sqlGetById, rowMapper);
                _gradeSheetTemplate = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _gradeSheetTemplate;
            }

            return _gradeSheetTemplate;
        }

        public List<GradeSheetTemplate> GetAll()
        {
            List<GradeSheetTemplate> gradeSheetTemplateList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<GradeSheetTemplate> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<GradeSheetTemplate>(sqlGetAll, mapper);
                IEnumerable<GradeSheetTemplate> collection = accessor.Execute();

                gradeSheetTemplateList = collection.ToList();
            }

            catch (Exception ex)
            {
                return gradeSheetTemplateList;
            }

            return gradeSheetTemplateList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, GradeSheetTemplate gradeSheetTemplate, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "GradeSheetTemplateID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "GradeSheetTemplateID", DbType.Int32, gradeSheetTemplate.GradeSheetTemplateID);
            }

            db.AddInParameter(cmd, "Name", DbType.String, gradeSheetTemplate.Name);
            db.AddInParameter(cmd, "Code", DbType.String, gradeSheetTemplate.Code);
            db.AddInParameter(cmd, "Path", DbType.String, gradeSheetTemplate.Path);
            db.AddInParameter(cmd, "IsActive", DbType.Boolean, gradeSheetTemplate.IsActive);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, gradeSheetTemplate.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, gradeSheetTemplate.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, gradeSheetTemplate.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, gradeSheetTemplate.ModifiedDate);
            
            return db;
        }

        private IRowMapper<GradeSheetTemplate> GetMaper()
        {
            IRowMapper<GradeSheetTemplate> mapper = MapBuilder<GradeSheetTemplate>.MapAllProperties()
            .Map(m => m.GradeSheetTemplateID).ToColumn("GradeSheetTemplateID")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.Path).ToColumn("Path")
            .Map(m => m.IsActive).ToColumn("IsActive")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            
            .Build();

            return mapper;
        }
        #endregion
    }
}
