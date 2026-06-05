using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal class Logger
    {
        public static string LogExceptionToHtml(Exception ex)
        {
            try
            {
                // 1. Create a dedicated folder for crash logs
                string logDirectory = Path.Combine(Application.StartupPath, "CrashLogs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // 2. Generate a unique file name using the current timestamp
                string fileName = $"CrashReport_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.html";
                string fullPath = Path.Combine(logDirectory, fileName);

                // 3. Build the HTML Document with responsive styles for readability
                StringBuilder html = new StringBuilder();
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html>");
                html.AppendLine("<head>");
                html.AppendLine("    <title>Application Crash Report</title>");
                html.AppendLine("    <style>");
                html.AppendLine("        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 20px; background-color: #f9f9f9; color: #333; }");
                html.AppendLine("        .container { max-width: 1000px; margin: 0 auto; background: white; padding: 30px; border-radius: 8px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); border-top: 6px solid #d9534f; }");
                html.AppendLine("        h1 { color: #d9534f; margin-top: 0; font-size: 24px; border-bottom: 2px solid #eee; padding-bottom: 10px; }");
                html.AppendLine("        .meta-table { width: 100%; border-collapse: collapse; margin-bottom: 25px; }");
                html.AppendLine("        .meta-table th, .meta-table td { text-align: left; padding: 10px; border-bottom: 1px solid #eee; }");
                html.AppendLine("        .meta-table th { width: 20%; color: #666; font-weight: 600; background-color: #fcfcfc; }");
                html.AppendLine("        .error-message { background-color: #fdf7f7; border-left: 4px solid #d9534f; padding: 15px; margin-bottom: 25px; font-size: 16px; font-weight: bold; color: #b94a48; }");
                html.AppendLine("        h3 { color: #2c3e50; margin-bottom: 10px; font-size: 18px; }");
                html.AppendLine("        pre { background-color: #2d3748; color: #f7fafc; padding: 15px; border-radius: 5px; overflow-x: auto; font-family: 'Consolas', 'Courier New', monospace; font-size: 13px; line-height: 1.6; }");
                html.AppendLine("        .highlight-line { color: #f6ad55; font-weight: bold; }");
                html.AppendLine("    </style>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");
                html.AppendLine("    <div class='container'>");
                html.AppendLine("        <h1>⚠️ Application Crash Report</h1>");

                // Summary Meta Details
                html.AppendLine("        <table class='meta-table'>");
                html.AppendFormat("            <tr><th>Time of Incident</th><td>{0}</td></tr>\n", DateTime.Now.ToString("F"));
                html.AppendFormat("            <tr><th>Exception Type</th><td>{0}</td></tr>\n", ex.GetType().FullName);
                html.AppendFormat("            <tr><th>Target Method</th><td>{0}</td></tr>\n", ex.TargetSite != null ? ex.TargetSite.ToString() : "N/A");
                html.AppendFormat("            <tr><th>Source Assembly</th><td>{0}</td></tr>\n", ex.Source);
                html.AppendLine("        </table>");

                // The Absolute Core Error Description
                html.AppendLine("        <h3>Error Message</h3>");
                html.AppendFormat("        <div class='error-message'>{0}</div>\n", WebUtility.HtmlEncode(ex.Message));

                // The Stack Trace (Formatted for clean layout)
                html.AppendLine("        <h3>Stack Trace (Where the error occurred)</h3>");
                html.AppendLine("        <pre>");

                // Highlight line numbers in the stack trace text string
                string encodedStackTrace = WebUtility.HtmlEncode(ex.StackTrace ?? "No stack trace details available.");
                string highlightedStackTrace = encodedStackTrace.Replace("line", "<span class='highlight-line'>line</span>");

                html.AppendLine(highlightedStackTrace);
                html.AppendLine("        </pre>");

                // Check for deeper Inner Exceptions (e.g., SqlException details inside a Business layer failure)
                if (ex.InnerException != null)
                {
                    html.AppendLine("        <h3 style='margin-top:30px; color:#e056fd;'>Inner Exception Details</h3>");
                    html.AppendFormat("        <div class='error-message' style='border-left-color:#e056fd; color:#6c5ce7; background-color:#f8f7ff;'>{0}</div>\n", WebUtility.HtmlEncode(ex.InnerException.Message));
                    html.AppendLine("        <pre style='background-color:#1e272e;'>");
                    html.AppendLine(WebUtility.HtmlEncode(ex.InnerException.StackTrace ?? ""));
                    html.AppendLine("        </pre>");
                }

                html.AppendLine("    </div>");
                html.AppendLine("</body>");
                html.AppendLine("</html>");

                // 4. Save the completed document string to disk
                File.WriteAllText(fullPath, html.ToString(), Encoding.UTF8);
                return fullPath;
            }
            catch
            {
                // Fallback failsafe in case the logging operation itself fails (e.g., hard drive write permission restrictions)
                return "Failed to write HTML log file.";
            }
        }
    }
}

