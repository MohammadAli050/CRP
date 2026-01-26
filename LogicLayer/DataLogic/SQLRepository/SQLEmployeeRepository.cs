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
    public partial class SQLEmployeeRepository : IEmployeeRepository
    {
        Database db = null;

        private string sqlInsert = "EmployeeInsert";
        private string sqlUpdate = "EmployeeUpdate";
        private string sqlDelete = "EmployeeDeleteById";
        private string sqlGetById = "EmployeeGetById";
        private string sqlGetByPersonId = "EmployeeGetByPersonId";
        private string sqlGetAll = "EmployeeGetAll";
        private string sqlGetAllByCode = "EmployeeGetAllByCode";
        private string sqlGetByNameOrCode = "EmployeeGetAllByNameOrCode";
        private string sqlEmployeeVaidateCheck = "Employee_Validation";

        public int Insert(Employee employee)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, employee, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "EmployeeID");

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

        public bool Update(Employee employee)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, employee, isInsert);

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

                db.AddInParameter(cmd, "EmployeeID", DbType.Int32, id);
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

        public Employee GetById(int id)
        {
            Employee _employee = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Employee> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Employee>(sqlGetById, rowMapper);
                _employee = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _employee;
            }

            return _employee;
        }

        public Employee GetByPersonId(int id)
        {
            Employee _employee = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Employee> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Employee>(sqlGetByPersonId, rowMapper);
                _employee = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _employee;
            }

            return _employee;
        }

        public List<Employee> GetAll()
        {
            List<Employee> employeeList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Employee> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Employee>(sqlGetAll, mapper);
                IEnumerable<Employee> collection = accessor.Execute();

                employeeList = collection.ToList();
            }

            catch (Exception ex)
            {
                return employeeList;
            }

            return employeeList;
        }

        public List<Employee> GetAllByCode(string code)
        {
            List<Employee> examList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Employee> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Employee>(sqlGetAllByCode, mapper);
                List<Employee> collection = accessor.Execute(code).ToList();

                examList = collection.ToList();
            }

            catch (Exception ex)
            {
                return examList;
            }

            return examList;
        }

        public List<Employee> GetAllByNameOrCode(string name, string code)
        {
            List<Employee> _employee = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Employee> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Employee>(sqlGetByNameOrCode, rowMapper);
                _employee = accessor.Execute(name, code).ToList();

            }
            catch (Exception ex)
            {
                return _employee;
            }

            return _employee;
        }

        public bool ValidateEmployee(string code)
        {
            List<Employee> teacherList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Employee> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Employee>(sqlEmployeeVaidateCheck, mapper);
                IEnumerable<Employee> collection = accessor.Execute(code);

                teacherList = collection.ToList();

                if (teacherList != null && teacherList.Count > 0)
                    return false;
                else
                    return true;
            }

            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public List<rEmployeeInfo> GetEmployeeInfoByDeptId(int deptId)
        {
            List<rEmployeeInfo> _employee = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<rEmployeeInfo> rowMapper = MapBuilder<rEmployeeInfo>.MapAllProperties()
                .Map(m => m.Code).ToColumn("Code")
                .Map(m => m.DateOfBirth).ToColumn("DOB")
                .Map(m => m.DeptId).ToColumn("DeptID")
                .Map(m => m.Email).ToColumn("Email")
                .Map(m => m.FullName).ToColumn("FullName")
                .Map(m => m.Gender).ToColumn("Gender")
                .Map(m => m.LogInID).ToColumn("LogInID")
                .Map(m => m.Phone).ToColumn("Phone")
                .Map(m => m.PhotoPath).ToColumn("PhotoPath")
                .Build();

                var accessor = db.CreateSprocAccessor<rEmployeeInfo>("RptEmployeeInfoGetByDeptId", rowMapper);
                _employee = accessor.Execute(deptId).ToList();

            }
            catch (Exception ex)
            {
                return _employee;
            }

            return _employee;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Employee employee, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "EmployeeID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "EmployeeID", DbType.Int32, employee.EmployeeID);
            }

            db.AddInParameter(cmd, "Code", DbType.String, employee.Code);
            db.AddInParameter(cmd, "DeptID", DbType.Int32, employee.DeptID);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, employee.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, employee.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, employee.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, employee.ModifiedDate);
            db.AddInParameter(cmd, "SchoolId", DbType.Int32, employee.SchoolId);
            db.AddInParameter(cmd, "Remarks", DbType.String, employee.Remarks);
            db.AddInParameter(cmd, "History", DbType.String, employee.History);
            db.AddInParameter(cmd, "PersonId", DbType.Int32, employee.PersonId);
            db.AddInParameter(cmd, "Program", DbType.String, employee.Program);
            db.AddInParameter(cmd, "DOJ", DbType.DateTime, employee.DOJ);
            db.AddInParameter(cmd, "Status", DbType.Int32, employee.Status);
            db.AddInParameter(cmd, "Designation", DbType.String, employee.Designation);
            db.AddInParameter(cmd, "LibraryCardNo", DbType.String, employee.LibraryCardNo);
            db.AddInParameter(cmd, "MIUEmployeeID", DbType.Int32, employee.MIUEmployeeID);
            db.AddInParameter(cmd, "EmployeeTypeId", DbType.Int32, employee.EmployeeTypeId);
            db.AddInParameter(cmd, "InstituteId", DbType.Int32, employee.InstituteId);

            return db;
        }

        private IRowMapper<Employee> GetMaper()
        {
            IRowMapper<Employee> mapper = MapBuilder<Employee>.MapAllProperties()
            .Map(m => m.EmployeeID).ToColumn("EmployeeID")
            .Map(m => m.Code).ToColumn("Code")
            .Map(m => m.DeptID).ToColumn("DeptID")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.SchoolId).ToColumn("SchoolId")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.History).ToColumn("History")
            .Map(m => m.PersonId).ToColumn("PersonId")
            .Map(m => m.Program).ToColumn("Program")
            .Map(m => m.DOJ).ToColumn("DOJ")
            .Map(m => m.Status).ToColumn("Status")
            .Map(m => m.Designation).ToColumn("Designation")
            .Map(m => m.LibraryCardNo).ToColumn("LibraryCardNo")
            .Map(m => m.MIUEmployeeID).ToColumn("MIUEmployeeID")
            .Map(m=> m.EmployeeTypeId).ToColumn("EmployeeTypeId")
            .Map(m => m.InstituteId).ToColumn("InstituteId")
            .Build();

            return mapper;
        }
        #endregion
    }
}
