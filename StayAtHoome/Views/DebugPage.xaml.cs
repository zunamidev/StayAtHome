using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using StayAtHoome.Data;
using StayAtHoome.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StayAtHoome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DebugPage : ContentPage
    {
        public DebugPage()
        {
            InitializeComponent();
        }

        private void ClearData(object sender, EventArgs e)
        {
            LocalDatabase.Clear();
        }

        private void LoadDemoData(object sender, EventArgs e)
        {
            new DemoService().LoadDemoData().SafeFireAndForget();
        }
    }
}