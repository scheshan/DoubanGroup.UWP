using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DoubanGroup.Client.Controls
{
    [TemplatePart(Name = "RefreshButton", Type = typeof(Button))]
    public class RefreshGridView : GridView
    {
        private Button refreshButton;

        private ScrollViewer scrollViewer;

        public Visibility RefreshButtonVisibility
        {
            get { return (Visibility)GetValue(RefreshButtonVisibilityProperty); }
            set { SetValue(RefreshButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty RefreshButtonVisibilityProperty =
            DependencyProperty.Register("RefreshButtonVisibility", typeof(Visibility), typeof(RefreshGridView), new PropertyMetadata(Visibility.Visible));

        private static void OnRefreshButtonVisibilityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as RefreshGridView;
            if (control == null || control.refreshButton == null)
            {
                return;
            }

            Visibility value = (Visibility)e.NewValue;

            control.refreshButton.Visibility = value;
        }

        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }
        
        public static readonly DependencyProperty RefreshCommandProperty =
            DependencyProperty.Register("RefreshCommand", typeof(ICommand), typeof(RefreshGridView), new PropertyMetadata(null));

        public object RefreshCommandParameter
        {
            get { return (object)GetValue(RefreshCommandParameterProperty); }
            set { SetValue(RefreshCommandParameterProperty, value); }
        }
        
        public static readonly DependencyProperty RefreshCommandParameterProperty =
            DependencyProperty.Register("RefreshCommandParameter", typeof(object), typeof(RefreshGridView), new PropertyMetadata(null));

        public event RoutedEventHandler Refresh;

        public RefreshGridView()
        {
            this.DefaultStyleKey = typeof(RefreshGridView);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.scrollViewer = this.GetTemplateChild("ScrollViewer") as ScrollViewer;
            refreshButton = this.GetTemplateChild("RefreshButton") as Button;
            if (refreshButton != null)
            {
                refreshButton.Click += RefreshButton_Click;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            var handler = this.Refresh;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }

            var command = this.RefreshCommand;
            if (command != null)
            {
                command.Execute(this.RefreshCommandParameter);
            }
        }
    }
}
