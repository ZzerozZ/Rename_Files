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
            bool isApply = (MessageBoxResult.Yes == MessageBox.Show("Are you sure for rename all files on selected folder?\nYou can't go back when apply this request!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes));

            if (isApply)
            {
                //Do it!
                if (txbFolder.Text != "")
                {
                    string path = txbFolder.Text;
                    //Get anyfile in this folder:
                    FileInfo[] file = (new DirectoryInfo(@path)).GetFiles();

                    //Rename and change file format: 
                    foreach (FileInfo info in file)
                    {
                        info.MoveTo(@path + "//" + newName(info.Name));
                        //If error existed, stop this event:
                        if (!done)
                            return;
                    }

                    //Clear data in Window:
                    txbNewFormat.Text = txbRemoveFrom.Text = txbRemoveTo.Text = txbReplace.Text = txbReplaceBy.Text = "";
                   
                    MessageBox.Show("Done!");

                }
            }
        }

        /// <summary>
        /// Change the name of file
        /// </summary>
        /// <param name="oldName">Old name of file</param>
        /// <returns>New name</returns>
        public string newName(string oldName)
        {
            done = true;
            string newname = "";

            //Delete string from txbRemoveFrom to txbRemoveTo:
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


            //Replace string txbReplace by txbReplaceBy:
            if (txbReplace.Text != "")
                newname = newname.Replace(txbReplace.Text, txbReplaceBy.Text);
            else
                newname = oldName;

            //Change the file format:
            if (txbNewFormat.Text != "")
            {
                newname = newname.Remove(newname.LastIndexOf('.'), newname.Length - newname.LastIndexOf('.')) + txbNewFormat.Text;
            }

            return newname;
        }
    }
}
