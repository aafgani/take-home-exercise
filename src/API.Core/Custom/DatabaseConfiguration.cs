using System;

namespace API.Core;

public class DatabaseConfiguration
{
    public Database TESTRead { get; set; }
    public Database TESTWrite { get; set; }
}

public class Database
{
    public string Constring { get; set; }
    public int RetryAttempt { get; set; }
    public int SleepTime { get; set; }
    public int SqlTimeOut { get; set; }
}
