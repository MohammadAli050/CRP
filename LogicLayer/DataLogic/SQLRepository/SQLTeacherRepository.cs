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
    public partial class SQLTeacherRepository : ITeacherRepository
    {

        Database db = null;

        private string sqlGetAll = "TeacherGetAll";
          
        public List<Teacher> GetAll()
        {
            List<Teacher> teacherList= null;

            try
            {
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>();

                IRowMapper<Teacher> mapper = GetMaper();

                var accessor = db.CreateSprocAccessor<Teacher>(sqlGetAll, mapper);
                IEnumerable<Teacher> collection = accessor.Execute();

                teacherList = collection.ToList();
            }

            catch (Exception ex)
            {
                return teacherList;
            }

            return teacherList;
        }

       
        #region Mapper
        private Database addParam(Database db, DbCommand cmd, Teacher teacher, bool isInsert)
        {
            if (isInsert)
            {
                db.AddOutParameter(cmd, "TeacherId", DbType.Int32, Int32.MaxValue);
            }
            else
            {
                db.AddInParameter(cmd, "TeacherId", DbType.Int32, teacher.TeacherId);
            }

            	
		db.AddInParameter(cmd,"TeacherName",DbType.String,teacher.TeacherName);
            
            return db;
        }

        private IRowMapper<Teacher> GetMaper()
        {
            IRowMapper<Teacher> mapper = MapBuilder<Teacher>.MapAllProperties()

       	    .Map(m => m.TeacherId).ToColumn("TeacherId")
		    .Map(m => m.TeacherName).ToColumn("TeacherName")
            
            .Build();

            return mapper;
        }
        #endregion

    }
}

