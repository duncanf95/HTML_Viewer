using System;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using HtmlAgilityPack;



namespace Brower_v1
{
    class HTML
    {
 
        private string htmlDoc;
        private string url;
        private string title;

        public string HtmlDoc
        {
            get { return htmlDoc; }
        }

        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        public string Title
        {
            get { return title; }
        }
    
        /// <summary>
        /// starts new http objects
        /// trys to get request from response
        /// if try successful get html doc and title
        /// if caught print web exception
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public string RequestPage(string URL)
        {
            url = URL;
            if(url.Substring(0, 4) != "http"){
                if(url.Substring(0, 4) != "www.")
                {
                    url = "http://www." + url;
                }
            }
            Console.WriteLine("url: " + URL);
            HttpWebRequest request; 
            HttpWebResponse response;

            Uri uri = new Uri(url);
            StreamReader reader = null;
            Encoding enc = Encoding.GetEncoding(1252);
            

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
         
                reader = new StreamReader(response.GetResponseStream(),enc);

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("ok");
                }

                htmlDoc = reader.ReadToEnd();
                string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                File.WriteAllText(Path.Combine(mydocpath, "WriteFile.txt"), htmlDoc);

                UpdateTitle();

                Console.WriteLine(title);
                
                response.Close();
                reader.Close();



            }
            catch (WebException e)
            {
                htmlDoc = "This program is expected to throw WebException on successful run." +
                        "\n\nException Message :" + e.Message;
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    htmlDoc += "\nStatus Code :"+ ((HttpWebResponse)e.Response).StatusCode + Environment.NewLine;
                    htmlDoc += "\nStatus Description :"+ ((HttpWebResponse)e.Response).StatusDescription + Environment.NewLine;
                    string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    response = (HttpWebResponse)e.Response;
                }

                
            }
            

            return htmlDoc;

        }
        /// <summary>
        /// updates the title from the html doc
        /// </summary>
        private void UpdateTitle()
        {

            title = Regex.Match(htmlDoc, @"<title>\s*(.+?)\s*</title>").Value.Trim();
            title = title.Replace("<title>", "");
            title = title.Replace("</title>", "");
            
        }

        
    }
}

        
