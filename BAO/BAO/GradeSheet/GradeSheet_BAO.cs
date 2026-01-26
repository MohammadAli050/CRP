using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;
using System.Data;

namespace BussinessObject
{
    public class GradeSheet_BAO
    {
        public static int Save(List<GradeSheetEntity> gsEntities)
        {
            return GradeSheet_DAO.Save(gsEntities);
        }

        public static DataTable GetDataTable(int acaCalId, int courseID, int versionID, int teacherId, int sectionID)
        {
            List<GradeSheetEntity> entities = new List<GradeSheetEntity>();

            entities = GradeSheet_DAO.GetsBy(acaCalId, courseID, versionID, teacherId,sectionID);

            DataTable dt = new DataTable();
            DataRow row = null;

            if (entities != null && entities.Count != 0)
            {
                dt.Columns.Add(new DataColumn("StudentID", typeof(int)));
                dt.Columns.Add(new DataColumn("Roll", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalMarks", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Grade", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId", typeof(int)));

                foreach (GradeSheetEntity item in entities)
                {
                    row = dt.NewRow();
                    row["StudentID"] = item.StudentID;
                    row["Roll"] = Student.GetStudent(item.StudentID).Roll;
                    row["Name"] = Student.GetStudent(item.StudentID).StdName;
                    row["TotalMarks"] = item.ObtainMarks;
                    row["Grade"] = item.Grade;
                    row["GradeId"] = item.GradeId;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static DataTable GetDataTable1(int acaCalId, int courseID, int versionID, int sectionID)
        {
            List<GradeSheetEntity1> entities = new List<GradeSheetEntity1>();

            entities = GradeSheet_DAO.GetsBy1(acaCalId, courseID, versionID, sectionID);

            DataTable dt = new DataTable();
            DataRow row = null;
            if (entities != null && entities.Count != 0)
            {                
                dt.Columns.Add(new DataColumn("Id", typeof(int)));
                dt.Columns.Add(new DataColumn("StudentID", typeof(int)));
                dt.Columns.Add(new DataColumn("Roll", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalMarks", typeof(decimal)));
                dt.Columns.Add(new DataColumn("ObtainGrade", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId1", typeof(int)));
                dt.Columns.Add(new DataColumn("GradeId2", typeof(int)));
                dt.Columns.Add(new DataColumn("GradeId3", typeof(int)));
                dt.Columns.Add(new DataColumn("GradeId4", typeof(int)));
                dt.Columns.Add(new DataColumn("GradeId5", typeof(int)));
                dt.Columns.Add(new DataColumn("GradeId6", typeof(int)));
                dt.Columns.Add(new DataColumn("GradeId11", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId21", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId31", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId41", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId51", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId61", typeof(string)));

                
                    
                

                foreach (GradeSheetEntity1 item in entities)
                {
                    row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["StudentID"] = item.StudentID;
                    row["Roll"] = Student.GetStudent(item.StudentID).Roll;
                    row["Name"] = Student.GetStudent(item.StudentID).StdName;
                    row["TotalMarks"] = item.ObtainMarks;
                    row["ObtainGrade"] = item.ObatinGrade;
                    row["GradeId1"] = item.GradeId1;
                    row["GradeId2"] = item.GradeId2;
                    row["GradeId3"] = item.GradeId3;
                    row["GradeId4"] = item.GradeId4;
                    row["GradeId5"] = item.GradeId5;
                    row["GradeId6"] = item.GradeId6;

                    List<GradeDetailsEntity> _gdEntities1=new List<GradeDetailsEntity>();

                    if (item.GradeId1 != 0)
                    {
                        _gdEntities1 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId1).ToList();
                        row["GradeId11"] = _gdEntities1[0].Grade.ToString();
                    }
                    else
                        row["GradeId11"] = " ";

                    //List<GradeDetailsEntity> _gdEntities2 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId2).ToList();
                    //List<GradeDetailsEntity> _gdEntities3 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId3).ToList();
                    //List<GradeDetailsEntity> _gdEntities4 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId4).ToList();
                    //List<GradeDetailsEntity> _gdEntities5 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId5).ToList();
                    //List<GradeDetailsEntity> _gdEntities6 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId6).ToList();


                    if (item.GradeId2 != 0)
                    {
                        _gdEntities1 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId2).ToList();
                        row["GradeId21"] = _gdEntities1[0].Grade.ToString();
                    }
                    else
                        row["GradeId21"] = " ";



                    if (item.GradeId3 != 0)
                    {
                        _gdEntities1 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId3).ToList();
                        row["GradeId31"] = _gdEntities1[0].Grade.ToString();
                    }
                    else
                        row["GradeId31"] = " ";




                    if (item.GradeId4 != 0)
                    {
                        _gdEntities1 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId4).ToList();
                        row["GradeId41"] = _gdEntities1[0].Grade.ToString();
                    }
                    else
                        row["GradeId41"] = " ";



                    if (item.GradeId5 != 0)
                    {
                        _gdEntities1 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId5).ToList();
                        row["GradeId51"] = _gdEntities1[0].Grade.ToString();
                    }
                    else
                        row["GradeId51"] = " ";



                    if (item.GradeId6 != 0)
                    {
                        _gdEntities1 = GradeDetails_BAO.Load(12, 13).Where(c => c.Gradeid == item.GradeId6).ToList();
                        row["GradeId61"] = _gdEntities1[0].Grade.ToString();
                    }
                    else
                        row["GradeId61"] = " ";
                    
                    

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static int UpdateByReg(List<GradeSheetEntity> gsEntities)
        {
            return GradeSheet_DAO.UpdateByReg(gsEntities);
        }

        

        public static int TransferByReg(List<GradeSheetEntity> gsEntities)
        {
            return GradeSheet_DAO.TransferByReg(gsEntities);
        }

        public static DataTable GetConflictDataTable(int acaCalId, int courseID, int versionID, int sectionID)
        {
            List<GradeSheetEntity> entities = new List<GradeSheetEntity>();

            entities = GradeSheet_DAO.GetsConflictRows(acaCalId, courseID, versionID, sectionID);

            DataTable dt = new DataTable();
            DataRow row = null;
            if (entities != null && entities.Count != 0)
            {
                dt.Columns.Add(new DataColumn("Id", typeof(int)));
                dt.Columns.Add(new DataColumn("StudentID", typeof(int)));
                dt.Columns.Add(new DataColumn("Roll", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalMarks", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Grade", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeId", typeof(int)));

                foreach (GradeSheetEntity item in entities)
                {
                    row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["StudentID"] = item.StudentID;
                    row["Roll"] = Student.GetStudent(item.StudentID).Roll;
                    row["Name"] = Student.GetStudent(item.StudentID).StdName;
                    row["TotalMarks"] = item.ObtainMarks;
                    row["Grade"] = item.Grade;
                    row["GradeId"] = item.GradeId;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static DataTable GetsStudents()
        {
            List<GradeSheetEntity> entities = new List<GradeSheetEntity>();

            entities = GradeSheet_DAO.GetsStudents();

            DataTable dt = new DataTable();
            DataRow row = null;
            if (entities != null && entities.Count != 0)
            {
                
                dt.Columns.Add(new DataColumn("StudentID", typeof(int)));
                

                foreach (GradeSheetEntity item in entities)
                {
                    row = dt.NewRow();
                    
                    row["StudentID"] = item.StudentID;
                    

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static int UpdateConflictRetake(int gradeSheetId)
        {
            return GradeSheet_DAO.UpdateConflictRetake(gradeSheetId);
        }
    }
}
