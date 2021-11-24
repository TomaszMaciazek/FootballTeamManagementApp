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
    public class PlayerUserResolver : IValueResolver<CreatePlayerVM, Player, User>
    {
        private readonly IApplicationDbContext _dbContext;

        public PlayerUserResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Resolve(CreatePlayerVM source, Player destination, User destMember, ResolutionContext context)
        {
            var roles = new List<Role>
            {
                _dbContext.Roles.FirstOrDefault(x => x.Name == "player")
            };
            var user = new User
            {
                Email = source.Email,
                Username = source.Email.ToLower(),
                Name = source.Name,
                MiddleName = source.MiddleName,
                Surname = source.Surname,
                PasswordHash = PasswordHashHelper.HashPassword(source.Password),
                Roles = roles,
                LastPasswordSet = DateTime.Now,
                BadLogonCount = 0
            };


            return user;
        }
    }
}
