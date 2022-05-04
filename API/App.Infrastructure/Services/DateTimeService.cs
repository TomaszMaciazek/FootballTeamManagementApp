using App.DataAccess.Interfaces;
using System;

namespace App.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
