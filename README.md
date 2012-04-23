# CSharp Temporal Method Analyzer #
###### for git repositories ######

Comes with a "build it yourself" policy. It uses that NuGet thing to restore packages. Specify the path to git in the app.config, thanks.

Usage sharp temporal gitalyzer:
> stgalyzer &lt;path to your local git file repo&gt;

Spits out a .csv file under .\analysis for each commit containing all csharp methods at that point in the working copy in the following format:
> FullMethodName\tMethodBodyLength\tMethodBodyLineCount\tMethodBodyHash\tFilePath\tCommitHash\tCommitter\tCommitDate

Right now, it's hard-coded to do this for the 'master' branch.

Usage of change over time:
> spitcot &lt;path to the .\analysis folder&gt;

Spits out a .csv file under . containing the number of methods added, removed, modified on a date by date basis. The format used is:
> CommitUtcDate\tAdded\tModified\tRemoved

Use a spreadsheet to graph the add/modify/remove ratio over time.

### What can I do with these .csv files ? ###
Given that I don't know what analysis you are interested in, it requires you to write another piece of code, one that reads and analyzes the .csv files. Things you could do:

- read and compare each commit's .csv with the previous one to detect the methods that were added, removed, modified (spitcot). 
- rank methods/types/projects/areas by their size over time.
- produce another intermediate format that suits your needs.
- you might pick up some ideas [here](http://michaelfeathers.typepad.com/michael_feathers_blog/2011/09/temporal-correlation-of-class-changes.html).

### Disclaimer ###
**Unstable! Only do this on a throw-away copy of your repo. If shit hits the fan, you're on your own. I'm not liable and have a good lawyer ;-) This is a sample, not a project/product. You're supposed to change the code to make it do what you want. Duh!!**
