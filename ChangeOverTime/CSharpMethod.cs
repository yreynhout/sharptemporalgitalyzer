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

    public static CSharpMethod Parse(string csvEntry) {
      var data = csvEntry.Split(',');
      return new CSharpMethod(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7]);
    }

    public bool Equals(CSharpMethod other) {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Equals(other._fullmethodname, _fullmethodname);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (CSharpMethod)) return false;
      return Equals((CSharpMethod) obj);
    }

    public override int GetHashCode() {
      return _fullmethodname.GetHashCode();
    }

    public bool BodyHashEquals(CSharpMethod next) {
      return _hash.Equals(next._hash);
    }
  }
}