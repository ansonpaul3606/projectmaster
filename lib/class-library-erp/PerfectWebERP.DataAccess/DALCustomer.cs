using PerfectWebERP.Business;
using PerfectWebERP.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerfectWebERP.DataAccess
{
    public class DALCustomer: IDALCustomer
    {
        public BLCommon.Output Add(BLCustomer.Customer data)
        {
            try
            {




                //------if updated true
                return new BLCommon.Output
                {
                    IsProcess = true,
                    Message = "Success",
                    Data=new BLCustomer.Customer
                    {
                        CustomerID=1
                    }
                };

                //----- if some other issue
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = "Insert to database failed",
                    Code = "DBError"
                };
            }
            catch (Exception ex)
            {
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = $"Error :{ex.Message}",
                    Code="Exception"
                };
            }
        }

        public BLCommon.Output Update(BLCustomer.UpdateCustomer data)
        {
            try
            {




                //------if updated true
                return new BLCommon.Output
                {
                    IsProcess = true,
                    Message = "Success",
                    Data = new BLCustomer.Customer
                    {
                        CustomerID = 1
                    }
                };

                //----- if some other issue
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = "Insert to database failed",
                    Code = "DBError"
                };
            }
            catch (Exception ex)
            {
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = $"Error :{ex.Message}",
                    Code = "Exception"
                };
            }
        }

        public BLCommon.Output GetAll()
        {
            try
            {
                List<BLCustomer.Customer> customerList = new List<BLCustomer.Customer>();



                //------if updated true
                return new BLCommon.Output
                {
                    IsProcess = true,
                    Message = "Success",
                    Data = customerList
                };

                //----- if some other issue
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = "Insert to database failed",
                    Code = "DBError"
                };
            }
            catch (Exception ex)
            {
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = $"Error :{ex.Message}",
                    Code = "Exception"
                };
            }
        }

        public BLCommon.Output GetByID(int CustomerID)
        {
            try
            {
                BLCustomer.Customer customer = new BLCustomer.Customer();



                //------if updated true
                return new BLCommon.Output
                {
                    IsProcess = true,
                    Message = "Success",
                    Data = customer
                };

                //----- if some other issue
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = "Insert to database failed",
                    Code = "DBError"
                };
            }
            catch (Exception ex)
            {
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = $"Error :{ex.Message}",
                    Code = "Exception"
                };
            }
        }
        public BLCommon.Output Delete(int CustomerID)
        {
            try
            {




                //------if updated true
                return new BLCommon.Output
                {
                    IsProcess = true,
                    Message = "Success"
                };

                //----- if some other issue
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = "Insert to database failed",
                    Code = "DBError"
                };
            }
            catch (Exception ex)
            {
                return new BLCommon.Output
                {
                    IsProcess = false,
                    Message = $"Error :{ex.Message}",
                    Code = "Exception"
                };
            }
        }
    }
}
