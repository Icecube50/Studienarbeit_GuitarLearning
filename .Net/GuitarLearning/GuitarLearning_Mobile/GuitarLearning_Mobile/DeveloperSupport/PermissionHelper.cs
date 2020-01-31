using Plugin.Permissions;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.DeveloperSupport
{
    /// <summary>
    /// Class to handle all permission requests
    /// </summary>
    public static class PermissionHelper
    {
        /// <summary>
        /// Generic method to request any permission
        /// </summary>
        /// <param name="permission">Permission to request</param>
        /// <returns>Task, so the request can be made asynchronously</returns>
        public static async Task<Plugin.Permissions.Abstractions.PermissionStatus> RequestPermission(Plugin.Permissions.Abstractions.Permission permission)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { permission });
                    status = results[permission];
                }
            }
            return status;
        }
        /// <summary>
        /// Ask for all permissions needed by the application
        /// </summary>
        /// <exception cref="System.Exception">Throws an exception when the permission is denied. The application can't function properly without those permissions.</exception>
        public static void AskForAllPermissions()
        {
            Task.Run(async () =>
            {
                var status = await RequestPermission(Plugin.Permissions.Abstractions.Permission.Microphone);
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted) throw new System.Exception("Permission [Microphone] not granted");
            });
            Task.Run(async () =>
            {
                var status = await RequestPermission(Plugin.Permissions.Abstractions.Permission.Storage);
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted) throw new System.Exception("Permission [Storage] not granted");
            });
        }
    }
}
