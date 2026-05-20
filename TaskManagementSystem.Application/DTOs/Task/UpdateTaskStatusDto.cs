using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs.Task
{
    public class UpdateTaskStatusDto
    {
        [Required]
        public Status Status { get; set; }
    }
}
