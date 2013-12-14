using System;
using System.Collections.Generic;
using System.IO;

using xml_search_app.Models;

namespace xml_search_app.ExportEngines
{
    public class TxtExportEngine : IExportEngine
    {
        private string _path = "";

        public void Export(List<BookItem> resultsList)
        {
            string contents = "";

            foreach(var item in resultsList)
            {
                contents += "##########" + Environment.NewLine + Environment.NewLine;
                contents += "Full name: " + item.Name.LastName + " " + item.Name.FirstName + ". " + item.Name.MiddleName + "." + Environment.NewLine;
                contents += "Address: " + item.Address.City + ", " + item.Address.House + " "
                    + item.Address.Street + ", apt. " + item.Address.Apartment + Environment.NewLine;
                contents += "Phone number: " + item.PhoneNumber + Environment.NewLine + Environment.NewLine;
            }

            try
            {
                File.WriteAllText(_path, contents);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SetPathToExport(string path)
        {
            _path = Path.Combine(path, ProgramValues.EXPORT_ENGINE_FILE_NAME + ".txt");
        }
    }
}
