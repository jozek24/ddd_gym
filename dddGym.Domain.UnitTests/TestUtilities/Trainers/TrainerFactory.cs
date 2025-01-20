using dddGym.Domain.UnitTests.TestConstants;

namespace dddGym.Domain.UnitTests.TestUtilities.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(
        Guid? userId = null,
        Guid? id = null)
    {
        return new Trainer(
            userId: userId ?? Constants.User.Id,
            id: id ?? Constants.Trainer.Id);
    }
}