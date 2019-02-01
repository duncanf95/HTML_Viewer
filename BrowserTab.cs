using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Brower_v1
{
    class BrowserTab : TabPage
    {
        private Button btnRequest = new Button();
        private Button btnFavourite = new Button();
        private TabControl tc = new TabControl();

        private TextBox txtQuery = new TextBox();
        private TextBox txtHtml = new TextBox();

        private HTML html = new HTML();

        private int tab_width;
        private int tab_height;
 

        public BrowserTab(TabControl current)
        {
            tab_width = current.Width;
            tab_height = current.Height;

            tc = current;

            SetLocation();
            SetFunctions();

            Controls.Add(txtQuery);
            Controls.Add(btnRequest);
            Controls.Add(btnFavourite);
            Controls.Add(txtHtml);

            
        }

        private void SetLocation()
        {
            txtQuery.Width = (int)(tab_width * 0.8);

            txtHtml.Width = tab_width -5;
            txtHtml.Multiline = true;
            txtHtml.ScrollBars = ScrollBars.Vertical;
            txtHtml.ReadOnly = true;
            txtHtml.Height = tc.Height - txtQuery.Height;


            btnRequest.Height = txtQuery.Height;
            btnRequest.Width = txtQuery.Height;
            btnRequest.Text = "→";

            btnFavourite.Height = txtQuery.Height;
            btnFavourite.Width = txtQuery.Height;
            btnFavourite.Text = "★";

            btnRequest.Location = new Point(txtQuery.Width, 0);

            btnFavourite.Location = new Point(txtQuery.Width + btnRequest.Width, 0);

            txtHtml.Location = new Point(0, txtQuery.Height);


        }

        private void SetFunctions()
        {
            btnRequest.Click += new EventHandler(requestPage);
            btnFavourite.Click += new EventHandler(AddTab);
            
            
        }

        private void AddTab(object sender, EventArgs e)
        {
            BrowserTab newPage = new BrowserTab(tc);
            tc.TabPages.Add(newPage);
        }

        private void requestPage(object sender, EventArgs e)
        {
            Thread thread;
            ThreadStart htmlWorker = new ThreadStart(RequestPageWork);
            thread = new Thread(htmlWorker);
            thread.Start();
        }

        private void RequestPageWork()
        {
            html.RequestPage(txtQuery.Text);
            txtHtml.Text = html.HtmlDoc;
            Text = html.Title;
        }
    }
}
