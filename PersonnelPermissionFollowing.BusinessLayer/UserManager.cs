using PersonnelPermissionFollowing.BusinessLayer.Abstract;
using PersonnelPermissionFollowing.BusinessLayer.Results;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using PersonnelPermissionFollowing.Entities.ValueObjects;
using PersonnelPermissionFollowing.Common.Helpers;

namespace PersonnelPermissionFollowing.BusinessLayer
{
    public class UserManager : ManagerBase<Users>
    {
        private BusinessLayerResult<Users> layerResult = new BusinessLayerResult<Users>();

        public BusinessLayerResult<Users> LoginUser(LoginViewModel data)
        {
            string pass = Crypto.Hash(data.Password.ToString(), "MD5");
            layerResult.Result = Find(x => x.Username == data.Username && x.Password == pass);

            if (layerResult.Result != null)
            {
                if (layerResult.Result.Personnels.ExitOfJobDatetime != null)
                {
                    layerResult.AddError(ErrorMessageCode.PersonnelIsGivedWorkExit, "Personel iş çıkışı verildi.");
                }                
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.UsernameOrPasswordWrong, "Kullanıcı adı veya şifre hatalı.");
            }

            return layerResult;
        }

        public BusinessLayerResult<Users> InsertUser(Users data)
        {
            layerResult.Result = Find(x => x.PersonnelsId == data.PersonnelsId || x.Email == data.Email || x.Username == data.Username);

            if (layerResult.Result != null)
            {
                if (layerResult.Result.PersonnelsId == data.PersonnelsId)
                    layerResult.AddError(ErrorMessageCode.UserAlreaydExists, "Bu personel zaten kullanıcı olarak kayıtlıdır");
                else if (layerResult.Result.Email == data.Email)
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu email adresi zaten kayıtlı.");
                else if (layerResult.Result.Username == data.Username)
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu kullanıcı adı adresi zaten kayıtlı.");
                return layerResult;
            }

            Users user = new Users()
            {
                PersonnelsId = data.PersonnelsId,
                Username = data.Username,
                Email = data.Email,
                Password = Crypto.Hash(data.Password.ToString(), "MD5"),
                RePassword = Crypto.Hash(data.RePassword.ToString(), "MD5"),
                AuthorityId = data.AuthorityId
            };

            if (Insert(user) == 0)
            {
                layerResult.AddError(ErrorMessageCode.UserCouldNotRegister, "Kullanıcı kayıt edilirken hata oluştu");
            }

            return layerResult;
        }

        public BusinessLayerResult<Users> ForgetPasswordUser(ForgetPasswordViewModel data)
        {
            layerResult.Result = Find(x => x.Email == data.Email);
            if (layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.EmailColudNotFind, "Lütfen sistemde kayıtlı bir email adresi giriniz");
            }
            else
            {
                int newnumber = 0;
                Random rnd = new Random();
                newnumber = rnd.Next();
                layerResult.Result.Password = Crypto.Hash(newnumber.ToString(), "MD5");
                layerResult.Result.RePassword = Crypto.Hash(newnumber.ToString(), "MD5");

                if (Update(layerResult.Result) > 0)
                {
                    string body = $"Merhaba {layerResult.Result.Personnels.Name} {layerResult.Result.Personnels.Surname}; <br> <br> Yeni Şİfre : {newnumber}";
                    MailHelper.SendMail(body, layerResult.Result.Email, "Personel İzin Takip Sistemi - Yeni Şİfre Talebi", true);
                }
                else
                {
                    layerResult.AddError(ErrorMessageCode.CouldNotSendPassword, "Şifre gönderilirken hata oluştu!");
                }
            }

            return layerResult;
        }

        public BusinessLayerResult<Users> UpdateUser(Users data)
        {
            layerResult.Result = Find(x => x.PersonnelsId == data.PersonnelsId || x.Email == data.Email || x.Username == data.Username);

            if (layerResult.Result != null && layerResult.Result.Id != data.Id)
            {
                if (layerResult.Result.PersonnelsId == data.PersonnelsId)
                    layerResult.AddError(ErrorMessageCode.UserAlreaydExists, "Bu personel zaten kullanıcı olarak kayıtlıdır");
                else if (layerResult.Result.Email == data.Email)
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu email adresi zaten kayıtlı.");
                else if (layerResult.Result.Username == data.Username)
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu kullanıcı adı zaten kayıtlı.");
                return layerResult;
            }

            layerResult.Result = Find(x => x.Id == data.Id);
            layerResult.Result.PersonnelsId = data.PersonnelsId;
            layerResult.Result.Email = data.Email;
            layerResult.Result.Username = data.Username;
            layerResult.Result.Password = Crypto.Hash(data.Password.ToString(), "MD5");
            layerResult.Result.RePassword = Crypto.Hash(data.RePassword.ToString(), "MD5");
            layerResult.Result.AuthorityId = data.AuthorityId;

            if (Update(layerResult.Result) == 0)
            {
                layerResult.AddError(ErrorMessageCode.UserCouldNotUpdate, "Kullanıcı güncellenirken hata oluştu");
            }

            return layerResult;
        }

        public BusinessLayerResult<Users> ShowUserProfileById(int? id)
        {
            layerResult.Result = Find(x => x.Id == id);

            if(layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı");
            }

            return layerResult;
        }

        public BusinessLayerResult<Users> ChangePassword(ChangePasswordViewModel data)
        {
            layerResult.Result = Find(x => x.Username == data.Username);
            if (layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı");
                return layerResult;
            }

            layerResult.Result.Password = Crypto.Hash(data.Password.ToString(), "MD5");
            layerResult.Result.RePassword = Crypto.Hash(data.RePassword.ToString(), "MD5");

            if (Update(layerResult.Result) == 0)
            {
                layerResult.AddError(ErrorMessageCode.UserCouldNotUpdate, "Şifre değiştirilirken hata oluştu");
            }

            return layerResult;
        }

        //public BusinessLayerResult<Users> DeleteUser(int? id)
        //{
        //    layerResult.Result = Find(x => x.Id == id.Value);

        //    if (layerResult.Result != null)
        //    {
        //        if (Delete(layerResult.Result) == 0)
        //        {
        //            layerResult.AddError(ErrorMessageCode.UserCouldNotDeleted, "Kullanıcı silinirken hata oluştu");
        //            return layerResult;
        //        }
        //    }
        //    else
        //    {
        //        layerResult.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
        //    }

        //    return layerResult;
        //}
    }
}
