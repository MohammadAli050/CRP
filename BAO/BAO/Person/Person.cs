using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DataAccess;

namespace BussinessObject
{
    [Serializable]
    public class Person : Base
    {
        #region Variables
        private Prefix _personPrefix;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _nickOrOtherName;
        private DateTime _dOB;
        private Gender _personGender;
        private MaritalStatus _matrialStatus;
        private BloodGroup _bloodGroup;
        private int _religionID;
        private int _nationalityID;
        private string _photoPath;
        private string _personName;
        private int _prefix;        
        private int _gender;        
        private int _candidateId;
        private string _personPhone;
        #endregion

        #region Constructor
        public Person():base()
        {
            _personPrefix = Prefix.NA;
            _firstName = string.Empty;
            _middleName = string.Empty;
            _lastName = string.Empty;
            _nickOrOtherName = string.Empty;
            _dOB = DateTime.MinValue;//6
            _personGender = Gender.NA;
            _matrialStatus = 0;
            _bloodGroup = 0;
            _religionID = 0;
            _nationalityID = 0;
            _photoPath = string.Empty;
            _prefix = 0;
            _gender = 0;
            _candidateId = 0;
            
        }
        #endregion

        #region Constants

        #region Column Constants
        private const string PERSONID = "PersonID";//0

        private const string PREFIX = "Prefix";//1
        private const string PREFIX_PA = "@Prefix";

        private const string FIRSTNAME = "FirstName";//2
        private const string FIRSTNAME_PA = "@FirstName";

        private const string MIDDLENAME = "MiddleName";//3
        private const string MIDDLENAME_PA = "@MiddleName";

        private const string LASTNAME = "LastName";//4
        private const string LASTNAME_PA = "@LastName";

        private const string NICKOROTHERNAME = "NickOrOtherName";//5
        private const string NICKOROTHERNAME_PA = "@NickOrOtherName";

        private const string DOBCON = "DOB";//6
        private const string DOBCON_PA = "@DOB";

        private const string GENDER = "Gender";//7
        private const string GENDER_PA = "@Gender";

        private const string MATRIALSTATUS = "MatrialStatus";//8
        private const string MATRIALSTATUS_PA = "@MatrialStatus";

        private const string BLOODGROUP = "BloodGroup";//9
        private const string BLOODGROUP_PA = "@BloodGroup";

        private const string RELIGIONID = "ReligionID";//10
        private const string RELIGIONID_PA = "@ReligionID";

        private const string NATIONALITYID = "NationalityID";//11
        private const string NATIONALITYID_PA = "@NationalityID";

        private const string PHOTOPATH = "PhotoPath";//12
        private const string PHOTOPATH_PA = "@PhotoPath";

        private const string CANDIDATEID = "CandidateID";//12
        private const string CANDIDATEID_PA = "@CandidateID";

        #endregion

        #region PKCOlumns
        private const string ALLCOLUMNS = "[" + PERSONID + "], "//0
                                        + "[" + PREFIX + "], "//1
                                        + "[" + FIRSTNAME + "], "//2
                                        + "[" + MIDDLENAME + "], "//3
                                        + "[" + LASTNAME + "], "//4
                                        + "[" + CANDIDATEID + "], "//4
                                        + "[" + NICKOROTHERNAME + "], ";//5
        //+ "[" + DOBCON + "], "//6
        //+ "[" + GENDER + "], "//7
        //+ "[" + MATRIALSTATUS + "], "//8
        //+ "[" + BLOODGROUP + "], "//9
        //+ "[" + RELIGIONID + "], "//10
        //+ "[" + NATIONALITYID + "], "//11
        //+ "[" + PHOTOPATH + "], ";//12
        #endregion

        #region NOPKCOLUMNS
        private const string NOPKCOLUMNS = "[" + PREFIX + "], "//1
                                        + "[" + FIRSTNAME + "], "//2
                                        + "[" + MIDDLENAME + "], "//3
                                        + "[" + LASTNAME + "], "//4
                                        + "[" + CANDIDATEID + "], "//4
                                        + "[" + NICKOROTHERNAME + "], ";//5
        //+ "[" + DOBCON + "], "//6
        //+ "[" + GENDER + "], "//7
        //+ "[" + MATRIALSTATUS + "], "//8
        //+ "[" + BLOODGROUP + "], "//9
        //+ "[" + RELIGIONID + "], "//10
        //+ "[" + NATIONALITYID + "], "//11
        //+ "[" + PHOTOPATH + "], ";//12
        #endregion

