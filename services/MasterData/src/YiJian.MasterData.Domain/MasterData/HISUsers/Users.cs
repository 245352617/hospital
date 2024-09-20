using System;

namespace YiJian.MasterData.HISUsers;

public class Users
{
    public int totalCount { get; set; }
    public User[] items { get; set; }
}

public class User
{
    public string userName { get; set; }
    public string normalizedUserName { get; set; }
    public string name { get; set; }
    public string passwordHash { get; set; }
    public DateTime? lockoutEnd { get; set; }
    public bool lockoutEnabled { get; set; }
    public int accessFailedCount { get; set; }
    public string phoneNumber { get; set; }
    public bool phoneNumberConfirmed { get; set; }
    public string email { get; set; }
    public string normalizedEmail { get; set; }
    public bool emailConfirmed { get; set; }
    public bool twoFactorEnabled { get; set; }
    public string securityStamp { get; set; }
    public DateTime creationTime { get; set; }
    public string department { get; set; }
    public string signature { get; set; }
    public int branch { get; set; }
    public string position { get; set; }
    public string technicalTitle { get; set; }
    public Role[] roles { get; set; }
    public object[] claims { get; set; }
    public Department[] departments { get; set; }
    public Extraproperties extraProperties { get; set; }
    public string concurrencyStamp { get; set; }
    public string id { get; set; }
}

public class Extraproperties
{
}

public class Role
{
    public string name { get; set; }
    public string normalizedName { get; set; }
    public bool isDefault { get; set; }
    public bool isPublic { get; set; }
    public bool isStatic { get; set; }
    public string remark { get; set; }
    public object users { get; set; }
    public object claims { get; set; }
    public object menus { get; set; }
    public Extraproperties1 extraProperties { get; set; }
    public string concurrencyStamp { get; set; }
    public string id { get; set; }
}

public class Extraproperties1
{
}

public class Department
{
    public string parentId { get; set; }
    public string code { get; set; }
    public string displayName { get; set; }
    public string hisCode { get; set; }
    public bool enabled { get; set; }
    public string intro { get; set; }
    public DateTime creationTime { get; set; }
    public object[] children { get; set; }
    public object[] users { get; set; }
    public Extraproperties2 extraProperties { get; set; }
    public string concurrencyStamp { get; set; }
    public string id { get; set; }
}

public class Extraproperties2
{
}
