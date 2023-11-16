using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AccountServer.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Account_AccountDbAccountId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_AccountDbAccountId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Account_AccountName",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "AccountDbAccountId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Item",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Account",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Player_Stat",
                columns: table => new
                {
                    PlayerStatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MaxHp = table.Column<int>(type: "int", nullable: false),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    Atk = table.Column<int>(type: "int", nullable: false),
                    AtkSpeed = table.Column<float>(type: "float", nullable: false),
                    CritRate = table.Column<int>(type: "int", nullable: false),
                    CritDamage = table.Column<float>(type: "float", nullable: false),
                    MoveSpeed = table.Column<float>(type: "float", nullable: false),
                    RunMultiplier = table.Column<float>(type: "float", nullable: false),
                    DodgeTime = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Stat", x => x.PlayerStatId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerName = table.Column<string>(type: "longtext", nullable: false),
                    PlayerStatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Player_Stat_PlayerStatId",
                        column: x => x.PlayerStatId,
                        principalTable: "Player_Stat",
                        principalColumn: "PlayerStatId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlayerId",
                table: "Item",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountId",
                table: "Account",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_PlayerId",
                table: "Account",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerId",
                table: "Player",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerStatId",
                table: "Player",
                column: "PlayerStatId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Stat_PlayerStatId",
                table: "Player_Stat",
                column: "PlayerStatId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Player_PlayerId",
                table: "Account",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Player_PlayerId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Player_PlayerId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Player_Stat");

            migrationBuilder.DropIndex(
                name: "IX_Item_PlayerId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Account_AccountId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_PlayerId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Account");

            migrationBuilder.AddColumn<int>(
                name: "AccountDbAccountId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Account",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateIndex(
                name: "IX_Item_AccountDbAccountId",
                table: "Item",
                column: "AccountDbAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountName",
                table: "Account",
                column: "AccountName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Account_AccountDbAccountId",
                table: "Item",
                column: "AccountDbAccountId",
                principalTable: "Account",
                principalColumn: "AccountId");
        }
    }
}
