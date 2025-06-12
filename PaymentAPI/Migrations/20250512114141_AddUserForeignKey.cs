using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "employee_id",
                table: "PaymentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "employee_id1",
                table: "PaymentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "PaymentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_employee_id1",
                table: "PaymentDetails",
                column: "employee_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Employees_employee_id1",
                table: "PaymentDetails",
                column: "employee_id1",
                principalTable: "Employees",
                principalColumn: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Employees_employee_id1",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_employee_id1",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "employee_id",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "employee_id1",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "PaymentDetails");
        }
    }
}
