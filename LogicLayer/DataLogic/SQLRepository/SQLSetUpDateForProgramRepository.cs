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
    public partial class SqlSetUpDateForProgramRepository : ISetUpDateForProgramRepository
    {

        Database db = null;

        private string sqlInsert = "SetUpDateForProgramInsert";
        private string sqlUpdate = "SetUpDateForProgramUpdate";
        private string sqlDelete = "SetUpDateForProgramDeleteById";
        private string sqlGetById = "SetUpDateForProgramGetById";
        private string sqlGetAll = "SetUpDateForProgramGetAll";
        private string sqlGetAllByAcaCalProgType = "SetUpDateForProgramGetAllByAcaCalProgType";
        private string sqlGetActiveByProgram = "SetUpDateForProgramGetActiveByProgram";
               
        public int Insert(SetUpDateForProgram setupdateforprogram)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, setupdateforprogram, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "Id");

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

        public bool Update(SetUpDateForProgram setupdateforprogram)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, setupdateforprogram, isInsert);

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

                db.AddInParameter(cmd, "Id", DbType.Int32, id);
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

        public SetUpDateForProgram GetById(int id)
        {
            SetUpDateForProgram _setupdateforprogram = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SetUpDateForProgram> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SetUpDateForProgram>(sqlGetById, rowMapper);
                _setupdateforprogram = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _setupdateforprogram;
            }

            return _setupdateforprogram;
        }

        public List<SetUpDateForProgram> GetAll()
        {
            List<SetUpDateForProgram> setupdateforprogramList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SetUpDateForProgram> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SetUpDateForProgram>(sqlGetAll, mapper);
                IEnumerable<SetUpDateForProgram> collection = accessor.Execute();

                setupdateforprogramList = collection.ToList();
            }

            catch (Exception ex)
            {
                return setupdateforprogramList;
            }

            return setupdateforprogramList;
        }

        public List<SetUpDateForProgram> GetAll(int acaCalId, int programId, int typeId)
        {
            List<SetUpDateForProgram> setupdateforprogramList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SetUpDateForProgram> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SetUpDateForProgram>(sqlGetAllByAcaCalProgType, mapper);
                IEnumerable<SetUpDateForProgram> collection = accessor.Execute(acaCalId, programId, typeId);

                setupdateforprogramList = collection.ToList();
            }

            catch (Exception ex)
            {
                return setupdateforprogramList;
            }

            return setupdateforprogramList;
        }

        public SetUpDateForProgram GetActiveByProgram(int programId)
        {
            SetUpDateForProgram _setupdateforprogram = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<SetUpDateForProgram> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<SetUpDateForProgram>(sqlGetActiveByProgram, rowMapper);
                _setupdateforprogram = accessor.Execute(programId).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _setupdateforprogram;
            }

            return _setupdateforprogram;
        }
       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, SetUpDateForProgram setupdateforprogram, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "Id", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "Id", DbType.Int32, setupdateforprogram.Id);
            }
            	
		    db.AddInParameter(cmd,"ActivityTypeId",DbType.Int32,setupdateforprogram.ActivityTypeId);
		    db.AddInParameter(cmd,"AcaCalId",DbType.Int32,setupdateforprogram.AcaCalId);
		    db.AddInParameter(cmd,"ProgramId",DbType.Int32,setupdateforprogram.ProgramId);
		    db.AddInParameter(cmd,"IsLock",DbType.Boolean,setupdateforprogram.IsLock);
		    db.AddInParameter(cmd, "StartDate", DbType.DateTime, setupdateforprogram.StartDate);
            db.AddInParameter(cmd, "StartTime", DbType.DateTime, setupdateforprogram.StartTime);
            db.AddInParameter(cmd, "EndDate", DbType.DateTime, setupdateforprogram.EndDate);
            db.AddInParameter(cmd, "EndTime", DbType.DateTime, setupdateforprogram.EndTime);
		    db.AddInParameter(cmd,"IsActive",DbType.Boolean,setupdateforprogram.IsActive);
		    db.AddInParameter(cmd,"Attribute1",DbType.String,setupdateforprogram.Attribute1);
		    db.AddInParameter(cmd,"Attribute2",DbType.String,setupdateforprogram.Attribute2);
		    db.AddInParameter(cmd,"Attribute3",DbType.String,setupdateforprogram.Attribute3);
		    db.AddInParameter(cmd,"CreatedBy",DbType.Int32,setupdateforprogram.CreatedBy);
		    db.AddInParameter(cmd,"CreatedDate",DbType.DateTime,setupdateforprogram.CreatedDate);
		    db.AddInParameter(cmd,"ModifiedBy",DbType.Int32,setupdateforprogram.ModifiedBy);
		    db.AddInParameter(cmd,"ModifiedDate",DbType.DateTime,setupdateforprogram.ModifiedDate);
            
            return db;
        }

        private IRowMapper<SetUpDateForProgram> GetMaper()
        {
            IRowMapper<SetUpDateForProgram> mapper = MapBuilder<SetUpDateForProgram>.MapAllProperties()

       	    .Map(m => m.Id).ToColumn("Id")
		    .Map(m => m.ActivityTypeId).ToColumn("ActivityTypeId")
		    .Map(m => m.AcaCalId).ToColumn("AcaCalId")
		    .Map(m => m.ProgramId).ToColumn("ProgramId")
		    .Map(m => m.IsLock).ToColumn("IsLock")
		    .Map(m => m.StartDate).ToColumn("StartDate")
		    .Map(m => m.StartTime).ToColumn("StartTime")
		    .Map(m => m.EndDate).ToColumn("EndDate")
		    .Map(m => m.EndTime).ToColumn("EndTime")
		    .Map(m => m.IsActive).ToColumn("IsActive")
		    .Map(m => m.Attribute1).ToColumn("Attribute1")
		    .Map(m => m.Attribute2).ToColumn("Attribute2")
		    .Map(m => m.Attribute3).ToColumn("Attribute3")
		    .Map(m => m.CreatedBy).ToColumn("CreatedBy")
		    .Map(m => m.CreatedDate).ToColumn("CreatedDate")
		    .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
		    .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .DoNotMap(m => m.TypeName)
            .DoNotMap(m => m.AcaCalName)
            .DoNotMap(m => m.ProgramName)
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

