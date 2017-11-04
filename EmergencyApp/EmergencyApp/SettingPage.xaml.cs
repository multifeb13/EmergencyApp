using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmergencyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public SettingPage()
        {
            InitializeComponent();
            SelectVoice.Detail = (string)Application.Current.Properties["VoiceType"];
        }

        //--------------------------------------------
        //  イベントハンドラ
        //--------------------------------------------
        //  設定画面 → 声の選択リスト画面に遷移
        private void VoiceCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VoiceList(SelectVoice), true);
        }

        // 設定画面 → 電話番号登録画面に遷移
        private void ShortDialCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TelephoneList(SelectTEL),true);
        }

        // [登録]ボタンイベント処理（非同期）
//        private async void SaveButton_Clicked(object sender, EventArgs e)
//        {
        // 電話番号登録ボタンイベント用
        //            var entry = new Entry();　入力欄
        //
        //            string save = await SaveTextAsync(entry.Text);

        // 設定画面に戻る
        //            await Navigation.PopAsync(true);
//        }

    }
}