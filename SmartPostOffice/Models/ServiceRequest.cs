using System.ComponentModel.DataAnnotations;

namespace SmartPostOffice.Models
{
    public class ServiceRequest
    {
        [Key]
        public int Id { get; set; }

        public string? ReferenceNumber { get; set; }

        [Required]
        public string ServiceType { get; set; } = string.Empty;

        [Required]
        public string SenderName { get; set; } = string.Empty;

        [Required]
        public string SenderPhone { get; set; } = string.Empty; // අලුතින් එක් කළා

        [Required]
        public string ReceiverName { get; set; } = string.Empty;

        [Required]
        public string ReceiverPhone { get; set; } = string.Empty; // අලුතින් එක් කළා

        [Required]
        public string Destination { get; set; } = string.Empty;

        [Required]
        public decimal Weight { get; set; } // අලුතින් එක් කළා

        [Required]
        public decimal Price { get; set; } // අලුතින් එක් කළා

        public string Status { get; set; } = "PENDING";

        public DateTime? CreatedDate { get; set; }
    }
}