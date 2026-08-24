using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGivers.Models
{
    public class ApplicationUser : IdentityUser
    { 
        
        public string FullName { get; set; } = string.Empty;

        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;

        public ICollection<Donation> Donations { get; set; } = [];

        public ICollection<Volunteer> VolunteerProfiles { get; set; } = [];

        public ICollection<ProjectUpdate> ProjectUpdates { get; set; } = [];
    }
}