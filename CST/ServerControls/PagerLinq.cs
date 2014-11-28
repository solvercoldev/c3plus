
/* 
 
    * PAGINADOR DE CONTROLES UTILIZANDO LINQ.
    * WALTER MOLANO
    * VIERNES 23 DE SEPTIEMBRE DE 2011
    
 */
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServerControls
{

    [DefaultProperty("Text"), ToolboxData("<{0}:Pager runat=server></{0}:Pager>")]
    public class PagerLinq : WebControl, INamingContainer
    {

        public delegate void PageChangedEventHandler(object sender, PageChanged e);
        public event PageChangedEventHandler PageChanged;

        private const int DefaultMaxDisplayPages = 10;
        private const int DefaultPageSize = 10;

        #region Properties

        /// <summary>
        /// Numero de Total de registros.
        /// </summary>
        [Browsable(true)]
        public int RowCount
        {
            private get
            {
                return ViewState["RowCount"] != null ?  (int)ViewState["RowCount"] : 0;
            }
            set { ViewState["RowCount"] = value; }
        }

        
        private int TotalPages
        {
            get
            {
                return ViewState["TotalPages"] != null ? (int)ViewState["TotalPages"] : 0;
            }
            set { ViewState["TotalPages"] = value; }
        }

        [DefaultValue(DefaultMaxDisplayPages)]
        public int MaxDisplayPages
        {
            get
            {
                return ViewState["MaxDisplayPages"] != null
                           ? (int)ViewState["MaxDisplayPages"]
                           : DefaultMaxDisplayPages;
            }
            set
            {
                ViewState["MaxDisplayPages"] = value;
            }
        }

        private int CurrentPageIndex
        {
            get { return ViewState["CurrentPageIndex"] != null ? (int)ViewState["CurrentPageIndex"] : 0; }
            set { ViewState["CurrentPageIndex"] = value; }
        }

        [DefaultValue(DefaultPageSize)]
        [Browsable(true)]
        public int PageSize
        {
            get { return (int)ViewState["PageSize"]; }
            set { ViewState["PageSize"] = value; }
        }

        #endregion

        #region Method

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Site != null && this.Site.DesignMode)
            {
                writer.Write("1 2 3 ...");
            }
            base.Render(writer);
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            TotalPages = GetTotalPages();
            BuildNavigationControls();
            base.CreateChildControls();
        }

        private void BuildNavigationControls()
        {


            var currentPageGroupIndex = GetCurrentPageGroupIndex();
            var totalPageGroups = GetTotalPageGroups();

            if (TotalPages == 1) CurrentPageIndex = 0;

            // First
            var firstPageControl = CreateLinkControl(ButtonAction.First, "<<", 0);
            firstPageControl.Visible = CurrentPageIndex > 0;
            Controls.Add(firstPageControl);

            // Prev
            var prevPageControl = CreateLinkControl(ButtonAction.Prev, "<", CurrentPageIndex - 1);
            prevPageControl.Visible = CurrentPageIndex > 0;
            Controls.Add(prevPageControl);

            // Previous page group
            var prevPageGroupPageIndex = (currentPageGroupIndex - 1) * MaxDisplayPages;
            var prevGroupControl = CreateLinkControl(ButtonAction.PrevGroup, "...", prevPageGroupPageIndex);
            prevGroupControl.Visible = currentPageGroupIndex > 0;
            Controls.Add(prevGroupControl);

            // Numbers
            var beginPageNumberIndex = currentPageGroupIndex * MaxDisplayPages;
            var endPageNumberIndex = beginPageNumberIndex + MaxDisplayPages - 1;
            if (TotalPages <= endPageNumberIndex)
            {
                endPageNumberIndex = TotalPages - 1;
                if (endPageNumberIndex - MaxDisplayPages >= 0)
                {
                    beginPageNumberIndex = endPageNumberIndex - MaxDisplayPages;
                }
            }
            for (var i = 0; i < TotalPages; i++)
            {
                var pageNumberString = Convert.ToString(i + 1);
                var numberControl = CreateLinkControl(ButtonAction.Page, pageNumberString, i);
                if (i >= beginPageNumberIndex && i <= endPageNumberIndex)
                {
                    if (CurrentPageIndex == i)
                    {
                        var currentPageLabel = new Label { Text = pageNumberString };
                        currentPageLabel.Font.Bold = true;
                        numberControl = currentPageLabel;
                    }
                }
                else
                {
                    numberControl.Visible = false;
                }
                Controls.Add(numberControl);
            }

            // Next page group
            var nextPageGroupPageIndex = (currentPageGroupIndex + 1) * MaxDisplayPages;
            var nextGroupControl = CreateLinkControl(ButtonAction.NextGroup, "...", nextPageGroupPageIndex);
            nextGroupControl.Visible = currentPageGroupIndex < totalPageGroups - 1;
            Controls.Add(nextGroupControl);

            // Next
            var nextPageControl = CreateLinkControl(ButtonAction.Next, ">", CurrentPageIndex + 1);
            nextPageControl.Visible = CurrentPageIndex < TotalPages - 1;
            Controls.Add(nextPageControl);

            // Last
            var lastPageControl = CreateLinkControl(ButtonAction.Last, ">>", TotalPages - 1);
            lastPageControl.Visible = CurrentPageIndex < TotalPages - 1;
            Controls.Add(lastPageControl);

        }

        private Control CreateLinkControl(ButtonAction buttonAction, string buttonText, int pageIndex)
        {
            var lbt = new LinkButton { CausesValidation = false, ID = buttonAction.ToString(), Text = buttonText };

            switch (buttonAction)
            {
                case ButtonAction.First:
                    lbt.Click += FirstClick;
                    break;
                case ButtonAction.Prev:
                    lbt.Click += PrevClick;
                    break;
                case ButtonAction.PrevGroup:
                    lbt.Click += PrevGroupClick;
                    break;
                case ButtonAction.Page:
                    // Override ID with the page index. 
                    lbt.ID = pageIndex.ToString();
                    lbt.Click += NumberClick;
                    break;
                case ButtonAction.NextGroup:
                    lbt.Click += NextGroupClick;
                    break;
                case ButtonAction.Next:
                    lbt.Click += NextClick;
                    break;
                case ButtonAction.Last:
                    lbt.Click += LastClick;
                    break;
            }

            Control control = lbt;
            return control;
        }

        private int GetCurrentPageGroupIndex()
        {
            return (int)Math.Floor((CurrentPageIndex / (double)MaxDisplayPages));
        }

        private int GetTotalPageGroups()
        {
            var temp = (TotalPages / (double)MaxDisplayPages);
            return (int)Math.Ceiling(temp);
        }

        private int GetTotalPages()
        {
            var temp = RowCount / (double)PageSize;
            return (int)Math.Ceiling(temp);
        }
        #endregion

        #region Events

        private void FirstClick(object sender, EventArgs e)
        {
            const int nextPage = 0;
            var args = new PageChanged(CurrentPageIndex, nextPage);
            OnPageChanged(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void PrevClick(object sender, EventArgs e)
        {
            var nextPage = CurrentPageIndex - 1;
            var args = new PageChanged(CurrentPageIndex, nextPage);
            OnPageChanged(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void NumberClick(object sender, EventArgs e)
        {
            var nextPage = Int32.Parse(((LinkButton)sender).ID);
            var args = new PageChanged(CurrentPageIndex, nextPage);
            OnPageChanged(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void NextClick(object sender, EventArgs e)
        {
            var nextPage = CurrentPageIndex + 1;
            var args = new PageChanged(CurrentPageIndex, nextPage);
            OnPageChanged(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void LastClick(object sender, EventArgs e)
        {
            var nextPage = TotalPages - 1;
            var args = new PageChanged(CurrentPageIndex, nextPage);
            OnPageChanged(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void PrevGroupClick(object sender, EventArgs e)
        {
            var nextPage = (GetCurrentPageGroupIndex() - 1) * MaxDisplayPages;
            var args = new PageChanged(CurrentPageIndex, nextPage);
            OnPageChanged(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void NextGroupClick(object sender, EventArgs e)
        {
            var nextPage = (GetCurrentPageGroupIndex() + 1) * MaxDisplayPages;
            var args = new PageChanged(CurrentPageIndex, nextPage);
            OnPageChanged(args);
            Controls.Clear();
            BuildNavigationControls();
        }

        private void OnPageChanged(PageChanged e)
        {
            CurrentPageIndex = e.CurrentPage;
            if (PageChanged != null)
                PageChanged(this, e);
        }

        private enum ButtonAction
        {
            First,
            Prev,
            PrevGroup,
            Page,
            NextGroup,
            Next,
            Last
        }

        #endregion
    }

    public class PageChanged
    {
        private readonly int _prevPage;
        private readonly int _currentPage;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prevPage"></param>
        /// <param name="currentPage"></param>
        public PageChanged(int prevPage, int currentPage)
        {
            _prevPage = prevPage;
            _currentPage = currentPage;
        }

        /// <summary>
        /// The page index of the previous page.
        /// </summary>
        public int PrevPage
        {
            get { return _prevPage; }
        }

        /// <summary>
        /// The page index of the current page (after clicking next).
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
        }


    }
}