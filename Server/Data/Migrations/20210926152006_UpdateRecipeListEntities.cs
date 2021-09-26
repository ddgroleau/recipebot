using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PBC.Server.Data.Migrations
{
    public partial class UpdateRecipeListEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListDays_Recipes_BreakfastRecipeId",
                table: "ListDays");

            migrationBuilder.DropForeignKey(
                name: "FK_ListDays_Recipes_DinnerRecipeId",
                table: "ListDays");

            migrationBuilder.DropForeignKey(
                name: "FK_ListDays_Recipes_LunchRecipeId",
                table: "ListDays");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeSubscriptions_Recipes_RecipeId",
                table: "RecipeSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_RecipeSubscriptions_RecipeId",
                table: "RecipeSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_ListDays_BreakfastRecipeId",
                table: "ListDays");

            migrationBuilder.DropIndex(
                name: "IX_ListDays_DinnerRecipeId",
                table: "ListDays");

            migrationBuilder.DropIndex(
                name: "IX_ListDays_LunchRecipeId",
                table: "ListDays");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeSubscriptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "RecipeSubscriptions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Lists",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Lists",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "LunchRecipeId",
                table: "ListDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DinnerRecipeId",
                table: "ListDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BreakfastRecipeId",
                table: "ListDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Lists");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeSubscriptions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "RecipeSubscriptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LunchRecipeId",
                table: "ListDays",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DinnerRecipeId",
                table: "ListDays",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BreakfastRecipeId",
                table: "ListDays",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSubscriptions_RecipeId",
                table: "RecipeSubscriptions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ListDays_BreakfastRecipeId",
                table: "ListDays",
                column: "BreakfastRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ListDays_DinnerRecipeId",
                table: "ListDays",
                column: "DinnerRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ListDays_LunchRecipeId",
                table: "ListDays",
                column: "LunchRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListDays_Recipes_BreakfastRecipeId",
                table: "ListDays",
                column: "BreakfastRecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListDays_Recipes_DinnerRecipeId",
                table: "ListDays",
                column: "DinnerRecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListDays_Recipes_LunchRecipeId",
                table: "ListDays",
                column: "LunchRecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeSubscriptions_Recipes_RecipeId",
                table: "RecipeSubscriptions",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
