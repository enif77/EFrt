/* (C) 2020 Premysl Fara */

namespace EFrt.GetVersion
{
    using System;
    using System.IO;


    static class Program
    {
        // <Version>1.0.0</Version>
        private const string PackageVersionOpeningTag = "<Version>";
        private const string PackageVersionClosingTag = "</Version>";


        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("GetVersion.exe project-file-path");

                return 1;
            }

            var projectFilePath = args[0];

            if (File.Exists(projectFilePath) == false)
            {
                Console.Error.WriteLine($"The '{projectFilePath}' project file not found.");

                return 5;
            }

            try
            {
                var projectFileContents = File.ReadAllText(projectFilePath);

                var pvsIndex = projectFileContents.IndexOf(PackageVersionOpeningTag);
                if (pvsIndex < 0)
                {
                    Console.Error.WriteLine($"The '{PackageVersionOpeningTag}' in the {projectFilePath} project file not found.");

                    return 5;
                }

                var pveIndex = projectFileContents.IndexOf(PackageVersionClosingTag, pvsIndex + 1);
                if (pveIndex < 0)
                {
                    Console.Error.WriteLine($"The '{PackageVersionClosingTag}' in the {projectFilePath} project file not found.");

                    return 5;
                }

                Console.WriteLine(projectFileContents.Substring(pvsIndex + PackageVersionOpeningTag.Length, pveIndex - (pvsIndex + PackageVersionOpeningTag.Length)));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);

                return 5;
            }

            return 0;
        }
    }
}
