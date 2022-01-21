using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using DynamoDBRepository;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TestLambda
{
    public class Function
    {

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
        }

        string Ip = "localhost";
        int Port = 8000;
       
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(ILambdaContext context)
        {

            /*https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/SettingUp.DynamoWebService.html#SettingUp.DynamoWebService.GetCredentials
             * I created an AWS User called DynamoDBRepoTestUser and assigned it the policy
             * DynamoDBRepoTestMovieTablePolicy
             * The Access Key ID and secret access key are in KeePass
             * */

            //https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Tools.CLI.html

            //access codes can be set in env vars as described here  https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-envvars.html

            //This article explains how to setup policies, but the sample json he gives does not allow for ListTables
            //https://aws.amazon.com/blogs/security/how-to-create-an-aws-iam-policy-to-grant-aws-lambda-access-to-an-amazon-dynamodb-table/
            //ReturnValuesOnConditionCheckFailure the BradsPolicy.json file I added which fixes this
            //the addition is needed in order to see the table in the AWS explorer in VS
            //I added a bunch more permissions before I found my error was a bad endpoint, so some of the permissions in
            //BradsPolicy.json may be unnecessary

            var cfg = DynamoDbConfigFactory.GetConfigForEndpoint(RegionEndpoint.USEast1);
            var mgr = new DynamoDBTableManager(cfg);
            //var tables = await mgr.GetTableListAsync();
            var c = new AmazonDynamoDBClient(cfg);
            var tables= await c.ListTablesAsync();


            return tables != null ? tables.TableNames.Last() : "no tables";

            //https://docs.aws.amazon.com/general/latest/gr/ddb.html

        }


    }
}
