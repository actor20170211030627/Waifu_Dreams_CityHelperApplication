using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Waifu_Dreams_CityHelperApplication.Utils;

namespace Waifu_Dreams_CityHelperApplication.Pages {
    public partial class MainPage : Page {

        //Waifu Dreams City Build 97\Waifu_Dreams_City\Binaries\Win64\Waifu_Dreams_City-Win64-Shipping.exe
        private const string Md5_Version97  = "d474ffa9d6624f32de31197bc56d0773";
        //Waifu Dreams City Build 101\Waifu_Dreams_City\Binaries\Win64\Waifu_Dreams_City-Win64-Shipping.exe
        private const string Md5_Version101 = "3e573578fa755470ebed56680a75b4d1";

        private readonly Frame _contentFrame;
        private DispatcherTimer _dispatcherTimer;
        
        public MainPage(Frame contentFrame) {
            InitializeComponent();
            this._contentFrame = contentFrame;
            
            this.Loaded += MyPage_Loaded;  // 订阅Loaded事件
            this.Unloaded += MyPage_Unloaded;  // 订阅Unloaded事件
            
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(600.0);
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                string processPath = ProcessUtils.GetProcessPath(TRainer97Helper.ProcessName);
                if (processPath == null) return;
                //计算文件的MD5值
                string md5Hash = Md5Utils.CalculateFileMd5(processPath);
                Console.WriteLine($"文件路径：{processPath}\nMD5值：{md5Hash}");
                if (md5Hash == null) return;

                switch (md5Hash) {
                    case Md5_Version97:
                        Go2Version97();
                        break;
                    case Md5_Version101:
                        Go2Version101();
                        break;
                    default:
                        MessageBoxUtils.NewMessageBox("你当前版本游戏, 修改器还没做, 请加Q群206483634反馈\nThe trainer is not yet support with your current game version!")
                            .SetCaption("修改器提示(Trainer tips)")
                            .SetIcon(MessageBoxImage.Error)
                            .Show();
                        break;
                }
            };
            _dispatcherTimer.Start();
        }

        private void Go2Version97() {
            _contentFrame.Navigate(new Build_97_Page());
            //手动清理历史记录
            _contentFrame.NavigationService.RemoveBackEntry();
        }

        private void Go2Version101() {
            _contentFrame.Navigate(new Build_101_Page());
            //手动清理历史记录
            _contentFrame.NavigationService.RemoveBackEntry();
        }

        private void OnVersionSelectClick(object sender, RoutedEventArgs routedEventArgs) {
            if (!(sender is Button button)) return;
            string name = button.Name;
            if (name == this.Btn_Version_97.Name) {
                Go2Version97();
                return;
            }
            if (name == this.Btn_Version_101.Name) {
                Go2Version101();
                return;
            }
        }



        private void MyPage_Loaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面加载完成 - 在这里初始化数据");
            // 加载数据、绑定事件等
        }

        // 页面卸载时执行 - 这是主要的方法
        private void MyPage_Unloaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面卸载 - 在这里清理资源");
            //停止定时器
            _dispatcherTimer.Stop();
            // _dispatcherTimer.Tick -= ;
        }
    }
}