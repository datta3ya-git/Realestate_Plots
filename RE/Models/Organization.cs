using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class Organization
    {
        public long OrganizationId { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AlternatePhone { get; set; }
        public string Website { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string RegistrationNumber { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string TANNumber { get; set; }
        public string ISO_Certification { get; set; }
        public int? YearOfEstablishment { get; set; }
        public string LogoUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }

    public class OrganizationStaticResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<Organization> Organization { get; set; }
    }
}