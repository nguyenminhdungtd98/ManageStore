using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21880024.Entities
{
    public class ErrorMessage
    {
        public const string DUPLICATE = "Sản phẩm bạn vừa thêm đã tồn tại";
        public const string ERROR = "Đã có lỗi xảy ra";
        public const string SUCCESS = "Thao tác được thực hiện thành công";
        public const string ZERO = "Mã của sản phẩm phải khác 0";
        public const string NOT_FOUND = "Không tồn tại";
        public const string NULL_VALUE = "Giá trị NULL";
        public const string OUTSTOCK = "Sản phẩm trong kho không còn đủ số lượng";
        public const string EMPTY_PRODUCTTYPE = "Vui lòng thêm loại hàng trước khi thêm mặt hàng";
        public const string PERMISION = "Sản phẩm thuộc loại hàng này đã tồn tại trong hóa đơn";
    }
}
