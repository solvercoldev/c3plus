using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ServerControls
{
    /// <summary>
    ///  
    /// (http://www.dynarch.com/projects/calendar/). 
    /// </summary>
    [DefaultProperty("Text"), ToolboxData("<{0}:Calendar runat=server></{0}:Calendar>")]
    [ValidationProperty("Text")]
    public sealed class Calendar : WebControl, INamingContainer
    {
        private TextBox _dateTextBox;
        private Image _calendarImage;

        #region properties

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Text
        {
            get
            {
                EnsureChildControls();
                return this._dateTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                this._dateTextBox.Text = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(CalendarTheme.system)]
        public CalendarTheme Theme
        {
            get { return (ViewState["Theme"] != null ? (CalendarTheme)ViewState["Theme"] : CalendarTheme.system); }
            set { ViewState["Theme"] = value; }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(CalendarLanguage.en)]
        public CalendarLanguage Language
        {
            get { return (ViewState["Language"] != null ? (CalendarLanguage)ViewState["Language"] : CalendarLanguage.en); }
            set { ViewState["Language"] = value; }
        }

        [Bindable(true), Category("Behavior"), DefaultValue("~/Support/JsCalendar")]
        public string SupportDir
        {
            get { return (ViewState["SupportDir"] != null ? (string)ViewState["SupportDir"] : ""); }
            set { ViewState["SupportDir"] = value; }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(false)]
        public bool DisplayTime
        {
            get { return (ViewState["DisplayTime"] != null ? (bool)ViewState["DisplayTime"] : false); }
            set { ViewState["DisplayTime"] = value; }
        }

        [Browsable(false), Bindable(false)]
        public string DateFormat
        {
            get { return (ViewState["DateFormat"] != null ? (string)ViewState["DateFormat"] : ""); }
            set { ViewState["DateFormat"] = value; }
        }

        [Browsable(false), Bindable(false)]
        public string TimeFormat
        {
            get { return (ViewState["TimeFormat"] != null ? (string)ViewState["TimeFormat"] : ""); }
            set { ViewState["TimeFormat"] = value; }
        }

        [Bindable(true), Category("Behavior")]
        public DateTime SelectedDate
        {
            get
            {
                EnsureChildControls();
                if (Text.Length > 0)
                {
                    try
                    {
                        return DateTime.Parse(Text);
                    }
                    catch (FormatException ex)
                    {
                        System.Diagnostics.Trace.WriteLine("Invalid datetime: " + _dateTextBox.Text + " " + ex.Message, GetType().FullName);
                        return DateTime.MinValue;
                    }
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                EnsureChildControls();
                Text = value.ToShortDateString();
                if (DisplayTime)
                {
                    Text += " " + value.ToShortTimeString();
                }
            }
        }

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        [TypeConverter(typeof(UnitConverter))]
        public override Unit Width
        {
            get
            {
                EnsureChildControls();
                return base.Width;
            }
            set
            {
                EnsureChildControls();
                base.Width = value;
                this._dateTextBox.Width = Unit.Pixel((int)value.Value - 24);
            }
        }



        #endregion

        public event System.EventHandler DateChanged;

        private void OnDateChanged(object sender)
        {
            if (DateChanged != null)
            {
                DateChanged(sender, EventArgs.Empty);
            }
        }

        public Calendar()
        {
            // Set defaults
            Theme = CalendarTheme.system;
            SupportDir = "~/Support/JsCalendar";
            DisplayTime = false;
            DateFormat = ConvertDateFormat(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
            TimeFormat = ConvertTimeFormat(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
            SetLanguage();
        }

        protected override void CreateChildControls()
        {
            _dateTextBox = new TextBox();
            _calendarImage = new Image();

            _dateTextBox.EnableViewState = true;
            _dateTextBox.ID = "dateTextBox";
            _dateTextBox.TextChanged += DateTextBox_TextChanged;

            _calendarImage.EnableViewState = false;
            _calendarImage.ID = "trigger";
            _calendarImage.ImageUrl = GetClientFileUrl("cal.gif");
            _calendarImage.Attributes["align"] = "top";
            _calendarImage.Attributes["hspace"] = "4";

            Controls.Add(_dateTextBox);
            Controls.Add(_calendarImage);
        }

        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (Site != null && Site.DesignMode)
            {
                _dateTextBox.RenderControl(output);
                output.Write("[" + ID + "]");
            }
            else
            {
                base.Render(output);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            var themeCss = GetClientCssImport(String.Format("calendar-{0}.css", Theme.ToString().Replace("_", "-")));
            Page.RegisterClientScriptBlock("calendarcss", themeCss);
            
            var calendarScripts = "";
            calendarScripts += GetClientScriptInclude("calendar.js");
            calendarScripts += GetClientScriptInclude("calendar-setup.js");
            var languageFile = String.Format("lang/calendar-{0}.js", Language);
            calendarScripts += GetClientScriptInclude(languageFile);
            Page.RegisterClientScriptBlock("calendarscripts", calendarScripts);
            var setupScript = GetCalendarSetupScript(_dateTextBox.ClientID, GetFormatString(), ClientID);
            Page.RegisterStartupScript(ClientID + "script", setupScript);
        }

        private void SetLanguage()
        {
            var currentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            try
            {
                var cl = (CalendarLanguage)Enum.Parse(typeof(CalendarLanguage), currentLanguage);
                Language = cl;
            }
            catch
            {
                // Default is 'en'
                Language = CalendarLanguage.en;
            }
        }

        private string GetClientFileUrl(string fileName)
        {
            return ResolveUrl(SupportDir + "/" + fileName);
        }

        private string GetClientScriptInclude(string scriptFile)
        {
            return "<script language=\"JavaScript\" src=\"" +
                GetClientFileUrl(scriptFile) + "\"></script>\n";
        }

        private string GetClientCssImport(string fileName)
        {
            return "<style type=\"text/css\">@import url(" + GetClientFileUrl(fileName) + ");</style>\n";
        }

        private string GetCalendarSetupScript(string inputField, string format, string trigger)
        {
            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">Calendar.setup( { inputField : \"");
            sb.Append(inputField);
            sb.Append("\", ifFormat : \"");
            sb.Append(format);
            sb.Append("\", button : \"");
            sb.Append(trigger);
            sb.Append("\", showsTime : ");
            sb.Append(DisplayTime.ToString().ToLower());
            sb.Append(" } ); </script>");
            return sb.ToString();
        }

        public string GetFormatString()
        {
            if (DisplayTime)
            {
                return DateFormat + " " + TimeFormat;
            }
            return DateFormat;
        }

        private void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            OnDateChanged(sender);
        }

        private string ConvertDateFormat(string shortDateFormat)
        {
            var tempFormat = ReplaceFormatCharacter(shortDateFormat, "y", "%Y");
            tempFormat = ReplaceFormatCharacter(tempFormat, "M", "%m");
            tempFormat = ReplaceFormatCharacter(tempFormat, "d", "%d");
            return tempFormat;
        }

        private string ConvertTimeFormat(string shortTimeFormat)
        {
            var tempFormat = ReplaceFormatCharacter(shortTimeFormat, "H", "%H");
            tempFormat = ReplaceFormatCharacter(tempFormat, "m", "%M");
            tempFormat = ReplaceFormatCharacter(tempFormat, "h", "%I");
            tempFormat = tempFormat.Replace("tt", "%p");
            return tempFormat;
        }

        private string ReplaceFormatCharacter(string shortDateFormat, string from, string to)
        {
            var pattern = from + "{1,4}";
            var regex = new Regex(pattern, RegexOptions.Compiled);
            return regex.Replace(shortDateFormat, to);
        }
    }

    public enum CalendarTheme
    {
        Aqua,
        Blue,
        Blue2,
        Brown,
        Green,
        Win2K1,
        Win2K2,
        Win2KCold1,
        Win2KCold2,
        system
    }

    public enum CalendarLanguage
    {
        af,
        br,
        ca,
        da,
        de,
        du,
        el,
        en,
        es,
        fi,
        fr,
        hr,
        hu,
        it,
        jp,
        ko,
        lt,
        nl,
        no,
        pl,
        pt,
        ro,
        ru,
        sl,
        si,
        sk,
        sp,
        sv,
        tr,
        zh
    }
}