using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;

using HtmlAgilityPack;
using Elmah;

using System.Net.Http;
using System.Xml.XPath;

using System.ServiceModel.Syndication;
using System.Xml;

namespace BlogReader
{
    public class Reader
    {
        static List<Post> listpost = new List<Post>();

        static int Main(string[] args)
        {

            Parsing("http://leninja.com.br");

            //Console.WriteLine( + "\n");
                



//             XmlTextReader reader = new XmlTextReader("http://blog.mundodositio.globo.com/feed/");
//             SyndicationFeed feed = SyndicationFeed.Load(reader);
//             Console.WriteLine(feed.Title.Text);
//             Console.WriteLine(feed.Items.Count());

            Console.ReadKey();

            return 0;
        }

         
       static private async void Parsing(string website)
        {
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.GetByteArrayAsync(website);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument resultat = new HtmlDocument();
                resultat.LoadHtml(source);

                List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
                (x => (x.Name == "section" && x.Attributes["id"] != null && x.Attributes["id"].Value.Contains("posts"))).ToList();

                var pos = toftitle[0].Descendants("article").ToList();
                foreach (var item in pos)
                {
                    var link = item.Descendants("a").ToList()[0].GetAttributeValue("href", null);
                    var img = item.Descendants("img").ToList()[0].GetAttributeValue("src", null);
                    var title = item.Descendants("h1").ToList()[0].InnerText;

                    Console.WriteLine("\n");
                    Console.WriteLine("Post Title: " + title);
                    Console.WriteLine("Post Image URL: " + img);
                    Console.WriteLine("Post Link: " + link);
                    Console.WriteLine("\n");

                    listpost.Add(new Post()
                     {
                         ImageAuthorUrl = img,
                         Title = title,
                         Link = link
                     });
                 }
 
                 //listboxproduct.DataContext = listproduct;

            }
            catch (Exception)
            {

                //MessageBox.Show("Network Problem!");
            }

        }


        public static string GetHTML(string strURL)
        {
            string strResult;

            HttpWebRequest wbrq = (HttpWebRequest)WebRequest.Create(strURL);
            wbrq.Method = "GET";
            HttpWebResponse wbrs = (HttpWebResponse)wbrq.GetResponse();
            StreamReader sr = new StreamReader(wbrs.GetResponseStream());

            strResult = sr.ReadToEnd();
            sr.Close();

            return strResult;
        }

    }
}
