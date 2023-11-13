using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Markup;
using WindowsFormsApp1;
using static System.Windows.Forms.Design.AxImporter;

namespace windows_ocr
{
    public partial class Form1 : Form
    {

        //prvate　メンバを定義
        private Ocr ocr = new Ocr();
        //private 英語文字列を定義　"Copy the image to the clipboard and press this button"
        private string myEnglishString = "Copy the image to the clipboard and press this button";


        public Form1()
        {
            InitializeComponent();
        }

        //フォームロード時に、ユーザーのOSが日本語以外の場合、button1のTextを次の文章にする "BOYBOYBOY"
        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.Globalization.CultureInfo.CurrentCulture.Name != "ja-JP")
            {
                button1.Text = myEnglishString;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // イメージの表示方法を設定
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            if (Clipboard.ContainsImage())
            {
                // クリップボードの画像をPictureBoxに表示する
                pictureBox1.Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);

                var myBmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                //OCR処理開始 画像変換
                var mySbitmap = await ocr.GetSoftwareSnapShot(myBmp);
                //OCR処理 OcrEngine
                var ocrResult = await ocr.RecognizeText(mySbitmap);
                //OCR処理 Text
                textBox1.Text = ocrResult.Text.Replace(" ", " ");
            }

        }
        //フォームがロードされたら、button1にフォーカスを当てる
        private void Form1_Shown(object sender, EventArgs e)
        {
            button1.Focus();
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }


    }

}