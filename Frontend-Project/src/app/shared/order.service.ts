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
    this.formData.customerId = orderModel.customerId;
    this.formData.grandTotal = orderModel.grandTotal;
    this.formData.orderId = orderModel.orderId;
    this.formData.orderNo = orderModel.orderNo;
    this.formData.paymentMethod = orderModel.paymentMethod;

    var body = {
      OrderSubDto: this.formData,
      OrderItemModelDtos: this.orderItemModel,
    };

    return this.http.post(environment.apiUrl + '/api/Order/SaveOrder', body);
  }
  GetOrderList() {
    var data = this.http
      .get(environment.apiUrl + '/api/Order/GetOrders')
      .toPromise();
    console.log('Get Order List data =>', data);

    return data;
  }

  DeleteOrder(orderId: number) {
    return this.http
      .delete(environment.apiUrl + '/api/Order/DeleteOrder/' + orderId)
      .toPromise();
  }
}
