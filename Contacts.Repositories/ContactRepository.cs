using Contacts.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task DeleteContact(int id)
        {
            var sql = @"DELETE FROM Contacts WHERE Id = @Id";

            var result =  await _dbConnection.ExecuteAsync(sql, new {Id = id});
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            try
            {
                var sql = @" SELECT Id, FirstName, LastName, Phone, Adress
                                FROM Contacts";

                return await _dbConnection.QueryAsync<Contact>(sql);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Contact> GetById(int id)
        {
            try
            {
                var sql = @" SELECT Id, FirstName, LastName, Phone, Adress
                                FROM Contacts
                                WHERE Id = @Id";

                return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(sql, new { Id = id });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> InsertContact(Contact contact)
        {
            try
            {
                var sql = @" INSERT INTO Contacts(FirstName, LastName, Phone, Adress)
                                VALUES (@FirstName, @LastName, @Phone, @Adress)";

                var result = await _dbConnection.ExecuteAsync(sql, new { contact.FirstName, contact.LastName, contact.Phone, contact.Adress });

                return result > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            try
            {
                var sql = @" UPDATE Contacts
                                SET FirstName = @FirstName,
                                    LastName = @LastName,
                                    Phone = @Phone,
                                    Adress = @Adress
                                WHERE Id = @Id";

                var result = await _dbConnection.ExecuteAsync(sql, new { contact.FirstName, contact.LastName, contact.Phone, contact.Adress, contact.Id });

                return result > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
