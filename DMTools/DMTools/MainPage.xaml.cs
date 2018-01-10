using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DMTools
{
    public sealed partial class MainPage : Page
    {
        ItemCollection nameslist;

        public MainPage()
        {
            this.InitializeComponent();
            List.ItemsSource = nameslist;

            RemoveButton.IsEnabled = false;
            NextButton.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text;
            int roll = 0;
            if (RollBox.Text != "")
            {
                roll = Convert.ToInt32(RollBox.Text);
            }

            Character s = new Character(name, roll);
            nameslist = List.Items;
            nameslist.Add(s);
            List.SelectedItem = 0;

            NameBox.Text = "";
            RollBox.Text = "";
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(List.SelectedIndex == -1)
            {
                RemoveButton.IsEnabled = false;
            }
            else
            {
                RemoveButton.IsEnabled = true;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if(List.SelectedIndex != -1)
            {
                List.Items.RemoveAt(List.SelectedIndex);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //sort the list, lock the add button, make top-most the active selection
            List<Character> Characters = List.Items.OfType<Character>().ToList();

            Characters = Characters.OrderBy(a => a.Roll).ToList();
            Characters.Reverse();

            nameslist.Clear();

            foreach(Character c in Characters)
            {
                nameslist.Add(c);
            }

            List.SelectedIndex = 0;

            // enable/disable buttons
            AddButton.IsEnabled = false;
            NextButton.IsEnabled = true;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // select the next in the list, if bottom then cycle to top
            if(List.SelectedIndex + 1 < List.Items.Count())
            {
                List.SelectedIndex++;
            }
            else
            {
                List.SelectedIndex = 0;
            }
        }

        private void RollBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            RoutedEventArgs e2 = new RoutedEventArgs();

            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                AddButton_Click(sender, e2);
                NameBox.Focus(FocusState.Pointer);
                e.Handled = true;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            AddButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            NextButton.IsEnabled = false;
        }
    }

    class Character
    {
        public string Name { get; set; }
        public int Roll { get; set; }

        public Character(string name, int roll)
        {
            Name = name;
            Roll = roll;
        }
    }
}
