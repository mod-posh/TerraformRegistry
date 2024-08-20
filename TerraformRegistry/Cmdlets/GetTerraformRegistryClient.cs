using ModPosh.TerraformRegistry.Common;
using ModPosh.TerraformRegistryClient;
using System.Management.Automation;

namespace ModPosh.TerraformRegistry.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "TerraformRegistryClient")]
    public class GetTerraformRegistryClientCommand : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var client = TerraformRegistryClientManager.TerraformRegistryClient;

            if (client == null)
            {
                WriteError(new ErrorRecord(
                    new InvalidOperationException("The TerraformRegistryClient has not been initialized."),
                    "ClientNotInitialized",
                    ErrorCategory.InvalidOperation,
                    null));
                return;
            }

            WriteObject(client);
        }
    }
}