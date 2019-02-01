using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Brower_v1
{
    class HistoryPanel : Panel
    {
        private Button btnClose = new Button();
        private Button btnClear = new Button();
        private int maxWidth;
        public int MaxWidth
        {
            get { return maxWidth; }
        }
        private Timer timer = new Timer();
        private bool hidden = true;
        private History history;
        private List<LinkLabel> HistoryLinks = new List<LinkLabel>();
        private List<Button> btnRemove = new List<Button>();
        private BrowserTab curTab;



        public HistoryPanel(int panelWidth, int panelHeight, History inputHistory, BrowserTab Tab)
        {
            timer.Interval = 1;



            AutoScroll = true;
            VerticalScroll.Visible = true;
            VerticalScroll.Enabled = true;


            history = inputHistory;
            curTab = Tab;

            Width = 0;
            maxWidth = panelWidth;
            Height = panelHeight;

            btnClose.Click += new EventHandler(TogglePanel);
            btnClear.Click += new EventHandler(ClearHistory);
            timer.Tick += new EventHandler(TimerTick);

            SetLinks();
            DrawLabels();


            BackColor = Color.LightGray;

            btnClear.Location = new Point(btnClose.Width, 0);
            btnClear.Text = "Clear History";

            btnClose.Text = "Close";

            Controls.Add(btnClose);
            Controls.Add(btnClear);

            


        }

        /// <summary>
        /// toggles the visibility of a panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TogglePanel(object sender, EventArgs e)
        {
            timer.Start();
        }

        /// <summary>
        /// increases or decreases width on each timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimerTick(object sender, EventArgs e)
        {
            if (hidden)
            {
                ChangeSize(+30);


            }
            else
            {
                ChangeSize(-30);

            }

        }

        /// <summary>
        /// adds input val onto width
        /// stops timer when max width has been reached
        /// change status of hidden
        /// </summary>
        /// <param name="val"></param>
        private void ChangeSize(int val)
        {
            Width += val;

            if (Width >= maxWidth || Width <= 0)
            {
                timer.Stop();
                hidden = !hidden;
            }
        }

        /// <summary>
        /// set attributes of links and their corresponding edit and delete buttons
        /// </summary>
        private void SetLinks()
        {
            int c = 0;
            foreach (string[] s in history.SiteData)
            {

                HistoryLinks.Add(new LinkLabel());
                HistoryLinks[c].Text = s[1];
                HistoryLinks[c].Width = maxWidth - HistoryLinks[c].Height - 5;
                HistoryLinks[c].Click += new EventHandler((sender, e) => SelectFavourite(sender, e, s[0]));


                btnRemove.Add(new Button());
                btnRemove[c].Text = "x";
                btnRemove[c].Height = HistoryLinks[c].Height;
                btnRemove[c].Width = HistoryLinks[c].Height;
                btnRemove[c].Click += new EventHandler((sender, e) => RemoveHistory(sender, e, s[0]));

                c++;
            }
        }

        /// <summary>
        /// adds labels and their corresponding buttons to the correct location
        /// </summary>
        private void DrawLabels()
        {
            int c = 0;
            foreach (LinkLabel l in HistoryLinks)
            {
                l.Location = new Point(0, (l.Height * c) + btnClose.Height);
                Controls.Add(l);
                c++;
            }

            c = 0;

            foreach (Button b in btnRemove)
            {
                b.Location = new Point(maxWidth - b.Width, (b.Height * c) + btnClose.Height);
                Controls.Add(b);
                c++;
            }

        }

        /// <summary>
        /// instructs current tab to request the selected url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="url">the url the user has clicked</param>
        private void SelectFavourite(object sender, EventArgs e, string url)
        {
            TogglePanel(sender, e);
            curTab.URL = url;

            curTab.RequestPageFromClick(sender, e);
        }
        /// <summary>
        /// removes history from history object
        /// makes sure data is not being edited
        /// updates panel to reflect new list of links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="url"></param>
        private void RemoveHistory(object sender, EventArgs e, string url)
        {
            history.RemoveFromHistory(url);
            curTab.WaitForUpdate();
            PanelUpdate();
        }

        /// <summary>
        /// remeoves all buttons and labels 
        /// removes confirm button and edit text box
        /// resets all links for link labels
        /// redraws new labels
        /// </summary>
        public void PanelUpdate()
        {
            foreach (LinkLabel l in HistoryLinks)
            {
                Controls.Remove(l);
            }
            foreach (Button b in btnRemove)
            {
                Controls.Remove(b);
            }
            HistoryLinks = new List<LinkLabel>();
            btnRemove = new List<Button>();
            SetLinks();
            DrawLabels();
        }

        /// <summary>
        /// calls clear history from history object
        /// ensures the thread is not accessing data
        /// updates panel to show updated list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearHistory(object sender, EventArgs e)
        {
            history.ClearHistory();
            curTab.WaitForUpdate();
            PanelUpdate();
        }


    }
}
