//using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace WebApplication1.Models
{
    [Table("Employee_Master")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be atleast {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} can not be blank.")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "The {0} must be atleast {2} characters long.", MinimumLength = 12)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} can not be blank.")]
        [StringLength(1000, ErrorMessage = "The {0} must be atleast {2} characters long.", MinimumLength = 20)]
        public string Description { get; set; }
    }
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EmployeeContext : DbContext
    {

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
      // public EmployeeContext() : base("MySQLConnection")
        {
        }
        public DbSet<Employee> listEmployee { get; set; }
        public DbSet<customer> listcustomers { get; set; }
        public DbSet<account> listaccounts { get; set; }
    }



    public class MySqlDatabase : IDisposable
    {
        public MySql.Data.MySqlClient.MySqlConnection Connection;

        public MySqlDatabase(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            this.Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
