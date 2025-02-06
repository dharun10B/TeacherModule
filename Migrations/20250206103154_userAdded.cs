using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeacherModule.Migrations
{
    /// <inheritdoc />
    public partial class userAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "BatchId", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "789 Pine St", null, "john.doe@example.com", "John Doe", "345-678-9012" },
                    { 2, "321 Maple St", null, "jane.roe@example.com", "Jane Roe", "456-789-0123" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Address", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Elm St", "alice@example.com", "Alice Smith", "123-456-7890" },
                    { 2, "456 Oak St", "bob@example.com", "Bob Johnson", "234-567-8901" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseName", "Description", "TeacherId" },
                values: new object[,]
                {
                    { 1, "Math 101", "Introduction to Mathematics", 1 },
                    { 2, "Science 101", "Introduction to Science", 2 }
                });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "BatchName", "BatchTiming", "BatchType", "CourseId" },
                values: new object[,]
                {
                    { 1, "Batch A", "09:00 AM - 12:00 PM", "Regular", 1 },
                    { 2, "Batch B", "01:00 PM - 04:00 PM", "Evening", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
