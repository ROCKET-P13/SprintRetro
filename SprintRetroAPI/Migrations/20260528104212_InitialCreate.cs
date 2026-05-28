using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SprintRetroAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.id);
                    table.ForeignKey(
                        name: "FK_Columns_Rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    connection_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Participants_Rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    column_id = table.Column<Guid>(type: "uuid", nullable: false),
                    participant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    VoteCount = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comments_Columns_column_id",
                        column: x => x.column_id,
                        principalTable: "Columns",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Participants_participant_id",
                        column: x => x.participant_id,
                        principalTable: "Participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columns_room_id",
                table: "Columns",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_column_id",
                table: "Comments",
                column: "column_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_participant_id",
                table: "Comments",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_room_id",
                table: "Comments",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_connection_id",
                table: "Participants",
                column: "connection_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_room_id",
                table: "Participants",
                column: "room_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
