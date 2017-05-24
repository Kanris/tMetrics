using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;


namespace tMetrics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImportFiles importFiles;
        private Metrics metrics = new Metrics();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseApplication()
        {
            Close();
        }

        private void ShowHelp()
        {
            string infoCYC = "CYC - Число лінійно - незалижних шляхів через операцію";
            string infoLOC = "LOC - Кільксть рядків куду методу включаючи коментарі";
            System.Windows.MessageBox.Show($"{infoLOC}\n{infoCYC}", "About program");
        }

        private void SetTitle(string projectName)
        {
            this.Title = $"Project name: {projectName}";
        }

        private void LOC(FileInfo[] pathToFile, out int[] lengthOfCode)
        {
            lengthOfCode = new int[pathToFile.Length];

            for (int i = 0; i < lengthOfCode.Length; ++i)
            {
                lengthOfCode[i] = metrics.LOC(pathToFile[i].FullName);
            }
        }

        private void CYC(FileInfo[] pathToFile, out int[] cyclomatic)
        {
            cyclomatic = new int[pathToFile.Length];

            for (int i = 0; i < cyclomatic.Length; ++i)
            {
                cyclomatic[i] = metrics.CYC(pathToFile[i].FullName);
            }
        }

        private void btnImportProject_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openNewProject = new FolderBrowserDialog();

            var result = openNewProject.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string pathToProject = openNewProject.SelectedPath;
                importFiles = new ImportFiles(pathToProject);

                FileInfo[] pathToSource = importFiles.getPathToSource();

                int[] cyclomatic = new int[pathToSource.Length], 
                    lengthOfCode = new int[pathToSource.Length];

                Thread locThread = new Thread(() => LOC(pathToSource, out lengthOfCode));
                locThread.Start();

                Thread cycThread = new Thread(() => CYC(pathToSource, out cyclomatic));
                cycThread.Start();

                SetTitle(importFiles.getNameOfProject());

                locThread.Join();
                cycThread.Join();

                CreateGrid(lengthOfCode, cyclomatic, pathToSource);

            }
        }

        private void CreateGrid(int[] lenthOfCode, int[] cyclomatic, FileInfo[] pathToSource)
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

            list.Add(new Item() { FullName= projectName, LOC = globalLoc, CYC = globalCyc });

            _myDataGrid.ItemsSource = list;
            _myDataGrid.Items.Refresh();
        }

        private Item createDataGridItem(string name, int loc, int cyc)
        {
            return new Item() { FullName = name, LOC = loc, CYC = cyc };
        }

        public class Item
        {
            public string FullName { get; set; }
            public int LOC { get; set; }
            public int CYC { get; set; }
        }

        private void btnExit_OnClick(object sender, RoutedEventArgs e)
        {
            CloseApplication();
        }

        private void btnHelp_OnClick(object sender, RoutedEventArgs e)
        {
            ShowHelp();
        }
    }
}
