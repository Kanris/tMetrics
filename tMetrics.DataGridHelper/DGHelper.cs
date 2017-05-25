using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace tMetrics.DataGridHelper
{
    public static class DGHelper
    {
        public static Item createDataGridItem(string name, int loc, int cyc)
        {
            return new Item() { FullName = name, LOC = loc, CYC = cyc };
        }

        public static ObservableCollection<Item> CreateListForDataGrid(int[] lenthOfCode, int[] cyclomatic, FileInfo[] pathToSource, ImportFiles importFiles)
        {
            ObservableCollection<Item> list = new ObservableCollection<Item>();

            string projectName = importFiles.getNameOfProject();
            int globalLoc = 0;
            int globalCyc = 0;

            for (int i = 0; i < pathToSource.Length; ++i)
            {
                list.Add(createDataGridItem(pathToSource[i].Name, lenthOfCode[i], cyclomatic[i]));
                globalLoc += lenthOfCode[i];
                globalCyc += cyclomatic[i];
            }

            list.Add(new Item() { FullName = projectName, LOC = globalLoc, CYC = globalCyc });

            return list;
        }

        public static void CreateGrid(int[] lenthOfCode, int[] cyclomatic, FileInfo[] pathToSource, ImportFiles importFiles, DataGrid myDataGrid)
        {
            var list = DGHelper.CreateListForDataGrid(lenthOfCode, cyclomatic, pathToSource, importFiles);

            myDataGrid.ItemsSource = list;
            myDataGrid.Items.Refresh();
        }
    }
}
