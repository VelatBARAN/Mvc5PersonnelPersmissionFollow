using PersonnelPermissionFollowing.BusinessLayer.Abstract;
using PersonnelPermissionFollowing.BusinessLayer.Results;
using PersonnelPermissionFollowing.DataAccessLayer.EntityFramework;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;

namespace PersonnelPermissionFollowing.BusinessLayer
{
    public class PersonnelManager : ManagerBase<Personnels>
    {
        private BusinessLayerResult<Personnels> layerResult = new BusinessLayerResult<Personnels>();
        private DatabaseContext db = new DatabaseContext();

        private int totalAllowDay = 0;
        private int totalWorkingYear = 0;

        public BusinessLayerResult<Personnels> InsertPersonnel(Personnels data)
        {
            // personel TC kontrolü
            layerResult.Result = Find(x => x.Tc == data.Tc);

            if (layerResult.Result != null)
            {
                layerResult.AddError(ErrorMessageCode.TCAlreadyExist, "Aynı TC nolu personel zaten kayıtlı.");
            }
            else
            {
                var a = Convert.ToInt32((DateTime.Now.Subtract(data.StartToJobDateTime.GetValueOrDefault()).TotalDays));
                totalWorkingYear = a / 365;
                if (totalWorkingYear > 5)
                {
                    totalAllowDay = (5 * 16) + ((totalWorkingYear - 5) * 22);
                }
                else if (totalWorkingYear >= 1 || totalWorkingYear <= 5)
                {
                    totalAllowDay = totalWorkingYear * 16;
                }

                int dbResult = Insert(new Personnels()
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Tc = data.Tc,
                    ProfileImage = "user.png",
                    StartToJobDateTime = data.StartToJobDateTime,
                    TotalWorkingYear = totalWorkingYear,
                    TotalAllowDay = totalAllowDay,
                    TotalUsingAllowDay = 0,
                    TotalRemainAllowDay = totalAllowDay,
                    IsStateWorking = true,
                    PersonnelDegreesId = data.PersonnelDegreesId,
                    PersonnelPositionsId = data.PersonnelPositionsId,
                    PersonnelTasksId = data.PersonnelTasksId,
                    Description = data.Description
                });

                if (dbResult == 0)
                {
                    layerResult.AddError(ErrorMessageCode.PersonnelCouldNotRegister, "Personel kayıt edilirken hata oluştu");
                }

            }

            return layerResult;
        }

        public BusinessLayerResult<Personnels> UpdatePersonnel(Personnels data)
        {
            layerResult.Result = Find(x => x.Tc == data.Tc);

            if (layerResult.Result != null && layerResult.Result.Id != data.Id)
            {
                layerResult.AddError(ErrorMessageCode.TCAlreadyExist, "Aynı TC nolu personel zaten kayıtlı.");
                return layerResult;
            }

            var a = Convert.ToInt32((DateTime.Now.Subtract(data.StartToJobDateTime.GetValueOrDefault()).TotalDays));
            totalWorkingYear = a / 365;
            if (totalWorkingYear > 5)
            {
                totalAllowDay = (5 * 16) + ((totalWorkingYear - 5) * 22);
            }
            else if (totalWorkingYear >= 1 || totalWorkingYear <= 5)
            {
                totalAllowDay = totalWorkingYear * 16;
            }

            layerResult.Result = Find(x => x.Id == data.Id);
            layerResult.Result.Name = data.Name;
            layerResult.Result.Surname = data.Surname;
            layerResult.Result.Tc = data.Tc;
            if (string.IsNullOrEmpty(data.ProfileImage) == false)
            {
                layerResult.Result.ProfileImage = data.ProfileImage;
            }
            layerResult.Result.StartToJobDateTime = data.StartToJobDateTime;
            layerResult.Result.TotalWorkingYear = totalWorkingYear;
            layerResult.Result.TotalAllowDay = totalAllowDay;
            layerResult.Result.TotalRemainAllowDay = totalAllowDay;
            if (data.ExitOfJobDatetime == null)
            {
                layerResult.Result.ExitOfJobDatetime = null;
                layerResult.Result.IsStateWorking = true;
            }
            else
            {
                layerResult.Result.ExitOfJobDatetime = data.ExitOfJobDatetime;
                layerResult.Result.IsStateWorking = false;
            }
            layerResult.Result.PersonnelDegreesId = data.PersonnelDegreesId;
            layerResult.Result.PersonnelPositionsId = data.PersonnelPositionsId;
            layerResult.Result.PersonnelTasksId = data.PersonnelTasksId;
            layerResult.Result.Description = data.Description;

            if (Update(layerResult.Result) == 0)
            {
                layerResult.AddError(ErrorMessageCode.PersonnelCouldNotUpdate, "Personel güncellenirken hata oluştu.");
            }

            return layerResult;
        }

        public List<Personnels> GetPersonnelPermissionDetails(int id)
        {
            List<Personnels> per = db.ExecuteGetPersonnelPermissionDetails(id);
            return per;
        }
    }
}
