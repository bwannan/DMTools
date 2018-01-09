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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DMTools
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ItemCollection nameslist;
        ItemCollection temp;

        public MainPage()
        {
            this.InitializeComponent();
            List.ItemsSource = nameslist;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Character s = new Character("", 0);
            nameslist = List.Items;
            nameslist.Add(s);
            List.SelectedItem = 0;
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Character c = new Character("", 0);
            if(List.SelectedValue != null)
            {
                c = (Character)List.SelectedValue;
            }
            NameBox.Text = c.Name;
            RollBox.Text = c.Roll.ToString();
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List.SelectedItem = 0;

            var lst = List.Items.OfType<Character>().ToList();
            lst.ElementAt(List.SelectedIndex).Name = NameBox.Text;


            nameslist.Clear();
            foreach (object a in lst)
            {
                nameslist.Add(a);
            }

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
