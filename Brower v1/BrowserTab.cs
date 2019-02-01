using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;

namespace Brower_v1
{
    class BrowserTab : TabPage
    {
        private Button btnRequest = new Button();
        private Button btnFavourite = new Button();
        private Button btnForward = new Button();
        private Button btnBack = new Button();
        private Button btnHomeSave = new Button();
        private Button btnRefresh = new Button();
        private Button btnNewTab = new Button();
        private Button btnCloseTab = new Button();
        private Button btnShowHistory = new Button();
        private Button btnShowFavourites = new Button();

        private TabControl tc = new TabControl();

        private TextBox txtQuery = new TextBox();
        private TextBox txtHtml = new TextBox();

        private List<string> sessionUrls = new List<string>();
        private int curIndex = 0;

        private HTML html = new HTML();
        public string URL
        {
            get { return html.URL; }
            set { html.URL = value; }
        }

 
        private int tab_index = 0;

        private Thread thread;

        private Form form;

        private History history;
        private Favourites favourites;
        private User user;

        private FavouritesPanel favePanel;
        private HistoryPanel historyPanel;
        private LoginPanel loginPanel;

        private delegate void SetTextCallback(string text);
        private delegate void SetBoolCallback(bool boolean);
        private delegate void SetBrowserTabCallBack(BrowserTab b);

