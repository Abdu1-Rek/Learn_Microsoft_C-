using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

public static class Tester
{
    public static void RunTests(string studentsPath, string resultsPath, List<Variant> variants, Dictionary<int, Exercise> exercises, Dictionary<int, List<Test>> tests, int numberOfThreads)
    {
        var students = Directory.GetDirectories(studentsPath).Select(Path.GetFileName).ToList();

        int studentsPerThread = students.Count / numberOfThreads;
        int remainingStudents = students.Count % numberOfThreads;

        var tasks = new List<Task>();

        for (int i = 0; i < numberOfThreads; i++)
        {
            var start = i * studentsPerThread;
            var count = studentsPerThread + (remainingStudents-- > 0 ? 1 : 0);

            var studentsSubset = students.Skip(start).Take(count).ToList();

            tasks.Add(Task.Run(() => ProcessStudents(studentsSubset, studentsPath, resultsPath, variants, exercises, tests)));
        }

        Task.WaitAll(tasks.ToArray());
    }

    private static void ProcessStudents(List<string> students, string studentsPath, string resultsPath, List<Variant> variants, Dictionary<int, Exercise> exercises, Dictionary<int, List<Test>> tests)
    {
        foreach (var student in students)
        {
            var studentVariants = variants.FirstOrDefault(v => v.Student == student);
            if (studentVariants == null) continue;

            var studentResults = new List<string>();

            foreach (var exerciseNumber in studentVariants.Exercises)
            {
                if (!exercises.ContainsKey(exerciseNumber))
                {
                    studentResults.Add($"Exercise {exerciseNumber}: No such exercise");
                    continue;
                }

                var exercise = exercises[exerciseNumber];
                var dllPath = Path.Combine(studentsPath, student, $"exercise_{exerciseNumber}.dll");

                if (!File.Exists(dllPath))
                {
                    studentResults.Add($"Exercise {exerciseNumber}: Assembly not found");
                    continue;
                }

                try
                {
                    var assembly = Assembly.LoadFile(dllPath);
                    var type = assembly.GetType(exercise.ClassName);
                    if (type == null)
                    {
                        studentResults.Add($"Exercise {exerciseNumber}: Class {exercise.ClassName} not found");
                        continue;
                    }

                    var method = type.GetMethod(exercise.MethodName);
                    if (method == null)
                    {
                        studentResults.Add($"Exercise {exerciseNumber}: Method {exercise.MethodName} not found");
                        continue;
                    }

                    var instance = Activator.CreateInstance(type);

                    var exerciseTests = tests[exerciseNumber];
                    int successCount = 0;
                    int failureCount = 0;

                    foreach (var test in exerciseTests)
                    {
                        try
                        {
                            var result = method.Invoke(instance, test.ParameterValues.ToArray());
                            if (result.Equals(test.ExpectedResult))
                            {
                                successCount++;
                            }
                            else
                            {
                                failureCount++;
                                studentResults.Add($"Exercise {exerciseNumber}: Test failed. Expected {test.ExpectedResult}, got {result}");
                            }
                        }
                        catch (Exception ex)
                        {
                            failureCount++;
                            studentResults.Add($"Exercise {exerciseNumber}: Test failed with exception {ex.Message}");
                        }
                    }

                    studentResults.Add($"Exercise {exerciseNumber}: {successCount} successful, {failureCount} failed");
                }
                catch (Exception ex)
                {
                    studentResults.Add($"Exercise {exerciseNumber}: Failed with exception {ex.Message}");
                }
            }

            var studentResultPath = Path.Combine(resultsPath, student);
            Directory.CreateDirectory(studentResultPath);
            File.WriteAllLines(Path.Combine(studentResultPath, "results.txt"), studentResults);
        }
    }
}