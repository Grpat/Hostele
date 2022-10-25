#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hostele.Data;
using Hostele.Repository;
using Hostele.ViewModels;

namespace Hostele.Models
{
    [Area("User")]
    public class WypozyczeniaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WypozyczeniaController(ApplicationDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Wypozyczenia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Wypozyczenia.Include(w => w.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Wypozyczenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = await _context.Wypozyczenia
                .Include(w => w.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }

            return View(wypozyczenie);
        }

        // GET: Wypozyczenia/Create
        public async Task <IActionResult> Create()
        {
            var wypozyczenieCreateViewModel = new WypozyczenieCreateViewModel();
            var wypozyczenia =
                await _unitOfWork.Wypozyczenie.GetSelected(filter: x => x.Status == StatusWypozyczenia.Rezerwacja,select:x=>x.PokojId);
            
            
            wypozyczenieCreateViewModel.ListaPokoi= new SelectList( await _unitOfWork.Pokoj.GetDostepnePokoje(wypozyczenia), "Id", "Nazwa");
            return View(wypozyczenieCreateViewModel);
            
        }

        // POST: Wypozyczenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataRozpoczecia,DataZakonczenia,PokojId")] WypozyczenieCreateViewModel wypozyczenieCreateViewModel)
        {
            var wypozyczenia =
                await _unitOfWork.Wypozyczenie.GetSelected(filter: x => x.Status == StatusWypozyczenia.Rezerwacja,select:x=>x.PokojId);
            wypozyczenieCreateViewModel.ListaPokoi= new SelectList( await _unitOfWork.Pokoj.GetDostepnePokoje(wypozyczenia), "Id", "Nazwa", wypozyczenieCreateViewModel.PokojId);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var wypozyczenie = _mapper.Map<Wypozyczenie>(wypozyczenieCreateViewModel);
                wypozyczenie.Status = StatusWypozyczenia.Rezerwacja;
                wypozyczenie.AppUserId = claim.Value;
                _context.Add(wypozyczenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
           
            return View(wypozyczenieCreateViewModel);
        }

        // GET: Wypozyczenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = await _context.Wypozyczenia.FindAsync(id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", wypozyczenie.AppUserId);
            return View(wypozyczenie);
        }

        // POST: Wypozyczenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataUtworzenia,DataRozpoczecia,DataZakonczenia,Status,AppUserId,PokojId")] Wypozyczenie wypozyczenie)
        {
            if (id != wypozyczenie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wypozyczenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WypozyczenieExists(wypozyczenie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", wypozyczenie.AppUserId);
            return View(wypozyczenie);
        }

        // GET: Wypozyczenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wypozyczenie = await _context.Wypozyczenia
                .Include(w => w.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }

            return View(wypozyczenie);
        }

        // POST: Wypozyczenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wypozyczenie = await _context.Wypozyczenia.FindAsync(id);
            _context.Wypozyczenia.Remove(wypozyczenie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WypozyczenieExists(int id)
        {
            return _context.Wypozyczenia.Any(e => e.Id == id);
        }
    }
}
