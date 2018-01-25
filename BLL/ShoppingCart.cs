using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for ShoppingCart
/// </summary>

namespace UserClass
{
    [Serializable]
    public class NewCartPacket
    {
        public string ProductID;
        public string ProductName;
        public string Productquantity;
        public int Quantity = 1;
        public float ActualPrice = 0F;
        public float OtherCharges = 0F;
        public float Discount = 0F;
        public float SingleProductPriceIncudingCharges = 0F;
        public float ShippingCharge = 0F;
        public float TotalPrice = 0F;
    }


    public static class ShoppingCart
    {

        public static bool AddToCart(string UserID, NewCartPacket product, out string errorDescription)
        {
            errorDescription = "";

            if (UserID == "")
                return false;

            string SQLstr;

            //check for existing item in product table
            string verifySQL = "Select TempID from " + customUtility.DBPrefix + "ShoppingBagTMP " +
                " where ProductID='" + product.ProductID + "'  and UserID='"+UserID+"'";

           
            
            DataTable tmpDt = customUtility.GetTableData(verifySQL).Tables[0];
            if (tmpDt.Rows.Count > 0)
            {
                   errorDescription = "Product already added to your cart.";
                
                return false;
            }


            SQLstr = "insert into " + customUtility.DBPrefix +
                "ShoppingBagTmp (UserID, productid, productname,ProductQuantity, price, qty, total, sessionid)" +
                " values('" + UserID + "'," + product.ProductID + ",'" + product.ProductName + "','"+product.Productquantity+"'," 
                + product.ActualPrice + "," + product.Quantity + "," + product.TotalPrice 
                + ",'" + HttpContext.Current.Session.SessionID + "')";

            customUtility.ExecuteNonQuery(SQLstr);
            return true;
        }

        public static bool UpdateCart(string ShoppingBagID, string SearchControl, GridView grdCart, out string errorString)
        {
            int ret;
            errorString = "";

            if (Int32.TryParse(ShoppingBagID, out ret))
            {
                GridViewRow gr = GetSelectedRow(ShoppingBagID, SearchControl, grdCart);
                if (gr == null)
                    return false;

                float Total = 0F;
                DataTable dtBag = customUtility.GetTableData("Select * from " + customUtility.DBPrefix + "ShoppingBagTMP where TempID=" + ShoppingBagID).Tables[0];
                if (dtBag.Rows.Count <= 0)
                    return false;

                DataRow dtbagRow = dtBag.Rows[0];

                DataTable dtProduct = customUtility.GetTableData("Select * from " + customUtility.DBPrefix + "Product where ID=" + dtbagRow["ProductID"].ToString()).Tables[0];
                if (dtProduct.Rows.Count <= 0)
                    return false;

                DataRow dtProductRow = dtProduct.Rows[0];

                int requestedQty = Convert.ToInt32(((TextBox)gr.FindControl("txtQty")).Text.ToString());

                if (requestedQty > (Convert.ToInt32(dtProductRow["Qty"].ToString()) - Convert.ToInt32(dtProductRow["SoldQty"].ToString())))
                {
                    //errorString = "Please enter a quantity less than " + Convert.ToString(Convert.ToInt32(dtProductRow["Qty"].ToString()) - Convert.ToInt32(dtProductRow["SoldQty"].ToString()));
                    errorString = "Sorry ! Requested quantity is not in stock";
                    grdCart.SelectedIndex = gr.RowIndex;
                    return false;
                }

                string updateSQL = "";

                updateSQL = "Update " + customUtility.DBPrefix + "ShoppingBagTMP set  Qty=" + requestedQty.ToString() + ", Total = Price * " + requestedQty.ToString() + " where TempID=" + ShoppingBagID;
                return customUtility.ExecuteNonQuery(updateSQL);

            }
            return true;
        }





