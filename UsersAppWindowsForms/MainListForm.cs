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

namespace UsersAppWindowsForms
{
    public partial class MainListForm : Form
    {
        private const int totalRecords = 43;
        private const int pageSize = 10;
        private int offset = 0;
        public MainListForm()
        {
            InitializeComponent();
            if (Datas.IsCreateUser)
            {
            Datas datas = new Datas();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Index" });
                Datas.IsCreateUser = false;
            }
            bindingNavigator1.BindingSource = bindingSource1;
            bindingSource1.CurrentChanged += new System.EventHandler(bindingSource1_CurrentChanged);
            bindingSource1.DataSource = new PageOffsetList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            // The desired page has changed, so fetch the page of records using the "Current" offset 
            var records = new List<User>();
            offset = (int)bindingSource1.Current;
            int offsetPlus = offset;
            for (int i = offset; i < offset + pageSize && i < Datas.Users.Count; i++)
            {
                if (Datas.Users.Last() == Datas.Users.ElementAt(i))
                {
                    records.Add(Datas.Users.ElementAt(i));
                    break;
                }
                records.Add(Datas.Users.ElementAt(i));
                offsetPlus++;
            }
            offset = offsetPlus;
            dataGridView1.DataSource = records;
        }

        class Record
        {
            public int Index { get; set; }
        }

        class PageOffsetList : System.ComponentModel.IListSource
        {
            public bool ContainsListCollection { get; protected set; }

            public System.Collections.IList GetList()
            {
                // Return a list of page offsets based on "totalRecords" and "pageSize"
                var pageOffsets = new List<int>();
                for (int offset = 0; offset < Datas.Users.Count; offset += pageSize)
                    pageOffsets.Add(offset);
                return pageOffsets;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddUserForm form = new AddUserForm();
            form.Show();
            this.Hide();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Datas.Users.RemoveAt(dataGridView1.CurrentRow.Index);
            MainListForm listForm = new MainListForm();
            listForm.Show();
            this.Close();
        }
    }
}
