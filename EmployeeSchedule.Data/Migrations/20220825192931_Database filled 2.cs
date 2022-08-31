using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSchedule.Data.Migrations
{
    public partial class Databasefilled2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Name", "Surname", "Adress", "Number", "Email", "Password", "Possition", "CompanyId", "Administrator", "CityName" },
                values: new object[,] {
                    {"Andrea", "Pesic", "Miluna Ivanovica 2", "0692108099", "deapesic@live.com", "andrea", "Digital team member", 2, true, "Kraljevo" },
                    {"Maja", "Petrovic", "Stevana Sremca 53", "066545545", "petrovic.maja99@gmail.com", "majapet", "Digital team member", 2, false, "Pirot" },
                    {"Iva", "Petkovic", "Omladinska 9", "0603366920", "ivaa99petkovic@gmail.com", "ivapet", "BI Department Intern", 3, false, "Zajecar" },
                    {"Rada", "Obucina", "Vilovi bb", "0603364296", "radaobucina@gmail.com", "radaobu", "BI Department Intern", 3, false, "Nova Varos" },
                }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Employee", true);
        }
    }
}
