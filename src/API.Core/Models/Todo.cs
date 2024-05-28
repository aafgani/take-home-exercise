using System;

namespace API.Core.Models;

public class Todo
{
    public string Id { get; set; }
    public string ItemName { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
