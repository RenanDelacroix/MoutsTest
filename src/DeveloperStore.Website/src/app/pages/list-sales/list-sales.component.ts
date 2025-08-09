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
    public userService: UserService
  ) { }

  ngOnInit(): void {
    this.userName = this.userService.userName; // Nome fixo do usuário
    this.loadSales();
  }

  displayedColumns = ['number', 'customer', 'branch', 'subtotal', 'discount', 'total', 'status', 'createdAt', 'actions'];
  pageSize = 10;
  pageNumber = 1;

  onPageChange(event: any) {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadSales();
  }

  getSubtotal(sale: any): number {
    return sale.items.reduce((sum: number, item: any) => sum + item.subtotal, 0);
  }

  loadSales() {
    this.saleService.getSales().subscribe({
      next: (data) => {
        this.sales = data.items.map((sale: any) => ({
          ...sale,
          totalComDesconto: sale.total - sale.discount
        }));
        this.totalCount = data.totalCount;
      },
      error: (err) => {
        console.error('Erro ao carregar vendas', err);
      }
    });
  }

  cancelSale(id: string) {
    if (!confirm('Tem certeza que deseja cancelar esta venda?')) return;

    this.saleService.cancelSale(id).subscribe({
      next: (res) => {
        alert(res.message);
        this.loadSales(); // Atualiza lista após cancelar
      },
      error: (err) => {
        console.error('Erro ao cancelar venda', err);
        alert('Erro ao cancelar a venda.');
      }
    });
  }

}
