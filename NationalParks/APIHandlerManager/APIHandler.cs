using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using NationalParks.DataAccess;
using NationalParks.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using NationalParks.Models;

namespace NationalParks.APIHandlerManager
{
    public static class DbInitializer
    {
        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://www.nps.gov/subjects/developer/get-started.htm
        static HttpClient httpClient;
        static string BASE_URL = "https://data.lacity.org/resource/d5tf-ez2w.json";
        //static string API_KEY = "FUuWFYKJN0aGJrCu47wYMCVKWKXZkMrAxOKbFzXg";

        //HttpClient httpClient;

        //public ApplicationDbContext dbContext;

        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
           getParks(context);
            //getLocation1(context);
            
        }
        //public static void Initialize(ApplicationDbContext context)
        //{
          //  context.Database.EnsureCreated();
            //getClass1(context);
            //getLocation1(context);
       // }

            
        

        /// <summary>
        ///  Constructor to initialize the connection to the data source
        /// </summary>
       // public APIHandler()
  //  {
  //    httpClient = new HttpClient();
  //    httpClient.DefaultRequestHeaders.Accept.Clear();
     // httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
   //   httpClient.DefaultRequestHeaders.Accept.Add(
   //       new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
  //  }

    /// <summary>
    /// Method to receive data from API end point as a collection of objects
    /// 
    /// JsonConvert parses the JSON string into classes
    /// </summary>
    /// <returns></returns>
    public static void getParks(ApplicationDbContext context)
    {
            if (context.Classes.Any())
            {
                return;
            }
            string NATIONAL_PARK_API_PATH = BASE_URL + "?$limit=10";
      string parksData = "";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            // JsonArrayAttribute jArray = null;

            List<Class1> parks = null;

      httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

      // It can take a few requests to get back a prompt response, if the API has not received
      //  calls in the recent past and the server has put the service on hibernation
      try
      {
        HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();
        if (response.IsSuccessStatusCode)
        {
          parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    //Console.WriteLine(parksData);
                    //jArray = new JsonArrayAttribute(parksData);
        }

        if (!parksData.Equals(""))
        {
          // JsonConvert is part of the NewtonSoft.Json Nuget package
          parks = JsonConvert.DeserializeObject<List<Class1>>(parksData);
                    //string area = parks.GetType()
                   // Console.WriteLine(parks);
        }
        foreach(var item in parks)
                {
                    Class1 c = new Class1();
                    c.dr_no = item.dr_no;
                    context.Classes.Add(item);
                }
                context.SaveChanges();
        
                /*JObject parsedResponse = JObject.Parse(parksData);
                JArray activities = (JArray)parsedResponse[""];
                foreach (JObject jsonactivity in activities)
                {
                    Class1 activity = new Class1
                    {
                        id = (int)jsonactivity["id"],
                        dr_no = (string)jsonactivity["dr_no"],
                        vict_age = (string)jsonactivity["vict_age"],
                        area_name = (string)jsonactivity["area_name"],
                        crm_cd_desc =(string)jsonactivity["crm_cd_desc"],
                        vict_sex = (string)jsonactivity["vict_sex"],

                    };
                    context.Classes.Add(activity);
                }
                    
                context.SaveChanges();*/
            }
      catch (Exception e)
      {
        // This is a useful place to insert a breakpoint and observe the error message
        Console.WriteLine(e.Message);
      }

     // return parks;


    }
  }
}