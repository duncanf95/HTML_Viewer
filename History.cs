using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Brower_v1
{
    class History : URL_List
    {
        private string xml_path;
        string url_node = "url";
        string title_node = "title";

        public History(string input_xml_path)
        {
            xml_path = input_xml_path;

            if (!File.Exists(xml_path))
            {
                create_xml_doc();
            }

           
            site_data = xml_reader();

            
        }

        private List<Dictionary<string, string>> xml_reader()
        {
            XmlDocument doc = new XmlDocument();

            

                List<Dictionary<string, string>> return_data =
                new List<Dictionary<string, string>>();

            doc.Load(xml_path);

            foreach (XmlNode node in doc.DocumentElement)
            {
                add_item(node[url_node].InnerText, node[title_node].InnerText);
            }


            return return_data;
        }

        private int xml_writer(string input_url, string input_title)
        {
            int err = 1;
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(xml_path))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(xml_path, settings);

                writer.WriteStartElement("sites");
                writer.WriteStartElement("site");
                writer.WriteStartElement(url_node);
                writer.WriteString(input_url);
                writer.WriteEndElement();
                writer.WriteStartElement(title_node);
                writer.WriteString(input_title);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();

                err = 0;
            }
            else
            {
                doc.Load(xml_path);
                XmlNode site = doc.CreateElement("site");
                XmlNode url = doc.CreateElement(url_node);
                XmlNode title = doc.CreateElement(title_node);
                url.InnerText = input_url;
                title.InnerText = input_title;
                site.AppendChild(url);
                site.AppendChild(title);
                doc.DocumentElement.AppendChild(site);
                doc.Save(xml_path);
                err = 0;
            }

            return err;
        }

        public void add_to_history(string input_url, string input_title)
        {
            xml_writer(input_url, input_title);
            site_data = xml_reader();
        }

        private void create_xml_doc()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(xml_path, settings);
            writer.WriteStartElement("sites");
            writer.WriteEndElement();
            writer.Close();
        }
    }

   

}
