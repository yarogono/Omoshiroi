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
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nickname = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    Diamond = table.Column<int>(type: "int", nullable: false),
                    playerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                    table.ForeignKey(
                        name: "FK_Currency_Player_playerId",
                        column: x => x.playerId,
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
                    playerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.GuestId);
                    table.ForeignKey(
                        name: "FK_Guest_Player_playerId",
                        column: x => x.playerId,
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
                    playerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventory_Player_playerId",
                        column: x => x.playerId,
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
                    OauthToken = table.Column<string>(type: "longtext", nullable: true),
                    oatuhType = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    playerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oauth", x => x.OauthId);
                    table.ForeignKey(
                        name: "FK_Oauth_Player_playerId",
                        column: x => x.playerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                    DodgeTime = table.Column<float>(type: "float", nullable: false),
                    playerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Stat", x => x.PlayerStatId);
                    table.ForeignKey(
                        name: "FK_Player_Stat_Player_playerId",
                        column: x => x.playerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Material_Item",
                columns: table => new
                {
                    MaterialItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    inventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material_Item", x => x.MaterialItemId);
                    table.ForeignKey(
                        name: "FK_Material_Item_Inventory_inventoryId",
                        column: x => x.inventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Potion_Item",
                columns: table => new
                {
                    PotionItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    inventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potion_Item", x => x.PotionItemId);
                    table.ForeignKey(
                        name: "FK_Potion_Item_Inventory_inventoryId",
                        column: x => x.inventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rune_Item",
                columns: table => new
                {
                    RuneItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Equipped = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    inventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rune_Item", x => x.RuneItemId);
                    table.ForeignKey(
                        name: "FK_Rune_Item_Inventory_inventoryId",
                        column: x => x.inventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Weapon_Item",
                columns: table => new
                {
                    WeaponItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Equipped = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    inventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon_Item", x => x.WeaponItemId);
                    table.ForeignKey(
                        name: "FK_Weapon_Item_Inventory_inventoryId",
                        column: x => x.inventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_CurrencyId",
                table: "Currency",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_playerId",
                table: "Currency",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_Guest_GuestId",
                table: "Guest",
                column: "GuestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guest_playerId",
                table: "Guest",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_InventoryId",
                table: "Inventory",
                column: "InventoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_playerId",
                table: "Inventory",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Item_inventoryId",
                table: "Material_Item",
                column: "inventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_Item_MaterialItemId",
                table: "Material_Item",
                column: "MaterialItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oauth_OauthId",
                table: "Oauth",
                column: "OauthId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oauth_playerId",
                table: "Oauth",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerId",
                table: "Player",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_Stat_playerId",
                table: "Player_Stat",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Stat_PlayerStatId",
                table: "Player_Stat",
                column: "PlayerStatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Potion_Item_inventoryId",
                table: "Potion_Item",
                column: "inventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Potion_Item_PotionItemId",
                table: "Potion_Item",
                column: "PotionItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rune_Item_inventoryId",
                table: "Rune_Item",
                column: "inventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapon_Item_inventoryId",
                table: "Weapon_Item",
                column: "inventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Material_Item");

            migrationBuilder.DropTable(
                name: "Oauth");

            migrationBuilder.DropTable(
                name: "Player_Stat");

            migrationBuilder.DropTable(
                name: "Potion_Item");

            migrationBuilder.DropTable(
                name: "Rune_Item");

            migrationBuilder.DropTable(
                name: "Weapon_Item");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
