using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Brower_v1
{
    class FavouritesPanel : Panel
    {
        private Button btnClose = new Button();
        private int maxWidth;
        public int MaxWidth
        {
            get { return maxWidth; }
        }
        private Timer timer = new Timer();
        private bool hidden = true;
        private Favourites fave;
        private List<LinkLabel> faveLinks = new List<LinkLabel>();
        private List<Button> btnRemove = new List<Button>();
        private List<Button> btnEdit = new List<Button>();
        private Button btnConfirm = new Button();
        private BrowserTab curTab;
        private TextBox input;
        private string urlEdit;



        public FavouritesPanel(int panelWidth, int panelHeight, Favourites favourite, BrowserTab Tab)
        {
            timer.Interval = 1;
            AutoScroll = true;
            VerticalScroll.Visible = true;
            VerticalScroll.Enabled = true;
            

            fave = favourite;
            curTab = Tab;

            Width = 0;
            maxWidth = panelWidth;
            Height = panelHeight;

            btnClose.Click += new EventHandler((sender, e) => TogglePanel(sender, e));
            btnConfirm.Click += new EventHandler((sender, e) => ConfirmFavourite(sender, e));
            timer.Tick += new EventHandler(TimerTick);
            

            SetLinks();
            DrawLabels();
            

            BackColor = Color.LightGray;

            btnClose.Text = "Close";

            Controls.Add(btnClose);


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
            foreach (string [] s in fave.SiteData)
            {
                
                faveLinks.Add(new LinkLabel());
                faveLinks[c].Text = s[1];
                faveLinks[c].Width = (maxWidth/4)*3;
                faveLinks[c].Click += new EventHandler((sender, e) => SelectFavourite(sender, e,  s[0]));
            

                btnRemove.Add(new Button());
                btnRemove[c].Text = "x";
                btnRemove[c].Height = faveLinks[c].Height;
                btnRemove[c].Width = faveLinks[c].Height;
                btnRemove[c].Click += new EventHandler((sender, e) => RemoveFavourite(sender, e, s[0]));

                btnEdit.Add(new Button());
                btnEdit[c].Text = "Edit";
                btnEdit[c].Height = faveLinks[c].Height;
                btnEdit[c].Width = faveLinks[c].Height * 2;
                btnEdit[c].Click += new EventHandler((sender, e) => EditFavourite(sender, e, s[2]));

                
                c++;
            }
        }

        /// <summary>
        /// adds labels and their corresponding buttons to the correct location
        /// </summary>
        private void DrawLabels()
        {
            int c = 0;
            foreach (LinkLabel l in faveLinks)
            {
                l.Location = new Point(0, (l.Height * c) + btnClose.Height);
                Controls.Add(l);
                c++;
            }

            c = 0;

            foreach (Button b in btnRemove)
            {

                btnEdit[c].Location = new Point(maxWidth - btnEdit[c].Width, (b.Height * c) + btnClose.Height);
                Controls.Add(btnEdit[c]);

                b.Location = new Point(maxWidth - b.Width - btnEdit[c].Width, (b.Height * c)+btnClose.Height);
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
        /// removes favourite from fave object
        /// makes sure data is not being edited
        /// updates panel to reflect new list of links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="url"></param>
        private void RemoveFavourite(object sender, EventArgs e, string url)
        {
            fave.RemoveFromFavourites(url);
            curTab.WaitForUpdate();
            PanelUpdate();
        }
        /// <summary>
        /// parses input index from string to int
        /// sets attributes of edit text box and confirm button
        /// gets url required to be edited using index
        /// hides current controlx
        /// adds edit textbox and
        /// </summary>
        /// <param name="s"></param>
        /// <param name="err"></param>
        /// <param name="inputIndex">index of the url within the list</param>
        private void EditFavourite(object s, EventArgs err, string inputIndex)
        {
            int index;
            Int32.TryParse(inputIndex, out index);
            input = new TextBox();
            input.Width = (maxWidth / 4) * 3;
            btnConfirm.Width = (maxWidth / 4);

            input.Location = new Point(0, (input.Height * index) + btnClose.Height);
            btnConfirm.Location = new Point(maxWidth - btnConfirm.Width, (faveLinks[index].Height * index) + btnClose.Height);

            urlEdit = fave.SiteData[index][0];

            input.BringToFront();
            btnConfirm.BringToFront();

            btnEdit[index].Hide();
            btnRemove[index].Hide();
            faveLinks[index].Hide();
            btnConfirm.Text = "confirm";

            Controls.Add(input);
            Controls.Add(btnConfirm);



        }
        /// <summary>
        /// calls edit fave function using the urlEdit variable
        /// updates panel to reflect new fave list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmFavourite(object sender, EventArgs e)
        {

            fave.EditFavourite(urlEdit, input.Text);
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
            foreach(LinkLabel l in faveLinks)
            {
                l.Show();
                Controls.Remove(l);
            }
            int c = 0;
            foreach(Button b in btnRemove)
            {
                b.Show();
                Controls.Remove(b);
                btnEdit[c].Show();
                Controls.Remove(btnEdit[c]);
                c++;
            }
            faveLinks = new List<LinkLabel>();
            btnRemove = new List<Button>();
            Controls.Remove(input);
            Controls.Remove(btnConfirm);
            SetLinks();
            DrawLabels();
        }

       
    }
}
