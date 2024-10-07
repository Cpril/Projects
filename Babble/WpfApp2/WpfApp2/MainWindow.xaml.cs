using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections;

namespace BabbleSample
{
    public partial class MainWindow : Window
    {
        private string input; // input file
        private string[] words; // input file broken into array of words
        private int wordCount = 200; // number of words to babble
        private Dictionary<string, ArrayList> hashtable; 
        private Random random = new Random(); // Random number generator

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
                input = File.ReadAllText(ofd.FileName); // read file
                words = Regex.Split(input, @"\s+"); // split into array of words

                int totalWords = words.Length; 
                int uniqueWords = words.Distinct().Count();
                textBlock1.Text += $"Total Words: {totalWords}\n"; // display word count
                textBlock1.Text += $"Unique Words: {uniqueWords}\n\n"; // display unique word count
                textBlock1.Text += "Original Text:\n" + input + "\n\n"; // display the text

                hashtable = HashExample.makeHashtable(words, orderComboBox.SelectedIndex);
            }
        }

        private void analyzeInput(int order) // analyze the order and display babble.
        {
            if (words == null) return;
            // if order 0, just show the text
            if (order == 0)
            {
                for (int i = 0; i < Math.Min(wordCount, words.Length); i++)
                {
                    textBlock1.Text += " " + words[i];
                }
            }
            else if (order == 1 && hashtable != null)
            {
                int rand_index = random.Next(words.Length-1);
                string currentword = words[rand_index];
                for (int i = 0; i < wordCount; i++)
                {
                    textBlock1.Text += " " + currentword;

                    if (hashtable.ContainsKey(currentword))
                    {
                        var following_words = hashtable[currentword];
                        if (following_words.Count > 0)
                        {
                            currentword = (string)following_words[random.Next(following_words.Count)];
                        }
                        else { currentword = words[random.Next(words.Length-1)]; }
                    }
                    else
                    {
                        currentword = words[random.Next(words.Length-1)];
                    }
                }
            }
            else if (order == 2 && hashtable != null) // Order 2
            {
                if (words.Length < 2) return; // If there're less than 2 words, just return.
                int rand_index = random.Next(words.Length - 2);
                string currentword = words[rand_index] + " " + words[rand_index + 1];
                for (int i = 0; i < wordCount; i++)
                {
                    textBlock1.Text += " " + currentword;
                    if (hashtable.ContainsKey(currentword))
                    {
                        var following_words = hashtable[currentword];
                        if (following_words.Count > 0)
                        {
                            string nextWord = (string)following_words[random.Next(following_words.Count)];
                            currentword = currentword.Split(' ')[1] + " " + nextWord;
                        }
                        else
                        {
                            rand_index = random.Next(words.Length - 1);
                            currentword = words[rand_index] + " " + words[rand_index + 1];
                        }
                    }
                    else
                    {
                        // If currentword is not in the hashtable, choose a new random pair
                        rand_index = random.Next(words.Length - 1);
                        currentword = words[rand_index] + " " + words[rand_index + 1];
                    }
                }
            }
            else if (order == 3 && hashtable != null) // Order 3
            {
                if (words.Length < 3) return; // If there're less than three words, just return
                int rand_index = random.Next(words.Length - 3);
                string currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2];
                for (int i = 0; i < wordCount; i++)
                {
                    textBlock1.Text += " " + currentword;
                    if (hashtable.ContainsKey(currentword))
                    {
                        var following_words = hashtable[currentword];
                        if (following_words.Count > 0)
                        {
                            string nextWord = (string)following_words[random.Next(following_words.Count)];
                            currentword = currentword.Split(' ')[1] + " " + currentword.Split(' ')[2] + " "+ nextWord;
                        }
                        else
                        {
                            rand_index = random.Next(words.Length - 3);
                            currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2];
                        }
                    }
                    else
                    {
                        rand_index = random.Next(words.Length - 3);
                        currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2];
                    }
                }
            }
            else if (order == 4 && hashtable != null) // Order 4
            {
                if (words.Length < 4) return; // If there're less than 4 words, just return
                int rand_index = random.Next(words.Length - 4);
                string currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2] + " " + words[rand_index + 3];
                for (int i = 0; i < wordCount; i++)
                {
                    textBlock1.Text += " " + currentword;
                    if (hashtable.ContainsKey(currentword))
                    {
                        var following_words = hashtable[currentword];
                        if (following_words.Count > 0)
                        {
                            string nextWord = (string)following_words[random.Next(following_words.Count)];
                            currentword = currentword.Split(' ')[1] + " " + currentword.Split(' ')[2] + " " + currentword.Split(' ')[3] + " " + nextWord;
                        }
                        else
                        {
                            rand_index = random.Next(words.Length-4);
                            currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2] + " " + words[rand_index + 3];
                        }
                    }
                    else
                    {
                        rand_index = random.Next(words.Length - 4);
                        currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2] + " " + words[rand_index + 3];
                    }
                }
            }
            else if (order == 5 && hashtable != null) // Order 5
            {
                if (words.Length < 3) return; // If there're less than 5 words, just return
                int rand_index = random.Next(words.Length - 5);
                string currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2] + " " + words[rand_index + 3] + " " + words[rand_index + 4];
                for (int i = 0; i < wordCount; i++)
                {
                    textBlock1.Text += " " + currentword;
                    if (hashtable.ContainsKey(currentword))
                    {
                        var following_words = hashtable[currentword];
                        if (following_words.Count > 0)
                        {
                            string nextWord = (string)following_words[random.Next(following_words.Count)];
                            currentword = currentword.Split(' ')[1] + " " + currentword.Split(' ')[2] + " " + currentword.Split(' ')[3] + " " + currentword.Split(' ')[4] + " " + nextWord;
                        }
                        else
                        {
                            rand_index = random.Next(words.Length - 5);
                            currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2] + " " + words[rand_index + 3] + " " + words[rand_index + 4];
                        }
                    }
                    else
                    {
                        rand_index = random.Next(words.Length - 5);
                        currentword = words[rand_index] + " " + words[rand_index + 1] + " " + words[rand_index + 2] + " " + words[rand_index + 3] + " " + words[rand_index + 4];
                    }
                }
            }
        }

        private void babbleButton_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = ""; // clear the space for the next babble.
            analyzeInput(orderComboBox.SelectedIndex);
        }

        private void orderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderComboBox.SelectedIndex >= 0) 
            {
                // babble again.
                analyzeInput(orderComboBox.SelectedIndex);
            }
        }
    }

    class HashExample
    {
        public static Dictionary<string, ArrayList> makeHashtable(String[] words, int order) // making the hashtable
        {
            Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();
            if (order == 1)
            {
                for (int i = 0; i < words.Length - 1; i++)
                {
                    string current_word = words[i];
                    string following_word = words[i + 1];
                    if (!hashTable.ContainsKey(current_word)) // if word not found, add into the hashtable
                    {
                        hashTable.Add(current_word, new ArrayList());
                    }
                    hashTable[current_word].Add(following_word);
                }
            }
            else if (order == 2)
            {
                for (int i = 0; i < words.Length - 2; i++)
                {
                    string current_word = words[i] + " " + words[i + 1];
                    string following_word = words[i + 2];
                    if (!hashTable.ContainsKey(current_word))
                    {
                        hashTable.Add(current_word, new ArrayList());
                    }
                    hashTable[current_word].Add(following_word);
                }
            }
            else if (order == 3)
            {
                for (int i = 0; i < words.Length - 3; i++)
                {
                    string current_word = words[i] + " " + words[i + 1] + " " + words[i + 2];
                    string following_word = words[i + 3];
                    if (!hashTable.ContainsKey(current_word))
                    {
                        hashTable.Add(current_word, new ArrayList());
                    }
                    hashTable[current_word].Add(following_word);
                }
            }
            else if (order == 4)
            {
                for (int i = 0; i < words.Length - 4; i++)
                {
                    string current_word = words[i] + " " + words[i + 1] + " " + words[i + 2] + " " + words[i + 3];
                    string following_word = words[i + 4];
                    if (!hashTable.ContainsKey(current_word))
                    {
                        hashTable.Add(current_word, new ArrayList());
                    }
                    hashTable[current_word].Add(following_word);
                }
            }
            else if (order == 5)
            {
                for (int i = 0; i < words.Length - 5; i++)
                {
                    string current_word = words[i] + " " + words[i + 1] + " " + words[i + 2] + " " + words[i + 3] + " " + words[i + 4];
                    string following_word = words[i + 5];
                    if (!hashTable.ContainsKey(current_word))
                    {
                        hashTable.Add(current_word, new ArrayList());
                    }
                    hashTable[current_word].Add(following_word);
                }
            }
            return hashTable;
        }
    }
}
