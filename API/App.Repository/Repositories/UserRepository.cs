using App.DataAccess.Interfaces;
using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Repository.Repositories
{
    public interface IUserRepository
    {
        void Add(User newUser);
        IQueryable<User> GetAll();
        Task<User> GetByEmailOrUsername(string searchString);
        IQueryable<User> GetById(Guid id);
        void Remove(Guid id);
        void Update(User user);
    }

    public class UserRepository : IUserRepository
    {
        protected readonly IApplicationDbContext _dbContext;
        protected readonly DbSet<User> _dbSet;

        public UserRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Users;
        }

        public IQueryable<User> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<User> GetByEmailOrUsername(string searchString) => await _dbSet
            .AsNoTracking()
            .Include(x => x.Role).ThenInclude(x => x.Claims)
            .SingleOrDefaultAsync(x => x.Email == searchString || x.Username == searchString);

        public void Add(User newUser)
        {
            _dbSet.Add(newUser);
        }

        public IQueryable<User> GetById(Guid id)
        {
            return _dbSet.Where(x => x.Id == id);
        }

        public void Remove(Guid id)
        {
            var userToDelete = _dbSet
                .Include(x => x.Role)
                .Include(x => x.PlayerDetails)
                .Include(x => x.CoachDetails)
                .SingleOrDefault(entity => entity.Id == id);
            if (userToDelete != null)
            {
                if(userToDelete.Role.Name == "player")
                {
                    var matchesEvents = _dbContext.MatchPlayersPerformances.AsNoTracking().Include(x => x.Player).Where(x => x.Player.Id == userToDelete.PlayerDetails.Id).Count();
                    var teamJoinedEvents = _dbContext.PlayerJoinedTeamEvents.AsNoTracking().Include(x => x.PlayerHistory).Where(x => x.PlayerHistory.PlayerId == userToDelete.PlayerDetails.Id).Count();
                    var teamLeftEvents = _dbContext.PlayerLeftTeamEvents.AsNoTracking().Include(x => x.PlayerHistory).Where(x => x.PlayerHistory.PlayerId == userToDelete.PlayerDetails.Id).Count();
                    var cards = _dbContext.PlayersCards.AsNoTracking().Include(x => x.Player).Where(x => x.Player.Id == userToDelete.PlayerDetails.Id).Count();
                    var points = _dbContext.MatchPoints.AsNoTracking().Include(x => x.GoalScorer).Where(x => x.GoalScorer.Id == userToDelete.PlayerDetails.Id).Count();
                    var transmissions = _dbContext.MessageTransmissions.AsNoTracking().Include(x => x.MailboxOwner).Where(x => x.MailboxOwner.Id == userToDelete.Id).Count();
                    var surveysResults = _dbContext.UsersSurveyResults.AsNoTracking().Include(x => x.User).Where(x => x.User.Id == userToDelete.Id).Count();
                    var testResults = _dbContext.UsersTestResults.AsNoTracking().Include(x => x.User).Where(x => x.User.Id == userToDelete.Id).Count();

                    if (matchesEvents + teamJoinedEvents + teamLeftEvents + cards + points + transmissions + surveysResults + testResults > 0)
                    {
                        userToDelete.IsActive = false;
                        _dbSet.Update(userToDelete);
                    }
                    else
                    {
                        _dbSet.Remove(userToDelete);
                    }
                }
                else if (userToDelete.Role.Name == "coach")
                {
                    
                    var teamJoinedEvents = _dbContext.CoachAssignedToTeamEvents.AsNoTracking().Include(x => x.Coach).Where(x => x.Coach.Id == userToDelete.CoachDetails.Id).Count();
                    var cards = _dbContext.CoachesCards.AsNoTracking().Include(x => x.Coach).Where(x => x.Coach.Id == userToDelete.CoachDetails.Id).Count();
                    var transmissions = _dbContext.MessageTransmissions.AsNoTracking().Include(x => x.MailboxOwner).Where(x => x.MailboxOwner.Id == userToDelete.Id).Count();
                    var surveysResults = _dbContext.UsersSurveyResults.AsNoTracking().Include(x => x.User).Where(x => x.User.Id == userToDelete.Id).Count();
                    var testResults = _dbContext.UsersTestResults.AsNoTracking().Include(x => x.User).Where(x => x.User.Id == userToDelete.Id).Count();
                    var createdSurveys = _dbContext.SurveyTemplates.AsNoTracking().Include(x => x.Author).Where(x => x.Author.Id == userToDelete.Id).Count();
                    var createdTests = _dbContext.TestTemplates.AsNoTracking().Include(x => x.Author).Where(x => x.Author.Id == userToDelete.Id).Count();

                    if (teamJoinedEvents + cards + transmissions + surveysResults + testResults + createdSurveys + createdTests > 0)
                    {
                        userToDelete.IsActive = false;
                        _dbSet.Update(userToDelete);
                    }
                    else
                    {
                        _dbSet.Remove(userToDelete);
                    }
                }
                else
                {
                    var transmissions = _dbContext.MessageTransmissions.AsNoTracking().Include(x => x.MailboxOwner).Where(x => x.MailboxOwner.Id == userToDelete.Id).Count();
                    var surveysResults = _dbContext.UsersSurveyResults.AsNoTracking().Include(x => x.User).Where(x => x.User.Id == userToDelete.Id).Count();
                    var testResults = _dbContext.UsersTestResults.AsNoTracking().Include(x => x.User).Where(x => x.User.Id == userToDelete.Id).Count();
                    var createdSurveys = _dbContext.SurveyTemplates.AsNoTracking().Include(x => x.Author).Where(x => x.Author.Id == userToDelete.Id).Count();
                    var createdTests = _dbContext.TestTemplates.AsNoTracking().Include(x => x.Author).Where(x => x.Author.Id == userToDelete.Id).Count();

                    if (transmissions + surveysResults + testResults + createdSurveys + createdTests > 0)
                    {
                        userToDelete.IsActive = false;
                        _dbSet.Update(userToDelete);
                    }
                    else
                    {
                        _dbSet.Remove(userToDelete);
                    }
                }
            }

            else
            {
                throw new InvalidOperationException("There is no user with designated Id");
            }
        }

        public void Update(User user)
        {
            _dbSet.Update(user);
        }
    }
}
