using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BabbleSample
{
    /// Babble framework
    /// Starter code for CS212 Babble assignment
    public partial class MainWindow : Window
    {
        private string input;               // input file
        private string[] words;             // input file broken into array of words
        private int wordCount = 200;        // number of words to babble

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.FileName = "Sample"; // Default file name
            ofd.DefaultExt = ".txt"; // Default file extension
            ofd.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            if ((bool)ofd.ShowDialog())
            {
                textBlock1.Text = "Loading file " + ofd.FileName + "\n";
                input = System.IO.File.ReadAllText(ofd.FileName);  // read file
                words = Regex.Split(input, @"\s+");       // split into array of words
            }
        }
        private Dictionary<string, ArrayList> makeHashtable()
        {
            Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();
            if (orderComboBox.SelectedIndex == 1)
            {
                for (int i = 0; i < words.Length - 1; i++)
                {
                    string oneWord = words[i];
                    if (!hashTable.ContainsKey(oneWord))
                        hashTable.Add(oneWord, new ArrayList());
                    hashTable[oneWord].Add(words[i + 1]);
                }
            } else if (orderComboBox.SelectedIndex == 2)
            {
                for (int i = 1; i < words.Length - 1; i++)
                {
                    string twoWords = words[i - 1] + " " + words[i];
                    if (!hashTable.ContainsKey(twoWords))
                        hashTable.Add(twoWords, new ArrayList());
                    hashTable[twoWords].Add(words[i + 1]);
                }
            } else if (orderComboBox.SelectedIndex == 3)
            {
                for (int i = 2; i < words.Length - 1; i++)
                {
                    string threeWords = words[i - 2] + " " + words[i - 1] + " " + words[i];
                    if (!hashTable.ContainsKey(threeWords))
                        hashTable.Add(threeWords, new ArrayList());
                    hashTable[threeWords].Add(words[i + 1]);
                }
            } else if (orderComboBox.SelectedIndex == 4)
            {
                for (int i = 3; i < words.Length - 1; i++)
                {
                    string fourWords = words[i - 3] + " " + words[i - 2] + " " + words[i - 1] + " " + words[i];
                    if (!hashTable.ContainsKey(fourWords))
                        hashTable.Add(fourWords, new ArrayList());
                    hashTable[fourWords].Add(words[i + 1]);
                }
            }
            return hashTable;

            /*
            for (int i = 1; i < names.Length - 1; i++)
            {
                string twoWord = names[i-1]+" "+names[i];
                if (!hashTable.ContainsKey(twoWord))
                    hashTable.Add(twoWord, new ArrayList());
                hashTable[twoWord].Add(names[i+1]);
            }
            return hashTable;
            */
            /*
            for (int i = 2; i < words.Length - 1; i++)
            {
                string threeWord = words[i - 2] + " " + words[i - 1] + " " + words[i];
                if (!hashTable.ContainsKey(threeWord))
                    hashTable.Add(threeWord, new ArrayList());
                hashTable[twoWord].Add(words[i + 1]);
            }
            return hashTable;
            */
        }
        private void analyzeInput(int order)
        {
            if (order > 0)
            {
                MessageBox.Show("Analyzing at order: " + order);
            }
        }
        
        private void babbleButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (orderComboBox.SelectedIndex == 1)
            {
                for (int i = 0; i < Math.Min(wordCount, words.Length); i++)
                    
                    textBlock1.Text += " " + words[i];
            } else if (orderComboBox.SelectedIndex > 0)
            {

                foreach (KeyValuePair<string, ArrayList> entry in makeHashtable())
                {
                    textBlock1.Text += (entry.Key + "->");
                    foreach (string name in entry.Value)
                        textBlock1.Text += (name + " ");
                    textBlock1.Text += "\n";
                }
            

                    //Dictionary<string, ArrayList> hashTable = makeHashtable();


                    /*
                    Random random = new Random();
                    string key = hashTable.Keys.ElementAt(0);
                    int length = key.Length;
                    int ranNum = random.Next(0, length);
                    string value = hashTable.Keys.ElementAt(0).;
                    for (int i = 0; i < Math.Min(wordCount, words.Length); i++)

                        textBlock1.Text += " " + hashTable[key];

                                foreach (KeyValuePair<string, ArrayList> entry in makeHashtable())
                    {
                        textBlock1.Text += (entry.Key + "->");
                        foreach (string name in entry.Value)
                            textBlock1.Text += (name+" ");
                        textBlock1.Text += "\n";

                    }
                    */



                }
            else
            {
                for (int i = 0; i < Math.Min(wordCount, words.Length); i++)

                    textBlock1.Text += " " + words[i];
            }
            
            
        }

        private void orderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            analyzeInput(orderComboBox.SelectedIndex);
        }
    }
}

