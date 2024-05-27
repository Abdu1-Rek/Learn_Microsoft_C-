using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string variantsPath = "path/to/Variants";
        string exercisesPath = "path/to/Exercises";
        string testsPath = "path/to/Tests";
        string studentsPath = "path/to/Students";
        string resultsPath = "path/to/Results";

        int numberOfThreads = 4;

        var variants = DataLoader.LoadVariants(variantsPath);
        var exercises = DataLoader.LoadExercises(exercisesPath);
        var tests = DataLoader.LoadTests(testsPath);

        Tester.RunTests(studentsPath, resultsPath, variants, exercises, tests, numberOfThreads);

        Console.WriteLine("Testing completed.");
    }
}