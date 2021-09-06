using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Command.DeleteAuthor
{
    public class DeleteAuthor
    {    
        
        public int AuthorId{get;set;}

        private readonly BookStoreDbContext _context;
        public DeleteAuthor(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle() 
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException(" Yazar Bulunamadı!");
            if(_context.Books.FirstOrDefault(x=>x.AuthorId==author.Id) is not null)
                throw new InvalidOperationException("Kitabı Yayında olan bir yazarı silemezsiniz!.");
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

    }
}