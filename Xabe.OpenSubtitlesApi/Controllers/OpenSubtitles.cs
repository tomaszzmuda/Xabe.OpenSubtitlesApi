using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using OSDBnet;
using OSDBnet.Backend;
using RestSharp;

namespace Xabe.OpenSubtitlesApi.Controllers
{
    public class OpenSubtitlesController: ApiController
    {
        public async Task<IHttpActionResult> GetByHash(string hash)
        {
            var client = Osdb.Login("OSTestUserAgentTemp");

            var request = new SearchSubtitlesRequest
            {
                imdbid = string.Empty,
                query = string.Empty,
                moviehash = hash,
                sublanguageid = "pol"
            };

            MethodInfo dynMethod = client.GetType()
                                         .GetMethod("SearchSubtitlesInternal",
                                             BindingFlags.NonPublic | BindingFlags.Instance);
            var subtitlesList = (List<Subtitle>) dynMethod.Invoke(client, new object[] {request});
            var subtitles = subtitlesList.OrderByDescending(x => x.Rating)
                                         .FirstOrDefault();
            var subtitlesPath = client.DownloadSubtitleToPath(Path.GetTempPath(), subtitles);


            string srt = ConvertToSrt(subtitlesPath);

            return Ok(Encoding.UTF8.GetBytes(srt));
        }

        private string ConvertToSrt(string path)
        {
            var client = new RestClient("https://opensubtitles-subtitle-tools.p.mashape.com/convert");
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Mashape-Key", Environment.GetEnvironmentVariable("MASHAP_API_KEY"));
            request.AddFileBytes("file", File.ReadAllBytes(path), "file.txt");
            request.AddParameter("convert_to", "srt");
            request.AddParameter("output_charset_encoding", "UTF-8");
            IRestResponse<Model.Root> response = client.Execute<Model.Root>(request);
            return response.Data.converted;
        }
    }
}
