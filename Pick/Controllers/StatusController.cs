using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pick.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Pick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StatusController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            String query = @"select StatusId, StatusName from dbo.Status";
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

            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult Put(Status sts)
        {
            String query = @"
                             update dbo.Status set
                             StatusName ='" + sts.StatusName + @"'
                             Where StatusId = " + sts.StatusId + @"
                               ";
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

            return new JsonResult("Successfully Updated!!");
        }
    }
}
