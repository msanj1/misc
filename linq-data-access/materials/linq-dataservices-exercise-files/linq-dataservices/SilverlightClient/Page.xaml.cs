using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Services.Client;
using SilverlightClient.MovieReviews;

namespace SilverlightClient
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var ctx = new MovieReviews.MovieReviewEntities(
                        new Uri("MovieReviewService.svc", UriKind.Relative));

            DataServiceQuery<Movie> query = ctx.Movies.OrderBy(m => m.Title)
                                  .Take(100) as DataServiceQuery<Movie>;
            query.BeginExecute((result) => 
              _grid.ItemsSource = query.EndExecute(result).ToList(), null
            );
        }       
    }
}
