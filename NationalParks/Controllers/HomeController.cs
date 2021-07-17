using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationalParks.Models;
using NationalParks.APIHandlerManager;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using NationalParks.Controllers;

using NationalParks.DataAccess;

namespace NationalParks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Parks(string dr_no, string vict_age, string vict_sex, string crm_cd_desc, string area_name)
        {
            // APIHandler webHandler = new APIHandler();
            // List<Class1> parks = webHandler.getParks();            
            //ViewBag.Total = parks;
            dr_no = (dr_no == null) ? "" : dr_no;
            vict_age = (vict_age == null) ? "" : vict_age;
            vict_sex = (vict_sex == null) ? "" : vict_sex;
            crm_cd_desc = (crm_cd_desc == null) ? "" : crm_cd_desc;
            area_name = (area_name == null) ? "" : area_name;
            
            
            List<Class1> plist = new List<Class1>();
            //ViewBag.Total = par;
            if (dr_no == "" && vict_age == "" && vict_sex == "" && crm_cd_desc == "" && area_name == "")
            {
                plist = dbContext.Classes.ToList();
            }
            // else
            //{
            //   plist = dbContext.Class1
            //              .Where(p => p.dr_no.Any(s => s.Class1.name.Contains(activityname)))
            //             .Where(p => p.states.Any(s => s.state.ID.Contains(statename)))
            //            .Where(p => p.fullName.Contains(parkname))
            //          .ToListAsync();
            //  }
            
            ViewBag.Total = plist;
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Visualization()
        {
            return View();
        }

        public IActionResult Model()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        public IActionResult Details(string dr_no)
        {
            dr_no = (dr_no == null) ? "" : dr_no;
            /*List<Class1> detailsPark = new List<Class1>(); */
            List<Class1> c = new List<Class1>();

            if (dr_no != "")
            {
                c = dbContext.Classes.Where(p => p.dr_no == dr_no).ToList();
            }
           /* var dtpk = new Class1()
            {
                // id = updatePark.id,
                id = detailsPark.id.SingleOrDefault(p => p.id == d),
                dr_no = detailsPark.dr_no,
                vict_age = detailsPark.vict_age,
                vict_sex = detailsPark.vict_sex,
                crm_cd_desc = detailsPark.crm_cd_desc,
                area_name = detailsPark.area_name,
                location = detailsPark.location,
                premis_desc = detailsPark.premis_desc
                 //date_occ = updatePark.date_occ
             }; */
            

            /*  {
                  if (d == null)
                  {
                      return NotFound();
                  }
                  var detailsPark = dbContext.Classes.AsNoTracking().FirstOrDefault(u => u.id.Equals(d));
                  if (detailsPark == null)
                  {
                      return NotFound();
                  }
                  ViewData["Title"] = "Details: " + detailsPark;
                  */
            ViewBag.Total = c[0];
            
            return View();
        }
      //  {
       //     APIHandler webHandler = new APIHandler();
           // List<Class1> parks = webHandler.getParks();
       //     ViewBag.Total = parks;
       //     return View();
       // }

       // public IActionResult Update()
       // {
       //     APIHandler webHandler = new APIHandler();
         //   List<Class1> parks = webHandler.GetParks();
         //   ViewBag.Total = parks;
          //  return View();
       // }

        public IActionResult Update(int id)
        {
            Class1 updatePark = dbContext.Classes.Where(p => p.id == id).FirstOrDefault();


            Update updtpk = new Update()
            {
                // id = updatePark.id,
                dr_no = updatePark.dr_no,
                vict_age = updatePark.vict_age,
                vict_sex = updatePark.vict_sex,
                crm_cd_desc = updatePark.crm_cd_desc,
                area_name = updatePark.area_name,
                date_occ = updatePark.date_occ,
                location = updatePark.location,
                premis_desc=updatePark.premis_desc
            };

            return View(updtpk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("dr_no,vict_age,vict_sex,crm_cd_desc,area_name,date_occ,location,premis_desc")] Update updatedpk)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Class1 ptobeupdated = dbContext.Classes
                        .Where(p => p.id == id)
                        .FirstOrDefault();

                    ptobeupdated.dr_no = updatedpk.dr_no;
                    ptobeupdated.vict_age = updatedpk.vict_age;
                    ptobeupdated.vict_sex = updatedpk.vict_sex;
                    ptobeupdated.crm_cd_desc = updatedpk.crm_cd_desc;
                    ptobeupdated.area_name = updatedpk.area_name;
                    ptobeupdated.date_occ = updatedpk.date_occ;
                    ptobeupdated.location = updatedpk.location;
                    ptobeupdated.premis_desc = updatedpk.premis_desc;


                    dbContext.Update(ptobeupdated);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Error Occured");
            }

            
            return View(updatedpk);
        }

        public async Task<IActionResult> Delete(int id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            Class1 deletePark = dbContext.Classes.Where(s => s.id == id).FirstOrDefault();
            

            if (deletePark == null)
            {
                return NotFound();
            }
            ViewData["Title"] = "Delete: " + deletePark.id;

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Error Occured";
            }

            return View(deletePark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Class1 deletepark = dbContext.Classes.Where(s => s.id == id).FirstOrDefault();

            if (deletepark == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                dbContext.Classes.Remove(deletepark);
                 dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        public async Task<IActionResult> Create()
        {
            
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("dr_no,vict_age,vict_sex,crm_cd_desc,area_name,date_occ,location,premis_desc")] AddNewPark pk)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Class1 npk = new Class1()
                    {
                        dr_no = pk.dr_no,
                    vict_age = pk.vict_age,
                    vict_sex = pk.vict_sex,
                    crm_cd_desc = pk.crm_cd_desc,
                    area_name = pk.area_name,
                        date_occ = pk.date_occ,
                        location = pk.location,
                        premis_desc = pk.premis_desc
                    };
                    dbContext.Classes.Add(npk);
                    
                       
                    
                    
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Error Occured");
            }

            
            return View(pk);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
