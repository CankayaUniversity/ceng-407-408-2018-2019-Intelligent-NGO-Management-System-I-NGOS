using Ilkyar.Contracts.Entities.DTO;
using Ilkyar.Contracts.Entities.Enums;
using Ilkyar.Contracts.Helpers.Api;
using Ilkyar.Contracts.Services;
using Ilkyar.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ilkyar.Web
{
    public partial class InviteNGO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserHelper.CurrentUser.UserTypeId != (int)EnumUserType.Schoolmaster)
                Response.Redirect("Home.aspx");

            int userId = Convert.ToInt32(UserHelper.CurrentUser.UserId);
            GetUser(userId);

            labelErrorMessage.Visible = false;


        }

        private void Init_Schoolmaster(UserDTO selectedUser)
        {
            if (selectedUser.UserTypeId == (int)EnumUserType.Schoolmaster)
            {
                Schoolmaster_FirstName.Text = selectedUser.FirstName;
                Schoolmaster_LastName.Text = selectedUser.LastName;
                Schoolmaster_TCKN.Text = selectedUser.Username;
                Schoolmaster_Email.Text = selectedUser.Email;
                Schoolmaster_Phone.Text = selectedUser.PhoneNum;
                var cityId = selectedUser.CityId;
                GetCity(cityId);
                var townId = selectedUser.TownId;
                GetTown(cityId, townId);
                Schoolmaster_School.Text = selectedUser.School;
            }
        }

        protected void buttonInvite_Click(object sender, EventArgs e)
        {
            if (uplFileUploader.HasFile)
            {
                try
                {
                    string strTestFilePath = uplFileUploader.PostedFile.FileName; // This gets the full file path on the client's machine ie: c:\test\myfile.txt
                    string strTestFileName = Path.GetFileName(strTestFilePath); // use the System.IO Path.GetFileName method to get specifics about the file without needing to parse the path as a string
                    Int32 intFileSize = uplFileUploader.PostedFile.ContentLength;
                    string strContentType = uplFileUploader.PostedFile.ContentType;

                    // Convert the uploaded file to a byte stream to save to your database. This could be a database table field of type Image in SQL Server
                    Stream strmStream = uplFileUploader.PostedFile.InputStream;
                    Int32 intFileLength = (Int32)strmStream.Length;
                    byte[] bytUpfile = new byte[intFileLength + 1];
                    strmStream.Read(bytUpfile, 0, intFileLength);
                    strmStream.Close();

                    //saveFileToDb(strTestFileName, intFileSize, strContentType, bytUpfile); // or use uplFileUploader.SaveAs(Server.MapPath(".") + "filename") to save to the server's filesystem.

                    try
                    {

                        var newFile = new UploadRequirementListFileDTO();

                        newFile.Name = strTestFileName;
                        newFile.FileSize = intFileSize;
                        newFile.ContentType = strContentType;
                        newFile.AttachedFile = bytUpfile;

                        ServiceResult<long> serviceResult = new ServiceResult<long>();
                        var queryString = new Dictionary<string, string>();
                        var response = ApiHelper.CallSendApiMethod(ApiKeys.ParameterApiUrl, "UploadRequirementListFile", queryString, newFile);
                        if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                        var data = response.Content.ReadAsStringAsync().Result;
                        serviceResult = JsonConvert.DeserializeObject<ServiceResult<long>>(data);

                        if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                            throw new Exception(serviceResult.ErrorMessage);

                        var requirementListId = serviceResult.Result;

                        try
                        {
                            CreateNGOInvitationDTO newNGOInvitation = new CreateNGOInvitationDTO()
                            {
                                SchoolmasterId = UserHelper.CurrentUser.Id,
                                NumberOfStudent = Convert.ToInt32(TextBoxNumberOfStudent.Text),
                                RequirementListId = requirementListId,
                                StatusId = (int)EnumNGOInvitationStatusType.Beklemede

                            };


                            ServiceResult<long> serviceResult2 = new ServiceResult<long>();
                            var queryString2 = new Dictionary<string, string>();
                            var response2 = ApiHelper.CallSendApiMethod(ApiKeys.ProjectApiUrl, "CreateNGOInvitation", queryString2, newNGOInvitation);
                            if (!response2.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                            var data2 = response2.Content.ReadAsStringAsync().Result;
                            serviceResult2 = JsonConvert.DeserializeObject<ServiceResult<long>>(data2);

                            if (serviceResult2.ServiceResultType != EnumServiceResultType.Success)
                                throw new Exception(serviceResult2.ErrorMessage);

                            labelErrorMessage.Text = "ILKYAR davet edildi.";
                            labelErrorMessage.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            labelErrorMessage.Text = ex.Message;
                            labelErrorMessage.Visible = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        labelErrorMessage.Text = ex.Message;
                        labelErrorMessage.Visible = true;
                    }

                }
                catch (Exception err)
                {
                    labelErrorMessage.Text = "Dosya yüklenemedi.";
                }
            }
            else
            {
                labelErrorMessage.Text = "İhtiyaç Listesi seçmediniz.";
            }
        }


        private void GetUser(int userId)
        {
            try
            {
                ServiceResult<UserDTO> serviceResult = new ServiceResult<UserDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("userId", userId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.UserApiUrl, "GetUser", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<UserDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var user = serviceResult.Result;

                Session["ViewUser_UserId"] = user.UserId;
                Session["ViewUser_UserTypeId"] = user.UserTypeId;
                
                Init_Schoolmaster(user);

            }
            catch (Exception ex)
            {
                Response.Redirect("ListUsers.aspx");
            }
        }
        private void GetCity(int? cityId)
        {
            try
            {
                ServiceResult<CityDTO> serviceResult = new ServiceResult<CityDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("cityId", cityId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetCity", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<CityDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var city = serviceResult.Result;

                if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
                    Schoolmaster_City.Text = city.Name;

            }
            catch (Exception ex)
            {
                Response.Redirect("Home.aspx");
            }
        }

        private void GetTown(int? cityId, int? townId)
        {
            try
            {
                ServiceResult<TownDTO> serviceResult = new ServiceResult<TownDTO>();
                var queryString = new Dictionary<string, string>();
                queryString.Add("cityId", cityId.ToString());
                queryString.Add("townId", townId.ToString());
                var response = ApiHelper.CallGetApiMethod(ApiKeys.ParameterApiUrl, "GetTown", queryString);
                if (!response.IsSuccessStatusCode) throw new Exception("Hata oluştu!");
                var data = response.Content.ReadAsStringAsync().Result;
                serviceResult = JsonConvert.DeserializeObject<ServiceResult<TownDTO>>(data);

                if (serviceResult.ServiceResultType != EnumServiceResultType.Success)
                    throw new Exception(serviceResult.ErrorMessage);

                if (serviceResult.Result == null)
                    throw new Exception(serviceResult.ErrorMessage);

                var town = serviceResult.Result;

                if (UserHelper.UserTypeId == (int)EnumUserType.Schoolmaster)
                    Schoolmaster_Town.Text = town.Name;
            }
            catch (Exception ex)
            {
                Response.Redirect("Home.aspx");
            }
        }
    }
}