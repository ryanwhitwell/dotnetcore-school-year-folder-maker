using System;
using System.Collections.Generic;
using System.IO;

namespace dotnetcore_school_year_folder_maker
{
  enum Iteration
  {
    Weekly
  }

  class Program
  {
    static void Main(string[] args)
    {
      string startDateString = args[0];
      string endDateString = args[1];
      string folderIterationString = args[2];

      // Get Start Date from args
      DateTime startDate;
      if (!DateTime.TryParse(startDateString, out startDate))
      {
        throw new Exception("Cannot determine start date. Please provide start date using format MM/DD/YYYY.");
      }

      // Get end date from args
      DateTime endDate;
      if (!DateTime.TryParse(endDateString, out endDate))
      {
        throw new Exception("Cannot determine end date. Please provide start date using format MM/DD/YYYY.");
      }

      // Get iteration from args
      Iteration iteration;
      if (!Enum.TryParse(folderIterationString, true, out iteration))
      {
        throw new Exception("Cannot determine iteration. Please provide a valid iteration.");
      }

      // Create folder iterations
      DateTime iterationDate = startDate;
      int iterationCounter = 1;
      List<string> iterationFolderNames = new List<string>();

      while (iterationDate <= endDate)
      {
        DateTime newIterationDate;
        switch (iteration)
        {
          case Iteration.Weekly:
            {
              newIterationDate = iterationDate.AddDays(7);
              iterationFolderNames.Add($"{iterationDate.ToString("yyyyMMdd")}-{newIterationDate.Subtract(new TimeSpan(1, 0, 0, 0)).ToString("yyyyMMdd")} Week {iterationCounter}");
              iterationCounter++;
              break;
            }
          default:
            throw new Exception("Unsupported iteration. Please provide a valid iteration.");
        }

        iterationDate = newIterationDate;
      }

      // Create directories in current working directory based on iteration folder names
      foreach (string folderName in iterationFolderNames)
      {
        Directory.CreateDirectory(folderName);
      }
    }
  }
}
