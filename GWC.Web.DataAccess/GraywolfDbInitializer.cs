using Gwc.Common.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GWC.Web.DataAccess
{
    public static class GraywolfDbInitializer
    {
        public static void Initialize(GwcDbContext context)
        {
            try
            {
                if (context.Users.Any() || context.BillingCalendar.Any())
                {
                    return;
                }
            } catch 
            {
                return;
            }

            var baseDir = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("GWC.Web"))
                .Add("GWC.Web.DataAccess\\seed\\");

            var sqlCommands = Directory.EnumerateFiles(baseDir, "data_*.sql")
                                .OrderBy(x => x)
                                .SelectMany(ReadAllCommands);

            foreach (string command in sqlCommands)
            {
                try
                {
                    context.Database.ExecuteSqlCommand(command);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static IEnumerable<string> ReadAllCommands(string path)
        {
            StringBuilder sb = null;
            foreach (var line in File.ReadLines(path))
            {
                if (string.Equals(line, "GO", StringComparison.OrdinalIgnoreCase))
                {
                    if (null != sb && 0 != sb.Length)
                    {
                        string item = sb.ToString();
                        if (!string.IsNullOrWhiteSpace(item)) yield return item;
                        sb = null;
                    }
                }
                else
                {
                    if (null == sb) sb = new StringBuilder();
                    sb.AppendLine(line);
                }
            }
            if (null != sb && 0 != sb.Length)
            {
                string item = sb.ToString();
                if (!string.IsNullOrWhiteSpace(item)) yield return item;
            }
        }
    }
}