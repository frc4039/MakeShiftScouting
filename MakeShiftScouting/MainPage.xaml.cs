using MakeShiftScouting.Models;
using Newtonsoft.Json;
using static MakeShiftScouting.Helpers.Constants;

namespace MakeShiftScouting;

public partial class MainPage : ContentPage
{
    ScoutingPage scoutingPage = null;

    private string currentAppDirectory = FileSystem.Current.AppDataDirectory;

    private string sourceJsonFile = string.Empty;
    private string makeShiftScoutingJsonFile = string.Empty;
    private string makeShiftScoutingHtmlFile = string.Empty;
    private string makeShiftScoutingStylesFile = string.Empty;
    private string makeShiftScoutingScriptsFile = string.Empty;

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

        string sourceStylesFile = SOURCE_FOLDER + MAKESHIFT_SCOUTING_STYLES_FILENAME;
        if (!File.Exists(makeShiftScoutingStylesFile))
        {
            File.Copy(sourceStylesFile, makeShiftScoutingStylesFile);
        }
        else
        {
            if (File.GetLastWriteTime(sourceStylesFile) > File.GetLastWriteTime(makeShiftScoutingStylesFile))
            {
                File.Copy(sourceStylesFile, makeShiftScoutingStylesFile, true);
            }
        }

        string sourceScriptsFile = SOURCE_FOLDER + MAKESHIFT_SCOUTING_SCRIPTS_FILENAME;
        if (!File.Exists(makeShiftScoutingScriptsFile))
        {
            File.Copy(sourceScriptsFile, makeShiftScoutingScriptsFile);
        }
        else
        {
            if (File.GetLastWriteTime(sourceScriptsFile) > File.GetLastWriteTime(makeShiftScoutingScriptsFile))
            {
                File.Copy(sourceScriptsFile, makeShiftScoutingScriptsFile, true);
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
            Console.WriteLine(ex.Message);
        }
    }

    private void ParsePageObjectIntoHtml()
    {
        int backgroundCount = 1;
        try
        {
            using (StreamWriter streamWriter = new StreamWriter(makeShiftScoutingHtmlFile))
            {
                streamWriter.WriteLine("<html><head>");
                streamWriter.WriteLine(BOOTSTRAP_CSS);
                streamWriter.WriteLine(BOOTSTRAP_JS);
                streamWriter.WriteLine(JQUERY);
                streamWriter.WriteLine(string.Format("<link rel='stylesheet' href='{0}' />", MAKESHIFT_SCOUTING_STYLES_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine(string.Format("<script src='{0}'></script>", MAKESHIFT_SCOUTING_SCRIPTS_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine("</head><body>");
                streamWriter.WriteLine("<h1 class='centered'><span class='GameName'>MakeShift Scouting - {0}</span></h1>", scoutingPage.page_title);
                streamWriter.WriteLine("<div class='container text-center'>");
                streamWriter.WriteLine("<div class='row row-cols-auto'>");
                foreach (ScoutingPageSection section in scoutingPage.sections)
                {
                    streamWriter.WriteLine("<div class='col colWidth'><div class='columnContents'>");
                    streamWriter.WriteLine(string.Format("<div class='sectionHeader headerBackground{1}'>{0}</div>", section.name.ToUpper(), backgroundCount));
                    backgroundCount++;
                    if (backgroundCount > 7)
                    {
                        backgroundCount = 1;
                    }
                    foreach (ScoutingPageSectionField field in section.fields)
                    {
                        streamWriter.WriteLine(string.Format("<div>{0}</div>", field.title));
                        streamWriter.WriteLine(CreateHtmlFieldFromJson(field));
                    }
                    streamWriter.WriteLine("</div></div>");
                }
                streamWriter.WriteLine("</div></div>");
                streamWriter.WriteLine("</body></html>");
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
        catch (Exception ex)
        {
            //load error page for parsing
            Console.WriteLine(ex.Message);
        }
    }

    private void RenderPageDefinition()
    {
        webViewScounting.Source = makeShiftScoutingHtmlFile;
    }

    private string CreateHtmlFieldFromJson(ScoutingPageSectionField scoutingSectionField)
    {
        string htmlFieldFromJson = string.Empty;
        switch (scoutingSectionField.type)
        {
            case "select":
                htmlFieldFromJson = "<select id='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' class='fieldStyle'>" + GetChoices(scoutingSectionField.choices, scoutingSectionField.defaultValue) + "</select>";
                break;
            case "text":
                htmlFieldFromJson = "<textarea id='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' class='fieldStyle'></textarea>";
                break;
            case "number":
                htmlFieldFromJson = "<input id ='" + scoutingSectionField.code + "' type='number' class='fieldStyle'/>";
                break;
            case "boolean":
                htmlFieldFromJson = "<input id ='" + scoutingSectionField.code + "' type='checkbox' role='switch' class='fieldStyle'/>";
                break;
            case "counter":
                htmlFieldFromJson = "<div class='number'><span class='minus'>-</span><input class='counter' id ='" + scoutingSectionField.code + "' type='text' value='0'/><span class='plus'>+</span></div>";
                break;
        }
        return htmlFieldFromJson;
    }

    private string GetChoices(Dictionary<string, string> choices, string defaultValue)
    {
        string choices1 = string.Empty;
        if (!choices.ContainsKey(defaultValue))
            choices1 = "<option value='' selected disabled>Select option</option>";
        foreach (KeyValuePair<string, string> choice in choices)
            choices1 += string.Format("<option value='{0}' {1}>{2}</option>", choice.Key, choice.Key.Equals(defaultValue) ? "selected" : "", choice.Value);
        return choices1;
    }
}

