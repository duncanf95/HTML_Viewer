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
        }

        public string Title
        {
            get { return title; }
        }

        



        public HTML()
        {
           
        }

        

        private bool Loaded()
        {
            return true;
        }

        public string RequestPage(string URL)
        {
            url = URL;
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
                    htmlDoc += "\nStatus Code : {0}"+ ((HttpWebResponse)e.Response).StatusCode + Environment.NewLine;
                    htmlDoc += "\nStatus Description : {0}"+ ((HttpWebResponse)e.Response).StatusDescription + Environment.NewLine;
                    string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    File.WriteAllText(Path.Combine(mydocpath, "WriteFile.txt"), htmlDoc);

                    response = (HttpWebResponse)e.Response;
                }

                
            }
            catch(Exception e)
            {
                htmlDoc = e.ToString();
            }
            
            if (htmlDoc == "") htmlDoc = "allfailed";
           // Console.Write(result);
            return htmlDoc;

        }

        private void UpdateTitle()
        {

            title = Regex.Match(htmlDoc, @"<title>\s*(.+?)\s*</title>").Value.Trim();
            title = title.Replace("<title>", "");
            title = title.Replace("</title>", "");

            
        }

        public int create_xml()
        {

            int flag = 1;
            XmlDocument doc = new XmlDocument();
            string doc_path = Environment.CurrentDirectory + "\\html_test.xml";

            

            if (!File.Exists(doc_path))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(doc_path, settings);

                writer.WriteStartElement("sites");
                    writer.WriteStartElement("site");
                        writer.WriteStartElement("url");
                            writer.WriteString("new.com");
                        writer.WriteEndElement();
                        writer.WriteStartElement("title");
                            writer.WriteString("new_title");
                        writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();

                flag = 0;
            }else
            {
                doc.Load(doc_path);
                XmlNode site = doc.CreateElement("site");
                XmlNode url = doc.CreateElement("url");
                XmlNode title = doc.CreateElement("title");
                url.InnerText = "heyhey.com";
                title.InnerText = "hey_title";
                site.AppendChild(url);
                site.AppendChild(title);
                doc.DocumentElement.AppendChild(site);
                doc.Save(doc_path);
                flag = 0;
            }

            return flag;

        }
    }
}

        
