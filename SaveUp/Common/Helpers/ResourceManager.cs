using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp.Common.Helper
{
    public class ResourceManager
    {
        /// <summary>
        /// Register Syncfusion license
        /// </summary>
        public static void RegisterSyncfusionLicense()
        {
            var assembly = Assembly.GetExecutingAssembly();
            // a license can be obtained from https://www.syncfusion.com/products/communitylicense
            var resourceName = "SaveUp.Resources.syncfusion_license.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string licenseKey = reader.ReadToEnd();
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            }
        }
    }
}
