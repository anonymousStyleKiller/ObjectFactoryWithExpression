// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ObjectFactoryWithExpression.Models;

var list = new List<Cat>();
var stopwatch = Stopwatch.StartNew();
for (var i = 0; i < 100000; i++)
{
    var cat = Activator.CreateInstance<Cat>();
    list.Add(cat);
}

Console.WriteLine($"{stopwatch.Elapsed} - Activator");
