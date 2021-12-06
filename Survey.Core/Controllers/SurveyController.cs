
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mail;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Extensions;
using Umbraco.Cms.Core.Security;
using SurveyPackage.Models;

namespace SurveyPackage.Controllers
{
    public class SubmitController : SurfaceController
    {
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<SubmitController> _logger;
        private readonly IContentService _contentService;
        private readonly IMemberService _memberService;
        private readonly IMemberManager _manager;
        private readonly IPasswordHasher _hasher;
        public SubmitController(
           IUmbracoContextAccessor umbracoContextAccessor,
           IUmbracoDatabaseFactory databaseFactory,
           ServiceContext services,
           AppCaches appCaches,
           IProfilingLogger profilingLogger,
           IConfiguration Configuration,
           IEmailSender emailSender,
           ILogger<SubmitController> logger,
           IPublishedUrlProvider publishedUrlProvider,
           IMemberService memberService, IMemberManager manager, IPasswordHasher hasher)
           : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _config = Configuration;
            _logger = logger;
            _emailSender = emailSender;
            _memberService = memberService;
            _manager = manager;
            _hasher = hasher;
        }

        public async Task<IActionResult> SubmitForm(Survey model)
        {
            if (ModelState.IsValid)
            {
                var parentId = new Guid("328d4790-f0d3-42f4-b21b-6ffe6f6bcf56");
                TempData["ResponseMessage"] = "Thank you for filling out the form!";
                var contentService = Services.ContentService;
                var childnode = contentService.Create(model.category, parentId, "responseItem");
                childnode.SetValue("response", model.category);
                contentService.SaveAndPublish(childnode);
                return RedirectToCurrentUmbracoPage();
            }
            else
            {
                ModelState.AddModelError("", "Something goes wrong!");
                return CurrentUmbracoPage();
            }

        }
    }
}
