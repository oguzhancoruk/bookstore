using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.Getbooks

{
     public class GetBookQuery

     {
         private readonly BookStoreDbContext _dbcontext;
         private readonly IMapper _mapper;

         public GetBookQuery(BookStoreDbContext dbContext, IMapper mapper)
         {   
             _mapper=mapper;
            _dbcontext=dbContext;

         }
         public List<BookViewModel> Handle()
         {
          var booklist=_dbcontext.Books.Include(x=>x.Genre).Include(x=>x.Author).OrderBy(x=>x.Id).ToList<Book>();
          
          List<BookViewModel> vn= _mapper.Map<List<BookViewModel>>(booklist);
         
           
          return vn;
         }
         
     }

     public class  BookViewModel
     {
       public string Title{get;set;}
       public string Genre{get;set;}  
       public int PageCount{get;set;}
       public string Author{get;set;}
       public string PublishDate {get;set;}
     }
}