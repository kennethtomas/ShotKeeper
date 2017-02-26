using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShotKeeper.Models
{
    public class MainPageListItem
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime LastModified { get; set; }
    }
}
