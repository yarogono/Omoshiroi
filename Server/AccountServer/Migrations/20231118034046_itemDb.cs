using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AccountServer.Migrations
{
    /// <inheritdoc />
    public partial class itemDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Player_Stat_PlayerStatId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Player_PlayerStatId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LoginType",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "OauthToken",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PlayerStatId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "PlayerName",
                table: "Player",
                newName: "Nickname");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Player_Stat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    Diamond = table.Column<int>(type: "int", nullable: false),
                    PlayerId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                    table.ForeignKey(
                        name: "FK_Currency_Player_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    GuestUid = table.Column<string>(type: "longtext", nullable: false),
                    PlayerId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.GuestId);
                    table.ForeignKey(
                        name: "FK_Guest_Player_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventory_Player_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Oauth",
                columns: table => new
                {
                    OauthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OauthToken = table.Column<string>(type: "longtext", nullable: false),
                    oatuhType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PlayerId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oauth", x => x.OauthId);
                    table.ForeignKey(
                        name: "FK_Oauth_Player_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaterialItem",
                columns: table => new
                {
                    MaterialItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    InventoryId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialItem", x => x.MaterialItemId);
                    table.ForeignKey(
                        name: "FK_MaterialItem_Inventory_InventoryId1",
                        column: x => x.InventoryId1,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PotionItem",
                columns: table => new
                {
                    PotionItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    InventoryId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotionItem", x => x.PotionItemId);
                    table.ForeignKey(
                        name: "FK_PotionItem_Inventory_InventoryId1",
                        column: x => x.InventoryId1,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RuneItem",
                columns: table => new
                {
                    RuneItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Equipped = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InventoryId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuneItem", x => x.RuneItemId);
                    table.ForeignKey(
                        name: "FK_RuneItem_Inventory_InventoryId1",
                        column: x => x.InventoryId1,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WeaponItem",
                columns: table => new
                {
                    WeaponItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Equipped = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InventoryId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponItem", x => x.WeaponItemId);
                    table.ForeignKey(
                        name: "FK_WeaponItem_Inventory_InventoryId1",
                        column: x => x.InventoryId1,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Stat_PlayerId",
                table: "Player_Stat",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_CurrencyId",
                table: "Currency",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_PlayerId1",
                table: "Currency",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Guest_GuestId",
                table: "Guest",
                column: "GuestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guest_PlayerId1",
                table: "Guest",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_InventoryId",
                table: "Inventory",
                column: "InventoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PlayerId1",
                table: "Inventory",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialItem_InventoryId1",
                table: "MaterialItem",
                column: "InventoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialItem_MaterialItemId",
                table: "MaterialItem",
                column: "MaterialItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oauth_OauthId",
                table: "Oauth",
                column: "OauthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oauth_PlayerId1",
                table: "Oauth",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_PotionItem_InventoryId1",
                table: "PotionItem",
                column: "InventoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_PotionItem_PotionItemId",
                table: "PotionItem",
                column: "PotionItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RuneItem_InventoryId1",
                table: "RuneItem",
                column: "InventoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponItem_InventoryId1",
                table: "WeaponItem",
                column: "InventoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Stat_Player_PlayerId",
                table: "Player_Stat",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Stat_Player_PlayerId",
                table: "Player_Stat");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "MaterialItem");

            migrationBuilder.DropTable(
                name: "Oauth");

            migrationBuilder.DropTable(
                name: "PotionItem");

            migrationBuilder.DropTable(
                name: "RuneItem");

            migrationBuilder.DropTable(
                name: "WeaponItem");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Player_Stat_PlayerId",
                table: "Player_Stat");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Player_Stat");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Player",
                newName: "PlayerName");

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

            migrationBuilder.AddColumn<int>(
                name: "PlayerStatId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    ItemName = table.Column<string>(type: "longtext", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerStatId",
                table: "Player",
                column: "PlayerStatId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemId",
                table: "Item",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlayerId",
                table: "Item",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Player_Stat_PlayerStatId",
                table: "Player",
                column: "PlayerStatId",
                principalTable: "Player_Stat",
                principalColumn: "PlayerStatId");
        }
    }
}
