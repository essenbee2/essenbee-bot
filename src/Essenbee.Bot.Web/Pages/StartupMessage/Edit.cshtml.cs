﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Essenbee.Bot.Web.Pages.StartupMessage
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;

        public EditModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public List<SelectListItem> Statuses { get; set; }

        [BindProperty]
        public Core.Data.StartupMessage StartupMessage { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StartupMessage = _repository.Single<Core.Data.StartupMessage>(DataItemPolicy<Core.Data.StartupMessage>.ById(id.Value));

            if (StartupMessage == null)
            {
                return NotFound();
            }

            Statuses = new List<SelectListItem>
            {
                new SelectListItem {Text = "Active", Value = "0"},
                new SelectListItem {Text = "Draft", Value = "1"},
                new SelectListItem {Text = "Disabled", Value = "2"}
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Update<Core.Data.StartupMessage>(StartupMessage);

            return RedirectToPage("/Admin");
        }

        private bool StartupMessageExists(Guid id)
        {
            return _repository.List<Core.Data.StartupMessage>(DataItemPolicy<Core.Data.StartupMessage>.ById(id)).Any();
        }
    }
}
