import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/order.model';
import { OrderService } from '../shared/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {
  orderList: Order[];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderService.GetOrderList().then(res => this.orderList = res as Order[]);
  }
}
