using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public interface IContactService
    {
        Task SaveContact(Contact contact);
        Task DeleteContact(int id);
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> GetById(int id);
    }
}
