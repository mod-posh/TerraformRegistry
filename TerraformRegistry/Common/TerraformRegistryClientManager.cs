using ModPosh.TerraformRegistryClient;

namespace ModPosh.TerraformRegistry.Common
{
    public static class TerraformRegistryClientManager
    {
        private static Client? _terraformRegistryClient;

        public static Client? TerraformRegistryClient
        {
            get
            {
                if (_terraformRegistryClient == null)
                {
                    throw new InvalidOperationException("The TerraformRegistryClient has not been initialized.");
                }
                return _terraformRegistryClient;
            }
            set => _terraformRegistryClient = value;
        }

        public static void ClearClient()
        {
            _terraformRegistryClient = null;
        }
    }
}
