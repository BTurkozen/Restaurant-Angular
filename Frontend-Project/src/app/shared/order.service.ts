import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OrderItem } from './order-item.model';
import { Order } from './order.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  formData: Order = new Order();
  orderItemModel: Array<OrderItem> = [];
  constructor(private http: HttpClient) {}

  saveOrder(orderModel: Order) {
    this.formData.CustomerId = orderModel.CustomerId;
    this.formData.GrantTotal = orderModel.GrantTotal;
    this.formData.OrderId = orderModel.OrderId;
    this.formData.OrderNo = orderModel.OrderNo;
    this.formData.PaymentMethod = orderModel.PaymentMethod;

    var body = {
      OrderSubDto: this.formData,
      orderItemModelDtos: this.orderItemModel,
    };

    return this.http.post(environment.apiUrl + 'Order/SaveOrder', body);
  }
  GetOrderList() {
    var data = this.http
      .get(environment.apiUrl + 'Order/GetOrders')
      .toPromise();
    console.log('Get Order List data =>', data);

    return data;
  }
}
