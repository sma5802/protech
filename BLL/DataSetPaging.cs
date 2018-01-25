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
/// Summary description for DataSetPaging
/// </summary>
namespace UserClass
{
    [Serializable]
    public class DataSetPaging
    {
        DataSet ds;
        long pageSize = 8; //(This one will hold the no of records return i mean "no. of records per page").
        long pageIndex = 0; //(This one is for checking the current page). 
        long pageCount = 0;
        long outPageIndex = 0;
        long currentRecordPosition = 0;
        long maxRecordPosition = 0;
        long recordCount = 0;

        //Read-only properties..
        public long PageIndex { get { return outPageIndex; } }
        public long OutPageIndex { get { return outPageIndex; } }
        public long PageCount { get { return pageCount; } }
        public long PageSize { get { return pageSize; } }
        public long RecordCount { get { return recordCount; } }
        public long CurrentRecordPosition { get { return currentRecordPosition; } }
        public long MaxRecordPosition { get { return maxRecordPosition; } }


        //contructors..
        public  DataSetPaging(DataSet ds)
        {
            this.ds = ds;
        }

        public DataSetPaging(DataSet ds, int pageSize)
        {
            this.ds = ds;
            if (pageSize > 0)
                this.pageSize = pageSize;
        }


        public  DataSet BindList(long pageIndex)
        {

            try
            {
                long count = 0;

                //for paging
                long page = 0;
                recordCount = ds.Tables[0].Rows.Count;
                pageCount = (long)(Math.Ceiling((double)(recordCount) / pageSize));

                //checking the whether the pageIndex value is not <First and >Last.
                //And if it is then assigning the default values for pageIndex and page variables. 
                if (((pageIndex - 1) <= pageCount) && (pageIndex - 1) >= 0)
                {
                    //If the pageIndex is >=first and =<last then assigning the start position
                    //eg. if pageIndex = 2 then value of 'page' = 8. So in the loop it will add rows to the table
                    //from the 8 th row.
                    page = pageSize * (pageIndex - 1);
                }
                else if ((pageIndex - 1) < 0)
                {
                    //Assigning default values. 
                    page = 0;
                    pageIndex = 1;
                }
                else if ((pageIndex - 1) > pageCount)
                {
                    //Assigning default values. 
                    page = pageSize * (pageCount - 1);
                    pageIndex = pageCount;
                }
                currentRecordPosition = page + 1;
                this.pageIndex = pageIndex;
                maxRecordPosition = page + pageSize;
                if (maxRecordPosition > recordCount)
                    maxRecordPosition = recordCount;

                //creating a data table for adding the required rows or u can clone the existing table.
                DataSet dsPaged = new DataSet();
                DataTable dtTbl = new DataTable("PagedTable");
                DataColumn newCol;
                foreach (DataColumn nColumn in ds.Tables[0].Columns)
                {
                    newCol = new DataColumn(nColumn.ColumnName, nColumn.DataType);
                    dtTbl.Columns.Add(newCol);//For storing image id. 
                }
                //adding the required rows to the datatable dtTbl. 
                foreach (DataRow nRow in ds.Tables[0].Rows)
                {
                    //if the page=8 and pageIndex =2 then
                    //rows between 8 to 16(if exists) will be added to the new table. 
                    if (count >= page && count < (pageSize * pageIndex))
                    {
                        //Adding rows to the datatable 'dtImg'. 
                        dtTbl.Rows.Add(nRow.ItemArray);
                    }
                    count++;
                }
                outPageIndex = pageIndex;
                dsPaged.Tables.Add(dtTbl);
                return dsPaged;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}