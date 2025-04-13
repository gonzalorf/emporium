using Emporium.Application.Services;

namespace Emporium.Infrastructure.Services;
public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
