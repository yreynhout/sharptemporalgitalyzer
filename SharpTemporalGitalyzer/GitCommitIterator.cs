using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Seabites.SharpTemporalGitalyzer {
  public class GitCommitIterator {
    const int InitialIndex = 1;
    readonly string _gitPath;

    public GitCommitIterator(string gitPath) {
      if (gitPath == null) throw new ArgumentNullException("gitPath");
      _gitPath = gitPath;
    }

    public IEnumerable<Commit> GetCommits(string fileRepositoryPath) {
      if (fileRepositoryPath == null) throw new ArgumentNullException("fileRepositoryPath");
      var fileRepositoryDirectory = new DirectoryInfo(fileRepositoryPath);
      if (!fileRepositoryDirectory.GetDirectories(".git").Any()) {
        throw new ArgumentException("The file repository path does not seem to contain a .git folder.", "fileRepositoryPath");
      }
      var startInfo = new ProcessStartInfo(
        string.Format(
          "{0}\\git.cmd", _gitPath),
          "log --pretty=format:\"%H|%ci|%cn\" --branches=master --reverse") {
                                                                            RedirectStandardOutput = true,
                                                                            WindowStyle = ProcessWindowStyle.Hidden,
                                                                            UseShellExecute = false,
                                                                            WorkingDirectory = fileRepositoryPath
                                                                          };
      var commits = new List<Commit>();
      using(var process = Process.Start(startInfo)) {
        string logLine;
        var index = InitialIndex;
        while((logLine = process.StandardOutput.ReadLine()) != null) {
          var data = logLine.Split('|');
          commits.Add(new Commit(index, data[0], data[1], data[2]));
          index++;
        }
        process.WaitForExit();
      }
      return commits;
    }
  }
}