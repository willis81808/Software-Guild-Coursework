using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using EntityFrameworkIntro.Models;
using Models;

namespace EntityFrameworkIntro.Controllers
{
    public class Movie
    {
        public int MovieId { get; set; }
        public int? RatingId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index(int id = 1)
        {
            /*
            Movie result;
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["MovieCatalog"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@MovieId", id);

                result = conn.Query<Movie>(
                    "MovieSelectById",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            ViewBag.Movie = result;
            */

            var repository = new MovieCatalogEntities();

            ViewBag.Movie = (from movie in repository.Movies
                            where movie.MovieId == id
                            select new MovieListView
                            {
                                MovieId = movie.MovieId,
                                Title = movie.Title,
                                GenreType = movie.Genre.GenreType,
                                RatingName = movie.Rating.RatingName
                            }).FirstOrDefault();

            return View();
        }

        public ActionResult AddMovie()
        {
            var repository = new MovieCatalogEntities();
            AddMovieViewModel model = new AddMovieViewModel();

            model.Genres = from g in repository.Genres
                           select new SelectListItem
                           {
                               Text = g.GenreType,
                               Value = g.GenreId.ToString()
                           };

            model.Ratings = from r in repository.Ratings
                            select new SelectListItem
                            {
                                Text = r.RatingName,
                                Value = r.RatingId.ToString()
                            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddMovie(AddMovieViewModel model)
        {
            var movie = new EntityFrameworkIntro.Models.Movie();
            movie.Title = model.Title;
            movie.GenreId = model.GenreId;
            movie.RatingId = model.RatingsId;

            var repository = new MovieCatalogEntities();
            repository.Movies.Add(movie);
            repository.SaveChanges();

            return RedirectToAction("Index", new { id = movie.MovieId });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}