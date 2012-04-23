using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Seabites.ChangeOverTime {
  class Program {
    static void Main(string[] args) {
      var allMethods = new AllMethods();
      using (var csvWriter = new StreamWriter(File.OpenWrite("changeovertime.csv"))) {
        foreach (var file in
          Directory.EnumerateFiles(args[0], "*.csv", SearchOption.TopDirectoryOnly).
            OrderBy(path => Convert.ToInt32(Path.GetFileNameWithoutExtension(path)))) {
          var allMethodsInCommit = ReadMethodsInCommit(file).ToList();
          if (allMethodsInCommit.Any()) {
            var changes = allMethods.WhatHasChanged(allMethodsInCommit);
            var changeRatioInCommit = (from mapped in
                                         (from change in changes
                                          select new { change.Method.CommitHash, change.ChangeType, Count = 1 })
                                       group mapped by mapped.CommitHash
                                         into reduced
                                         select new {
                                           CommitHash = reduced.Key,
                                           Added = reduced.Count(i => i.ChangeType == ChangeType.Added),
                                           Removed = reduced.Count(i => i.ChangeType == ChangeType.Removed),
                                           Modified = reduced.Count(i => i.ChangeType == ChangeType.Modified)
                                         }).Single();
            csvWriter.WriteLine(
              "{0},{1},{2},{3}",
              changeRatioInCommit.CommitHash,
              changeRatioInCommit.Added,
              changeRatioInCommit.Modified,
              changeRatioInCommit.Removed);
            allMethods.AcceptChanges(allMethodsInCommit);
          }
        }
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