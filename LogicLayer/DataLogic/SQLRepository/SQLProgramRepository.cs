using LogicLayer.BusinessObjects;
using LogicLayer.DataLogic.IRepository;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DataLogic.SQLRepository
{
    public partial class SQLProgramRepository : IProgramRepository
    {
        Database db = null;

        private string sqlInsert = "ProgramInsert";
        private string sqlUpdate = "ProgramUpdate";
        private string sqlDelete = "ProgramDeleteById";
        private string sqlGetById = "ProgramGetById";
        private string sqlGetAll = "ProgramGetAll";

        public int Insert(Program program)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, program, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "ProgramID");

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

        public bool Update(Program program)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, program, isInsert);

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

                db.AddInParameter(cmd, "ProgramID", DbType.Int32, id);
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

        public Program GetById(int? id)
        {
            Program _program = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Program> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Program>(sqlGetById, rowMapper);
                _program = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _program;
            }

            return _program;
        }

        public List<Program> GetAll()
        {
            List<Program> programList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Program> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Program>(sqlGetAll, mapper);
                IEnumerable<Program> collection = accessor.Execute();

                programList = collection.ToList();
            }

            catch (Exception ex)
            {
                return programList;
            }

            return programList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Program program, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "ProgramID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "ProgramID", DbType.Int32, program.ProgramID);
            }


            db.AddInParameter(cmd, "Code", DbType.String, program.Code);
            db.AddInParameter(cmd, "ShortName", DbType.String, program.ShortName);
            db.AddInParameter(cmd, "TotalCredit", DbType.Decimal, program.TotalCredit);
            db.AddInParameter(cmd, "DeptID", DbType.Int32, program.DeptID);
            db.AddInParameter(cmd, "DetailName", DbType.String, program.DetailName);
            db.AddInParameter(cmd, "ProgramTypeID", DbType.Int32, program.ProgramTypeID);
            db.AddInParameter(cmd, "DegreeName", DbType.String, program.DegreeName);
            db.AddInParameter(cmd, "Duration", DbType.Int32, program.Duration);
            db.AddInParameter(cmd, "CalenderUnitMasterID", DbType.Int32, program.CalenderUnitMasterID);
            db.AddInParameter(cmd, "FacultyId", DbType.Int32, program.FacultyId);
            db.AddInParameter(cmd, "Attribute1", DbType.String, program.Attribute1);
            db.AddInParameter(cmd, "Attribute2", DbType.String, program.Attribute2);
            db.AddInParameter(cmd, "Attribute3", DbType.String, program.Attribute3);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, program.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, program.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, program.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, program.ModifiedDate);
            db.AddInParameter(cmd, "InstituteId", DbType.Int32, program.InstituteId);
            return db;
        }

        private IRowMapper<Program> GetMaper()
        {
            IRowMapper<Program> mapper = MapBuilder<Program>.MapAllProperties()
           .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.ShortName).ToColumn("ShortName")
            .Map(m => m.TotalCredit).ToColumn("TotalCredit")
            .Map(m => m.DeptID).ToColumn("DeptID")
            .Map(m => m.DetailName).ToColumn("DetailName")
            .Map(m => m.ProgramTypeID).ToColumn("ProgramTypeID")
            .Map(m => m.DegreeName).ToColumn("DegreeName")
            .Map(m => m.Duration).ToColumn("Duration")
            .Map(m => m.CalenderUnitMasterID).ToColumn("CalenderUnitMasterID")
            .Map(m => m.FacultyId).ToColumn("FacultyId")
            .Map(m => m.Attribute1).ToColumn("Attribute1")
            .Map(m => m.Attribute2).ToColumn("Attribute2")
            .Map(m => m.Attribute3).ToColumn("Attribute3")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.InstituteId).ToColumn("InstituteId")

            .Build();

            return mapper;
        }
        #endregion
        
        public List<Program> GetByAcaCalSectionID(int AcaCalSectionID)
        {
            List<Program> programList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Program> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Program>("ProgramGetByAcademicCalenderSectionId", mapper);
                IEnumerable<Program> collection = accessor.Execute(AcaCalSectionID);

                programList = collection.ToList();
            }

            catch (Exception ex)
            {
                return programList;
            }

            return programList;
        }
    }
}
