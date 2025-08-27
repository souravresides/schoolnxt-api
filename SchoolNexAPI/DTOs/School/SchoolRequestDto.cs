using SchoolNexAPI.Models;

namespace SchoolNexAPI.DTOs.School
{
    public class SchoolRequestDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Board { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string LogoUrl { get; set; }
        public string Timezone { get; set; }
        public string Locale { get; set; }
        public string Currency { get; set; }

        public string CreatedBy { get; set; }
    }
}
