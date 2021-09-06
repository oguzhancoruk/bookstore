using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook

{
    public class CreateBookCommand
    {   
        public CreateBookModel Model {get;set;}

        private readonly BookStoreDbContext _dbContext; 
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)

        {
            _mapper=mapper;
            _dbContext=dbContext;

        }
        public void Handle()
        {
          var book =_dbContext.Books.SingleOrDefault(x=>x.Title==Model.Title);
          if(book is not null)
        throw new InvalidOperationException("kitap zaten mevcut");

        book= _mapper.Map<Book>(Model); 
       _dbContext.Books.Add(book);
       _dbContext.SaveChanges();
        }
        public class CreateBookModel
       
        {
       public string Title{get;set;}
       public int GenreId{get;set;}  
       public int PageCount{get;set;}
       public DateTime PublishDate {get;set;}

            
        }
    }
    
}