using System.ComponentModel.DataAnnotations;

namespace UserControl.Dtos.Requests
{
    public class ActivateAccountRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string ActivationCode { get; set; }
    }
}
