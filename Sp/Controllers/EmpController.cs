using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Sp.Model;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;
using System;

namespace Sp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : Controller
    {
        private IConfiguration config;
        public EmpController(IConfiguration config)
        {
            this.config = config;
        }
       

        
        [HttpPost]
        public async Task<IActionResult> post(employee emp)
        {
            //var conntionString = config.GetConnectionString("Con");
            //var conn = new SqlConnection(conntionString);
            var conn =new SqlConnection(config.GetConnectionString("Con"));
            try
            {

            
            if (emp != null)
            {
                await conn.OpenAsync(); 
                SqlCommand cmd = new SqlCommand("AddNewEmp", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", emp.name);
                cmd.Parameters.AddWithValue("@Email", emp.Email);

                await cmd.ExecuteNonQueryAsync();
                   

            }
                return Ok("record created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
