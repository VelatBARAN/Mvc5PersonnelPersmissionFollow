USE [PersonnelPermissionFollowing]
GO
/****** Object:  Trigger [dbo].[trgDeletePersonnelPermission]    Script Date: 07.10.2020 14:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER trigger [dbo].[trgDeletePersonnelPermission]
on [dbo].[PersonnelPermissionRequest]
after delete
as
begin

declare @personnelId int
declare @numberday int
declare @usingday int
declare @remainday int
declare @permissiontipId int
declare @permissionstatesId int
declare @permissiontipname nvarchar(50)

select @personnelId = PersonnelsId , @numberday = NumberOfDays , @permissiontipId = PersonnelPermissionTipsId,@permissionstatesId=PermissionStatesId from deleted
select @permissiontipname = p.Name from PersonnelPermissionTips p where p.Id = @permissiontipId
select @usingday=TotalUsingAllowDay,@remainday=TotalRemainAllowDay from Personnels where Id = @personnelId

if(@permissionstatesId = 2)
begin

if(@permissiontipname = 'Yıllık')
begin

set @usingday = @usingday - @numberday
set @remainday = @remainday + @numberday

update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@permissiontipname = 'Doğum')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@permissiontipname = 'Raporlu')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@permissiontipname = 'Vefat')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
else if(@permissiontipname = 'İdari')
begin
update Personnels set TotalRemainAllowDay = @remainday, TotalUsingAllowDay = @usingday where Id = @personnelId
end;
end
end;