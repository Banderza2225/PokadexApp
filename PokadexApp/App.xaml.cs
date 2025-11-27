namespace PokadexApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           // ApplyTheme();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    


    
    } 

}