
using System.ComponentModel;

namespace FPCS.Data.Enums
{
    public enum Role
    {
        [DescriptionAttribute("Администратор")]
        Admin = 1,
        [DescriptionAttribute("Пользователь СМО")]
        User = 2,
        [DescriptionAttribute("Пользователь ТФОМС")]
        TfomsUser = 3
    }
}
