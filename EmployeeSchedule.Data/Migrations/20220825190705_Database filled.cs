using EmployeeSchedule.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSchedule.Data.Migrations
{
    public partial class Databasefilled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "CompanyDomain",
              columns: new[] { "Id", "Name" },
              values: new object[,] {
                    {1, "Finance" },
                    {2, "Pharmacy" },
                    {3, "ICT" },
                    {4, "Commerce" },
                    {5, "Logistics" },
              }
              );


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.Sql("DELETE FROM CompanyDomain", true);

        }
    }
}
