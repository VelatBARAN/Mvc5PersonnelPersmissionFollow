USE [PersonnelPermissionFollowing]
GO
/****** Object:  Trigger [dbo].[trgUpdatePersonnelPermission]    Script Date: 06.10.2020 14:09:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER trigger [dbo].[trgUpdatePersonnelPermission]
on [dbo].[PersonnelPermissionRequest]
after update
as
begin

declare @personnelId int
declare @oldrequiestday int
declare @newrequiestday int
declare @totalday int
declare @usingday int
declare @remainday int
declare @oldpermissiontipId int
declare @newpermissiontipId int
declare @oldpermissiontipname nvarchar(50)
declare @newpermissiontipname nvarchar(50)

select @personnelId = PersonnelsId , @oldrequiestday = NumberOfDays , @oldpermissiontipId = PersonnelPermissionTipsId from deleted
select @personnelId = PersonnelsId , @newrequiestday = NumberOfDays , @newpermissiontipId = PersonnelPermissionTipsId from inserted
select @oldpermissiontipname = p.Name from PersonnelPermissionTips p where p.Id = @oldpermissiontipId
select @newpermissiontipname = p.Name from PersonnelPermissionTips p where p.Id = @newpermissiontipId
select @totalday=TotalAllowDay,@usingday=TotalUsingAllowDay,@remainday=TotalRemainAllowDay from Personnels where Id = @personnelId

if(@oldpermissiontipname = 'Yıllık')
begin

set @usingday = @usingday - @oldrequiestday
set @remainday = @remainday + @oldrequiestday

end;
else if(@oldpermissiontipname = 'Doğum')
begin
set @usingday = @usingday + 0
set @remainday = @remainday - 0
end;
else if(@oldpermissiontipname = 'Raporlu')
begin
set @usingday = @usingday + 0
set @remainday = @remainday - 0
end;
else if(@oldpermissiontipname = 'Vefat')
begin
set @usingday = @usingday + 0
set @remainday = @remainday - 0
end;
else if(@oldpermissiontipname = 'İdari')
begin
set @usingday = @usingday + 0
set @remainday = @remainday - 0
end;


if(@newpermissiontipname = 'Yıllık')
begin

set @usingday = @usingday + @newrequiestday
set @remainday = @remainday - @newrequiestday
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@newpermissiontipname = 'Doğum')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@newpermissiontipname = 'Raporlu')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@newpermissiontipname = 'Vefat')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@newpermissiontipname = 'İdari')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;

end;