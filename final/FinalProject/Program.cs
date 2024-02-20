using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace YouTubeVideoInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select language / Selecciona idioma:");
            Console.WriteLine("1. Español");
            Console.WriteLine("2. English");

            int languageOption;
            if (!int.TryParse(Console.ReadLine(), out languageOption))
            {
                Console.WriteLine("Invalid option. Please select a valid option.");
                return;
            }

            switch (languageOption)
            {
                case 1:
                    MenuPrincipal("es");
                    break;
                case 2:
                    MenuPrincipal("en");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }

        static void MenuPrincipal(string language)
        {
           // YouTube API key (you must replace it with your own key)
            string apiKey = "AIzaSyB9NSovIeETU8Iv3YI60OLpKroHahT3yf4";

           // Create the YouTube service
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "YouTube Video Info"
            });

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine(language == "es" ? "Selecciona una opción:" : "Select an option:");
                Console.WriteLine("1. " + (language == "es" ? "Información básica del video" : "Basic video information"));
                Console.WriteLine("2. " + (language == "es" ? "Comentarios del video" : "Video comments"));
                Console.WriteLine("3. " + (language == "es" ? "Estadísticas del video" : "Video statistics"));
                Console.WriteLine("4. " + (language == "es" ? "Salir" : "Exit"));

                int opcion;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine(language == "es" ? "Opción inválida. Por favor, selecciona una opción válida." : "Invalid option. Please select a valid option.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        ObtenerInformacionBasica(youtubeService, language);
                        break;
                    case 2:
                        ObtenerComentarios(youtubeService, language);
                        break;
                    case 3:
                        ObtenerEstadisticas(youtubeService, language);
                        break;
                    case 4:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine(language == "es" ? "Opción inválida. Por favor, selecciona una opción válida." : "Invalid option. Please select a valid option.");
                        break;
                }
            }
        }

        static void ObtenerInformacionBasica(YouTubeService youtubeService, string language)
        {
            string videoId = ObtenerIdVideoDesdeEnlace(language);

            if (string.IsNullOrEmpty(videoId))
            {
                Console.WriteLine(language == "es" ? "No se pudo obtener el ID del video. Verifica el enlace e inténtalo de nuevo." : "Could not get the video ID. Please check the link and try again.");
                return;
            }

            try
            {
                // Create the YouTube service
                var videoRequest = youtubeService.Videos.List("snippet,contentDetails,statistics");
                videoRequest.Id = videoId;
                var videoResponse = videoRequest.Execute();

                // Check if the video was found
                if (videoResponse.Items.Count > 0)
                {
                    var video = videoResponse.Items[0];

                    // Display basic information of the video
                    Console.WriteLine(language == "es" ? "Información del Video:" : "Video Information:");
                    Console.WriteLine(language == "es" ? "Título: " + video.Snippet.Title : "Title: " + video.Snippet.Title);
                    Console.WriteLine(language == "es" ? "Canal: " + video.Snippet.ChannelTitle : "Channel: " + video.Snippet.ChannelTitle);
                    var duration = XmlConvert.ToTimeSpan(video.ContentDetails.Duration);
                    Console.WriteLine(language == "es" ? "Duración: " + duration.ToString(@"hh\:mm\:ss") : "Duration: " + duration.ToString(@"hh\:mm\:ss"));

                    // Shows video statistics
                    Console.WriteLine(language == "es" ? "Estadísticas del Video:" : "Video Statistics:");
                    Console.WriteLine(language == "es" ? "Vistas: " + video.Statistics.ViewCount : "Views: " + video.Statistics.ViewCount);
                    Console.WriteLine(language == "es" ? "Me gusta: " + video.Statistics.LikeCount : "Likes: " + video.Statistics.LikeCount);
                    Console.WriteLine(language == "es" ? "No me gusta: " + video.Statistics.DislikeCount : "Dislikes: " + video.Statistics.DislikeCount);
                    Console.WriteLine(language == "es" ? "Comentarios: " + video.Statistics.CommentCount : "Comments: " + video.Statistics.CommentCount);
                }
                else
                {
                    Console.WriteLine(language == "es" ? "No se encontró ningún video con el ID proporcionado." : "No video found with the provided ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(language == "es" ? "Error al obtener la información del video: " + ex.Message : "Error getting video information: " + ex.Message);
            }
        }

        static void ObtenerComentarios(YouTubeService youtubeService, string language)
        {
            string videoId = ObtenerIdVideoDesdeEnlace(language);

            if (string.IsNullOrEmpty(videoId))
            {
                Console.WriteLine(language == "es" ? "No se pudo obtener el ID del video. Verifica el enlace e inténtalo de nuevo." : "Could not get the video ID. Please check the link and try again.");
                return;
            }

            try
            {
                // Get the video comments
                var commentThreadsRequest = youtubeService.CommentThreads.List("snippet");
                commentThreadsRequest.VideoId = videoId;
                commentThreadsRequest.MaxResults = 10; // You can adjust this value as needed
                var commentThreadsResponse = commentThreadsRequest.Execute();

                // Display comments in the console
                Console.WriteLine(language == "es" ? "Comentarios del Video:" : "Video Comments:");
                foreach (var commentThread in commentThreadsResponse.Items)
                {
                    var comment = commentThread.Snippet.TopLevelComment.Snippet;
                    Console.WriteLine(language == "es" ? $"Autor: {comment.AuthorDisplayName}" : $"Author: {comment.AuthorDisplayName}");
                    Console.WriteLine(language == "es" ? $"Comentario: {comment.TextDisplay}" : $"Comment: {comment.TextDisplay}");
                    Console.WriteLine();
                }

                if (commentThreadsResponse.Items.Count == 0)
                {
                    Console.WriteLine(language == "es" ? "El video no tiene comentarios." : "The video has no comments.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(language == "es" ? "Error al obtener los comentarios del video: " + ex.Message : "Error getting video comments: " + ex.Message);
            }
        }

        static void ObtenerEstadisticas(YouTubeService youtubeService, string language)
        {
            string videoId = ObtenerIdVideoDesdeEnlace(language);

            if (string.IsNullOrEmpty(videoId))
            {
                Console.WriteLine(language == "es" ? "No se pudo obtener el ID del video. Verifica el enlace e inténtalo de nuevo." : "Could not get the video ID. Please check the link and try again.");
                return;
            }

            try
            {
                // Get the video statistics
                var videoRequest = youtubeService.Videos.List("statistics");
                videoRequest.Id = videoId;
                var videoResponse = videoRequest.Execute();

               // Check if the video was found
                if (videoResponse.Items.Count > 0)
                {
                    var statistics = videoResponse.Items[0].Statistics;

                    // Show the video statistics
                    Console.WriteLine(language == "es" ? "Estadísticas del Video:" : "Video Statistics:");
                    Console.WriteLine(language == "es" ? "Vistas: " + statistics.ViewCount : "Views: " + statistics.ViewCount);
                    Console.WriteLine(language == "es" ? "Me gusta: " + statistics.LikeCount : "Likes: " + statistics.LikeCount);
                    Console.WriteLine(language == "es" ? "No me gusta: " + statistics.DislikeCount : "Dislikes: " + statistics.DislikeCount);
                    Console.WriteLine(language == "es" ? "Comentarios: " + statistics.CommentCount : "Comments: " + statistics.CommentCount);
                }
                else
                {
                    Console.WriteLine(language == "es" ? "No se encontró ningún video con el ID proporcionado." : "No video found with the provided ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(language == "es" ? "Error al obtener las estadísticas del video: " + ex.Message : "Error getting video statistics: " + ex.Message);
            }
        }

        static string ObtenerIdVideoDesdeEnlace(string language)
        {
            Console.WriteLine(language == "es" ? "Por favor, introduce el enlace completo del video de YouTube:" : "Please enter the full link of the YouTube video:");
            string enlace = Console.ReadLine();

            //Regular expression to extract video ID from YouTube link
            var regex = new Regex(@"(?:https?:\/\/)?(?:www\.)?youtu(?:\.be|be\.com)\/(?:.*v(?:\/|=)|(?:.*\/)?)([a-zA-Z0-9-_]+)");

            var match = regex.Match(enlace);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return null;
            }
        }
    }
}
