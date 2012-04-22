//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace Seabites.ChangeOverTime {
//  class Program {
//    static void Main(string[] args) {
//      var allMethods = new AllMethods();
//      foreach(var file in
//        Directory.EnumerateFiles(args[0], "*.csv", SearchOption.TopDirectoryOnly).
//        OrderBy(path => Convert.ToInt32(Path.GetFileNameWithoutExtension(path)))) {
//        var allMethodsInCommit = ReadMethodsInCommit(file).ToList();
//        var changes = allMethods.WhatHasChanged(allMethodsInCommit);
//        var changeRatioInCommit = from mapped in (from change in changes
//                                                  select new {change.ChangeType, Count = 1})
//                                  group mapped by mapped.ChangeType
//                                  into reduced
//                                  select new {ChangeType = reduced.Key, Count = reduced.Sum(i => i.Count)};
        

//      }
//    }

//    static IEnumerable<CSharpMethod> ReadMethodsInCommit(string path) {
//      using(var file = File.OpenRead(path)) {
//        using(var csvReader = new StreamReader(file)) {
//          string csvEntry;
//          while((csvEntry = csvReader.ReadLine()) == null) {
//            yield return CSharpMethod.Parse(csvEntry);
//          }
//        }
//      }
//    }
//  }

//  public class AllMethods {
//    List<CSharpMethod> _methods;

//    public AllMethods() {
//      _methods = new List<CSharpMethod>();
//    }

//    public IEnumerable<CSharpMethodChange> WhatHasChanged(IEnumerable<CSharpMethod> methods) {
//      foreach(var removed in _methods.Except(methods)) {
//        yield return new CSharpMethodChange(ChangeType.Removed, removed);
//      }
//      foreach(var added in methods.Except(_methods)) {
//        yield return new CSharpMethodChange(ChangeType.Removed, added);
//      }
//      foreach(var modified in methods.Intersect(_methods)) {
//        if(modified.)
//        yield return new CSharpMethodChange(ChangeType.Removed, added);
//      }
//    }
//  }



//  public class CSharpMethodChange {
//    readonly ChangeType _typeOfChange;
//    readonly CSharpMethod _method;

//    public CSharpMethodChange(ChangeType typeOfChange, CSharpMethod method) {
//      _typeOfChange = typeOfChange;
//      _method = method;
//    }

//    public ChangeType ChangeType {
//      get {
//        return _typeOfChange;
//      }
//    }
//  }

//  public enum ChangeType {
//    Added,
//    Removed,
//    Modified
//  }

//  public class CSharpMethod {
//    readonly string _fullmethodname;
//    readonly string _length;
//    readonly string _linecount;
//    readonly string _file;
//    readonly string _commitHash;
//    readonly string _committer;
//    readonly string _commitDate;

//    CSharpMethod(string fullmethodname, string length, string linecount, string file, string commitHash, string committer, string commitDate) {
//      if (fullmethodname == null) throw new ArgumentNullException("fullmethodname");
//      _fullmethodname = fullmethodname;
//      _length = length;
//      _linecount = linecount;
//      _file = file;
//      _commitHash = commitHash;
//      _committer = committer;
//      _commitDate = commitDate;
//    }

//    public static CSharpMethod Parse(string csvEntry) {
//      var data = csvEntry.Split(',');
//      return new CSharpMethod(data[0], data[1], data[2], data[3], data[4], data[5], data[6]);
//    }

//    public bool Equals(CSharpMethod other) {
//      if (ReferenceEquals(null, other)) return false;
//      if (ReferenceEquals(this, other)) return true;
//      return Equals(other._fullmethodname, _fullmethodname);
//    }

//    public override bool Equals(object obj) {
//      if (ReferenceEquals(null, obj)) return false;
//      if (ReferenceEquals(this, obj)) return true;
//      if (obj.GetType() != typeof (CSharpMethod)) return false;
//      return Equals((CSharpMethod) obj);
//    }

//    public override int GetHashCode() {
//      return _fullmethodname.GetHashCode();
//    }
//  }
