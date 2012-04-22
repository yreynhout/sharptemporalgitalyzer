namespace Seabites.SharpTemporalGitalyzer {
  public struct CSharpMethodBodyLineCountAndLength {
    readonly int _lineCount;
    readonly int _length;

    public CSharpMethodBodyLineCountAndLength(int lineCount, int length) {
      _lineCount = lineCount;
      _length = length;
    }

    public int Length {
      get { return _length; }
    }

    public int LineCount {
      get { return _lineCount; }
    }
  }
}