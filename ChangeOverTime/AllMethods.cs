using System.Collections.Generic;
using System.Linq;

namespace Seabites.ChangeOverTime {
  public class AllMethods {
    readonly List<CSharpMethod> _methods;

    public AllMethods() {
      _methods = new List<CSharpMethod>();
    }

    public IEnumerable<CSharpMethodChange> WhatHasChanged(IEnumerable<CSharpMethod> methods) {
      foreach(var removed in _methods.Except(methods)) {
        yield return new CSharpMethodChange(ChangeType.Removed, removed);
      }
      foreach(var added in methods.Except(_methods)) {
        yield return new CSharpMethodChange(ChangeType.Removed, added);
      }
      foreach(var unmodified in methods.Intersect(_methods)) {
        var current = _methods.First(method => method.Equals(unmodified));
        var next = methods.First(method => method.Equals(unmodified));
        if(!current.BodyHashEquals(next)) {
          yield return new CSharpMethodChange(ChangeType.Modified, next);
        }
      }
    }

    public void AcceptChanges(IEnumerable<CSharpMethod> methods) {
      foreach (var method in methods.Where(method => _methods.Contains(method))) {
        _methods.Remove(method);
      }
      _methods.AddRange(methods);
    }
  }
}