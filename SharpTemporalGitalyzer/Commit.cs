namespace Seabites.SharpTemporalGitalyzer {
  public class Commit {
    readonly int _index;
    readonly string _hash;
    readonly string _committer;
    readonly string _commitDate;

    public Commit(int index, string hash, string committer, string commitDate) {
      _index = index;
      _hash = hash;
      _committer = committer;
      _commitDate = commitDate;
    }

    public string CommitDate {
      get { return _commitDate; }
    }

    public string Committer {
      get { return _committer; }
    }

    public string Hash {
      get { return _hash; }
    }

    public string AnalysisOutputFileName { get { return string.Format("{0}.csv", _index); }}

    public override string ToString() {
      return string.Format("{0}:{1}", _index, _hash);
    }
  }
}