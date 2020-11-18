import { Component, OnInit } from '@angular/core';
import { OrderItem } from 'src/app/shared/order-item.model';
import { Order } from 'src/app/shared/order.model';
import { OrderService } from 'src/app/shared/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent implements OnInit {
  orderModel: Order = new Order();
  orderItemModel: Array<OrderItem> = new Array<OrderItem>();
  constructor(private orderService: OrderService) {}

  ngOnInit(): void {}

  AddOrEditOrderItem(orderItemIndex, orderId) {}
}
