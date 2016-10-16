using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BussinessObjects
{
    public class GetQuoteInfo
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z ]*$", ErrorMessage = "Please enter name in alphabates only.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z ]*$", ErrorMessage = "Please enter name in alphabates only.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Email is required (we promise not to spam you!).")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|in|info|mobi|name|aero|asia|jobs|museum)\b",
            ErrorMessage = "Please enter a valid email in lower case only")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter mobile number in digits only.")]
        [DataType(DataType.PhoneNumber)]
        public String MobileNumber { get; set; }
        public int ServiceId { get; set; }
        public String ServiceName { get; set; }

        [Required(ErrorMessage = "Please select preferred date.")]
        [DataType(DataType.Date)]
        public DateTime PreferredDate { get; set; }

        [Required(ErrorMessage = "Please select preferred time, it will help us setting appointment better!!!")]
        [DataType(DataType.Time)]
        public TimeSpan PreferredTime { get; set; }

        public String AdditionalInfo { get; set; }
        public bool IsEmailSent { get; set; }
        public DateTime EmailSentDt { get; set; }
        public DateTime LastUpdateDt { get; set; }
        public DateTime CreateDt { get; set; }
        public bool NeedToCreateUser { get; set; }

        public int CityId { get; set; }
    }
}
