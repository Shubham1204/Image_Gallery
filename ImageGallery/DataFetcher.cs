using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery
{
    class DataFetcher
    {

        async Task<string> GetDatafromService(string searchstring)
        {
            string readText = null;
            try
            {
                //Read data from the server
                var azure = @"https://imagefetcher20200529182038.azurewebsites.net";
                string url = azure + @"/api/fetch_images?query=" +
                searchstring + "&max_count=8";
                using (HttpClient c = new HttpClient())
                {
                    readText = await c.GetStringAsync(url);
                }
            }
            catch
            {
                //Read json file if there is some error while fetching the data from the server
                readText = File.ReadAllText("../../Data/sampleData.json");
            }
            return readText;
        }



        public async Task<List<ImageItem>> GetImageData(string search)
        {
            string data = await GetDatafromService(search);
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }
    }
}
