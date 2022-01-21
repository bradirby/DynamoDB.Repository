﻿using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DynamoDBRepository.UnitTests;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBRepository.IntgTests.UnitTests
{

    public interface IMovieRepository : IDynamoDBRepository<Movie>
    {
        /// <summary>
        /// This is a custom method - no method declarations are required here.
        /// </summary>
        Task<Movie> GetByIdAsync(int year, string title);
        Movie GetById(int year, string title);
    }

    public class MovieRepository : DynamoDBRepository<Movie>, IMovieRepository
    {
        public static string DynamoDBTableName => "Movies";

        /// <summary>
        /// The only thing required for the repository is the name of the table and
        /// a Configuration Provider that can be resolved
        /// </summary>
        public MovieRepository(DynamoDBTableManager tableMgr) : base(DynamoDBTableName, tableMgr)
        {
        }

        /// <summary>
        /// Sample custom method to get a row by its key properties.  This is not required in a repository.
        /// </summary>
        public async Task<Movie> GetByIdAsync(int year, string title)
        {
            var rowKey = new Dictionary<string, DynamoDBEntry> { { "year", year }, { "title", title } };
            return await GetByKeyAsync(rowKey);
        }

        /// <summary>
        /// Sample custom method to get a row by its key properties.  This is not required in a repository.
        /// </summary>
        public Movie GetById(int year, string title)
        {
            return GetByIdAsync(year, title).GetAwaiter().GetResult();
        }

        public static void CreateNewTable(DynamoDBTableManager mgr)
        {
            var thru = new ProvisionedThroughput(1, 1);
            var keys = new List<DynamoDBKeyDescriptor>
            {
                //this is the optional sort key
                new DynamoDBKeyDescriptor("year", DynamoDBKeyType.Hash, DynamoDBDataType.Number),

                //this is the partition key
                new DynamoDBKeyDescriptor("title", DynamoDBKeyType.Range, DynamoDBDataType.String)
            };

            mgr.CreateTable(DynamoDBTableName, keys, thru);
        }

    }

}
