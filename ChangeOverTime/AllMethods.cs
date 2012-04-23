using System.Collections.Generic;
using System.Linq;

namespace Seabites.ChangeOverTime {
  public class AllMethods {
    readonly Dictionary<string, CSharpMethod> _indexOfCurrent;

    public AllMethods() {
      _indexOfCurrent = new Dictionary<string, CSharpMethod>();
    }

    public IEnumerable<CSharpMethodChange> WhatHasChanged(IEnumerable<CSharpMethod> methods) {
      var indexOfNext = methods.ToDictionary(method => method.FullMethodName);

      foreach(var method in _indexOfCurrent.Values.Union(indexOfNext.Values)) {
        CSharpMethod next;
        CSharpMethod current;
        if(indexOfNext.TryGetValue(method.FullMethodName, out next) && _indexOfCurrent.TryGetValue(method.FullMethodName, out current)) {
          //Modified?
          if(!current.BodyHashEquals(next)) {
            //Modified
            yield return new CSharpMethodChange(ChangeType.Modified, next);
          }
        } else if(indexOfNext.ContainsKey(method.FullMethodName)) {
          //Added
          yield return new CSharpMethodChange(ChangeType.Added, method);
        } else if(_indexOfCurrent.ContainsKey(method.FullMethodName)) {
          //Removed
          yield return new CSharpMethodChange(ChangeType.Removed, method);
        }
      }
    }

    public void AcceptChanges(IEnumerable<CSharpMethod> methods) {
      var indexOfNext = methods.ToDictionary(method => method.FullMethodName);
      foreach(var method in _indexOfCurrent.Values.ToArray().Union(indexOfNext.Values)) {
        CSharpMethod next;
        CSharpMethod current;
        if(indexOfNext.TryGetValue(method.FullMethodName, out next) && _indexOfCurrent.TryGetValue(method.FullMethodName, out current)) {
          //Modified?
          if(!current.BodyHashEquals(next)) {
            //Modified
            _indexOfCurrent[method.FullMethodName] = next;
          }
        } else if(indexOfNext.ContainsKey(method.FullMethodName)) {
          //Added
          _indexOfCurrent.Add(method.FullMethodName, method);
        } else if(_indexOfCurrent.ContainsKey(method.FullMethodName)) {
          //Removed
          _indexOfCurrent.Remove(method.FullMethodName);
        }
      }
    }
  }
}