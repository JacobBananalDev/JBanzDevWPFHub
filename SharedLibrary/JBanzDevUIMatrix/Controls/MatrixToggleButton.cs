using System.Windows;
using System.Windows.Controls.Primitives;

namespace JBanzDevUIMatrix.Controls
{
    public class MatrixToggleButton : ToggleButton
    {
        static MatrixToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatrixToggleButton), new FrameworkPropertyMetadata(typeof(MatrixToggleButton)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(MatrixToggleButton), new PropertyMetadata(default(CornerRadius)));

    }
}
