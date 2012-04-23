using System;
using System.Globalization;
using System.Xml;

namespace Seabites.ChangeOverTime {
  public class CSharpMethod {
    readonly string _fullmethodname;
    readonly int _length;
    readonly int _linecount;
    readonly int _hash;
    readonly string _file;
    readonly string _commitHash;
    readonly string _committer;
    readonly DateTimeOffset _commitDate;

    CSharpMethod(string fullmethodname, int length, int linecount, int hash, string file, string commitHash, string committer, DateTimeOffset commitDate) {
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

    public DateTimeOffset CommitDate {
      get { return _commitDate; }
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
      var data = csvEntry.Split('\t');
      return new CSharpMethod(
        data[0], 
        Convert.ToInt32(data[1]), 
        Convert.ToInt32(data[2]),
        Convert.ToInt32(data[3]), 
        data[4], 
        data[5], 
        data[6],
        DateTimeOffset.ParseExact(data[7], "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.InvariantCulture));
    }

    public bool BodyHashEquals(CSharpMethod next) {
      return _hash.Equals(next._hash);
    }
  }
}