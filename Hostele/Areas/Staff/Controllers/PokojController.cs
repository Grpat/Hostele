#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hostele.Data;
using Hostele.Models;
using Hostele.Models.ViewModels;
using Hostele.Repository;

namespace Hostele.Controllers
{
    [Area("Staff")]
    public class PokojController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IMapper _mapper;

        public PokojController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Pokoj
        public async Task<IActionResult> Index()
        {
            var pokoje = await _unitOfWork.Pokoj.GetAll(includeProperties: "Rodzaj,Hostel");
            var pokojViewModel= _mapper.Map<IEnumerable<Pokoj>, IEnumerable<PokojViewModel>>(pokoje);
            return View(pokojViewModel);
        }

        // GET: Pokoj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findPokojById = await _unitOfWork.Pokoj.GetFirstOrDefault(u => u.Id == id, includeProperties: "Rodzaj,Hostel");
                
            if (findPokojById == null)
            {
                return NotFound();
            }
            var pokojItemDetailsViewModel= _mapper.Map<Pokoj, PokojItemDetailsViewModel>(findPokojById);
            return View(pokojItemDetailsViewModel);
        }

        // GET: Pokoj/Create
        public async Task<IActionResult> Create()
        {
            PokojItemViewModel pokojVm = new PokojItemViewModel();
            pokojVm.RodzajePokoi=new SelectList(await _unitOfWork.Rodzaj.GetAll(), "Id", "NazwaRodzaju");
            pokojVm.ListofHostels = new SelectList(await _unitOfWork.Hostel.GetAll(), "Id", "Nazwa");
            
            return View(pokojVm);
        }

        // POST: Pokoj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,HostelId,RodzajId,CenaZaNocleg,IloscLozek")] PokojItemViewModel viewPokoj)
        {
            viewPokoj.RodzajePokoi=new SelectList(await _unitOfWork.Rodzaj.GetAll(), "Id", "NazwaRodzaju", viewPokoj.RodzajId);
            viewPokoj.ListofHostels = new SelectList(await _unitOfWork.Hostel.GetAll(), "Id", "Nazwa", viewPokoj.HostelId);
            
            if (ModelState.IsValid)
            {
                
                var pokoj = _mapper.Map<Pokoj>(viewPokoj);
               // pokoj.
                
                _unitOfWork.Pokoj.Add(pokoj);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
          
           
            return View(viewPokoj);
        }

        // GET: Pokoj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pokoj = await _unitOfWork.Pokoj.GetFirstOrDefault(u => u.Id == id);
            if (pokoj == null)
            {
                return NotFound();
            }
            var pokojItemViewModel=_mapper.Map<PokojItemViewModel>(pokoj);
            ViewData["HostelId"] = new SelectList(await _unitOfWork.Hostel.GetAll(), "Id", "Nazwa");
            ViewData["RodzajId"] = new SelectList(await _unitOfWork.Rodzaj.GetAll(), "Id", "Id");
            return View(pokojItemViewModel);
        }

        // POST: Pokoj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis,HostelId,RodzajId,CenaZaNocleg,IloscLozek")] PokojItemViewModel viewPokoj)
        {
            if (id != viewPokoj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pokoj = _mapper.Map<Pokoj>(viewPokoj);
                    _unitOfWork.Pokoj.Update(pokoj);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokojExists(viewPokoj.Id))
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
            
            ViewData["HostelId"] = new SelectList( await _unitOfWork.Hostel.GetAll(), "Id", "Nazwa", viewPokoj.HostelId);
            ViewData["RodzajId"] = new SelectList(await _unitOfWork.Rodzaj.GetAll(), "Id", "Id", viewPokoj.RodzajId);
            return View(viewPokoj);
        }

        // GET: Pokoj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokoj = await _unitOfWork.Pokoj.GetFirstOrDefault(m => m.Id == id, includeProperties: "Hostel");
               
            if (pokoj == null)
            {
                return NotFound();
            }

            return View(pokoj);
        }

        // POST: Pokoj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokoj = await _unitOfWork.Pokoj.GetFirstOrDefault(m => m.Id == id);
            if (pokoj != null) _unitOfWork.Pokoj.Remove(pokoj);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PokojExists(int id)
        {
            return _unitOfWork.Pokoj.CheckIfExists(e => e.Id == id);
        }
    }
}
