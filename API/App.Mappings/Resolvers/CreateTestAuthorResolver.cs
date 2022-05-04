using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.Entities.TestEntities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateTestAuthorResolver : IValueResolver<CreateTestVM, TestTemplate, User>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTestAuthorResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Resolve(CreateTestVM source, TestTemplate destination, User destMember, ResolutionContext context)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == source.AuthorId);
        }
    }
}
