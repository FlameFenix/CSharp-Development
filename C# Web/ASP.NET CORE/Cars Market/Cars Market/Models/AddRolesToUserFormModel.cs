using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cars_Market.Models
{
    public class AddRolesToUserFormModel
    {
        public ICollection<string> SelectedRoles { get; set; }

        public string UserEmail { get; set; }
        public AddRolesToUserFormModel()
        {
            SelectedRoles = new HashSet<string>();
        }
    }
}
