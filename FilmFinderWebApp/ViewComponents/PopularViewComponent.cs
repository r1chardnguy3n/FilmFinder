using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace FilmFinderWebApp.ViewComponents
{
    public class PopularViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TMDbClient client = new TMDbClient("02214396be00b0615b4005941508943d");

            SearchContainer<SearchMovie> popularMovies = await client.GetMoviePopularListAsync();

            var getPopularMovies = (from result in popularMovies.Results.Take(15)
                             select new
                             {
                                 Title = result.Title,
                                 PosterPath = result.PosterPath,
                                 Id = result.Id

                             }).ToList().Select(p => new Movie()
                             {
                                 Title = p.Title,
                                 PosterPath = "https://image.tmdb.org/t/p/w200/" + p.PosterPath,
                                 Id = p.Id
                             });

            return View(getPopularMovies);
        }
    }
}
