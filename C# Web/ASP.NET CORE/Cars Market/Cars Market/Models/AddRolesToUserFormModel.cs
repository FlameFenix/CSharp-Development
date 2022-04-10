using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cars_Market.Models
{
    public class AddRolesToUserFormModel
    {
        public AddRolesToUserFormModel()
        {
            SelectedRoles = new HashSet<string>();
        }
        public ICollection<string> SelectedRoles { get; set; }

        public string? UserEmail { get; set; }
        
    }
}
