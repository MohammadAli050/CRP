using CommonUtility;
using LogicLayer.BusinessLogic;
using LogicLayer.BusinessObjects;
using LogicLayer.BusinessObjects.RO;
using Microsoft.Reporting.WebForms;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EMS.Module.student.report
{
    public partial class RptStudentCertificate : BasePage
    {
        BussinessObject.UIUMSUser userObj = null;
        private string _ipAddress = ConfigurationManager.AppSettings["IPAddress"];
        static string BasePath = "";
        static string LoginUserLoginId = "";
        static int LoginUserUserId = 0;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            {
                Page.ClientTarget = "uplevel";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BasePath = Server.MapPath("~/Upload/Archive/");
            base.CheckPage_Load();
            int userId = base.BaseCurrentUserObj.Id;
            LoginUserLoginId = base.BaseCurrentUserObj.LogInID;
            LoginUserUserId = base.BaseCurrentUserObj.Id;


            try
            {
                userObj = (BussinessObject.UIUMSUser)GetFromSession(Constants.SESSIONCURRENT_USER);
                SessionManager.DeletFromSession("StudentEdit");
                base.CheckPage_Load();

                if (!IsPostBack)
                {
                    ucProgram.LoadDropdownWithUserAccess(userObj.Id);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
                //Utilities.ShowMassage(this.lblErr, Color.Red, Ex.Message);
            }
        }

        protected void OnProgramSelectedIndexChanged(object sender, EventArgs e)
        {
            ucBatch.LoadDropDownList(Convert.ToInt32(ucProgram.selectedValue));
        }

        protected void OnBatchSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected void chkProvisional_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                OnBatchSelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


    

        protected void chkNotSet_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chk = sender as CheckBox;
            GridViewRow row1 = chk.NamingContainer as GridViewRow;

            CheckBox CheckBoxRoll = (CheckBox)row1.FindControl("ChkSelect");
            if (CheckBoxRoll.Checked)
            {
                Label roll = (Label)row1.FindControl("lblRoll");

                lblStudentRoll.Text = roll.Text;
            }
            else
            {
                lblStudentRoll.Text = "";
            }
        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            ClearAll();

            int programId = Convert.ToInt32(ucProgram.selectedValue);
            int batchId = Convert.ToInt32(ucBatch.selectedValue);
            string roll = txtStudent.Text == "" ? "0" : txtStudent.Text;
            List<StudentRegistration> studentRegistrationList = StudentRegistrationManager.GetAllByProgramBatchStudent(programId, batchId, roll);

            if (string.IsNullOrEmpty(txtStudent.Text.Trim()))
            {
                try
                {
                    List<LogicLayer.BusinessObjects.Student> stdList = StudentManager.GetAllByProgramIdBatchId(Convert.ToInt32(ucProgram.selectedValue), Convert.ToInt32(ucBatch.selectedValue));
                    if (stdList != null && stdList.Count > 0)
                    {
                        foreach (LogicLayer.BusinessObjects.Student tempStudent in stdList)
                        {
                            try
                            {
                                if (tempStudent.BasicInfo.PhotoPath != null)
                                    tempStudent.Attribute3 = tempStudent.BasicInfo.PhotoPath.Length > 0 ? "../Upload/Avatar/" + tempStudent.BasicInfo.PhotoPath : tempStudent.Gender == "Female" ? "../Upload/Avatar/Female.jpg" : "../Upload/Avatar/Male.jpg";
                                else
                                    tempStudent.Attribute3 = tempStudent.Gender == "Female" ? "../Upload/Avatar/Female.jpg" : "../Upload/Avatar/Male.jpg";
                            }
                            catch
                            {
                                tempStudent.Attribute3 = tempStudent.Gender == "Female" ? "../Upload/Avatar/Female.jpg" : "../Upload/Avatar/Male.jpg";
                            }
                            if (studentRegistrationList.Count > 0 && studentRegistrationList != null)
                            {

                                tempStudent.Attribute1 = "";
                                tempStudent.Attribute2 = "";
                                StudentRegistration tempStudentRegistration = studentRegistrationList.Where(x => x.StudentId == tempStudent.StudentID).SingleOrDefault();
                                if (tempStudentRegistration != null)
                                {
                                    //This field Temporary Use for Student Registration Number
                                    tempStudent.Attribute1 = tempStudentRegistration.RegistrationNo;
                                    //This field Temporary Use for  Student Session
                                    tempStudent.Attribute2 = tempStudentRegistration.Attribute1;//attritube pass session name using at Query
                                                                                                //This field Temporary Use for Studet Photo Path
                                }
                            }
                        }

                        stdList = stdList.OrderBy(x => x.Roll).ToList();
                        GvStudent.DataSource = stdList;
                        GvStudent.DataBind();
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    LogicLayer.BusinessObjects.Student student = StudentManager.GetByRoll(txtStudent.Text.Trim());
                    if (student != null)
                    {
                        if (studentRegistrationList.Count > 0 && studentRegistrationList != null)
                        {
                            student.Attribute1 = studentRegistrationList[0].RegistrationNo;
                            student.Attribute2 = studentRegistrationList[0].Attribute1;//attritube pass session name using at Query
                        }

                        if (base.ProgramAccessAuthentication(userObj, student.ProgramID))
                        {
                            List<LogicLayer.BusinessObjects.Student> stdlist = new List<LogicLayer.BusinessObjects.Student>();
                            stdlist.Add(student);
                            GvStudent.DataSource = stdlist;
                            GvStudent.DataBind();
                        }
                        else
                            lblMessage.Text = "Access denied";
                    }
                    else
                        lblMessage.Text = "Student not found!";
                }
                catch { }
            }
        }

        private void ClearAll()
        {
            GvStudent.DataSource = null;
            GvStudent.DataBind();
        }


        [System.Web.Services.WebMethod]
        public static string LoadCertificateData(string RollList, int Session, string isProvisional)
        {


          //  BUP_UCAM_LOG_Core_Entities dbLog = new BUP_UCAM_LOG_Core_Entities();
            UCAMDAL.UCAMEntities dbLog = new UCAMDAL.UCAMEntities();
           
            //int type = 0;
            //if (chkProvisional.Checked)
            //{
            //    type = 2;
            //}
            //else
            //{
            //    type = 1;
            //}
            List<rStudentGradeCertificateInfo> studentGeneralInfo = new List<rStudentGradeCertificateInfo>();

            List<StudentRoll> rollList = (List<StudentRoll>)JsonConvert.DeserializeObject(RollList, typeof(List<StudentRoll>));

            List<IList> TotalList = new List<IList>();

            int Count = 0, DegreeCom = 0;
            #region Degree Completion Check
            foreach (var roll in rollList)
            {
                Count++;
                Student student = StudentManager.GetByRoll(roll.Roll);
                if (student != null)
                {
                    LogicLayer.BusinessObjects.DegreeCompletion degreeCompletion = DegreeCompletionManager.GetByStudentId(student.StudentID);

                    if (degreeCompletion != null && degreeCompletion.IsDegreeComplete == true)
                    {
                        DegreeCom++;
                    }
                }
            }
            #endregion

            if (Count != 0 && Count == DegreeCom)
            {

                string logOk = CheckLogApproval(rollList, isProvisional);

                if (!string.IsNullOrEmpty(logOk))
                {
                    //RptStudentCertificate myClass = new RptStudentCertificate();
                    //myClass.ShowMessage(logOk);
                    //Page page = HttpContext.Current.CurrentHandler as Page;
                    //ScriptManager.RegisterStartupScript(page, page.GetType(), "Course", "alert('" + logOk + "');", true);
                    string jsonA = JsonConvert.SerializeObject(logOk, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    return jsonA;
                }

                foreach (var roll in rollList)
                {
                    Student student = StudentManager.GetByRoll(roll.Roll);
                    if (student != null)
                    {


                        studentGeneralInfo = StudentManager.GetStudentGradeCertificateInfoByRoll(student.Roll, Session, 0);
                        #region Get Logged in User Name
                        if (LoginUserLoginId != "")
                        {
                            User usr = UserManager.GetByLogInId(LoginUserLoginId);
                            if (usr != null && usr.Person != null)
                            {
                                studentGeneralInfo[0].FatherName = usr.Person.FullName;
                                UserInPerson uip = UserInPersonManager.GetById((int)usr.User_ID);
                                string Designation = "";
                                if (uip != null && uip.Person != null)
                                {
                                    Designation = uip.Person.Employee.Designation == null ? "" : uip.Person.Employee.Designation;
                                }
                                studentGeneralInfo[0].MotherName = Designation;
                            }
                        }
                        #endregion
                        TotalList.Add(studentGeneralInfo);
                        UCAMDAL.CoE_PaperGenerate modelUpdate = dbLog.CoE_PaperGenerate.Where(x => x.StudentId == student.StudentID && x.Type == isProvisional && x.IsDownloaded == false).FirstOrDefault();
                        if (modelUpdate != null)
                        {
                            modelUpdate.IsDownloaded = true;
                            modelUpdate.DownloadDate = DateTime.Now;
                            modelUpdate.DownloaderLoginId = LoginUserLoginId;
                            dbLog.Entry(modelUpdate).State = System.Data.Entity.EntityState.Modified;
                            dbLog.SaveChanges();
                        }
                        else
                        {
                            UCAMDAL.CoE_PaperGenerate logModel = new UCAMDAL.CoE_PaperGenerate();
                            logModel.AcaCalId = 0;
                            logModel.ProgramId = student.ProgramID;
                            logModel.PageName = "RptStudentCertificate.aspx";
                            logModel.PageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                            logModel.IsApproved = true;
                            logModel.IsDownloaded = true;
                            logModel.DownloadDate = DateTime.Now;
                            logModel.DownloaderLoginId = LoginUserLoginId;
                            logModel.StudentRoll = student.Roll;
                            logModel.StudentId = student.StudentID;
                            logModel.Type = isProvisional;
                            logModel.UserLoginId = LoginUserLoginId;
                            logModel.MessageType = "Certificate Download";
                            logModel.Message = "Certificate Download 1st Time by" + LoginUserLoginId + " at " + DateTime.Now;
                            logModel.EventName = "btnApprove_Click";
                            logModel.DateTime = DateTime.Now;

                            dbLog.CoE_PaperGenerate.Add(logModel);
                            dbLog.SaveChanges();
                        }


                    }

                }
            }




            //JavaScriptSerializer js = new JavaScriptSerializer();

            //var serializer = new JavaScriptSerializer();
            //var serializedResult = js.Serialize(studentGeneralInfo);

            //return serializedResult;

            string json = JsonConvert.SerializeObject(TotalList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
           // ShowMessage("");
            try
            {
                int programId = Convert.ToInt32(ucProgram.selectedValue);
                int batchId = Convert.ToInt32(ucBatch.selectedValue);
                //int sessionId = Convert.ToInt32(ucSession.selectedValue);
                int type = 0;
                if (chkProvisional.Checked)
                {
                    type = 2;
                }
                else
                {
                    type = 1;
                }
                UCAMDAL.UCAMEntities dbLog1 = new UCAMDAL.UCAMEntities();
                foreach (GridViewRow row in GvStudent.Rows)
                {
                    CheckBox ckBox = (CheckBox)row.FindControl("ChkSelect");
                    if (ckBox.Checked)
                    {
                        HiddenField hdnRoll = (HiddenField)row.FindControl("HiddenField1");
                        string roll = hdnRoll.Value;
                        Student student = StudentManager.GetByRoll(roll);
                        if (student != null)
                        {
                            List<UCAMDAL.CoE_PaperGenerate> logList = dbLog1.CoE_PaperGenerate.Where(x => x.StudentId == student.StudentID).ToList();
                            if (logList != null && logList.Count > 0)
                            {
                                if (logList.Where(x => x.IsApproved == false).FirstOrDefault() != null)
                                {

                                }
                                else
                                {
                                    if (logList.Where(x => x.IsDownloaded == false).FirstOrDefault() != null)
                                    {

                                    }
                                    else
                                    {

                                        UCAMDAL.CoE_PaperGenerate logModel = new UCAMDAL.CoE_PaperGenerate();
                                        logModel.AcaCalId = 0;
                                        logModel.ProgramId = programId;
                                        logModel.PageName = "RptStudentCertificate.aspx";
                                        logModel.PageUrl = Request.Url.AbsoluteUri;
                                       // logModel.IsApproved = false;
                                        logModel.IsApproved = true;
                                        logModel.IsDownloaded = false;
                                        logModel.StudentRoll = roll;
                                        logModel.StudentId = student.StudentID;
                                        logModel.Type = type.ToString();
                                        logModel.UserLoginId = LoginUserLoginId;
                                        logModel.MessageType = "Certificate Download";
                                        logModel.Message = "Certificate Download Requested by" + LoginUserLoginId + " at " + DateTime.Now;
                                        logModel.EventName = "btnApprove_Click";
                                        logModel.DateTime = DateTime.Now;

                                        dbLog1.CoE_PaperGenerate.Add(logModel);
                                        dbLog1.SaveChanges();


                                    }
                                }
                            }
                            else
                            {

                            }

                        }
                        else
                        {

                        }

                    }
                }
               // ShowMessage("Sent Approval Successfully.");
            }
            catch (Exception ex)
            {

            }
        }

        private static string CheckLogApproval(List<StudentRoll> rollList, string isProvisional)
        {
            try
            {
                //RptStudentCertificate myClass = new RptStudentCertificate();
                 //BUP_UCAM_LOG_Core_Entities dbLog1 = new BUP_UCAM_LOG_Core_Entities();
                UCAMDAL.UCAMEntities dbLog2 = new UCAMDAL.UCAMEntities();



                foreach (var roll in rollList)
                {
                    Student student = StudentManager.GetByRoll(roll.Roll);
                    if (student != null)
                    {
                        List<UCAMDAL.CoE_PaperGenerate> logList = dbLog2.CoE_PaperGenerate.Where(x => x.StudentId == student.StudentID && x.Type == isProvisional).ToList();
                        if (logList != null && logList.Count > 0)
                        {
                            if (logList.Where(x => x.IsApproved == false).FirstOrDefault() != null)
                            {
                               // return "Approval Pending For Required Download";
                            }
                            else
                            {
                                if (logList.Where(x => x.IsDownloaded == false).FirstOrDefault() != null)
                                {

                                }
                                else
                                {
                                   // return "Need Approval";
                                }
                            }
                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        return "Student Not Found !";
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadCertificateDataPrev(string RollList, int Session, string isProvisional)
        {

            List<rStudentGradeCertificateInfo> studentGeneralInfo = new List<rStudentGradeCertificateInfo>();

            List<StudentRoll> rollList = (List<StudentRoll>)JsonConvert.DeserializeObject(RollList, typeof(List<StudentRoll>));

            List<IList> TotalList = new List<IList>();

            int Count = 0, DegreeCom = 0;
            #region Degree Completion Check
            foreach (var roll in rollList)
            {
                Count++;
                Student student = StudentManager.GetByRoll(roll.Roll);
                if (student != null)
                {
                    LogicLayer.BusinessObjects.DegreeCompletion degreeCompletion = DegreeCompletionManager.GetByStudentId(student.StudentID);

                    if (degreeCompletion != null && degreeCompletion.IsDegreeComplete == true)
                    {
                        DegreeCom++;
                    }
                }
            }
            #endregion

            if (Count != 0 && Count == DegreeCom)
            {



                foreach (var roll in rollList)
                {
                    Student student = StudentManager.GetByRoll(roll.Roll);
                    if (student != null)
                    {


                        studentGeneralInfo = StudentManager.GetStudentGradeCertificateInfoByRoll(student.Roll, Session, 0);
                        #region Get Logged in User Name
                        if (LoginUserLoginId != "")
                        {
                            User usr = UserManager.GetByLogInId(LoginUserLoginId);
                            if (usr != null && usr.Person != null)
                            {
                                studentGeneralInfo[0].FatherName = usr.Person.FullName;
                                UserInPerson uip = UserInPersonManager.GetById((int)usr.User_ID);
                                string Designation = "";
                                if (uip != null && uip.Person != null)
                                {
                                    Designation = uip.Person.Employee.Designation == null ? "" : uip.Person.Employee.Designation;
                                }
                                studentGeneralInfo[0].MotherName = Designation;
                            }
                        }
                        #endregion
                        TotalList.Add(studentGeneralInfo);



                    }

                }
            }




            //JavaScriptSerializer js = new JavaScriptSerializer();

            //var serializer = new JavaScriptSerializer();
            //var serializedResult = js.Serialize(studentGeneralInfo);

            //return serializedResult;

            string json = JsonConvert.SerializeObject(TotalList, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;

        }


        //[System.Web.Services.WebMethod]
        //public static string LoadCertificateSave(string file, string Roll)
        //{
        //    try
        //    {
        //        Student st = StudentManager.GetByRoll(Roll);
        //        if (st == null)
        //        {
        //            return string.Empty;
        //        }
        //        char[] spearator = { ',', ' ', '-', ':', '.' };
        //        string[] NameStr = st.Name.Trim().Split(spearator);
        //        string lastName = NameStr[NameStr.Length - 1];
        //        int fileTypeId = Convert.ToInt32(CommonEnum.ArchiveFileType.Certificate);
        //        LogicLayer.EF.EDMX.ArchiveFileType ft = null;
        //        int? fileversion = 1;
        //        using (var dbEF1 = new LogicLayer.EF.EDMX.BUP_UCAMEntities())
        //        {
        //            ft = dbEF1.ArchiveFileTypes.Find(fileTypeId);
        //            LogicLayer.EF.EDMX.ArchiveFileDetail afd = dbEF1.ArchiveFileDetails.Where(x => x.FileName == ft.FileTypeName + "_" + st.Roll + "_" + lastName + ".pdf" && x.IsDeleted == false && x.FileTypeID == ft.FileTypeID).FirstOrDefault();
        //            if (afd != null)
        //            {
        //                return string.Empty;
        //                //fileversion = afd.FileVersion + 1;
        //                //dbEF1.Entry(afd).State = System.Data.Entity.EntityState.Deleted;
        //                //dbEF1.SaveChanges();
        //            }
        //        }

        //        if (st != null && !string.IsNullOrEmpty(file))
        //        {

        //            string file2 = (string)JsonConvert.DeserializeObject(file, typeof(string));


        //            file2 = file2.Replace("data:application/pdf;filename=generated.pdf;base64,", "");

        //            byte[] bytes = Convert.FromBase64String(file2);


        //            #region Api CAll
        //            ////////////////////
        //            FilePropertyItem modelApi = new FilePropertyItem();
        //            modelApi.FileTypeId = ft.FileTypeID;
        //            modelApi.FileName = ft.FileTypeName + "_" + st.Roll + "_" + lastName + ".pdf";
        //            modelApi.FileData = bytes;
        //            string isLiveServerString = WebConfigurationManager.AppSettings["IsLiveServer"];
        //            string base_url = "";
        //            if (isLiveServerString == "1")
        //            {
        //                base_url = "http://103.157.135.78:8083/";
        //            }
        //            else
        //            {
        //                //base_url = "http://103.157.135.78:8083/";
        //            }

        //            var url = base_url + "api/ArchiveManage/PostArchiveFile";
        //            var json = JsonConvert.SerializeObject(modelApi);
        //            var data = new StringContent(json, Encoding.UTF8, "application/json");
        //            var client = new HttpClient();
        //            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //            var response = client.PostAsync(url, data);
        //            response.Wait();
        //            var result = response.Result;
        //            var contentString = result.Content.ReadAsStringAsync();
        //            int statusCode = (int)result.StatusCode;
        //            if (result.IsSuccessStatusCode == true)
        //            {
        //                ResponseAPI resp = JsonConvert.DeserializeObject<ResponseAPI>(contentString.Result);
        //                if (resp.ResponseCode == 200)
        //                {
        //                    LogicLayer.EF.EDMX.ArchiveFileDetail archiveModel = new LogicLayer.EF.EDMX.ArchiveFileDetail();
        //                    archiveModel.FileName = ft.FileTypeName + "_" + st.Roll + "_" + lastName + ".pdf";
        //                    archiveModel.FileTypeID = ft.FileTypeID;
        //                    archiveModel.FileVersion = fileversion;
        //                    archiveModel.IsDeleted = false;
        //                    archiveModel.UploadBy = LoginUserUserId;
        //                    archiveModel.UploadPersonName = LoginUserLoginId;
        //                    archiveModel.UploadDate = DateTime.Now;
        //                    archiveModel.CreatedBy = LoginUserUserId;
        //                    archiveModel.CreatedDate = DateTime.Now;
        //                    archiveModel.ArchivedPageName = "RptStudentCertificate.aspx";
        //                    archiveModel.ProgramID = st.ProgramID;
        //                    archiveModel.BatchID = st.BatchId;
        //                    using (var dbEF = new LogicLayer.EF.EDMX.BUP_UCAMEntities())
        //                    {
        //                        dbEF.ArchiveFileDetails.Add(archiveModel);
        //                        dbEF.SaveChanges();
        //                    }



        //                }
        //                else
        //                {

        //                }
        //            }
        //            else
        //            {

        //            }

        //            ////////////////// 
        //            #endregion



        //            //using (FileStream fs = new FileStream(BasePath + ft.FileTypeName + "_" + st.Roll + "_" + lastName + ".pdf", FileMode.Create))
        //            //{
        //            //    fs.Write(bytes, 0, bytes.Length);
        //            //}





        //            //using (var dbEF = new LogicLayer.EF.EDMX.BUP_UCAMEntities())
        //            //{
        //            //    LogicLayer.EF.EDMX.ArchiveFileDetail archiveModel = new LogicLayer.EF.EDMX.ArchiveFileDetail();
        //            //    archiveModel.FileName = ft.FileTypeName + "_" + st.Roll + "_" + lastName + ".pdf";
        //            //    archiveModel.FileTypeID = ft.FileTypeID;
        //            //    //archiveModel.FilePath = "~/Upload/Archive/" + ft.FileTypeName + "_" + st.Roll + "_" + lastName + ".pdf";
        //            //    archiveModel.FileVersion = fileversion;
        //            //    archiveModel.IsDeleted = false;
        //            //    archiveModel.UploadBy = LoginUserUserId;
        //            //    archiveModel.UploadPersonName = LoginUserLoginId;
        //            //    archiveModel.UploadDate = DateTime.Now;
        //            //    archiveModel.CreatedBy = LoginUserUserId;
        //            //    archiveModel.CreatedDate = DateTime.Now;
        //            //    archiveModel.ArchivedPageName = "RptStudentCertificate.aspx";
        //            //    archiveModel.ProgramID = st.ProgramID;
        //            //    archiveModel.BatchID = st.BatchId;

        //            //    dbEF.ArchiveFileDetails.Add(archiveModel);
        //            //    dbEF.SaveChanges();
        //            //}
        //        }

        //        return string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        return string.Empty;
        //    }

        //}


        public class StudentRoll
        {
            public string Roll { get; set; }
        }




        protected void GvStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int studentId = Convert.ToInt32(e.CommandArgument);
            LogicLayer.BusinessObjects.Student studentObj = new LogicLayer.BusinessObjects.Student();
            studentObj = StudentManager.GetById(studentId);
            //HiddenStudentId.Value = Convert.ToString(studentId);

            if (e.CommandName == "StudentEdit")
            {
                if (studentObj != null)
                {
                    SessionManager.SaveObjToSession(studentObj, "StudentEdit");
                    Response.Redirect("~/StudentManagement/Profile/StudentProfile.aspx");
                }
            }
        }

        public class ResponseAPI
        {
            public int ResponseCode { get; set; }
            public string ResponseStatus { get; set; }
            public string ResponseMessage { get; set; }
            public object ResponseData { get; set; }
        }

        public class FileGetModel
        {
            public int FileTypeId { get; set; }
            public string FileName { get; set; }
        }

        public class FilePropertyItem
        {
            public string FileName { get; set; }
            public int FileTypeId { get; set; }
            public byte[] FileData { get; set; }
        }



    }
}