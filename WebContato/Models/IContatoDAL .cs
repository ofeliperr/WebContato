using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebContato.Models
{
    public interface IContatoDAL
    {
        IEnumerable<Contato> GetAllContatos();
        void AddContato(Contato contato);
        void UpdateContato(Contato contato);
        Contato GetContato(int? id);
        void DeleteContato(int? id);
    }
}
