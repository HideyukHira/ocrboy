using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace WindowsFormsApp1
{
    internal class Ocrinner
    {
    }
    public class Ocr
    {

        public async Task<SoftwareBitmap> GetSoftwareSnapShot(Bitmap snap)
        {
            //OCR用にビットマップを softwareBitmap に変更して保存、return

            // 取得した画像をファイルとして保存
            var folder              = Directory.GetCurrentDirectory();
            var imageName           = "ScreenCapture.bmp";

            StorageFolder appFolder = await StorageFolder.GetFolderFromPathAsync(@folder);
            snap.Save(folder + "\\" + imageName, ImageFormat.Bmp);
            SoftwareBitmap softwareBitmap;
            var bmpFile = await appFolder.GetFileAsync(imageName);

            // 保存した画像をSoftwareBitmap形式で読み込み
            using (IRandomAccessStream stream = await bmpFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder         = await BitmapDecoder.CreateAsync(stream);
                softwareBitmap                = await decoder.GetSoftwareBitmapAsync();
            }

            // 保存した画像ファイルの削除
            File.Delete(folder + "\\" + imageName);

            // SoftwareBitmap形式の画像を返す
            return softwareBitmap;
        }

        public async Task<OcrResult> RecognizeText(SoftwareBitmap snap)
        {
            OcrEngine ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
            // OCR実行
            var ocrResult = await ocrEngine.RecognizeAsync(snap);
            return ocrResult;
        }

    }

}