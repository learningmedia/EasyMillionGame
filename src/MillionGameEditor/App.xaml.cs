using System.Windows;
using System.Windows.Input;

namespace MillionGameEditor
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public sealed partial class App
    {
        private void OnUnfocusKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Escape)
            {
                FrameworkElement p2 = (FrameworkElement) LogicalTreeHelper.GetParent((DependencyObject) sender);
                p2.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }
        }
    }
}