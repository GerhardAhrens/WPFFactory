namespace WPFFactory.Beispiele
{
    using System.Windows;

    /// <summary>
    /// Interaktionslogik für DialogOverviewUC.xaml
    /// </summary>
    public partial class DialogOverviewUC : UserControlBase
    {
        public DialogOverviewUC(ChangeViewEventArgs args)
        {
            this.InitializeComponent();

            this.CurrentCtorArgs = args;

            WeakEventManager<UserControlBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);

            this.GoBackCommand = new CommandBase(this.OnGoBack, () => true);
            this.EditDialogCommand = new CommandBase(this.OnEditDialog, () => true);
            this.DataContext = this;
        }

        public string StatusMessage
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public ChangeViewEventArgs CurrentCtorArgs { get; set; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        public CommandBase GoBackCommand { get; private set; }
        public CommandBase EditDialogCommand { get; private set; }

        private async void OnGoBack()
        {
            ChangeViewEventArgs args = this.CurrentCtorArgs;
            args.MenuButton = DialogView.Home;
            args.FromPage = DialogView.DialogOverView;


            if (App.EventAgg.IsSubscription<ChangeViewEventArgs>() == true)
            {
                await App.EventAgg.PublishAsync(args);
            }
        }

        private void OnEditDialog()
        {
            Guid id = Guid.CreateVersion7();
        }
    }
}

