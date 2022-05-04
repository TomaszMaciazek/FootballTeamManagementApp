using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.Entities.SurveyEntities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateSurveyAuthorResolver : IValueResolver<CreateSurveyVM, SurveyTemplate, User>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateSurveyAuthorResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Resolve(CreateSurveyVM source, SurveyTemplate destination, User destMember, ResolutionContext context)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == source.AuthorId);
        }
    }
}
