using System;

namespace Seabites.ChangeOverTime {
  public class CSharpMethod {
    readonly string _fullmethodname;
    readonly string _length;
    readonly string _linecount;
    readonly string _hash;
    readonly string _file;
    readonly string _commitHash;
    readonly string _committer;
    readonly string _commitDate;

    CSharpMethod(string fullmethodname, string length, string linecount, string hash, string file, string commitHash, string committer, string commitDate) {
      if (fullmethodname == null) throw new ArgumentNullException("fullmethodname");
      _fullmethodname = fullmethodname;
      _length = length;
      _linecount = linecount;
      _hash = hash;
      _file = file;
      _commitHash = commitHash;
      _committer = committer;
      _commitDate = commitDate;
    }

    public string CommitHash {
      get {
        return _commitHash;
      }
    }

    public string FullMethodName {
      get {
        return _fullmethodname;
      }
    }

    public static CSharpMethod ReadAsCsv(string csvEntry) {
      var data = csvEntry.Split(',');
      return new CSharpMethod(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7]);
    }

    public bool BodyHashEquals(CSharpMethod next) {
      return _hash.Equals(next._hash);
    }
  }
}