using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SQLiteDb
{
    public class Categories
    {
        public int CategoryID { get; set; }
        public string Category { get; set; }

        public Categories(int categoryID, string category)
        {
            CategoryID = categoryID;
            Category = category;
        }
    }

    public class Products
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string Descripcion { get; set; }
        public int Price { get; private set; }
        public int Stock { get; set; }

        public Products(int productID, int categoryID, string descripcion, int price, int stock)
        {
            ProductID = productID;
            CategoryID = categoryID;
            Descripcion = descripcion;
            Price = price;
            Stock = stock;
        }
    }

    public class Payment
    {
        public int TransactionID { get; set; }
        public string TransactionType { get; set; }

        public Payment(int transactionID, string transactionType)
        {
            TransactionID = transactionID;
            TransactionType = transactionType;
        }
    }

    public class Users
    {
     public int UserID { get; set; }
     public int RoleID { get; set; }
     public string  Name { get; set; }
     public string Sex { get; set; }
     public string Username { get; set; }
     public int Password { get; set; }
     public int Cellphone { get; set; }

        public Users(int userID, int roleID, string name, string sex, string username, int password, int cellphone)
        {
            UserID = userID;
            RoleID = roleID;
            Name = name;
            Sex = sex;
            Username = username;
            Password = password;
            Cellphone = cellphone;
        }
    }

    public class Sales
    {
        public int RecordID { get; set; }
        public string Date { get; set; }
        public int UserID { get; set; }
        public string TranscactionType { get; set; }

        public Sales(int recordID, string date, int userID, string transcactionType)
        {
            RecordID = recordID;
            Date = date;
            UserID = userID;
            TranscactionType = transcactionType;
        }
    }

    public class SalesByProduct
    {
        public int UserID { get; set; }
        public int RecordID { get; set; }
        public int SoldQty { get; set; }
        public int Price { get; set; }

    }
   

    public partial class SQLiteConn
    {
        private static string databaseName = "Terminal_de_venta.db";

        
        public List<Categories> GetCategories()
        {
            List<Categories> categories = new List<Categories>();

            string query = $"SELECT * FROM categories ORDER BY id";

            using (SQLiteRecordSet rs = ExecuteQuery(query))
            {
                while (rs.NextRecord())
                {
                    int categoryID = rs.GetInt32("id");
                    string category = rs.GetString("category");

                    categories.Add(new Categories(categoryID, category));
                }
            }

            return categories;
        }

        public void DeleteCategory(int id)
        {

            string query = $"DELETE FROM categories WHERE id= {id}";
            ExecuteNonQuery(query);
        }

        public void AddCategory(Categories c)
        {

            string query = $"INSERT INTO categories id = {c.CategoryID}, category = {c.Category} ";
            ExecuteNonQuery(query);
        }


        public List<Products> GetProducts()
        {
            List<Products> products = new List<Products>();

            string query = $"SELECT * FROM products ORDER BY product_id";

            using (SQLiteRecordSet rs = ExecuteQuery(query))
            {
                while (rs.NextRecord())
                {
                    int productID = rs.GetInt32("product_id");
                    int categoryID = rs.GetInt32("category_id");
                    string descripcion = rs.GetString("description");
                    int price = rs.GetInt32("price");
                    int stock = rs.GetInt32("qty");

                    products.Add(new Products(productID, categoryID, descripcion, price, stock));
                }
            }

            return products;
        }

        public List<Products> GetProductsByID(int id)
        {
            List<Products> products = new List<Products>();

            string query = $"SELECT * FROM products ORDER BY product_id = {id}";

            using (SQLiteRecordSet rs = ExecuteQuery(query))
            {
                while (rs.NextRecord())
                {
                    int productID = rs.GetInt32("product_id");
                    int categoryID = rs.GetInt32("category_id");
                    string descripcion = rs.GetString("description");
                    int price = rs.GetInt32("price");
                    int stock = rs.GetInt32("qty");

                    products.Add(new Products(productID, categoryID, descripcion, price, stock));
                }
            }

            return products;
        }

        public List<Products> GetProductsByName(string name)
        {
            List<Products> products = new List<Products>();

            string query = $"SELECT * FROM products ORDER BY description = {name}";

            using (SQLiteRecordSet rs = ExecuteQuery(query))
            {
                while (rs.NextRecord())
                {
                    int productID = rs.GetInt32("product_id");
                    int categoryID = rs.GetInt32("category_id");
                    string descripcion = rs.GetString("description");
                    int price = rs.GetInt32("price");
                    int stock = rs.GetInt32("qty");

                    products.Add(new Products(productID, categoryID, descripcion, price, stock));
                }
            }

            return products;
        }

        public void UpdateProduct(int id, Products p)
        {
            string query = $"UPDATE product SET qty {p.Stock}, price {p.Price} WHERE id = {id}";
            ExecuteNonQuery(query);
        }

        public void DeleteProduct(int id)
        {

            string query = $"DELETE FROM product WHERE id= {id}";
            ExecuteNonQuery(query);
        }

        public void AddProduct(Products p)
        {

            string query = $"INSERT INTO products product_id = {p.ProductID}, category_id={p.CategoryID}, descripcion = {p.Descripcion}" +
                $"price = {p.Price}, stock = {p.Stock} ";
            ExecuteNonQuery(query);
        }


        public List<Sales> GetSalesByDate(string dates)
        {
            List<Sales> sales = new List<Sales>();

            string query = $"SELECT * FROM sales_product ORDER BY date_sale = {dates}";

            using (SQLiteRecordSet rs = ExecuteQuery(query))
            {
                while (rs.NextRecord())
                {
                    int recordID = rs.GetInt32("record_id");
                    string date = rs.GetString("date_sale");
                    int userID = rs.GetInt32("by_user");
                    string transcactionType = rs.GetString("transaction_type");
                    

                    sales.Add(new Sales(recordID,date,userID,transcactionType));
                }
            }

            return sales;
        }

        public List<Sales> GetSalesByUser(int id)
        {
            List<Sales> sales = new List<Sales>();

            string query = $"SELECT * FROM sales_product ORDER BY by_user = {id} ";

            using (SQLiteRecordSet rs = ExecuteQuery(query))
            {
                while (rs.NextRecord())
                {
                    int recordID = rs.GetInt32("record_id");
                    string date = rs.GetString("date_sale");
                    int userID = rs.GetInt32("by_user");
                    string transcactionType = rs.GetString("transaction_type");


                    sales.Add(new Sales(recordID, date, userID, transcactionType));
                }
            }

            return sales;
        }

        public List<Sales> GetSalesByTransactionType(string ty)
        {
            List<Sales> sales = new List<Sales>();

            string query = $"SELECT * FROM sales_product ORDER BY transaction_type={ty}";

            using (SQLiteRecordSet rs = ExecuteQuery(query))
            {
                while (rs.NextRecord())
                {
                    int recordID = rs.GetInt32("record_id");
                    string date = rs.GetString("date_sale");
                    int userID = rs.GetInt32("by_user");
                    string transcactionType = rs.GetString("transaction_type");


                    sales.Add(new Sales(recordID, date, userID, transcactionType));
                }
            }

            return sales;
        }

        public int GetSalesByRecord()
        {
            string query = "SELECT record_id FROM sales_product WHERE record_id =(SELECT max(record_id) From Sales)";
            ExecuteNonQuery(query);

            return dr.GetInt32("record_id");
        }

        public void AddSale(Sales s)
        {
            string query = $"INSERT INTO sales_product ";
        }

        public void DeleteSale(int id)
        {

            string query = $"DELETE FROM sales_product WHERE id= {id}";
            ExecuteNonQuery(query);
        }

    }
}

