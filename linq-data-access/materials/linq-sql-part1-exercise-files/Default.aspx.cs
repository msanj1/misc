using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Configuration;
using System.Data.Linq.Mapping;
using Poco;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MovieReviewsDataContext ctx = new MovieReviewsDataContext();           

            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<Movie>(m => m.Reviews);
            ctx.LoadOptions = loadOptions;

            var movies =

                ctx.GetHighestRankedMovies(new DateTime(2002, 1, 1)).Take(15);

                 //(from m in ctx.Movies
                 // orderby m.ReleaseDate ascending
                 // where m.ReleaseDate.Year > 2000 &&
                 //       m.Reviews.Count > 3
                 // select m).Skip(15).Take(15);


            ctx.Log = Response.Output;

            _grid.DataSource = movies;
            _grid.DataBind();
        }
    }
}
