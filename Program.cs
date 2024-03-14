using Pulumi;
using Pulumi.GoogleNative.Storage.V1;
using Pulumi.AwsNative.S3;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;
using System.Collections.Generic;
using System.Threading.Tasks;
using GCPBucket = Pulumi.GoogleNative.Storage.V1.Bucket;
using AWSBucket = Pulumi.AwsNative.S3.Bucket;

public class GCPStack
{
    public GCPStack()
    {
        // Create a Google Cloud resource (Storage Bucket)
        var bucket = new GCPBucket("my-bucketGCP");

        // Export the DNS name of the bucket
        new Dictionary<string, object?>
        {
            ["bucketSelfLink"] = bucket.SelfLink
        };
    }
}

public class AWSStack
{
    public AWSStack()
    {
        // Create an AWS resource (S3 Bucket)
        var bucket = new AWSBucket("my-bucketAWS");

        // Export the name of the bucket
        new Dictionary<string, object?>
        {
            ["bucketName"] = bucket.Id
        };
    }
}

public class AzureStack
{
    public AzureStack()
    {
        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("resourceGroup");

        // Create an Azure resource (Storage Account)
        var storageAccount = new StorageAccount("sa", new StorageAccountArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Sku = new SkuArgs
            {
                Name = SkuName.Standard_LRS
            },
            Kind = Kind.StorageV2
        });

        var storageAccountKeys = ListStorageAccountKeys.Invoke(new ListStorageAccountKeysInvokeArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AccountName = storageAccount.Name
        });

        var primaryStorageKey = storageAccountKeys.Apply(accountKeys =>
        {
            var firstKey = accountKeys.Keys[0].Value;
            return Output.CreateSecret(firstKey);
        });

        // Export the primary key of the Storage Account
        new Dictionary<string, object?>
        {
            ["primaryStorageKey"] = primaryStorageKey
        };
    }
}

public class MainStack : Stack
{
    public MainStack()
    {
        // Create an instance of MyFirstStack
        var aWSStack = new AWSStack();

        // Create an instance of MySecondStack
        var azureStack = new AzureStack();
        
        // Create an instance of MySecondStack
        var gCPStack = new GCPStack();

        // If you have output values from the stacks that you would like
        // to export, you can do so here.
        /*
        this.aWSStack = aWSStack.SomeOutputValue;
        this.azureStack = azureStack.SomeOutputValue;
        this.gCPStack = gCPStack.SomeOutputValue;
        */
    }
}
class Program
{
    static Task<int> Main(string[] args)
    {
        return Pulumi.Deployment.RunAsync<MainStack>();
    }
}