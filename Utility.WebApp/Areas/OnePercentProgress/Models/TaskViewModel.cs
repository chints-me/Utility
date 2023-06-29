using System.ComponentModel.DataAnnotations;

namespace Utility.WebApp.Areas.OnePercentProgress.Models
{
    public class TaskViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Task Title")]
        public string Name { get; set; }

        public Guid ProjectId { get; set; }
    }
}
