using System.ComponentModel.DataAnnotations;

namespace Utility.WebApp.Domains.OnePercentProgress
{
    public class OPP_Project
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<OPP_Task> OPP_Tasks { get; set; }
    }
}
