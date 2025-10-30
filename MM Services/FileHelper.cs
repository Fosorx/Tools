using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_Services
{
    internal class FileHelper
    {
        private static readonly string logPath = @"Log-services.txt";

        public static void WriteLog(string message)
        {
            try
            {
                // Vytvoří složku, pokud neexistuje
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));

                // Přidá text do souboru (vytvoří ho, pokud neexistuje)
                File.AppendAllText(logPath,
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Když by se něco pokazilo (např. nedostatečná oprávnění)
                // doporučuje se logovat aspoň do EventLogu:
                try
                {
                    System.Diagnostics.EventLog.WriteEntry("MyService",
                    $"Chyba při zápisu logu: {ex.Message}",
                    System.Diagnostics.EventLogEntryType.Error);
                }
                catch { }
            }
        }
    }
}