        public static bool UpdateAllCart(string SearchControl, GridView grdCart, out string errorString)
        {
            int ret;
            bool retValue = false;
            errorString = "";

            foreach (GridViewRow gr in grdCart.Rows)
            {
                string ShoppingBagID = "0";

                ShoppingBagID = ((HiddenField)gr.FindControl(SearchControl)).Value;

                float Total = 0F;
                DataTable dtBag = customUtility.GetTableData("Select * from " + customUtility.DBPrefix + "ShoppingBagTMP where TempID=" + ShoppingBagID).Tables[0];
                if (dtBag.Rows.Count <= 0)
                    continue;

                DataRow dtbagRow = dtBag.Rows[0];

                DataTable dtProduct = customUtility.GetTableData("Select * from " + customUtility.DBPrefix + "catalog where ID=" + dtbagRow["ProductID"].ToString()).Tables[0];
                if (dtProduct.Rows.Count <= 0)
                    continue;

                DataRow dtProductRow = dtProduct.Rows[0];

                int requestedQty = Convert.ToInt32(((TextBox)gr.FindControl("txtQty")).Text.ToString());
                //int soldqty = 0;
                //if (dtProductRow["SoldQty"].ToString() != "")
                //    soldqty = Convert.ToInt32(dtProductRow["SoldQty"].ToString());
                //if (requestedQty > (Convert.ToInt32(dtProductRow["Qty"].ToString()) - soldqty))
                //{
                    // errorString += "<br/>Please enter a quantity less than " + Convert.ToString(Convert.ToInt32(dtProductRow["Qty"].ToString()) - soldqty);
                //    errorString = "Sorry ! Requested quantity is not in stock";
                //    grdCart.SelectedIndex = gr.RowIndex;
                //    continue;
                //}

                string updateSQL = "";

                updateSQL = "Update " + customUtility.DBPrefix + "ShoppingBagTMP set Qty=" + requestedQty.ToString() + ", Total = Price * " + requestedQty.ToString() + " where TempID=" + ShoppingBagID;
                customUtility.ExecuteNonQuery(updateSQL);

            }
            return true;
        }



        public static GridViewRow GetSelectedRow(string ShoppingBagID, string SearchControl, GridView grdCart)
        {
            foreach (GridViewRow gr in grdCart.Rows)
                if (Convert.ToInt64(((HiddenField)gr.FindControl(SearchControl)).Value) == Convert.ToInt64(ShoppingBagID))
                    return gr;

            return null;
        }



        public static bool EmptyCart()
        {
            if (HttpContext.Current.Session["UserID"] != null)
                return customUtility.ExecuteNonQuery("Delete from " + customUtility.DBPrefix + "ShoppingBagTMP where UserID=" + HttpContext.Current.Session["UserID"].ToString());
            else
                return false;
        }
    }



    //CreditCard Structure ...
    [Serializable()]
    public class CreditCardStruct
    {
        public string CardName = "";
        public string CardNumber = "";
        public string CSVNumber = "";
        public string CardType = "";
        public string ExpiryDate = "";

        public CreditCardStruct(string CardName, string CardNumber, string CSVNumber, string CardType, string ExpiryDate)
        {
            this.CardName = CardName;
            this.CardNumber = CardNumber;
            this.CSVNumber = CSVNumber;
            this.CardType = CardType;
            this.ExpiryDate = ExpiryDate;
        }
    }


    ///<summary>
    /// Credit Card Handler Class
    ///</summary>
   

    //discount information class
    public class discountinfo
    {
        public string CouponID;
        public float CouponDiscountAmount=0F;
        public string counponType;

           
    }

    ///<summary>
    /// Credit Card Handler Class
    ///</summary>
    [Serializable]
    public class CreditCardInfo
    {
        private string cname = "";
        private string cnumber = "";
        private string csv = "";
        private string ctype = "";
        private string cexpdate = "";
        private string hpin = "";

        public string CardHolderName { set { cname = value; } }
        public string CardNumber { set { cnumber = value; } }
        public string CSVNumber { set { csv = value; } }
        public string CardType { set { ctype = value; } }
        public string ExpiryDate { set { cexpdate = value; } }

        public CreditCardInfo()
        {
            hpin = GeneratePin();
        }

        protected string GeneratePin()
        {
            HttpContext.Current.Session[customUtility.DBPrefix + "HexPin"] = "1234";
            return HttpContext.Current.Session[customUtility.DBPrefix + "HexPin"].ToString();
        }

        public CreditCardStruct GetCreditCardInfo()
        {
            CreditCardStruct card = null;

            if (HttpContext.Current.Session[customUtility.DBPrefix + "HexPin"] == null) return card;

            if (hpin.Equals(HttpContext.Current.Session[customUtility.DBPrefix + "HexPin"].ToString(), StringComparison.CurrentCulture))
                card = new CreditCardStruct(this.cname, this.cnumber, this.csv, this.ctype, this.cexpdate);

            return card;
        }

    }

    //discount information class
    //public class discountinfo
    //{
    //    public string CouponID;
    //    public float CouponDiscountAmount = 0F;
    //    public string counponType;


    //}





}