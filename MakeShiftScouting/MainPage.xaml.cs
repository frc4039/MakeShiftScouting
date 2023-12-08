using MakeShiftScouting.Models;
using Newtonsoft.Json;
using static MakeShiftScouting.Helpers.Constants;

namespace MakeShiftScouting;

public partial class MainPage : ContentPage
{
    ScoutingPage scoutingPage = null;

    private string currentAppDirectory = FileSystem.Current.AppDataDirectory;

    private string makeShiftScoutingJsonFile;
    private string makeShiftScoutingHtmlFile;
    private string makeShiftScoutingStylesFile;
    private string makeShiftScoutingScriptsFile;

    private string sourceJsonFile;


    public MainPage()
	{
		InitializeComponent();
        InitializeVariables();
        UpdateSupportFiles();
        LoadPageDefinition();
        ParsePageObjectIntoHtml();
        RenderPageDefinition();
	}

    private void InitializeVariables()
    {
        makeShiftScoutingJsonFile = currentAppDirectory + MAKESHIFT_SCOUTING_JSON_FILENAME;
        makeShiftScoutingHtmlFile = currentAppDirectory + MAKESHIFT_SCOUTING_HTML_FILENAME;
        makeShiftScoutingStylesFile = currentAppDirectory + MAKESHIFT_SCOUTING_STYLES_FILENAME;
        makeShiftScoutingScriptsFile = currentAppDirectory + MAKESHIFT_SCOUTING_SCRIPTS_FILENAME;

        sourceJsonFile = SOURCE_FOLDER + "\\4039 QR Scout Feb 25 v11.json";
    }

    private void UpdateSupportFiles()
    {
        if(!File.Exists(makeShiftScoutingJsonFile))
        {
            File.Copy(sourceJsonFile, makeShiftScoutingJsonFile);
        }
        else
        {
            if(File.GetLastWriteTime(sourceJsonFile) > File.GetLastWriteTime(makeShiftScoutingJsonFile))
            {
                File.Copy(sourceJsonFile, makeShiftScoutingJsonFile, true);
            }
        }
    }


    private void LoadPageDefinition()
	{
        string jsonContent = "";
        try
        {
            //C:\Users\Pablo\AppData\Local\Packages\5d67b27d-2fdc-4271-8dc4-19c056becf83_9zz4h110yvjzm\LocalState
            //C:\Users\pcypc\AppData\Local\Packages\8c337c81-847f-4075-9202-d201b971f860_9zz4h110yvjzm\LocalState
            using (StreamReader streamReader = new StreamReader(makeShiftScoutingJsonFile))
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
        webViewScounting.Source = makeShiftScoutingHtmlFile;
        //using (StreamReader streamReader = new StreamReader(FileSystem.Current.AppDataDirectory + "\\MainPage.xaml"))
        //{
        //    xamlPage = streamReader.ReadToEnd();
        //    streamReader.Close();
        //}
        //this.Content.LoadFromXaml(xamlPage);
    }
}

