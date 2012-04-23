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
        ChangeStatisticsByDate.WriteCsvHeader(csvWriter);
        var allChangeStatisticsByDate = new List<ChangeStatisticsByDate>();
        foreach (var file in
          Directory.EnumerateFiles(args[0], "*.csv", SearchOption.TopDirectoryOnly).
            OrderBy(path => Convert.ToInt32(Path.GetFileNameWithoutExtension(path)))) {
          var allMethodsInCommit = ReadMethodsInCommit(file).ToList();
          
          ReportAnyDuplicateMethods(allMethodsInCommit);

          var distinctMethodsInCommit = allMethodsInCommit.Distinct(new CSharpMethodFullMethodNameEqualityComparer()).ToList();
          if(distinctMethodsInCommit.Any()) {
            var commitDate = distinctMethodsInCommit.First().CommitDate.Date;
            var changes = allMethods.WhatHasChanged(distinctMethodsInCommit);
            var changeStatisticsByDate = (from mapped in
                                      (from change in changes
                                       select new {change.ChangeType, Count = 1})
                                    group mapped by 1
                                    into reduced
                                    select new ChangeStatisticsByDate
                                      (commitDate,
                                       reduced.Count(i => i.ChangeType == ChangeType.Added),
                                       reduced.Count(i => i.ChangeType == ChangeType.Removed),
                                       reduced.Count(i => i.ChangeType == ChangeType.Modified)
                                      )).SingleOrDefault();
            if(changeStatisticsByDate != null) {
              allChangeStatisticsByDate.Add(changeStatisticsByDate);
            }
            allMethods.AcceptChanges(distinctMethodsInCommit);
          }
        }

        var reducedChangeStatisticsByDate =
          from changeStatisticsByDate in allChangeStatisticsByDate
          group changeStatisticsByDate by changeStatisticsByDate.Date
          into reduced
          select
            new ChangeStatisticsByDate(reduced.Key,
                                       reduced.Sum(i => i.Added),
                                       reduced.Sum(i => i.Modified),
                                       reduced.Sum(i => i.Removed));
        foreach (var changeStatisticsByDate in reducedChangeStatisticsByDate) {
          changeStatisticsByDate.WriteAsCsv(csvWriter);
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