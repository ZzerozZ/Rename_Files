using System;
using System.Collections.Generic;
using System.IO;
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

namespace RenameFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool done = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool result =  (MessageBoxResult.Yes == MessageBox.Show("Are you sure for rename all files on selected folder?\nYou can't go back when apply this request!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes));
            if (result)
            {
                //Do it!
                if (txbFolder.Text != "")
                {
                    string path = txbFolder.Text;
                    FileInfo[] file = (new DirectoryInfo(@path)).GetFiles();

                    foreach (FileInfo info in file)
                    {
                        info.MoveTo(@path + "//" + newName(info.Name));
                        if (!done)
                            return;
                    }


                }
            }
        }

        public string newName(string oldName)
        {
            done = true;
            string newname = "";

            string[] request = txbNameFormat.Text.Split(',');

            try
            {
                if (request[0] != "" && request[1] != "")
                    newname = oldName.Remove(oldName.IndexOf(request[0]), oldName.IndexOf(request[1]) + request[1].Length - oldName.IndexOf(request[0]));
                else
                    newname = oldName;
            }
            catch(Exception ex)
            {
                MessageBox.Show("You must be typing exactly request!\n\n\nError code: " + ex.ToString());
                done = false;
                return oldName;
            }

            if(request.Length > 2)
            {
                if (request[2] != "")
                    newname = newname.Replace(request[2], request[3]);
                else
                    newname = oldName;
            }

            if(request.Length > 4)
            {
                newname = newname.Remove(newname.LastIndexOf('.'), newname.Length - newname.LastIndexOf('.')) + request[4];
            }

            return newname;
        }
    }
}
