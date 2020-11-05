using PersonnelPermissionFollowing.BusinessLayer.Abstract;
using PersonnelPermissionFollowing.BusinessLayer.Results;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.Entities.Messages;
using System;

namespace PersonnelPermissionFollowing.BusinessLayer
{
    public class PersonnelPermissionRequestManager : ManagerBase<PersonnelPermissionRequest>
    {
        private BusinessLayerResult<PersonnelPermissionRequest> layerResult = new BusinessLayerResult<PersonnelPermissionRequest>();
        private PersonnelManager personnelManager = new PersonnelManager();

        public BusinessLayerResult<PersonnelPermissionRequest> InsertPersonnelPermissionRequest(PersonnelPermissionRequest data)
        {
            Personnels personnels = personnelManager.Find(x => x.Id == data.PersonnelsId);

            if (data.PersonnelPermissionTipsId == 2)
            {
                if (personnels.TotalRemainAllowDay == 0 ||
                     personnels.TotalAllowDay == 0 ||
                     (personnels.TotalRemainAllowDay < data.NumberofDays))
                {
                    if (personnels.TotalRemainAllowDay == 0 || personnels.TotalAllowDay == 0)
                        layerResult.AddError(ErrorMessageCode.PersonnelAllowDayNotRemain, "Personelin şu an izni bulunmamaktadır.");
                    else if (personnels.TotalRemainAllowDay < data.NumberofDays)
                        layerResult.AddError(ErrorMessageCode.RemainAllowDayLessThanRequiestAllowDay, "Talep edilen izin kalan izinden daha fazla.");

                    return layerResult;
                }
            }

                int dbResult = Insert(new PersonnelPermissionRequest()
                {
                    PersonnelsId = data.PersonnelsId,
                    PersonnelPermissionTipsId = data.PersonnelPermissionTipsId,
                    NumberofDays = data.NumberofDays,
                    PermissionStartDatetime = data.PermissionStartDatetime,
                    PermissionEndDatetime = data.PermissionEndDatetime,
                    PermissionStatesId = 1
                });
                if (dbResult == 0)
                {
                    layerResult.AddError(ErrorMessageCode.PersonnelCouldNotPermission, "Personele izin verilirken hata oluştu");
                }

            return layerResult;
        }

        public BusinessLayerResult<PersonnelPermissionRequest> UpdatePersonnelPermissionRequest(PersonnelPermissionRequest data)
        {
            layerResult.Result = Find(x => x.Id == data.Id);
            Personnels personnels = personnelManager.Find(x => x.Id == data.PersonnelsId);

            if (data.PersonnelPermissionTipsId == 2)
            {
                if (personnels.TotalRemainAllowDay == 0 ||
                     personnels.TotalAllowDay == 0 ||
                     (personnels.TotalRemainAllowDay < data.NumberofDays))
                {
                    if (personnels.TotalRemainAllowDay == 0 || personnels.TotalAllowDay == 0)
                        layerResult.AddError(ErrorMessageCode.PersonnelAllowDayNotRemain, "Personelin şu an izni bulunmamaktadır.");
                    else if (personnels.TotalRemainAllowDay < data.NumberofDays)
                        layerResult.AddError(ErrorMessageCode.RemainAllowDayLessThanRequiestAllowDay, "Talep edilen izin kalan izinden daha fazla.");

                    return layerResult;
                }
            }

                layerResult.Result.PersonnelsId = data.PersonnelsId;
                layerResult.Result.PersonnelPermissionTipsId = data.PersonnelPermissionTipsId;
                layerResult.Result.NumberofDays = data.NumberofDays;
                layerResult.Result.PermissionStartDatetime = data.PermissionStartDatetime;
                layerResult.Result.PermissionEndDatetime = data.PermissionEndDatetime;

                if (Update(layerResult.Result) == 0)
                {
                    layerResult.AddError(ErrorMessageCode.PersonnelCouldNotPermission, "Personel güncellenirken hata oluştu");
                }
            return layerResult;

        }

        public BusinessLayerResult<PersonnelPermissionRequest> CancelPersonnelPermissionRequest(PersonnelPermissionRequest data)
        {
            layerResult.Result = Find(x => x.Id == data.Id);

            if(layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.UserNotFound, "Personel bulunamadı.");
                return layerResult;
            }

            layerResult.Result.PermissionStatesId = 3;
            layerResult.Result.ModifiedOnDatetime = DateTime.Now;
            layerResult.Result.Users = data.Users;

            if (Update(layerResult.Result) == 0)
            {
                layerResult.AddError(ErrorMessageCode.PersonnelPermissionCouldNotCancelled, "Personele verilen izin iptal edilirken hata olutu");
            }

            return layerResult;
        }

        public BusinessLayerResult<PersonnelPermissionRequest> ApprovePersonnelPermissionRequest(PersonnelPermissionRequest data)
        {
            layerResult.Result = Find(x => x.Id == data.Id);

            if (layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.UserNotFound, "Personel bulunamadı.");
                return layerResult;
            }

            layerResult.Result.PermissionStatesId = 2;
            layerResult.Result.ModifiedOnDatetime = DateTime.Now;
            layerResult.Result.Users = data.Users;

            if (Update(layerResult.Result) == 0)
            {
                layerResult.AddError(ErrorMessageCode.PersonnelPermissionCouldNotCancelled, "Personele verilen izin onaylanırken hata olutu");
            }

            return layerResult;
        }


    }
}
