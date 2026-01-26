using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;

public partial class Admin_ActiveStudentSummary : BasePage
{
    private int cse,bba,eee,ete,cte,mscse,mce,ais,eco, mbar,mbae,meco;
    private int totalStudentCount;
    private List<ActiveStudent> activeStu = new List<ActiveStudent>();

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadResult();
    }

    private void LoadResult()
    {
        List<Student> studentList = StudentManager.GetAll();

        foreach (var student in studentList)
        {
            if (student.ProgramID == 1 && student.IsActive == true)
                cse++;
            else if (student.ProgramID == 2 && student.IsActive == true)
                bba++;
            else if (student.ProgramID == 3 && student.IsActive == true)
                eee++;
            else if (student.ProgramID == 4 && student.IsActive == true)
               ete++;
            else if (student.ProgramID == 5 && student.IsActive == true)
                mscse++;
            else if (student.ProgramID == 6 && student.IsActive == true)
                mce++;
            else if (student.ProgramID == 7 && student.IsActive == true)
                cte++;
            else if (student.ProgramID == 8 && student.IsActive == true)
                ais++;
            else if (student.ProgramID == 9 && student.IsActive == true)
                eco++;
            else if (student.ProgramID == 10 && student.IsActive == true)
                mbar++;
            else if (student.ProgramID ==11 && student.IsActive == true)
                mbae++;
            else if (student.ProgramID == 12 && student.IsActive == true)
                meco++;

            
        }

        activeStu.Add(new ActiveStudent("CSE", cse));
        activeStu.Add(new ActiveStudent("MSCSE", mscse));
        activeStu.Add(new ActiveStudent("MSCE", mce));
        activeStu.Add(new ActiveStudent("CTE", cte));
        activeStu.Add(new ActiveStudent("EEE", eee));
        activeStu.Add(new ActiveStudent("ETE", ete));
        activeStu.Add(new ActiveStudent("BBA", bba));
        activeStu.Add(new ActiveStudent("BBA in AIS", ais));
        activeStu.Add(new ActiveStudent("MBA(Regular)", mbar));
        activeStu.Add(new ActiveStudent("MBA(Executive)", mbae));
        activeStu.Add(new ActiveStudent("BSECO", eco));
        activeStu.Add(new ActiveStudent("MSECO", meco));

        Active_Student_View.DataSource = activeStu;
        Active_Student_View.DataBind();
        totalStudentCount = cse + mscse + mce + eee + ete + bba + ais + mbar + mbae + eco + meco+cte;
        totalCount.Text = totalStudentCount.ToString();
    }

    private class ActiveStudent
    {
        public string Program { get; set; }
        public int TotalStudent { get; set; }

        public ActiveStudent(string prog,int student)
        {
            Program = prog;
            TotalStudent = student;
        }
    }

}
