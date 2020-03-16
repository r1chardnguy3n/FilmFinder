using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using cloudscribe.Pagination.Models;


namespace FilmFinderWebApp.Controllers
{
    public class MovieController : Controller
    {
        public async Task<IActionResult> Index()
        {
 

            return View();
        }

        public async Task<IActionResult> SearchMovie(string searchString, int? page)
        {
            TMDbClient client = new TMDbClient("02214396be00b0615b4005941508943d");

            var pageNumber = page ?? 1;

            await FetchConfig(client);

            return View(await FetchMovies(client, searchString, pageNumber));
        }

        private static async Task FetchConfig(TMDbClient client)
        {
            FileInfo configJson = new FileInfo("config.json");

            if (configJson.Exists && configJson.LastWriteTimeUtc >= DateTime.UtcNow.AddHours(-1)) 
            {
                string json = System.IO.File.ReadAllText(configJson.FullName, Encoding.UTF8);

                client.SetConfig(JsonConvert.DeserializeObject<TMDbConfig>(json));
                
            }
            else
            {
                var config = await client.GetConfigAsync();

                string json = JsonConvert.SerializeObject(config);
                System.IO.File.WriteAllText(configJson.FullName, json, Encoding.UTF8);
            }
        }

        private static void ProcessImages(TMDbClient client, IEnumerable<ImageData> images, IEnumerable<string> sizes)
        {
            // Displays basic information about each image, as well as all the possible adresses for it.
            // All images should be available in all the sizes provided by the configuration.

            foreach (ImageData imageData in images)
            {
                Console.WriteLine(imageData.FilePath);
                Console.WriteLine("\t " + imageData.Width + "x" + imageData.Height);

                // Calculate the images path
                // There are multiple resizing available for each image, directly from TMDb.
                // There's always the "original" size if you're in doubt which to choose.
                foreach (string size in sizes)
                {
                    Uri imageUri = client.GetImageUrl(size, imageData.FilePath);
                    Console.WriteLine("\t -> " + imageUri);
                }

                Console.WriteLine();
            }
        }



        private static async Task<PagedResult<Movie>> FetchMovies(TMDbClient client, string query, int page)
        {      

            
            
            SearchContainer<SearchMovie> results = await client.SearchMovieAsync(query, page);

            var getMovies = (from result in results.Results
                             select new
                             {
                                 Title = result.Title,
                                 PosterPath = result.PosterPath,
                                 Id = result.Id
                              
                               
                             }).ToList().Select(p => new Movie()
                             {
                                 Title = p.Title,
                                 PosterPath = "https://image.tmdb.org/t/p/w200/"+ p.PosterPath,
                                 Id = p.Id
                             });

            var pagedMovie = new PagedResult<Movie>
            {
                Data = getMovies.ToList(),
                TotalItems = results.TotalResults,
                PageNumber = page,
                PageSize = 20
            };

            

            return pagedMovie;
        }


    }
}