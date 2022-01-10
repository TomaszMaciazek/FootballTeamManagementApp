using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.Repositories
{
    public interface IMessageTransmissionRepository : IRepository<MessageTransmission> { }
    public class MessageTransmissionRepository : BaseRepository<MessageTransmission>, IMessageTransmissionRepository
    {
        public MessageTransmissionRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
