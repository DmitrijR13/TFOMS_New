using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Enums;
using FPCS.Data.Exceptions;

namespace FPCS.Data.Repo.Impl
{
    internal class UserRepo : RepoBase<User, StudentManagementContext>, IUserRepo
    {
        public UserRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<User> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }

        public override void Remove(User entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTimeOffset.Now;
        }

        #endregion override methods

        public User Get(Int64 Id)
        {
            return GetAll().FirstOrDefault(x => x.DbUserId == Id);
        }

        public Boolean IsExistLogin(String login)
        {
            return IsExist(x => x.Login == login);
        }

        public User Add(String login, String password, String email, String firstName, String lastName, String middleInitial, Int64? smoId, Role role)
        {
            if (UnitOfWork.GetRepo<IDbUserRepo>().IsExistLogin(login)) throw new DuplicateEntityException("The login already exist");

            return Add(new User
            {
                Login = login,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                FullName = lastName + " " + firstName,
                MiddleInitial = middleInitial,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now,
                IsDeleted = false,
                IsLocked = false,
                Role = role,
                SMOId = smoId
            });
        }

        public User Update(Int64 dbUserId, String login, String password, String email, String firstName, 
            String lastName, String middleInitial, Int64? smoId, Boolean isLocked, Role role)
        {
            var dbEntity = Get(dbUserId);
            if (dbEntity == null) throw new NotFoundEntityException("Пользователь {0} не найден", dbUserId);

            dbEntity.Email = email;
            dbEntity.Login = login;
            dbEntity.IsLocked = isLocked;
            if (!String.IsNullOrEmpty(password)) dbEntity.Password = password;
            dbEntity.UpdatedDate = DateTimeOffset.Now;
            dbEntity.FirstName = firstName;
            dbEntity.LastName = lastName;
            dbEntity.MiddleInitial = middleInitial;
            dbEntity.SMOId = smoId;
            dbEntity.Role = role;

            return dbEntity;
        }

        public IQueryable<User> GetByRoles(params Role[] roles)
        {
            return GetAll().Where(x => roles.Contains(x.Role));
        }
    }
}
