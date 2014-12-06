using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;

using HtmlAgilityPack;

using System.Net.Http;
using System.Xml;

using System.ServiceModel.Syndication;
using System.Xml;

using Windows.UI.Popups;

using Signals;

namespace BlogReader
{
    //public Signal<bool> onBlogReceived;

    public class Reader
    {
        private List<Post> m_postList = new List<Post>();

        public List<Post> GetPostList()
        {
            return m_postList;
        }

        public void Parsing(string website)
        {
            try
            {
                HttpClient http = new HttpClient();

                var responseTask = http.GetByteArrayAsync(website);
                responseTask.Wait();

                var response = responseTask.Result;
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


                    System.Diagnostics.Debug.WriteLine("link: " + link);
                    System.Diagnostics.Debug.WriteLine("img: " + img);
                    System.Diagnostics.Debug.WriteLine("title: " + title);

                    m_postList.Add(new Post()
                     {
                         ImageAuthorUrl = img,
                         Title = title,
                         Link = link
                     });
                 }
 
                 //listboxproduct.DataContext = listproduct;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

/*
        public static string GetHTML(string strURL)
        {
            string strResult;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";

            request.BeginGetResponse(GetPostsInformation, request);


            return strResult;
        }*/

        static void GetPostsInformation(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;

            if (request != null)
            {
                try
                {
                    WebResponse response = request.EndGetResponse(result);

//                  avatarImg = Texture2D.FromStream(
//                  graphics.GraphicsDevice,
//                  response.GetResponseStream());
                }
                catch (WebException e)
                {
//                     gamerTag = "Gamertag not found.";
//                     return;
                }
            }
        }

    }
}
