using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace MillionGamePlayer
{
    public partial class TelephoneJokerWindow
    {
        private readonly JokerResult result;
        private DispatcherTimer timer;
        private SoundPlayer player;
        private int count;

        public TelephoneJokerWindow(JokerResult result)
        {
            this.result = result;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 500) };
            timer.Tick += TimerTick;
            timer.Start();

            player = new SoundPlayer(GetResourceStream("telephone.wav"));
        }

        private Stream GetResourceStream(string uri)
        {
            uri = string.Concat(Assembly.GetExecutingAssembly().GetName().Name, '.', uri);
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(uri);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            count += 1;
            if (count == 2)
            {
                player.Play();
            }
            if (count == 14)
            {
                tb_question.Visibility = Visibility.Visible;
            }
            if (count == 18)
            {
                tb_answer.Text = result.Message;
                tb_answer.Visibility = Visibility.Visible;
                timer.Stop();
                timer.Tick -= TimerTick;
            }
        }
    }
}
