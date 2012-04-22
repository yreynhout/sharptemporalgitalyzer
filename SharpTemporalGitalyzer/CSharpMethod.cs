using System.IO;

namespace Seabites.SharpTemporalGitalyzer {
  public class CSharpMethod {
    readonly string _fullmethodname;
    readonly CSharpMethodBodyLineCountAndLength _bodyLineCountAndLength;
    readonly string _file;
    readonly Commit _commit;

    public CSharpMethod(string fullmethodname, CSharpMethodBodyLineCountAndLength bodyLineCountAndLength, string file, Commit commit) {
      _fullmethodname = fullmethodname;
      _bodyLineCountAndLength = bodyLineCountAndLength;
      _file = file;
      _commit = commit;
    }

    public void WriteAsCsv(TextWriter writer) {
      writer.WriteLine(
        "{0},{1},{2},{3},{4},{5},{6}",
        _fullmethodname, _bodyLineCountAndLength.Length, _bodyLineCountAndLength.LineCount, _file, _commit.Hash, _commit.Committer, _commit.CommitDate);
    }

    public static void WriteCsvHeader(TextWriter writer) {
      writer.WriteLine(
        "FullMethodName,MethodLength,MethodLineCount,FilePath,CommitHash,Committer,CommitDate");
    }
  }
}