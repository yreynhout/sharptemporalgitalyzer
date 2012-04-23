using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public static string GetFullName(this MethodDeclaration declaration) {
      var name = GetPartialMethodSignature(declaration);
      var parent = declaration.Parent;
      if(parent != null && parent is TypeDeclaration) {
        name = string.Format("{0}.{1}", GetNameWithTypeParameters((TypeDeclaration)parent), name);
        parent = parent.Parent;
        while (parent != null && parent is TypeDeclaration) {
          name = string.Format("{0}+{1}", GetNameWithTypeParameters((TypeDeclaration)parent), name);
          parent = parent.Parent;
        }
      }
      if(parent != null && parent is NamespaceDeclaration) {
        return string.Format("{0}.{1}", ((NamespaceDeclaration)parent).FullName, name);
      }
      return name;
    }

    static string GetNameWithTypeParameters(TypeDeclaration declaration) {
      return GetNameWithTypeParameters(declaration.Name, declaration.TypeParameters);
    }

    static string GetNameWithTypeParameters(MethodDeclaration declaration) {
      return GetNameWithTypeParameters(declaration.Name, declaration.TypeParameters);
    }

    static string GetPartialMethodSignature(MethodDeclaration declaration) {
      if(declaration.Parameters.Count > 0) {
        using (var writer = new StringWriter()) {
          var visitor = new CSharpOutputVisitor(writer, FormattingOptionsFactory.CreateAllman());
          var parameterIndex = 0;
          foreach (var parameter in declaration.Parameters) {
            if (parameterIndex > 0)
              writer.Write(",");
            parameter.AcceptVisitor(visitor);
            parameterIndex++;
          }
          return string.Format("{0}({1})", GetNameWithTypeParameters(declaration), writer);
        }
      }
      return string.Format("{0}()", GetNameWithTypeParameters(declaration));
    }

    static string GetNameWithTypeParameters(string name, IEnumerable<TypeParameterDeclaration> typeParameters) {
      var parameters = String.Join(",", typeParameters.Select(typeParameter => typeParameter.Name));
      if(!string.IsNullOrEmpty(parameters)) {
        return name + "<" + parameters + ">";
      }
      return name;
    }
  }
}