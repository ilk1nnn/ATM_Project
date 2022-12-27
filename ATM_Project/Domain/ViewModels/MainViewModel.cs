using ATM_Project.DataAccess.Entities;
using ATM_Project.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ATM_Project.Domain.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand InsertCardCommand { get; set; }
        public RelayCommand LoadDataCommand { get; set; }
        public RelayCommand TransferMoneyCommand { get; set; }

        private TextBox myTextbox;

        public TextBox MyTextBox
        {
            get { return myTextbox; }
            set { myTextbox = value; OnPropertyChanged(); }
        }


        private Button myButton;

        public Button MyButton
        {
            get { return myButton; }
            set { myButton = value; OnPropertyChanged(); }
        }


        private TextBlock myTextBlock;

        public TextBlock MyTextBlock
        {
            get { return myTextBlock; }
            set { myTextBlock = value; OnPropertyChanged(); }
        }


        private ObservableCollection<User> allUsers;

        public ObservableCollection<User> AllUsers
        {
            get { return allUsers; }
            set { allUsers = value; }
        }


        private TextBox moneyTxtb;

        public TextBox MoneyTxtb
        {
            get { return moneyTxtb; }
            set { moneyTxtb = value; OnPropertyChanged(); }
        }


        private Button tbtn;

        public Button Tbtn
        {
            get { return tbtn; }
            set { tbtn = value; OnPropertyChanged(); }
        }

        public RelayCommand RefreshCommand { get; set; }


        static Mutex mutextObj = new Mutex();


        static object obj = new object();

        public MainViewModel()
        {


            var usersFromDataBase = App.DB.UserRepository.GetAll();
            AllUsers = new ObservableCollection<User>(usersFromDataBase);


            InsertCardCommand = new RelayCommand(i =>
            {
                MyTextBox.Visibility = System.Windows.Visibility.Visible;
                MyButton.Visibility = System.Windows.Visibility.Visible;
            });

            LoadDataCommand = new RelayCommand(l =>
            {
                MyTextBlock.Visibility = System.Windows.Visibility.Visible;


                for (int i = 0; i < AllUsers.Count(); i++)
                {
                    if (AllUsers[i].CardNumber == MyTextBox.Text)
                    {

                        MyTextBlock.Text = $@"-Info About User-
   Fullname is {AllUsers[i].FullName}
   Balance is {AllUsers[i].Balance} AZN";
                    }
                }


            });

            User user = new User();

            RefreshCommand = new RelayCommand(r =>
            {
        

                        MyTextBlock.Text = $@"-Info About User-
   Fullname is {user.FullName}
   Balance is {user.Balance} AZN";
                    
            });


            TransferMoneyCommand = new RelayCommand(t =>
            {


                var money = int.Parse(MoneyTxtb.Text);
                if (money >= 10)
                {

                    var ten_money = money / 10;

                    string mutexName = "MyMutex";

                    using (var m = new Mutex(false, mutexName))
                    {

                        if (!m.WaitOne(50, false))
                        {
                            MessageBox.Show("Second Instance running");

                        }
                        else
                        {
                            Tbtn.IsEnabled = false;
                            var result = 0;
                            for (int k = 0; k < AllUsers.Count; k++)
                            {
                                if (AllUsers[k].CardNumber == MyTextBox.Text)
                                {
                                    result = AllUsers[k].Balance;
                                }
                            }
                            for (int i = 0; i < ten_money; i++)
                            {
                                result -= 10;
                                Thread.Sleep(1000);
                            }
                            var notification = string.Empty;
                            for (int j = 0; j < AllUsers.Count; j++)
                            {
                                if (AllUsers[j].CardNumber == MyTextBox.Text)
                                {

                                    AllUsers[j].Balance = result;
                                    if (int.Parse(MoneyTxtb.Text) > AllUsers[j].Balance)
                                    {
                                        MessageBox.Show("Your entered money count is greater than balance.");
                                        Tbtn.IsEnabled = true;

                                        break;
                                    }
                                    else if (int.Parse(MoneyTxtb.Text) <= 0)
                                    {
                                        MessageBox.Show("Your entered money count is less than balance or equals to zero(0)");
                                        Tbtn.IsEnabled = true;

                                        break;
                                    }
                                    else
                                    {

                                        lock (obj)
                                        {
                                            User u = new User
                                            {
                                                FullName = AllUsers[j].FullName,
                                                CardNumber = MyTextBox.Text,
                                                Balance = AllUsers[j].Balance,
                                            };
                                            App.DB.UserRepository.Update(u);
                                            user.FullName = u.FullName;
                                            user.CardNumber = u.CardNumber;
                                            user.Balance = u.Balance;
                                        }

                                    }
                                    MessageBox.Show("Transaction finished successfully");
                                    //MessageBox.Show(notification);
                                    Tbtn.IsEnabled = true;

                                }
                            }


                            m.ReleaseMutex();
                        }

                    }
                }
                else if (money <= 0)
                {
                    MessageBox.Show("Money must be at least 10 AZN");
                }
                else
                {
                    MessageBox.Show("Minimum money transaction is 10 AZN");

                }



            });


        }


    }
}
