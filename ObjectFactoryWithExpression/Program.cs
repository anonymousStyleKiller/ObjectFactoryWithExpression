// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ObjectFactoryWithExpression.Features.ExpressionCreator;
using ObjectFactoryWithExpression.Models;

var list = new List<Cat>();
var stopwatch = Stopwatch.StartNew();
for (var i = 0; i < 1000000; i++)
{
    var cat = Activator.CreateInstance<Cat>();
    list.Add(cat); 
}

Console.WriteLine(list.Count);
Console.WriteLine($"{stopwatch.Elapsed} - Activator");

Console.WriteLine(new string('-', 50));

list = new List<Cat>();
var stopwatch2 = Stopwatch.StartNew();
for (var i = 0; i < 1000000; i++)
{
    var cat = New<Cat>.Instance();
    list.Add(cat);
}

Console.WriteLine(list.Count);
Console.WriteLine($"{stopwatch2.Elapsed} - Expression Trees");
