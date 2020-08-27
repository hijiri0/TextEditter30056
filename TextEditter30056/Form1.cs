using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditter30056
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string fileName = "";   //Camel形式(⇔Pascal形式)

        public Form1()
        {
            InitializeComponent();
        }

        //終了
        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }

        //上書き保存
        private void SaveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ファイル名がなければ、名前を付けて保存
            if (this.fileName == "")
            {
                FileSave(fileName);
            }

            //ファイル名があれば、そのまま保存
            else
            {
                SaveNameAToolStripMenuItem_Click(sender, e);
            }
        }

        //名前を付けて保存
        private void SaveNameAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
            }
        }

        //ファイル名を指定して内容を保存
        private void FileSave(string fileName)
        {
            //【名前を付けて保存】ダイアログを表示
            using (StreamWriter sw = new StreamWriter(sfdFileSave.FileName, false, Encoding.GetEncoding("utf-8")))
            {
                //書き込み
                sw.WriteLine(rtTextArea.Text);
            }
        }

        //開く
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //【開く】ダイアログを表示
            if (sfdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //StreamReaderクラスを使用してファイル読込
                using (StreamReader sr = new StreamReader(sfdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    //this.fileName=OpenFileDialog
                }
            }
        }

        //新規作成
        private void NewNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //警告画面を表示する
            DialogResult result = MessageBox.Show("ファイルを保存しますか？", "質問",
                                                MessageBoxButtons.YesNo,         //はい、いいえ
                                                MessageBoxIcon.Exclamation,      //警告の標識
                                                MessageBoxDefaultButton.Button1);//デフォルトを『はい』にする

            //何が選択されたか調べる
            if (result == DialogResult.Yes)
            {
                //『はい』が選択された時、保存して新規作成
                SaveNameAToolStripMenuItem_Click(sender, e);
                AllClear();
            }

            else if (result == DialogResult.No)
            {
                //『いいえ』が選択された時、新規作成
                AllClear();
            }

        }

        //何もなかったようにする
        private void AllClear()
        {
            //テキストを初期状態にする
            rtTextArea.Text = "";

            //ファイル名を初期状態にする
            this.fileName = "";
        }

        //元に戻す
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();
        }

        //やり直す
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        //切り取り
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();
        }

        //貼り付け
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.ResetText();
        }
    }
}
