using System.IO;

public static class DataLoader
{
    public static List<Variant> LoadVariants(string variantsPath)
    {
        var variants = new List<Variant>();
        var files = Directory.GetFiles(variantsPath);

        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);
            var student = Path.GetFileNameWithoutExtension(file);
            var exercises = new List<int>();

            foreach (var line in lines)
            {
                if (int.TryParse(line.Replace("exercise_", ""), out int exerciseNumber))
                {
                    exercises.Add(exerciseNumber);
                }
            }

            variants.Add(new Variant { Student = student, Exercises = exercises });
        }

        return variants;
    }

    public static Dictionary<int, Exercise> LoadExercises(string exercisesPath)
    {
        var exercises = new Dictionary<int, Exercise>();
        var files = Directory.GetFiles(exercisesPath, "*.txt");

        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);
            var exerciseNumber = int.Parse(Path.GetFileNameWithoutExtension(file).Replace("exercise_", ""));

            var exercise = new Exercise
            {
                Name = lines[0],
                ClassName = lines[1].Split('.')[0],
                MethodName = lines[1].Split('.')[1],
                ReturnType = lines[2],
                Parameters = new List<(string, string)>()
            };

            for (int i = 3; i < lines.Length; i++)
            {
                var parts = lines[i].Split(':');
                exercise.Parameters.Add((parts[0], parts[1]));
            }

            exercises.Add(exerciseNumber, exercise);
        }

        return exercises;
    }

    public static Dictionary<int, List<Test>> LoadTests(string testsPath)
    {
        var tests = new Dictionary<int, List<Test>>();
        var directories = Directory.GetDirectories(testsPath);

        foreach (var dir in directories)
        {
            var exerciseNumber = int.Parse(Path.GetFileName(dir).Replace("exercise_", ""));
            var testFiles = Directory.GetFiles(dir);

            var exerciseTests = new List<Test>();

            foreach (var file in testFiles)
            {
                var lines = File.ReadAllLines(file);
                var parameterValues = new List<object>();

                for (int i = 0; i < lines.Length - 1; i++)
                {
                    var parts = lines[i].Split(':');
                    parameterValues.Add(Convert.ChangeType(parts[1], Type.GetType($"System.{parts[1].GetType().Name}")));
                }

                var expectedResult = Convert.ChangeType(lines[^1], Type.GetType($"System.{lines[^1].GetType().Name}"));

                exerciseTests.Add(new Test
                {
                    ParameterValues = parameterValues,
                    ExpectedResult = expectedResult
                });
            }

            tests.Add(exerciseNumber, exerciseTests);
        }

        return tests;
    }
}