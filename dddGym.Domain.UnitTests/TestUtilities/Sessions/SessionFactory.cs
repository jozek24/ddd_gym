using dddGym.Domain.UnitTests.TestConstants;

namespace dddGym.Domain.UnitTests.TestUtilities.Sessions;

internal static class SessionFactory
{
    public static Session CreateSession(
        int maxParticipants = Constants.Session.MaxParticipants,
        DateOnly? date = null,
        TimeRange? time = null,
        Guid? trainerId = null,
        Guid? id = null)
    {
        return new Session(
            date ?? Constants.Session.Date,
            time ?? Constants.Session.Time,
            maxParticipants,
            trainerId ?? Constants.Trainer.Id,
            id ?? Constants.Session.Id);
    }
}