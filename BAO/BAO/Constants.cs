using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessObject
{
    public class BOConstants
    {
        //public static string[] Grades = new string[] { "N/A","A+", "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D+", "D", "D-", "F", "W", "I" };
        public static SortedList Grades = new SortedList() { {  0,"N/A" },
                                                           { 1,"A+" },
                                                           { 2,"A" },
                                                           { 3,"A-" },
                                                           { 4,"B+" },
                                                           { 5,"B" },
                                                           { 6,"B-" },
                                                           { 7,"C+" },
                                                           { 8,"C" },
                                                           { 9,"C-" },
                                                           { 10,"D+" },
                                                           { 11,"D" },
                                                           { 12,"D-" },
                                                           { 13,"X" },
                                                           { 14,"F" },
                                                           { 15,"W" },
                                                           { 16,"I" }};
    }
}
