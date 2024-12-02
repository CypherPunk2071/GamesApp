using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class GameModel
    {
        public Game Record { get; set; }

        public string Title => Record.Title;

        [DisplayName("Is Online?")]
        public string IsMultiplayer => Record.IsMultiplayer ? "Yes" : "No";

        [DisplayName("Release Date")]
        public string ReleaseDate => Record.ReleaseDate is null ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy");

        public string Price => Record.Price.HasValue ? Record.Price.Value.ToString("C2") : "0.00$";

        public string Publisher => Record.Publisher?.Name;
    }
}
