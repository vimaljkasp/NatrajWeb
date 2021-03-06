﻿using Platform.DTO;
using Platform.Repository;
using Platform.Sql;
using Platform.Utilities;
using System;
using System.Collections.Generic;


namespace Platform.Service
{
    public class DockCollectionReportService : IDockCollectionReportService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ResponseDTO DockCollectionSummaryByDate(DateTime collectionStartDate, DateTime collectionEndDate, int startShift, int endShift, int milkType)
        {

            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Data = unitOfWork.DockReportRepository.DockCollectionSummaryByDate(collectionStartDate, collectionEndDate, startShift, endShift, milkType);
            responseDTO.Status = true;
            responseDTO.Message = "Dock Collection Summary Report By Date";
            return responseDTO;
        }

        public ResponseDTO DockCollectionSummaryByVLC(int vlcId, DateTime collectionStartDate, DateTime collectionEndDate, int startShift, int endShift, int milkType)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var list = unitOfWork.DockReportRepository.DockCollectionSummaryByVLC(vlcId, collectionStartDate, collectionEndDate, startShift, endShift, milkType);
            if (vlcId == 0)
                list.VLCName = "ALL VLC";
            else
                list.VLCName = this.GetVLCName(vlcId);
            responseDTO.Data = list;
            responseDTO.Status = true;
            responseDTO.Message = "Dock Collection Summary Report By VLC";
            return responseDTO;
        }

        public ResponseDTO VLCExpenseSummaryByVLC(DateTime collectionStartDate, DateTime collectionEndDate)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            var list = unitOfWork.DockReportRepository.VLCExpenseSummary(collectionStartDate, collectionEndDate);
            responseDTO.Data = list;
            responseDTO.Status = true;
            responseDTO.Message = "VLC Expense Summary Report By VLC";
            return responseDTO;
        }


        //public ResponseDTO DockCollectionSummaryDetailByVLC(int vlcId, DateTime collectionStartDate, DateTime collectionEndDate)
        //{
        //    ResponseDTO responseDTO = new ResponseDTO();
        //    var list= unitOfWork.DockReportRepository.DockCollectionSummaryDetailByVLC(vlcId, collectionStartDate, collectionEndDate, startShift, endShift, milkType);
        //    list.VLCName = this.GetVLCName(vlcId);
        //    responseDTO.Data = list;
        //    responseDTO.Status = true;
        //    responseDTO.Message = "Dock Collection Summary Detail Report By VLC";
        //    return responseDTO;
        //}


        private string GetVLCName(int vlcId)
        {
            if (vlcId == 0)
                return "ALL VLC";
            var vlc = unitOfWork.VLCRepository.GetById(vlcId);
            if (vlc != null)
                return vlc.VLCName;
            else
                throw new PlatformModuleException("Given VLC does not exist");
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
