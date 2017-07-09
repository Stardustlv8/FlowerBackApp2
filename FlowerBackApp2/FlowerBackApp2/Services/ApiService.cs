﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlowerBackApp2.Services
{
    public class ApiService
    {
        public async Task<List<T>> Get<T>(string urlBase, string serviceprefix, string controller)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri(urlBase);

                var url = $"{serviceprefix}{controller}";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}