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
            var reader = new Reader();
            reader.Parsing(Link);

            List<Post> L_list = reader.GetPostList();

            for (int i = 0; i < L_list.Count; ++i)
                System.Diagnostics.Debug.WriteLine("Carregado de dentro do Post: " + L_list[i].Title);
            // I need first load the html file and after parse the information.
            // Because the memory issues load 3 posts per time
            //Here will load the current post in the windowsphone screen
        }
    }
}
