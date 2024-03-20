using System;
using Pulumi;
//using Pulumi.GoogleNative.Storage.V1;
using Pulumi.AwsNative.S3;
using System.Threading.Tasks;
using Pulumi.AzureNative.NetworkCloud;

public class MainStack : Stack
{
    public MainStack()
    {
        // Create an instance of aWSStack
        //var aWSStack = new AWSStack();

        // Create an instance of azureStack
        var azureStack = new AzureStack();

        // Create an instance of gCPStack
        //var gCPStack = new GCPStack();

        // If you have output values from the stacks that you would like
        // to export, you can do so here.
        /*
        this.aWSStack = aWSStack.SomeOutputValue;
        this.azureStack = azureStack.SomeOutputValue;
        this.gCPStack = gCPStack.SomeOutputValue;
        */
        //System.Console.WriteLine(gCPStack.ApiKeyId);
    }
}
class Program
{
    static Task<int> Main(string[] args)
    {
        return Pulumi.Deployment.RunAsync<MainStack>();
    }
}