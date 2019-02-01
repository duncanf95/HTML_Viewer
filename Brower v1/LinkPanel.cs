using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Brower_v1
{
    class LinkPanel : Panel
    {
        private Button btnClose = new Button();
        private int maxWidth;
        private Timer timer = new Timer();
        private bool hidden = true;
        private Favourites fave;
        private List<LinkLabel> faveLinks = new List<LinkLabel>();


        public LinkPanel(int panelWidth, Favourites favourite)
        {
            timer.Interval = 1;

            fave = favourite;

            Width = 0;
            maxWidth = panelWidth;
            Height = panelWidth;

            btnClose.Click += new EventHandler((sender, e) => ClosePanel(sender, e));
            timer.Tick += new EventHandler(TimerTick);

            SetLabels();
            DrawLabels();
            

            BackColor = Color.Green; 

            Controls.Add(btnClose);
        }

        private void ClosePanel(object sender, EventArgs e)
        {
            timer.Start();
        }

        public void ShowPanel()
        {
            timer.Start();
        }

        public void TimerTick(object sender, EventArgs e)
        {
            if (hidden)
            {
                ChangeSize(+20);
                
                
            }
            else
            {
                ChangeSize(-20);

            }
            
        }

        private void ChangeSize(int val)
        {
            Width += val;

            if (Width >= maxWidth || Width <= 0)
            {
                timer.Stop();
                hidden = !hidden;
            }
        }

        private void SetLabels()
        {
            int c = 0;
            Console.WriteLine("set");
            foreach (string [] s in fave.SiteData)
            {
                
                faveLinks.Add(new LinkLabel());
                faveLinks[c].Text = s[1];
                faveLinks[c].Width = maxWidth;
                faveLinks[c].Click += new EventHandler((sender, e) => SelectFaveourite(sender, e, c));
                foreach(string i in s)
                {
                    Console.WriteLine(i);
                }
                c++;
            }
        }

        private void DrawLabels()
        {
            int c = 0;
            foreach (LinkLabel l in faveLinks)
            {
                Console.WriteLine("tired");
                l.Location = new Point(0, l.Height * c);
                Controls.Add(l);
                c++;
            }
        }

        private void SelectFaveourite(object sender, EventArgs e, int index)
        {

        }
    }
}
