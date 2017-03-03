using System;

using FPCS.Data.Entities;
using FPCS.Data.Enums;
using System.Linq;

namespace FPCS.Data.Repo
{
    public interface IUserRepo : IRepoBase<User>
    {
        User Get(Int64 id);

        Boolean IsExistLogin(String login);

        User Add(String login, String password, String email, String firstName, String lastName, String middleInitial, Int64? smoId, Role role);

        User Update(Int64 dbUserId, String login, String password, String email, String firstName, String lastName, String middleInitial, Int64? smoId, Boolean isLocked, Role role);

        IQueryable<User> GetByRoles(params Role[] roles);
    }
}
