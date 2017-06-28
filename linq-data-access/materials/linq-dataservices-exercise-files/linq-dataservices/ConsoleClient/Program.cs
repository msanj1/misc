using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleClient.MovieReviews;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryMovies();
            AddMovie();
            UpdateMovie();
            QueryAllMovies();
            InvokeServiceOperation();
        }





        private static void InvokeServiceOperation()
        {
            var ctx = new MovieReviews.MovieReviewEntities(new Uri("http://localhost.:15074/Web/MovieReviewService.svc"));

            var query = ctx.CreateQuery<Movie>("TopRatedMovies")
                           .AddQueryOption("minReviews", 3)
                           .Take(10);
                             
            foreach (var movie in query)
            {
                Console.WriteLine("{0}: {1}", movie.ReleaseDate.Year, movie.Title);
            }
        }










        private static void QueryAllMovies()
        {
            var ctx = new MovieReviews.MovieReviewEntities(new Uri("http://localhost.:15074/Web/MovieReviewService.svc"));

            var query = from m in ctx.Movies
                        select m;

            foreach (var movie in query)
            {
                Console.WriteLine("{0}: {1}", movie.ReleaseDate.Year, movie.Title);
            }
        }

        private static void UpdateMovie()
        {
            var ctx = new MovieReviews.MovieReviewEntities(new Uri("http://localhost.:15074/Web/MovieReviewService.svc"));

            Movie movie = ctx.Movies.Where(m => m.ID == 101).First();
            movie.ReleaseDate = DateTime.Now;
            ctx.UpdateObject(movie);
            ctx.SaveChanges();
        }

        private static void AddMovie()
        {
            var ctx = new MovieReviews.MovieReviewEntities(new Uri("http://localhost.:15074/Web/MovieReviewService.svc"));

            Movie newMovie = new Movie {
                Title = "Demo madness!",
                ReleaseDate = DateTime.Now
            };

            ctx.AddToMovies(newMovie);
            ctx.SaveChanges();
        }

        private static void QueryMovies()
        {
            var ctx = new MovieReviews.MovieReviewEntities(new Uri("http://localhost.:15074/Web/MovieReviewService.svc"));


            var query =
                ctx.Movies.OrderBy(m => m.Title)
                          .Skip(20)
                          .Take(10);

            var movie =
                ctx.Movies.Where(m => m.ID == 100).First();

            Console.WriteLine(movie.Title);

            foreach (var m in query)
            {
                Console.WriteLine(m.Title);
            }
        }
    }
}
