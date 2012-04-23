using System;
using System.IO;

namespace Seabites.ChangeOverTime {
  public class ChangeStatisticsByDate {
    readonly DateTimeOffset _date;
    readonly int _added;
    readonly int _modified;
    readonly int _removed;

    public ChangeStatisticsByDate(DateTimeOffset date, int added, int modified, int removed) {
      _date = date;
      _added = added;
      _modified = modified;
      _removed = removed;
    }

    public DateTimeOffset Date {
      get {
        return _date;
      }
    }

    public int Added {
      get {
        return _added;
      }
    }

    public int Modified {
      get {
        return _modified;
      }
    }

    public int Removed {
      get {
        return _removed;
      }
    }

    public void WriteAsCsv(TextWriter csvWriter) {
      csvWriter.WriteLine(
        "{0},{1},{2},{3}",
        _date.Date.ToString("yyyy-MM-dd"),
        _added,
        _modified,
        _removed);
    }

    public static void WriteCsvHeader(TextWriter csvWriter) {
      csvWriter.WriteLine("CommitUtcDate,Added,Modified,Removed");
    }
  }
}