using System;
using System.IO;
using ICSharpCode.NRefactory.CSharp;

namespace Seabites.SharpTemporalGitalyzer {
  public static class ExtensionOfMethodDeclaration {
    public static CSharpMethodBodyLineCountAndLength GetBodyLineCountAndLength(this MethodDeclaration declaration) {
      using (var writer = new StringWriter()) {
        var visitor = new CSharpOutputVisitor(writer, FormattingOptionsFactory.CreateAllman());
        declaration.AcceptVisitor(visitor);
        var bodyAsString = writer.ToString();
        return new CSharpMethodBodyLineCountAndLength(
          bodyAsString.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Length,
          bodyAsString.Length);
      }
    }
  }
}