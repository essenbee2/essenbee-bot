﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Essenbee.Bot.Core.Data;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Web.Pages.TimedMessage
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository _repository;

        public DeleteModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Core.Data.TimedMessage TimedMessage { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimedMessage = _repository.Single<Core.Data.TimedMessage>(DataItemPolicy<Core.Data.TimedMessage>.ById(id.Value));

            if (TimedMessage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TimedMessage = _repository.Single<Core.Data.TimedMessage>(DataItemPolicy<Core.Data.TimedMessage>.ById(id.Value));

            if (TimedMessage != null)
            {
                _repository.Remove<Core.Data.TimedMessage>(TimedMessage);
            }

            return RedirectToPage("./Index");
        }
    }
}
