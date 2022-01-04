using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.Entities.SurveyEntities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CreateSurveyRespondentsResolver : IValueResolver<CreateSurveyVM, SurveyTemplate, ICollection<UserSurveyResult>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateSurveyRespondentsResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<UserSurveyResult> Resolve(CreateSurveyVM source, SurveyTemplate destination, ICollection<UserSurveyResult> destMember, ResolutionContext context)
        {
            var results = new List<UserSurveyResult>();

            var respondents = _dbContext.Users.Where(x => source.RespondentsIds.Contains(x.Id)).ToList();

            foreach(var respondent in respondents)
            {
                var result = new UserSurveyResult
                {
                    IsCompleated = false,
                    User = respondent
                };

                results.Add(result);
            }

            return results;
        }
    }
}
