# CSharp Temporal Method Analyzer #
###### for git repositories ######

Usage:
> stgalyzer &lt;path to your local git file repo&gt;

Spits out a .csv file under .\analysis for each commit containing all csharp methods at that point in the working copy in the following format:
> FullMethodName,MethodLength,MethodLineCount,FilePath,CommitHash,Committer,CommitDate

**Unstable! Only do this on a throw-away copy of your repo. This is a sample, not a project/product. You're supposed to change the code, to make it do what you want.**

### What can I do with these .csv files ? ###
Given that I don't know what analysis you are interested in, it requires you to write another piece of code, one that reads and analyzes the .csv files. Things you could do:

- read and compare each commit's .csv with the previous one to detect the methods that were added, removed, modified. 
- rank methods/types by their size over time.
- produce another intermediate format that suits your needs.
- you might pick up some ideas [here](http://michaelfeathers.typepad.com/michael_feathers_blog/2011/09/temporal-correlation-of-class-changes.html)