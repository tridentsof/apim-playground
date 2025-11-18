using ApiProject.Data;
using ApiProject.Models;
using System.Text.Json;

namespace ApiProject.Services;

public class ItemService
{
    private readonly List<Item> _items = new();
    private int _nextId = 1;

    public ItemService()
    {
        LoadSeedData();
    }

    private void LoadSeedData()
    {
        // Try to load from JSON file first
        try
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "items-seed-data.json");
            
            if (File.Exists(jsonPath))
            {
                var jsonContent = File.ReadAllText(jsonPath);
                var seedItems = JsonSerializer.Deserialize<List<Item>>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (seedItems != null && seedItems.Any())
                {
                    _items.AddRange(seedItems);
                    _nextId = _items.Max(i => i.Id) + 1;
                    return;
                }
            }
        }
        catch (Exception)
        {
            // If JSON loading fails, fall back to C# seed data
        }

        // Fallback to C# seed data if JSON file doesn't exist or fails to load
        try
        {
            var seedItems = ItemSeedData.GetSeedItems();
            if (seedItems != null && seedItems.Any())
            {
                _items.AddRange(seedItems);
                _nextId = _items.Max(i => i.Id) + 1;
            }
        }
        catch (Exception)
        {
            // If both fail, start with empty list
            // In production, you might want to log this
        }
    }

    public IEnumerable<Item> GetAllItems()
    {
        return _items;
    }

    public Item? GetItemById(int id)
    {
        return _items.FirstOrDefault(i => i.Id == id);
    }

    public Item CreateItem(Item item)
    {
        item.Id = _nextId++;
        item.CreatedAt = DateTime.UtcNow;
        _items.Add(item);
        return item;
    }

    public Item? UpdateItem(int id, Item updatedItem)
    {
        var existingItem = GetItemById(id);
        if (existingItem == null)
            return null;

        existingItem.Name = updatedItem.Name;
        existingItem.Description = updatedItem.Description;
        existingItem.UpdatedAt = DateTime.UtcNow;
        return existingItem;
    }

    public bool DeleteItem(int id)
    {
        var item = GetItemById(id);
        if (item == null)
            return false;

        _items.Remove(item);
        return true;
    }
}