        private const string TABLENAME = " [Person] ";

        #region SELECT
        private const string SELECT = "SELECT "
                    + ALLCOLUMNS
                    + BASECOLUMNS
                    + "FROM" + TABLENAME;
        #endregion

        #region INSERT
        private const string INSERT = "INSERT INTO" + TABLENAME + "("
                     + ALLCOLUMNS
                     + BASECOLUMNS + ")"
                     + "VALUES ( "
                     + ID_PA + ", "
                     + PREFIX_PA + ", "//1
                     + FIRSTNAME_PA + ", "//2
                     + MIDDLENAME_PA + ", "//3
                     + LASTNAME_PA + ", "//4
                     + CANDIDATEID_PA + ", "//4
                     + NICKOROTHERNAME_PA + ", "//5
            //+ DOBCON_PA + ", "//6
            //+ GENDER_PA + ", "//7
            //+ MATRIALSTATUS_PA + ", "//8
            //+ BLOODGROUP_PA + ", "//9
            //+ RELIGIONID_PA + ", "//10
            //+ NATIONALITYID_PA + ", "//11
            //+ PHOTOPATH_PA + ")"; //12
                    + CREATORID_PA + ", "
                    + CREATEDDATE_PA + ", "
                    + MODIFIERID_PA + ", "
                    + MOIDFIEDDATE_PA + ")";

        #endregion

        #region UPDATE
        private const string UPDATE = "UPDATE" + TABLENAME
                    + "SET [" + PREFIX + "] = " + PREFIX_PA + ", "//1
                    + "[" + FIRSTNAME + "] = " + FIRSTNAME_PA + ", "//2
                    + "[" + MIDDLENAME + "] = " + MIDDLENAME_PA + ", "//3
                    + "[" + LASTNAME + "] = " + LASTNAME_PA + ", "//4
                    + "[" + CANDIDATEID + "] = " + CANDIDATEID_PA + ", "//4
                    + "[" + NICKOROTHERNAME + "] = " + NICKOROTHERNAME_PA + ", "//5
            //+ "[" + DOBCON + "] = " + DOBCON_PA + ", "//6
            //+ "[" + GENDER + "] = " + GENDER_PA + ", "//7
            //+ "[" + MATRIALSTATUS + "] = " + MATRIALSTATUS_PA + ", "//8
            //+ "[" + BLOODGROUP + "] = " + BLOODGROUP_PA + ", "//9
            //+ "[" + RELIGIONID + "] = " + RELIGIONID_PA + ", "//10
            //+ "[" + NATIONALITYID + "] = " + NATIONALITYID_PA + ", "//11
            //+ "[" + PHOTOPATH + "] = " + PHOTOPATH_PA + ", "//12

                    + "[" + CREATORID + "] = " + CREATORID_PA + ", "//13
                    + "[" + CREATEDDATE + "] = " + CREATEDDATE_PA + ", "//14
                    + "[" + MODIFIERID + "] = " + MODIFIERID_PA + ", "//15
                    + "[" + MOIDFIEDDATE + "] = " + MOIDFIEDDATE_PA;//16
        #endregion

        #endregion

        #region Properties

