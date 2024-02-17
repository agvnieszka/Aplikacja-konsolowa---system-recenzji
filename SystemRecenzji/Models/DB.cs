using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRecenzji.Models
{
    public class DB
    {
        public List<Movie> dbMovies;
        public List<Review> dbReviews;
        public List<User> dbUsers;

        public DB()
        {
            dbMovies = new Movie().ReadFromCSV<Movie>("db_movies.txt");
            dbReviews = new Review().ReadFromCSV<Review>("db_reviews.txt");
            dbUsers = new User().ReadFromCSV<User>("db_users.txt");
        }

        internal void SaveChanges()
        {
            Movie.WriteToCSV("db_movies.txt", dbMovies);
            Review.WriteToCSV("db_reviews.txt", dbReviews);
            User.WriteToCSV("db_users.txt", dbUsers);
        }
    }
}
