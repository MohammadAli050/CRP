using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SelectSectionEntity
    {
        #region Variables
        private int _acaCal_SectionID;
        private string _sectionName;
        private string _timeSlot_1;
        private string _dayOne;
        private string _timeSlot_2;
        private string _dayTwo;
        private string _faculty_1;
        private string _faculty_2;
        private string _roomNo_1;
        private string _roomNo_2;
        private int _capacity;
        private int _occupied; 
        #endregion

        public SelectSectionEntity()
        {
            AcaCal_SectionID = 0;
            SectionName = string.Empty;
            TimeSlot_1 = string.Empty;
            DayOne = string.Empty;
            TimeSlot_2 = string.Empty;
            DayTwo = string.Empty;
            Faculty_1 = string.Empty;
            Faculty_2 = string.Empty;
            RoomNo_1 = string.Empty;
            RoomNo_2 = string.Empty;
            Capacity = 0;
            Occupied = 0;
        }

        public int AcaCal_SectionID
        {
            get { return _acaCal_SectionID; }
            set { _acaCal_SectionID = value; }
        }
        public string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        public string TimeSlot_1
        {
            get { return _timeSlot_1; }
            set { _timeSlot_1 = value; }
        }
        public string DayOne
        {
            get { return _dayOne; }
            set { _dayOne = value; }
        }
        public string TimeSlot_2
        {
            get { return _timeSlot_2; }
            set { _timeSlot_2 = value; }
        }
        public string DayTwo
        {
            get { return _dayTwo; }
            set { _dayTwo = value; }
        }
        public string Faculty_1
        {
            get { return _faculty_1; }
            set { _faculty_1 = value; }
        }
        public string Faculty_2
        {
            get { return _faculty_2; }
            set { _faculty_2 = value; }
        }
        public string RoomNo_1
        {
            get { return _roomNo_1; }
            set { _roomNo_1 = value; }
        }
        public string RoomNo_2
        {
            get { return _roomNo_2; }
            set { _roomNo_2 = value; }
        }
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        public int Occupied
        {
            get { return _occupied; }
            set { _occupied = value; }
        }

    }
}
