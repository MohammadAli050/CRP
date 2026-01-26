using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Common;
using System.Data;

namespace BussinessObject
{
    public class SiblingSetup_BAO
    {
        public static int Save(SiblingSetupEntity siblingSetupEntity)
        {
            return SiblingSetup_DAO.Save(siblingSetupEntity);
        }

        public static int Save(List<SiblingSetupEntity> siblingSetupEntities)
        {
            return SiblingSetup_DAO.Save(siblingSetupEntities);
        }

        //public static List<SiblingSetupEntity> GetAllInGroupBy(string roll)
        //{
        //    return SiblingSetup_DAO.GetAllInGroupBy(roll);
        //}

        public static DataTable GetAllInGroupBy(string roll)
        {
            List<SiblingSetupEntity> entities = new List<SiblingSetupEntity>();

            entities = SiblingSetup_DAO.GetAllInGroupBy(roll);

            DataTable dt = new DataTable();
            DataRow row = null;

            if (entities != null && entities.Count != 0)
            {
                dt.Columns.Add(new DataColumn("SiblingSetupId", typeof(int)));
                dt.Columns.Add(new DataColumn("GroupID", typeof(int)));
                dt.Columns.Add(new DataColumn("ApplicantId", typeof(int)));
                dt.Columns.Add(new DataColumn("Roll", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));

                foreach (SiblingSetupEntity item in entities)
                {
                    row = dt.NewRow();
                    row["SiblingSetupId"] = item.Id;
                    row["GroupID"] = item.GroupId;
                    row["ApplicantId"] = item.ApplicantId;
                    row["Roll"] = Student.GetStudent(item.ApplicantId).Roll;
                    row["Name"] = Student.GetStudent(item.ApplicantId).StdName;
                   
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static int Delete(int id)
        {
            return SiblingSetup_DAO.Delete(id);
        }

        public static DataTable GetAllInGroupBy(int groupId)
        {
            List<SiblingSetupEntity> entities = new List<SiblingSetupEntity>();

            entities = SiblingSetup_DAO.GetAllInGroupBy(groupId);

            DataTable dt = new DataTable();
            DataRow row = null;

            if (entities != null && entities.Count != 0)
            {
                dt.Columns.Add(new DataColumn("SiblingSetupId", typeof(int)));
                dt.Columns.Add(new DataColumn("GroupID", typeof(int)));
                dt.Columns.Add(new DataColumn("ApplicantId", typeof(int)));
                dt.Columns.Add(new DataColumn("Roll", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));

                foreach (SiblingSetupEntity item in entities)
                {
                    row = dt.NewRow();
                    row["SiblingSetupId"] = item.Id;
                    row["GroupID"] = item.GroupId;
                    row["ApplicantId"] = item.ApplicantId;
                    row["Roll"] = Student.GetStudent(item.ApplicantId).Roll;
                    row["Name"] = Student.GetStudent(item.ApplicantId).StdName;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static bool CheckDuplicate(int applicantId)
        {
            bool boo=false;
            int i = SiblingSetup_DAO.CheckDuplicate(applicantId);

            if (i > 0)
            { return true; }
            else 
            { return false; }
        }
    }
}
