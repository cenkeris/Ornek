using CenkEris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CenkEris
{
    public class SaveRecords
    {

        
        
        public void Save(string operationtype,string request,string response)
        {
            Context db = new Context();
            Logs newlog = new Logs();
            newlog.OperationType = operationtype;
            newlog.Request = request;
            newlog.Response = response;
            newlog.datetime = DateTime.Now;
            db.Logs.Add(newlog);
            db.SaveChanges();
        }
    }
}
