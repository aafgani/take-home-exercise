using API.Core;
using API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI;

public class TodoController : BaseController
{
    private readonly IMediator mediator;

    public TodoController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var cmd = new GetTodosCommand { UserId = GetUserName() };
        var result = await mediator.Send(cmd);

        if (result.IsError)
            return BadRequest(string.Join(",", result.Error));

        return View(result.Result.Data);
    }

    [HttpPost]
    public async Task<ActionResult> Create(AddTodosCommand todo)
    {
        todo.Id = Guid.NewGuid().ToString();
        todo.CreatedDate = DateTime.Now;
        todo.CreatedBy = GetUserName();
        var result = await mediator.Send(todo);

        if (result.IsError)
            return BadRequest(string.Join(",", result.Error));

        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<JsonResult> PopulateForm(string id)
    {
        var result = await mediator.Send(new GetTodoByIdCommand { TodoId = id });

        if (result.IsError)
            return Json(string.Join(",", result.Error));

        return Json(result.Result);
    }

    [HttpPost]
    public async Task<ActionResult> Update(UpdateTodosCommand todo)
    {
        var result = await mediator.Send(new GetTodoByIdCommand { TodoId = todo.Id });

        if (result.IsError)
            return BadRequest(string.Join(",", result.Error));

        var item = result.Result;
        item.ItemName = todo.ItemName;
        result = await mediator.Send(todo);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> CompleteItem(UpdateTodosCommand todoCommand)
    {
        var result = await mediator.Send(new GetTodoByIdCommand { TodoId = todoCommand.Id });

        if (result.IsError)
            return BadRequest(string.Join(",", result.Error));

        var todoItem = result.Result;
        if (todoItem == null)
            return BadRequest("todo item not found");

        todoCommand.ItemName = todoItem.ItemName;
        todoCommand.CreatedDate = todoItem.CreatedDate;
        todoCommand.IsCompleted = true;
        result = await mediator.Send(todoCommand);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        var result = await mediator.Send(new GetTodoByIdCommand { TodoId = id });

        if (result.IsError)
            return BadRequest(string.Join(",", result.Error));

        var todoItem = result.Result;
        if (todoItem == null)
            return BadRequest("todo item not found");

        var cmd = new UpdateTodosCommand
        {
            Id = id,
            ItemName = todoItem.ItemName,
            CreatedBy = todoItem.CreatedBy,
            CreatedDate = todoItem.CreatedDate,
            IsDeleted = true
        };

        result = await mediator.Send(cmd);

        return RedirectToAction("Index");
    }

}
