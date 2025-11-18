using ApiProject.Models;

namespace ApiProject.Data;

public static class ItemSeedData
{
    public static List<Item> GetSeedItems()
    {
        return new List<Item>
        {
            new Item
            {
                Id = 1,
                Name = "Laptop",
                Description = "High-performance laptop for development work",
                CreatedAt = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                UpdatedAt = null
            },
            new Item
            {
                Id = 2,
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse with long battery life",
                CreatedAt = new DateTime(2024, 1, 16, 14, 20, 0, DateTimeKind.Utc),
                UpdatedAt = null
            },
            new Item
            {
                Id = 3,
                Name = "Mechanical Keyboard",
                Description = "RGB mechanical keyboard with Cherry MX switches",
                CreatedAt = new DateTime(2024, 1, 17, 9, 15, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 18, 11, 45, 0, DateTimeKind.Utc)
            },
            new Item
            {
                Id = 4,
                Name = "Monitor",
                Description = "27-inch 4K monitor with HDR support",
                CreatedAt = new DateTime(2024, 1, 18, 16, 0, 0, DateTimeKind.Utc),
                UpdatedAt = null
            },
            new Item
            {
                Id = 5,
                Name = "USB-C Hub",
                Description = "Multi-port USB-C hub with HDMI, USB 3.0, and SD card reader",
                CreatedAt = new DateTime(2024, 1, 19, 8, 30, 0, DateTimeKind.Utc),
                UpdatedAt = null
            }
        };
    }
}

