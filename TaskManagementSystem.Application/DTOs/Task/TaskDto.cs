using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Enums;
namespace TaskManagementSystem.Application.DTOs.Task
{
    public class TaskDto
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public TaskPriority Priority { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
