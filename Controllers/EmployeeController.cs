using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapicrud.Models;

namespace webapicrud.Controllers
{
    public class EmployeeController : ApiController
    {
        CurdReactWebAPIEntities db = new CurdReactWebAPIEntities();

        [Route("api/Employee/AddEmployee")]
        [HttpPost]
        public Response AddEmployee(EmployeeModel employeeModel)
        {
            System.Net.Http.Headers.HttpHeaders headers = Request.Headers;
            Response response = new Response();
            Employee employee = new Employee();

            employee.Id = employeeModel.Id;
            employee.FirstName = employeeModel.FirstName;
            employee.LastName = employeeModel.LastName;
            employee.Mobile = employeeModel.Mobile;
            employee.Email = employeeModel.Email;

            // Add Employee
            if (employeeModel.Type == "Add")
            {
                if(employee != null)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    response.ResponseCode = "200";
                    response.ResponseMessage = "Employee added";
                }
                else
                {
                    response.ResponseCode = "100";
                    response.ResponseMessage = "Some error occured";
                }
            }

            // Update Employee
            if (employeeModel.Type == "Update")
            {
                if (employee != null)
                {
                    db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    response.ResponseCode = "200";
                    response.ResponseMessage = "Employee updated";
                }
                else
                {
                    response.ResponseCode = "100";
                    response.ResponseMessage = "Some error occured";
                }
            }

            // Delete Employee
            if (employeeModel.Type == "Delete")
            {
                if (employee != null)
                {
                    db.Entry(employee).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    response.ResponseCode = "200";
                    response.ResponseMessage = "Employee deleted";
                }
                else
                {
                    response.ResponseCode = "100";
                    response.ResponseMessage = "Some error occured";
                }
            }

            return response;
        }
        
        [Route("api/Employee/GetEmployees")]
        [HttpGet]
        public Response GetEmployees()
        {
            Response response = new Response();
            List<Employee> lstEmployees = new List<Employee>();
            lstEmployees = db.Employees.ToList();
            response.ResponseCode = "200";
            response.ResponseMessage = "Data fetched";
            response.lstEmployees = lstEmployees;

            return response;
        }

        [Route("api/Employee/EmployeeById")]
        [HttpPost]
        public Response EmployeeById(EmployeeModel employeeModel)
        {
            Response response = new Response();
            Employee employee = new Employee();
            if(employeeModel != null && employeeModel.Id > 0)
            {
                employee = db.Employees.FirstOrDefault(x => x.Id == employeeModel.Id);
                response.employee = employee;
                response.ResponseCode = "200";
                response.ResponseMessage = "Data fetched";
            }

            return response;
        }
    }
}
