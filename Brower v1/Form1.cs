using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Brower_v1
{
    public partial class Browser : Form
    {
        private HTML html = new HTML();
        private Favourites fave = new Favourites("fave.xml");
        private History history = new History("history.xml");
        private string url, html_text, title;




        public Browser()
        {
            InitializeComponent();

        }

        private void URL_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            string binDir = @"bin";
            string faveDir = binDir + @"\favourites";
            string histDir = binDir + @"\historys";
            string userDir = binDir + @"\users";
            string settingDir = binDir + @"\settings";
            



            if (!Directory.Exists(binDir))
            {
                Directory.CreateDirectory(binDir);
            }

            if (!Directory.Exists(faveDir))
            {
                Directory.CreateDirectory(faveDir);
            }

            if (!Directory.Exists(histDir))
            {
                Directory.CreateDirectory(histDir);
            }

            if (!Directory.Exists(userDir))
            {
                Directory.CreateDirectory(userDir);
            }

            if (!Directory.Exists(settingDir))
            {
                Directory.CreateDirectory(settingDir);
            }

            User user = new User();
            Favourites fave = new Favourites("bin/favourites/favourites.xml");
            History hist = new History("bin/historys/history.xml");
            BrowserTab homePage = new BrowserTab(tabController, this, tabController.TabPages[0].Width, tabController.TabPages[0].Height, user, fave, hist);
            KeyPress += new KeyPressEventHandler(homePage.KeyPressFunc);
            tabController.TabPages.Clear();
            tabController.TabPages.Add(homePage);
            splitContainer1.Panel1.Hide();
        }

        private void tp__Click(object sender, EventArgs e)
        {

        }

        private void request_btn_Click(object sender, EventArgs e)
        {


       
            
        }

        private void btn_Fave_Click(object sender, EventArgs e)
        {
            // fave.add_to_favourites(url, title);
            
            
            
            
        }
        
    }
}
