using Microsoft.AspNetCore.Mvc;
using Data;
using Domain.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace Mvc.Controllers
{
    public class PersonController : Controller
    {


        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = _context.People.ToList();
            return View(people);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Person model)
        {

            _context.People.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

            //var person = _context.People.First(c => c.Id == model.Id);

        }

    }
}