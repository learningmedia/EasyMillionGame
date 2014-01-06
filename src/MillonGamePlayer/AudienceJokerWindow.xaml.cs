using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MillionGamePlayer
{
    public partial class AudienceJokerWindow
    {
        private int count;
        private DispatcherTimer timer;

        private readonly JokerResult result;
        private readonly Button[] buttons;

        public AudienceJokerWindow(JokerResult result, Button[] buttons)
        {
            InitializeComponent();
            this.result = result;
            this.buttons = buttons;
            tb_wait.Visibility = Visibility.Visible;
            count = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 500) };
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            count += 1;

            if (tb_wait.Visibility == Visibility.Visible)
            {
                tb_wait.Visibility = Visibility.Collapsed;
            }
            else
            {
                tb_wait.Visibility = Visibility.Visible;
            }

            if (count == 8)
            {
                timer.Stop();
                ShowPublicResult();
            }
        }

        private void ShowPublicResult()
        {
            tb_wait.Visibility = Visibility.Collapsed;

            Chart chart = result.Chart;
            int[] data = chart.Data;

            int height1 = data[0] * 2;
            lbl_1.Height = height1;
            lbl_1.Background = buttons[0].Visibility == Visibility.Visible ? Brushes.Goldenrod : Brushes.Transparent;
            lbl_2.Height = data[1] * 2;
            lbl_2.Background = buttons[1].Visibility == Visibility.Visible ? Brushes.Firebrick : Brushes.Transparent;
            lbl_3.Height = data[2] * 2;
            lbl_3.Background = buttons[2].Visibility == Visibility.Visible ? Brushes.SeaGreen : Brushes.Transparent;
            lbl_4.Height = data[3] * 2;
            lbl_4.Background = buttons[3].Visibility == Visibility.Visible ? Brushes.Coral : Brushes.Transparent;
        }
    }
}
