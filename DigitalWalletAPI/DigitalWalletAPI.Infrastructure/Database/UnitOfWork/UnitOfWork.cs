using DigitalWalletAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWalletAPI.Infrastructure.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DigitalWalletContext _context;

        public UnitOfWork(DigitalWalletContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Confirma todas as alterações no banco de dados.
        /// </summary>
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
