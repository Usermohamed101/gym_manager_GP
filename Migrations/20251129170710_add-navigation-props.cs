using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class addnavigationprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_MemberId1",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_MemberId1",
                table: "TrainingPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_TrainerId1",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_MemberId1",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_TrainerId1",
                table: "TrainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_MemberId1",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "TrainerId1",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "NextRenewalDate",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "TrainingPlans",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "TrainingPlans",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MemberTrainer",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrainersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTrainer", x => new { x.MembersId, x.TrainersId });
                    table.ForeignKey(
                        name: "FK_MemberTrainer_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemberTrainer_AspNetUsers_TrainersId",
                        column: x => x.TrainersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "member-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c5e01b9-3146-45f6-9bca-c5de76d3dfdb", "AQAAAAIAAYagAAAAELq/9Cd/jcFT1l2xOgvNNbdiRIKImPLe2FP5U4ovZJg/t1+iKUMnhTNpsPqBJD6Ndw==", "db1c1f53-4d82-4d7c-b614-8f24cf513b92" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "trainer-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3a79594-3caa-401a-92a5-cd5d74eb0c4b", "AQAAAAIAAYagAAAAEKgpVFZ3+1IVbAL82rUa7FJhMTIHtSE978Awuek5Ii54nNPfo+e+8IxjqCTxLTfogA==", "94be2e09-ba59-4fbe-a464-773b63e78d2d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8ceaba6-6a52-4c50-93a3-c581faeaf5f3", "AQAAAAIAAYagAAAAEHNo1mTs6iEVDwp2g4RUPocmFJfnaCgBvFwrRac+TEqL5sRGmwxaE8v/WizOv53Wcg==", "c4338bb8-9c3b-4ff2-9245-36f7a9b2e22e" });

            migrationBuilder.UpdateData(
                table: "MemberMemberships",
                keyColumns: new[] { "MemberId", "MembershipId" },
                keyValues: new object[] { "member-1", 1 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 7, 8, 905, DateTimeKind.Utc).AddTicks(4861), new DateTime(2025, 11, 29, 17, 7, 8, 905, DateTimeKind.Utc).AddTicks(4855) });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_MemberId",
                table: "TrainingPlans",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_TrainerId",
                table: "TrainingPlans",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MemberId",
                table: "Payments",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MembershipId",
                table: "Payments",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTrainer_TrainersId",
                table: "MemberTrainer",
                column: "TrainersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_MemberId",
                table: "Payments",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Memberships_MembershipId",
                table: "Payments",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_MemberId",
                table: "TrainingPlans",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_TrainerId",
                table: "TrainingPlans",
                column: "TrainerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_MemberId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Memberships_MembershipId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_MemberId",
                table: "TrainingPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_TrainerId",
                table: "TrainingPlans");

            migrationBuilder.DropTable(
                name: "MemberTrainer");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_MemberId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_TrainerId",
                table: "TrainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_MemberId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_MembershipId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.AlterColumn<int>(
                name: "TrainerId",
                table: "TrainingPlans",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "TrainingPlans",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "MemberId1",
                table: "TrainingPlans",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainerId1",
                table: "TrainingPlans",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Payment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "MemberId1",
                table: "Payment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextRenewalDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "member-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7820284-7c12-4f5e-ba29-3a009d7c30ee", "AQAAAAIAAYagAAAAENQef26N56Zmd78XgaK6cezeI4jNxhiEIU5tdV1C2OHlapzDX4MKa1/zzfv3r2H9pA==", "8f15e65f-9fe1-41ad-ab08-ff608cc1f508" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "trainer-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02e53003-c5e3-43e1-9885-a741f3fe7838", "AQAAAAIAAYagAAAAEC4C8iYL7i1r+Fa9h176DQBSv9YHKx7KDndgyZyUXUHKSYFupmufEn/v1v6we2VYdg==", "5a25f05f-94ab-4e4f-a917-f967e3cf44af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin-1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0706dde9-b5d0-4e5c-b41e-1a358205b6fd", "AQAAAAIAAYagAAAAEFvx1hbDklkGCPVoTw9wDOxgUdaTunvJUKJ4E1n94G6qjSNAoYsE3RofvxP6cf6+7g==", "b9be71aa-eae6-4b17-b807-ab0b789041f2" });

            migrationBuilder.UpdateData(
                table: "MemberMemberships",
                keyColumns: new[] { "MemberId", "MembershipId" },
                keyValues: new object[] { "member-1", 1 },
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 20, 12, 45, 40, 862, DateTimeKind.Utc).AddTicks(9216), new DateTime(2025, 11, 20, 12, 45, 40, 862, DateTimeKind.Utc).AddTicks(9208) });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_MemberId1",
                table: "TrainingPlans",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_TrainerId1",
                table: "TrainingPlans",
                column: "TrainerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_MemberId1",
                table: "Payment",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_MemberId1",
                table: "Payment",
                column: "MemberId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_MemberId1",
                table: "TrainingPlans",
                column: "MemberId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_TrainerId1",
                table: "TrainingPlans",
                column: "TrainerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
