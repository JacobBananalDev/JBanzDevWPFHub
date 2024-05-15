using System.Windows;
using System.Windows.Controls;

namespace JBanzDevUIMatrix.Controls
{
    public class MatrixButton : Button
    {
        static MatrixButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatrixButton), new FrameworkPropertyMetadata(typeof(MatrixButton)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(MatrixButton), new PropertyMetadata(default(CornerRadius)));


    }
}
