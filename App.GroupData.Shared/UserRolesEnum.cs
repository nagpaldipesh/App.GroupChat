using System.ComponentModel;

namespace App.GroupData.Shared {
    public enum UserRoles {

        [Description("Super Admin")]
        SuperAdmin = 1,
        [Description("Admin")]
        Admin = 2,
        [Description("User")]
        User = 3
    }
}