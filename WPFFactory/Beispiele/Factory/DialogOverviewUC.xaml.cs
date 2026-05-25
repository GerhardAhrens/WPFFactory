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
            this.DeleteDataCommand = new CommandBase(this.OnDeleteData, () => true);

            this.DataContext = this;
        }

        public string StatusMessage
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public ChangeViewEventArgs CurrentCtorArgs { get; set; }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (App.EventAgg.IsSubscription<StatusEvent>() == true)
            {
                await App.EventAgg.PublishAsync(new StatusEvent(Guid.NewGuid(), "Bereit"));
            }
        }

        public CommandBase GoBackCommand { get; private set; }
        public CommandBase EditDialogCommand { get; private set; }
        public CommandBase DeleteDataCommand { get; private set; }

        private MessageBase Message { get; } = new MessageBase();

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

        private async void OnEditDialog()
        {
            ChangeViewEventArgs args = this.CurrentCtorArgs;
            args.MenuButton = DialogView.DialogEdit;
            args.FromPage = DialogView.DialogOverView;
            args.EntityId = Guid.CreateVersion7();

            if (App.EventAgg.IsSubscription<ChangeViewEventArgs>() == true)
            {
                await App.EventAgg.PublishAsync(args);
            }
        }
        private void OnDeleteData()
        {
            this.Message.Question("Löschen", "Soll der gewählte Datensatz gelöscht werden=");
        }
    }
}

