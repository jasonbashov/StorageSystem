﻿namespace AAS.Web.Controllers
{
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
    }
}