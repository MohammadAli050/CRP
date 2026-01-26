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
    public class SQLTeacherInformationRepository:ITeacherInformationRepository
    {
         Database db = null;

       // private string sqlInsert = "UIUEMS_CC_AcademicCalenderSectionInsert";
       // private string sqlUpdate = "UIUEMS_CC_AcademicCalenderSectionUpdate";
       // private string sqlDelete = "UIUEMS_CC_AcademicCalenderSectionDeleteById";

        private string sqlTeacherInfoInsert = "UIUEMS_CC_TeacherInformation_Insert";
        private string sqlTeacherInfoGetByCode = "UIUEMS_CC_TeacherInformation_GetById";
        private string sqlTeacherInfoUpdate = "UIUEMS_CC_TeacherInformation_Update";
        private string sqlGetTeacheListByNameAndId = "UIUEMS_CC_TeacherInformation_GetByNameOrId";
        private string sqlTeacherValidateCheck = "UIUEMS_CC_TeacherInformation_Validation";

        public bool Insert(TeacherInfo teacher)
        {

            bool isInserted = false;
           
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlTeacherInfoInsert);

                db = addParam(db, cmd, teacher);
                int rowaffected=db.ExecuteNonQuery(cmd);
                if(rowaffected>0)
                   isInserted = true;

            }
            catch (Exception ex)
            {
                isInserted = false;
            }

            return isInserted;
        }
        /*
        public bool Update(RoomInformation roominfo)
        {
            bool isInsert = false;
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlRoomInfoUpdate);

                db = addParam(db, cmd, roominfo, isInsert);
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
        public bool Delete(int roomInfoID)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlRoomInfoDelete);

                db.AddInParameter(cmd, "RoomInfoID", DbType.Int32, roomInfoID);
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
        }*/
        public List<TeacherInfo> GetByNameOrId(string name, string id)
        {
            List<TeacherInfo> teacherList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TeacherInfo> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TeacherInfo>(sqlGetTeacheListByNameAndId, mapper);
                IEnumerable<TeacherInfo> collection = accessor.Execute(name,id);

                teacherList = collection.ToList();
            }

            catch (Exception ex)
            {
                return teacherList;
            }

            return teacherList;
        }
        public bool VaildateTeacher(string id)
        {
            List<TeacherInfo> teacherList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TeacherInfo> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TeacherInfo>(sqlTeacherValidateCheck, mapper);
                IEnumerable<TeacherInfo> collection = accessor.Execute(id);

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
        public TeacherInfo GetById(string id)
        {
            TeacherInfo _teacher = null;
            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<TeacherInfo> rowMapper = GetMaper();

                var accessor = db.CreateSprocAccessor<TeacherInfo>(sqlTeacherInfoGetByCode, rowMapper);
                _teacher = accessor.Execute(id).SingleOrDefault();

            }
            catch (Exception ex)
            {
                return _teacher;
            }

            return _teacher;
        }

        public bool Update(TeacherInfo teacher)
        {
            bool result = false;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                DbCommand cmd = db.GetStoredProcCommand(sqlTeacherInfoUpdate);

                db = addParam(db, cmd, teacher);
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
        /*
        public List<RoomInformation> GetAll()
        {
            List<RoomInformation> RoomInformationList = null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<RoomInformation> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<RoomInformation>(sqlGetAll, mapper);
                IEnumerable<RoomInformation> collection = accessor.Execute();

                RoomInformationList = collection.ToList();
            }

            catch (Exception ex)
            {
                return RoomInformationList;
            }

            return RoomInformationList;
        }
        */
        #region Mapper
        private Database addParam(Database db, DbCommand cmd,TeacherInfo teacher)
        {
            db.AddInParameter(cmd, "TeacherName", DbType.String, teacher.TeacherName);
            db.AddInParameter(cmd, "TeacherId", DbType.String, teacher.TeacherId);
            db.AddInParameter(cmd, "AcademicBackground", DbType.String, teacher.AcademicBackground);
            db.AddInParameter(cmd, "Publish", DbType.String, teacher.Publish);
            db.AddInParameter(cmd, "Phone", DbType.String, teacher.Phone);
            db.AddInParameter(cmd, "Email", DbType.String, teacher.Email);
            db.AddInParameter(cmd, "WebAddress", DbType.String, teacher.WebAddress);
            db.AddInParameter(cmd, "MaxNoTobeAdvised", DbType.Double, teacher.MaxNoTobeAdvised);
            db.AddInParameter(cmd, "UserID", DbType.String, teacher.UserID);
            return db;
        }
        private IRowMapper<TeacherInfo> GetMaper()
        {
            IRowMapper<TeacherInfo> mapper = MapBuilder<TeacherInfo>.MapAllProperties()
            .Map(m => m.TeacherId).ToColumn("TeacherId")
            .Map(m => m.TeacherName).ToColumn("TeacherName")
            .Map(m => m.Publish).ToColumn("Publish")
            .Map(m => m.Phone).ToColumn("Phone")
            .Map(m => m.WebAddress).ToColumn("WebAddress")
            .Map(m => m.MaxNoTobeAdvised).ToColumn("MaxNoTobeAdvised")
            .Map(m => m.Email).ToColumn("Email")
            .Map(m => m.AcademicBackground).ToColumn("AcademicBackground")
            .Map(m => m.UserID).ToColumn("UserID")
            .Build();

            return mapper;
        }
        #endregion
    }
}
