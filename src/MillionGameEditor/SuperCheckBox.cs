using System.Windows;
using System.Windows.Controls;

namespace MillionGameEditor
{
    public sealed class SuperCheckBox : CheckBox
    {
        protected override void OnToggle()
        {
            if (this.IsChecked != true)
            {
                this.IsChecked = true;

                DependencyObject parent = LogicalTreeHelper.GetParent(this);
                foreach (object child in LogicalTreeHelper.GetChildren(parent))
                {
                    if (child is SuperCheckBox && !ReferenceEquals(child, this))
                    {
                        SuperCheckBox sc = (SuperCheckBox) child;
                        if (sc.IsChecked == true)
                        {
                            sc.IsChecked = false;
                        }
                    }
                }
            }
        }
    }
}