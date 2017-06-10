using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CargaDeDatosForecast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=dbserver;Initial Catalog=Grundfos_Scala;User ID=grundfos;Password=grundfos;");
            DataSet data = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter("SELECT " + dateTimePicker1.Value.Year + " AS Año, " + Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTimePicker1.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday) + " AS Sem, ISNULL(Compras.NumeroStock, Ventas.NumeroStock) AS NumeroStock, Compras.CantCom AS Compras, Ventas.CantVen AS Ventas, 0 AS Stock FROM (SELECT dbo.PC030100.PC03005 AS NumeroStock, SUM(dbo.PC030100.PC03011) AS CantCom FROM dbo.PC010100 INNER JOIN dbo.PC030100 ON dbo.PC010100.PC01001 = dbo.PC030100.PC03001 WHERE (" + dateTimePicker1.Value.Year + " = YEAR(dbo.PC010100.PC01015)) AND (" + Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTimePicker1.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday) + " = DATEPART(wk, dbo.PC010100.PC01015)) GROUP BY YEAR(dbo.PC010100.PC01015), DATEPART(wk, dbo.PC010100.PC01015), dbo.PC030100.PC03005) AS Compras FULL OUTER JOIN (SELECT dbo.OR030100.OR03005 AS NumeroStock, SUM(dbo.OR030100.OR03011) AS CantVen FROM dbo.OR010100 INNER JOIN dbo.OR030100 ON dbo.OR010100.OR01001 = dbo.OR030100.OR03001 WHERE (" + dateTimePicker1.Value.Year + " = YEAR(dbo.OR010100.OR01015)) AND (" + Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTimePicker1.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday) + " = DATEPART(wk, dbo.OR010100.OR01015)) GROUP BY YEAR(dbo.OR010100.OR01015), DATEPART(wk, dbo.OR010100.OR01015), dbo.OR030100.OR03005) AS Ventas ON Compras.NumeroStock = Ventas.NumeroStock ORDER BY NumeroStock", con);
            adap.Fill(data, "Datos");

            dataGridView1.DataSource = data;
            dataGridView1.DataMember = "Datos";
            

            textBox1.Text = Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTimePicker1.Value,CalendarWeekRule.FirstFourDayWeek,DayOfWeek.Sunday).ToString();
        }

    }
}