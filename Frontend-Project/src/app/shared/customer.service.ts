import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private http: HttpClient) {}
  GetCustomerList() {
   return this.http.get(environment.apiUrl + 'Customer/GetCustomerList').toPromise();
  }
}
