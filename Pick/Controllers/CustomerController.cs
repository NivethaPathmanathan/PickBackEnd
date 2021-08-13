using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Pick.Models;

namespace Pick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            String query = @"select Id, Name, Email, Complaint, StatusId from dbo.Customer";
            DataTable table = new DataTable();
            String SqlDataSource = _configuration.GetConnectionString("ComplaintAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult (table);
        }

        [HttpPost]
        public JsonResult Post(Customer cust)
        {
            string query = @"insert into dbo.Customer values('" + cust.Name + @"',
                                                             '" + cust.Email + @"',
                                                             '" + cust.Complaint + @"')";
                                                            
            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("ComplaintAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Successfully Added!!");
        }

        [HttpPut]
        public JsonResult Put(Customer cust)
        {
            string query = @"
                             update dbo.Status set
                             StatusId='" + cust.StatusId + @"',
                             where Id = " + cust.Id + @"
                               ";
            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("ComplaintAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Successfully updated!!");
        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"
                            delete from dbo.Customer
                            where " + Id + @"
                            ";
            DataTable table = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("ComplaintAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Successfully Deleted!!");
        }
    }
}
