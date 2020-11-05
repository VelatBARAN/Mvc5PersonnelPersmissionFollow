using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.Entities.Messages
{
    public enum ErrorMessageCode
    {
        UsernameAlreadyExists = 101,
        TCAlreadyExist = 102,
        PersonnelCouldNotRegister = 103,
        PersonnelCouldNotUpdate = 104,
        ZonePersonnelAlreadyExist = 105,
        PersonnelAllowDayNotRemain = 106,
        PersonnelNotTotalAllowDay = 107,
        RemainAllowDayLessThanRequiestAllowDay = 108,
        PersonnelCouldNotPermission = 109,
        UserNotFound = 110,
        PersonnelPermissionCouldNotCancelled = 111,
        UserAlreaydExists = 112,
        EmailAlreadyExists = 113,
        UserCouldNotRegister = 114,
        UserCouldNotUpdate = 115,
        UserCouldNotDeleted = 116,
        UsernameOrPasswordWrong = 117,
        EmailColudNotFind = 118,
        CouldNotSendPassword = 119,
        PersonnelLeavedWork = 120,
        PersonnelIsGivedWorkExit = 121,
    }
}
