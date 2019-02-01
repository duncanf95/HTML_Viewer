using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized;

namespace Brower_v1
{
    partial class URL_List
    {
        protected List<string[]> site_data = new List<string[]>();

        public List<string[]> SiteData
        {
            get { return site_data; }
        }

        /// <summary>
        /// adds url and title to list of urls
        /// </summary>
        /// <param name="url">string with url</param>
        /// <param name="title">string with title</param>
        protected void add_item(string url, string title)
        {
            site_data.Add(new string[3]);
            site_data[site_data.Count - 1][0] = url;
            site_data[site_data.Count - 1][1] = title;
            site_data[site_data.Count - 1][2] = (site_data.Count-1).ToString();

        }
        /// <summary>
        /// removes item from list
        /// </summary>
        /// <param name="index"></param>
        protected void remove_item(int index)
        {
            site_data.RemoveAt(index);
        }
        
    }

    
}
