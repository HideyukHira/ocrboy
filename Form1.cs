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

        //prvate�@�����o���`
        private Ocr ocr = new Ocr();
        //private �p�ꕶ������`�@"Copy the image to the clipboard and press this button"
        private string myEnglishString = "Copy the image to the clipboard and press this button";


        public Form1()
        {
            InitializeComponent();
        }

        //�t�H�[�����[�h���ɁA���[�U�[��OS�����{��ȊO�̏ꍇ�Abutton1��Text�����̕��͂ɂ��� "BOYBOYBOY"
        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.Globalization.CultureInfo.CurrentCulture.Name != "ja-JP")
            {
                button1.Text = myEnglishString;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // �C���[�W�̕\�����@��ݒ�
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            if (Clipboard.ContainsImage())
            {
                // �N���b�v�{�[�h�̉摜��PictureBox�ɕ\������
                pictureBox1.Image = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);

                var myBmp = (Bitmap)Clipboard.GetDataObject().GetData(DataFormats.Bitmap);
                //OCR�����J�n �摜�ϊ�
                var mySbitmap = await ocr.GetSoftwareSnapShot(myBmp);
                //OCR���� OcrEngine
                var ocrResult = await ocr.RecognizeText(mySbitmap);
                //OCR���� Text
                textBox1.Text = ocrResult.Text.Replace(" ", " ");
            }

        }
        //�t�H�[�������[�h���ꂽ��Abutton1�Ƀt�H�[�J�X�𓖂Ă�
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