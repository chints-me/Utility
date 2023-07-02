using System.ComponentModel.DataAnnotations;

namespace Utility.WebApp.Domains.OnePercentProgress
{
    public class OPP_Task
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public Guid OPP_ParentTaskId { get; set; }

        [Required]
        public Guid OPP_ProjectId { get; set; }
        public OPP_Project OPP_Project { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
