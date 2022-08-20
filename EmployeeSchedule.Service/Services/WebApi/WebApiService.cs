using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Entities.ApiEntities;
using EmployeeSchedule.Data.Interface.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services.WebApi
{
    public class WebApiService : IWebApiService
    {
        public const string baseUrl = @"http://localhost:5000/api";
        public async Task<bool> DeleteEmployee(int id)
        {
            using(var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{baseUrl}/employee/{id}/");
                return response.IsSuccessStatusCode; 
            }
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{baseUrl}/company/");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var companies = JsonConvert.DeserializeObject<List<Company>>(content);
                return companies;
            }
        }

        public async Task<List<Holiday>> GetHolidays()
        {
            if(Storage.Storage.Instance.Holidays != null && Storage.Storage.Instance.Holidays.Any())
            {
                return Storage.Storage.Instance.Holidays;
            }

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://public-holiday.p.rapidapi.com/2022/RS"),
                    Headers =
                {
                    { "X-RapidAPI-Host", "public-holiday.p.rapidapi.com" },
                    { "X-RapidAPI-Key", "7002e73079mshe763fe05ff40383p14cc92jsnc2396a8a4bf2" },
                },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var holidays = JsonConvert.DeserializeObject<List<Holiday>>(content);

                    if(holidays == null || !holidays.Any()) 
                    {
                        return null;    
                    }

                    Storage.Storage.Instance.Holidays = holidays;
                    return holidays;
                }
            }
        }

        public async Task<List<City>> GetCities()
        {
            if (Storage.Storage.Instance.Cities != null && Storage.Storage.Instance.Cities.Any())
            {
                return Storage.Storage.Instance.Cities;
            }
             
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://spott.p.rapidapi.com/places?skip=0&country=RS&limit=100"),
                    Headers =
                            {
                                { "X-RapidAPI-Host", "spott.p.rapidapi.com" },
                                { "X-RapidAPI-Key", "7002e73079mshe763fe05ff40383p14cc92jsnc2396a8a4bf2" },
                            },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var cities = JsonConvert.DeserializeObject<List<City>>(content);

                    if (cities == null || !cities.Any())
                    {
                        return null;
                    }

                    Storage.Storage.Instance.Cities = cities;
                    return cities;
                }
            }
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync($"{baseUrl}/schedule/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var content = await response.Content.ReadAsStringAsync();
                var companies = JsonConvert.DeserializeObject<Schedule>(content);
                return companies;
            }
        }

        public async Task<bool> UpdateSchedule(Schedule schedule)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Put;
                request.Content = JsonContent.Create(schedule);
                request.RequestUri = new Uri(baseUrl + $"/schedule/{schedule.Id}");
                var response = await client.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
