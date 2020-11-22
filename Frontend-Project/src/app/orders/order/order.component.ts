import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Customer } from 'src/app/shared/customer.model';
import { CustomerService } from 'src/app/shared/customer.service';
import { OrderItem } from 'src/app/shared/order-item.model';
import { Order } from 'src/app/shared/order.model';
import { OrderService } from 'src/app/shared/order.service';
import { OrderItemsComponent } from '../order-items/order-items.component';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent implements OnInit {
  orderModel: Order = new Order();
  orderItemModel: Array<OrderItem> = new Array<OrderItem>();
  customerList: Customer[];
  constructor(
    private orderService: OrderService,
    private matDialog: MatDialog,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.orderItemModel = this.orderService.orderItemModel;

    this.customerService.GetCustomerList().then(res => this.customerList = res as Customer[]);
  }

  AddOrEditOrderItem(orderItemIndex, orderId) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;

    dialogConfig.disableClose = true;

    dialogConfig.width = '60%';

    dialogConfig.data = { orderItemIndex, orderId };

    this.matDialog
      .open(OrderItemsComponent, dialogConfig)
      .afterClosed()
      .subscribe((res) => {
        this.updateGrandTotal();
      });
  }

  DeleteOrderItem(orderItemIndex, orderId) {
    this.orderService.orderItemModel.splice(orderItemIndex, 1);
    this.updateGrandTotal();
  }

  updateGrandTotal() {
    this.orderModel.GrantTotal = this.orderService.orderItemModel.reduce(
      (prev, curr) => {
        return prev + curr.Total;
      },
      0
    );
    this.orderModel.GrantTotal = parseFloat(
      this.orderModel.GrantTotal.toFixed(2)
    );
  }
}
