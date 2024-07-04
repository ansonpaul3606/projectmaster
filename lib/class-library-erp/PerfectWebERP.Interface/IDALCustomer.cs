using PerfectWebERP.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerfectWebERP.Interface
{
    public interface IDALCustomer
    {
        /// <summary>
        /// Function to add New customer
        /// </summary>
        /// <param name="newData"> New Custoemr Data</param>
        /// <returns> </returns>
        BLCommon.Output Add(BLCustomer.Customer newData);
        /// <summary>
        /// Function to update a customer
        /// </summary>
        /// <param name="data"> new and old value from front end</param>
        /// <returns></returns>
        BLCommon.Output Update(BLCustomer.UpdateCustomer data);
        /// <summary>
        /// Function to get all customer list
        /// </summary>
        /// <returns></returns>
        BLCommon.Output GetAll();
        /// <summary>
        /// function to get customer by ID
        /// </summary>
        /// <param name="CustomerID">Primarykey/cutomer id</param>
        /// <returns></returns>
        BLCommon.Output GetByID(int CustomerID);
        /// <summary>
        /// Function to delete a customer
        /// </summary>
        /// <param name="CustomerID"> Customer ID /primarykey</param>
        /// <returns></returns>
        BLCommon.Output Delete(int CustomerID);
    }
}
