using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Controllers
{
    public class PlannersController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public PlannersController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Planners
        public async Task<IActionResult> Index()
        {
            // Include the related SpeakersTopics collection
            var planners = await _context.Planner
                .Include(p => p.SpeakersTopics) // Load related SpeakersTopics
                .ToListAsync();

            return View(planners);
        }


        // GET: Planners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Eagerly load the related SpeakersTopics collection
            var planner = await _context.Planner
                .Include(p => p.SpeakersTopics) // Include related SpeakersTopics
                .FirstOrDefaultAsync(m => m.PlannerId == id);

            if (planner == null)
            {
                return NotFound();
            }

            return View(planner);
        }


        // GET: Planners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Planner planner)
        {
            planner.SpeakersTopics = planner.SpeakersTopics
                .Where(st => 
                    !string.IsNullOrEmpty(st.Speaker) || !string.IsNullOrEmpty(st.Topic)
                    )
                .ToList();
            foreach(SpeakerTopic spk in planner.SpeakersTopics)
            {
                if(string.IsNullOrEmpty(spk.Speaker))
                {
                    spk.Speaker = "None Selected";
                }
                else if(string.IsNullOrEmpty(spk.Topic))
                {
                    spk.Topic = "None Selected";
                }
            }

            if (ModelState.IsValid)
            {
                // Add the planner to the database
                _context.Planner.Add(planner);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(planner);
        }


        // GET: Planners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planner = await _context.Planner
                .Include(p => p.SpeakersTopics) // Load the related SpeakersTopics
                .FirstOrDefaultAsync(m => m.PlannerId == id);

            if (planner == null)
            {
                return NotFound();
            }

            return View(planner);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Planner planner)
        {

            if (id != planner.PlannerId)
            {
                return NotFound();
            }

            planner.SpeakersTopics = planner.SpeakersTopics.Where(
                st => !string.IsNullOrEmpty(st.Speaker) || !string.IsNullOrEmpty(st.Topic))
                .ToList();

            foreach (SpeakerTopic spk in planner.SpeakersTopics)
            {
                if (string.IsNullOrEmpty(spk.Speaker))
                {
                    spk.Speaker = "None Selected";
                }
                else if (string.IsNullOrEmpty(spk.Topic))
                {
                    spk.Topic = "None Selected";
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing Planner with its related SpeakersTopics
                    var existingPlanner = await _context.Planner
                        .Include(p => p.SpeakersTopics)
                        .FirstOrDefaultAsync(p => p.PlannerId == id);

                    if (existingPlanner == null)
                    {
                        return NotFound();
                    }

                    // Update Planner details
                    existingPlanner.Date = planner.Date;
                    existingPlanner.Conducting = planner.Conducting;
                    existingPlanner.OpeningHymn = planner.OpeningHymn;
                    existingPlanner.Invocation = planner.Invocation;
                    existingPlanner.SacramentHymn = planner.SacramentHymn;
                    existingPlanner.IntermediateHymn = planner.IntermediateHymn;
                    existingPlanner.ClosingHymn = planner.ClosingHymn;
                    existingPlanner.Benediction = planner.Benediction;

                    // Handle SpeakersTopics
                    // Remove topics that were removed in the form
                    existingPlanner.SpeakersTopics.RemoveAll(
                        st => !planner.SpeakersTopics.Any(pst => pst.SpeakerTopicId == st.SpeakerTopicId)
                    );

                    // Add new or updated topics
                    foreach (var speakerTopic in planner.SpeakersTopics)
                    {
                        if (speakerTopic.SpeakerTopicId == 0)
                        {
                            // New SpeakerTopic (add)
                            existingPlanner.SpeakersTopics.Add(speakerTopic);
                        }
                        else
                        {
                            // Existing SpeakerTopic (update)
                            var existingTopic = existingPlanner.SpeakersTopics
                                .FirstOrDefault(st => st.SpeakerTopicId == speakerTopic.SpeakerTopicId);

                            if (existingTopic != null)
                            {
                                existingTopic.Speaker = speakerTopic.Speaker;
                                existingTopic.Topic = speakerTopic.Topic;
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlannerExists(planner.PlannerId))
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

            return View(planner);
        }


        private bool PlannerExists(int id)
        {
            return _context.Planner.Any(e => e.PlannerId == id);
        }



        // GET: Planners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Eagerly load related SpeakersTopics
            var planner = await _context.Planner
                .Include(p => p.SpeakersTopics) // Load SpeakersTopics for display
                .FirstOrDefaultAsync(m => m.PlannerId == id);

            if (planner == null)
            {
                return NotFound();
            }

            return View(planner);
        }


        // POST: Planners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Fetch the Planner along with its related SpeakersTopics
            var planner = await _context.Planner
                .Include(p => p.SpeakersTopics) // Include related SpeakersTopics
                .FirstOrDefaultAsync(m => m.PlannerId == id);

            if (planner != null)
            {
                // Remove the Planner along with its related SpeakersTopics
                _context.Planner.Remove(planner);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
