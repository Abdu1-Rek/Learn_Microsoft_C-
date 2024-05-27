using System;
using System.Collections.Generic;

public class Variant
{
    public string Student { get; set; }
    public List<int> Exercises { get; set; }
}

public class Exercise
{
    public string Name { get; set; }
    public string ClassName { get; set; }
    public string MethodName { get; set; }
    public string ReturnType { get; set; }
    public List<(string ParamName, string ParamType)> Parameters { get; set; }
}

public class Test
{
    public List<object> ParameterValues { get; set; }
    public object ExpectedResult { get; set; }
}