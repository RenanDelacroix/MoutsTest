import { Component, OnInit } from '@angular/core';
import { SaleService } from '../../services/sale.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-list-sales',
  templateUrl: './list-sales.component.html'
})
export class ListSalesComponent implements OnInit {
  sales: any[] = [];
  totalCount = 0;
  userName = '';

  constructor(
    private saleService: SaleService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.userName = this.userService.userName; // Nome fixo do usuário
    this.loadSales();
  }

  loadSales() {
    this.saleService.getSales().subscribe({
      next: (data) => {
        this.sales = data.items.map((sale: any) => ({
          ...sale,
          branchName: sale.branchName || (sale.branch ? sale.branch.name : ''),
          customerName: this.userName // sempre o mesmo usuário fixo
        }));
        this.totalCount = data.totalCount;
      },
      error: (err) => {
        console.error('Erro ao carregar vendas', err);
      }
    });
  }
}