        /// <summary>
        /// Constructor for the browser tab
        /// </summary>
        /// <param name="current"> Inputs the current tab control in order to read its width and height</param>
        /// <param name="owningForum"> gives tab te input form that it is initialised on</param>
        /// <param name="inputWidth">gives the width for the tab</param>
        /// <param name="inputHeight">gives the height for the tab</param>
        public BrowserTab(TabControl current, Form owningForum, int inputWidth, int inputHeight, User inputUser, Favourites inputFave, History inputHist)
        {
            
            user = inputUser;

            favourites = inputFave;
            history = inputHist;
            Width = inputWidth;
            Height = inputHeight;

            favePanel = new FavouritesPanel(Width / 5, Height,favourites, this);
            historyPanel = new HistoryPanel(Width / 5, Height, history, this);
            

            form = owningForum;
            Margin = new Padding(50, 50, 50, 50);
            tc = current;

            tab_index += tc.TabCount;

            SetAttributes();
            SetLocations();
            SetFunctions();

            btnForward.Enabled = false;
            btnBack.Enabled = false;

            Controls.Add(txtQuery);
            Controls.Add(btnRequest);
            Controls.Add(btnRefresh);
            Controls.Add(btnFavourite);
            Controls.Add(btnForward);
            Controls.Add(btnBack);
            Controls.Add(btnHomeSave);
            Controls.Add(txtHtml);
            Controls.Add(btnRefresh);
            Controls.Add(btnNewTab);
            Controls.Add(btnCloseTab);
            Controls.Add(favePanel);
            Controls.Add(historyPanel);
            Controls.Add(btnShowHistory);
            Controls.Add(btnShowFavourites);
            

            if(user.LoggedIn != true)
            {
                loginPanel = new LoginPanel(Width, Height, user, this);
                Controls.Add(loginPanel);
                loginPanel.Show();
                loginPanel.BringToFront();              
            }
            
        }
        /// <summary>
        /// Set the attributes of the gui elements of the form, I.E width, height, text
        /// </summary>
        private void SetAttributes()
        {
            txtQuery.Width = (Width) - (txtQuery.Height * 7);

            txtHtml.Width = Width;
            txtHtml.Multiline = true;
            txtHtml.ScrollBars = ScrollBars.Vertical;
            txtHtml.ReadOnly = true;
            txtHtml.Height = tc.Height - txtQuery.Height;


            btnRequest.Height = txtQuery.Height;
            btnRequest.Width = txtQuery.Height * 2;
            btnRequest.Text = "Go";

            btnFavourite.Height = txtQuery.Height;
            btnFavourite.Width = txtQuery.Height;
            btnFavourite.Text = "★";

            btnForward.Height = txtQuery.Height;
            btnForward.Width = txtQuery.Height;
            btnForward.Text = "→";

            btnBack.Height = txtQuery.Height;
            btnBack.Width = txtQuery.Height;
            btnBack.Text = "←";

            btnHomeSave.Height = txtQuery.Height;
            btnHomeSave.Width = txtQuery.Height;
            btnHomeSave.Text = "🏠";

            btnRefresh.Height = txtQuery.Height;
            btnRefresh.Width = txtQuery.Height;
            btnRefresh.Text = "↻";

            btnNewTab.Height = txtQuery.Height * 2;
            btnNewTab.Width = (btnRequest.Width + btnRefresh.Width + btnFavourite.Width 
                + btnHomeSave.Width) / 2;
            btnNewTab.Text = "New\nTab";

            btnCloseTab.Height = txtQuery.Height * 2;
            btnCloseTab.Width = btnNewTab.Width;
            btnCloseTab.Text = "Close\nTab ";

            btnShowHistory.Height = txtQuery.Height * 2;
            btnShowHistory.Width = btnNewTab.Width;
            btnShowHistory.Text = "Show\nHistory";

            btnShowFavourites.Height = txtQuery.Height * 2;
            btnShowFavourites.Width = btnNewTab.Width;
            btnShowFavourites.Text = "Show\nFavourites";


        }
        /// <summary>
        /// sets the locations of the gui elements of the tab depending on the width and height
        /// </summary>
        private void SetLocations()
        {
            int xDrawCursor = 0;
            int yDrawCursor = 0;

            favePanel.Location = new Point(Width - favePanel.MaxWidth - 25, 0);
            historyPanel.Location =new Point(Width - historyPanel.MaxWidth - 25, 0);
            xDrawCursor += btnBack.Width;

            Console.WriteLine("tab_width"+Width);
            Console.WriteLine("width"+Width);

            btnForward.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnForward.Width;

            txtQuery.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += txtQuery.Width;

            btnRequest.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnRequest.Width;

            btnRefresh.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnRefresh.Width;

            btnFavourite.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnFavourite.Width;

            btnHomeSave.Location = new Point(xDrawCursor, yDrawCursor);
            yDrawCursor += txtQuery.Height;
            xDrawCursor -= txtQuery.Height * 9;

            btnShowFavourites.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnShowFavourites.Width;

            btnShowHistory.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnShowHistory.Width; 

            btnNewTab.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor += btnNewTab.Width;

            btnCloseTab.Location = new Point(xDrawCursor, yDrawCursor);
            xDrawCursor = 0;
            yDrawCursor += btnCloseTab.Height;




            txtHtml.Location = new Point(xDrawCursor, yDrawCursor);
            
        }
        /// <summary>
        /// set what functions will execute when each button is pressed
        /// </summary>
        private void SetFunctions()
        {
            btnRequest.Click += new EventHandler(RequestPage);
            btnFavourite.Click += new EventHandler(AddFave);
            btnBack.Click += new EventHandler(PageBack);
            btnForward.Click += new EventHandler(PageForward);
            btnRefresh.Click += new EventHandler(Refresh);
            btnNewTab.Click += new EventHandler(AddTab);
            btnCloseTab.Click += new EventHandler(RemoveTab);
            btnHomeSave.Click += new EventHandler(AddHome);
            btnShowHistory.Click += new EventHandler(ShowHistory);
            btnShowFavourites.Click += new EventHandler(ShowFavourites);
     

        }

        /// <summary>
        /// removes tab from owning tab control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void RemoveTab(object sender, EventArgs e)
        {
            tc.TabPages.Remove(tc.SelectedTab);
            if(tc.TabCount == 0)
            {
                form.Close();
            }
        }

