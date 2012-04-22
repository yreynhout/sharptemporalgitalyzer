using System;
using System.IO;
using ICSharpCode.NRefactory.CSharp;

namespace Seabites.SharpTemporalGitalyzer {
  public static class ExtensionOfMethodDeclaration {
    public static CSharpMethodBodyStatistics GetBodyLineCountAndLength(this MethodDeclaration declaration) {
      using (var writer = new StringWriter()) {
        var visitor = new CSharpOutputVisitor(writer, FormattingOptionsFactory.CreateAllman());
        declaration.AcceptVisitor(visitor);
        var bodyAsString = writer.ToString();
        return new CSharpMethodBodyStatistics(
          bodyAsString.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Length,
          bodyAsString.Length,
          bodyAsString.GetHashCode());
      }
    }
  }
}