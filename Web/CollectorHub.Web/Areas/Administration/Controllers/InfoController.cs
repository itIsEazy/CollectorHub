namespace CollectorHub.Web.Areas.Administration.Controllers
{
    using CollectorHub.Services.Data.Administration;
    using CollectorHub.Web.ViewModels.Administration;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class InfoController : AdministrationController
    {
        private readonly IAdministrationService administrationService;

        public InfoController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        [Authorize]
        public IActionResult BecomeAdmin()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult BecomeAdmin(BecomeAdminViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.administrationService.AddNewAdmin(this.User.Identity.Name, input.UniquePassword).Result)
            {
                return this.Redirect("https://localhost:5001/");
            }
            else
            {
                return this.View(input);
            }
        }
    }
}
