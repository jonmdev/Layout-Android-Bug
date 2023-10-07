using System.Diagnostics;

namespace Layout_Android_Bug {
    public partial class App : Application {

        public event Action screenSizeChanged = null;
        public double screenWidth = 0;
        public double screenHeight = 0;
        ContentPage mainPage;
        public App() {

            InitializeComponent();

            //=========
            //LAYOUT
            //=========
            mainPage = new();
            mainPage.Background = Colors.Magenta;
            MainPage = mainPage;
            mainPage.SizeChanged += delegate {
                invokeScreenSizeChangeEvent();
            };

            //VerticalStackLayout vert = new();
            AbsoluteLayout vert = new();
            mainPage.Content = vert;
            vert.BackgroundColor = Colors.YellowGreen;

            Border border1 = new Border();
            border1.BackgroundColor = Colors.BlueViolet;
            border1.HorizontalOptions = LayoutOptions.Center;
            border1.Margin = 0;
            border1.Padding = new Thickness(10, 0);
            vert.Children.Add(border1);
            border1.SizeChanged += delegate {
                Debug.WriteLine("BORDER WIDTH " + border1.Width + "BORDER HEIGHT " + border1.Height + " BOUNDS " + border1.Bounds);
            };

            //==================
            //RESIZE FUNCTION
            //==================
            screenSizeChanged += delegate {

                Debug.WriteLine("VERT resized to " + screenWidth + " " + screenHeight);
                vert.HeightRequest = screenHeight;
                vert.WidthRequest = screenWidth;


            };
            Debug.WriteLine("FINISHED BUILD OKAY");

        }
        private void invokeScreenSizeChangeEvent() {
            if (mainPage.Width > 0 && mainPage.Height > 0) {
                screenWidth = mainPage.Width;
                screenHeight = mainPage.Height;
                Debug.WriteLine("main page size changed | width: " + screenWidth + " height: " + screenHeight);
                screenSizeChanged?.Invoke();
            }
        }
    }
}