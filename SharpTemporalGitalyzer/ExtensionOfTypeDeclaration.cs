using ICSharpCode.NRefactory.CSharp;

namespace Seabites.SharpTemporalGitalyzer {
  public static class ExtensionOfTypeDeclaration {
    public static string GetNamespace(this TypeDeclaration declaration) {
      if (declaration.Parent is NamespaceDeclaration) {
        return ((NamespaceDeclaration)declaration.Parent).FullName;
      }
      return string.Empty;
    }
  }
}