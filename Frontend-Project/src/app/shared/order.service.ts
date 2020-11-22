import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OrderItem } from './order-item.model';
import { Order } from './order.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  formData: Order;
  orderItemModel: Array<OrderItem> = [];
  constructor(private http: HttpClient) {}

  saveOrder() {
    var body = {
      ...this.formData,
      OrderItemModel: this.orderItemModel,
    };

    return this.http.post(environment.apiUrl + 'Order/SaveOrder', body);
  }
}
