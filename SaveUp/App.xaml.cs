namespace SaveUp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Register Syncfusion license
            RegisterSyncfusionLicense();

            MainPage = new AppShell();
        }

        private void RegisterSyncfusionLicense()
        {
            try
            {
                // Get the path to the license file
                var assembly = typeof(App).Assembly;
                string resourceName = "SaveUp.Resources.syncfusion_license.txt";

                // Debugging: Print available resource names
                foreach (var resource in assembly.GetManifestResourceNames())
                {
                    Console.WriteLine(resource);
                }

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string licenseKey = reader.ReadToEnd().Trim();
                            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException($"The embedded resource '{resourceName}' was not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Syncfusion license: {ex.Message}");
            }
        }
    }
}
