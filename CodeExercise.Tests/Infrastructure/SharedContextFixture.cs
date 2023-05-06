using Autofac;

namespace CodeExercise.Tests;

public class SharedContextFixture : IDisposable
{
    public ILifetimeScope Container { get; }

    public SharedContextFixture()
    {
        Container = TestInitializer.Container.BeginLifetimeScope();
    }

    public void Dispose()
    {
        Container.Dispose();
    }
}