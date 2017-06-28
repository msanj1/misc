using System;
using System.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using MovieReviewsModel;
using System.Linq.Expressions;
using System.ServiceModel;

[ServiceBehavior(IncludeExceptionDetailInFaults=true)]
public class MovieReviewService : DataService<MovieReviewEntities>
{
    // This method is called only once to initialize service-wide policies.
    public static void InitializeService(IDataServiceConfiguration config)
    {
        config.SetEntitySetAccessRule("*", EntitySetRights.All);
        config.SetServiceOperationAccessRule(
                         "TopRatedMovies", 
                         ServiceOperationRights.All);
    }

    [QueryInterceptor("Movies")]
    public Expression<Func<Movie, bool>> OnQueryMovies()
    {
        return movie => movie.ReleaseDate.Year < 1950;
    }

    [WebGet]
    public IQueryable<Movie> TopRatedMovies(int minReviews)
    {
        var result = from m in CurrentDataSource.Movies
                     where m.Reviews.Count > minReviews
                     orderby m.Reviews.Average(r => r.Rating) ascending
                     select m;
        return result;
    }

}
