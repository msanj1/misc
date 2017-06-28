using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data.Linq;

namespace linq_sql_part2
{
    class Program
    {
        static void Main(string[] args)
        {
            //IdentityMapDemo();
            UpdateEntity();

            //MoveReviews();
            //AddAReview();

            //AddAReviewWithoutAMovie();

            //AddAMovieAndReview();

                
        }















        private static void AddAReviewWithoutAMovie()
        {
            using (var context = new MoviesDataContext())
            {              
                Review review = new Review()
                {
                    Body = "I want to see it over and over!",
                    MovieID = 100,
                    Rating = 10,
                    Reviewer = "Scott",
                    Summary = "Great!"
                };
                context.Reviews.InsertOnSubmit(review);
                context.SubmitChanges();
            }
            
        }

        private static void AddAMovieAndReview()
        {
            using (var context = new MoviesDataContext())
            {
                Movie movie = new Movie
                {
                    Title = "Hairspray",
                    ReleaseDate = new DateTime(2007, 6, 1)
                };

                Review myReview = new Review
                {
                    Rating = 10,
                    Reviewer = "scott",
                    Body = "I want to see it again and again!",
                    Summary = "Fantastic!"
                };

                movie.Reviews.Add(myReview);
                context.Movies.InsertOnSubmit(movie);
                context.SubmitChanges();
            }
        }

        private static void MoveReviews()
        {
            using (var context = new MoviesDataContext())
            {
                var firstMovie = context.Movies
                                         .Where(m => m.Reviews.Count > 0)
                                         .First();
                var secondMovie = context.Movies 
                                         .Where(m => m.ID != firstMovie.ID)
                                         .First();

                secondMovie.Reviews.AddRange(firstMovie.Reviews);
                context.SubmitChanges();
            }
        }


        private static void AddAReview()
        {
            using (var context = new MoviesDataContext())
            {
                var movie = context.Movies.Where(m => m.ID == 100).First();

                Review review = new Review()
                {
                    Body = "I want to see it over and over!",
                    Movie = movie,
                    Rating = 10,
                    Reviewer = "Scott",
                    Summary="Great!"
                };

                context.SubmitChanges();
            }
        }





        private static void UpdateEntity()
        {
            using (var context = new MoviesDataContext())
            {
                var movies = context.Movies.Where(m => m.ID > 100 && m.ID < 105);
                foreach (var movie in movies)
                {
                    movie.ReleaseDate = movie.ReleaseDate.AddDays(1);
                }
                context.SubmitChanges();
            }
        }








        private static void IdentityMapDemo()
        {
            using(var dc1 = new MoviesDataContext())
            {
                Movie m1 = dc1.Movies.Where(movie => movie.ID == 100).First();
                Movie m2 = dc1.Movies.Where(movie => movie.ID == 100).First();
                Debug.Assert(Object.ReferenceEquals(m1, m2) == true);
            }

            using (var dc1 = new MoviesDataContext())
            using (var dc2 = new MoviesDataContext())
            {
                Movie m1 = dc1.Movies.Where(movie => movie.ID == 100).First();
                Movie m2 = dc2.Movies.Where(movie => movie.ID == 100).First();
                Debug.Assert(Object.ReferenceEquals(m1, m2) == false);

            }

            using (var dc1 = new MoviesDataContext())
            using (var dc2 = new MoviesDataContext())
            {
                dc1.Log = Console.Out;
                dc2.Log = Console.Out;

                Movie m1 = dc1.Movies.Where(movie => movie.ID == 100).First();
                Movie m2 = dc2.Movies.Where(movie => movie.ID == 100).First();

                m1.Title = "The LINQ Story";
                dc1.SubmitChanges();

                m2 = dc2.Movies.Where(movie => movie.ID == 100).First();
                //Debug.Assert(m2.Title != "The LINQ Story");

                dc2.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, m2);               
                //Debug.Assert(m2.Title == "The LINQ to SQL Story");

            }
        }
    }
}
