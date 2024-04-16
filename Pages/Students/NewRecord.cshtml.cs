using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Library.Pages.Students
{
    public class NewRecordModel : PageModel
    {
        public StudentRecord studentRecord = new StudentRecord();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
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
                studentRecord.CellNumber.Length == 0 || studentRecord.ParentNumber.Length == 0|| studentRecord.HomeAddress.Length == 0 ||
                studentRecord.CampusName.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            try
            {
                string connectionString = "[Insert Connection String]";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO StudentsRecords (BookTitle, AuthorName, StudentNumber, CellNumber, ParentNumber, HomeAddress, CampusName) " +
             "SELECT @BookTitle, @AuthorName, @StudentNumber, @CellNumber, @ParentNumber, @HomeAddress, @CampusName " +
             "WHERE NOT EXISTS (SELECT 1 FROM StudentsRecords WHERE StudentNumber = @StudentNumber);";


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
            //Save the new record into the database
            studentRecord.BookTitle = ""; studentRecord.AuthorName = ""; studentRecord.StudentNumber = ""; studentRecord.CellNumber = ""; studentRecord.ParentNumber = ""; studentRecord.HomeAddress = ""; studentRecord.CampusName = "";
            successMessage = "New Record Added Successfully";

            Response.Redirect("StudentsRecords");
        }
    }
}
