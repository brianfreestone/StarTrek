using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace June2018.StarTrek
{
    public partial class DisplayImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var src = Page.Request["source"].ToString();
            src = "https://image.tmdb.org/t/p/w342" + src;
            img.ImageUrl = src;
        }
    }
}