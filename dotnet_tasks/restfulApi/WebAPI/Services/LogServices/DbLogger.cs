using System;

namespace WebAPI.Services.LogServices;

public class DbLogger : ILoggerService
{
    public void Write(string message)
    {
        Console.WriteLine("[DbLogger] - " + message);
    }
}
