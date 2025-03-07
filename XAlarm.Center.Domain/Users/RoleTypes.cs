using System.ComponentModel;

namespace XAlarm.Center.Domain.Users;

public enum RoleTypes
{
    [Description("realm-system")] RealmSystem = 0,
    [Description("realm-administrator")] RealmAdministrator = 1,
    [Description("realm-supervisor")] RealmSupervisor = 2,
    [Description("realm-basic")] RealmBasic = 3
}