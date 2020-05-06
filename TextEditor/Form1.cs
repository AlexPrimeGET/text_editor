using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class te_form : Form
    {

        const string TITLE = "Текстовый редактор";
        const string QUANTITY = "Сим ";
        const string PATH = "Путь: ";


        public te_form(string FileName)
        {
            InitializeComponent();
            if (FileName.Length > 0)
            {
                string[] path = FileName.Split('\\');

                this.Text = TITLE + $" || {path[path.Length - 1]}";

                te_sb_path.Text = PATH + FileName;

                te_rtb_editor.Text = File.ReadAllText(FileName, Encoding.Default);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            te_sb_quantity.Text = QUANTITY + 0.ToString();

            te_menu_newFile.Click += Te_menu_newFile_Click;

            te_tsb_newFile.Click += Te_menu_newFile_Click;

            te_menu_saveFile.Click += Te_menu_saveFile_Click;

            te_tsb_save.Click += Te_menu_saveFile_Click;

            te_menu_openFile.Click += Te_menu_openFile_Click;

            te_tsb_open.Click += Te_menu_openFile_Click;

            te_menu_closeFile.Click += Te_menu_closeFile_Click;

            te_menu_saveAs.Click += Te_menu_saveAs_Click;

            te_rtb_editor.TextChanged += Te_rtb_editor_TextChanged;

            te_rtb_editor.SelectionChanged += Te_rtb_editor_SelectionChanged;

            te_menu_copy.Click += Te_menu_copy_Click;
            te_tsb_copy.Click += Te_menu_copy_Click;
            te_cm_copy.Click += Te_menu_copy_Click;

            te_menu_cut.Click += Te_menu_cut_Click;
            te_tsb_cut.Click += Te_menu_cut_Click;
            te_cm_cut.Click += Te_menu_cut_Click;

            te_menu_insert.Click += Te_menu_insert_Click;
            te_tsb_insert.Click += Te_menu_insert_Click;
            te_cm_insert.Click += Te_menu_insert_Click;

            te_menu_undo.Click += Te_menu_undo_Click;
            te_tsb_undo.Click += Te_menu_undo_Click;
            te_cm_undo.Click += Te_menu_undo_Click;

            te_menu_redo.Click += Te_menu_redo_Click;
            te_tsb_redo.Click += Te_menu_redo_Click;
            te_cm_redo.Click += Te_menu_redo_Click;

            te_menu_selectAll.Click += Te_menu_selectAll_Click;

            te_menu_font.Click += Te_menu_font_Click;
            te_tsb_font.Click += Te_menu_font_Click; ;

            te_menu_fontColor.Click += Te_menu_fontColor_Click;
            te_tsb_fontColor.Click += Te_menu_fontColor_Click;

            te_menu_backgroundColor.Click += Te_menu_backColor_Click;
            te_tsb_backColor.Click += Te_menu_backColor_Click;

            te_menu_exit.Click += Te_menu_exit_Click;

            this.Width = Properties.Settings.Default.formWidth;
            this.Height = Properties.Settings.Default.formHeight;
            this.Location = new Point(200, 200);
            this.Location = Properties.Settings.Default.Location;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Properties.Settings.Default.formWidth = this.Width;
            Properties.Settings.Default.formHeight = this.Height;
            base.OnFormClosing(e);
        }
        
        /// Завершение работы

        private void Te_menu_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// Изменение цвета фона

        private void Te_menu_backColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                te_rtb_editor.BackColor = cd.Color;
            }
        }

        
        /// Изменение цвета шрифта
        
        private void Te_menu_fontColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                te_rtb_editor.ForeColor = cd.Color;
            }
        }

        
        /// Именение шрифта в редакторе
        
        private void Te_menu_font_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                te_rtb_editor.Font = fd.Font;
            }
        }

        
        /// Выбрать все в окне редактора
        private void Te_menu_selectAll_Click(object sender, EventArgs e)
        {
            te_rtb_editor.Focus();
            te_rtb_editor.SelectAll();
        }

        
        /// Вернуть предыдущее действие 
        
        private void Te_menu_redo_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.CanRedo == true)
            {
                if (te_rtb_editor.RedoActionName != "Delete")
                {
                    te_rtb_editor.Redo();
                }
            }
        }

        
        /// Отменить предыдущее действие
        
        private void Te_menu_undo_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.CanUndo == true)
            {
                te_rtb_editor.Undo();
                te_rtb_editor.ClearUndo();
            }
        }

        
        /// Вставить скопированный фрагмент
        
        private void Te_menu_insert_Click(object sender, EventArgs e)
        {
            te_rtb_editor.Paste();
        }

        
        /// Вырезать выделенный фрагмент
        
        private void Te_menu_cut_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.SelectionLength > 0)
            {
                te_rtb_editor.Cut();
            }
        }

       
        /// Копирование выделенного фрагмента   
        
        private void Te_menu_copy_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.SelectionLength > 0)
            { 
                te_rtb_editor.Copy();
            }
        }

        
        /// Обработка измениения выделения в редакторе
        
        private void Te_rtb_editor_SelectionChanged(object sender, EventArgs e)
        {
            if (te_rtb_editor.SelectionLength > 0)
            {
                te_menu_copy.Enabled = true;
                te_menu_cut.Enabled = true;
                te_tsb_copy.Enabled = true;
                te_tsb_cut.Enabled = true;
                te_cm_copy.Enabled = true;
                te_cm_cut.Enabled = true;
            }
            else
            {
                te_menu_copy.Enabled = false;
                te_menu_cut.Enabled = false;
                te_tsb_copy.Enabled = false;
                te_tsb_cut.Enabled = false;
                te_cm_copy.Enabled = false;
                te_cm_cut.Enabled = false;
            }
            te_sb_row.Text = "Стр " + (te_rtb_editor.GetLineFromCharIndex(te_rtb_editor.SelectionStart) + 1);
            te_sb_col.Text = "Стлб " + (te_rtb_editor.SelectionStart - te_rtb_editor.GetFirstCharIndexOfCurrentLine());
        }

        
        /// Сохранить файл как
        
        private void Te_menu_saveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*txt)|*.txt";
            sfd.FilterIndex = 2;
            sfd.Title = "Тестовый редактор || Сохранить файл как ...";
            sfd.FileName = "file";              //начальное наименование файла
            sfd.DefaultExt = "txt";                 //задаем начальное раширение файла
            sfd.ValidateNames = true;               //валидация введенного имени

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string[] path = sfd.FileName.Split('\\');

                this.Text = TITLE + $" || {path[path.Length - 1]}";

                te_sb_path.Text = PATH + sfd.FileName;

                File.WriteAllText(sfd.FileName, te_rtb_editor.Text);

                EnableDisableElements(true);
            }
        }

        
        /// Закрытие файла
        
        private void Te_menu_closeFile_Click(object sender, EventArgs e)
        {
            te_rtb_editor.Clear();
            EnableDisableElements(false);
        }

        
        /// Открытие файла
        
        private void Te_menu_openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*txt)|*.txt";
            ofd.FilterIndex = 2;
            ofd.Title = "Тестовый редактор :: Открытие файла";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                te_rtb_editor.Clear();

                string[] path = ofd.FileName.Split('\\');

                this.Text = TITLE + $" :: {path[path.Length - 1]}";

                te_sb_path.Text = PATH + ofd.FileName;

                te_rtb_editor.Text = File.ReadAllText(ofd.FileName);

                EnableDisableElements(true);
            }
        }


        /// Подсчет символов в редактора
        
        private void Te_rtb_editor_TextChanged(object sender, EventArgs e)
        {
            te_sb_quantity.Text = QUANTITY +  te_rtb_editor.TextLength.ToString();
            
        }


        /// Сохранение файла

        private void Te_menu_saveFile_Click(object sender, EventArgs e)
        {
            File.WriteAllText(te_sb_path.Text.Substring(6), te_rtb_editor.Text);
        }

        
        /// Создание нового файла
        
        private void Te_menu_newFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Все файлы(*.*)|*.*|Текстовые файлы(*txt)|*.txt";
            sfd.FilterIndex = 2;
            sfd.Title = "Тестовый редактор :: Создание новго файла";
            sfd.FileName = "file";              //начальное наименование файла
            sfd.DefaultExt = "txt";                 //начальное расширение файла
            sfd.ValidateNames = true;               //валидация введенного имени

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                te_rtb_editor.Clear();
                string[] path = sfd.FileName.Split('\\');

                this.Text = TITLE + $" :: {path[path.Length - 1]}";

                te_sb_path.Text = PATH + sfd.FileName;

                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default);
                sw.Write("");
                sw.Close();

                EnableDisableElements(true);

            }
        }

        
        /// Включение/выключение элементов
        
        private void EnableDisableElements(bool status)
        {
            te_menu_saveFile.Enabled = status;
            te_tsb_save.Enabled = status;

            if (!status)
            {
                te_sb_path.Text = PATH;
                this.Text = TITLE;
            }
        }

        /// Справка
        
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 te_license = new Form2();
            te_license.Show();
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// Увеличение шрифта
        private void te_tsb_fontEnlarge_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.Font.Size < 300F)
                te_rtb_editor.SelectionFont = new Font(te_rtb_editor.Font.Name, te_rtb_editor.SelectionFont.Size+2F, te_rtb_editor.Font.Style);
        }

        /// Уменьшение шрифта
        private void te_tsb_fontReduce_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.Font.Size > 2F)
            te_rtb_editor.Font = new Font(te_rtb_editor.Font.Name, te_rtb_editor.Font.Size - 2F, te_rtb_editor.Font.Style);
        }

        private void te_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.formWidth = this.Width;
            Properties.Settings.Default.formHeight = this.Height;
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();
        }

        private void строкаСостоянияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            строкаСостоянияToolStripMenuItem.Checked = !строкаСостоянияToolStripMenuItem.Checked;
            te_statusBar.Visible = !te_statusBar.Visible;
        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.ZoomFactor < 100F)
                te_rtb_editor.ZoomFactor = te_rtb_editor.ZoomFactor + 0.1F;
        }

        private void восстановитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            te_rtb_editor.ZoomFactor = 1F;
        }

        private void уменьшитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (te_rtb_editor.ZoomFactor > 0.21F)
                te_rtb_editor.ZoomFactor = te_rtb_editor.ZoomFactor - 0.1F;
        }
    }
}
