using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmergencyApp
{
    public partial class MainPage : ContentPage
    {
        bool isVoiceOn;

        public MainPage()
        {
            InitializeComponent();

            // 変数初期化
            isVoiceOn = false;

            // 初回アプリ起動時
            if (!Application.Current.Properties.ContainsKey("VoiceId"))
            {
                Application.Current.Properties["VoiceId"] = 0;
            }
            if (!Application.Current.Properties.ContainsKey("VoiceType"))
            {
                Application.Current.Properties["VoiceType"] = "助けて下さい";
            }
        }

        private void OnOffButton_Clicked(object sender, EventArgs e)
        {
            int rawId;

            // 音声出力開始
            if (isVoiceOn == false)
            {
                rawId = (int)Application.Current.Properties["VoiceId"];
                DependencyService.Get<IVoicePlayer>().PlaySound(rawId);

                OnOffBtn.Text = "STOP";
                OnOffBtn.BackgroundColor = Color.Coral;
                isVoiceOn = true;
            }
            else
            {
                // 音声停止
                DependencyService.Get<IVoicePlayer>().PlayStop();

                // ボタンを元に戻す
                OnOffBtn.Text = "PUSH";
                OnOffBtn.BackgroundColor = Color.Red;
                isVoiceOn = false;
            }
        }

        //--------------------------------------------
        //  イベントハンドラ
        //--------------------------------------------
        //［設定］ボタン
        private void SetButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingPage(), true);    // 設定画面への遷移

        }
        //---
        // 電話［110：警察］
        //---
        async void TelPolice_Clicked(object sender, EventArgs e)
        {
            // 音声出力中チェック
            if (isVoiceOn == true)
                return;
            try
            {
                // Callチェック
                bool result = await DisplayAlert("【警察】",
                                                 "　電話をかけてもいいですか？",
                                                 "OK", "キャンセル");
                if (result)
                {
                    // 正式版            DependencyService.Get<IPhone>().Call("警察","110");
                    DependencyService.Get<IPhone>().Call("時報", "117");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }

        }

        //---
        // 電話［119：救急車］
        //---
        async void TelAmbulance_Clicked(object sender, EventArgs e)
        {
            // 音声出力中チェック
            if (isVoiceOn == true)
                return;

            try
            {
                // Callチェック
                bool result = await DisplayAlert("【救急車】",
                                                "　電話をかけてもいいですか？",
                                                "OK", "キャンセル");
                if (result)
                {
                    // 正式版           DependencyService.Get<IPhone>().Call("救急車", "119");
                    DependencyService.Get<IPhone>().Call("天気予報", "177");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }

        }

        //---
        // 短縮ダイヤル
        //---
        async void TelShort_Clicked(object sender, EventArgs e)
        {
            // 音声出力中チェック
            if (isVoiceOn == true)
                return;

            // 短縮ダイヤル設定チェック
            if (Application.Current.Properties.ContainsKey("TelId"))
            {
                string telNo;

                // 短縮ダイヤル番号の取得
                telNo = (string)Application.Current.Properties["TelNo"];

                // 電話をかける
//                DependencyService.Get<IPhone>().Call("短縮", telNo);
            }
            else
            {
                await DisplayAlert("短縮ダイヤル", "設定されてません", "OK");
            }

        }
    }
}
