using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Seabites.ChangeOverTime {
  class Program {
    static void Main(string[] args) {
      var allMethods = new AllMethods();
      File.Delete("changeovertime.csv");
      using (var csvWriter = new StreamWriter(File.OpenWrite("changeovertime.csv"))) {
        csvWriter.WriteLine("CommitHash,Added,Modified,Removed");
        foreach (var file in
          Directory.EnumerateFiles(args[0], "*.csv", SearchOption.TopDirectoryOnly).
            OrderBy(path => Convert.ToInt32(Path.GetFileNameWithoutExtension(path)))) {
          var allMethodsInCommit = ReadMethodsInCommit(file).ToList();
          
          ReportAnyDuplicateMethods(allMethodsInCommit);

          var distinctMethodsInCommit = allMethodsInCommit.Distinct(new CSharpMethodFullMethodNameEqualityComparer()).ToList();
          if(distinctMethodsInCommit.Any()) {
            var changes = allMethods.WhatHasChanged(distinctMethodsInCommit);
            var changeRatioInCommit = (from mapped in
                                         (from change in changes
                                          select new { change.ChangeType, Count = 1 })
                                       group mapped by 1
                                         into reduced
                                         select new {
                                           Added = reduced.Count(i => i.ChangeType == ChangeType.Added),
                                           Removed = reduced.Count(i => i.ChangeType == ChangeType.Removed),
                                           Modified = reduced.Count(i => i.ChangeType == ChangeType.Modified)
                                         }).SingleOrDefault();
            if(changeRatioInCommit != null) {
              csvWriter.WriteLine(
                "{0},{1},{2},{3}",
                allMethodsInCommit.First(),
                changeRatioInCommit.Added,
                changeRatioInCommit.Modified,
                changeRatioInCommit.Removed);
            }
            allMethods.AcceptChanges(distinctMethodsInCommit);
          }
        }
      }
      Console.WriteLine("Yeah, I'm done.");
      Console.ReadLine();
    }

    static void ReportAnyDuplicateMethods(IEnumerable<CSharpMethod> allMethodsInCommit) {
      var list = new HashSet<string>();
      var duplicateHeaderWritten = false;
      foreach (var method in allMethodsInCommit.Where(method => !list.Add(method.FullMethodName))) {
        if (!duplicateHeaderWritten) {
          Console.WriteLine("Found a method name more than once in commit '{0}'.", method.CommitHash);
          duplicateHeaderWritten = true;
        }
        Console.WriteLine("\t{0}", method.FullMethodName);
      }
    }

    static IEnumerable<CSharpMethod> ReadMethodsInCommit(string path) {
      using (var file = File.OpenRead(path)) {
        using (var csvReader = new StreamReader(file)) {
          csvReader.ReadLine();
          string csvEntry;
          while ((csvEntry = csvReader.ReadLine()) != null) {
            yield return CSharpMethod.ReadAsCsv(csvEntry);
          }
        }
      }
    }
  }
}