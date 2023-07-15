using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationAuthorizationProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addRelTblGrupandPermms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GroupPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_GroupId",
                table: "GroupPermissions",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions");

            migrationBuilder.DropIndex(
                name: "IX_GroupPermissions_GroupId",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupPermissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermissions",
                table: "GroupPermissions",
                columns: new[] { "GroupId", "PermissionId" });
        }
    }
}
