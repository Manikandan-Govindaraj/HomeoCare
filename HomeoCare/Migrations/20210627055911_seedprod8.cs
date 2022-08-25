using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class seedprod8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TBL_Products",
                columns: new[] { "ProductID", "CategoryId", "Description", "ImagePath", "OriginalPrice", "Price", "ProductName", "Quantity", "QuantityOnHand", "SKU", "ShortDesc", "WSPrice" },
                values: new object[,]
                {
                    { 2, 1, "Say goodbye to tired and puffy skin as you gently apply this pore tightening toner from Biotique. It combines the cooling effect.", "619932b1-132a-40bf-8455-bd913123dd29.jfif", 0m, 32m, "Bio Cucumber", 10, 10, null, "Cucumber, with its cooling property, helps to reduce skin puffiness and tiredness so as to restore.", 0m },
                    { 3, 2, "Smooth skin begins with the NIVEA Aloe Hydration Body Lotion. Ideal for all skin types, this lotion is mild enough to be used daily. Though it is primarily designed for use on the body.", "88a12012-645b-4254-b5d2-a9f541f5200b.jfif", 0m, 56m, "Body Lotion", 10, 10, null, "Enriched with aloe vera extracts, this alcohol-free lotion soothes dry and irritated skin, and nourishes.", 0m },
                    { 4, 2, "Do you suffer from lifeless and lifeless skin? If yes, this rejuvenating face wash gel by Biotique is the face wash that will help you. This face wash gel combines groundnut, papaya and neem to give your skin the cleaning and nourishment it needs", "3fe85a01-7d19-4e3d-b3fc-e037b27a4296.jfif", 0m, 74m, "Papaya Exfoliating", 10, 10, null, "This naturally tones your skin and leaves it supple, soft and moisturized to ensure your skin has a natural glow.", 0m },
                    { 5, 3, "Let the goodness of natural ingredients, such as Fruits and Multani Mitti, of the Biotique BIO Fruit Whitening & Depigmentation Face Pack lighten your skin and give you flawless charm. By adding this pack to your regular skincare routine, you can get smooth and soft skin with a youthful texture.", "5c52cf14-43b5-4079-9963-223c5e6fdfc8.jfif", 0m, 68m, "BIOTIQUE BIO Fruit BIO Fruit", 10, 10, null, "Enriched with nutrients and essential vitamins, fruits give you bright and beautiful skin.", 0m },
                    { 6, 3, "A brainchild of a chemist, Olay was born out of love and was Graham Wulff’s gift to his doting wife. Graham wanted to create a new beauty product that would leave his wife’s skin nourished and moisturized without leaving a greasy feel. Graham and Dinah started an extensive research and finally created the legendary Oil of Olay Beauty Fluid.", "9547f45c-1a08-4ac8-9890-7bf54600575a.jfif", 0m, 95m, "Fairness Cream", 10, 10, null, "This cream works wonders on your skin as a fairness cream, spot remover, moisturizer.", 0m },
                    { 7, 1, "A brainchild of a chemist, Olay was born out of love and was Graham Wulff’s gift to his doting wife. Graham wanted to create a new beauty product that would leave his wife’s skin nourished and moisturized without leaving a greasy feel. Graham and Dinah started an extensive research and finally created the legendary Oil of Olay Beauty Fluid.", "5453d81e-0270-4097-ab30-791cb0ad5023.jpg", 0m, 49m, "test", 10, 10, null, "This cream works wonders on your skin as a fairness cream, spot remover, moisturizer.", 0m },
                    { 8, 2, "A brainchild of a chemist, Olay was born out of love and was Graham Wulff’s gift to his doting wife. Graham wanted to create a new beauty product that would leave his wife’s skin nourished and moisturized without leaving a greasy feel. Graham and Dinah started an extensive research and finally created the legendary Oil of Olay Beauty Fluid.", "f17c0590-9d20-4263-a7c0-ed7b10fef8fe.png", 0m, 52m, "test 2", 10, 10, null, "This cream works wonders on your skin as a fairness cream, spot remover, moisturizer.", 0m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 8);
        }
    }
}
