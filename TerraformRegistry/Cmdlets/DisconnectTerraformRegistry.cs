using ModPosh.TerraformRegistry.Common;
using System.Management.Automation;

namespace ModPosh.TerraformRegistry.Cmdlets
{
    [Cmdlet(VerbsCommunications.Disconnect, "TerraformRegistry")]
    public class DisconnectTerraformRegistryCommand : Cmdlet
    {
        protected override void ProcessRecord()
        {
            TerraformRegistryClientManager.ClearClient();
            WriteVerbose("Disconnected from the Terraform Registry.");
        }
    }
}