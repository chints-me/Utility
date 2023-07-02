using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Utility.WebApp.Areas.OnePercentProgress.Models
{
    public class TasksViewModel
    {
        public Guid ProjectId { get; set; }
        public Guid GrandParentTaskId { get; set; }
        public Guid ParentTaskId { get; set; }
        public Guid TaskId { get; set; }
        public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }

    public class TaskViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Task Title")]
        public string Name { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public Guid ParentTaskId { get; set; }

        [Display(Name = "Task Status")]
        public string Status { get; set; }
        public List<SelectListItem> StatusList { get; set; } = new List<SelectListItem>();
        public int TotalTasks { get; set; }
        public int TotalTasksDone { get; set; }
        public int CompletionPercentage { get; set; }
    }
}
