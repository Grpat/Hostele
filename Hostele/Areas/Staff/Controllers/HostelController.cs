#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class HostelController : Controller
    {
        private readonly IMapper _mapper;
        
        private readonly IUnitOfWork _unitOfWork;

        public HostelController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Hostel
        public async Task<IActionResult> Index()
        {
            var hostele = await _unitOfWork.Hostel.GetAll(includeProperties:"Pokoje");
            var hostelViewModel= _mapper.Map<IEnumerable<Hostel>, IEnumerable<HostelViewModel>>(hostele);
            
            
            return View(hostelViewModel);
           
        }

        // GET: Hostel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findHostelById = await _unitOfWork.Hostel.GetFirstOrDefault(u=>u.Id==id,includeProperties:"Pokoje")
               ;
            if (findHostelById == null)
            {
                return NotFound();
            }
            var hostelItemDetailsViewModel= _mapper.Map<Hostel, HostelItemDetailsViewModel>(findHostelById);
            return View(hostelItemDetailsViewModel);
        }

        // GET: Hostel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hostel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,Pojemnosc")] HostelItemViewModel viewHostel)
        {
            if (ModelState.IsValid)
            {
                var hostel = _mapper.Map<Hostel>(viewHostel);
                _unitOfWork.Hostel.Add(hostel);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(viewHostel);
        }

        // GET: Hostel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hostel = await _unitOfWork.Hostel.GetFirstOrDefault(u => u.Id == id);
            var hostelItemViewModel=_mapper.Map<HostelItemViewModel>(hostel);
            if (hostel == null)
            {
                return NotFound();
            }
            return View(hostelItemViewModel);
        }

        // POST: Hostel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis,Pojemnosc")] HostelItemViewModel viewHostel)
        {
            if (id != viewHostel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var hostel = _mapper.Map<Hostel>(viewHostel);
                    _unitOfWork.Hostel.Update(hostel);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HostelExists(viewHostel.Id))
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
            return View(viewHostel);
        }

        // GET: Hostel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hostel = await _unitOfWork.Hostel.GetFirstOrDefault(u => u.Id == id);
            if (hostel == null)
            {
                return NotFound();
            }

            return View(hostel);
        }

        // POST: Hostel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hostel = await _unitOfWork.Hostel.GetFirstOrDefault(u => u.Id == id,includeProperties:"Pokoje");
            //if (hostel != null) _unitOfWork.Hostel.Remove(hostel);
            if (hostel.Pokoje != null)
            {
                hostel.Pokoje.Clear();
                _unitOfWork.Hostel.Remove(hostel);
            }
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool HostelExists(int id)
        {
            return _unitOfWork.Hostel.CheckIfExists(u => u.Id == id);
        }
    }
}
