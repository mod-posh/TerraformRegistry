using ModPosh.TerraformRegistry.Common;
using System.Management.Automation;

namespace ModPosh.TerraformRegistry.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "TerraformRegistry")]
    public class GetTerraformRegistry : Cmdlet
    {
        [Parameter(Mandatory = false)]
        public string ModuleNamespace { get; set; } = string.Empty;

        [Parameter(Mandatory = false)]
        public string Provider { get; set; } = string.Empty;

        [Parameter(Mandatory = false)]
        public bool? Verified { get; set; }

        [Parameter(Mandatory = false)]
        public int? Offset { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                // Ensure the client is initialized
                var client = TerraformRegistryClientManager.TerraformRegistryClient
                    ?? throw new InvalidOperationException("TerraformRegistryClient is not initialized.");

                // Call the ListModulesAsync method on the client
                var modules = client.ListModulesAsync(ModuleNamespace, provider: Provider, verified: Verified, offset: Offset).Result;

                WriteObject(modules);
            }
            catch (InvalidOperationException ex)
            {
                WriteError(new ErrorRecord(ex, "ClientNotInitialized", ErrorCategory.InvalidOperation, null));
            }
            catch (AggregateException ex) when (ex.InnerException is HttpRequestException)
            {
                WriteError(new ErrorRecord(ex.InnerException, "HttpRequestFailed", ErrorCategory.ConnectionError, null));
            }
        }
    }
}
