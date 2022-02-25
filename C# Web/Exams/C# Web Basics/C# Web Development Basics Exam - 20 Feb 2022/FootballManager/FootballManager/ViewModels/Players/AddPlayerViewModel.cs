using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.ViewModels.Players
{
    public class AddPlayerViewModel
    {
        public string FullName { get; init; }

        public string ImageUrl { get; init; }

        public string Position { get; init; }

        public byte Speed { get; init; }

        public byte Endurance { get; init; }

        public string Description { get; init; }
    }
}
