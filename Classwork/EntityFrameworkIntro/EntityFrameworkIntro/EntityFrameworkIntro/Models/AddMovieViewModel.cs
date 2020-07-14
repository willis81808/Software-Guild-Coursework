using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkIntro.Models
{
    public class AddMovieViewModel
    {
        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Ratings { get; set; }
        public int GenreId { get; set; }
        public int RatingsId { get; set; }
        public string Title { get; set; }
    }
}