// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int [] numbers = {15,20,23,50,32,67, 92, 153, 15};

foreach (var item in numbers)
{
    Console.WriteLine(item);
}

System.Console.WriteLine("***********************************************");
var result = numbers
    .Where(x => x>30)
    .Where(x=>x<100);

foreach (var item in result)
{
    Console.WriteLine(item);
}

System.Console.WriteLine("***********************************************");    
string [] names = {"alice", "brad", "pel", "mira", "valora"};
var resultNames = names
    .Where(x=> x.Length>=5)
    .Select(x=> x.ToUpper()); //use select to change a value.
foreach (var item in resultNames)
{
    System.Console.WriteLine(item);
}
System.Console.WriteLine("***********************************************");  
System.Console.WriteLine("Write a query that returns top 5 numbers from the list of integers in descending order.");  
int [] values = {78, -9, 0, 23, 54,  21, 7, 86};
var ascResult = values
    .OrderByDescending(x=> x) //order an array.
    .Take(5);
foreach (var item in ascResult)
{
    System.Console.WriteLine(item);
}
System.Console.WriteLine("***********************************************");  
System.Console.WriteLine("Write a query that returns list of numbers and their squares only if square is greater than 20.");  
int [] squareVal = {7, 2, 30, 1, 0, 5, 22};
var sqResult = squareVal
    .Where(x=> x*x>20);

foreach (var item in sqResult)
{
    System.Console.WriteLine(item);
}

System.Console.WriteLine("***********************************************");  
System.Console.WriteLine("Write a query that replaces 'ea' substring with astersik (*) in given list of words.");
string [] words = {"learn", "current", "deal"};
var wordsResult = words
    .Select(x => x.Contains("ea") ? x.Replace("ea", "*") : x);

foreach (var item in wordsResult)
{
    System.Console.WriteLine(item);
}

System.Console.WriteLine("***********************************************");  
System.Console.WriteLine("Given a non-empty list of words, sort it alphabetically and return a word that contains letter 'e'.");

string [] vals = {"plane", "ferry", "car", "bike"};
var vals2 = vals
    .OrderBy(x => x)
    .Last(x => x.Contains("e"));
System.Console.WriteLine(vals2);

System.Console.WriteLine("***********************************************");  
System.Console.WriteLine("Write a query that shuffles sorted array.");

int [] sortedArray = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
var shuffled = sortedArray.OrderBy(a => Guid.NewGuid());
foreach (var item in shuffled)
{
    Console.Write("{0} ", item);
}

System.Console.WriteLine("***********************************************");  
System.Console.WriteLine("Write a query that shuffles sorted array.");

char [] specialCharacters = {'@', '!','-', '$', '*', '%'};

var encrypted = "$-!@*%$$";

var decrypted = string.Join("", encrypted.Select(c => Array.IndexOf(specialCharacters, c)));

System.Console.WriteLine(decrypted);

System.Console.WriteLine("***********************************************");  
System.Console.WriteLine("Write a query that returns most frequent character in string. Assume that there is only one such character.");
