using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogReader
{
    public class Post
    {
        public string ImageAuthorUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        public void LoadPost()
        {
            // I need first load the html file and after parse the information.
            // Because the memory issues load 3 posts per time
            //Here will load the current post in the windowsphone screen
        }
    }
}
