using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.Getbooks;
using WebApi.Application.BookOperations.Queries.GetBooksDetail;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Queries.GetBooksDetail.GetbooksDetailQuery;

namespace WebApi.AddControllers{
[ApiController]
[Route("[controller]s")]
public class BookController:ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
   public BookController(BookStoreDbContext context ,IMapper mapper)
   {
     _context=context;
     _mapper=mapper;
   }

[HttpGet]
public IActionResult GetBooks()
{
  GetBookQuery query=new GetBookQuery(_context,_mapper);
  var result=query.Handle();
  return Ok(result);
  
} 
[HttpGet("{id}")]
public IActionResult GetById(int id)
{
        BookDetailViewModel result;
        
        GetbooksDetailQuery query=new GetbooksDetailQuery(_context, _mapper);
        query.BookId=id;
        GetbooksDetailQueryValidator validator =new GetbooksDetailQueryValidator();
        validator.ValidateAndThrow(query); 
        result= query.Handle();
   

        return Ok(result);
        
} 
//[HttpGet]
//public Book Get([FromQuery] string id)
//{
  //  var book=BookList.Where(book=>book.Id==Convert.ToInt32(id)).SingleOrDefault();
    //return book;
//} 
//POST
[HttpPost]
public IActionResult AddBook([FromBody] CreateBookModel newBook)
{ 
 CreateBookCommand command=new CreateBookCommand(_context,_mapper);
  

 command.Model=newBook;
 CreateBookCommandValidator validator=new CreateBookCommandValidator();
 validator.ValidateAndThrow(command);
 command.Handle();
// if(!result.IsValid)
// foreach(var item in result.Errors)

// Console.WriteLine("Özellik :"+item.PropertyName+"Error Message"+item.ErrorMessage);
// else
// {
//       command.Handle();
// }
return Ok();

}
//put
[HttpPut("{id}")]
public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBook)
{
   UpdateBookCommand command=new UpdateBookCommand(_context);
   
   command.BookId=id;
   command.Model=updateBook;
   UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
   validator.ValidateAndThrow(command); 
   command.Handle();
  
  return Ok();
}
//delete
[HttpDelete("{id}")]
public IActionResult DeleteBook(int id)
{
      DeleteBookCommand command=new DeleteBookCommand(_context);
      command.BookId=id;
      DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
      validator.ValidateAndThrow(command);
      command.Handle();

      return Ok();
}
}

} 