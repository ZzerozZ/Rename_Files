using System;
using System.IO;
using System.Windows;

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

            try
            {
                if (txbRemoveFrom.Text != "" && txbRemoveTo.Text != "")
                    newname = oldName.Remove(oldName.IndexOf(txbRemoveFrom.Text), oldName.IndexOf(txbRemoveTo.Text) + txbRemoveTo.Text.Length - oldName.IndexOf(txbRemoveFrom.Text));
                else
                    newname = oldName;
            }
            catch(Exception ex)
            {
                MessageBox.Show("You must be typing exactly request!\n\n\nError code: " + ex.ToString());
                done = false;
                return oldName;
            }
            
                if (txbReplace.Text != "")
                    newname = newname.Replace(txbReplace.Text, txbReplaceBy.Text);
                else
                    newname = oldName;

            if(txbNewFormat.Text != "")
            {
                newname = newname.Remove(newname.LastIndexOf('.'), newname.Length - newname.LastIndexOf('.')) + txbNewFormat.Text;
            }

            return newname;
        }
    }
}
