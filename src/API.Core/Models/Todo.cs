using System;

namespace API.Core;

public class Todo
{
    public string Id { get; set; }
    public string? ItemName { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
}
