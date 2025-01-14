using dddGym.Domain.UnitTests.TestConstants;

namespace dddGym.Domain.UnitTests.TestUtilities.Sessions;

internal static class SessionFactory
{
    public static Session CreateSession(int maxNumberOfParticipants)
    {
        return new Session(maxNumberOfParticipants, Constants.Trainer.Id, Constants.Session.Id);
    }
}