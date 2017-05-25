﻿using System.Windows;
using System.IO;
using System.Windows.Forms;
using System.Threading;


namespace tMetrics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImportFiles importFiles;

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

        private void btnImportProject_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openNewProject = new FolderBrowserDialog();

            var result = openNewProject.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string pathToProject = openNewProject.SelectedPath;
                importFiles = new ImportFiles(pathToProject);

                FileInfo[] pathToSource = importFiles.getPathToSource();

                int[] cyclomatic = new int[pathToSource.Length], 
                    lengthOfCode = new int[pathToSource.Length];

                Thread locThread = new Thread(() => Metrics.LOC(pathToSource, out lengthOfCode));
                locThread.Start();

                Thread cycThread = new Thread(() => Metrics.CYC(pathToSource, out cyclomatic));
                cycThread.Start();

                SetTitle(importFiles.getNameOfProject());

                locThread.Join();
                cycThread.Join();

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
