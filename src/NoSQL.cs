using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace lagginDragon
{
    public class NoSQL
    {
        // this will hold all functions/facades for db handling; pretty much so that users can draw from internal funcs instead of reffing the 3rd party lib directly.

        public void Insert<Tclass>(string dbPath, string collectionName, Tclass classInstance)
        {
            // Open database (or create if doesn't exist)
            using (var db = new LiteDatabase(dbPath))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Tclass>(collectionName);
                // Insert new customer document (Id will be auto-incremented)
                col.Insert(classInstance);
            }
        }
    }
}
