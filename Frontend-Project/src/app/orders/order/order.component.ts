import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  isValid: boolean = true;
  sOrderItemId: number;
  sOrderId: number;
  constructor(
    private orderService: OrderService,
    private matDialog: MatDialog,
    private customerService: CustomerService,
    private toastr: ToastrService,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.orderItemModel = this.orderService.orderItemModel;

    this.customerService
      .GetCustomerList()
      .then((res) => (this.customerList = res as Customer[]));
  }

  resetForm(form?: NgForm) {
    if ((form = null)) {
      form.resetForm();
      this.orderModel = {
        OrderId: 0,
        OrderNo: Math.floor(100000 + Math.random() * 900000).toString(),
        CustomerId: 0,
        PaymentMethod: '',
        GrantTotal: 0,
        CustomerName : '',
      };
    }
    this.orderItemModel = [];
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

  validateForm() {
    this.isValid = true;
    if (this.orderModel.CustomerId === 0) {
      this.isValid = false;
    } else if (this.orderItemModel.length === 0) {
      this.isValid = false;
    }
    return this.isValid;
  }

  onSubmit(form: NgForm) {
    if (this.validateForm()) {
      this.orderService.saveOrder(this.orderModel).subscribe((res) => {
        this.resetForm();
        this.toastr.success('Tebrikler', 'Siparişiniz Başarıyla Oluştu...');
        this.route.navigate(['/orders']);
      });
    }
  }
}
