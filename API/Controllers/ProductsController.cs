using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts() {
            return "List of Products";
        }
        [HttpGet("{id}")]
        public string GetProduct() 
        { 
            return "single Product";
        }
    }
}