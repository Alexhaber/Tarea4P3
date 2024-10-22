using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ViewModels.Post
{
    public class PostViewModel
    {
        public string User { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
