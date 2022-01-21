using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DynamoDBRepository;

namespace SchoolHolidays
{
    public interface ISchoolCalendarRepository : IDynamoDBRepository<SchoolCalendar>
    {
        /// <summary>
        /// This is a custom method - no method declarations are required here.
        /// </summary>
        Task<SchoolCalendar> GetByIdAsync(string id);
        SchoolCalendar GetById(string id);
    }

    public class SchoolCalendarRepository : DynamoDBRepository<SchoolCalendar>, ISchoolCalendarRepository
    {
        public static string DynamoDBTableName => "SchoolCalendars";

        /// <summary>
        /// The only thing required for the repository is the name of the table and
        /// a Configuration Provider that can be resolved
        /// </summary>
        public SchoolCalendarRepository(DynamoDBTableManager tableMgr) : base(DynamoDBTableName, tableMgr)
        {
        }

        /// <summary>
        /// Sample custom method to get a row by its key properties.  This is not required in a repository.
        /// </summary>
        public async Task<SchoolCalendar> GetByIdAsync(string id)
        {
            var rowKey = new Dictionary<string, DynamoDBEntry> { { "id", id }};
            return await GetByKeyAsync(rowKey);
        }

        /// <summary>
        /// Sample custom method to get a row by its key properties.  This is not required in a repository.
        /// </summary>
        public SchoolCalendar GetById(string id)
        {
            return GetByIdAsync(id).GetAwaiter().GetResult();
        }

        public static void CreateNewTable(DynamoDBTableManager mgr)
        {
            var thru = new ProvisionedThroughput(1, 1);
            var keys = new List<DynamoDBKeyDescriptor>
            {
                //this is the primary key
                new DynamoDBKeyDescriptor("id", DynamoDBKeyType.Hash, DynamoDBDataType.String)
            };

            mgr.CreateTable(DynamoDBTableName, keys, thru);
        }

    }
}
