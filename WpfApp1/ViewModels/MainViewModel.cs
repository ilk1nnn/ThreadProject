using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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



        private ObservableCollection<string> allConvertedDatas;

        public ObservableCollection<string> AllConvertedDatas
        {
            get { return allConvertedDatas; }
            set { allConvertedDatas = value; OnPropertyChanged(); }
        }


        public Thread thread { get; set; }
        public DispatcherTimer timer { get; set; }
        public RelayCommand PlayCommand { get; set; }
        public RelayCommand ResumeCommand { get; set; }
        public RelayCommand StopCommand { get; set; }
        public RelayCommand PauseCommand { get; set; }


        

        public bool IsSuspended { get; set; }

        public MainViewModel()
        {
            IsSuspended = false;
            var datas = Data.GetAll();
            AllConvertedDatas = new ObservableCollection<string>();
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
                MyTextbox.Text = String.Empty;
            });


            PlayCommand = new RelayCommand(p =>
            {
                thread.Start();
            },
            (p) =>
            {
                return !thread.IsAlive;
            });


            PauseCommand = new RelayCommand(pa =>
            {
                thread.Suspend();
                IsSuspended= true;
            },
            (p) =>
            {
                return !IsSuspended;
            });


            ResumeCommand = new RelayCommand(r =>
            {
                thread.Resume();
                IsSuspended = false;
            },
            (p) =>
            {
                return IsSuspended;
            });



            StopCommand = new RelayCommand(s =>
            {
                thread.Abort();
            },
            (p) =>
            {
                return thread.IsAlive && !IsSuspended;
            });



            thread = new Thread(() => { Converting(); });


        }


        public void Converting()
        {
            while (true)
            {
                if(AllDatas.Count>0)
                {
                    for (int i = 0; i < AllDatas.Count; i++)
                    {
                        App.Current.Dispatcher.BeginInvoke((Action)delegate
                        {
                            string converted_text = "123" + AllDatas[i] + "456";
                            AllConvertedDatas.Add(converted_text);
                            AllDatas.RemoveAt(i);
                            i -= 1;
                            if(AllDatas.Count == 0)
                            {
                                MessageBox.Show("Process Ended Successfully!");
                            }
                        });
                        Thread.Sleep(1000);
                    }
                }
            }
        }

    }
}
