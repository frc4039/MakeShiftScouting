using MakeShiftScouting.Models;

namespace MakeShiftScouting
{
    public partial class MainPage : ContentPage
    {
        ScoutingPage scoutingPage = null;
        string xamlPage = "";

        public MainPage()
        {
            InitializeComponent();
            LoadPageDefinition();
            ParsePageObjectIntoHtml();
            RenderPageDefinition();
        }

        private void LoadPageDefinition()
        {
            string jsonContent = "";
            try
            {
                //C:\Users\Pablo\AppData\Local\Packages\5d67b27d-2fdc-4271-8dc4-19c056becf83_9zz4h110yvjzm\LocalState
                using (StreamReader streamReader = new StreamReader(FileSystem.Current.AppDataDirectory + "\\4039 QR Scout Feb 25 v11.json"))
                {
                    jsonContent = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                scoutingPage = JsonConvert.DeserializeObject<ScoutingPage>(jsonContent);
            }
            catch (Exception ex)
            {
                //load error page for json
            }
        }

        private void ParsePageObjectIntoHtml()
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(FileSystem.Current.AppDataDirectory + "MakeShiftScoutingPage.html"))
                {
                    //set title using scoutingPage title and page title
                    foreach (ScoutingPageSection scoutingPageSection in scoutingPage.sections)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                //load error page for parsing
            }
        }

        private void RenderPageDefinition()
        {
            webViewScounting.Source = "C:\\Users\\Pablo\\Downloads\\MacPGames\\MacPGames.htm";
            //using (StreamReader streamReader = new StreamReader(FileSystem.Current.AppDataDirectory + "\\MainPage.xaml"))
            //{
            //    xamlPage = streamReader.ReadToEnd();
            //    streamReader.Close();
            //}
            //this.Content.LoadFromXaml(xamlPage);
        }
    }

