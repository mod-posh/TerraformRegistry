using ModPosh.TerraformRegistry.Common;
using ModPosh.TerraformRegistryClient;
using System.Management.Automation;

namespace ModPosh.TerraformRegistry.Cmdlets
{
    [Cmdlet(VerbsCommunications.Connect, "TerraformRegistry")]
    public class ConnectTerraformRegistryCommand : Cmdlet
    {
        [Parameter(Mandatory = false)]
        public TerraformRegistryConnectionInfo? ConnectionInfo { get; set; }

        protected override void ProcessRecord()
        {
            if (ConnectionInfo == null)
            {
                // Use default connection info if none is provided
                ConnectionInfo = new TerraformRegistryConnectionInfo();
            }

            TerraformRegistryClientManager.TerraformRegistryClient = new Client(ConnectionInfo);
            WriteVerbose("Connected to the Terraform Registry.");
        }
    }
}