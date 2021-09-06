using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Queries.GetAuthorDetail

{
    public class GetAuthorDetail

    {
        public int AuthorId { get; set; }
         public readonly BookStoreDbContext _context;
           public readonly IMapper _mapper;
        public GetAuthorDetail(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author= _context.Authors.SingleOrDefault(x=> x.Id==AuthorId); 
            if(author is null)
            throw new InvalidOperationException("Yazar Bulunamadı");
          
            return  _mapper.Map<AuthorDetailViewModel>(author);
        } 


    }

    public class  AuthorDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date{get;set;}
        
    }
    
}