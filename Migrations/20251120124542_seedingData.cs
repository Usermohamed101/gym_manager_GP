using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin", null, "Admin", "ADMIN" },
                    { "role-member", null, "Member", "MEMBER" },
                    { "role-trainer", null, "Trainer", "TRAINER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImageUrl", "SecName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "member-1", 0, null, "b7820284-7c12-4f5e-ba29-3a009d7c30ee", "Member", "member1@gym.com", true, "Mohamed", false, null, "MEMBER1@GYM.COM", "MEMBER1@GYM.COM", "AQAAAAIAAYagAAAAENQef26N56Zmd78XgaK6cezeI4jNxhiEIU5tdV1C2OHlapzDX4MKa1/zzfv3r2H9pA==", null, false, null, "Ibrahim", "8f15e65f-9fe1-41ad-ab08-ff608cc1f508", false, "member1@gym.com" },
                    { "trainer-1", 0, null, "02e53003-c5e3-43e1-9885-a741f3fe7838", "Trainer", "trainer1@gym.com", true, "Mohamed", false, null, "TRAINER1@GYM.COM", "TRAINER1@GYM.COM", "AQAAAAIAAYagAAAAEC4C8iYL7i1r+Fa9h176DQBSv9YHKx7KDndgyZyUXUHKSYFupmufEn/v1v6we2VYdg==", null, false, null, "Mohsen", "5a25f05f-94ab-4e4f-a917-f967e3cf44af", false, "trainer1@gym.com" },
                    { "user-admin-1", 0, null, "0706dde9-b5d0-4e5c-b41e-1a358205b6fd", "User", "admin@gym.com", true, "Admin", false, null, "ADMIN@GYM.COM", "ADMIN@GYM.COM", "AQAAAAIAAYagAAAAEFvx1hbDklkGCPVoTw9wDOxgUdaTunvJUKJ4E1n94G6qjSNAoYsE3RofvxP6cf6+7g==", null, false, null, "Admin", "b9be71aa-eae6-4b17-b807-ab0b789041f2", false, "admin@gym.com" }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "DurationInDays", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 30, "Basic", 20m },
                    { 2, 90, "Standard", 50m },
                    { 3, 365, "Premium", 150m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-member", "member-1" },
                    { "role-trainer", "trainer-1" },
                    { "role-admin", "user-admin-1" }
                });

            migrationBuilder.InsertData(
                table: "MemberMemberships",
                columns: new[] { "MemberId", "MembershipId", "EndDate", "IsActive", "StartDate" },
                values: new object[] { "member-1", 1, new DateTime(2025, 12, 20, 12, 45, 40, 862, DateTimeKind.Utc).AddTicks(9216), true, new DateTime(2025, 11, 20, 12, 45, 40, 862, DateTimeKind.Utc).AddTicks(9208) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-member", "member-1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-trainer", "trainer-1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-admin", "user-admin-1" });

            migrationBuilder.DeleteData(
                table: "MemberMemberships",
                keyColumns: new[] { "MemberId", "MembershipId" },
                keyValues: new object[] { "member-1", 1 });

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-member");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-trainer");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "member-1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "trainer-1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-1");

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
