using System.Collections.Generic;

namespace Seabites.ChangeOverTime {
  public class CSharpMethodFullMethodNameEqualityComparer : IEqualityComparer<CSharpMethod> {
    public bool Equals(CSharpMethod x, CSharpMethod y) {
      return x.FullMethodName.Equals(y.FullMethodName);
    }

    public int GetHashCode(CSharpMethod obj) {
      return obj.FullMethodName.GetHashCode();
    }
  }
}