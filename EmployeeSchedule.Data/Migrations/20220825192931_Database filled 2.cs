using EmployeeSchedule.Data.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSchedule.Data.Migrations
{
    public partial class Databasefilled2 : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Name", "IdentificationNumber", "Adress", "DomainId" },
               
                values: new object[,] {
                    {"Banca Intesa", "2536367373", "Vladimira Popovica 10", 1 },
                    {"Roche", "145374934", "Vladimira Popovica 8", 2 },
                    {"A1", "2536367373", "Bulevar Umetnosti 35", 3 }
                }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Company", true);
        }
    }
}
