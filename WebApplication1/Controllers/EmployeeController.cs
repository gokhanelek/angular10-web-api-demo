using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Web;

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select EmployeeId,EmployeeName,Department,
                    convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                    PhotoFileName
                    from 
                    dbo.Employee
                   ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager
                .ConnectionStrings["EmployeeAppDb"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }

        public string Post(Employee em)
        {
            try
            {
                string query = @"
                       insert into dbo.Employee values
                       ('" + em.EmployeeName + @"'
                       ,'" + em.Department + @"'
                       ,'" + em.DateOfJoining + @"'
                       ,'" + em.PhotoFileName + @"'
                        )
                        ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager
                    .ConnectionStrings["EmployeeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Post Successfully!!";
            }
            catch
            {
                return "Failed to Post!!";
            }
        }

        public string Put(Employee em)
        {
            try
            {
                string query = @"
                       update dbo.Employee set 
                       EmployeeName='" + em.EmployeeName + @"'
                       ,Department='" + em.Department + @"'
                       ,DateOfJoining='" + em.DateOfJoining + @"'     
                       ,PhotoFileName='" + em.PhotoFileName + @"'
                       where EmployeeId=" + em.EmployeeId + @"
                        ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager
                    .ConnectionStrings["EmployeeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Put Successfully!!";
            }
            catch
            {
                return "Failed to Put!!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                       delete from dbo.Employee 
                       where EmployeeId=" + id + @"
                        ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager
                    .ConnectionStrings["EmployeeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Deleted Successfully!!";
            }
            catch
            {
                return "Failed to Delete!!";
            }
        }

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentnames()
        {
            string query = @"
                    select DepartmentName
                    from 
                    dbo.Department
                   ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager
                .ConnectionStrings["EmployeeAppDb"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
