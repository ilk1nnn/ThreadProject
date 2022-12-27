using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Commands;
using WpfApp1.Entities;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private TextBox myTextbox;

        public TextBox MyTextbox
        {
            get { return myTextbox; }
            set { myTextbox = value; OnPropertyChanged(); }
        }

        public RelayCommand AddCommand { get; set; }



        private ObservableCollection<string> allDatas;

        public ObservableCollection<string> AllDatas
        {
            get { return allDatas; }
            set { allDatas = value; OnPropertyChanged(); }
        }




        public RelayCommand PlayCommand { get; set; }
        public RelayCommand ResumeCommand { get; set; }
        public RelayCommand StopCommand { get; set; }
        public RelayCommand PauseCommand { get; set; }

        public MainViewModel()
        {

            var datas = Data.GetAll();
            AllDatas = new ObservableCollection<string>();
            foreach (var data in datas)
            {
                AllDatas.Add(data.Name);
            }


            AddCommand = new RelayCommand(a =>
            {
                Data data = new Data
                {
                    Name = MyTextbox.Text
                };
                AllDatas.Add(data.Name);
            });




   

        }

    }
}
