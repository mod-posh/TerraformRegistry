using ModPosh.TerraformRegistry.Common;
using System.Management.Automation;

namespace ModPosh.TerraformRegistry.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "TerraformModule")]
    public class GetTerraformModule : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string ModuleNamespace { get; set; } = string.Empty;

        [Parameter(Mandatory = true, Position = 1)]
        public string ModuleName { get; set; } = string.Empty;

        [Parameter(Mandatory = true, Position = 2)]
        public string Provider { get; set; } = string.Empty;

        [Parameter(Mandatory = false)]
        public string? Version { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                // Ensure the client is initialized
                var client = TerraformRegistryClientManager.TerraformRegistryClient
                    ?? throw new InvalidOperationException("TerraformRegistryClient is not initialized.");

                // Call the GetModuleAsync method on the client
                var moduleTask = string.IsNullOrEmpty(Version)
                    ? client.GetModuleAsync(ModuleNamespace, ModuleName, Provider)
                    : client.GetModuleAsync(ModuleNamespace, ModuleName, Provider, Version);

                var module = moduleTask.Result;

                WriteObject(module);
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
