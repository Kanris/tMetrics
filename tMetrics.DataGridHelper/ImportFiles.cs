using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tMetrics.DataGridHelper
{
    public class ImportFiles
    {
        private string nameOfProject;
        private FileInfo[] pathToSource;
        private const string FILE_FORMAT = "*.java";

        public ImportFiles(string pathToProject)
        {
            DirectoryInfo countInfo = new DirectoryInfo(pathToProject);
            pathToSource = countInfo.GetFiles(FILE_FORMAT, SearchOption.AllDirectories);
            nameOfProject = countInfo.Name;
        }

        public FileInfo[] getPathToSource()
        {
            if (pathToSource != null) return pathToSource;

            return null;
        }

        public string getNameOfProject()
        {
            return nameOfProject;
        }

    }
}
