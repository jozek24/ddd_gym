using dddGym.Domain.UnitTests.TestConstants;

namespace dddGym.Domain.UnitTests.TestUtilities.Sessions;

internal static class SessionFactory
{
    public static Session CreateSession(
        int maxNumberOfParticipants,
        DateOnly? date = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        Guid? trainerId = null,
        Guid? sessionId = null)
    {
        return new Session(
            date ?? Constants.Session.Date,
            startTime ?? Constants.Session.StartTime,
            endTime ?? Constants.Session.EndTime,
            maxNumberOfParticipants,
            trainerId ?? Constants.Trainer.Id,
            sessionId ?? Constants.Session.Id);
    }
}