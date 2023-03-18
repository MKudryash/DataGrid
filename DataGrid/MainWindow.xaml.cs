using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DataGrid
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /*
    INotifyPropertyChanged -  Для поддержки однократного или двухкратного привязки, позволяющего вашим
    целевым свойствам привязки автоматически отражать динамические изменения источника привязки
     */
    public partial class MainWindow : Window, INotifyPropertyChanged
    {    
        public event PropertyChangedEventHandler PropertyChanged; //Обязательно для реализации
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private int row = 0, column = 0;
        public object Matrix { get; set; }   //Его биндим к DataGrid

        private void TBRow_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                row = Convert.ToInt32(TBRow.Text); //Получаем количество строк
                completionDataGrid(); //Вызываем метод отрисовки
            }
            catch { }
        }
        private void TBColumn_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                column = Convert.ToInt32(TBColumn.Text); //Получаем количество столбцов
                completionDataGrid();//Вызываем метод отрисовки
            }
            catch { }
        }
        private void completionDataGrid()//Метод для заполнения пустыми значенями 
        {
            if (column != 0 && row != 0)//Проверка на не нудевые значения
            {
                DataTable dt = new DataTable();// создаем объект DataTable
                for (var i = 0; i < column; i++)
                    dt.Columns.Add(new DataColumn("c" + i, typeof(string))); //Добавляем нужное количество столбцов
                for (var i = 0; i < row; i++) ////Добавляем нужное количество строк
                {
                    var r = dt.NewRow();
                    for (var c = 0; c < column; c++)
                        r[c] = 0;
                    dt.Rows.Add(r);
                }
                Matrix = dt.DefaultView; //Кладем все в object
                PropertyChanged(this, new PropertyChangedEventArgs("Matrix")); //Вызываем изменение
            }
        }

        private int[,] massiv; //Массив, который в дальнейшем будем заполнять и использовать в задаче

        private void btnSaveButton_Click(object sender, RoutedEventArgs e) //Просиходит когда сохраняем массив
        {
            massiv = new int[row, column];
            int indexRow = 0;
            for (int i = 0; i < row; i++)
            {
                DataRowView rowView = dataGrid.Items[i] as DataRowView;//Получаем строку
                for (int j = 0; j < column; j++)
                {
                    massiv[indexRow, j] = Convert.ToInt32(rowView[j]);//Получаем значение в строке
                }
                indexRow++;
            }
        }
    }
}
