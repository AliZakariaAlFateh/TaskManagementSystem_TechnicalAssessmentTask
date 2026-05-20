using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.Enums;
using Status = TaskManagementSystem.Domain.Enums.Status;
namespace TaskManagementSystem.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; }
    //public TaskPriority Priority { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime DueDate { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}