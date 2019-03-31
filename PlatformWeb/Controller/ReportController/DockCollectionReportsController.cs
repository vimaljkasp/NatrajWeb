using Platform.DTO;
using Platform.Utilities;
using System;
using System.Web.Http;

namespace PlatformWeb.Controller
{
    [Route("api/DockCollectionReports")]
    public class DockCollectionReportsController : ApiController
    {

        private readonly IDockCollectionReportService _dockCollectionReportService;

        public DockCollectionReportsController(IDockCollectionReportService dockCollectionReportService)
        {
            _dockCollectionReportService = dockCollectionReportService;
        }


      

        [Route("api/GetDockCollectionSummaryByDate/")]
        public IHttpActionResult GetDockCollectionSummaryByDate([FromUri] DateTime collectionStartDate, [FromUri] DateTime collectionEndDate,
            [FromUri] int startShift,[FromUri] int endShift,[FromUri] int milkType)
        {
            try
            {
                return Ok(_dockCollectionReportService.DockCollectionSummaryByDate(collectionStartDate, collectionEndDate,startShift,endShift,milkType));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }

        }

     
        [Route("api/GetDockCollectionSummaryByVLC/{id}")]
        public IHttpActionResult GetDockCollectionSummaryByVLC(int id, [FromUri] DateTime collectionStartDate, [FromUri] DateTime collectionEndDate, [FromUri] int startShift, [FromUri] int endShift, [FromUri] int milkType)
        {
            try
            {
                return Ok(_dockCollectionReportService.DockCollectionSummaryByVLC(id, collectionStartDate, collectionEndDate, startShift, endShift, milkType));
            }
            catch (PlatformModuleException ex)
            {
                return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
            }
        }

        //[Route("api/GetDockCollectionSummaryDetailByVLC/{id}")]
        //public IHttpActionResult GetDockCollectionSummaryDetailByVLC(int id, [FromUri] DateTime collectionStartDate, [FromUri] DateTime collectionEndDate)
        //{
        //    try
        //    {
        //        return Ok(_dockCollectionReportService.DockCollectionSummaryDetailByVLC(id, collectionStartDate, collectionEndDate));
        //    }
        //    catch (PlatformModuleException ex)
        //    {
        //        return Ok(ResponseHelper.CreateResponseDTOForException(ex.Message));
        //    }
        //}

    }
}
