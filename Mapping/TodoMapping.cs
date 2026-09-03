using AutoMapper;
using TodoCs.Dtos;
using TodoCs.Models;

namespace TodoCs.Mapping;

public class TodoMapping : Profile
{
    public TodoMapping()
    {
        CreateMap<Todo, TodoResponse>();
        CreateMap<CreateTodoRequest, Todo>();
        CreateMap<UpdateTodoRequest, Todo>();
    }
}