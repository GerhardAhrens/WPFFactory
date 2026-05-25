//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Lifeprojects.de">
//     Class: MainWindow
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>05.03.2026 18:21:36</date>
//
// <summary>
// WPF Template mit Minimalfunktionen
// </summary>
//-----------------------------------------------------------------------

namespace WPFFactory
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using WPFFactory.Beispiele;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            this.InitializeComponent();
            WeakEventManager<WindowBase, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
            WeakEventManager<WindowBase, CancelEventArgs>.AddHandler(this, "Closing", this.OnWindowClosing);

            this.SetVectorIcon("IconApplication", 64);

            this.RegisterFactory();

            this.QuitCommand = new CommandBase(this.OnQuit, () => true);
            this.ChangeViewCommand = new CommandBase(p => this.ChangeView(p), () => true);

            this.InformationCommand = new CommandBase(this.OnInformationPopup);
            this.CloseInformationPopupCommand = new CommandBase(this.OnCloseInformation);
            this.SettingsCommand = new CommandBase(this.OnSettingsPopup);
            this.CloseSettingsPopupCommand = new CommandBase(this.OnCloseSettingsPopup);

            this.WindowTitel = LocalizationValue.Get("WindowsTitelZeile");
            this.ApplikationVersion = base.ApplicationVersion.ToString();
            this.LaufzeitVersion = base.RuntimeVersion;
            this.WinVersion = base.WindowsVersion;
            this.DataContext = this;

        }

        #region Commands
        public CommandBase QuitCommand { get; private set; }
        public CommandBase ChangeViewCommand { get; private set; }

        public CommandBase InformationCommand { get; private set; }
        public CommandBase CloseInformationPopupCommand { get; private set; }
        public CommandBase SettingsCommand { get; private set; }
        public CommandBase CloseSettingsPopupCommand { get; private set; }
        #endregion Commands

        #region Properties
        public string WindowTitel
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string ApplikationVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string LaufzeitVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string WinVersion
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public object WorkContent
        {
            get { return base.GetValue<object>(); }
            set { base.SetValue(value); }
        }

        private MessageBase Message { get; } = new MessageBase();
        #endregion Properties

        #region Windows Event Handler
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StatusbarMain.Statusbar.DatabaseInfo = "Keine";
            StatusbarMain.Statusbar.DatabaseInfoTooltip = "Keine Datenbank verbunden";
            StatusbarMain.Statusbar.Notification = "Bereit";

            App.EventAgg.Subscribe<ChangeViewEventArgs>(async (evt, ct) => this.ChangeControl(evt));
            App.EventAgg.Subscribe<StatusEvent>(async (evt, ct) => this.OnUpdateStatusBar(evt));


            ChangeViewEventArgs args = new();
            args.MenuButton = BasisView.Home;
            args.FromPage = BasisView.Home;
            this.ChangeControl(args);
        }

        private void OnCloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;

            MessageBoxResult msgYN;
            if (this.Tag != null)
            {
                msgYN = this.Message.AppExitMessage(this.Tag.ToString());
            }
            else
            {
                msgYN = this.Message.AppExitMessage();
            }

            if (msgYN == MessageBoxResult.Yes)
            {
                App.ApplicationExit();
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion Windows Event Handler

        #region Command Event Handler
        private void OnQuit()
        {
            this.Tag = null;
            this.Close();
        }

        private void ChangeView(object p)
        {
            if (p != null && p is string viewName)
            {
                if (viewName == "Home")
                {
                    ChangeViewEventArgs args = new();
                    args.MenuButton = BasisView.Home;
                    args.FromPage = BasisView.Home;
                    this.ChangeControl(args);
                }
                else if (viewName == "Liste")
                {
                    ChangeViewEventArgs args = new();
                    args.MenuButton = DialogView.DialogOverView;
                    args.FromPage = BasisView.Home;
                    this.ChangeControl(args);
                }
            }
        }

        private void OnInformationPopup()
        {
            this.InformationPopup.SetValue(MaskLayerBehavior.IsOpenProperty, true);
        }

        private void OnCloseInformation()
        {
            this.InformationPopup.SetValue(MaskLayerBehavior.IsOpenProperty, false);
        }

        private void OnSettingsPopup()
        {
            this.SettingsPopup.SetValue(MaskLayerBehavior.IsOpenProperty, true);
        }

        private void OnCloseSettingsPopup()
        {
            this.SettingsPopup.SetValue(MaskLayerBehavior.IsOpenProperty, false);
        }
        #endregion Command Event Handler

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Member als statisch markieren", Justification = "<Ausstehend>")]
        private void RegisterFactory()
        {
            Factory.RegisterSingleton<BasisView>(BasisView.Home, () => new HelloUC());
            Factory.RegisterTransient<DialogView>(DialogView.DialogOverView, (param) => new DialogOverviewUC((ChangeViewEventArgs)param!));
            Factory.RegisterTransient<DialogView>(DialogView.DialogEdit, (param) => new DialogEditUC((ChangeViewEventArgs)param!));
        }

        private void ChangeControl(ChangeViewEventArgs args)
        {
            this.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait);

            this.WorkContent = null;

            if (args.MenuButton is BasisView)
            {
                this.WorkContent = Factory.Get<UserControl, BasisView>((BasisView)args.MenuButton, args);
            }
            else if (args.MenuButton is DialogView)
            {
                this.WorkContent = Factory.Get<UserControl, DialogView>((DialogView)args.MenuButton, args);
            }

            this.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Arrow);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Member als statisch markieren", Justification = "<Ausstehend>")]
        private void OnUpdateStatusBar(StatusEvent evt)
        {
            StatusbarMain.Statusbar.Notification = evt.Message;
        }

    }
}