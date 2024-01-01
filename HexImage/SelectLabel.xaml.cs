using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HexImage;

/// <summary>
/// UserControl1.xaml 的交互逻辑
/// </summary>
public partial class SelectLabel : UserControl
{
    public SelectLabel()
    {
        InitializeComponent();
    }

    public static DependencyProperty NoticeTextProperty =
        DependencyProperty.Register("NoticeText", typeof(string), typeof(SelectLabel));

    public string NoticeText
    {
        get => (string)GetValue(NoticeTextProperty);
        set { SetValue(NoticeTextProperty, value); }
    }

    public static DependencyProperty StatusProperty =
        DependencyProperty.Register("Status", typeof(bool), typeof(SelectLabel));

    public bool Status
    {
        get => (bool)GetValue(StatusProperty);
        set {
            SetValue(StatusProperty, value);
        }
    }
}