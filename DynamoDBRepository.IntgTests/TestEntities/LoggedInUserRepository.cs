using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB.Repository.IntgTests.TestEntities
{

    public interface ILoggedInUserRepository : IDynamoDBRepository<AlexaUser>
    {
        /// <summary>
        /// This is a custom method - no method declarations are required here.
        /// </summary>
        Task<AlexaUser> GetByIdAsync(string alexaId);
        AlexaUser GetById(string alexaId);
    }


    public class LoggedInUserRepository : DynamoDBRepository<AlexaUser>, ILoggedInUserRepository
    {
        /// <summary>
        /// The only thing required for the repository is the name of the table and
        /// a Configuration Provider that can be resolved
        /// </summary>
        public LoggedInUserRepository(DynamoDBTableManager tableMgr) : base(AlexaUser.DynamoDBTableName, tableMgr)
        {
        }

        /// <summary>
        /// Sample custom method to get a row by its key properties.  This is not required in a repository.
        /// </summary>
        public async Task<AlexaUser> GetByIdAsync(string alexaId)
        {
            var rowKey = new Dictionary<string, DynamoDBEntry> { { AlexaUser.DynamoDBKeyFieldName, alexaId }};
            return await GetByKeyAsync(rowKey);
        }

        /// <summary>
        /// Sample custom method to get a row by its key properties.  This is not required in a repository.
        /// </summary>
        public AlexaUser GetById(string alexaId)
        {
            return GetByIdAsync(alexaId).GetAwaiter().GetResult();
        }

    }

}
