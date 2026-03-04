using System.Windows;
using System.Windows.Controls;

namespace Waifu_Dreams_CityHelperApplication.Pages {
    public partial class MainPage : Page {

        private readonly Frame _contentFrame;
        
        public MainPage(Frame contentFrame) {
            InitializeComponent();
            this._contentFrame = contentFrame;
        }

        private void OnVersionSelectClick(object sender, RoutedEventArgs routedEventArgs) {
            if (!(sender is Button button)) return;
            string name = button.Name;
            
            if (name == this.Btn_Version_97.Name) {
                _contentFrame.Navigate(new Build_97_Page());
                //手动清理历史记录
                _contentFrame.NavigationService.RemoveBackEntry();
                return;
            }
            if (name == this.Btn_Version_98.Name) {
                // _contentFrame.Navigate(new Build_98_Page());
                //手动清理历史记录
                _contentFrame.NavigationService.RemoveBackEntry();
                return;
            }
        }
    }
}