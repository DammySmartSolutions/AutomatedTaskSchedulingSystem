using AutomatedTaskSchedulingSystem.BusinessLogic.Services.Interfaces;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class AddEmployee
    {


        ATSSUtilityClass Utility = new ATSSUtilityClass();
        
        private readonly ATSSEntities _db;
        private readonly IFileService _fileService;
        private readonly IDatabaseService _dbService;
        private readonly string _connectionString;

        





        public AddEmployee(ATSSEntities db = null, IFileService fileService = null, IDatabaseService dbService = null, string connectionString = null)
        {
            _db = db ?? new ATSSEntities();
          

          

            _fileService = fileService ?? new FileService();

            _dbService = dbService ?? new DatabaseService(_db);

         

            



            _connectionString = !string.IsNullOrEmpty(connectionString)? connectionString: ConfigurationManager.ConnectionStrings["SqlConnection"]?.ConnectionString ?? "Fake_Connection_String";

   


        }
      









        public string SaveEmployee(tblEmployee model)
        {


            //if (model == null)
            //    throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrWhiteSpace(model.EmpID))
                return "invalid";


            try
            {
                var existing = _db.tblEmployee.FirstOrDefault(x => x.EmpID == model.EmpID);

                if (existing == null)
                {
                    _db.tblEmployee.Add(model);
                    _db.SaveChanges();

                    

                    return "created";
                }
                else
                {
                    existing.FirstName = model.FirstName;
                    existing.LastName = model.LastName;
                    existing.Position = model.Position;
                    existing.Sex = model.Sex;



                    _db.SaveChanges();
                    

                    return "updated";
                }
            }




            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"Property: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                    }
                }
                throw; // Optional: rethrow if you want the error to bubble up
            }



        }






        //public string UploadEmployee(string excelPath)
        //{




        //    if (!_fileService.Exists(excelPath)) // ✅ CORRECT
        //        return "error: File not found.";


        //    string csvData = _fileService.ReadAllText(excelPath); // ✅





        //    try
        //    {
        //        // Clear previous temporary data
        //       // _db.Database.ExecuteSqlCommand("DELETE FROM tblEmployeeeTemp");

        //        _dbService.ExecuteSql("DELETE FROM tblEmployeeeTemp");
        //        _db.SaveChanges();

        //        // Read CSV content


        //        //if (!File.Exists(excelPath))
        //        //    return "error: File not found.";


        //       //  csvData = File.ReadAllText(excelPath);



        //        string newCsvData = csvData.Substring(csvData.IndexOf(Environment.NewLine) + Environment.NewLine.Length);

        //        DataTable dt = new DataTable();
        //        dt.Columns.AddRange(new DataColumn[]
        //            {
        //                    new DataColumn("EmpID", typeof(string)),
        //                    new DataColumn("FirstName", typeof(string)),
        //                    new DataColumn("LastName", typeof(string)), 
        //                    new DataColumn("Sex", typeof(string)),
        //                    new DataColumn("Position", typeof(string))
        //        });

        //        foreach (string line in newCsvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            var cols = line.Split(',');
        //            if (cols.Length != 5)
        //                continue; // Skip invalid row

        //            DataRow row = dt.NewRow();
        //            for (int i = 0; i < 5; i++)
        //                row[i] = cols[i].Trim(); // Trim for safety

        //            dt.Rows.Add(row);
        //        }

        //        // Upload to database
        //        string connStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        //        using (SqlConnection con = new SqlConnection(connStr))
        //        using (SqlBulkCopy bulk = new SqlBulkCopy(con))
        //        {
        //            bulk.DestinationTableName = "dbo.tblEmployeeeTemp";
        //            bulk.ColumnMappings.Add("EmpID", "EmpID");
        //            bulk.ColumnMappings.Add("FirstName", "FirstName");
        //            bulk.ColumnMappings.Add("LastName", "LastName");
        //            bulk.ColumnMappings.Add("Sex", "Sex");
        //            bulk.ColumnMappings.Add("Position", "Position");

        //            con.Open();
        //            bulk.WriteToServer(dt);
        //        }

        //        return "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"error: {ex.Message}";
        //    }
        //}




        public string UploadEmployee(string excelPath)
        {
            if (!_fileService.Exists(excelPath))
                return "error: File not found.";

            string csvData = _fileService.ReadAllText(excelPath);

            try
            {

               


                _dbService.ExecuteSql("DELETE FROM tblEmployeeeTemp");
                _db.SaveChanges();

                string newCsvData = csvData.Substring(csvData.IndexOf(Environment.NewLine) + Environment.NewLine.Length);

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[]
                {
            new DataColumn("EmpID", typeof(string)),
            new DataColumn("FirstName", typeof(string)),
            new DataColumn("LastName", typeof(string)),
            new DataColumn("Sex", typeof(string)),
            new DataColumn("Position", typeof(string))
                });

                foreach (string line in newCsvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var cols = line.Split(',');
                    if (cols.Length != 5)
                        continue;

                    DataRow row = dt.NewRow();
                    for (int i = 0; i < 5; i++)
                        row[i] = cols[i].Trim();

                    dt.Rows.Add(row);
                }

                ///string connStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

                string connStr = _connectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlBulkCopy bulk = new SqlBulkCopy(con))
                {
                    bulk.DestinationTableName = "dbo.tblEmployeeeTemp";
                    bulk.ColumnMappings.Add("EmpID", "EmpID");
                    bulk.ColumnMappings.Add("FirstName", "FirstName");
                    bulk.ColumnMappings.Add("LastName", "LastName");
                    bulk.ColumnMappings.Add("Sex", "Sex");
                    bulk.ColumnMappings.Add("Position", "Position");

                    con.Open();
                    bulk.WriteToServer(dt);
                }

                return "success";
            }
            catch (Exception ex)
            {
                return $"error: {ex.Message}";
            }
        }













        public string SaveUploadEmployee()
        {
            try
            {
                var tempRecords = _db.tblEmployeeeTemp.ToList();

                if (tempRecords == null || !tempRecords.Any())
                    return "No records found in temp table.";

                foreach (var item in tempRecords)
                {
                    if (string.IsNullOrWhiteSpace(item.EmpID)) continue;

                    // Check if employee already exists to avoid duplicates
                    bool exists = _db.tblEmployee.Any(e => e.EmpID == item.EmpID.Trim());
                    if (exists)
                        continue;

                    var employee = new tblEmployee
                    {
                        EmpID = Utility.ToSentenceCase(item.EmpID.Trim()),
                        FirstName = Utility.ToSentenceCase(item.FirstName.Trim()),
                        LastName = Utility.ToSentenceCase(item.LastName.Trim()),
                        FullName = Utility.ToSentenceCase($"{item.FirstName.Trim()} {item.LastName.Trim()}"),
                        Sex = Utility.ToSentenceCase(item.Sex?.Trim() ?? ""),
                        Position = Utility.ToSentenceCase(item.Position?.Trim() ?? "")
                    };

                    _db.tblEmployee.Add(employee);
                }

                _db.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return "SaveUploadEmployee error: " + ex.Message;
            }
        }
















    }
}
