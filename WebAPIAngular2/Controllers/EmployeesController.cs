using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIAngular2.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (EmployeeDB1Entities entities = new EmployeeDB1Entities())
            {
                return entities.Employees.ToList();
            }
        }

        public Employee Get(string code)
        {
            using (EmployeeDB1Entities entities = new EmployeeDB1Entities())
            {
                return entities.Employees.FirstOrDefault(s => s.code == code);
            }
        }

    }
}
