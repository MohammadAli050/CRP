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
    public partial class SQLDepartmentRepository : IDepartmentRepository
    {
        Database db = null;

        private string sqlInsert = "DepartmentInsert";
        private string sqlUpdate = "DepartmentUpdate";
        private string sqlDelete = "DepartmentDeleteById";
        private string sqlGetById = "DepartmentGetById";
        private string sqlGetAll = "DepartmentGetAll";


        public int Insert(Department department)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, department, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "DeptID");

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

        public bool Update(Department department)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, department, isInsert);

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

                db.AddInParameter(cmd, "DeptID", DbType.Int32, id);
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

        public Department GetById(int? id)
        {
            Department _department = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Department> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Department>(sqlGetById, rowMapper);
                _department = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _department;
            }

            return _department;
        }

        public List<Department> GetAll()
        {
            List<Department> departmentList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Department> mapper = GetMaper();

                var accessor = db.CreateSqlStringAccessor<Department>("Select * from Department", mapper);
                IEnumerable<Department> collection = accessor.Execute();

                departmentList = collection.ToList();
            }

            catch (Exception ex)
            {
                return departmentList;
            }

            return departmentList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Department department, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "DeptID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "DeptID", DbType.Int32, department.DeptID);
            }

            db.AddInParameter(cmd, "DeptID", DbType.Int32, department.DeptID);
            db.AddInParameter(cmd, "Code", DbType.String, department.Code);
            db.AddInParameter(cmd, "Name", DbType.String, department.Name);
            db.AddInParameter(cmd, "OpeningDate", DbType.DateTime, department.OpeningDate);
            db.AddInParameter(cmd, "SchoolID", DbType.Int32, department.SchoolID);
            db.AddInParameter(cmd, "DetailedName", DbType.String, department.DetailedName);
            db.AddInParameter(cmd, "ClosingDate", DbType.DateTime, department.ClosingDate);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, department.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, department.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, department.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, department.ModifiedDate);

            return db;
        }

        private IRowMapper<Department> GetMaper()
        {
            IRowMapper<Department> mapper = MapBuilder<Department>.MapAllProperties()
            .Map(m => m.DeptID).ToColumn("DeptID")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.Name).ToColumn("Name")
            .Map(m => m.OpeningDate).ToColumn("OpeningDate")
            .DoNotMap(m => m.SchoolID)
            .Map(m => m.DetailedName).ToColumn("DetailedName")
            .Map(m => m.ClosingDate).ToColumn("ClosingDate")
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
