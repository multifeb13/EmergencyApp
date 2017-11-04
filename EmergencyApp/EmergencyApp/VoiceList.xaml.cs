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
    public partial class VoiceList : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        TextCell voiceCell;

        public VoiceList( TextCell detail )
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
               "助けて下さい",
               "痴漢です",
            };

            BindingContext = this;

            // 設定画面反映用
            voiceCell = detail;
        }

        async void SelectItem_Tapped(object sender, SelectedItemChangedEventArgs e)
        {
            string listName = "";

            if ( e.SelectedItem == null)
                return;

            // 選択項目、indexの設定
            listName = ((ListView)sender).SelectedItem.ToString();
            Application.Current.Properties["VoiceType"] = listName;
            Application.Current.Properties["VoiceId"] = Items.IndexOf(listName);

            // 設定画面：選択声の表示
            voiceCell.Detail = listName;

            // 設定画面に戻る
            await Navigation.PopAsync();

        }
    }
}