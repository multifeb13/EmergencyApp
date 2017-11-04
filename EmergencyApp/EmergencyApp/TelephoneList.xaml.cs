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
    public partial class TelephoneList : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        TextCell phoneCell;

        public TelephoneList( TextCell detail )
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "my携帯",
                "実家",
            };

            BindingContext = this;

            // 設定画面反映用
            phoneCell = detail;
        }

        async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}