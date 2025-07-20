using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services.Interfaces
{
    public class DatabaseService : IDatabaseService
    {


        private readonly ATSSEntities _db;

        public DatabaseService(ATSSEntities db)
        {
            _db = db;
        }





        //public DatabaseService(ATSSEntities context)
        //{
        //    _context = context;
        //}
        public int ExecuteSql(string sql)
        {
            return _db.Database.ExecuteSqlCommand(sql);
        }


        private readonly ATSSEntities _context;

       


    }
}
