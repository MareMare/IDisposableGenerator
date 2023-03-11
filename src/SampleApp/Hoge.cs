using IDisposableGenerator;

namespace SampleApp;

[GenerateIDisposable]
internal partial class Hoge
{
    private readonly MemoryStream _stream = new ();
    
    partial void DisposeManagedInstances()
    {
        this._stream.Dispose();
    }
}
