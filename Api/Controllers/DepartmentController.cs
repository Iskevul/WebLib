using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return DataAccess.GetAllDepartments();
        }
        [HttpGet("{id}")]
        public ActionResult<Department> Get(int id)
        {
            var result = DataAccess.GetDepartment(id);
            if (result == null)
                return NotFound();

            return result;
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            DataAccess.AddDepartment(department);
            return NoContent();
        }
    }
}