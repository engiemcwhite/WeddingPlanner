using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace planner.Controllers
{
    public class PlannerController : Controller
    {

        private PlannerContext _context;
 
        public PlannerController(PlannerContext context)
        {
            _context = context;
        }
 
        [HttpGet]
        [Route("/dashboard")]
        public IActionResult Index()
        {
            ViewBag.AllWeddings = _context.Weddings.Include(wed => wed.Guests).ToList();
            if(HttpContext.Session.GetInt32("id") == null){
                return Redirect("/");
            }
            ViewBag.Id = HttpContext.Session.GetInt32("id");
            ViewBag.Temp = _context.Users.Include(a => a.Rsvps);
            return View();
            // Other code
        }

        [HttpPost]
        [Route("/newwedding")]
        public IActionResult NewWedding(PlanViewModel model)
        {
            if(ModelState.IsValid){
                Wedding NewWed = new Wedding {
                    Wedder1=model.Wedder1,
                    Wedder2=model.Wedder2,
                    Date=model.Date, 
                    Address=model.Address,
                    UserId = (int)HttpContext.Session.GetInt32("id"),
                };
                _context.Add(NewWed);
                _context.SaveChanges();
                return Redirect("/dashboard");
            }
            return View("PlanWedding");
        }

        [HttpGet]
        [Route("/delete/{num}")]
        public IActionResult Delete(int num){
            Wedding thiswedding = _context.Weddings.Include(b => b.Guests).SingleOrDefault(a => a.WeddingId == num);
            foreach(Rsvp guest in thiswedding.Guests){
                _context.Rsvps.Remove(guest);
            }
            _context.Weddings.Remove(thiswedding);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

       [HttpGet]
       [Route("/rsvp/{num}")]
       public IActionResult Rsvp(int num){
          // Wedding thiswedding = _context.Weddings.SingleOrDefault(a => a.WeddingId == num);
           Rsvp newRsvp = new Rsvp {
               UserId = (int)HttpContext.Session.GetInt32("id"),
               WeddingId = num
           };
           _context.Rsvps.Add(newRsvp);
           _context.SaveChanges();
           return RedirectToAction("Index");
       }

       [HttpGet]
       [Route("/unrsvp/{num}")]
       public IActionResult Unrsvp(int num){
           Wedding thiswedding = _context.Weddings.Include(a => a.Guests).SingleOrDefault(a => a.WeddingId == num);
           foreach(Rsvp guest in thiswedding.Guests){
               if(guest.UserId == (int)HttpContext.Session.GetInt32("id")){
                   _context.Rsvps.Remove(guest);
               }
           }
            _context.SaveChanges();
           return RedirectToAction("Index");
       }

       [HttpGet]
       [Route("/weddings/{num}")]
       public IActionResult Wedding(int num){
           ViewBag.Wedding = _context.Weddings.Where(a => a.WeddingId == num).Include(wed => wed.Guests).ThenInclude(a => a.User).ToList();
           return View();
       }

       [HttpGet]
       [Route("/planwedding")]
       public IActionResult PlanWedding(){
           return View();
       }
    }
}