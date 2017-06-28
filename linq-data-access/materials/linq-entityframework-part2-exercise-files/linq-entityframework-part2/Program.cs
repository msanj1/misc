using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Transactions;
using System.ComponentModel;

namespace linq_entityframework_part2
{

    class Program
    {
        static void Main(string[] args)
        {
            //IdentityMap();
            //Isolation();
            //RelatedEntities();
            //Concurrency();
            //Transactions();
            //AddRelated();
            DetachAndAttach();
        }

        private static void DetachAndAttach()
        {
            Movie movie;

            using (var ctx = new MovieReviewEntities())
            {
                movie = ctx.Movies.First();
                ctx.Detach(movie);
            }
            
            var originalMovieValues = new Movie
            {
                ID = movie.ID,
                EntityKey = movie.EntityKey,                 
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate 
            };
            
            movie.ReleaseDate = DateTime.Now.AddYears(-1);

            using (var ctx = new MovieReviewEntities())
            {
                ctx.Attach(originalMovieValues);
                ctx.ApplyPropertyChanges("Movies", movie);
                ctx.SaveChanges();
            }
            
        }







        private static void AddRelated()
        {
            using (var ctx = new MovieReviewEntities())
            {
                var movie = new Movie
                {
                    Title = ".NET 4.0",
                    ReleaseDate = new DateTime(2010, 1, 1)
                };

                var review = new Review
                {
                    Rating = 10,
                    Summary = "Great!",
                    Body = "We can't wait for EF 2.0!",
                    Reviewer = "Scott"
                };

                ctx.AddToMovies(movie);
                movie.Reviews.Add(review);

                ctx.SaveChanges();

            }

        }

        private static void Transactions()
        {
            using (var txn = new TransactionScope())
            using (var ctx = new MovieReviewEntities())
            {
                var movie = ctx.Movies.First();

                // do work ...
                
                ctx.SaveChanges();
            }
        }

        private static void Concurrency()
        {

            var ctx1 = new MovieReviewEntities();
            var ctx2 = new MovieReviewEntities();

            var m1 = ctx1.Movies.Where(movie => movie.ID == 100).First();
            var m2 = ctx2.Movies.Where(movie => movie.ID == 100).First();

            try
            {

                m2.ReleaseDate = DateTime.Now;
                ctx2.SaveChanges();

                m1.ReleaseDate = DateTime.Now.AddYears(1);
                ctx1.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                ctx1.Refresh(System.Data.Objects.RefreshMode.StoreWins, m1);
                m1.ReleaseDate = DateTime.Now.AddYears(1);
                ctx1.SaveChanges();
            }
        }





        private static void RelatedEntities()
        {
            using (var ctx = new MovieReviewEntities())
            {
                var m1 = ctx.Movies.Include("Reviews")
                            .Where(m => m.Reviews.Count > 1).First();
                var m2 = ctx.Movies.Include("Reviews")
                            .Where(m => m.ID != m1.ID)
                            .First();

                var reviews = m1.Reviews.ToList();
                foreach (var review in reviews)
                {
                    review.Movie = m2;
                }

                ctx.SaveChanges();
            }
        }

 





        private static void Isolation()
        {            
            var ctx1 = new MovieReviewEntities();
            var ctx2 = new MovieReviewEntities();

            var m1 = ctx1.Movies.Where(movie => movie.ID == 100).First();
            var m2 = ctx2.Movies.Where(movie => movie.ID == 100).First();  
                      
            m2.ReleaseDate = DateTime.Now;
            ctx2.SaveChanges();

            m1 = ctx1.Movies.Where(movie => movie.ID == 100).First();
            Debug.Assert(m1.ReleaseDate != m2.ReleaseDate);
        }











        private static void IdentityMap()
        {
            Movie m1;
            Movie m2;

            using (var ctx = new MovieReviewEntities())
            {
                ctx.ObjectStateManager.ObjectStateManagerChanged += new CollectionChangeEventHandler(ObjectStateManager_ObjectStateManagerChanged);
                m1 = ctx.Movies.Where(movie => movie.ID == 100).First();
                m2 = ctx.Movies.Where(movie => movie.ID == 100).First();
                Debug.Assert(Object.ReferenceEquals(m1, m2));
            }
        }

        static void ObjectStateManager_ObjectStateManagerChanged(object sender, 
                        CollectionChangeEventArgs e)
        {
            
        }


    }
}
