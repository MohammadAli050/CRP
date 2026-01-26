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
    public partial class SQLDeptRegSetUpRepository : IDeptRegSetUpRepository
    {
        Database db = null;

        private string sqlInsert = "DeptRegSetUpInsert";
        private string sqlUpdate = "DeptRegSetUpUpdate";
        private string sqlDelete = "DeptRegSetUpDeleteById";
        private string sqlGetById = "DeptRegSetUpGetById";
        private string sqlGetAll = "DeptRegSetUpGetAll";
        
        public int Insert(DeptRegSetUp deptRegSetUp)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, deptRegSetUp, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "DeptRegSetUpID");

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

        public bool Update(DeptRegSetUp deptRegSetUp)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, deptRegSetUp, isInsert);

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

                db.AddInParameter(cmd, "DeptRegSetUpID", DbType.Int32, id);
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

        public DeptRegSetUp GetById(int? id)
        {
            DeptRegSetUp _deptRegSetUp = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DeptRegSetUp> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DeptRegSetUp>(sqlGetById, rowMapper);
                _deptRegSetUp = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _deptRegSetUp;
            }

            return _deptRegSetUp;
        }

        public List<DeptRegSetUp> GetAll()
        {
            List<DeptRegSetUp> deptRegSetUpList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<DeptRegSetUp> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<DeptRegSetUp>(sqlGetAll, mapper);
                IEnumerable<DeptRegSetUp> collection = accessor.Execute();

                deptRegSetUpList = collection.ToList();
            }

            catch (Exception ex)
            {
                return deptRegSetUpList;
            }

            return deptRegSetUpList;
        }

        #region Mapper
        private Database addParam(Database db, DbCommand cmd, DeptRegSetUp deptRegSetUp, bool isInsert)
        {

            if (isInsert)
            {
                db.AddOutParameter(cmd, "DeptRegSetUpID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "DeptRegSetUpID", DbType.Int32, deptRegSetUp.DeptRegSetUpID);
            }

            db.AddInParameter(cmd, "ProgramID", DbType.Int32, deptRegSetUp.ProgramID);
            db.AddInParameter(cmd, "LocalCGPA1", DbType.Decimal, deptRegSetUp.LocalCGPA1);
            db.AddInParameter(cmd, "LocalCredit1", DbType.Decimal, deptRegSetUp.LocalCredit1);
            db.AddInParameter(cmd, "LocalCGPA2", DbType.Decimal, deptRegSetUp.LocalCGPA2);
            db.AddInParameter(cmd, "LocalCredit2", DbType.Decimal, deptRegSetUp.LocalCredit2);
            db.AddInParameter(cmd, "LocalCGPA3", DbType.Decimal, deptRegSetUp.LocalCGPA3);
            db.AddInParameter(cmd, "LocalCredit3", DbType.Decimal, deptRegSetUp.LocalCredit3);
            db.AddInParameter(cmd, "ManCGPA1", DbType.Decimal, deptRegSetUp.ManCGPA1);
            db.AddInParameter(cmd, "ManCredit1", DbType.Decimal, deptRegSetUp.ManCredit1);
            db.AddInParameter(cmd, "ManRetakeGradeLimit1", DbType.String, deptRegSetUp.ManRetakeGradeLimit1);
            db.AddInParameter(cmd, "ManCGPA2", DbType.Decimal, deptRegSetUp.ManCGPA2);
            db.AddInParameter(cmd, "ManCredit2", DbType.Decimal, deptRegSetUp.ManCredit2);
            db.AddInParameter(cmd, "ManRetakeGradeLimit2", DbType.String, deptRegSetUp.ManRetakeGradeLimit2);
            db.AddInParameter(cmd, "ManCGPA3", DbType.Decimal, deptRegSetUp.ManCGPA3);
            db.AddInParameter(cmd, "ManCredit3", DbType.Decimal, deptRegSetUp.ManCredit3);
            db.AddInParameter(cmd, "ManRetakeGradeLimit3", DbType.String, deptRegSetUp.ManRetakeGradeLimit3);
            db.AddInParameter(cmd, "MaxCGPA1", DbType.Decimal, deptRegSetUp.MaxCGPA1);
            db.AddInParameter(cmd, "MaxCredit1", DbType.Decimal, deptRegSetUp.MaxCredit1);
            db.AddInParameter(cmd, "MaxCGPA2", DbType.Decimal, deptRegSetUp.MaxCGPA2);
            db.AddInParameter(cmd, "MaxCredit2", DbType.Decimal, deptRegSetUp.MaxCredit2);
            db.AddInParameter(cmd, "MaxCGPA3", DbType.Decimal, deptRegSetUp.MaxCGPA3);
            db.AddInParameter(cmd, "MaxCredit3", DbType.Decimal, deptRegSetUp.MaxCredit3);
            db.AddInParameter(cmd, "ProjectCGPA", DbType.Decimal, deptRegSetUp.ProjectCGPA);
            db.AddInParameter(cmd, "ProjectCredit", DbType.Decimal, deptRegSetUp.ProjectCredit);
            db.AddInParameter(cmd, "ThesisCGPA", DbType.Decimal, deptRegSetUp.ThesisCGPA);
            db.AddInParameter(cmd, "ThesisCredit", DbType.Decimal, deptRegSetUp.ThesisCredit);
            db.AddInParameter(cmd, "MajorCGPA", DbType.Decimal, deptRegSetUp.MajorCGPA);
            db.AddInParameter(cmd, "MajorCredit", DbType.Decimal, deptRegSetUp.MajorCredit);
            db.AddInParameter(cmd, "ProbationLock", DbType.Int32, deptRegSetUp.ProbationLock);
            db.AddInParameter(cmd, "CourseRetakeLimit", DbType.Int32, deptRegSetUp.CourseRetakeLimit);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, deptRegSetUp.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, deptRegSetUp.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, deptRegSetUp.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, deptRegSetUp.ModifiedDate);
            db.AddInParameter(cmd, "AutoPreRegCGPA1", DbType.Decimal, deptRegSetUp.AutoPreRegCGPA1);
            db.AddInParameter(cmd, "AutoPreRegCredit1", DbType.Decimal, deptRegSetUp.AutoPreRegCredit1);
            db.AddInParameter(cmd, "AutoPreRegCGPA2", DbType.Decimal, deptRegSetUp.AutoPreRegCGPA2);
            db.AddInParameter(cmd, "AutoPreRegCredit2", DbType.Decimal, deptRegSetUp.AutoPreRegCredit2);
            db.AddInParameter(cmd, "AutoPreRegCGPA3", DbType.Decimal, deptRegSetUp.AutoPreRegCGPA3);
            db.AddInParameter(cmd, "AutoPreRegCredit3", DbType.Decimal, deptRegSetUp.AutoPreRegCredit3);
            return db;
        }

        private IRowMapper<DeptRegSetUp> GetMaper()
        {
            IRowMapper<DeptRegSetUp> mapper = MapBuilder<DeptRegSetUp>.MapAllProperties()
            .Map(m => m.DeptRegSetUpID).ToColumn("DeptRegSetUpID")
            .Map(m => m.ProgramID).ToColumn("ProgramID")
            .Map(m => m.LocalCGPA1).ToColumn("LocalCGPA1")
            .Map(m => m.LocalCredit1).ToColumn("LocalCredit1")
            .Map(m => m.LocalCGPA2).ToColumn("LocalCGPA2")
            .Map(m => m.LocalCredit2).ToColumn("LocalCredit2")
            .Map(m => m.LocalCGPA3).ToColumn("LocalCGPA3")
            .Map(m => m.LocalCredit3).ToColumn("LocalCredit3")
            .Map(m => m.ManCGPA1).ToColumn("ManCGPA1")
            .Map(m => m.ManCredit1).ToColumn("ManCredit1")
            .Map(m => m.ManRetakeGradeLimit1).ToColumn("ManRetakeGradeLimit1")
            .Map(m => m.ManCGPA2).ToColumn("ManCGPA2")
            .Map(m => m.ManCredit2).ToColumn("ManCredit2")
            .Map(m => m.ManRetakeGradeLimit2).ToColumn("ManRetakeGradeLimit2")
            .Map(m => m.ManCGPA3).ToColumn("ManCGPA3")
            .Map(m => m.ManCredit3).ToColumn("ManCredit3")
            .Map(m => m.ManRetakeGradeLimit3).ToColumn("ManRetakeGradeLimit3")
            .Map(m => m.MaxCGPA1).ToColumn("MaxCGPA1")
            .Map(m => m.MaxCredit1).ToColumn("MaxCredit1")
            .Map(m => m.MaxCGPA2).ToColumn("MaxCGPA2")
            .Map(m => m.MaxCredit2).ToColumn("MaxCredit2")
            .Map(m => m.MaxCGPA3).ToColumn("MaxCGPA3")
            .Map(m => m.MaxCredit3).ToColumn("MaxCredit3")
            .Map(m => m.ProjectCGPA).ToColumn("ProjectCGPA")
            .Map(m => m.ProjectCredit).ToColumn("ProjectCredit")
            .Map(m => m.ThesisCGPA).ToColumn("ThesisCGPA")
            .Map(m => m.ThesisCredit).ToColumn("ThesisCredit")
            .Map(m => m.MajorCGPA).ToColumn("MajorCGPA")
            .Map(m => m.MajorCredit).ToColumn("ThesisCredit")
            .Map(m => m.ProbationLock).ToColumn("ProbationLock")
            .Map(m => m.CourseRetakeLimit).ToColumn("CourseRetakeLimit")
            .Map(m => m.CreatedBy).ToColumn("CreatedBy")
            .Map(m => m.CreatedDate).ToColumn("CreatedDate")
            .Map(m => m.ModifiedBy).ToColumn("ModifiedBy")
            .Map(m => m.ModifiedDate).ToColumn("ModifiedDate")
            .Map(m => m.AutoPreRegCGPA1).ToColumn("AutoPreRegCGPA1")
            .Map(m => m.AutoPreRegCredit1).ToColumn("AutoPreRegCredit1")
            .Map(m => m.AutoPreRegCGPA2).ToColumn("AutoPreRegCGPA2")
            .Map(m => m.AutoPreRegCredit2).ToColumn("AutoPreRegCredit2")
            .Map(m => m.AutoPreRegCGPA3).ToColumn("AutoPreRegCGPA3")
            .Map(m => m.AutoPreRegCredit3).ToColumn("AutoPreRegCredit3")
            .Build();

            return mapper;
        }    
        #endregion
    }
}
