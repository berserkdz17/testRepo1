using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TypeSpeedTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        private DispatcherTimer clock;
        private bool typingStarted;
        private int characterCount;
        private int charactersPerSec;
        private double maxWordsPerSec;

        public MainWindow()
        {
            InitializeComponent();

            clock = new DispatcherTimer();
            clock.Interval = new TimeSpan(0, 0, 1);
            clock.Tick += clock_Tick;

            
        }

        void clock_Tick(object sender, EventArgs e)
        {
            int timerNumber = Int32.Parse(timer.Text);

            if (timerNumber == 0)
            {
                // Timer reached zero
                clock.Stop();
                wpm.Text = (characterCount / 5).ToString();
                return;
            }
            else
            {
                // Test still ongoing
                double wordsPerSec = charactersPerSec / 5.0;
                if (wordsPerSec > maxWordsPerSec)
                {
                    maxWordsPerSec = wordsPerSec;
                    wps.Text = maxWordsPerSec.ToString();
                }
                charactersPerSec = 0;               

                timer.Text = (timerNumber - 1).ToString();
            }
        }


        private void word_TextChanged(object sender, TextChangedEventArgs e)  
        {
            if (!typingStarted)
            {
                // first word entered, i.e. typing test should stasrt
                typingStarted = true;
                clock.Start();
            }
            characterCount++;
            charactersPerSec++;
        }

        // RESET
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            clock.Stop();
            typingStarted = false;
            timer.Text = "60";

            characterCount = 0;
            maxWordsPerSec = 0;

            word.Text = string.Empty;
        }

        private void KeyBinding_Changed(object sender, EventArgs e)
        {
            word.Text = string.Empty;
        }
        
        private void word_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                word.Text = string.Empty;
                word.Select(0, 0);
            }
        } 

    }
}
