using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linq_entityframework_part1
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleQuery();
            QueryWithRelationships();
            CallStoredProcedure();
            RelatedEntites();

            int id = InsertMovie();
            UpdateMovie(id);
            DeleteMovie(id);            
        }

        private static void UpdateMovie(int id)
        {
            using (var ctx = new MovieReviewEntities())
            {
                var movie = ctx.Movies.Where(m => m.ID == id).First();

                movie.ReleaseDate = DateTime.Now.AddYears(1);

                ctx.SaveChanges();
            }
        }

        private static void DeleteMovie(int id)
        {
            using (var ctx = new MovieReviewEntities())
            {
                var movie = ctx.Movies.Where(m => m.ID == id).First();
                ctx.DeleteObject(movie);
                ctx.SaveChanges();
            }
        }

        private static int InsertMovie()
        {
            using (var ctx = new MovieReviewEntities())
            {
                Movie newMovie = new Movie
                {
                    Title = "The LINQ Love Story",
                    ReleaseDate = DateTime.Now
                };

                ctx.AddToMovies(newMovie);
                ctx.SaveChanges();
                return newMovie.ID;
            }
        }

        private static void RelatedEntites()
        {
            var ctx = new MovieReviewEntities();

            //var query = ctx.Movies.Include("Reviews").Take(10);
            var query = ctx.Movies.Take(10);

            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
                movie.Reviews.Load();
                foreach (var review in movie.Reviews)
                {
                    Console.WriteLine("\t" + review.Rating );
                }
            }


        }

        private static void QueryWithRelationships()
        {
            var ctx = new MovieReviewEntities();

            var query =
                from m in ctx.Movies
                where m.Reviews.Count > 0
                orderby m.Reviews.Average(r => r.Rating) descending
                select m;



            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }            
        }

        private static void CallStoredProcedure()
        {
            var ctx = new MovieReviewEntities();

            var query = ctx.GetHishestRankedMovies(new DateTime(2000, 1, 1));

            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }    
        }

        private static void SimpleQuery()
        {
            var ctx = new MovieReviewEntities();

            var query =
                 from m in ctx.Movies
                 where m.ReleaseDate > new DateTime(2000,1,1)
                 select m;

            foreach (var movie in query.Take(15))
            {
                Console.WriteLine(movie.Title);
            }
        }        
    }
}