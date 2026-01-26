using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BussinessObject
{
   public class Course_BAO
    {
       public static List<CourseEntity> GetAllCourses(string searchText)
       {
           try
           {
               List<CourseEntity> courseEntities = new List<CourseEntity>();
               //check if the user puts any text in the search box or not
                //search box is empty, then fill the grid with all courses
                //or else find a particular set of course(s) and populate the grid
               //
                if (string.IsNullOrEmpty(searchText))
                {
                    courseEntities = Course_DAO.GetAllCourses();
                }
                else
                {
                    courseEntities = Course_DAO.GetAllCoursesByCourseCode(searchText);
                }
               
               return courseEntities;
           }
           catch (Exception exception)
           {
               throw exception;
           }
       }

       public static CourseEntity GetCourseById(int courseID)
       {
           try
            {
                CourseEntity courseEntity = Course_DAO.GetCoursebyCourseID(courseID);
                return courseEntity;
            }
            catch (Exception exception)
            {
                throw exception;
            }
       }

       public static CourseEntity GetCourseByIdVersionID(int courseID, int versionId)
       {
           try
           {
               CourseEntity entity = Course_DAO.GetCourseByCourseIDVersionID(courseID, versionId);
               return entity;
           }
            catch (Exception exception)
            {
               throw exception;
            }
       }
    }
}
