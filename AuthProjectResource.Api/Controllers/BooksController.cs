using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProjectResource.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthProjectResource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStore store;

        public BooksController(BookStore store)
        {
            this.store = store;
        }
        [HttpGet]
        [Route("")]
        public IActionResult GetAvailableBooks()
        {
            return Ok(store.Books);
        }
    }
}
