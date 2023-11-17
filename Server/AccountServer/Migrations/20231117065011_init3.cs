using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AccountServer.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Player_Stat_PlayerStatId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerStatId",
                table: "Player",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Player",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LoginType",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OauthToken",
                table: "Player",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Player_Stat_PlayerStatId",
                table: "Player",
                column: "PlayerStatId",
                principalTable: "Player_Stat",
                principalColumn: "PlayerStatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Player_Stat_PlayerStatId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LoginType",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "OauthToken",
                table: "Player");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerStatId",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "longtext", nullable: false),
                    AccountPassword = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Account_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountId",
                table: "Account",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_PlayerId",
                table: "Account",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Player_Stat_PlayerStatId",
                table: "Player",
                column: "PlayerStatId",
                principalTable: "Player_Stat",
                principalColumn: "PlayerStatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
