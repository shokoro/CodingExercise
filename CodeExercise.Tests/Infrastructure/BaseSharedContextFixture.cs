using Autofac;

namespace CodeExercise.Tests;

[Collection("SharedContext")]
public abstract class BaseSharedContextFixture : IDisposable
{

    public SharedContextFixture SharedContext { get; }

    protected ILifetimeScope Container { get; }

    protected BaseSharedContextFixture(SharedContextFixture sharedContext)
    {
        SharedContext = sharedContext ?? throw new ArgumentNullException(nameof(sharedContext));
        Container = sharedContext.Container.BeginLifetimeScope();
    }

    public virtual void Dispose()
    {
        Container.Dispose();
    }
}