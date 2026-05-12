namespace ConsoleApp1;

[Disposable]
public partial class LogWriterWithFieldAndOrder : IDisposable, IAsyncDisposable {

    [Dispose(SetToNull = true)]
    [AsyncDispose]
    [DisposeOrder(3)]
    private StreamWriter _streamWriter;

    [AsyncDispose]
    [DisposeOrder(1)]
    private StreamWriter? _streamWriter2;

    [Dispose]
    [DisposeOrder(2)]
    private StreamWriter? _streamWriter3;

    public LogWriterWithFieldAndOrder(string path) => _streamWriter = new StreamWriter(path);

    public void WriteLine(string text) => _streamWriter.WriteLine($"{DateTime.Now}\t{text}");

}
