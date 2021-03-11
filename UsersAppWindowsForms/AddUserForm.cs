using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersAppWindowsForms.Data;
using UsersAppWindowsForms.Models;
using System.Text.RegularExpressions;
using System.Globalization;

namespace UsersAppWindowsForms
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                BirthDay = dateTimePicker.Value,
                Email = textBoxEmail.Text,
                FirstName = textBoxFirstName.Text,
                LastName = textBoxLastName.Text,
                Login = textBoxLogin.Text,
                Password = textBoxPassword.Text,
                PhoneNumber = textBoxPhone.Text
            };
            Datas.Users.Add(user);
            MainListForm listForm = new MainListForm();
            listForm.Show();
            this.Close();
        }

        private void textBoxLogin_Validating(object sender, CancelEventArgs e)
        {

            if (textBoxLogin.Text.Length < 5 || textBoxLogin.Text.Length > 15 || !Regex.IsMatch(textBoxLogin.Text, @"[a-zA-z_]{1}\w"))
            {

                MessageBox.Show("Логин меньше 5 символов или больше 15(Латиница)");
                e.Cancel = true;
            }
        }

        private void textBoxFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxFirstName.Text.Length < 3 || textBoxFirstName.Text.Length > 15)
            {
                MessageBox.Show("Имя меньше 3 символов или больше 15");
                e.Cancel = true;
            }
        }

        private void textBoxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(textBoxPassword.Text, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
            {
                MessageBox.Show("Минимум восемь символов, минимум одна буква и одна цифра");
                e.Cancel = true;
            }
        }

        private void textBoxLastName_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxLastName.Text.Length < 3 || textBoxLastName.Text.Length > 15)
            {
                MessageBox.Show("Фамилия меньше 3 символов или больше 15");
                e.Cancel = true;
            }
        }

        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {

            if (!IsValidEmail(textBoxEmail.Text))
            {
                MessageBox.Show("Некоректный ввод email!");
                e.Cancel = true;
            }
        }




        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void textBoxPhone_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(textBoxPhone.Text, @"^(\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$"))
            {
                MessageBox.Show("Неверный номер. Пример:\n+79855310868\n" +
                "+7 (926) 777-77-77\n84456464641\n89855310868");
                e.Cancel = true;
            }

        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
