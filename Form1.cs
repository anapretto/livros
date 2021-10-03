using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Livros
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Fill();
        }

        private void btn_adicionar_Click(object sender, EventArgs e)
        {
            if(tb_ano.Text != "" && tb_autor.Text != "" && tb_nome.Text != "")
            {
                Novo livro = new Novo();
                livro.nome = tb_nome.Text;
                livro.autor = tb_autor.Text;
                livro.ano = Convert.ToInt32(tb_ano.Text);
                Banco.NovoLivro(livro);

                string[] item = new string[3];
                item[0] = tb_nome.Text;
                item[1] = tb_autor.Text;
                item[2] = tb_ano.Text;
                lv_livros.Items.Add(new ListViewItem(item));

                tb_nome.Clear();
                tb_autor.Clear();
                tb_ano.Clear();
            }
            else
            {
                MessageBox.Show("Preencha todos os campos");
            }
           
        }
        private void Fill()
        {
            lv_livros.Columns.Add("Nome", 259);
            lv_livros.Columns.Add("Autor", 240);
            lv_livros.Columns.Add("Ano", 100).TextAlign = HorizontalAlignment.Center;
            lv_livros.View = View.Details;
            lv_livros.FullRowSelect = true;
            lv_livros.GridLines = true;
            lv_livros.MultiSelect = false;

            DataTable dt = new DataTable();
            dt = Banco.Todos();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] item = new string[3];
                item[0] = dt.Rows[i].Field<string>("S_NOME");
                item[1] = dt.Rows[i].Field<string>("S_AUTOR");
                item[2] = dt.Rows[i].Field<long>("I_ANO").ToString();
                lv_livros.Items.Add(new ListViewItem(item));
            }
        }

        private void lv_livros_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = lv_livros.HitTest(e.X, e.Y);
            ListViewItem item = info.Item;
            Int32 i = lv_livros.Items.IndexOf(lv_livros.SelectedItems[0])+1;

            if (item != null)
            {
                Atualizar l = new Atualizar();

                Button btn_ok = new Button();
                btn_ok.Text = "Atualizar";
                btn_ok.Location = new Point(534, 31);
                btn_ok.DialogResult = DialogResult.OK;
                l.AcceptButton = btn_ok;
                l.Controls.Add(btn_ok);

                Button btn_cancel = new Button();
                btn_cancel.Text = "Cancelar";
                btn_cancel.Location = new Point(534, 92);
                btn_cancel.DialogResult = DialogResult.Cancel;
                l.CancelButton = btn_cancel;
                l.Controls.Add(btn_cancel);

                TextBox tb_lname = new TextBox();
                tb_lname.Location = new Point(12, 32);
                tb_lname.Size = new Size(190, 23);
                tb_lname.Text = item.SubItems[0].Text;
                l.Controls.Add(tb_lname);

                TextBox tb_lautor = new TextBox();
                tb_lautor.Location = new Point(237, 32);
                tb_lautor.Size = new Size(171, 23);
                tb_lautor.Text = item.SubItems[1].Text;
                l.Controls.Add(tb_lautor);

                TextBox tb_lano = new TextBox();
                tb_lano.Location = new Point(467, 32);
                tb_lano.Size = new Size(55, 23);
                tb_lano.Text = item.SubItems[2].Text;
                l.Controls.Add(tb_lano);

                l.ShowDialog();

                if (l.DialogResult == DialogResult.OK)
                {
                    if (tb_lname.Text == "" || tb_lautor.Text == "" || tb_lano.Text == "")
                    {
                        MessageBox.Show("Preencha todos os campos.");
                    }
                    else
                    {
                        Novo n = new Novo();
                        n.nome = tb_lname.Text;
                        n.autor = tb_lautor.Text;
                        n.ano = Convert.ToInt32(tb_lano.Text);
                        n.index = i;
                        Banco.AtualizarLivro(n);

                        item.SubItems[0].Text = tb_lname.Text;
                        item.SubItems[1].Text = tb_lautor.Text;
                        item.SubItems[2].Text = tb_lano.Text;
                    }
                }
            }
            else
            {
                this.lv_livros.SelectedItems.Clear();
                MessageBox.Show("Selecione algum filme antes de continuar.");
            }            
        }

        private void btn_deletar_Click(object sender, EventArgs e)
        {
            if(lv_livros.SelectedItems.Count == 1)
            {
                Int32 i = lv_livros.Items.IndexOf(lv_livros.SelectedItems[0]) + 1;

                Novo n = new Novo();
                n.index = i;
                Banco.DeletarLivro(n);
                
                lv_livros.SelectedItems[0].SubItems.Clear();
                lv_livros.SelectedItems.Clear();
            }
        }
    }
}