        public Prefix PersonPrefix
        {
            get
            {
                return this._personPrefix;
            }
            set
            {
                this._personPrefix = value;
            }
        }
        private SqlParameter PersonPrefixParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PREFIX_PA;
                if (PersonPrefix == Prefix.NA)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = (int)PersonPrefix;
                }
                return sqlParam;
            }
        }

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
            }
        }
        private SqlParameter FirstNameParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = FIRSTNAME_PA;
                if (FirstName == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = FirstName;
                }
                return sqlParam;
            }
        }

        public string MiddleName
        {
            get
            {
                return this._middleName;
            }
            set
            {
                this._middleName = value;
            }
        }
        private SqlParameter MiddleNameParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MIDDLENAME_PA;
                if (MiddleName == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = MiddleName;
                }
                return sqlParam;
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
            }
        }
        private SqlParameter LastNameParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = LASTNAME_PA;
                if (LastName == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = LastName;
                }
                return sqlParam;
            }
        }

        public string NickOrOtherName
        {
            get
            {
                return this._nickOrOtherName;
            }
            set
            {
                this._nickOrOtherName = value;
            }
        }
        private SqlParameter NickOrOtherNameParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = NICKOROTHERNAME_PA;
                if (NickOrOtherName == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = NickOrOtherName;
                }
                return sqlParam;
            }
        }

        public DateTime DOB
        {
            get
            {
                return this._dOB;
            }
            set
            {
                this._dOB = value;
            }
        }
        private SqlParameter DOBParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = DOBCON_PA;
                if (DOB == DateTime.MinValue)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = DOB;
                }
                return sqlParam;
            }
        }

        public Gender StudentGender
        {
            get
            {
                return this._personGender;
            }
            set
            {
                this._personGender = value;
            }
        }
        private SqlParameter StudentGenderParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = GENDER_PA;
                if (StudentGender == Gender.NA)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = (int)StudentGender;
                }
                return sqlParam;
            }
        }

        public MaritalStatus StudentMatrialStatus
        {
            get
            {
                return this._matrialStatus;
            }
            set
            {
                this._matrialStatus = value;
            }
        }
        private SqlParameter MatrialStatusParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = MATRIALSTATUS_PA;
                if (StudentMatrialStatus == MaritalStatus.NA)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = (int)StudentMatrialStatus;
                }
                return sqlParam;
            }
        }

        public BloodGroup StudentBloodGroup
        {
            get
            {
                return this._bloodGroup;
            }
            set
            {
                this._bloodGroup = value;
            }
        }
        private SqlParameter StudentBloodGroupParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = BLOODGROUP_PA;
                if (StudentBloodGroup == BloodGroup.NA)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = (int)StudentBloodGroup;
                }
                return sqlParam;
            }
        }

        public int ReligionID
        {
            get
            {
                return this._religionID;
            }
            set
            {
                this._religionID = value;
            }
        }
        private SqlParameter ReligionIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = RELIGIONID_PA;
                if (ReligionID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = ReligionID;
                }
                return sqlParam;
            }
        }

        public int NationalityID
        {
            get
            {
                return this._nationalityID;
            }
            set
            {
                this._nationalityID = value;
            }
        }
        private SqlParameter NationalityIDParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = NATIONALITYID_PA;
                if (NationalityID == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = NationalityID;
                }
                return sqlParam;
            }
        }

        public string PhotoPath
        {
            get { return _photoPath; }
            set { _photoPath = value; }
        }
        private SqlParameter PhotoPathParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = PHOTOPATH_PA;
                if (PhotoPath == string.Empty)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = PhotoPath;
                }
                return sqlParam;
            }
        }

        public string PersonName
        {
            get
            {
                string str = FirstName + " " + MiddleName + " " + LastName + " " + NickOrOtherName;
                _personName = (int)PersonPrefix > 0 ? Enum.GetName(typeof(Prefix), PersonPrefix) + " " + str : str;
                return _personName;
            }
        }

        public int PrefixInt
        {
            get { return _prefix; }
            set { value = _prefix; }
        }

        public int GenderInt
        {
            get { return _gender; }
            set { value = _gender; }
        }

        public int CandidateId
        {
            get { return _candidateId; }
            set { _candidateId = value; }
        }
        private SqlParameter CandidateIdParam
        {
            get
            {
                SqlParameter sqlParam = new SqlParameter();
                sqlParam.ParameterName = CANDIDATEID_PA;
                if (CandidateId == 0)
                {
                    sqlParam.Value = DBNull.Value;
                }
                else
                {
                    sqlParam.Value = CandidateId;
                }
                return sqlParam;
            }
        }

        public string PersonPhone
        {
            get { return _personPhone; }
            set { _personPhone = value; }
        }

        #endregion

        #region Function

        private static Person personMapper(SQLNullHandler nullHandler)
        {
            Person student = new Person();

            student.Id = nullHandler.GetInt32(PERSONID);
            student.PersonPrefix = (Prefix)nullHandler.GetInt32(PREFIX);
            student.FirstName = nullHandler.GetString(FIRSTNAME);
            student.MiddleName = nullHandler.GetString(MIDDLENAME);
            student.LastName = nullHandler.GetString(LASTNAME);
            student.NickOrOtherName = nullHandler.GetString(NICKOROTHERNAME);
            student.CreatorID = nullHandler.GetInt32(CREATORID);
            student.CreatedDate = nullHandler.GetDateTime(CREATEDDATE);
            student.ModifierID = nullHandler.GetInt32(MODIFIERID);
            student.ModifiedDate = nullHandler.GetDateTime(MOIDFIEDDATE);

            return student;
        }

        private static List<Person> mapPersons(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            List<Person> persons = null;

            while (theReader.Read())
            {
                if (persons == null)
                {
                    persons = new List<Person>();
                }
                Person person = personMapper(nullHandler);
                persons.Add(person);
            }

            return persons;
        }

        private static Person mapPerson(SqlDataReader theReader)
        {
            SQLNullHandler nullHandler = new SQLNullHandler(theReader);

            Person person = null;
            if (theReader.Read())
            {
                person = new Person();
                person = personMapper(nullHandler);
            }

            return person;
        }

        internal static Person GetPerson(int id)
        {
            string command = SELECT
                            + "WHERE PersonID = " + ID_PA;

            SqlParameter iDParam = MSSqlConnectionHandler.MSSqlParamGenerator(id, ID_PA);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { iDParam });

            Person person = mapPerson(theReader);
            theReader.Close();

            MSSqlConnectionHandler.CloseDbConnection();

            return person;
        }

        #endregion


        internal static int savePerson(Person person, SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            try
            {
                int counter = 0;

                string command = string.Empty;
                SqlParameter[] sqlParams = null;


                if (person.Id == 0)
                {
                    #region Insert
                    person.Id = Person.GetMaxPersonID(sqlConn, sqlTran);
                    command = INSERT;
                    sqlParams = new SqlParameter[] { person.IDParam,
                                                 person.PersonPrefixParam,  
                                                 person.FirstNameParam, 
                                                 person.MiddleNameParam, 
                                                 person.LastNameParam,  
                                                 person.NickOrOtherNameParam, 
                                                 person.CandidateIdParam,
                                                 
                                                 person.CreatorIDParam,
                                                 person.CreatedDateParam,
                                                 person.ModifierIDParam,
                                                 person.ModifiedDateParam};
                    #endregion
                }
                else
                {
                    #region Update
                    command = UPDATE
                    + " WHERE [" + PERSONID + "] = " + ID_PA;
                    sqlParams = new SqlParameter[] { person.IDParam,
                                                 person.PersonPrefixParam,  
                                                 person.FirstNameParam, 
                                                 person.MiddleNameParam, 
                                                 person.LastNameParam,  
                                                 person.NickOrOtherNameParam,  
                                                 
                                                 person.CreatorIDParam,
                                                 person.CreatedDateParam,
                                                 person.ModifierIDParam,
                                                 person.ModifiedDateParam};
                    #endregion
                }
                counter += DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchAction(command, sqlConn, sqlTran, sqlParams);

                if (counter <= 0)
                {
                    return 0;
                }
                else
                {
                    return person.Id;
                }
            }
            catch (Exception ex)
            {
                MSSqlConnectionHandler.RollBackAndClose();
                throw ex;
            }
        }

        private static int GetMaxPersonID(SqlConnection sqlConn, SqlTransaction sqlTran)
        {
            int newPersonID = 0;

            string command = "SELECT MAX(PersonID) FROM" + TABLENAME;
            object ob = DataAccess.MSSqlConnectionHandler.MSSqlExecuteBatchScalar(command, sqlConn, sqlTran);

            if (ob == null || ob == DBNull.Value)
            {
                newPersonID = 1;
            }
            else if (ob is Int32)
            {
                newPersonID = Convert.ToInt32(ob) + 1;
            }

            return newPersonID;
        }

        public static Person GetPersonById(int id)
        {
            Person person = new Person();

            string command = SELECT
                            + "WHERE [" + PERSONID + "] = " + ID_PA;

            SqlParameter sqlParam = new SqlParameter(ID_PA, id);

            SqlConnection sqlConn = MSSqlConnectionHandler.GetConnection();
            SqlDataReader theReader = MSSqlConnectionHandler.MSSqlExecuteQuerry(command, sqlConn, new SqlParameter[] { sqlParam });

            person = mapPerson(theReader);
            theReader.Close();
            MSSqlConnectionHandler.CloseDbConnection();
            return person;
        }
    }
}
