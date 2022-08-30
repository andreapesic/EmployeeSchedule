using EmployeeSchedule.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSchedule.Data.Migrations
{
    public partial class Databasefilled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Name", "IdentificationNumber", "Adress", "Domain" },
                values: new object[,] {
                    {"Banca Intesa", "2536367373", "Vladimira Popovica 10", "Finance" },
                    {"Roche", "145374934", "Vladimira Popovica 8", "Pharmacy" },
                    {"A1", "2536367373", "Bulevar Umetnosti 35", "ICT" }
                }
                );



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Company", true);

        }
    }
}
