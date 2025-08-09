import { Component, ViewChild } from '@angular/core';
import { UserService } from './services/user.service';
import { ListSalesComponent } from './pages/list-sales/list-sales.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DeveloperStore.Website';
  @ViewChild('salesList') salesList!: ListSalesComponent;

  onTabChange(index: number) {
    if (index === 1 && this.salesList) { // Aba "Listar Vendas"
      this.salesList.loadSales();
    }
  }
  constructor(public userService: UserService) { }
}
