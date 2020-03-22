using System;
using System.IO;
using SQLite;

namespace StayAtHoome.Data {
  public static class Constants {
    public const string DatabaseFilename = "StayAtHome.db3";

    public const SQLiteOpenFlags Flags = SQLiteOpenFlags.Create |
                                         SQLiteOpenFlags.ReadWrite |
                                         SQLiteOpenFlags.FullMutex;

    public static string DatabasePath {
      get {
        var basePath = Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(basePath, DatabaseFilename);
      }
    }
  }
}