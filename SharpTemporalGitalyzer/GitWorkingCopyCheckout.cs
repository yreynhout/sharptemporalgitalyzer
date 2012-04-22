using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Seabites.SharpTemporalGitalyzer {
  public class GitWorkingCopyCheckout {
    readonly string _gitPath;

    public GitWorkingCopyCheckout(string gitPath) {
      if (gitPath == null) throw new ArgumentNullException("gitPath");
      _gitPath = gitPath;
    }

    public void CheckoutCommit(string workingCopyPath, Commit commit) {
      if (workingCopyPath == null) throw new ArgumentNullException("workingCopyPath");
      var workingCopyDirectory = new DirectoryInfo(workingCopyPath);
      if (!workingCopyDirectory.GetDirectories(".git").Any()) {
        throw new ArgumentException("The working copy path does not seem to contain a .git folder.", "workingCopyPath");
      }
      var startInfo = new ProcessStartInfo(
        string.Format(
          "{0}\\git.cmd", _gitPath),
        string.Format("checkout {0}", commit.Hash)) {
                                                      RedirectStandardOutput = true,
                                                      WindowStyle = ProcessWindowStyle.Hidden,
                                                      UseShellExecute = false,
                                                      WorkingDirectory = workingCopyPath
                                                    };
      using (var process = Process.Start(startInfo)) {
        process.WaitForExit();
      }
    }
  }
}