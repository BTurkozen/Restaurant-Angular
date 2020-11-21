import { Component, OnInit, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Item } from 'src/app/shared/item.model';
import { ItemService } from 'src/app/shared/item.service';
import { OrderItem } from 'src/app/shared/order-item.model';
import { OrderService } from 'src/app/shared/order.service';

@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styleUrls: ['./order-items.component.css'],
})
export class OrderItemsComponent implements OnInit {
  formData: OrderItem;
  itemList: Item[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data,
    public dialogRef: MatDialogRef<OrderItemsComponent>,
    private itemService: ItemService,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.itemService
      .getItemList()
      .then((res) => (this.itemList = res as Item[]));

    this.formData = {
      OrderItemId: null,
      OrderId: 0,
      ItemId: 0,
      ItemName: '',
      Quantity: 0,
      Total: 0,
      Price: 0,
    };
  }

  updatePrice(ctrl) {
    if (ctrl.selectedIndex === 0) {
      this.formData.Price = 0;
      this.formData.ItemName = '';
    } else {
      this.formData.Price = this.itemList[ctrl.selectIndex - 1].price;
      this.formData.ItemName = this.itemList[ctrl.selectIndex - 1].name;
    }
    this.updateTotal();
  }

  updateTotal() {
    this.formData.Total = parseFloat(
      (this.formData.Quantity * this.formData.Price).toFixed(2)
    );
  }

  onSubmit(form: NgForm) {
    this.orderService.orderItemModel.push(form.value);
    this.dialogRef.close();
  }
}
