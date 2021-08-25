using App.Model.Entities.SurveyEntities.QuestionTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Configurations
{
    public class BoolSurveyQuestionTemplateConfiguration : IEntityTypeConfiguration<BoolSurveyQuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<BoolSurveyQuestionTemplate> builder)
        {
            builder.HasMany(x => x.UserAnswers)
                .WithOne(x => x.Question);
        }
    }
}
