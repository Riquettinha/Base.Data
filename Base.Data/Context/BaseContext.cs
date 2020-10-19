﻿using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Extensions.Configuration;
using Base.Data.Programmability.Functions;
using Base.Data.Programmability.Stored_Procedures;

namespace Base.Data.Context
{
    public partial class BaseContext : DbContext, IBaseContext
    {
        public Guid UserId;
        public const string ContextName = "BaseContext";

        public BaseContext(DbConnection connection) : base(connection, true)
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public BaseContext() : base(GetConnectionString(ContextName))
        {
            StoredProcedures = new StoredProcedures(this);
            ScalarValuedFunctions = new ScalarValuedFunctions(this);
            TableValuedFunctions = new TableValuedFunctions(this);
        }

        public static string GetConnectionString(string connectionString)
        {
            var defaultConnection = ConfigurationManager.ConnectionStrings[connectionString]?.ConnectionString;
            var connection = defaultConnection ?? connectionString;

            return connection;
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}



