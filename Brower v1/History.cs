using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Brower_v1
{
    class History : URL_List
    {
        private string xmlPath;
        string url_node = "url";
        string title_node = "title";

        public string XmlPath
        {
            get { return xmlPath; }
            set { xmlPath = value; }
        }

        public History(string input_xml_path)
        {
            xmlPath = input_xml_path;

            if (!File.Exists(xmlPath))
            {
                CreateFile();
            }

            XmlReader();

        }

        /// <summary>
        /// reads content of xml_document to list
        /// </summary>
        private void XmlReader()
        {
            if (!File.Exists(xmlPath))
            {
                CreateFile();
            }
            XmlDocument doc = new XmlDocument();

            site_data = new List<string[]>();

            doc.Load(xmlPath);

            string[] data = new string[2];

            foreach (XmlNode node in doc.DocumentElement)
            {
                add_item(node[url_node].InnerText, node[title_node].InnerText);
            }
        }

        /// <summary>
        /// writes url and corresponding title to XML document
        /// </summary>
        /// <param name="inputUrl"></param>
        /// <param name="inputTitle"></param>
        private void XmlWrite(string input_url, string input_title)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(xmlPath);
            XmlNode site = doc.CreateElement("site");
            XmlNode url = doc.CreateElement(url_node);
            XmlNode title = doc.CreateElement(title_node);
            url.InnerText = input_url;
            title.InnerText = input_title;
            site.AppendChild(url);
            site.AppendChild(title);
            doc.DocumentElement.AppendChild(site);
            doc.Save(xmlPath);
        }

        /// <summary>
        /// writes item to history file 
        /// reads updated history file
        /// </summary>
        /// <param name="input_url"></param>
        /// <param name="input_title"></param>
        public void AddToHistory(string input_url, string input_title)
        {
            XmlWrite(input_url, input_title);
            XmlReader();
        }

        /// <summary>
        /// removes item from document with input url
        /// saves document
        /// reads new xml document
        /// </summary>
        /// <param name="url">url to search for in XML document</param>
        public void RemoveFromHistory(string url)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node["url"].InnerText == url)
                {
                    node.ParentNode.RemoveChild(node);
                    doc.Save(xmlPath);
                    break;
                }
            }

            XmlReader();
        }

        /// <summary>
        /// creates new XML file
        /// </summary>
        private void CreateFile()
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(xmlPath, settings);

            writer.WriteStartElement("sites");
            writer.WriteEndElement();
            writer.Close();
        }

        /// <summary>
        /// Deletes XML file
        /// </summary>
        private void DeleteFile()
        {
            File.Delete(xmlPath);
        }

        /// <summary>
        /// clears users history
        /// </summary>
        public void ClearHistory()
        {
            DeleteFile();
            CreateFile();
            site_data = new List<string[]>();

        }

        public void Update()
        {
            XmlReader();
        }


    }

   

}
