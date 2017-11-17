using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class RolesConfiguration
    {
        public enum Permissions
        {
            CreateFile = 0,
            DeleteFile = 1,
            WriteInFile = 2,
            ReadFile = 3
        }

        public enum Roles
        {
            Admins = 0,
            Readers = 1,
            Writers = 2,
        }

        public class RolesConfig
        {
            static string[] AdminsPermissions = new string[] { Permissions.CreateFile.ToString(), Permissions.DeleteFile.ToString(), Permissions.ReadFile.ToString() };
            static string[] ReadersPermissions = new string[] { Permissions.ReadFile.ToString() };
            static string[] WritersPermissions = new string[] { Permissions.WriteInFile.ToString(), Permissions.ReadFile.ToString() };
            static string[] Empty = new string[] { };

            public static string[] GetPermissions(string role)
            {
                
                switch (role)
                {
                    case "admins": return AdminsPermissions;
                    case "readers": return ReadersPermissions;
                    case "writers": return WritersPermissions;
                    default: return Empty;
                }
            }
        }
    }
}
