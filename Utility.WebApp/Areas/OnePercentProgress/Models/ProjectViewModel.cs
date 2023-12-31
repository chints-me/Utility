﻿using System.ComponentModel.DataAnnotations;

namespace Utility.WebApp.Areas.OnePercentProgress.Models
{
    public class ProjectViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Project Title")]
        public string Name { get; set; }
        public int TotalTasks { get; set; }
        public int TotalTasksDone { get; set; }
        public int CompletionPercentage { get; set; }
    }
}
