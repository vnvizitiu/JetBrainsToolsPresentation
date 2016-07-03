
namespace ProfilingExample
{
    using System;
    using System.Threading;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Blocking_Click(object sender, RoutedEventArgs e)
        {
            long maxLimit = (long)Math.Pow(10, 7);
            Status.Text = "Doing intensive work";

            for (int i = 0; i < maxLimit; i++)
            {
                Status.Text = Guid.NewGuid().ToString();
            }

            Status.Text = "Done";
        }

        private void Gc_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                long maxLimit2 = (long)Math.Pow(10, 5);
                for (int i = 0; i < maxLimit2; i++)
                {
                    Dispatcher.Invoke(() => { Status.Text = Guid.NewGuid().ToString(); });
                }

                Dispatcher.Invoke(() => { Status.Text = "done with second thread"; });
            });

            thread.Start();
        }

        public static void DoStuff()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}
