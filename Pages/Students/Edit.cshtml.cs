using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Library.Pages.Students
{
    public class EditModel : PageModel
    {
        public StudentRecord studentRecord = new StudentRecord();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string StudentNumber = Request.Query["StudentNumber"];

            try
            {
                string connectionString = "Data Source=st10356476studentsrecords.database.windows.net;Initial Catalog=studentsRecords;User ID=AdminSQL-studentsRecords;Password=Munyai@1;\r\n";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM StudentsRecords WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StudentNumber", StudentNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentRecord.ID = reader.GetInt32(0);
                                studentRecord.BookTitle = reader.GetString(1);
                                studentRecord.AuthorName = reader.GetString(2);
                                studentRecord.StudentNumber = reader.GetString(3);
                                studentRecord.CellNumber = reader.GetString(4);
                                studentRecord.ParentNumber = reader.GetString(5);
                                studentRecord.HomeAddress = reader.GetString(6);
                                studentRecord.CampusName = reader.GetString(7);
                            }
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            studentRecord.BookTitle = Request.Form["BookTitle"];
            studentRecord.AuthorName = Request.Form["AuthorName"];
            studentRecord.StudentNumber = Request.Form["StudentNumber"];
            studentRecord.CellNumber = Request.Form["CellNumber"];
            studentRecord.ParentNumber = Request.Form["ParentNumber"];
            studentRecord.HomeAddress = Request.Form["HomeAddress"];
            studentRecord.CampusName = Request.Form["CampusName"];


            if (studentRecord.BookTitle.Length == 0 || studentRecord.AuthorName.Length == 0 || studentRecord.StudentNumber.Length == 0 ||
                studentRecord.CellNumber.Length == 0 || studentRecord.ParentNumber.Length == 0 || studentRecord.HomeAddress.Length == 0 ||
                studentRecord.CampusName.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            try
            {
                string connectionString = "Data Source=st10356476studentsrecords.database.windows.net;Initial Catalog=studentsRecords;User ID=AdminSQL-studentsRecords;Password=Munyai@1;\r\n";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE StudentsRecords " +
                                 "SET BookTitle=@BookTitle, AuthorName=@AuthorName, StudentNumber=@StudentNumber, CellNumber=@CellNumber, ParentNumber=@ParentNumber, HomeAddress=@HomeAddress, CampusName=@CampusName" +
                                 "WHERE StudentNumber=@StudentNumber";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@BookTitle", studentRecord.BookTitle);
                        command.Parameters.AddWithValue("@AuthorName", studentRecord.AuthorName);
                        command.Parameters.AddWithValue("@StudentNumber", studentRecord.StudentNumber);
                        command.Parameters.AddWithValue("@CellNumber", studentRecord.CellNumber);
                        command.Parameters.AddWithValue("@ParentNumber", studentRecord.ParentNumber);
                        command.Parameters.AddWithValue("@HomeAddress", studentRecord.HomeAddress);
                        command.Parameters.AddWithValue("@CampusName", studentRecord.CampusName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("StudentsRecords");
        }
    }
}
