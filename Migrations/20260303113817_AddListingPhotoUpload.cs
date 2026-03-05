using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShopFinal.Migrations
{
    /// <inheritdoc />
    public partial class AddListingPhotoUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarImage",
                table: "Listings");

            migrationBuilder.AddColumn<List<string>>(
                name: "CarImages",
                table: "Listings",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarImages",
                table: "Listings");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "CarImage",
                table: "Listings",
                type: "uuid[]",
                nullable: false);
        }
    }
}
