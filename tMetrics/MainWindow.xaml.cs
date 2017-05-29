using System.Windows;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using tMetrics.Library;
using tMetrics.DataGridHelper;
using System.Threading.Tasks;

namespace tMetrics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

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

        private async void btnImportProject_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openNewProject = new FolderBrowserDialog();

            var result = openNewProject.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var pathToProject = openNewProject.SelectedPath;
                var importFiles = new ImportFiles(pathToProject);

                FileInfo[] pathToSource = importFiles.getPathToSource();

                var lengthOfCode = await Task.Run(() => Metrics.LOC(pathToSource));
                var cyclomatic = await Task.Run(() => Metrics.CYC(pathToSource));

                SetTitle(importFiles.getNameOfProject());

                DGHelper.CreateGrid(lengthOfCode, cyclomatic, pathToSource, importFiles, _myDataGrid);

            }
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
