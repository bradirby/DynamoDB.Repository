using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DynamoDB.Repository
{
    public static class Bootstrapper
    {
        public static void AddDynamoDBRepository(this IServiceCollection services)
        {
            services.AddTransient<IDynamoDBConfigProvider, DynamoDBConfigProvider>();
        }

    }
}
