using LinkedListAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkedListAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LinkedListController : ControllerBase
{
    private static readonly LinkedList _linkedList = new LinkedList();

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_linkedList.ToList());
    }

    [HttpPost]
    public IActionResult Post([FromBody] NodeValueModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.Value))
        {
            return BadRequest("Value is required");
        }

        _linkedList.Add(model.Value);
        var lastNode = _linkedList.ToList().Last();
        return Ok(new { Id = lastNode.GetType().GetProperty("Id").GetValue(lastNode) });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (_linkedList.Remove(id))
        {
            return Ok($"Node with ID {id} removed successfully");
        }

        return NotFound($"Node with ID {id} not found");
    }
}

public class NodeValueModel
{
    public string Value { get; set; }
}