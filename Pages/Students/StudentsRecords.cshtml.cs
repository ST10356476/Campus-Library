using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Library.Pages.Students
{
    public class StudentsRecordsModel : PageModel
    {

        public List<StudentRecord> records = new List<StudentRecord>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=st10356476studentsrecords.database.windows.net;Initial Catalog=studentsRecords;User ID=AdminSQL-studentsRecords;Password=Munyai@1;\r\n";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM StudentsRecords";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentRecord studentRecord = new StudentRecord();
                                studentRecord.ID = reader.GetInt32(0);
                                studentRecord.BookTitle = reader.GetString(1);
                                studentRecord.AuthorName = reader.GetString(2);
                                studentRecord.StudentNumber = reader.GetString(3);
                                studentRecord.CellNumber = reader.GetString(4);
                                studentRecord.ParentNumber = reader.GetString(5);
                                studentRecord.HomeAddress = reader.GetString(6);
                                studentRecord.CampusName = reader.GetString(7);

                                records.Add(studentRecord);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

   
}