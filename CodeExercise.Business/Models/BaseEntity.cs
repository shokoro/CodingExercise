using CodeExercise.Utilities;

namespace CodeExercise.Models;

public abstract class BaseEntity
{
    public long Id { get; set; }

    protected BaseEntity()
    {
        Id = SnowflakeGenerator.Current.GetNextId();
    }
}