using System.Diagnostics;
using System.Reflection;
using UMaTLMS.Console;
using UMaTLMS.Core.TimeTable;

Stopwatch stopwatch = Stopwatch.StartNew();

//const string fileName = @"C:\Users\admin\source\repos\UMaT Lecture Management System\LMS.Console\GaSchedule.json";
const string fileName = @"C:\Users\admin\source\repos\UMaT Lecture Management System\LMS.Console\DefaultData.json";
var configuration = new Configuration();
configuration.ParseFile(fileName);

// var alg = new GeneticAlgorithm<Schedule>(new Schedule(configuration));
var alg = new Amga2<Schedule>(new Schedule(configuration));

Console.WriteLine("GA Schedule Version {0} C# .NET Core. Making a Class Schedule Using {1}.", Assembly.GetExecutingAssembly().GetName().Version, alg.ToString());
Console.WriteLine(".......");

alg.Run();
var htmlResult = HtmlOutput.GetResult(alg.Result);

//var tempFilePath = Path.GetTempPath() + fileName.Replace(".json", ".htm");
var tempFilePath = fileName.Replace(".json", ".htm");
using (StreamWriter outputFile = new StreamWriter(tempFilePath))
{
    outputFile.WriteLine(htmlResult);
}
Console.WriteLine("");
Console.WriteLine(@"Completed in {0:s\.fff} secs with peak memory usage of {1}.", stopwatch.Elapsed, Process.GetCurrentProcess().PeakWorkingSet64.ToString("#,#"));

using (var proc = new Process())
{
    proc.StartInfo.FileName = tempFilePath;
    proc.StartInfo.UseShellExecute = true;
    proc.StartInfo.Verb = "open";
    proc.Start();
}