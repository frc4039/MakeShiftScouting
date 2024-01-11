using MakeShiftScouting.Models;
using Newtonsoft.Json;
using static MakeShiftScouting.Helpers.Constants;

namespace MakeShiftScouting;

public partial class MainPage : ContentPage
{
    ScoutingPage scoutingPage = null;

    private string currentAppDataDirectory = FileSystem.Current.AppDataDirectory + FOLDER_SEPARATOR; //Android needed?

    private string sourceJsonFile = string.Empty;
    private string makeShiftScoutingJsonFile = string.Empty;
    private string makeShiftScoutingHtmlFile = string.Empty;
    private string makeShiftScoutingStylesFile = string.Empty;
    private string makeShiftScoutingScriptsFile = string.Empty;
    private string bootstrapCssFile = string.Empty;
    private string bootstrapJsFile = string.Empty;
    private string jqueryFile = string.Empty;
    private string qrcodejsFile = string.Empty;

    public MainPage()
	{
		InitializeComponent();
        InitializeVariables();
        UpdateSupportFiles();
        LoadPageDefinition();
        ParsePageObjectIntoHtml();
        RenderPageDefinition();
	}

    protected override async void OnAppearing()
    {
        Permissions.StorageRead readPermision = new Permissions.StorageRead();
        PermissionStatus resultReadRequest = await readPermision.RequestAsync();
        Permissions.StorageWrite writePermision = new Permissions.StorageWrite();
        PermissionStatus resultWriteRequest = await writePermision.RequestAsync();
    }

