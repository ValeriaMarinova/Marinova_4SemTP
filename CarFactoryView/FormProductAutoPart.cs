
using System;
using System.Collections.Generic;
using AbstractFactoryBusinessLogic.Interfaces;
using System.Linq;
using System.Windows.Forms;
using Unity;
using AbstractFactoryBusinessLogic.ViewModels;

namespace CarFactoryView
{
    public partial class FormProductAutoPart : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxAutoPart.SelectedValue); }
            set { comboBoxAutoPart.SelectedValue = value; }
        }
        public string AutoPartName { get { return comboBoxAutoPart.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }
        public FormProductAutoPart(IAutoPartLogic logic)
        {
            InitializeComponent();
            List<AutoPartViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxAutoPart.DisplayMember = "AutoPartName";
                comboBoxAutoPart.ValueMember = "Id";
                comboBoxAutoPart.DataSource = list;
                comboBoxAutoPart.SelectedItem = null;
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAutoPart.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

