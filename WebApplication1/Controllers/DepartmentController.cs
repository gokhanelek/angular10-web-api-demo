using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select DepartmentId,DepartmentName from 
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

        public string Post(Department dep)
        {
            try
            {
                string query = @"
                       insert into dbo.Department values
                       ('" + dep.DepartmentName + @"')
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

        public string Put(Department dep)
        {
            try
            {
                string query = @"
                       update dbo.Department set DepartmentName=
                       '" + dep.DepartmentName + @"'
                       where DepartmentId=" + dep.DepartmentId + @"
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
                       delete from dbo.Department 
                       where DepartmentId=" + id + @"
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
    }

}