    private void InitializeVariables()
    {
        makeShiftScoutingJsonFile = currentAppDataDirectory + MAKESHIFT_SCOUTING_JSON_FILENAME;
        makeShiftScoutingHtmlFile = currentAppDataDirectory + MAKESHIFT_SCOUTING_HTML_FILENAME;
        makeShiftScoutingStylesFile = currentAppDataDirectory + MAKESHIFT_SCOUTING_STYLES_FILENAME;
        makeShiftScoutingScriptsFile = currentAppDataDirectory + MAKESHIFT_SCOUTING_SCRIPTS_FILENAME;
        bootstrapCssFile = currentAppDataDirectory + BOOTSTRAP_CSS_FILENAME;
        bootstrapJsFile = currentAppDataDirectory + BOOTSTRAP_JS_FILENAME;
        jqueryFile = currentAppDataDirectory + JQUERY_FILENAME;
        qrcodejsFile = currentAppDataDirectory + QR_CODE_JS_FILENAME;

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

        string sourceBootstrapCssFile = SOURCE_FOLDER + BOOTSTRAP_CSS_FILENAME;
        if (!File.Exists(bootstrapCssFile))
        {
            File.Copy(sourceBootstrapCssFile, bootstrapCssFile);
        }
        else
        {
            if (File.GetLastWriteTime(sourceBootstrapCssFile) > File.GetLastWriteTime(bootstrapCssFile))
            {
                File.Copy(sourceBootstrapCssFile, bootstrapCssFile, true);
            }
        }

        string sourceBootstrapJsFile = SOURCE_FOLDER + BOOTSTRAP_JS_FILENAME;
        if (!File.Exists(bootstrapJsFile))
        {
            File.Copy(sourceBootstrapJsFile, bootstrapJsFile);
        }
        else
        {
            if (File.GetLastWriteTime(sourceBootstrapJsFile) > File.GetLastWriteTime(bootstrapJsFile))
            {
                File.Copy(sourceBootstrapJsFile, bootstrapJsFile, true);
            }
        }

        string sourceJQueryFile = SOURCE_FOLDER + JQUERY_FILENAME;
        if (!File.Exists(jqueryFile))
        {
            File.Copy(sourceJQueryFile, jqueryFile);
        }
        else
        {
            if(File.GetLastWriteTime(sourceJQueryFile) > File.GetLastWriteTime(jqueryFile))
            {
                File.Copy(sourceJQueryFile, jqueryFile, true);
            }
        }

        string sourceQRCodeJsFile = SOURCE_FOLDER + QR_CODE_JS_FILENAME;
        if (!File.Exists(qrcodejsFile))
        {
            File.Copy(sourceQRCodeJsFile, qrcodejsFile);
        }
        else
        {
            if (File.GetLastWriteTime(sourceQRCodeJsFile) > File.GetLastWriteTime(qrcodejsFile))
            {
                File.Copy(sourceQRCodeJsFile, qrcodejsFile, true);
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
                streamWriter.WriteLine(string.Format(STYLESHEET_TAG, BOOTSTRAP_CSS_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine(string.Format(SCRIPT_TAG, BOOTSTRAP_JS_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine(string.Format(SCRIPT_TAG, JQUERY_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine(string.Format(SCRIPT_TAG, QR_CODE_JS_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine(string.Format("<link rel='stylesheet' href='{0}' />", MAKESHIFT_SCOUTING_STYLES_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine(string.Format("<script src='{0}'></script>", MAKESHIFT_SCOUTING_SCRIPTS_FILENAME.Replace("\\", "")));
                streamWriter.WriteLine("</head><body>");
                streamWriter.WriteLine("<h1 class='centered'><span class='gameName'>MakeShift Scouting - {0}</span></h1>", scoutingPage.page_title);
                streamWriter.WriteLine("<form id='scoutingForm'><div class='container text-center'>");
                streamWriter.WriteLine("<div class='row'>");
                foreach (ScoutingPageSection section in scoutingPage.sections)
                {
                    streamWriter.WriteLine("<div class='columnContents'>");
                    streamWriter.WriteLine(string.Format("<div class='sectionHeader headerBackground{1}'>{0}</div>", section.name.ToUpper(), backgroundCount));
                    backgroundCount++;
                    if (backgroundCount > 5)
                    {
                        backgroundCount = 1;
                    }
                    foreach (ScoutingPageSectionField field in section.fields)
                    {
                        streamWriter.WriteLine(string.Format("<div class='fieldTitle'>{0}{1}</div>", field.title, (field.required ? " <span style='color:red'>*</span> " : "")));
                        streamWriter.WriteLine(CreateHtmlFieldFromJson(field));
                    }
                    streamWriter.WriteLine("</div>");
                }
                streamWriter.WriteLine("<div class='columnContents'>");
                streamWriter.WriteLine("<div class='verticalSpacerBottom'><input type='submit' id='generateQrCode' value='Generate QR Code' class='button buttonSubmit rounded-5' /></div>");
                streamWriter.WriteLine("<input type='button' value='Reset Fields' class='button buttonReset rounded-5' id='resetFields'/>");
                streamWriter.WriteLine("</div>");
                streamWriter.WriteLine("</div></div></form>");
                //Modal beginning
                streamWriter.WriteLine("<div class='modal fade' tabindex='-1' role='dialog' id='qrCodeModal'>");
                streamWriter.WriteLine("<div class='modal-dialog' role='document'><div class='modal-content'>");
                streamWriter.WriteLine("<div class='modal-header'>");
                streamWriter.WriteLine("<h4 class='modal-title'><span id='modalTitle'>Scan QR Code</span></h4>");
                streamWriter.WriteLine("</div>");
                streamWriter.WriteLine("<div class='modal-body'>");
                streamWriter.WriteLine("<div id='qrcode'></div>");
                streamWriter.WriteLine("</div>");
                streamWriter.WriteLine("<div class='modal-footer'>");
                streamWriter.WriteLine("<button type='button' class='button buttonClose rounded-4' data-bs-dismiss='modal'>Close</button>");
                streamWriter.WriteLine("</div>");
                streamWriter.WriteLine("</div></div></div>");
                //Model end
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
                htmlFieldFromJson = "<select id='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' class='fieldStyle rounded-5 resettingList'" + (scoutingSectionField.required ? " required " : "") + ">" + GetChoices(scoutingSectionField.choices, scoutingSectionField.defaultValue) + "</select>";
                break;
            case "text":
                htmlFieldFromJson = "<textarea id='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' class='fieldStyle rounded-4 resettingText'" + (scoutingSectionField.required ? " required " : "") + "></textarea>";
                break;
            case "number":
                htmlFieldFromJson = "<input id ='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' type='number' class='fieldStyle rounded-5 plainNumber'" + (scoutingSectionField.required ? " required " : "") + "/>";
                break;
            case "boolean":
                htmlFieldFromJson = 
                    "<div class='form-check' style='margin-left: 10px' >" +
                        "<input id ='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' type='checkbox' role='switch' class='form-check-input rounded-5 checkboxStyle'/>" +
                    "</div><div style='height: 10px'></div>";
                break;
            case "counter":
                htmlFieldFromJson = "<div class='number'><span class='minus'>-</span><input class='counter rounded-5' id ='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' type='text' value='0'/><span class='plus'>+</span></div>";
                break;
            case "autoNumber":
                htmlFieldFromJson = "<input id ='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' type='number' class='fieldStyle rounded-5 autoNumber'" + (scoutingSectionField.required ? " required " : "") + "/>";
                break;
            case "persistentText":
                htmlFieldFromJson = "<textarea id='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' class='fieldStyle rounded-4'" + (scoutingSectionField.required ? " required " : "") + "></textarea>";
                break;
            case "persistentList":
                htmlFieldFromJson = "<select id='" + scoutingSectionField.code + "' name='" + scoutingSectionField.code + "' class='fieldStyle rounded-5'" + (scoutingSectionField.required ? " required " : "") + ">" + GetChoices(scoutingSectionField.choices, scoutingSectionField.defaultValue) + "</select>";
                break;
        }
        return htmlFieldFromJson;
    }

    private string GetChoices(Dictionary<string, string> choices, string defaultValue)
    {
        string choices1 = string.Empty;
        if (!choices.ContainsKey(defaultValue))
        {
            choices1 = "<option value='' selected disabled>Select option</option>";
        }
        foreach (KeyValuePair<string, string> choice in choices)
        {
            choices1 += string.Format("<option value='{0}' {1}>{2}</option>", choice.Key, choice.Key.Equals(defaultValue) ? "selected" : "", choice.Value);
        }
        return choices1;
    }
}

