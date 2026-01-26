using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class SelectedStudentForGPACalculation_DAO
    {
        #region Private Constant
            #region DBColumns
            private const string STUDENT_ID = "StudentID";
            private const string STUDENT_ROLL = "StudentRoll";
            private const string STUDENT_NAME = "StudentName";
            #endregion
            #region Parameters
            private const string STUDENT_ID_PA = "@StudentID";
            private const string STUDENT_ROLL_PA = "@StudentRoll";
            private const string STUDENT_NAME_PA = "@Name";
            private const string BATCHID_PA = "@BATCHID";
            private const string ACACALID_PA = "@ACACALID";
            private const string PROGRAMID_PA = "@PROGRAMID";
            #endregion
            
            
            private const string ALL_COLUMNS = STUDENT_ID + ","
                                           + STUDENT_ROLL + ","
                                           + STUDENT_NAME;

            private const string EQUALS = " = ";
        #endregion
        #region Methods
            #region Private Methods
            private static SelectedStudentForGPACalculationEntity Mapper(SQLNullHandler nullHandler)
            {
                SelectedStudentForGPACalculationEntity selectedStudent =
                    new SelectedStudentForGPACalculationEntity();
                selectedStudent.StudentID = nullHandler.GetInt32(STUDENT_ID);
                selectedStudent.StudentRoll = nullHandler.GetString(STUDENT_ROLL);
                selectedStudent.StudentName = nullHandler.GetString(STUDENT_NAME);
                return selectedStudent;
            }
            
            private static List<SelectedStudentForGPACalculationEntity> Maps(SqlDataReader reader)
            {
                SQLNullHandler nullHandler = new SQLNullHandler(reader);
                List<SelectedStudentForGPACalculationEntity> selectedStudents = null;
                while (reader.Read())
                {
                    if (selectedStudents == null)
                    {
                        selectedStudents = new List<SelectedStudentForGPACalculationEntity>();
                    }
                    SelectedStudentForGPACalculationEntity selectedStudent = Mapper(nullHandler);
                    selectedStudents.Add(selectedStudent);
                }
                return selectedStudents;
            }

            private static List<SelectedStudentForGPACalculationEntity> Maps(DataTable dataTable)
            {
                try
                {
                    //SQLNullHandler nullHandler = new SQLNullHandler(dataTable);
                    List<SelectedStudentForGPACalculationEntity> selectedStudents = null;
                    foreach (DataRow row in dataTable.Rows)
                    {
                       if(selectedStudents==null)
                       {
                           selectedStudents = new List<SelectedStudentForGPACalculationEntity>();
                           
                       }
                       SelectedStudentForGPACalculationEntity selectedStudent = new SelectedStudentForGPACalculationEntity();
                       selectedStudent.StudentID = (int)row[STUDENT_ID];
                       selectedStudent.StudentRoll = row[STUDENT_ROLL].ToString();
                       selectedStudent.StudentName = row[STUDENT_NAME].ToString();
                       
                       selectedStudents.Add(selectedStudent);
                    }

                    return selectedStudents;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            private static SelectedStudentForGPACalculationEntity Map(SqlDataReader reader)
            {
                SQLNullHandler nullHandler = new SQLNullHandler(reader);
                SelectedStudentForGPACalculationEntity selectedStudent = null;
                if (selectedStudent == null)
                {
                    selectedStudent = new SelectedStudentForGPACalculationEntity();
                    selectedStudent = Mapper(nullHandler);
                }
                return selectedStudent;
            }
            #endregion

        #region Public Methods 
        //public static List<SelectedStudentForGPACalculationEntity> GetStudentList(int batchID,int acaCalID,int programID)
        //{
        //   try
        //    {
        //        List<SelectedStudentForGPACalculationEntity> selectedStudents =
        //        new List<SelectedStudentForGPACalculationEntity>();
        //        string command = "EXEC SP_GET_REGISTERED_STUDENT_OF_TRIMESTER " +
        //                         BATCHID_PA + EQUALS + acaCalID + "," +
        //                         ACACALID_PA + EQUALS + acaCalID + "," +
        //                         PROGRAMID_PA + EQUALS + programID;
        //        SqlConnection connection = MSSqlConnectionHandler.GetConnection();
        //        SqlDataReader reader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, connection);
        //        if (connection.State == ConnectionState.Open)
        //        {
                    
        //        }
        //        //SQLNullHandler nullHandler = new SQLNullHandler(reader);
        //        selectedStudents = Maps(reader);
        //        int index = 0; 
        //       while (reader.Read())
        //        {
        //            SelectedStudentForGPACalculationEntity entity = new SelectedStudentForGPACalculationEntity();
                     
        //           //entity.StudentID = (int) reader[STUDENT_ID];
        //            //entity.StudentRoll = (String) reader[STUDENT_ROLL];
        //            //entity.StudentName = (String) reader[STUDENT_NAME];
        //            //selectedStudents.Add(entity);
        //            //index++;
        //        }
        //        //selectedStudents = Maps(reader);
        //        return selectedStudents;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
            
        //}

        public static List<SelectedStudentForGPACalculationEntity> GetStudentList(int batchID,int acaCalID,int programID)
        {
            try
            {
                List<SelectedStudentForGPACalculationEntity> selectedStudents =
                new List<SelectedStudentForGPACalculationEntity>();
                string command = "EXEC SP_GET_REGISTERED_STUDENT_OF_TRIMESTER " +
                                 BATCHID_PA + EQUALS + batchID + "," +
                                 ACACALID_PA + EQUALS + acaCalID + "," +
                                 PROGRAMID_PA + EQUALS + programID;
                SqlConnection connection = MSSqlConnectionHandler.GetConnection();
                DataTable dataTable = MSSqlConnectionHandler.MSSqlExecuteQuerry_dt(command, connection);
                //int numberOfRows = dataTable.Rows.Count;
                selectedStudents = Maps(dataTable);
                return selectedStudents;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        #endregion



        #endregion

    }
}
