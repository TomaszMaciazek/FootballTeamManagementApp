using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Mappings.Resolvers
{
    public class CreateTestRespondentsResolver : IValueResolver<CreateTestVM, TestTemplate, ICollection<UserTestResult>>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTestRespondentsResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<UserTestResult> Resolve(CreateTestVM source, TestTemplate destination, ICollection<UserTestResult> destMember, ResolutionContext context)
        {
            var results = new List<UserTestResult>();

            var respondents = _dbContext.Users.Where(x => source.RespondentsIds.Contains(x.Id)).ToList();

            foreach (var respondent in respondents)
            {
                var result = new UserTestResult
                {
                    IsCompleated = false,
                    Passed = null,
                    ScoredPoints = null,
                    User = respondent
                };

                results.Add(result);
            }

            return results;
        }
    }
}
