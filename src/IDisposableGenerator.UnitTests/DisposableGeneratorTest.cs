using IDisposableGenerator;

namespace IDisposableGenerator.UnitTests;

public class DisposableGeneratorTest
{
    [Fact]
    public void WhenDispose_ShouldCallMethods()
    {
        var disposable = new MyDisposable();
        Assert.NotNull(disposable);
        Assert.False(disposable.IsDisposed);
        Assert.False(disposable.IsCalledToDisposeManagedInstances);
        Assert.False(disposable.IsCalledToDisposeUnmanagedInstances);

        disposable.Dispose();
        Assert.True(disposable.IsDisposed);
        Assert.True(disposable.IsCalledToDisposeManagedInstances);
        Assert.True(disposable.IsCalledToDisposeUnmanagedInstances);
    }

    [Fact]
    public void Dispose_CallsByGc()
    {
        static WeakReference<MyDisposable> DisposeByScope()
        {
            // This will go out of scope after dispose() is executed.
            var disposable = new MyDisposable();
            return new WeakReference<MyDisposable>(disposable, true);
        }

        var weak = DisposeByScope();

        GC.WaitForPendingFinalizers();
        GC.Collect(0, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();

        if (weak.TryGetTarget(out var resurrection))
        {
            Assert.True(resurrection.IsDisposed);
            Assert.False(resurrection.IsCalledToDisposeManagedInstances); // Not called through finalizer.
            Assert.True(resurrection.IsCalledToDisposeUnmanagedInstances);
        }
    }
}

[GenerateIDisposable]
public partial class MyDisposable
{
    public bool IsCalledToDisposeManagedInstances { get; private set; }
    public bool IsCalledToDisposeUnmanagedInstances { get; private set; }
    partial void DisposeManagedInstances() => this.IsCalledToDisposeManagedInstances = true;
    partial void DisposeUnmanagedInstances() => this.IsCalledToDisposeUnmanagedInstances = true;
}
