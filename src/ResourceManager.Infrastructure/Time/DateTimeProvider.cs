using ResourceManager.SharedKernel;

namespace ResourceManager.Infrastructure.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
