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

namespace CS_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow a = new TaskWindow();
            a.Show();
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string expression = text.Text;
            string strRPN = "";
            string[] RPN = ConvertToRPN(expression);
            foreach (string i in RPN){
                strRPN += i + " ";
            }
            int result = CalculateRPN(RPN);
            Console.WriteLine(result);
        }

        public int CalculateRPN(string[] arr){
            for (int i=0; i<arr.Length; i++){
                if (arr[i] == "+"){
                    int? a = null;
                    int? b = null;
                    for(int j = i-1; j >= 0; j--){
                        if (arr[j] != "$"){
                            if (a is null){
                                a = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                            }
                            else {
                                b = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                                break;
                            }
                        }
                    }
                    if (a is not null && b is not null){
                        arr[i] = Convert.ToString(a.Value+b.Value);
                    }
                    else {
                        arr[i] = "0";
                    }
                }
                else if (arr[i] == "*"){
                    int? a = null;
                    int? b = null;
                    for(int j = i-1; j >= 0; j--){
                        if (arr[j] != "$"){
                            if (a is null){
                                a = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                            }
                            else {
                                b = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                                break;
                            }
                        }
                    }
                    if (a is not null && b is not null){
                        arr[i] = Convert.ToString(a.Value*b.Value);
                    }
                    else {
                        arr[i] = "0";
                    }
                }
                else if (arr[i] == "/"){
                    int? a = null;
                    int? b = null;
                    for(int j = i-1; j >= 0; j--){
                        if (arr[j] != "$"){
                            if (a is null){
                                a = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                            }
                            else {
                                b = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                                break;
                            }
                        }
                    }
                    if (a is not null && b is not null){
                        arr[i] = Convert.ToString(b.Value/a.Value);
                    }
                    else {
                        arr[i] = "0";
                    }
                }
                else if (arr[i] == "-"){
                    int? a = null;
                    int? b = null;
                    for(int j = i-1; j >= 0; j--){
                        if (arr[j] != "$"){
                            if (a is null){
                                a = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                            }
                            else {
                                b = Convert.ToInt32(arr[j]);
                                arr[j]="$";
                                break;
                            }
                        }
                    }
                    if (a is not null && b is not null){
                        arr[i] = Convert.ToString(b.Value-a.Value);
                    }
                }
            }
            foreach (string i in arr){
                if (i != "$"){
                    return Convert.ToInt32(i);
                }
            }
            return 0;
        }


        // Перевод в обратную польскую запись ---------- НАЧАЛО ФУНКЦИИ
        public string[] ConvertToRPN(string expression)
        {
            string stack = "";
            string[] RPN = new string[expression.Length];
            int priority(char a){
                if (a == '*' || a == '/'){
                    return 3;
                }
                else if (a == '+' || a == '-'){
                    return 2;
                }
                else if (a == '('){
                    return 1;
                }
                else if (a == ')'){
                    return 4;
                }
                else {
                    return 0;
                }
            }
            int k = 0;
            for(int i=0; i<expression.Length; i++){
                try {
                    if (priority(expression[i]) == 4){
                        while (stack[stack.Length-1] != '(') {
                            RPN[k] = Convert.ToString(stack[stack.Length-1]);
                            k++;
                            stack = stack.Substring(0, stack.Length-1);
                        }
                        stack = stack.Substring(0, stack.Length-1);
                    }
                    else if (priority(expression[i]) == 3){
                        if (priority(stack[stack.Length-1]) < 3){
                            stack += expression[i];
                        }
                        else{
                            while (priority(stack[stack.Length-1]) == 3){
                                RPN[k] = Convert.ToString(stack[stack.Length-1]);
                                k++;
                                stack = stack.Substring(0, stack.Length-1);
                            }
                            stack += expression[i];
                        }
                    }
                    else if (priority(expression[i]) == 2){
                        if (priority(stack[stack.Length-1]) < 2){
                            stack += expression[i];
                        }
                        else{
                            while (priority(stack[stack.Length-1]) >= 2){
                                RPN[k] = Convert.ToString(stack[stack.Length-1]);
                                k++;
                                stack = stack.Substring(0, stack.Length-1);

                            }
                            stack += expression[i];
                        }
                    }
                    else if (priority(expression[i]) == 1){
                        stack += expression[i];
                    }
                    else {
                        try{
                            while (priority(expression[i]) == 0){
                                RPN[k] += Convert.ToString(expression[i]);
                                i++;
                            }
                            i--;
                            k++;
                        }
                        catch(IndexOutOfRangeException){
                            i--;
                            k++;
                        }
                    }
                }
                catch(IndexOutOfRangeException){
                    stack += expression[i];
                }
            }
            while (stack != ""){
                RPN[k] = Convert.ToString(stack[stack.Length-1]);
                k++;
                stack = stack.Substring(0, stack.Length-1);
            }
            return RPN;
        }
        // Перевод в обратную польскую запись ---------- КОНЕЦ ФУНКЦИИ
    }
}
