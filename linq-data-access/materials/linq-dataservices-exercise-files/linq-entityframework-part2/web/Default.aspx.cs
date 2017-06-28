using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieReviewsModel;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (var ctx = new MovieReviewEntities())
            {
                var movie = ctx.Movies.First();
                _titleInput.Text = movie.Title;
                _releaseDateInput.Text = movie.ReleaseDate.ToString();
                ViewState["originalMovie"] = movie;
            }
        }
    }
    protected void _post_Click(object sender, EventArgs e)
    {
        var originalMovie = ViewState["originalMovie"] as Movie;
        var updateMovie = new Movie()
        {
            ID = originalMovie.ID,
            Title = _titleInput.Text,
            ReleaseDate = DateTime.Parse(_releaseDateInput.Text)
        };

        using (var ctx = new MovieReviewEntities())
        {
            ctx.Attach(originalMovie);
            ctx.ApplyPropertyChanges("Movies", updateMovie);
            ctx.SaveChanges();
        }
    }
}
