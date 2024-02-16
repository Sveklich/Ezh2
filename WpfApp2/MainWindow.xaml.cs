using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Newtonsoft.Json;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private Dictionary<DateTime, List<todo>> todoNote;

        public MainWindow()
        {
            InitializeComponent();
            todoNote = new Dictionary<DateTime, List<todo>>();
            DatePicker.SelectedDate = DateTime.Today;
            LoadList();
            UpdateList();

            TodoList.DisplayMemberPath = "Name";
            TodoList.SelectionChanged += TodoList_Select;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Today;

            todo newNote = new(Name.Text, Desc.Text, selectedDate);
            if (todoNote.ContainsKey(selectedDate))
            {
                todoNote[selectedDate].Add(newNote);
            }
            else
            {
                todoNote[selectedDate] = new List<todo>() { newNote };
            }
            Name.Text = "";
            Desc.Text = "";
            UpdateList();
            SaveList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            todo selectNote = (todo)TodoList.SelectedItem;

            if (selectNote != null)
            {
                selectNote.Name = Name.Text;
                selectNote.Description = Desc.Text;
                selectNote.Date = DatePicker.SelectedDate ?? DateTime.Today;

                UpdateList();
                SaveList();
            }
        }

        private void Delete__Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Today;

            if (todoNote.ContainsKey(selectedDate))
            {
                todoNote[selectedDate].Remove((todo)TodoList.SelectedItem);

                UpdateList();
                SaveList();
            }
        }
        private void TodoList_Select(object sender, SelectionChangedEventArgs e)
        {
            if (TodoList.SelectedItem != null)
            {
                todo selectNote = (todo)TodoList.SelectedItem;

                Name.Text = selectNote.Name;
                Desc.Text = selectNote.Description;
            }
            else
            {
                Name.Text = "";
                Desc.Text = "";
            }
        }

        private void UpdateList()
        {
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Today;

            if (todoNote.ContainsKey(selectedDate))
            {
                TodoList.ItemsSource = todoNote[selectedDate];
            }
            else
            {
                TodoList.ItemsSource = null;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void SaveList()
        {
            Load.Serialize(todoNote, "TodoList.json");
        }

        private void LoadList()
        {
            todoNote = Load.Deserialize("TodoList.json");
        }
    }
}
