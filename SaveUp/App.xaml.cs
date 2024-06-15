namespace SaveUp
{
    public partial class App : Application
    {
        public App()
        {
            // Pfad zur Lizenzdatei
            string licenseFilePath = Path.Combine(FileSystem.AppDataDirectory, "syncfusion_license.txt");

            if (File.Exists(licenseFilePath))
            {
                string licenseKey = File.ReadAllText(licenseFilePath).Trim();
                if (!string.IsNullOrEmpty(licenseKey))
                {
                    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);
                    Console.WriteLine("Syncfusion License Key registered successfully.");
                }
                else
                {
                    Console.WriteLine("Syncfusion License Key is empty.");
                }
            }
            else
            {
                Console.WriteLine($"License file not found: {licenseFilePath}");
            }

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
