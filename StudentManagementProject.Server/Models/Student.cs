using System;
using System.Collections.Generic;

namespace RankingApp.Server.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public int Grade { get; set; }
}
