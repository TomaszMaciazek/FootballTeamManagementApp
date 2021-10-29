using App.UserMiddleware;
using App.UserMiddleware.Helpers;
using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public interface IDatabaseInitializer
    {
        Task MigrateAsync();
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger _logger;

        public DatabaseInitializer(IApplicationDbContext context, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
        }

        public async Task SeedAsync()
        {
            var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "/InitialData");

            if (!await _context.Languages.AnyAsync())
            {
                await SeedLanguagesAsync(directoryInfo.GetFiles("*languages*").SingleOrDefault());
            }

            if(!await _context.Countries.AnyAsync())
            {
                await SeedCountries(directoryInfo.GetFiles("*countries*").SingleOrDefault());
            }

            var roles = new Dictionary<string, List<Permission>>() {
                    { "admin", new List<Permission>() {
                        Permissions.Users,
                        Permissions.AdminSettings,
                        Permissions.Trainings,
                        Permissions.TrainingsEdit,
                        Permissions.TrainingsActivate,
                        Permissions.TrainingsDeactivate,
                        Permissions.TrainingsAdd,
                        Permissions.TrainingsDelete,
                        Permissions.Matches,
                        Permissions.MatchesEdit,
                        Permissions.MatchesActivate,
                        Permissions.MatchesDeactivate,
                        Permissions.MatchesAdd,
                        Permissions.MatchesDelete,
                        Permissions.Surveys,
                        Permissions.SurveysEdit,
                        Permissions.SurveysActivate,
                        Permissions.SurveysDeactivate,
                        Permissions.SurveysAdd,
                        Permissions.SurveysDelete,
                        Permissions.Tests,
                        Permissions.TestsEdit,
                        Permissions.TestsActivate,
                        Permissions.TestsDeactivate,
                        Permissions.TestsAdd,
                        Permissions.TestsDelete,
                        Permissions.Chats,
                        Permissions.ChatsEdit,
                        Permissions.ChatsActivate,
                        Permissions.ChatsDeactivate,
                        Permissions.ChatsAdd,
                        Permissions.ChatsDelete,
                        Permissions.News,
                        Permissions.NewsAdd,
                        Permissions.NewsEdit,
                        Permissions.NewsDelete
                    } },
                    { "coach", new List<Permission>() {
                        Permissions.Trainings,
                        Permissions.TrainingsEdit,
                        Permissions.TrainingsActivate,
                        Permissions.TrainingsDeactivate,
                        Permissions.TrainingsAdd,
                        Permissions.TrainingsDelete,
                        Permissions.Matches,
                        Permissions.MatchesEdit,
                        Permissions.MatchesActivate,
                        Permissions.MatchesDeactivate,
                        Permissions.MatchesAdd,
                        Permissions.MatchesDelete,
                        Permissions.Surveys,
                        Permissions.SurveysEdit,
                        Permissions.SurveysActivate,
                        Permissions.SurveysDeactivate,
                        Permissions.SurveysAdd,
                        Permissions.SurveysDelete,
                        Permissions.Tests,
                        Permissions.TestsEdit,
                        Permissions.TestsActivate,
                        Permissions.TestsDeactivate,
                        Permissions.TestsAdd,
                        Permissions.TestsDelete,
                        Permissions.Chats,
                        Permissions.ChatsEdit,
                        Permissions.ChatsActivate,
                        Permissions.ChatsDeactivate,
                        Permissions.ChatsAdd,
                        Permissions.ChatsDelete,
                        Permissions.News,
                        Permissions.NewsAdd,
                        Permissions.NewsEdit
                    } },
                    { $"player", new List<Permission>() {
                        Permissions.Trainings,
                        Permissions.Matches,
                        Permissions.Surveys,
                        Permissions.Tests,
                        Permissions.Chats,
                        Permissions.News
                    } }
                };

            foreach (var role in roles)
            {
                await EnsureRoleAsync(role.Key);
                await EnsureRoleClaimsAsync(role.Key, role.Value.Select(p => p.Value).ToArray());
            }

            if(await _context.Users.FirstOrDefaultAsync(x => x.Username == "Administrator") == null)
            {
                await SeedAdministrator();
            }
        }

        private async Task SeedLanguagesAsync(FileInfo file)
        {
            _logger.LogInformation("Started seeding language data");

            string json = File.ReadAllText(file.FullName);
            var languages = JsonConvert.DeserializeObject<List<Language>>(json);
            _context.Languages.AddRange(languages);
            await _context.SaveChangesAsync();
        }

        private async Task EnsureRoleAsync(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == roleName);
            if (role == null)
            {
                try
                {
                    _logger.LogInformation($"Started creating {roleName} role");
                    role = new Role { Name = roleName };
                    _context.Roles.Add(role);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Successfully created {roleName} role");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Failed to create {roleName} role. Error : {ex.Message}");
                    throw new Exception(ex.Message);
                }
            }
        }

        private async Task EnsureRoleClaimsAsync(string roleName, IEnumerable<string> claims)
        {
            claims ??= Array.Empty<string>();

            var role = await _context.Roles.Include(x => x.Claims).FirstOrDefaultAsync(x => x.Name == roleName);

            try
            {
                _logger.LogInformation($"Removing old claims for {roleName} role");
                _context.RoleClaims.RemoveRange(role.Claims);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully removed old claims from {roleName} role");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to remove old claims from {roleName} role. Error : {ex.Message}");
                throw new Exception(ex.Message);
            }

            string[] invalidClaims = claims.Where(c => Permissions.GetPermissionByValue(c) == null).ToArray();
            if (invalidClaims.Any())
            {
                _logger.LogInformation("The following claim types are invalid: " + string.Join(", ", invalidClaims));
                throw new Exception("The following claim types are invalid: " + string.Join(", ", invalidClaims));
            }

            _logger.LogInformation($"Assigning new claims for {roleName} role");
            foreach (string claim in claims.Distinct())
            {
                var result = _context.RoleClaims.Add(new RoleClaim { ClaimType = Permissions.ClaimType, ClaimValue = claim, Role = role });
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to assign new claims from {roleName} role. Error : {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        private async Task SeedAdministrator()
        {
            var administrator = new User
            {
                Username = "admin",
                PasswordHash = PasswordHashHelper.HashPassword("admin"),
                IsActive = true,
                LastPasswordSet = DateTime.Now,
                Roles = new List<Role>()
            };

            var adminRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "admin");

            administrator.Roles.Add(adminRole);

            _context.Users.Add(administrator);
            await _context.SaveChangesAsync();
        }

        private async Task SeedCountries(FileInfo file)
        {
            _logger.LogInformation("Started seeding countries data");

            var template = new[] { new { Name_pl = "", Name_en = "", Code = ""} };

            string json = File.ReadAllText(file.FullName);
            var countriesData = JsonConvert.DeserializeAnonymousType(json, template);

            var englishLanguage = await _context.Languages.FirstOrDefaultAsync(x => x.Code == "EN");
            var polishLanguage = await _context.Languages.FirstOrDefaultAsync(x => x.Code == "PL");
            _context.Countries.AddRange(countriesData.Select(x => new Country { Code = x.Code }).OrderBy(x => x.Code));
            _context.Translations.AddRange(countriesData.Select(x => new Translation { Key = x.Code, Value = x.Name_en, Language = englishLanguage }));
            _context.Translations.AddRange(countriesData.Select(x => new Translation { Key = x.Code, Value = x.Name_pl, Language = polishLanguage }));

            await _context.SaveChangesAsync();
        }
    }
}
