using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public interface IVLCExpenseService
    {
        List<VLCExpenseDTO> GetAllVLCExpenses();

        ResponseDTO GetAllVLCExpensesByVLCId(int vlcId);

       

        ResponseDTO AddVLCExpenseDetail(VLCExpenseDTO vLCExpenseDTO);

        ResponseDTO UpdateVLCExpenseDetail(VLCExpenseDTO vLCExpenseDTO);

        ResponseDTO DeleteVLCExpenseDetail(int id);
    }
}
