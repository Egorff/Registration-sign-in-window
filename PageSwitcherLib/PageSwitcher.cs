using System.Windows.Controls;

namespace PageSwitcherLib
{
    public static class PageSwitcher
    {
        #region Fields

        static Dictionary<string, Page> Pages;

        #endregion

        static public event Action<Page> OnPageChange;

        static PageSwitcher()
        {
            Pages = new Dictionary<string, Page>();
        }

        #region Methods

        public static void RegisterPage(Page page, string key)
        {
            Pages.Add(key, page);     
        }

        public static void ChangePage(string key)
        {
            Page temp = null;

            Pages.TryGetValue(key, out temp);

            OnPageChange?.Invoke(temp);
        }

        public static void UnRegisterPage(string key)
        {
            Pages.Remove(key);
        }

        #endregion
    }
}