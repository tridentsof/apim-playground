using ApiProject.DTOs;
using ApiProject.Models;
using ApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[SwaggerTag("Operations for managing items")]
public class ItemsController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemsController(ItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Get all items
    /// </summary>
    /// <returns>List of all items</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Get all items", Description = "Retrieves a list of all items in the system")]
    [ProducesResponseType(typeof(IEnumerable<Item>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Item>> GetAllItems()
    {
        var items = _itemService.GetAllItems();
        return Ok(items);
    }

    /// <summary>
    /// Get item by ID
    /// </summary>
    /// <param name="id">The ID of the item to retrieve</param>
    /// <returns>The item with the specified ID</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get item by ID", Description = "Retrieves a specific item by its unique identifier")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Item> GetItemById(int id)
    {
        var item = _itemService.GetItemById(id);
        if (item == null)
        {
            return NotFound(new { message = $"Item with id {id} not found" });
        }
        return Ok(item);
    }

    /// <summary>
    /// Create a new item
    /// </summary>
    /// <param name="dto">The item data to create</param>
    /// <returns>The newly created item</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new item", Description = "Creates a new item with the provided information")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Item> CreateItem([FromBody] CreateItemDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var item = new Item
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var createdItem = _itemService.CreateItem(item);
        return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
    }

    /// <summary>
    /// Update an existing item
    /// </summary>
    /// <param name="id">The ID of the item to update</param>
    /// <param name="dto">The updated item data</param>
    /// <returns>The updated item</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update an existing item", Description = "Updates an existing item with new information")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Item> UpdateItem(int id, [FromBody] UpdateItemDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var item = new Item
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var updatedItem = _itemService.UpdateItem(id, item);
        if (updatedItem == null)
        {
            return NotFound(new { message = $"Item with id {id} not found" });
        }

        return Ok(updatedItem);
    }

    /// <summary>
    /// Delete an item
    /// </summary>
    /// <param name="id">The ID of the item to delete</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete an item", Description = "Deletes an item from the system")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteItem(int id)
    {
        var deleted = _itemService.DeleteItem(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Item with id {id} not found" });
        }

        return NoContent();
    }
}

