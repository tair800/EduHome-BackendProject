using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.SpeakerVMs
{
    public class SpeakerCreateVM
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Event is required.")]
        public int EventId { get; set; }
    }
}
