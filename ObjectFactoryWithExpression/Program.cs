// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ObjectFactoryWithExpression.Factory;
using ObjectFactoryWithExpression.Features.ExpressionCreator;
using ObjectFactoryWithExpression.Models;

var list = new List<Cat>();
var stopwatch = Stopwatch.StartNew();
for (var i = 0; i < 100; i++)
{
    var cat = Activator.CreateInstance<Cat>();
    list.Add(cat); 
}

Console.WriteLine(list.Count);
Console.WriteLine($"{stopwatch.Elapsed} - Activator");

Console.WriteLine(new string('-', 50));

list = new List<Cat>();
New<Cat>.Instance();
var stopwatch2 = Stopwatch.StartNew();
var catType = typeof(Cat);
catType.CreateInstance<Cat>();
for (var i = 0; i < 100; i++)
{
    var cat = New<Cat>.Instance();
    list.Add(cat);
}

Console.WriteLine(list.Count);
Console.WriteLine($"{stopwatch2.Elapsed} - Expression Trees");
