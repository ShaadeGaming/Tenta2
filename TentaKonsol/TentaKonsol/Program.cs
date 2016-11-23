using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TentaKonsol.DataModelCodeFirst;

namespace TentaKonsol
{
    class Program
    {
        static string cns = ConfigurationManager.ConnectionStrings["NorthwindContext"].ConnectionString;
        static void Main(string[] args)
        {
            //uppgift1
            //ProductsByCategoryName("Confections");
            //uppgift2
            //SalesByTerritory();
            //uppgift3
            // EmployeesPerRegion();
            //uppgift4
            //OrdersPerEmployee();
            //uppgift5
            //CustomerNamesLongerThan25Chars();
            //uppgift6
            //inne i viewn
            //

        }

        

        static void ProductsByCategoryName(string categoryName)
        {

            //Uppgift1
          
            SqlConnection cn = new SqlConnection(cns);
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT ProductName, UnitPrice, UnitsInStock FROM products";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine(rd.GetString(0), rd.GetSqlMoney(1), rd.GetSqlInt16(2));  
            }
            rd.Close();
            cn.Close();

        }
        private static void SalesByTerritory()
        {
            //uppgift2
            SqlConnection cn = new SqlConnection(cns);
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT   TOP (5) Territories.TerritoryDescription, SUM([Order Details].Quantity) AS TotalSales FROM Territories INNER JOIN EmployeeTerritories ON Territories.TerritoryID = EmployeeTerritories.TerritoryID INNER JOIN  Employees ON EmployeeTerritories.EmployeeID = Employees.EmployeeID INNER JOIN Orders ON Employees.EmployeeID = Orders.EmployeeID INNER JOIN  [Order Details] ON Orders.OrderID = [Order Details].OrderID GROUP BY Territories.TerritoryDescription, [Order Details].UnitPrice*[Order Details].Quantity* 1 - [Order Details].Discount ORDER BY TotalSales DESC";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine(rd.GetString(0));
                Console.WriteLine(rd.GetInt32(1));
            }
            rd.Close();
            cn.Close();
        }
        private static void EmployeesPerRegion()
        {
            //uppgift3
            SqlConnection cn = new SqlConnection(cns);
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT Region.RegionDescription, COUNT(Employees.Region) AS EmployeesPerRegion FROM Employees INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID INNER JOIN Region ON Territories.RegionID = Region.RegionID GROUP BY Region.RegionID, Region.RegionDescription";
        SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine(rd.GetString(0));
                Console.WriteLine(rd.GetInt32(1));
            }
            rd.Close();
            cn.Close();
        }

        private static void OrdersPerEmployee()
        {
            //uppgift 4
            using (NorthwindContext cx = new NorthwindContext())
            {
                //Find hittar entiteten från ett givet primärnyckelvärde så den letar igenom exempel ALFKI och får tillbaks den i    var customer
                
                //ordersperemployee behöver eployeename och count på alla ordrar
                foreach (var emp in cx.Employees)
                {
                    
                    IQueryable<Orders> orders = cx.Orders.Where(c => c.OrderID== emp.EmployeeID);
                    Console.WriteLine(emp.FirstName);
                    Console.WriteLine(emp.Orders.Count); 
                    
                }
            }
            Console.ReadLine();
        }
        private static void CustomerNamesLongerThan25Chars()
        {
            //uppgift 5
            using (NorthwindContext cx = new NorthwindContext())
            {
                foreach (var cust in cx.Customers)
                {
                    if (cust.CompanyName.Count() > 25)
                    {
                        Console.WriteLine(cust.CompanyName);
                    }
                }
            }
        }


    }
}
