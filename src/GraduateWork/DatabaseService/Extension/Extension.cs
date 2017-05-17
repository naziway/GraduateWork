using Model;

namespace DatabaseService.Extension
{
    public static class Extension
    {

        //public static List<Orders> ToOrder(this List<OrderRecordModel> orders, int id, int code)
        //{
        //    return orders.Select(orderRecordModel => orderRecordModel.ToOrder(id++, code)).ToList();
        //}
        //public static Order ToOrder(this OrderRecordModel order, int id, int code)//TODO finish converting
        //{
        //    return new Order
        //    {
        //        Id = id,
        //        OrderKods = code,
        //        UserId = order.UserId,
        //        DeviceId = order.Device.Id,
        //        OrderStatus = order.OrderStatus.ToString(),
        //        OrderType = order.OrderType.ToString(),
        //    };
        //}

        public static OrderModel ToOrderRecord(this Orders order)
        {
            //int count = 19;
            //var a = new List<Order>();
            //a.Reverse();
            //a.Take(count + 16).Skip(count);
            return new OrderModel();
            //return new OrderRecordModel//TODO
            //{
            //    Id = order.Id,
            //    DeviceId = order.DeviceId,
            //    OrderKods = order.OrderKods,
            //    OrderType = order.OrderType,
            //    PartId = order.PartId,
            //    WorkId = order.WorkId
            //};
        }
     
    }
}