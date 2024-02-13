using System;
using System.Collections.Generic;
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
using System.IO;
using System.Collections;

namespace planner
{
    public partial class MainWindow : Window
    {
        private List<Note> notes = new List<Note>();
        private string fileName = "notes.json";

        public MainWindow()
        {
            InitializeComponent();
            notes = SerDer.DeserData<Note>(fileName);
            Dtp.SelectedDate = DateTime.Today;
            Dtp.SelectedDateChanged += Dtp_SelectionChanged;
            Dtp_SelectionChanged(null, null);
        }

        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note();
            if (Lbx.SelectedItem is Note selectedNote)
            {
                note.Title = TitleTbx.Text;
                note.Description = DisTbx.Text;
                note.Date = Convert.ToDateTime(Dtp.Text);

                int index = notes.IndexOf(selectedNote);
                if (index != -1)
                {
                    notes[index] = note;
                }

                SerDer.SerData(notes, fileName);
                Dtp_SelectionChanged(null, null);
            }
            else
            {
                if (notes == null)
                {
                    notes = new List<Note>();
                }
                note.Title = TitleTbx.Text;
                note.Description = DisTbx.Text;
                note.Date = Convert.ToDateTime(Dtp.Text);
                notes.Add(note);
                SerDer.SerData<Note>(notes, fileName);
                Dtp_SelectionChanged(null, null);
                
            }


        }

        private void Dtp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = Convert.ToDateTime(Dtp.SelectedDate);

            List<Note> notedate = null;
            if (notes != null && notes.Any())
            {
                notedate = notes.Where(note => note.Date == selectedDate).ToList();
            }
            if (notedate != null)
            {
            }

            Lbx.ItemsSource = notedate;
            Clear_Click(null, null);

        }

        private void Lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lbx.SelectedItem != null)
            {
                Note selec = (Note)Lbx.SelectedItem;
                Note selectedNote = notes.FirstOrDefault(note => note.Title == selec.Title);

                if (selectedNote != null)
                {
                    TitleTbx.Text = selectedNote.Title;
                    DisTbx.Text = selectedNote.Description;
                    Dtp.SelectedDate = selectedNote.Date;
                }
            }
        }

        private void TitleTbx_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DisTbx_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Lbx.SelectedItem = null;
            TitleTbx.Text = "";
            DisTbx.Text = "";
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Note note = (Note)Lbx.SelectedItem;
            notes.Remove(note);
            SerDer.SerData<Note>(notes, fileName);
            Dtp_SelectionChanged(null, null);
        }
    }
}
