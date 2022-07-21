using Contacts.Model;
using Contacts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertContact([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest();

            if(string.IsNullOrEmpty(contact.FirstName))            
                ModelState.AddModelError("FirstName", "First name can't be empty");            

            if (string.IsNullOrEmpty(contact.LastName))            
                ModelState.AddModelError("LastName", "Last name can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRepository.InsertContact(contact);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest();

            if (string.IsNullOrEmpty(contact.FirstName))
                ModelState.AddModelError("FirstName", "First name can't be empty");

            if (string.IsNullOrEmpty(contact.LastName))
                ModelState.AddModelError("LastName", "Last name can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRepository.UpdateContact(contact);

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _contactRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Contact> GetById(int id)
        {
            return await _contactRepository.GetById(id);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _contactRepository.DeleteContact(id);
        }
    }
}
