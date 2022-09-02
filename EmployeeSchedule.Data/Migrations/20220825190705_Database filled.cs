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

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Name", "IdentificationNumber", "Adress", "Domain" },
                values: new object[,] {
                    {"Banca Intesa", "2536367373", "Vladimira Popovica 10", 1 },
                    {"Roche", "145374934", "Vladimira Popovica 8", 2 },
                    {"A1", "2536367373", "Bulevar Umetnosti 35", 3 }
                }
                );



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CompanyDomain", true);
            migrationBuilder.Sql("DELETE FROM Company", true);

        }
    }
}
