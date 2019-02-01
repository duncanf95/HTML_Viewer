using System;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Security.Cryptography; 
using System.Text.RegularExpressions;
using HtmlAgilityPack;


namespace Brower_v1
{
    class User
    {
        private string username;
        private string password;
        private string homePage = "";

        private bool loggedIn = false;

        public bool LoggedIn
        {
            get { return loggedIn; }
        }


        private string settingsXmlPath = "bin/settings/settings.xml";
        private string usersXmlPath = "bin/users/users.xml";

        public User()
        {
            if (!File.Exists(usersXmlPath))
            {
                CreateUsersFile();
            }

            if (!File.Exists(settingsXmlPath))
            {
                CreateSettingsFile();
            }
        }


        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string HomePage
        {
            get { return homePage; }
            set { homePage = value; }
        }

        /// <summary>
        /// checks users file for username
        /// checks username against password
        /// if match return true
        /// else returns false
        /// </summary>
        /// <returns>state of login sucess</returns>
        public bool Login(BrowserTab b)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(usersXmlPath);

            
                foreach (XmlNode node in doc.DocumentElement)
                {
                    if ((node["username"].InnerText == username) &
                            (node["password"].InnerText == password))
                    {
                        loggedIn = true;
                        settingsXmlPath = @"bin/settings/" + username + "settings.xml";
                        b.UpdateDirs();
                        Console.WriteLine(settingsXmlPath);
                        ReadSettings();
                        return  true;
                    }
                    

                }

            return false;
                
            
        }

        /// <summary>
        /// writes new user info to users xml file
        /// </summary>
        /// <returns></returns>
        public bool SignUp()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(usersXmlPath);


            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node["username"].InnerText == username) 
                {
                    return false;
                }
            }

            try
            {
                WriteNewSetting();

                doc.Load(usersXmlPath);
                XmlNode userInput = doc.CreateElement("user");
                XmlNode user = doc.CreateElement("username");
                XmlNode pass = doc.CreateElement("password");
                user.InnerText = username;
                pass.InnerText = password;
                userInput.AppendChild(user);
                userInput.AppendChild(pass);
                doc.DocumentElement.AppendChild(userInput);
                doc.Save(usersXmlPath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }     
        }

        /// <summary>
        /// writes settings for new user
        /// </summary>
        private void WriteNewSetting()
        {
            XmlDocument doc = new XmlDocument();

           

            doc.Load(settingsXmlPath);
            XmlNode userInput = doc.CreateElement("settings");
            XmlNode user = doc.CreateElement("user");
            XmlNode home = doc.CreateElement("homepage");
            user.InnerText = username;
            home.InnerText = homePage;
            userInput.AppendChild(user);
            userInput.AppendChild(home);
            doc.DocumentElement.AppendChild(userInput);
            doc.Save(settingsXmlPath);
        }
        /// <summary>
        /// reads users settings file
        /// </summary>
        private void ReadSettings()
        {
            if (!File.Exists(settingsXmlPath))
            {
                CreateSettingsFile();
                WriteNewSetting();
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(settingsXmlPath);

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node["user"].InnerText == username)
                {
                    homePage = node["homepage"].InnerText;
                }

            }
        }

        /// <summary>
        /// edits xml value for users home
        /// </summary>
        /// <param name="url"></param>
        public void SetHome(string url)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(settingsXmlPath);

            foreach (XmlNode node in doc.DocumentElement)
            {
                if (node["user"].InnerText == username)
                {
                    homePage = url;
                    node["homepage"].InnerText = url;
                    doc.Save(settingsXmlPath);
                }

            }
        }

        /// <summary>
        /// creates new settings file for users
        /// </summary>
        private void CreateSettingsFile()
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(settingsXmlPath, settings);

            writer.WriteStartElement("settings");
            writer.WriteEndElement();
            writer.Close();
        }

        /// <summary>
        /// creates new users xml file
        /// </summary>
        private void CreateUsersFile()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(usersXmlPath, settings);

            writer.WriteStartElement("Users");
            writer.WriteEndElement();
            writer.Close();

            CreateGuest();
            
        }

        /// <summary>
        /// delets file from input path
        /// </summary>
        /// <param name="path">path of file for deletion</param>
        private void DeleteFile(string path)
        {
            File.Delete(path);
        }

        private void CreateGuest()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(usersXmlPath);
            CreateGuestSetting();
            try
            {

                doc.Load(usersXmlPath);
                XmlNode userInput = doc.CreateElement("user");
                XmlNode user = doc.CreateElement("username");
                XmlNode pass = doc.CreateElement("password");
                user.InnerText = "";
                pass.InnerText = "";
                userInput.AppendChild(user);
                userInput.AppendChild(pass);
                doc.DocumentElement.AppendChild(userInput);
                doc.Save(usersXmlPath);
            }
            catch (Exception)
            {
                MessageBox.Show("something went wrong, sorry about that");
            }
        }

        private void CreateGuestSetting()
        {
            XmlDocument doc = new XmlDocument();



            doc.Load(settingsXmlPath);
            XmlNode userInput = doc.CreateElement("settings");
            XmlNode user = doc.CreateElement("user");
            XmlNode home = doc.CreateElement("homepage");
            user.InnerText = "";
            home.InnerText = "";
            userInput.AppendChild(user);
            userInput.AppendChild(home);
            doc.DocumentElement.AppendChild(userInput);
            doc.Save(settingsXmlPath);
        }

      
    }
}
