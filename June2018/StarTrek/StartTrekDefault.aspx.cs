using June2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace June2018.StarTrek
{
    public partial class StartTrekDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate TV ListBox
                PopulateTVListBox();

                // create temp session variable for UserID
                Session["UserID"] = 1;
                
            }
        }

        private void PopulateTVListBox()
        {
            StarTrekProductionModel dbContext = new StarTrekProductionModel();

            List<StarTrekSeriesName> listSeries = dbContext.StarTrekSeriesNames.ToList();

            lbSeries.DataTextField = "SeriesName";
            lbSeries.DataValueField = "ID";

            lbSeries.DataSource = listSeries;
            lbSeries.DataBind();


        }

        //

        protected void btnWatched_Click(object sender, EventArgs e)
        {

        }

        protected void btnShowTV_Click(object sender, EventArgs e)
        {
            MultiViewMain.SetActiveView(viewTV);
        }

        protected void btnShowMovies_Click(object sender, EventArgs e)
        {
            MultiViewMain.SetActiveView(viewFilms);
        }

        protected void lbSeries_TextChanged(object sender, EventArgs e)
        {
            
          
            

        }

        protected void lbSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate Series based on item
            PopulateSeriesInfoGridView(lbSeries.SelectedValue);
        }

        private void PopulateSeriesInfoGridView(string selectedValue)
        {
            int seriesID = Convert.ToInt32(selectedValue);
            int userID = (int)Session["UserID"];

            StarTrekUserModel dbContext = new StarTrekUserModel();

            List<StarTrekUserData> listSeriesInfo = dbContext.StarTrekUserDatas.Where(x=>x.UserID == 1 && x.StarTrekProduction.SeriesID== seriesID).ToList();


            gvSeriesInfo.DataSource = listSeriesInfo;
            gvSeriesInfo.DataBind();
        }


    }
}