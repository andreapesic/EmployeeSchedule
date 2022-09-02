using EmployeeSchedule.Data.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeSchedule.Data.Migrations
{
    public partial class Databasefilled3 : Migration
    {
        private string key = "b14ca5898a4e4133bbce2ea2315a1916";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            string p1 = AesOperation.EncryptString(key, "andrea");
            string p2 = AesOperation.EncryptString(key, "majapet");
            string p3 = AesOperation.EncryptString(key, "ivapet");
            string p4 = AesOperation.EncryptString(key, "radaobu");

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Name", "Surname", "Adress", "Number", "Email", "Password", "Possition", "CompanyId", "Administrator", "CityName" },
                values: new object[,] {
                    {"Andrea", "Pesic", "Miluna Ivanovica 2", "0692108099", "deapesic@live.com", p1, "Digital team member", 2, true, "Kraljevo" },
                    {"Maja", "Petrovic", "Stevana Sremca 53", "066545545", "petrovic.maja99@gmail.com", p2, "Digital team member", 2, false, "Pirot" },
                    {"Iva", "Petkovic", "Omladinska 9", "0603366920", "ivaa99petkovic@gmail.com", p3, "BI Department Intern", 3, false, "Zajecar" },
                    {"Rada", "Obucina", "Vilovi bb", "0603364296", "radaobucina@gmail.com", p4, "BI Department Intern", 3, false, "Nova Varos" },
                }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Employee", true);
        }
    }
}
