using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;

namespace Seabites.SharpTemporalGitalyzer {
  public class CSharpFile {
    readonly string _file;
    readonly Commit _commit;

    public CSharpFile(string file, Commit commit) {
      _file = file;
      _commit = commit;
    }

    public override string ToString() {
      return _file;
    }

    public IEnumerable<CSharpMethod> AnalyzeMethods(CSharpParser parser) {
      using(var stream = File.OpenRead(_file)) {
        var compilationUnit = parser.Parse(stream, _file);
        //Really should be checking if there are any parse errors ...
        var types = compilationUnit.GetTypes(true);
        foreach(var type in types) {
          foreach(var method in type.Members.OfType<MethodDeclaration>()) {
            yield return new CSharpMethod(
              string.Format("{0}.{1}.{2}", type.GetNamespace(), type.Name, method.Name),
              method.GetBodyLineCountAndLength(),
              _file,
              _commit);
          }
        }
      }
    }

    public static IEnumerable<CSharpFile> Enumerate(string workingCopyPath, Commit commit) {
      return Directory.EnumerateFiles(workingCopyPath, "*.cs", SearchOption.AllDirectories).Select(file => new CSharpFile(file, commit));
    }
  }
}