        /// <summary>
        /// sets the tab thread to request a page and will wait for a join if the thread is busy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RequestPage(object sender, EventArgs e)
        {

            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart htmlWorker = new ThreadStart(RequestPageWork);
            thread = new Thread(htmlWorker);
            
            thread.Start();
        }
        /// <summary>
        /// Use html object to request page from the input url
        /// set gui elements to reflect the new html
        /// adds element to the history
        /// changes forward and back buttons to reflect the current index
        /// </summary>
        private void RequestPageWork()
        {
            if (sessionUrls.Count != curIndex)
            {
                List <string> temp = new List<string>();
                for(int i = 0; i < curIndex; i++)
                {
                    temp.Add(sessionUrls[i]);
                }
                sessionUrls = temp;
                sessionUrls.Add(txtQuery.Text);
            }
            else
            {
                sessionUrls.Add(txtQuery.Text);
            }
            curIndex++;
            Console.WriteLine(curIndex);
            if (txtQuery.Text.Length >= 16)
            {
                if (txtQuery.Text.Substring(0, 16) == "http://deelay.me")
                {
                    int index = 17;
                    int sleep = 0;
                    string temp = "";
                    while (txtQuery.Text.Substring(index, 1) != "/")
                    {

                        temp += txtQuery.Text.Substring(index, 1);
                        index++;
                    }
                    Int32.TryParse(temp, out sleep);
                    Thread.Sleep(sleep);
                    Console.WriteLine(temp);
                }
                Console.WriteLine(txtQuery.Text.Substring(0, 16));
            }
            
            html.RequestPage(txtQuery.Text);
            SetTxtHtml(html.HtmlDoc);
            SetTabTitle(html.Title);
            SetTxtQuery(html.URL);
            history.AddToHistory(html.URL, html.Title);

            if (sessionUrls.Count == curIndex)
            {
                SetBtnForwardEnabled(false);
            }
            else
            {
                SetBtnForwardEnabled(true);
            }

            if(sessionUrls.Count > 1)
            {
                SetBtnBackEnabled(true);
            }else
            {
                SetBtnBackEnabled(false);
            }
        }
        /// <summary>
        /// request pgae from either a click on a favourite or history item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RequestPageFromClick(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart htmlWorker = new ThreadStart(RequestPageFromClickWork);
            thread = new Thread(htmlWorker);
            thread.Start();
        }
        /// <summary>
        /// Use html object to request page from the input url
        /// set gui elements to reflect the new html
        /// adds element to the history
        /// changes forward and back buttons to reflect the current index
        /// </summary>
        private void RequestPageFromClickWork()
        {
            Console.WriteLine(URL);
            sessionUrls.Add(URL);
            curIndex++;
            Console.WriteLine(curIndex);
            html.RequestPage(URL);
            SetTxtHtml(html.HtmlDoc);
            SetTxtQuery(html.URL);
            SetTabTitle(html.Title);
            history.AddToHistory(html.URL, html.Title);

            if (sessionUrls.Count == curIndex)
            {
                SetBtnForwardEnabled(false);
            }
            else
            {
                SetBtnForwardEnabled(true);
            }

            if (sessionUrls.Count > 1)
            {
                SetBtnBackEnabled(true);
            }
            else
            {
                SetBtnBackEnabled(false);
            }
        }

