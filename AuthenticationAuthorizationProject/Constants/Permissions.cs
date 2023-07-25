using System.Reflection;

namespace AuthenticationAuthorizationProject.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"
            };
        }
        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));

            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

            return allPermissions;
        }
        public static class Factory
        {
            public const string View   = "Permissions.Factory.View";
            public const string Create = "Permissions.Factory.Create";
            public const string Edit   = "Permissions.Factory.Edit";
            public const string Delete = "Permissions.Factory.Delete";
        }
    }
}
