﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PitalicaSeminar.DAL.Entities;

namespace PitalicaSeminar.Controllers
{
    public class ExamResultsController : Controller
    {
        private readonly PitalicaDbContext _context;

        public ExamResultsController(PitalicaDbContext context)
        {
            _context = context;
        }

        // GET: ExamResults
        public async Task<IActionResult> Index()
        {
            var pitalicaDbContext = _context.ExamResults.Include(e => e.Exam).Include(e => e.User);
            return View(await pitalicaDbContext.ToListAsync());
        }

        // GET: ExamResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults
                .Include(e => e.Exam)
                .Include(e => e.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // GET: ExamResults/Create
        public IActionResult Create()
        {
            ViewData["ExamId"] = new SelectList(_context.Exams, "ExamId", "ExamName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName");
            return View();
        }

        // POST: ExamResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExamId,UserId,Score,WriteDate")] ExamResults examResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamId"] = new SelectList(_context.Exams, "ExamId", "ExamName", examResult.ExamId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Password", examResult.UserId);
            return View(examResult);
        }

        // GET: ExamResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults.SingleOrDefaultAsync(m => m.Id == id);
            if (examResult == null)
            {
                return NotFound();
            }
            ViewData["ExamId"] = new SelectList(_context.Exams, "ExamId", "ExamName", examResult.ExamId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Password", examResult.UserId);
            return View(examResult);
        }

        // POST: ExamResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExamId,UserId,Score,WriteDate")] ExamResults examResult)
        {
            if (id != examResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamResultExists(examResult.Id))
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
            ViewData["ExamId"] = new SelectList(_context.Exams, "ExamId", "ExamName", examResult.ExamId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Password", examResult.UserId);
            return View(examResult);
        }

        // GET: ExamResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults
                .Include(e => e.Exam)
                .Include(e => e.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // POST: ExamResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examResult = await _context.ExamResults.SingleOrDefaultAsync(m => m.Id == id);
            _context.ExamResults.Remove(examResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamResultExists(int id)
        {
            return _context.ExamResults.Any(e => e.Id == id);
        }
    }
}
