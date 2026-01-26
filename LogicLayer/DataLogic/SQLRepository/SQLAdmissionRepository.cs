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
    public partial class SQLAdmissionRepository : IAdmissionRepository
    {
        Database db = null;

        private string sqlInsert = "AdmissionInsert";
        private string sqlUpdate = "AdmissionUpdate";
        private string sqlDelete = "AdmissionDeleteById";
        private string sqlGetById = "AdmissionGetById";
        private string sqlGetAll = "AdmissionGetAll";
        private string sqlGetByStudentID = "AdmissionGetByStudentID";
        
        
        public int Insert(Admission admission)
        {
            int id = 0;
            bool isInsert = true;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlInsert);

                db = addParam(db, cmd, admission, isInsert);
                db.ExecuteNonQuery(cmd);

                object obj = db.GetParameterValue(cmd, "AdmissionID");

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

        public bool Update(Admission admission)
        {
            bool result = false;
            bool isInsert = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlUpdate);

                db = addParam(db, cmd, admission, isInsert);

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

                db.AddInParameter(cmd, "AdmissionID", DbType.Int32, id);
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

        public Admission GetById(int? id)
        {
            Admission _admission = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Admission> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Admission>(sqlGetById, rowMapper);
                _admission = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _admission;
            }

            return _admission;
        }

        public Admission GetByStudentId(int studentID)
        {
            Admission _admission = null;
            try
            {

                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Admission> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Admission>(sqlGetByStudentID, rowMapper);
                _admission = accessor.Execute(studentID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _admission;
            }

            return _admission;
        }

        public List<Admission> GetAll()
        {
            List<Admission> admissionList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Admission> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Admission>(sqlGetAll, mapper);
                IEnumerable<Admission> collection = accessor.Execute();

                admissionList = collection.ToList();
            }

            catch (Exception ex)
            {
                return admissionList;
            }

            return admissionList;
        }


        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Admission admission, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "AdmissionID", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "AdmissionID", DbType.Int32, admission.AdmissionID);
            }

            db.AddInParameter(cmd, "StudentID", DbType.Int32, admission.StudentID);
            db.AddInParameter(cmd, "AdmissionCalenderID", DbType.Int32, admission.AdmissionCalenderID);
            db.AddInParameter(cmd, "Remarks", DbType.String, admission.Remarks);
            db.AddInParameter(cmd, "IsRule", DbType.Boolean, admission.IsRule);
            db.AddInParameter(cmd, "IsLastAdmission", DbType.Boolean, admission.IsLastAdmission);
            db.AddInParameter(cmd, "CreatedBy", DbType.Int32, admission.CreatedBy);
            db.AddInParameter(cmd, "CreatedDate", DbType.DateTime, admission.CreatedDate);
            db.AddInParameter(cmd, "ModifiedBy", DbType.Int32, admission.ModifiedBy);
            db.AddInParameter(cmd, "ModifiedDate", DbType.DateTime, admission.ModifiedDate);
            
            return db;
        }

        private IRowMapper<Admission> GetMaper()
        {
            IRowMapper<Admission> mapper = MapBuilder<Admission>.MapAllProperties()
            .Map(m => m.AdmissionID).ToColumn("AdmissionID")
            .Map(m => m.StudentID).ToColumn("StudentID")
            .Map(m => m.AdmissionCalenderID).ToColumn("AdmissionCalenderID")
            .Map(m => m.Remarks).ToColumn("Remarks")
            .Map(m => m.IsRule).ToColumn("IsRule")
            .Map(m => m.IsLastAdmission).ToColumn("IsLastAdmission")
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
