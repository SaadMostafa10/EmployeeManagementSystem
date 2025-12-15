using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeTech.EMS.DAL.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreationDateToDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CreationDate",
                table: "Departments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Departments");
        }
    }
}
