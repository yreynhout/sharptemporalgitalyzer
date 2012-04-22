namespace Seabites.SharpTemporalGitalyzer {
  public struct CSharpMethodBodyStatistics {
    readonly int _lineCount;
    readonly int _length;
    readonly int _hash;

    public CSharpMethodBodyStatistics(int lineCount, int length, int hash) {
      _lineCount = lineCount;
      _length = length;
      _hash = hash;
    }

    public int Length {
      get { return _length; }
    }

    public int LineCount {
      get { return _lineCount; }
    }

    public int Hash {
      get { return _hash; }
    }
  }
}