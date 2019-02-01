using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized;

namespace Brower_v1
{
    class URL_List
    {
        protected List<Dictionary<string, string>> site_data =
            new List<Dictionary<string, string>>();

        protected void add_item(string url, string title)
        {
            site_data.Add(new Dictionary<string, string>());
            site_data[site_data.Count - 1].Add("url", url);
            site_data[site_data.Count - 1].Add("title", title);
        }

        private void remove_item(int index)
        {
            site_data.RemoveAt(index);
        }
        
    }

    
}
