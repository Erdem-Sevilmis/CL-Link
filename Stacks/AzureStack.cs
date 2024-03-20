using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.CognitiveServices;
using Pulumi.AwsNative;

public class AzureStack
{
    public AzureStack()
    {
        // Create an Azure Resource Group
        var resourceGroup = new Pulumi.AzureNative.Resources.ResourceGroup("resourceGroup", new()
        {
            Location = "germanywestcentral",
            ResourceGroupName = "CL-Link-Resource",
        });

        // Create an Azure Cognitive Services Account for the Translator service
        var account = new Pulumi.AzureNative.CognitiveServices.Account("translatorServiceAccount", new()
        {
            AccountName = "CL-Link",
            Identity = new Pulumi.AzureNative.CognitiveServices.Inputs.IdentityArgs
            {
                Type = Pulumi.AzureNative.CognitiveServices.ResourceIdentityType.SystemAssigned,
            },
            Kind = "TextTranslation",
            Location = "germanywestcentral",
            ResourceGroupName = resourceGroup.Name,
            Sku = new Pulumi.AzureNative.CognitiveServices.Inputs.SkuArgs
            {
                Name= "S1"
            },
        });
        //SAMPLE CODE FOR TRANSLATOR
        // check if its possible to fetch
        //      translator-key
        //      Ocp-Apim-Subscription-Key
        //      Ocp-Apim-Subscription-Region
        //      <YOUR-RESOURCE-LOCATION> (-> in code above i think)
        //
        //from pulumi
        /*
        using System.Text;
        using Newtonsoft.Json;

        class Program
        {
            private static readonly string key = "<your-translator-key>";
            private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com";

            // location, also known as region.
            // required if you're using a multi-service or regional (not global) resource. It can be found in the Azure portal on the Keys and Endpoint page.
            private static readonly string location = "<YOUR-RESOURCE-LOCATION>";

            static async Task Main(string[] args)
            {
                // Input and output languages are defined as parameters.
                string route = "/translate?api-version=3.0&from=en&to=fr&to=zu";
                string textToTranslate = "I would really like to drive your car around the block a few times!";
                object[] body = new object[] { new { Text = textToTranslate } };
                var requestBody = JsonConvert.SerializeObject(body);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    // Build the request.
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(endpoint + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8 "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                    // location required if you're using a multi-service or regional (not global) resource.
                    request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                    // Send the request and get response.
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    // Read response as a string.
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
            }
        }
          
         */
    }
}


/*
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
*/

