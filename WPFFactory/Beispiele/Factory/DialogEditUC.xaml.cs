namespace WPFFactory.Beispiele
{
    using System.Windows;

    /// <summary>
    /// Interaktionslogik für DialogEditUC.xaml
    /// </summary>
    public partial class DialogEditUC : UserControlBase
    {
        public DialogEditUC(ChangeViewEventArgs args)
        {
            this.InitializeComponent();

            this.CurrentCtorArgs = args;

            WeakEventManager<UserControlBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);

            this.GoBackCommand = new CommandBase(this.OnGoBack, () => true);
            this.SaveDataCommand = new CommandBase(this.OnSaveData, () => true);
            this.DeleteDataCommand = new CommandBase(this.OnDeleteData, () => true);
            this.DataContext = this;
        }

        public CommandBase GoBackCommand { get; private set; }
        public CommandBase SaveDataCommand { get; private set; }
        public CommandBase DeleteDataCommand { get; private set; }


        private MessageBase Message { get; } = new MessageBase();
        private ChangeViewEventArgs CurrentCtorArgs { get; set; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DialogEditTitle.Text = $"Dialog bearbeiten: {this.CurrentCtorArgs.EntityId}";
        }

        private async void OnGoBack()
        {
            ChangeViewEventArgs args = this.CurrentCtorArgs;
            args.MenuButton = DialogView.DialogOverView;
            args.FromPage = DialogView.DialogEdit;

            if (App.EventAgg.IsSubscription<ChangeViewEventArgs>() == true)
            {
                await App.EventAgg.PublishAsync(args);
            }
        }

        private async void OnSaveData()
        {
            if (App.EventAgg.IsSubscription<StatusEvent>() == true)
            {
                await App.EventAgg.PublishAsync(new StatusEvent(Guid.NewGuid(), "Gespeichert"));
            }
        }

        private void OnDeleteData()
        {
            this.Message.Question("Löschen","Soll der gewählte Datensatz gelöscht werden=");
        }
    }
}

