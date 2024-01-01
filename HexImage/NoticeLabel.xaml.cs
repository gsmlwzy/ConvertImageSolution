using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
/// NoticeLabel.xaml 的交互逻辑
/// </summary>
public partial class NoticeLabel : UserControl, ICommandSource {
    public NoticeLabel() {
        InitializeComponent();
    }

    public static DependencyProperty InputTextProperty =
        DependencyProperty.Register("InputText", typeof(string), typeof(NoticeLabel));

    public string InputText {
        get => (string)GetValue(InputTextProperty);
        set => SetValue(InputTextProperty, value);
    }

    public static DependencyProperty NoticeTextProperty =
        DependencyProperty.Register("NoticeText", typeof(string), typeof(NoticeLabel));

    public string NoticeText {
        get => (string)GetValue(NoticeTextProperty);
        set => SetValue(NoticeTextProperty, value);
    }

    public static DependencyProperty CommandProperty =
        DependencyProperty.Register("Command", typeof(ICommand), typeof(NoticeLabel));

    public ICommand Command {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static DependencyProperty CommandParameterProperty =
        DependencyProperty.Register("CommandParameter", typeof(object), typeof(NoticeLabel));

    public object CommandParameter {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static DependencyProperty CommandTargetProperty =
        DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(NoticeLabel));

    public IInputElement CommandTarget {
        get => (IInputElement)GetValue(CommandTargetProperty);
        set => SetValue(CommandTargetProperty, value);
    }

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e) {
        TextBox tb = sender as TextBox;
        InputText = tb.Text;

        ICommand command = Command;
        object parameter = CommandParameter;
        IInputElement target = CommandTarget;
        
        if (command is null) return;
        
        RoutedCommand routedCmd = command as RoutedCommand;
        if (routedCmd != null && routedCmd.CanExecute(parameter, target)) {
            routedCmd.Execute(parameter, target);
        }
        else if (command.CanExecute(parameter)) {
            command.Execute(parameter);
        }
    }
}