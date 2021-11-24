using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.UserMiddleware.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateCoachUserResolver : IValueResolver<CreateCoachVM, Coach, User>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateCoachUserResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User Resolve(CreateCoachVM source, Coach destination, User destMember, ResolutionContext context)
        {
            var user = new User
            {
                Email = source.Email,
                Username = source.Email.ToLower(),
                Name = source.Name,
                MiddleName = source.MiddleName,
                Surname = source.Surname,
                PasswordHash = PasswordHashHelper.HashPassword(source.Password),
                Role = _dbContext.Roles.FirstOrDefault(x => x.Name == "coach"),
                LastPasswordSet = DateTime.Now,
                BadLogonCount = 0
            };

            return user;
        }
    }
}
