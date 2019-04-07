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
        ResponseDTO GetVLCExpensesById(int vLCExpenseId);
        ResponseDTO GetAllVLCExpensesByVLCId(int vlcId);

        ResponseDTO UpdateMachineAndHouseRentExpenseForALLVLC(DateTime expenseMonth);

        ResponseDTO AddVLCExpenseDetail(VLCExpenseDTO vLCExpenseDTO);

        ResponseDTO UpdateVLCExpenseDetail(VLCExpenseDTO vLCExpenseDTO);

        ResponseDTO DeleteVLCExpenseDetail(int id);
    }
}