        /// <summary>
        /// creates thread to load the page forward from the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageForward(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart htmlWorker = new ThreadStart(PageForwardWork);
            thread = new Thread(htmlWorker);
            thread.Start();  
        }
        /// <summary>
        /// creates thread to load the page back from the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageBack(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart htmlWorker = new ThreadStart(pageBackWork);
            thread = new Thread(htmlWorker);
            thread.Start();
        }
        /// <summary>
        /// Use html object to request page from the input url
        /// set gui elements to reflect the new html
        /// adds element to the history
        /// changes forward and back buttons to reflect the current index 
        /// </summary>
        private void PageForwardWork()
        {
            curIndex++;
            html.RequestPage(sessionUrls[curIndex - 1]);
            SetTxtHtml(html.HtmlDoc);
            SetTxtQuery(html.URL);
            SetTabTitle(html.Title);

            if (sessionUrls.Count > 1)
            {
                SetBtnBackEnabled(true);
            }
            else
            {
                SetBtnBackEnabled(false);
            }

            if (sessionUrls.Count == curIndex)
            {
                SetBtnForwardEnabled(false);
            }
            else
            {
                SetBtnForwardEnabled(true);
            }
        }
        /// <summary>
        /// Use html object to request page from the input url
        /// set gui elements to reflect the new html
        /// adds element to the history
        /// changes forward and back buttons to reflect the current index 
        /// </summary>
        private void pageBackWork()
        {
            curIndex--;
            html.RequestPage(sessionUrls[curIndex - 1]);
            SetTxtHtml(html.HtmlDoc);
            SetTxtQuery(html.URL);
            SetTabTitle(html.Title);
            Console.WriteLine("cureindex: " + curIndex);

            if (curIndex - 1 == 0)
            {
                SetBtnBackEnabled(false);
            }
            else if(sessionUrls.Count > 1)
            {
                SetBtnBackEnabled(true);
            }

            if (sessionUrls.Count == curIndex - 1)
            {
                SetBtnForwardEnabled(false);
            }
            else
            {
                SetBtnForwardEnabled(true);
            }
        }
        /// <summary>
        /// creates thread to refresh the page, waits for join if thread is in use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart htmlWorker = new ThreadStart(RefreshWork);
            thread = new Thread(htmlWorker);
            thread.Start();
        }
        /// <summary>
        /// Use html object to request page from the input url
        /// set gui elements to reflect the html
        /// changes forward and back buttons to reflect the current index 
        /// </summary>
        private void RefreshWork()
        {
            Console.WriteLine(curIndex);
            html.RequestPage(txtQuery.Text);
            SetTxtHtml(html.HtmlDoc);
            SetTabTitle(html.Title);

            if (sessionUrls.Count == curIndex)
            {
                SetBtnForwardEnabled(false); 
            }
            else
            {
                SetBtnForwardEnabled(true); 
            }

            if (sessionUrls.Count > 1)
            {
                SetBtnBackEnabled(true);
            }
            else
            {
                SetBtnBackEnabled(false);
            }
        }
        /// <summary>
        /// starts a new thread to add item to the users favourite list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFave(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart xmlWorker = new ThreadStart(addFaveWork);
            thread = new Thread(xmlWorker);
            thread.Start();
            
        }
        /// <summary>
        /// uses favourite object to add item to favourites
        /// </summary>
        private void addFaveWork()
        {
            favourites.AddToFavourites(html.URL, html.Title);
        }
        /// <summary>
        /// opens panel containing favourites
        /// brings panel to th front of the gui
        /// makes sure thread is not active so dfaves can be read safely
        /// updates the panel items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowFavourites(object sender, EventArgs e)
        {
            favePanel.BringToFront();
            if (thread != null)
            {
                thread.Join();
            }
            favePanel.PanelUpdate();
            favePanel.TogglePanel(sender, e);
        }
        /// <summary>
        /// opens panel containing history
        /// brings panel to th front of the gui
        /// makes sure thread is not active so dfaves can be read safely
        /// updates the panel items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHistory(object sender, EventArgs e)
        {
            historyPanel.BringToFront();
            if (thread != null)
            {
                thread.Join();
            }
            historyPanel.PanelUpdate();
            historyPanel.TogglePanel(sender, e);
        }
        /// <summary>
        /// allows panels to safely access information that is also accessed by the threads
        /// </summary>
        public void WaitForUpdate()
        {
            if (thread != null)
            {
                thread.Join();
            }
        }
        /// <summary>
        /// creates thread to make item home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddHome(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart xmlWorker = new ThreadStart(AddHomeWork);
            thread = new Thread(xmlWorker);
            thread.Start();
        }
        /// <summary>
        /// uses user object to set user home page
        /// </summary>
        private void AddHomeWork()
        {
            user.SetHome(txtQuery.Text);
        }
        /// <summary>
        /// creates thread to load home page
        /// </summary>
        public void LoadHome()
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart htmlWorker = new ThreadStart(LoadHomeWork);
            thread = new Thread(htmlWorker);
            thread.Start();
        }
        /// <summary>
        /// gets home page from user object
        /// adds home page to session urls
        /// sets tab title, html box and query bar to correct text
        /// adds url to history
        /// updates forward and back buttons
        /// </summary>
        private void LoadHomeWork()
        {
            curIndex++;
            Console.WriteLine(curIndex);
            html.RequestPage(user.HomePage);
            sessionUrls.Add(user.HomePage);
            SetTxtHtml(html.HtmlDoc);
            SetTxtQuery(html.URL);
            SetTabTitle(html.Title);
            history.AddToHistory(html.URL, html.Title);

            if (sessionUrls.Count == curIndex)
            {
                SetBtnForwardEnabled(false);
            }
            else
            {
                SetBtnForwardEnabled(true);
            }

            if (sessionUrls.Count > 1)
            {
                SetBtnBackEnabled(true);
            }
            else
            {
                SetBtnBackEnabled(false);
            }
        }
        /// <summary>
        /// safely access text element of html text box
        /// </summary>
        /// <param name="inputHtml">a string holding html document</param>
        private void SetTxtHtml(string inputHtml)
        {
            if (txtHtml.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTxtHtml);
                this.Invoke(d, new object[] { inputHtml });
            }
            else
            {
                txtHtml.Text = inputHtml;
            }
        }
        /// <summary>
        /// safely access text element of query text box
        /// </summary>
        /// <param name="inputUrl"></param>
        private void SetTxtQuery(string inputUrl)
        {
            if (txtQuery.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTxtQuery);
                this.Invoke(d, new object[] { inputUrl });
            }
            else
            {
                txtQuery.Text = inputUrl;
            }
        }
        /// <summary>
        /// safely access text element of tab title
        /// </summary>
        /// <param name="inputTitle"></param>
        private void SetTabTitle(string inputTitle)
        {
            if (InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTabTitle);
                this.Invoke(d, new object[] { inputTitle });
            }
            else
            {
                Text = inputTitle;
            }
        }
        /// <summary>
        /// safely access enabled element of forward button
        /// </summary>
        /// <param name="inputEnabled"></param>
        private void SetBtnForwardEnabled(bool inputEnabled)
        {
            if (InvokeRequired)
            {
                SetBoolCallback d = new SetBoolCallback(SetBtnForwardEnabled);
                this.Invoke(d, new object[] { inputEnabled });
            }
            else
            {
                btnForward.Enabled = inputEnabled;
            }
        }
        /// <summary>
        /// safely access enabled element of back button
        /// </summary>
        /// <param name="inputEnabled"></param>
        private void SetBtnBackEnabled(bool inputEnabled)
        {
            if (InvokeRequired)
            {
                SetBoolCallback d = new SetBoolCallback(SetBtnBackEnabled);
                this.Invoke(d, new object[] { inputEnabled });
            }
            else
            {
                btnBack.Enabled = inputEnabled;
            }
        }
        private void AddTab(object sender, EventArgs e)
        {
            if (thread != null)
            {
                thread.Join();
            }
            ThreadStart htmlWorker = new ThreadStart(AddTabWork);
            thread = new Thread(htmlWorker);
            thread.Start();
        }

        private void AddTabWork()
        {
            BrowserTab newPage = new BrowserTab(tc, form, Width, Height, user, favourites, history);
            AddTabSafe(newPage);
        }

        private void AddTabSafe(BrowserTab b)
        {
            
            
            if (InvokeRequired)
            {
                SetBrowserTabCallBack d = new SetBrowserTabCallBack(AddTabSafe);
                this.Invoke(d, new object[] { b });
            }
            else
            {
                tc.TabPages.Add(b);
            }
        }

        public void UpdateDirs()
        {
            favourites.XmlPath = @"bin/favourites/"+user.Username+"favourite.xml";
            history.XmlPath = @"bin/historys/" + user.Username + "history.xml";

            favourites.Update();
            history.Update();
        }

        public void KeyPressFunc(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.N)
            {
                AddTab(sender, e);
            }

            if (e.KeyChar == (char)Keys.R)
            {
                RemoveTab(sender, e);
            }

            if (e.KeyChar == (char)Keys.A)
            {
                AddFave(sender, e);
            }

            if (e.KeyChar == (char)Keys.H)
            {
                AddHome(sender, e);
            }
        }
    }
   
}
