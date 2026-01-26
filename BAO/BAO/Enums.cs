using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    public enum UserType
    {
        None = 0, General = 1
    }
    public enum UserSource
    {
        None = 0, Student = 1, Teacher = 2
    }
    public enum Gender
    {
        NA=0, Male = 1, Female = 2
    }
    public enum TimeSlotType
    {
        None = 0, Theory = 1, Sessional = 3 
    }
    public enum AMPM
    {
        AM =1, PM=2
    }
    public enum BloodGroup
    {
        NA = 0, A_positive = 1, A_negative = 2, B_negative = 3, B_positive = 4, AB_negative = 5, AB_positive = 6, O_negative = 7, O_positive = 8
    }
    public enum Prefix
    {
        NA = 0, MR = 1, MS = 2, MRS = 3, Md = 4, Dr = 5
    }

    public enum VoucherPrefix
    {
        NA = 0, Std = 1, Bill = 2, Bill_Std = 3
    }
    public enum MaritalStatus
    {
        NA = 0, Married = 1, Unmarried = 2, Widow = 3, Widower = 4, Divorced = 5
    }
    public enum WeekDays
    {
        NA = 0, Sunday = 1, Monday = 2, Tuesday = 3, Wedneseday = 4, Thursday = 5, Friday = 6, Saturday = 7
    }

    //public enum GradeType
    //{
    //    NA = 0, APlus = 1, A = 2, AMinus = 3, BPlus=4, B=5, BMinus=6, CPlus=7,C = 8,CMinus=9, DPlus = 10, D = 11,DMinus=12,I=5,F=6
    //}

    public enum FlatAssignedTreeLevels
    {
        None = 0, CalendarDetail = 1, CalCourseProgNode = 2, Node = 3, NodeCourse = 4, Course = 5
    }

    public enum FlatAssignedStructTreeLevels
    {
        None = 0, Node = 1, NodeCourse = 2, Course = 3, RootNode = 4, VNodeSetMaster = 5, VnodeSet = 6, TreeMaster = 7, TreeDetail = 8, AssociatedCOurse = 9
    }

    public enum NumberOfAdmissionTest
    {
        NA = 0, WithoutAT = 1, First = 2, Second = 3, Third = 4, Fourth = 5
    }
    public enum FormSubmissionTypes
    { 
        All = 0, Submitted = 1, NotSubmitted = 2
    }
    public class FriendlyWeekDays
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;

                _value = (int)Enum.Parse(typeof(WeekDays), _name, true);
            }
        }
        private int _value;

        public int Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                _name = Enum.GetName(typeof(WeekDays), _value);
            }
        }

        public FriendlyWeekDays(int value)
        {
            _value = value;
            _name = Enum.GetName(typeof(WeekDays), _value);
        }

        public static List<FriendlyWeekDays> GetEnum()
        {
            List<FriendlyWeekDays> weekdays = new List<FriendlyWeekDays>();
            foreach (string item in Enum.GetNames(typeof(WeekDays)))
            {
                FriendlyWeekDays weekday = new FriendlyWeekDays((int)Enum.Parse(typeof(WeekDays), item, true));
                weekdays.Add(weekday);
            }
            return weekdays;
        }
    }

    //public 

}
