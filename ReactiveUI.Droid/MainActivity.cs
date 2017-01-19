using Android.App;
using Android.Widget;
using Android.OS;
using ReactiveUI.Core.ViewModels;

namespace ReactiveUI.Droid
{
    [Activity(Label = "ReactiveUI.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ReactiveActivity, IViewFor<TheViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            ViewModel = new TheViewModel();

            this.WireUpControls();

            this.Bind(this.ViewModel, x => x.SearchQuery, x => x.TheEditText.Text);
            this.OneWayBind(this.ViewModel, x => x.SearchQuery, x => x.TheTextView.Text);
        }

        public EditText TheEditText { get; private set; }

        public TextView TheTextView { get; private set; }

        private TheViewModel _viewModel;
        public TheViewModel ViewModel
        {
            get { return _viewModel; }
            set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (TheViewModel)value; }
        }
    }
}

