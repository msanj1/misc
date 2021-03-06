﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("MovieReviewsModel", "fk_reviews_movies", "movies", global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(linq_entityframework_part2.Movie), "reviews", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(linq_entityframework_part2.Review))]

// Original file name:
// Generation date: 12/4/2008 4:42:58 PM
namespace linq_entityframework_part2
{
    
    /// <summary>
    /// There are no comments for MovieReviewEntities in the schema.
    /// </summary>
    public partial class MovieReviewEntities : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new MovieReviewEntities object using the connection string found in the 'MovieReviewEntities' section of the application configuration file.
        /// </summary>
        public MovieReviewEntities() : 
                base("name=MovieReviewEntities", "MovieReviewEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new MovieReviewEntities object.
        /// </summary>
        public MovieReviewEntities(string connectionString) : 
                base(connectionString, "MovieReviewEntities")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new MovieReviewEntities object.
        /// </summary>
        public MovieReviewEntities(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "MovieReviewEntities")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for Movies in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Movie> Movies
        {
            get
            {
                if ((this._Movies == null))
                {
                    this._Movies = base.CreateQuery<Movie>("[Movies]");
                }
                return this._Movies;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Movie> _Movies;
        /// <summary>
        /// There are no comments for ReviewSet in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<Review> ReviewSet
        {
            get
            {
                if ((this._ReviewSet == null))
                {
                    this._ReviewSet = base.CreateQuery<Review>("[ReviewSet]");
                }
                return this._ReviewSet;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Review> _ReviewSet;
        /// <summary>
        /// There are no comments for Movies in the schema.
        /// </summary>
        public void AddToMovies(Movie movie)
        {
            base.AddObject("Movies", movie);
        }
        /// <summary>
        /// There are no comments for ReviewSet in the schema.
        /// </summary>
        public void AddToReviewSet(Review review)
        {
            base.AddObject("ReviewSet", review);
        }
    }
    /// <summary>
    /// There are no comments for MovieReviewsModel.Movie in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ID
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="MovieReviewsModel", Name="Movie")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Movie : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Movie object.
        /// </summary>
        /// <param name="id">Initial value of ID.</param>
        /// <param name="title">Initial value of Title.</param>
        /// <param name="releaseDate">Initial value of ReleaseDate.</param>
        public static Movie CreateMovie(int id, string title, global::System.DateTime releaseDate)
        {
            Movie movie = new Movie();
            movie.ID = id;
            movie.Title = title;
            movie.ReleaseDate = releaseDate;
            return movie;
        }
        /// <summary>
        /// There are no comments for Property ID in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this.ReportPropertyChanging("ID");
                this._ID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ID");
                this.OnIDChanged();
            }
        }
        private int _ID;
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        /// <summary>
        /// There are no comments for Property Title in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this.OnTitleChanging(value);
                this.ReportPropertyChanging("Title");
                this._Title = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Title");
                this.OnTitleChanged();
            }
        }
        private string _Title;
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
        /// <summary>
        /// There are no comments for Property ReleaseDate in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.DateTime ReleaseDate
        {
            get
            {
                return this._ReleaseDate;
            }
            set
            {
                this.OnReleaseDateChanging(value);
                this.ReportPropertyChanging("ReleaseDate");
                this._ReleaseDate = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ReleaseDate");
                this.OnReleaseDateChanged();
            }
        }
        private global::System.DateTime _ReleaseDate;
        partial void OnReleaseDateChanging(global::System.DateTime value);
        partial void OnReleaseDateChanged();
        /// <summary>
        /// There are no comments for Reviews in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("MovieReviewsModel", "fk_reviews_movies", "reviews")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityCollection<Review> Reviews
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Review>("MovieReviewsModel.fk_reviews_movies", "reviews");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedCollection<Review>("MovieReviewsModel.fk_reviews_movies", "reviews", value);
                }
            }
        }
    }
    /// <summary>
    /// There are no comments for MovieReviewsModel.Review in the schema.
    /// </summary>
    /// <KeyProperties>
    /// ID
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="MovieReviewsModel", Name="Review")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class Review : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new Review object.
        /// </summary>
        /// <param name="id">Initial value of ID.</param>
        /// <param name="summary">Initial value of Summary.</param>
        /// <param name="rating">Initial value of Rating.</param>
        /// <param name="body">Initial value of Body.</param>
        public static Review CreateReview(int id, string summary, int rating, string body)
        {
            Review review = new Review();
            review.ID = id;
            review.Summary = summary;
            review.Rating = rating;
            review.Body = body;
            return review;
        }
        /// <summary>
        /// There are no comments for Property ID in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this.ReportPropertyChanging("ID");
                this._ID = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ID");
                this.OnIDChanged();
            }
        }
        private int _ID;
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        /// <summary>
        /// There are no comments for Property Summary in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Summary
        {
            get
            {
                return this._Summary;
            }
            set
            {
                this.OnSummaryChanging(value);
                this.ReportPropertyChanging("Summary");
                this._Summary = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Summary");
                this.OnSummaryChanged();
            }
        }
        private string _Summary;
        partial void OnSummaryChanging(string value);
        partial void OnSummaryChanged();
        /// <summary>
        /// There are no comments for Property Rating in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public int Rating
        {
            get
            {
                return this._Rating;
            }
            set
            {
                this.OnRatingChanging(value);
                this.ReportPropertyChanging("Rating");
                this._Rating = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Rating");
                this.OnRatingChanged();
            }
        }
        private int _Rating;
        partial void OnRatingChanging(int value);
        partial void OnRatingChanged();
        /// <summary>
        /// There are no comments for Property Body in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Body
        {
            get
            {
                return this._Body;
            }
            set
            {
                this.OnBodyChanging(value);
                this.ReportPropertyChanging("Body");
                this._Body = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Body");
                this.OnBodyChanged();
            }
        }
        private string _Body;
        partial void OnBodyChanging(string value);
        partial void OnBodyChanged();
        /// <summary>
        /// There are no comments for Property Reviewer in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string Reviewer
        {
            get
            {
                return this._Reviewer;
            }
            set
            {
                this.OnReviewerChanging(value);
                this.ReportPropertyChanging("Reviewer");
                this._Reviewer = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Reviewer");
                this.OnReviewerChanged();
            }
        }
        private string _Reviewer;
        partial void OnReviewerChanging(string value);
        partial void OnReviewerChanged();
        /// <summary>
        /// There are no comments for Movie in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("MovieReviewsModel", "fk_reviews_movies", "movies")]
        [global::System.Xml.Serialization.XmlIgnoreAttribute()]
        [global::System.Xml.Serialization.SoapIgnoreAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public Movie Movie
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Movie>("MovieReviewsModel.fk_reviews_movies", "movies").Value;
            }
            set
            {
                ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Movie>("MovieReviewsModel.fk_reviews_movies", "movies").Value = value;
            }
        }
        /// <summary>
        /// There are no comments for Movie in the schema.
        /// </summary>
        [global::System.ComponentModel.BrowsableAttribute(false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Data.Objects.DataClasses.EntityReference<Movie> MovieReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Movie>("MovieReviewsModel.fk_reviews_movies", "movies");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Movie>("MovieReviewsModel.fk_reviews_movies", "movies", value);
                }
            }
        }
    }
}
