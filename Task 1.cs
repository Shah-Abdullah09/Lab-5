using System;

public enum Department
{
    Science,
    Arts,
    Commerce,
    Engineering,
    Medicine
}

public class Person
{
    // Instance variable
    private string name;

    // No-argument constructor
    public Person()
    {
        name = null; // Default value for string is null
    }

    // Multi-argument constructor
    public Person(string name)
    {
        this.name = name;
    }

    // Property to get and set the name
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}

public class Student : Person
{
    // Instance variables
    private string regNo;
    private int age;
    private Department program;

    // No-argument constructor
    public Student() : base()
    {
        regNo = null;   // Default value for string is null
        age = 0;        // Default value for int is 0
        program = default(Department); // Default enum value
    }

    // Multi-argument constructor
    public Student(string name, string regNo, int age, Department program) : base(name)
    {
        this.regNo = regNo;
        this.age = age;
        this.program = program;
    }

    // Properties to get and set the instance variables
    public string RegNo
    {
        get { return regNo; }
        set { regNo = value; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    public Department Program
    {
        get { return program; }
        set { program = value; }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Using no-argument constructor
        Student student1 = new Student();
        student1.Name = "Wajahat";
        student1.RegNo = "233559";
        student1.Age = 20;
        student1.Program = Department.Engineering;

        // Using multi-argument constructor
        Student student2 = new Student("Laiba", "233560", 22, Department.Science);

        // Output the details
        Console.WriteLine("Student 1:");
        PrintStudentDetails(student1);

        Console.WriteLine("\nStudent 2:");
        PrintStudentDetails(student2);
    }

    // Helper method to print student details
    public static void PrintStudentDetails(Student student)
    {
        Console.WriteLine($"Name: {student.Name}");
        Console.WriteLine($"Registration Number: {student.RegNo}");
        Console.WriteLine($"Age: {student.Age}");
        Console.WriteLine($"Program: {student.Program}");
    }
}
