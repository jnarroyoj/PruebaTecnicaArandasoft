using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.ApplicationCore.ApplicationExceptions
{
    public class CatalogoDbUpdateException : DbUpdateException
    {
        public CatalogoDbUpdateException(string message, Exception ex) : base(message, ex)
        {

        }

        public CatalogoDbUpdateException(string message) : base(message)
        {

        }
    }
}
