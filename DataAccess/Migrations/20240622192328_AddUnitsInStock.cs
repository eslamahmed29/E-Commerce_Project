﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Test.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitsInStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitsInStock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsInStock",
                table: "Products");
        }
    }
}
