using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace Platform.Service
{
    public class VLCAdminService : IVLCAdminService , IDisposable
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public ResponseDTO UpdateMilkFixedRateDetails()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"http://service.natrajdairy.com/img/MilkRate.csv");
         //   Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:\MilkRate.csv");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            decimal clr = 0;
            decimal fat = 0;
            decimal value = 0;
            List<MilkRate> milkRates = new List<MilkRate>();
            for (int i = 2; i <= xlRange.Rows.Count; i++)
            {
                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    fat = Convert.ToDecimal(xlRange.Cells[i, 1].Value2.ToString());
                for (int j = 2; j <= xlRange.Columns.Count; j++)
                {
                   
                    if (xlRange.Cells[1, j] != null && xlRange.Cells[1, j].Value2 != null)
                        clr = Convert.ToDecimal(xlRange.Cells[1, j].Value2.ToString());
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        value = Convert.ToDecimal(xlRange.Cells[i, j].Value2.ToString());
                    milkRates.Add(new MilkRate { CLR = clr, Fat = fat, Rate = value });
                    Console.WriteLine("{0}\t{1}\t{2}", fat, clr, value);


                }
            }
            if (milkRates != null && milkRates.Count > 0)
            {
                unitOfWork.MilkRateRepository.Delete();
                unitOfWork.MilkRateRepository.AddMilkRates(milkRates);
                unitOfWork.SaveChanges();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();



            xlApp.Quit();
            responseDTO.Data = new object();
            responseDTO.Status = true;
            responseDTO.Message = "Milk Rate Detail has been updated Successfully";
            
            return responseDTO;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (unitOfWork != null)
                {
                    unitOfWork.Dispose();
                    unitOfWork = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
