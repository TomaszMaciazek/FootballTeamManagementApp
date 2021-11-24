using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateAdminRoleResolver : IValueResolver<CreateAdminVM, User, Role>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateAdminRoleResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role Resolve(CreateAdminVM source, User destination, Role destMember, ResolutionContext context)
        {
            return _dbContext.Roles.FirstOrDefault(x => x.Name == "player");
        }
    }
}
