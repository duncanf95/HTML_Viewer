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
            BrowserTab homePage = new BrowserTab(tabController);
            tabController.TabPages.Clear();
            tabController.TabPages.Add(homePage);
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
